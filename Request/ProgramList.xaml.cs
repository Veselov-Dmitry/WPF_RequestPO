using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace Request {
    /// <summary>
    /// Логика взаимодействия для ProgramList.xaml
    /// </summary>
    public partial class ProgramList : UserControl
    {
        public ProgramList()
        {
            InitializeComponent( );
            Visibility = Visibility.Hidden;
        }

        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;
        private ColumnDefinition _column;
        private string stringListProg = "";

        #region Message

        public string Message
        {
            get;
            set;
        }
        #endregion

        public void SetParent( UIElement parent, ColumnDefinition column )
        {
            _parent = parent;
            _column = column;
        }

        public bool ShowHandlerDialog( string message, List<List<string>> ExcelList )
        {
            Message = message;
            this.Visibility = Visibility.Visible;
            generateListProgs( ExcelList );
            _column.IsEnabled = false;

            _hideRequest = false;
            while( !_hideRequest )
            {
                // HACK: Stop the thread if the application is about to close
                if( this.Dispatcher.HasShutdownStarted || this.Dispatcher.HasShutdownFinished )
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background ,
                    new ThreadStart( delegate {
                    } ) );
                Thread.Sleep( 20 );
            }

            return _result;
        }

        private void generateListProgs( List<List<string>> NameNumberDepart )
        {
            if( NameNumberDepart.Count > 0 )
            {
                autoListProg.Items.Clear( );
                foreach( string nameProgr in NameNumberDepart[ 0 ] )
                {
                    int i = NameNumberDepart[ 0 ].IndexOf( nameProgr );
                    string numberProgr = ( NameNumberDepart[ 1 ] )[ i ];
                    string[] roleList = ( NameNumberDepart[ 3 ] )[ i ].Split( new char[] { '{','#','}'} );

                    WrapPanel wpMain = new WrapPanel { Orientation = Orientation.Vertical, MinWidth = 280 };
                    WrapPanel wpFirst = new WrapPanel { Orientation = Orientation.Horizontal };
                    WrapPanel wpSecond = new WrapPanel { Orientation = Orientation.Horizontal };
                    CheckBox chB1 = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
                    CheckBox chB2 = new CheckBox { Content = "Citrix   ", VerticalAlignment = VerticalAlignment.Center };
                    chB2.Checked += CheckBox_Checked;
                    TextBlock roleText = new TextBlock {  VerticalAlignment = VerticalAlignment.Center }; 
                    TextBlock name = new TextBlock { Text = nameProgr , FontSize = 12 , FontWeight = FontWeights.Normal };
                    TextBlock number = new TextBlock { Text = numberProgr , Width = 70, VerticalAlignment = VerticalAlignment.Center };
                    ComboBox cb1;
                    wpMain.Children.Add( wpFirst );
                    wpMain.Children.Add( wpSecond );
                    wpFirst.Children.Add( chB1 );//0 Children
                    wpFirst.Children.Add( number );//1 Children
                    wpFirst.Children.Add( chB2 );//2 Children
                    if( ( roleList.Length > 0 ) & ( roleList[0] != "" ))
                    {
                        roleText.Text = "экспл.";
                        cb1 = new ComboBox {
                            Width = 140 ,
                            Margin = new Thickness( 0 , 5 , 0 , 5 )
                        };
                        cb1.SelectionChanged += ComboBox_SelectionChanged;
                        foreach( string op in roleList )
                        {
                            if(!String.IsNullOrWhiteSpace(op))
                                cb1.Items.Add( op );
                        }
                        wpFirst.Children.Add( roleText );//3
                        wpFirst.Children.Add( cb1 );//4
                    }
                    wpSecond.Children.Add( name );
                    autoListProg.Items.Add( wpMain );
                    wpMain.MouseDown += new MouseButtonEventHandler( isCheckedChange );
                }
            }
        }

        void isCheckedChange( object sender , MouseButtonEventArgs e )
        {
            var wrap = sender as WrapPanel;
            if( wrap.GetType( ) == typeof( WrapPanel ) )
            {
                var inwrap = ( wrap as WrapPanel ).Children[ 0 ];
                if( inwrap.GetType( ) == typeof( WrapPanel ) )
                {
                    var checkB = ( inwrap as WrapPanel ).Children[ 0 ];
                    if( checkB.GetType( ) == typeof( CheckBox ) )
                        ( checkB as CheckBox ).IsChecked = !( checkB as CheckBox ).IsChecked;
                }
            }
        }

        private void ClearChecked( object sender , RoutedEventArgs e )
        {
            foreach( FrameworkElement wrap in autoListProg.Items )
            {
                if( wrap.GetType( ) == typeof( WrapPanel ) )
                {
                    var inwrap = ( wrap as WrapPanel ).Children[ 0 ];
                    if( inwrap.GetType( ) == typeof( WrapPanel ) )
                    {
                        var checkB = ( inwrap as WrapPanel ).Children[ 0 ];
                        if( checkB.GetType( ) == typeof( CheckBox ) )
                            ( checkB as CheckBox ).IsChecked = false;
                    }
                }
            }
        }
        
        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _column.IsEnabled = true;
        }

        /// <summary>
        /// формирует строку для возврата в текстблок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click( object sender , RoutedEventArgs e )
        {
            stringListProg = " ";
            if( autoListProg.Items.Count > 1 )
            {
                foreach( var item in autoListProg.Items )
                {
                    //var item -общий врап
                    if( item.GetType( ) == typeof( WrapPanel ) )
                    {
                        var inwrap = ( item as WrapPanel ).Children[ 0 ];// первый врап с чекбоксом, номером и отделом
                        if( inwrap.GetType( ) == typeof( WrapPanel ) )
                        {
                            var checkB = ( inwrap as WrapPanel ).Children[ 0 ];// чекбокс в первом врапе
                            if( checkB.GetType( ) == typeof( CheckBox ) && ( checkB as CheckBox ).IsChecked == true )
                            {
                                var chB = ( inwrap as WrapPanel ).Children[ 2 ];// галка Citrix
                                if( chB.GetType( ) == typeof( CheckBox ) )
                                    stringListProg += ( ( chB as CheckBox ).IsChecked == new bool?(true) ) ? "citrix " : "" ;
                                var textB = ( inwrap as WrapPanel ).Children[ 1 ];// текстблок(номер задачи) в первом врапе
                                if( textB.GetType( ) == typeof( TextBlock ) )
                                    stringListProg += ( textB as TextBlock ).Text;

                                if( ( inwrap as WrapPanel ).Children.Count > 3 )
                                {
                                    var roleText = ( inwrap as WrapPanel ).Children[ 4 ];// текстбокс с ролью если есть
                                    if( roleText.GetType( ) == typeof( ComboBox ) )
                                        stringListProg += ( String.IsNullOrWhiteSpace( ( roleText as ComboBox ).Text ) ) ? "," : "\n[" + ( roleText as ComboBox ).Text + "],";

                                }
                                else
                                    stringListProg += ",";
                            }
                        }
                    }
                }
                stringListProg = stringListProg.Substring( 0 , stringListProg.Length - 1 );
            }
            _result = true;
            HideHandlerDialog( );
        }
        
        private void CancelButton_Click( object sender , RoutedEventArgs e )
        {
            stringListProg = "";
               _result = false;
            HideHandlerDialog( );
        }

        internal string GetResult()
        {
            return ( stringListProg.Length < 2) ? "" : stringListProg ;
        }

        /// <summary>
        /// если отметить Citrix  то отметиться и выбранная зыдыча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked( object sender , RoutedEventArgs e )
        {
            if( sender.GetType( ) == typeof( CheckBox ) )
            {
                if( ( sender as CheckBox ).IsChecked == new bool?( true ) )
                {
                    CheckBox ch2 = ( sender as CheckBox );
                    if( ch2.Parent.GetType( ) == typeof( WrapPanel ) )
                    {
                        WrapPanel wp1 = ch2.Parent as WrapPanel;
                        if( wp1.Children[ 0 ].GetType( ) == typeof( CheckBox ) )
                        {
                            CheckBox chb1 = wp1.Children[ 0 ] as CheckBox;
                            chb1.IsChecked = true;
                        }
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            if( sender.GetType( ) == typeof( ComboBox ) )
            {
                if( ( sender as ComboBox ).SelectedIndex != -1 )
                {
                    ComboBox cb1 = ( sender as ComboBox );
                    if( cb1.Parent.GetType( ) == typeof( WrapPanel ) )
                    {
                        WrapPanel wp1 = cb1.Parent as WrapPanel;
                        if( wp1.Children[ 0 ].GetType( ) == typeof( CheckBox ) )
                        {
                            CheckBox chb1 = wp1.Children[ 0 ] as CheckBox;
                            chb1.IsChecked = true;
                        }
                    }
                }
            }
        }
    }
}
