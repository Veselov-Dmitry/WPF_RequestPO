using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Request
{
    public partial class MainWindow : Window
    {
        #region для конфига 
        private int procID = 378858;
        private string folder = @"D:\SEARCHWORK\";
        private string nameArchive = "ZND_OASU_NEW";
        private string classifFolder = @"ОАСУ\testZND";
        string nameDocType = "ЗНДnew";//имя типа документа 
        #endregion
        #region Глобальные переменные
        string keyCtlgRow = "ctlgRow";//шаблон названия ключа для списка прорам 
        UserListLists RequestUsers = new UserListLists( );
        List<List<string>> progrOASUList = new List<List<string>>();
        MyDict users = new MyDict();
        private static int countRow = 2;//счетчик серийных номеров строк таблицы
        S4.TS4App S4App =  null;
        #endregion

        public MainWindow()
        {
            InitializeComponent( );
            OnWorkerMethodStart( );
            OnExcelMethodStart( );
        }        

        private void AddRow( object sender , RoutedEventArgs  e )
        {
            countRow++;
            int rowIndexDig = MainGrid.RowDefinitions.Count;//2
            string lastRowIndex = getTripleNum( countRow );// 2 => 002
            
            //создаем новую строку в таблице
            RowDefinition rowDefinition = new RowDefinition( );
            rowDefinition.Height = new GridLength( 100 );
            rowDefinition.Name = "Row" + lastRowIndex;
            MainGrid.RowDefinitions.Add( rowDefinition );

            #region Number
            //initialization
            WrapPanel num = new WrapPanel();
            Label numLabel = new Label();
            //conection
            num.Children.Add( numLabel );
            //customization
            num.Name = "numberRow" + lastRowIndex;
            numLabel.Content = rowIndexDig - 1 + ".";
            numLabel.Width = 20;
            numLabel.Height = 100;
            #endregion
            #region Catalog
            //initialization
            WrapPanel catalog = new WrapPanel();
            TextBox catalogText = new TextBox { };
            TextBlock catalogBtnText = new TextBlock( );
            Button catalogBtn = new Button( );
            //conection
            catalog.Children.Add( catalogText );
            catalog.Children.Add( catalogBtn );
            //customization
            catalog.Name = keyCtlgRow + lastRowIndex;

            catalogText.Name = "ctlgTextRow" + lastRowIndex;
            catalogText.Width = 210;
            catalogText.Height = 100;
            catalogText.Style = this.FindResource( "TwitterTextBoxStyle" ) as Style;
            catalogText.MouseEnter += ctlgTextRow002_MouseEnter;
            catalogText.MouseLeave += ctlgTextRow002_MouseLeave;

            catalogBtnText.Text = "Список программ ОАСУ";
            catalogBtnText.Margin = new Thickness( 0 , -7 , 0 , -7 );

            catalogBtn.Name = "ctlgBtnProgRow" + lastRowIndex;
            catalogBtn.Height = 30;
            catalogBtn.Width = 159;
            catalogBtn.Margin = new Thickness( 30 , -35 , 0 , 0 );
            catalogBtn.FontSize = 12;
            catalogBtn.Visibility = Visibility.Hidden;
            catalogBtn.Padding = new Thickness( 0 , -1 , 0 , 0 );
            catalogBtn.Content = catalogBtnText; 
            catalogBtn.Style = this.FindResource( "Buttons" ) as Style;
            catalogBtn.Click += ctlgBtnProgRow002_Click;
            catalogBtn.MouseEnter += ctlgTextRow002_MouseEnter;
            catalogBtn.MouseLeave += ctlgTextRow002_MouseLeave;
            #endregion
            #region Rights
            //initialization
            Border borderRight = new Border( );
            StackPanel stackPRigths = new StackPanel( );
            string contentRight = "    ";
            char[] nameRButtons = { 'R', 'W', 'C', 'E', 'M', 'F' };
            string[] toolTipRight = { "чтение" , "создание" , "модифицирование" , "запись" , "удаление" , "просмотр содержимого каталога" };
            CheckBox[] rButtons = new CheckBox[ 6 ];
            for( int i = 0 ; i < rButtons.Length ; i++ )
                rButtons[ i ] = new CheckBox( );
            Label[] rLabels = new Label[ 6 ];
            for( int i = 0 ; i < rLabels.Length ; i++ )
                rLabels[ i ] = new Label( );

            //conection
            borderRight.Child = stackPRigths;
            for( int i = 0 ; i < rButtons.Length ; i++ )
            {
                stackPRigths.Children.Add( rButtons[ i ] );
                rButtons[ i ].Content = contentRight;
                rButtons[ i ].ToolTip = rLabels[ i ];
                rLabels[ i ].Content = toolTipRight[ i ];
            }
            //customization
            borderRight.Name = "rightsRow" + lastRowIndex;
            borderRight.IsEnabled = rightsTitle.IsEnabled;
            borderRight.BorderBrush = Brushes.LightGray;
            borderRight.BorderThickness = new Thickness( 0 , 1 , 0 , 1 );
            borderRight.Margin = new Thickness( 0 , 3 , 0 , 3 );

            stackPRigths.VerticalAlignment = VerticalAlignment.Center;
            stackPRigths.HorizontalAlignment = HorizontalAlignment.Center;
            stackPRigths.Orientation = Orientation.Horizontal;
            stackPRigths.Name = "rightsCheckRow" + lastRowIndex;

            for( int i = 0 ; i < rButtons.Length ; i++ )
            {
                rButtons[ i ].Name = "rButtRow" + lastRowIndex + "Col" + nameRButtons[ i ];
                rButtons[ i ].Style = this.FindResource( "DiscreteCheckBoxStyle" ) as Style;                
            }
            #endregion
            #region Users
            //initialization
            WrapPanel users = new WrapPanel();
            Border borderUser = new Border( );
            ListBox userslist = new ListBox( );
            Button usersCount = new Button( );
            TextBlock tbUserCount = new TextBlock( );
            //conection
            users.Children.Add( borderUser );
            users.Children.Add( usersCount );
            borderUser.Child = userslist;
            usersCount.Content = tbUserCount;
            //customization
            users.Name = "usersRow" + lastRowIndex;

            borderUser.Style = this.FindResource( "WarpPOutListBox" ) as Style;
            borderUser.MouseEnter += ctlgTextRow002_MouseEnter;
            borderUser.MouseLeave += ctlgTextRow002_MouseLeave;

            userslist.Name = "usersListRow" + lastRowIndex;
            userslist.Style = this.FindResource( "UsersListBox" ) as Style;
            userslist.Width = 245;
            userslist.Height = 100;
            userslist.MouseDoubleClick += usersTextChanged;

            usersCount.Name = "usersCountRow" + lastRowIndex;
            usersCount.Style =  this.FindResource( "Buttons" ) as Style;                
            usersCount.Width = 130;
            usersCount.Height = 30;
            usersCount.Padding = new Thickness( 0 , 2 , 0 , 0 );
            usersCount.Margin = new Thickness( 40 , -70 , 0 , 0 );
            usersCount.VerticalAlignment = VerticalAlignment.Bottom;
            usersCount.FontSize = 12;
            usersCount.MouseEnter += ctlgTextRow002_MouseEnter;
            usersCount.MouseLeave += ctlgTextRow002_MouseLeave;
            usersCount.Click += usersTextChanged;
            usersCount.Visibility = Visibility.Hidden;

            tbUserCount.Foreground = Brushes.Black;
            tbUserCount.Margin = new Thickness( 0 , -5 , 0 , -5 );
            tbUserCount.Text = "Редактировать(0)";
            #endregion
            #region Notes
            //initialization
            WrapPanel note = new WrapPanel( );
            TextBox tbNote = new TextBox( );
            //conection
            note.Children.Add( tbNote );
            //customization
            note.Name = "noteRow" + lastRowIndex;
            tbNote.Width = 225;
            tbNote.Height = 100;
            tbNote.Style = this.FindResource( "TwitterTextBoxStyle" ) as Style;
            #endregion
            #region Remove Row
            //initialization
            WrapPanel removeButton = new WrapPanel( );
            Button btnRemove = new Button();
            Label lbRemove = new Label( );
            //conection
            removeButton.Children.Add( btnRemove );
            btnRemove.Content = lbRemove;
            //customization
            removeButton.Name = "remBtnRow" + lastRowIndex;
            removeButton.VerticalAlignment = VerticalAlignment.Center;
            removeButton.Margin = new Thickness( -23,0,0,0);

            btnRemove.Style = this.FindResource( "Buttons" ) as Style;
            btnRemove.Width = 22;
            btnRemove.Click += RemLastRow;

            lbRemove.Foreground = Brushes.Green;
            lbRemove.FontSize = 16;
            lbRemove.Margin = new Thickness( -7,0,-7,0);
            lbRemove.Padding = new Thickness( 0);
            lbRemove.Content = "X";
            #endregion
            // добавление в список объектов
            MyDict newMyDict = new MyDict( );
            RequestUsers.Add( newMyDict );         


#region добавляем контент в созданную строку таблицы
            Grid.SetColumn( num , 0 );
            Grid.SetRow( num , rowIndexDig );

            Grid.SetColumn( catalog , 1 );
            Grid.SetRow( catalog , rowIndexDig );
            
            Grid.SetColumn( borderRight , 2 );
            Grid.SetRow( borderRight , rowIndexDig );

            Grid.SetColumn( users , 3 );
            Grid.SetRow( users , rowIndexDig );

            Grid.SetColumn( note , 4 );
            Grid.SetRow( note , rowIndexDig );

            Grid.SetColumn( removeButton , 5 );
            Grid.SetRow( removeButton , rowIndexDig );

            MainGrid.Children.Add( num );
            MainGrid.Children.Add( catalog );
            MainGrid.Children.Add( borderRight );
            MainGrid.Children.Add( users );
            MainGrid.Children.Add( note );
            MainGrid.Children.Add( removeButton );
            #endregion

            if( rowIndexDig == 3 )
                Request.Height = 800;
            else if( ( rowIndexDig == 4 ) )
            {
                Request.Top = 100;
            }
            else
                Request.Top = 0;
            scroll.ScrollToEnd( );
        }
        
        private void RemLastRow( object sender , RoutedEventArgs e )
        {
            string lastRowIndex = "";
            try
            {
                WrapPanel wp = ( sender as Button ).Parent as WrapPanel;
                string wpn = wp.Name;
                //номер строки таблицы с которой произведено нажатие
                int index = Grid.GetRow( wp );
                //серийный номер кнопки с которой производилось нажатие
                lastRowIndex = getTripleNum( Convert.ToDouble( wpn.Substring( wpn.Length - 3 ) ) );
#region находим все элементы по серийному номеру,которые принадлежат данной строке
                List<FrameworkElement> child = new List<FrameworkElement>( );
                foreach( FrameworkElement op in MainGrid.Children )
                {
                    if(  op.Name.IndexOf( lastRowIndex ) > -1)
                    {
                        child.Add( op );
                    }                
                        
                }
                #endregion
#region удаляем всю строку т.е. сначала удаляем саму строку а только потом все Children
                MainGrid.RowDefinitions.RemoveAt( index );
#endregion
                //удаляет из списка пользователей элемент соответствующий строке
                RequestUsers.RemoveAt( index - 2 );
#region удаляем все элементы имеющие серийные номера, которые принадлежали удаленной строке
                foreach( FrameworkElement op in child)
                {
                    MainGrid.Children.Remove( op );
                }
#endregion
#region Переропределение родителькой строки для элементов                
                foreach( RowDefinition op in MainGrid.RowDefinitions )
                {
                    string serial = op.Name.Substring( op.Name.Length - 3 );//004
                    int noRow = MainGrid.RowDefinitions.IndexOf( op );//3
                    //связываем строки и элементы по серийнику
                    foreach( FrameworkElement fr in MainGrid.Children )
                    {
                            if( fr.Name.Substring( fr.Name.Length - 3 ) == serial )//004 == 004
                            {
                                Grid.SetRow( fr , noRow );//(name,3)
                            //восстанавливаем порядок счета строй
                                if( "numberRow" + serial == fr.Name )//numberRow004
                            {                                    
                                    ( ( fr as WrapPanel ).Children[ 0 ] as Label ).Content = ( noRow -1) + ".";//2.
                                }
                            }
                    }
                }
#endregion
                scroll.ScrollToEnd( );



            }
            catch( Exception ) { }
        }

        #region TEST

        private void getcoord()
        {
            StringBuilder ty = new StringBuilder( );
            int i = 0;
            foreach( RowDefinition op in MainGrid.RowDefinitions)
            {
                string ok =  op.Name.ToString( );
                ok = ok.Substring( ok.Length-3);

                foreach(FrameworkElement fr in MainGrid.Children)
                {
                    string name = fr.Name.ToString( );
                    if(name!="")
                        if( name.Substring( name.Length - 3 ) == ok )
                        {
                            ty.Append( "["+name + " R=" + Grid.GetRow( fr ) + " C=" + Grid.GetColumn( fr )+"]" );
                        }

                    ty.Append( "  " );
                }
                    ty.Append( "\n\r=============================\n\r" );
                i++;
            }
            MessageBox.Show( ty.ToString());
        }

        private void WordTest( StringBuilder ty )
        {
            try
            {
                MessageBox.Show( "Word создан!" );

                using( StreamWriter sw = new StreamWriter( @"C:\Users\Все пользователи\LOG_REQEST\log.txt" , false , Encoding.Default ) )
                {
                    string separ = "\n\r||||||||||||||||||||||||||" + System.DateTime.Now + "|||||||||||||||||||||||||||||||||||||\n\r";
                    sw.WriteLine( separ );
                    sw.WriteLine( ty.ToString( ) );
                }
            }
            catch( Exception ) { };
        }

        private void testc( StringBuilder ty2 )
        {
            StringBuilder ty = new StringBuilder( );
            ty.Append( "old ty: \n\r" + ty2 + "\n\r=============================\n\r");
            ty.Append( "Row's:\n\r" );
            foreach( RowDefinition op in MainGrid.RowDefinitions)
            {
                ty.Append( op.Name + " index = " + MainGrid.RowDefinitions.IndexOf(op) + "\n\r" );
            }
            MessageBox.Show( ty.ToString());
        }

        #endregion 

        private void RClickBtn( object sender , RoutedEventArgs e )
        {
            try
            {
                CheckBox btn = sender as CheckBox;
                
                string litera = btn.Name.ToString( );//TbtnR
                litera = litera.Substring( litera.Length - 1 );//R
                if( btn.IsChecked == new bool?( true ) )//true
                {
                    foreach( FrameworkElement op in MainGrid.Children )
                    {
                        string nameSt = op.Name.ToString( );//rightsRow002
                        if( nameSt.Contains( "rightsRow" ) )// true
                            foreach( FrameworkElement ui in (( op as Border ).Child as StackPanel ).Children )//Grid -> Border -> StackPanel -> CheckBox
                            {
                                string nameCh = ui.Name.ToString( );//rButtRow002ColR
                                if(litera==nameCh.Substring(nameCh.Length-1))//R == R
                                    ( ui as CheckBox ).IsChecked = true;
                            }
                    }
                }
                else
                    foreach( FrameworkElement op in MainGrid.Children )
                    {
                        string nameSt = op.Name.ToString( );//rightsRow002
                        if( nameSt.Contains( "rightsRow" ) )//true
                            foreach( FrameworkElement ui in ( ( op as Border ).Child as StackPanel ).Children )
                            {
                                string nameCh = ui.Name.ToString( );//rButtRow002ColR
                                if( litera == nameCh.Substring( nameCh.Length - 1 ) )//R == (rButtRow002Col)R
                                    ( ui as CheckBox ).IsChecked = false;
                            }
                    }
            }
            catch( Exception richex) {
                MessageBox.Show( "не могу проставить права т.к ошибка в строке " + richex.StackTrace.Substring( richex.StackTrace.Length-3) , "Ошибка обработана" );
            }
            scroll.ScrollToRightEnd( );

        }

        private void InitWorkFlow( object sender , RoutedEventArgs e )
        {
            //UpperGrid.Visibility = System.Windows.Visibility.Hidden;
            string typeReq = "";
            Dictionary<string , string> buroName = new Dictionary<string , string>( );
            SortedDictionary<string , string> content = new SortedDictionary<string , string>( );
            string primechanie = "";//для сбора строки в параметр архива
            string polzovatel = "";//для сбора строки в параметр архива
            string prava = "";//для сбора строки в параметр архива
            string katalog = "";//для сбора строки в параметр архива
            try
            {
                #region СБОР ИНФОРМАЦИИ
                #region Определить тип заявки
                foreach( FrameworkElement op in TypeGrid.Children )
                {
                    var uelem = op as RadioButton;
                    if( uelem != null )
                        if( uelem.IsChecked == new bool?( true ) )
                        {
                            typeReq = uelem.Content.ToString( );// POVneshnee POOASU IzmenStructPrav 
                            switch( uelem.Name.ToString( ) )
                            {
                                case "IzmenStructPrav":
                                    {
                                        buroName.Add( "БССО" , "1" );
                                        break;
                                    }/**/
                                case "POOASU":
                                    {
                                        buroName.Add( "БССО" , "1" );
                                        buroName.Add( "БРОВТ" , "1" );
                                        break;
                                    }/**/
                                case "POVneshnee":
                                    {
                                        buroName.Add( "БРОВТ" , "1" );
                                        break;
                                    }/**/
                                default:
                                    break;
                            }

                        }

                }
                #endregion
                #region Доп согласование по бюро
                foreach( UIElement op in buroCheck.Children )
                    if( ( op as CheckBox ).IsChecked == true )
                    {
                        string key = ( op as FrameworkElement ).Name.ToString( );//BPO_BPV GRZP BSAPR
                        switch( key )
                        {
                            case "BPO_BPV":
                                {
                                    buroName.Add( "БПВ" , "1" );
                                    buroName.Add( "БПО" , "1" );
                                    break;
                                }
                            case "GVSIS":
                                {
                                    buroName.Add( "ГВСИС" , "1" );
                                    break;
                                }
                            case "BSAPR":
                                {
                                    buroName.Add( "БСАПР" , "1" );
                                    break;
                                }
                            case "BVBD":
                                {
                                    buroName.Add( "БВБД" , "1" );
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                #endregion
                #region Права доступа
                int countTemp = 0;
                foreach( FrameworkElement op in MainGrid.Children )
                {
                    string nameElem = op.Name.ToString( );//rightsRow002
                    nameElem = nameElem.Substring( 0 , nameElem.Length - 3 );//rightsRow
                    if( nameElem == "rightsRow" )
                    {
                        countTemp++;
                        foreach( CheckBox chb in ( ( op as Border ).Child as StackPanel ).Children )
                        {
                            string val = chb.IsChecked.ToString( );//true
                            if( chb.IsChecked == true ){
                                content.Add( chb.Name , val );//
                            }
                            else{
                                content.Add( chb.Name , val );//rButtRow002ColR , true
                            }
                        }
                    }
                    else if( ( op as TextBox ) != null )
                    {
                        string text = ( op as TextBox ).Text;
                        if( !String.IsNullOrEmpty( text ) )
                        {
                            content.Add( op.Name , text );
                        }
                    }
                }
                content.Add( "countRowsTemp" , countTemp.ToString() );
                content.Add( reasonText.Name , reasonText.Text );
                #endregion
                
                foreach( FrameworkElement framElem in MainGrid.Children )
                {
                    #region Список программ
                    if(Grid.GetColumn(framElem) == 1)
                    {
                        var wrap = framElem;
                        if( wrap.GetType( ) == typeof( WrapPanel ) )
                        {
                            var textBox = ( wrap as WrapPanel ).Children[ 0 ];
                            if( textBox.GetType( ) == typeof( TextBox ) )
                            {
                                if(!content.ContainsKey( ( wrap as WrapPanel ).Name ) ) {//без этой проверки выводит ошибку о том что такой ключ уже есть
                                    content.Add( ( wrap as WrapPanel ).Name , ( textBox as TextBox ).Text );// "ctlgTextRow002" , "1025,105"
                                    katalog += ( textBox as TextBox ).Text + "{#}";
                                }
                            }
                        }
                    }
                    #endregion
                    #region Примечание

                    if( Grid.GetColumn( framElem ) == 4 )
                    {
                        TextBox tbNote = ( framElem as WrapPanel ).Children[ 0 ] as TextBox;
                        if( tbNote.GetType( ) == typeof( TextBox ) )
                            {
                            if(!content.ContainsKey( framElem.Name ) ) {//без этой проверки выводит ошибку о том что такой ключ уже есть
                                content.Add( framElem.Name , tbNote.Text );// "noteRow002" , "для того"
                                primechanie += tbNote.Text + "{#}";
                            }
                        }                        
                    }
                    #endregion
                }
                #region Список пользователей
                string keyUsersRow = "usersRow";//шаблон названия ключа для списка пользователей
                int count = 2;
                StringBuilder userString = new StringBuilder( );
                User someUser = null;
                foreach( MyDict listUsers in RequestUsers.GetList())
                {
                    userString.Clear( );
                    if( listUsers  != null)
                    foreach(string keyUser in listUsers.Keys() )
                    {
                        if( keyUser != null )
                            listUsers.TryGetValue( keyUser , out someUser );
                        if(someUser != null)
                        //ТУТ ФОРМАТИРУЕТСЯ инфо о пользователе
                            userString.Append( someUser.FullName + "("  + someUser.LoginName + ") " + someUser.NameGroup + ",\n");
                    }
                    if( userString.Length > 1 )
                        userString.Remove( userString.Length - 1 , 1 );
                    content.Add( ( keyUsersRow + getTripleNum( count )) , userString.ToString() );
                    polzovatel += userString.ToString( ) + "{#}";
                    count++;
                }
                #endregion
                #endregion


                #region S4 sender
                if( null == S4App )
                {
                    S4App = new S4.TS4App( );
                    if( S4App.Login( ) != 1 )
                        throw new Exception( "\n\rНе создан COM объект Search!\n\r" );
                }
                var AppServer = S4App.GetSbServer( );// получаем объект sbserver;
                try
                {

                    //---=== Запуск процесса без диалога с пользователем===---



                    var Router = AppServer.GetRouter( );// получаем объект Router;
                    var Process = Router.CreateProcess( procID );//id маршрута согласования    254283
                    var procVars = Process.StartActivity.Variables;
                    var s4Classificator = S4App.GetClassificatorInterface( );// GetClassificatorInterface для доступа к работе с классификаторами 
                    string FolderKey = s4Classificator.OpenFolderByName( classifFolder );// OpenFolderByName открывает папку классификатора по ее полному имени и возвращает идентификатор открытой папки
                    string designation = s4Classificator.GetDesignationByKey( FolderKey , "" );// GetDesignationByKey возвращает сгенерированное обозначение для папки классификатора.
                                                                                               //designation = "36.%9999%-ЗНД-14";
                    string filename;
                    //filename = S4App.GenerateFileName( "" , fileExt );//=designation + ".doc";//GenerateFileName генерирует уникальное имя файла для новых документов Search. 
                    filename = designation ;
                    string fullfilename = folder + filename + ".doc";

                    S4App.OpenQuery( @"SELECT ARCHIVE_ID FROM ARCHIVES WHERE FILENAME ='" + nameArchive + "'" );//OpenQuery Эта функция позволяет выполнить произвольный SQL-запрос
                    int archiveId = int.Parse( S4App.QueryFieldByName( "ARCHIVE_ID" ) );//QueryFieldByName Эта функция позволяет получить значение поля FieldName в текущей записи полученной выборки
                    S4App.OpenQuery( @"SELECT DOC_TYPE  FROM Search.dbo.DOCTYPES  WHERE DOC_CODE = '" + nameDocType + "'" );
                    int docType = int.Parse( S4App.QueryFieldByName( "DOC_TYPE" ) );
                    S4App.CloseQuery( );//CloseQuery Эту функцию следует использовать по завершении работы с открытым запросом
                    if( Directory.Exists( folder ) == false )
                        Directory.CreateDirectory( folder );
                    int doc_id = S4App.CreateFileDocumentWithDocType2( fullfilename , docType , archiveId , filename , filename , 0 );//CreateFileDocumentWithDocType2 создает в архиве новый файловый документ и возвращает его инвентарный номер. 
                    //S4App.SyncDocument( );
                    if( !File.Exists( fullfilename ) )
                        throw new Exception( "\n\rФайл заявки\n\r" + fullfilename + "\n\r не создан" + "\n\r" );

                    #region Word creator

                    //проверка атрибутов чтнеия и исправление
                    FileAttributes attributes = System.IO.File.GetAttributes( fullfilename );
                    if( ( attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
                    {
                        attributes = attributes & FileAttributes.Normal;
                        System.IO.File.SetAttributes( fullfilename , attributes );
                    }

                    Word.Application word = new Word.Application( ); //создаем COM-объект Word
                    Word.Document wordDocument = null;
                    Object oMissing = System.Reflection.Missing.Value;
                    Object missing = Type.Missing;
                    string keyCtlgRow = "ctlgRow";
                    string usersRow = "usersRow";
                    string noteRow = "noteRow";
                    string rButtRow = "rButtRow";
                    string[] lit = new string[] { "ColR" , "ColW" , "ColC" , "ColE" , "ColM" , "ColF" };

                    try
                    {
                        word.Visible = false;

                        Object patternFile;
                        //patternFile = @"D:\ЗАЯВКА.dot";/*
                        patternFile = fullfilename;/**/
                        word.Documents.Open( ref patternFile );
                        wordDocument = word.Documents.Application.ActiveDocument;

                        Word.Table tableType = wordDocument.Shapes[ 1 ].TextFrame.TextRange.Tables[ 1 ];
                        Word.Table tableContent = wordDocument.Tables[ 1 ];
                        Word.Table tableStamp = wordDocument.Shapes[ 1 ].TextFrame.TextRange.Tables[ 2 ];
                        
                        tableType.Cell( 2 , 2 ).Range.Text = typeReq;
                        

                        string reasonTemp = "";
                        content.TryGetValue( reasonText.Name , out reasonTemp );
                        //причина
                        tableStamp.Cell( 2/*Column*/ , 1/*Row*/ ).Range.Text = reasonTemp;
                        //фамилия
                        tableStamp.Cell( 4 , 2 ).Range.Text = S4App.GetUserFullName_ByUserID( S4App.GetUserID( ) );
                        //дата сегодня
                        tableStamp.Cell( 4 , 4 ).Range.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        //таблица


                        string countRowsTemp = "";
                        content.TryGetValue( "countRowsTemp" , out countRowsTemp );
                        int countTable = Int32.Parse( countRowsTemp ) + 2;



                        foreach( Microsoft.Office.Interop.Word.Range tmpRange in wordDocument.StoryRanges )
                        {
                            // Set the text to find and replace
                            tmpRange.Find.Text = "ЗАЯВКА №";

                            tmpRange.Find.Replacement.Text = tmpRange.Find.Text + designation;// tmpRange.Find.Text + @"36.%9999%-ЗНД-14";

                            // Set the Find.Wrap property to continue (so it doesn't
                            // prompt the user or stop when it hits the end of
                            // the section)
                            tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

                            // Declare an object to pass as a parameter that sets
                            // the Replace parameter to the "wdReplaceAll" enum
                            object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

                            // Execute the Find and Replace -- notice that the
                            // 11th parameter is the "replaceAll" enum object
                            tmpRange.Find.Execute( ref missing , ref missing , ref missing ,
                                ref missing , ref missing , ref missing , ref missing ,
                                ref missing , ref missing , ref missing , ref replaceAll ,
                                ref missing , ref missing , ref missing , ref missing );
                        }
                        //заполнение таблицы
                        for( int i = 2 ; i < countTable ; i++ )
                        {
                            if( i >= 11 )
                                tableContent.Rows.Add( ref oMissing );
                            string patt = ( ( i < 10 ) ? ( "00" + i.ToString( ) ) : ( ( i < 100 ) ? ( "0" + i.ToString( ) ) : i.ToString( ) ) );
                            string value = "";
                            string keyName = "";

                            keyName = keyCtlgRow + patt;//"ctlgRow" + "002"
                            content.TryGetValue( keyName , out value );
                            tableContent.Cell( i + 1 , 1 ).Range.Text = value;

                            keyName = usersRow + patt;//"usersRow" + "002"
                            content.TryGetValue( keyName , out value );
                            tableContent.Cell( i + 1 , 3 ).Range.Text = value;

                            keyName = noteRow + patt;//"noteRow" + "002"
                            content.TryGetValue( keyName , out value );
                            tableContent.Cell( i + 1 , 4 ).Range.Text = value;

                            value = "";
                            string outValue = "";
                            if( rightsTitle.IsEnabled )
                            {
                                for( int j = 0 ; j < 6 ; j++ )
                                {
                                    keyName = rButtRow + patt + lit[ j ];//"rButtRow" + "002" + "ColR"
                                    content.TryGetValue( keyName , out outValue );
                                    if( outValue == "True" )
                                        value += "+ ";
                                    else
                                        value += "_ ";
                                }
                                tableContent.Cell( i + 1 , 2 ).Range.Text = value;
                                prava += value + "{#}";
                            }
                            else
                            {
                                tableContent.Cell( 1 , 2 ).Range.Text = "";
                                tableContent.Cell( 2 , 2 ).Range.Text = "";
                                tableContent.Cell( 3 , 2 ).Range.Text = "";

                            }

                        }
                        Object fileName = fullfilename;
                        wordDocument.SaveAs2( ref fileName );


                    }
                    catch( Exception expWord )
                    {
                        try
                        {
                            if( wordDocument != null )
                                wordDocument.Close( false );
                            word.Quit( );
                        }
                        catch { }
                        MessageBox.Show( "Текст ошибки:\n" + expWord.Message + "\nошибка в строке(" + expWord.StackTrace.Substring( expWord.StackTrace.Length-3,3) + ")", "");
                    }
                    finally
                    {
                        if( word != null )
                        {
                            try
                            {
                                if( wordDocument != null )
                                    wordDocument.Close( false );
                                word.Quit( );
                            }
                            catch { }
                        }
                    }

                    //UpperGrid.Visibility = System.Windows.Visibility.Visible;
                    #endregion

                    string buroString = "";
                    foreach( string op in buroName.Keys )
                        buroString += op.ToString( ) + "{#}";

                    S4App.OpenDocument( doc_id );
                    S4App.SetFieldValue( "DESIGNATIO" , designation );//SetFieldValue присваивает значение указанному параметру текущего документа.
                    S4App.SetFieldValue( "NAME" , designation );
                    S4App.SetFieldValue( "OFFISE_ZND" , buroString );
                    S4App.SetFieldValue( "PRIMECHANIE_ZND" , primechanie );
                    S4App.SetFieldValue( "POLZOVATEL_ZND" , polzovatel );
                    S4App.SetFieldValue( "KATALOG_ZND" , katalog );
                    S4App.SetFieldValue( "PRAVA_ZND" , prava );

                    //test
                    //string io = S4App.GetFieldValue( "OFFISE_ZND" );
                    //MessageBox.Show( "START! = " + io );

                    //S4App.SetFieldValue( "NAME" , docName );//           -//-
                    S4App.CheckIn( );//возвращает в архив текущий документ, если он взят на изменение вошедшим в Search пользователем.
                    s4Classificator.IncludeDocument( doc_id );//IncludeDocument включает в состав текущей открытой папки классификатора документ с инвентарным номером DocID.
                    int opp = Process.StartActivity.Attachments.AddLink( doc_id );//прикрепим этот документ как вложение
                    //procVars.GetVariableByName( "ZAYAVKA" ).value = designation;// присваивает значение указанному параметру текущего документа. 
                    Process.Start( );
                    Process.Name = "Заявка ЗНД new " + designation;
                    S4App.CloseDocument( );
                    /**/

                    //AppServer = null;
                    #endregion
                }
                catch(Exception exS4 )
                {
                    MessageBox.Show( "Ошибка при создании заявки!\n" + exS4.StackTrace , "Result!" );
                    //AppServer = null;
                }
                //UpperGrid.Visibility = System.Windows.Visibility.Visible;
                //MessageBox.Show( "COMPLITE!","Result");
                Environment.Exit( 0 );
            }
            catch( Exception ExStartPorc ) {
                MessageBox.Show( "ОШИБКА:\n\r[" + ExStartPorc.Message + "]" + "\n\r\n\rстрока: " + ExStartPorc.StackTrace.Substring( ExStartPorc.StackTrace.Length - 3 ) );
            }
            
        }

        /// <summary>
        /// Открытие окна с списком выбранных пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usersTextChanged( object sender , RoutedEventArgs e )
        {
            if( progrBStatus.Text != "Идет загрузка списка пользователей..." )
            {
                WrapPanel wp = new WrapPanel();
                if( sender.GetType( ) == typeof( Button ) )
                {
                    wp = ( sender as FrameworkElement ).Parent as WrapPanel;
                }
                else if( sender.GetType( ) == typeof( ListBox ) )
                {
                    wp = ( ( sender as FrameworkElement ).Parent as Border ).Parent as WrapPanel;
                }
                else
                    return;
                int index = Grid.GetRow( wp );
                MyDict dict = RequestUsers.ItemGet( index - 2 );
                //отдаем users для работы и измененеия
                SelectUsers.SetParent( mainParrent, mainParrentColumn , users, dict );
                //передаем mess для определения с какой строки было вызвано окно
                SelectUsers.ShowHandlerDialog( index.ToString() );
                //забираем отредактированный список либо не отредактированный
                RequestUsers.ItemSet( index - 2 , SelectUsers.Getresult( ) );
                Update(  );
            }
        }

        /// <summary>
        /// Обновить списки всех ячеек главной таблицы со списками пользователей
        /// </summary>
        private void Update()
        {
            int count = 2;
            foreach( var listSelectedUsers in RequestUsers.List )
            {
                MyDict uu = new MyDict( );
                User usersData = new User( );
                string namePattern = "";

                if( listSelectedUsers != uu )
                {
                    //usersListRow002.Items.Clear( );
                    foreach( FrameworkElement lookingWrapPanel in MainGrid.Children )
                    {
                        namePattern = "usersRow" + getTripleNum( count );
                        if( ( lookingWrapPanel.GetType( ) == typeof( WrapPanel ) )&( lookingWrapPanel.Name == namePattern ))
                        {
                            WrapPanel wp = lookingWrapPanel as WrapPanel;
                            if( wp.Children[ 0 ].GetType( ) == typeof( Border ) )
                            {
                                Border br = wp.Children[ 0 ] as Border;
                                if( br.Child.GetType( ) == typeof( ListBox ) )
                                {
                                    ListBox lb = br.Child as ListBox;
                                    lb.Items.Clear( );
                                    foreach( var someUser in listSelectedUsers.SortD.Keys )
                                    {
                                        listSelectedUsers.SortD.TryGetValue( someUser , out usersData );
                                        ListBoxItem newLB = new ListBoxItem( );
                                        WrapPanel newWP = new WrapPanel( );
                                        TextBlock newTB1 = new TextBlock( );
                                        newTB1.Text = usersData.FullName;
                                        TextBlock newTB2 = new TextBlock( );
                                        newTB2.Text = ( usersData.LoginName.Length < 2) ? "" : "  (" + usersData.LoginName + ")";
                                        newWP.Children.Add( newTB1 );
                                        newWP.Children.Add( newTB2 );
                                        lb.Items.Add( newWP );
                                    }                                    
                                }
                            }
                            if( wp.Children[ 1 ].GetType( ) == typeof( Button ) )
                            {
                                Button btn = wp.Children[ 1 ] as Button;
                                if( btn.Content.GetType( ) == typeof( TextBlock ) )
                                {
                                    TextBlock tb = btn.Content as TextBlock;
                                    tb.Text = "Редактировать(" + listSelectedUsers.SortD.Count( ) + ")";
                                }
                            }
                        }
                    }
                }
                count++;
            }
        }

        /// <summary>
        /// 1 to "001" or 53 to "053"
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string getTripleNum( double num )
        {
            return ( num < 10 ) ? ( "00" + num.ToString( ) ) : ( ( num < 100 ) ? ( "0" + num.ToString( ) ) : num.ToString( ) );
        }

        private void ctlgTextRow002_MouseEnter( object sender , MouseEventArgs e )
        {
            foreach( FrameworkElement element in ( ( sender as FrameworkElement ).Parent as WrapPanel ).Children )
            {
                if( element.Name.IndexOf( "ctlgBtnProgRow" ) != -1 )
                {
                    if( POOASU.IsChecked == new bool?( true ) )
                        element.Visibility = Visibility.Visible;
                }
                else if( element.Name.IndexOf( "usersCountRow" ) != -1 )
                {
                    element.Visibility = Visibility.Visible;
                }
            }
        }

        private void ctlgTextRow002_MouseLeave( object sender , MouseEventArgs e )
        {
            foreach( FrameworkElement element in ( ( sender as FrameworkElement ).Parent as WrapPanel ).Children )
            {
                if( ( element.Name.IndexOf( "ctlgBtnProgRow" ) != -1 ) | ( element.Name.IndexOf( "usersCountRow" ) != -1 ) )
                    element.Visibility = Visibility.Hidden;
            }

        }

        /// <summary>
        /// Открытие списка программ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlgBtnProgRow002_Click( object sender , RoutedEventArgs e )
        {
            if( progrBStatusExcel.Text != "Идет загрузка списка программ..." )
            {
                //отдаем users для работы и измененеия
                ProgramList.SetParent( mainParrent, mainParrentColumn );
                //передаем mess для определения с какой строки было вызвано окно
                ProgramList.ShowHandlerDialog( "" , progrOASUList );
                TextBox textBox;// текстовое поле списка программ
                // находим объект textBlock
                if( sender.GetType( ) == typeof( Button ) )
                {
                    var wrap = ( sender as Button ).Parent;
                    if( wrap.GetType( ) == typeof( WrapPanel ) )
                    {
                        var ttBk = ( wrap as WrapPanel ).Children[ 0 ];
                        if( ttBk.GetType( ) == typeof( TextBox ) )
                        {
                            textBox = ttBk as TextBox;
                            string outString = ProgramList.GetResult( );
                            if( outString.Length > 1)
                                textBox.Text += ( textBox.Text.Length < 2 ) ? outString : ", " + outString;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// в новом интерфейсе не нужно
        /// <param name="e"></param>
        private void TypeRequestSelect( object sender , RoutedEventArgs e )
        {
                //MessageBox.Show( ( sender as FrameworkElement ).Name.ToString( ) );
            if( sender.GetType( ) == typeof( RadioButton ) )
            {
                if( ( sender as RadioButton ).IsChecked == true )
                {
                    POVneshnee.IsChecked = false;
                    POOASU.IsChecked = false;
                    IzmenStructPrav.IsChecked = false;
                    ( sender as RadioButton ).IsChecked = true;
                }
            }
        }

        private void Exit( object sender , RoutedEventArgs e )
        {
            Environment.Exit( 0 );
        }

        private void TypeRequestSpecific( object sender , RoutedEventArgs e )
        {
            if( ( sender.GetType( ) == typeof( RadioButton ) ) & ( ( sender as RadioButton ).Name == "IzmenStructPrav" ) )
            {
                rightsTitle.IsEnabled = true;
                foreach( FrameworkElement op in MainGrid.Children )
                    if( op.Name.Contains( "rightsRow" ))
                        op.IsEnabled = true;
            }
            else if( ( sender.GetType( ) == typeof( RadioButton ) )&&( rightsTitle != null) )
            {
                foreach( CheckBox op in rightsTitle.Children )
                {
                    op.IsChecked = false;
                    RClickBtn( op , new RoutedEventArgs());
                }


                    rightsTitle.IsEnabled = false;
                foreach( FrameworkElement op in MainGrid.Children )
                    if( op.Name.Contains( "rightsRow" ) )
                        op.IsEnabled = false;

            }
        }

        private void Request_Loaded( object sender , RoutedEventArgs e )
        {
            TypeRequestSpecific( POOASU , new RoutedEventArgs( ) );
        }

     
    }

}
