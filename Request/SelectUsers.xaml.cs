using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System;
using System.Windows.Controls.Primitives;

namespace Request
{
    /// <summary>
    /// Логика взаимодействия для SelectUsers.xaml
    /// </summary>
    public partial class SelectUsers : UserControl
    {
        private MyDict users;
        MyDict usersSelected = new MyDict();
        MyDict oldUsersSelected = new MyDict();
        private SortedList<int , User> usersFound = new SortedList<int, User>();

        public SelectUsers()
        {
            InitializeComponent( );
            Visibility = Visibility.Hidden;

        }
        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;
        private ColumnDefinition _column;
        #region Message

        public string Message
        {
            get;
            set;
        }
        #endregion

        public void SetParent( UIElement parent, ColumnDefinition column, MyDict allUsers , MyDict editListUsers )
        {
            users = allUsers;
            usersSelected = editListUsers;
            oldUsersSelected = editListUsers;
            _column = column;
            _parent = parent;
        }

        public bool ShowHandlerDialog( string message )
        {
            Message = message;
            refreshUserToList( );
            Visibility = Visibility.Visible;
            Keyboard.Focus( inputArea );
            inputArea.Text = "";
            autoListUser.Items.Clear( );
            autoListUserSelected.Items.Clear( );
            refreshUserToList( );

            _column.IsEnabled = false;

            _hideRequest = false;
            while( !_hideRequest )
            {
                // HACK: Stop the thread if the application is about to close
                if( this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished )
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background ,
                    new ThreadStart( delegate
                    {
                    } ) );
                Thread.Sleep( 20 );
            }

            return _result;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _column.IsEnabled = true;
        }

        private void OkButton_Click( object sender , RoutedEventArgs e )
        {
            _result = true;
            HideHandlerDialog( );
            
        }

        private void CancelButton_Click( object sender , RoutedEventArgs e )
        {
            usersSelected = oldUsersSelected;
            _result = false;
            HideHandlerDialog( );
        }

#region Список поиска
        private void TextBox_TextChanged( object sender , TextChangedEventArgs e )
        {
            autoListUser.Items.Clear( );
            if( !String.IsNullOrWhiteSpace( inputArea.Text ) )
            {
                string search = inputArea.Text;
                User op = new User( );
                usersFound.Clear( );
                string[] info = new string[ 2 ];
                int i = 0;
                int n = search.Length;
                foreach( string io in users.Keys( ) )
                {
                    if( n <= io.Length )
                    {
                        string a = io.Substring( 0 , n );
                        if( a.ToLower( ) == search.ToLower( ) )
                        {
                            users.TryGetValue( io , out op );

                            info[ 0 ] = op.FullName;
                            info[ 1 ] = "т.№ " + op.LoginName + ", " + op.NameGroup + ", " + op.Rank;
                            usersFound.Add( i , op );
                            addListItem( info , (i % 2) );
                            i++;
                        }
                    }
                }
            }
               
        }

        private void addListItem( string [] NameAndInfo, int backgroundInventor)
        {
            #region ПРИМЕР
            //< ListBox >
            //    < WrapPanel >
            //        < WrapPanel Orientation = "Vertical" > 
            //            < TextBlock >
            //                "Имя:"
            //            </ TextBlock > 
            //            < TextBlock >
            //                "Инфо: "
            //            </ TextBlock > 
            //        </ WrapPanel > 
            //        < WrapPanel Orientation = "Vertical" >  
            //            < TextBlock >
            //                UserName
            //            </ TextBlock >  
            //            < TextBlock >
            //                UserInfo
            //            </ TextBlock >  
            //        </ WrapPanel >  
            //    </ WrapPanel >  
            //</ ListBox >
            #endregion
            ListBoxItem newElement = new ListBoxItem( );
            newElement.BorderBrush = Brushes.Aqua;
            newElement.Background = ( backgroundInventor == 0) ? Brushes.LightGray : Brushes.White;
            WrapPanel newWrapHorizontal = new WrapPanel( );
            WrapPanel newWrapVerticalHead = new WrapPanel { Orientation = Orientation.Vertical };
            WrapPanel newWrapVerticalInfo = new WrapPanel { Orientation = Orientation.Vertical };
            TextBlock newNameHead = new TextBlock { Text = "Имя:" };
            TextBlock newInfoHead = new TextBlock { Text = "Инфо: " , Foreground = Brushes.Orange };
            TextBlock newNameInfo = new TextBlock { Text = NameAndInfo[0] , Foreground = Brushes.Green };
            TextBlock newInfoInfo = new TextBlock { Text = NameAndInfo[1], FontWeight = FontWeights.Light};
            newWrapVerticalHead.Children.Add( newNameHead );
            newWrapVerticalHead.Children.Add( newInfoHead );

            newWrapVerticalInfo.Children.Add( newNameInfo );
            newWrapVerticalInfo.Children.Add( newInfoInfo );

            newWrapHorizontal.Children.Add( newWrapVerticalHead );
            newWrapHorizontal.Children.Add( newWrapVerticalInfo );

            newElement.Content = newWrapHorizontal;

            autoListUser.Items.Add( newElement );
            autoListUser.Items.MoveCurrentToFirst( );
        }
        #endregion

#region Удалить пользователя
        private void DelButton_Click( object sender , RoutedEventArgs e )
        {

            int item = autoListUserSelected.SelectedIndex;
            if( item > -1 & (usersSelected.Count() >= item))
            {
                usersSelected.RemoveAt( item );
                refreshUserToList( );
            }

        }
        #endregion

#region Добавить пользователя
        private void AddButton_Click( object sender , RoutedEventArgs e )
        {
            int item = autoListUser.SelectedIndex;
            if( item > -1 )
            {
                User op = new User( );
                usersFound.TryGetValue( item , out op );
                if( usersSelected.ContainsKey( op.FullName ) )
                {
                    MessageBox.Show( "Пользователь уже был добавлен" , "Инфо" , MessageBoxButton.OK , MessageBoxImage.Information );
                }
                else if( usersSelected.Count( ) < 5 )
                {
                    usersSelected.Add( op.FullName , op );
                    refreshUserToList( );
                }
            }
            else if( inputArea.Text.Length > 1)
            {
                User op = new User( );
                op.FullName = inputArea.Text;
                op.LoginName = "";
                op.NameGroup = "";
                op.Rank = "";
                usersSelected.Add( op.FullName , op );
                refreshUserToList( );
            }
        }

        private void refreshUserToList()
        {
            autoListUserSelected.Items.Clear( );
            int i = 0;
            foreach( string op in usersSelected.Keys() )
            {
                ListBoxItem newElement = new ListBoxItem( );
                WrapPanel newWrap = new WrapPanel( );
                newElement.BorderBrush = Brushes.Aqua;
                newWrap.Width = autoListUserSelected.Width;
                newElement.Background = ( ( ( i++ + 2 ) % 2 ) == 0 ) ? Brushes.LightGray : Brushes.White;
                User valUser;
                usersSelected.TryGetValue( op , out valUser );
                TextBlock newName = new TextBlock( );
                newName.Text = valUser.FullName;
                TextBlock newLogin = new TextBlock( );
                newLogin.Text = ( valUser.LoginName.Length < 2) ? "" : " (" + valUser.LoginName + ")";
                newWrap.Children.Add( newName );
                newWrap.Children.Add( newLogin );
                newElement.Content = newWrap;
                autoListUserSelected.Items.Add( newElement );
            }
        }
#endregion

#region Язаковая панель
        [DllImport( "user32.dll" )]
        private static extern long GetKeyboardLayoutName(
              System.Text.StringBuilder pwszKLID  //[out] string that receives the name of the locale identifier
              );
        [DllImport( "user32.dll" )]
        public static extern IntPtr GetForegroundWindow();

        [DllImport( "user32.dll" , CharSet = CharSet.Auto )]
        public static extern bool PostMessage( IntPtr hWnd , int Msg , int wParam , int lParam );

        [DllImport( "user32.dll" )]
        static extern int LoadKeyboardLayout( string pwszKLID , uint Flags );
        /// <summary>
        /// При загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputArea_GotFocus( object sender , RoutedEventArgs e )
        {
            languageSet( );
        }

        /// <summary>
        /// Задает название текущего языка ввода
        /// </summary>
        private void languageSet()
        {
            StringBuilder sb = new StringBuilder( );
            GetKeyboardLayoutName( sb );
            string lang = sb.ToString( ).Substring( sb.Length - 3 , 3 );
            if( lang == "409" )
            {
                if( languageText.Text != "En" )
                {
                    languageText.Text = "En";
                    languageText.Background = Brushes.Blue;
                }
            }
            else if( lang == "419" )
            {
                if( languageText.Text != "Ru" )
                {
                    languageText.Text = "Ru";
                    languageText.Background = Brushes.Brown;
                }
            }
            else
                MessageBox.Show( "Error lang = " + lang );
            if( languageText .Background == Brushes.Gray)
            {
                lang = "00000419";
                languageText.Text = "Ru";
                languageText.Background = Brushes.Brown;
                int ret = LoadKeyboardLayout( lang , 1 );
                int WM_INPUTLANGCHANGEREQUEST = 0x50;
                PostMessage( GetForegroundWindow( ) , WM_INPUTLANGCHANGEREQUEST , 1 , ret );
            }
        }

        private void languageText_ChangeLang( object sender , MouseButtonEventArgs e )
        {
            string lang;
            if( languageText.Background == Brushes.Blue )
            {
                lang = "00000419";
                languageText.Text = "Ru";
                languageText.Background = Brushes.Brown;
                int ret = LoadKeyboardLayout( lang , 1 );
                int WM_INPUTLANGCHANGEREQUEST = 0x50;
                PostMessage( GetForegroundWindow( ) , WM_INPUTLANGCHANGEREQUEST , 1 , ret );
            }
            else
            {
                lang = "00000409";
                languageText.Text = "En";
                languageText.Background = Brushes.Blue;
                int ret = LoadKeyboardLayout( lang , 1 );
                int WM_INPUTLANGCHANGEREQUEST = 0x50;
                PostMessage( GetForegroundWindow( ) , WM_INPUTLANGCHANGEREQUEST , 1 , ret );
            }

        }
        /// <summary>
        /// Ввод клавы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputArea_KeyDown( object sender , KeyEventArgs e )
        {
            if( Keyboard.IsKeyUp( Key.LeftShift )  && ( Keyboard.IsKeyUp( Key.LeftCtrl ) | Keyboard.IsKeyUp( Key.LeftAlt )) )
                languageSet( );
        }

        #endregion

        internal MyDict Getresult()
        {
            return usersSelected;
        }

        private void ArrowDownUp( object sender , KeyEventArgs e )
        {
            if( e.IsDown )
            {
                int i = autoListUser.SelectedIndex;
                int len = autoListUser.Items.Count;
                switch( e.Key.ToString( ) )
                {
                    case "Up":
                        {
                            if( autoListUser.Items.Count != 0 )
                            {
                                autoListUser.SelectedIndex = 0;
                                Keyboard.Focus( autoListUser.SelectedItem as IInputElement );
                            }

                                break;
                        }
                    case "Down":
                        {
                            if( autoListUser.Items.Count != 0 )
                            {
                                autoListUser.SelectedIndex = 0;
                                Keyboard.Focus( autoListUser.SelectedItem as IInputElement );
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

        }

        private void ArrowDownUp_BackToInput( object sender , KeyEventArgs e )
        {
            if( e.IsDown  )
            {
                int i = autoListUser.SelectedIndex;
                int len = autoListUser.Items.Count;
                string key = e.Key.ToString( );
                switch( key )
                {
                    case "Up":
                        {
                            if( i == 0 )
                            {
                                autoListUser.SelectedIndex = len - 1 ;
                                Keyboard.Focus( autoListUser.SelectedItem as IInputElement );
                                var border = ( Border )VisualTreeHelper.GetChild( autoListUser , 0 );
                                var scrollViewer = ( ScrollViewer )VisualTreeHelper.GetChild( border , 0 );
                                scrollViewer.ScrollToEnd( );
                                autoListUser.SelectedIndex = len - 1;
                            }
                            break;
                        }
                    case "Down":
                        {
                            if( i == len - 1 )
                            {
                                autoListUser.SelectedIndex = 0;
                                Keyboard.Focus( autoListUser.SelectedItem as IInputElement );
                                var border = ( Border )VisualTreeHelper.GetChild( autoListUser , 0 );
                                var scrollViewer = ( ScrollViewer )VisualTreeHelper.GetChild( border , 0 );
                                scrollViewer.ScrollToHome( );
                                autoListUser.SelectedIndex = 0;
                            }
                            break;
                        }
                    case "Return":
                        {
                            AddButton_Click( new object( ) , new RoutedEventArgs( ) );
                            Keyboard.Focus( inputArea );
                            inputArea.CaretIndex = inputArea.Text.Length + 2;
                            break;
                        }
                    case "Tab":
                        {
                            Keyboard.Focus( autoListUserSelected );
                            break;
                        }
                    default:
                        {
                            Keyboard.Focus( inputArea );
                            inputArea.CaretIndex = inputArea.Text.Length + 2;
                            break;
                        }
                }
            }


        }

        private void autoListUser_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            Selector selector = sender as Selector;
            if( selector is ListBox )
            {
                ( selector as ListBox ).ScrollIntoView( selector.SelectedItem );
            }
        }

    }
}
