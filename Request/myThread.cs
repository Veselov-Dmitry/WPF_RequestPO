using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Request
{
    class MyClass
    {
        S4.TS4App S4App = null;
        //SortedList<string , User> useresLoaded;
        public delegate void OnWorkerMethodCompleteDelegate( SortedList<string , User> op );
        public event OnWorkerMethodCompleteDelegate OnWorkerComplete;

        public void WorkerMethod()
        {
            SortedList<string , User> content = new SortedList<string , User>( 2000 );
            User op;
            try
            {
                MessageBoxResult mr = MessageBoxResult.Cancel;
                S4App = new S4.TS4App( );
                do
                {
                    if( S4App.Login( ) != 1 )
                    {
                        mr = MessageBox.Show( "Программа запущена, но Search не запущен \nВойдите в Search и затем нажмите [ ОК ] " , "Внимание!" , MessageBoxButton.OKCancel , MessageBoxImage.Exclamation );
                        if( mr == MessageBoxResult.Cancel )
                        {
                            Environment.Exit( 0 );
                        }
                    }
                }
                while( mr == MessageBoxResult.OK );

                string sqlQuery = "SELECT G.NAME_GROUP, U2.FULLNAME, U2.LOGINNAME, U1.RANK " +
    "FROM Search.dbo.grpingrp AS L " +
    "INNER JOIN Search.dbo.grpingrp AS L2 " +
        "ON L2.group_id = L.ingroup_id AND L2.ingroup_id <> 999999998 " +
    "INNER JOIN Search.dbo.GROUPS AS G " +
        "ON G.GROUP_ID = L.ingroup_id AND G.GROUP_CODE <> '' " +
    "INNER JOIN Search.dbo.GROUPS AS G2 " +
       "ON G2.GROUP_ID = L.group_id " +
    "INNER JOIN Search.dbo.USERS_INFO AS U1 " +
        "ON U1.USER_ID = G2.user_id " +
    "INNER JOIN Search.dbo.USERS AS U2 " +
        "ON U2.user_id = G2.user_id ";
                S4App.OpenQuery( sqlQuery );
                S4App.QueryGoFirst( );
                int countRec = S4App.QueryRecordCount( );
                for( int i = 0 ; i < countRec ; i++ )
                {
                    //QueryGoNext( ) returns 1 - перешел на следующую запись 0 -не перешел
                    //QueryEOF( ) returns 1 - конец файла 0 - конец файла не достигнут
                    if( S4App.QueryEOF( ) == 0 )
                    {
                        op = new User( );
                        op.FullName = S4App.QueryFieldByName( "FULLNAME" );
                        op.LoginName = S4App.QueryFieldByName( "LOGINNAME" );
                        op.NameGroup = S4App.QueryFieldByName( "NAME_GROUP" );
                        op.Rank = S4App.QueryFieldByName( "RANK" );
                        if( content.Keys.IndexOf( op.FullName ) != -1 )
                        {
                            content.Add( op.FullName + " " + i , op );
                        }
                        else
                        {
                            content.Add( op.FullName , op );
                        }
                        //System.ArgumentException
                        if( S4App.QueryGoNext( ) != 1 )
                            break;
                    }
                }
                OnWorkerComplete( content );
            }
            catch( Exception erS4Users )
            {
                System.Windows.MessageBox.Show(
                    "Произошла ошибка , приложиние будет закрыто. \n\rТекст ошибки\"" + erS4Users.StackTrace + "\"" ,
                    "Завершение работы приложения" ,
                    System.Windows.MessageBoxButton.OK ,
                    System.Windows.MessageBoxImage.Exclamation );
                Environment.Exit( 0 );
            }
        }
    }
    class MyExcel
    {
        S4.TS4App S4App = null;
        //SortedList<string , User> useresLoaded;
        public delegate void OnWorkerMethodCompleteDelegate( List<List<string>> op );
        public event OnWorkerMethodCompleteDelegate OnExcelMethodComplete;

        public void WorkerMethod()
        {
            List<List<string>> NameNumbDepart = new List<List<string>>( );//для возврата из метода
            //User op;
            try
            {
                DateTime startExel = DateTime.Now;
                //==============ALFABET=ENG== 	( A..Z >>>>	65..90 )	( a..z  >>>  97..122 ) ==
                List<char> alphabet = new List<char>( );
                for( int y = 65 ; y < 90 ; y++ )
                    alphabet.Add( ( char )y );
                //============================
                string serverPath = @"T:\OASU\DOCs\dok\Перечень ПС ОАСУП\перечень ПС ОАСУ.xlsx";
                //string serverPath = @"C:\Users\52758\Desktop\Копия перечень ПС ОАСУ.xlsx";
                string progrRolePath = @"\\sql-main\Application\1210\group.txt";
                if( !File.Exists( progrRolePath) )
                {
                    MessageBox.Show( "Не могу открыть файл \n\"" + progrRolePath + "\"\nпроверьте доступ к файлу" , "Нет доступа к файлу" );
                }
                if( !File.Exists( serverPath ) )
                {
                    MessageBox.Show( "Не могу открыть файл \n\"" + serverPath + "\"\nпроверьте доступ к файлу" , "Нет доступа к файлу" );
                }
                else
                {
                    Excel.Application exclApp = null;
                    int row = 1;// номер строки
                    try
                    {
                        exclApp = new Excel.Application( );
                        exclApp.Visible = false;
                        #region Excel open
                        object MissingValue = System.Reflection.Missing.Value;
                        object UpdateLinks = MissingValue;
                        object ReadOnly = true;
                        object Format = MissingValue;
                        object Password = MissingValue;
                        object WriteResPassword = MissingValue;
                        object IgnoreReadOnlyRecommended = MissingValue;
                        object Origin = MissingValue;
                        object Delimiter = MissingValue;
                        object Editable = MissingValue;
                        object Notify = MissingValue;
                        object Converter = MissingValue;
                        object AddToMru = MissingValue;
                        object Local = MissingValue;
                        object CorruptLoad = MissingValue;
                        //Открываем книгу и получаем на нее ссылку
                        Excel.Workbook exclWB = exclApp.Workbooks.Open( serverPath ,
                            UpdateLinks ,
                            ReadOnly ,
                            Format ,
                            Password ,
                            WriteResPassword ,
                            IgnoreReadOnlyRecommended ,
                            Origin ,
                            Delimiter ,
                            Editable ,
                            Notify ,
                            Converter ,
                            AddToMru ,
                            Local ,
                            CorruptLoad );
                        #endregion
                        //Получаем ссылку на лист 1
                        Excel.Worksheet exclWSh = ( Excel.Worksheet )exclWB.Worksheets.get_Item( 1 );
                        List<string> nameProgs = new List<string>( );//список названий программ
                        List<string> numbProgs = new List<string>( );//список номеров программ
                        List<string> departProgs = new List<string>( );//список использующий ПО подразделений
                        List<string> roleProgs = new List<string>( );//список ролей в программе
                        string val = "";// буфер для ячейки с номером
                        string checkVal = "";

                        object[,] table = exclWSh.get_Range( "A1" , "K2000" ).Value2;
                        do
                        {
                            #region
                            //excelcells = exclWSh.get_Range( "A" + row , MissingValue );
                            //val = Convert.ToString( excelcells.Value2 );
                            //int number = 0;
                            ////пробуем получить номер из ячейки
                            //int.TryParse( val , out number );

                            //excelcells = exclWSh.get_Range( "C" + row , MissingValue );
                            ////получаем содержимое номера программы
                            //checkVal = Convert.ToString( excelcells.Value2 );

                            //if( number != 0 & !( checkVal == null) )
                            //{
                            //    excelcells = exclWSh.get_Range( "B" + row , MissingValue );
                            //    nameProgs.Add( Convert.ToString( excelcells.Value2 ) );
                            //    numbProgs.Add( checkVal );
                            //    excelcells = exclWSh.get_Range( "E" + row , MissingValue );
                            //    departProgs.Add( Convert.ToString( excelcells.Value2 ) );
                            //}/**/
                            //row++;
                            #endregion
                            val = ( table[ row , 1 ] == null ) ? "" : table[ row , 1 ].ToString( );
                            int number = 0;
                            //пробуем получить номер из ячейки
                            int.TryParse( val , out number );
                            //получаем содержимое номера программы
                            checkVal = ( table[ row , 3 ] == null ) ? "" : table[ row , 3 ].ToString( );
                            int tryInt = 0;
                            Int32.TryParse( checkVal, out tryInt );
                            if( number != 0 & ( tryInt > 0 ) & !( checkVal == "" ) )
                            {
                                string name = ( table[ row , 2 ] == null ) ? "" : table[ row , 2 ].ToString( );
                                string depart = ( table[ row , 5 ] == null ) ? "" : table[ row , 5 ].ToString( );
                                nameProgs.Add( name );
                                numbProgs.Add( tryInt.ToString("0000") );
                                departProgs.Add( depart );
                            }
                            row++;
                        }
                        while( !( val == "" & checkVal == "" ) );

                        exclApp.Quit( );
                        NameNumbDepart.Add( nameProgs );//0
                        NameNumbDepart.Add( numbProgs );//1
                        NameNumbDepart.Add( departProgs );//2
                        List<string> tempRoleProgs = GetRoleList( progrRolePath );
                        StringBuilder roleString = new StringBuilder( );
                        foreach( string op in numbProgs )
                        {
                            roleString.Clear( );
                            foreach( string io in tempRoleProgs )
                            {
                                    string roles = io.Trim( );
                                    int l = roles.Length;
                                if( (op.Length == 4)&( l > 4) )
                                {
                                    if( roles.Contains( op ) )
                                    {
                                        roleString.Append( roles + "{#}" );
                                    }
                                }
                            }
                            roleProgs.Add( ( roleString.Length < 3 ) ? roleString.ToString() : roleString.Remove( roleString.Length - 3, 3).ToString( ) );
                        }
                        NameNumbDepart.Add( roleProgs );//3
                    }
                    catch( Exception exExcel )
                    {
                        MessageBox.Show( "Упал на Excel\nВываливаю фул стактрейс ошибки:\n" + exExcel.StackTrace + "\n" + exExcel.Message + "\n индекс=" + row , "OOPS!!!" , MessageBoxButton.OK , MessageBoxImage.Error );
                        if( exclApp != null )
                            exclApp.Quit( );

                    }
                }
                OnExcelMethodComplete( NameNumbDepart );
            }
            catch( Exception erS4Users )
            {
                System.Windows.MessageBox.Show(
                    "Произошла ошибка , список программ ОАСУ не загружен. \n\rТекст ошибки\"" + erS4Users.StackTrace + "\"" ,
                    "Ошибка" ,
                    System.Windows.MessageBoxButton.OK ,
                    System.Windows.MessageBoxImage.Exclamation );
            }
        }

        private List<string> GetRoleList( string progrRolePath )
        {
            List<string> listRoles = new List<string>( );//список номеров программ
            string[] lines = new string[2000];
            if( File.Exists( progrRolePath ) )
            {
                lines = System.IO.File.ReadAllLines( progrRolePath );
                foreach( string op in lines )
                    listRoles.Add( op );
            }

            return listRoles;
        }
    }
    public partial class MainWindow
    {
        /// <summary>
        /// Начало создания потока
        /// </summary>
        private void OnWorkerMethodStart()
        {
            MyClass myC = new MyClass( );
            myC.OnWorkerComplete += new MyClass.OnWorkerMethodCompleteDelegate( OnWorkerMethodComplete );

            ThreadStart tStart = new ThreadStart( myC.WorkerMethod
                );
            Thread t = new Thread( tStart );
            t.Start( );
        }
        /// <summary>
        /// Выполняется при завершенни потока
        /// </summary>
        /// <param name="message"></param>
        private void OnWorkerMethodComplete( SortedList<string , User> op )
        {
            progrB.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal ,
            new Action(
            delegate ()
            {
                progrB.Visibility = System.Windows.Visibility.Hidden;
            }
            ) );

            progrBStatus.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal ,
           new Action(
           delegate ()
           {
               users.SortD = op;
               string output = "Список пользователей загружен.";
               if( progrBStatusExcel.Text == "Список программ загружен." )
               {
                   progrBStatus.Text = "Все списки загружены";
                   progrBStatusExcel.Visibility = System.Windows.Visibility.Hidden;
               }
               else
               {
                   progrBStatus.Text = output;
               }
           }
           ) );

        }

        /// <summary>
        /// Начало создания потока
        /// </summary>
        private void OnExcelMethodStart()
        {
            MyExcel myE = new MyExcel( );
            myE.OnExcelMethodComplete += new MyExcel.OnWorkerMethodCompleteDelegate( OnExcelMethodComplete );

            ThreadStart tStart = new ThreadStart( myE.WorkerMethod
                );
            Thread t = new Thread( tStart );
            t.Start( );
        }
        /// <summary>
        /// Выполняется при завершенни потока
        /// </summary>
        /// <param name="message"></param>
        private void OnExcelMethodComplete( List<List<string>> op )
        {
            progrBExcel.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal ,
            new Action(
            delegate ()
            {
                progrBExcel.Visibility = System.Windows.Visibility.Hidden;
            }
            ) );

            progrBStatusExcel.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal ,
           new Action(
           delegate ()
           {
               progrOASUList = op;
               string output = "Список программ загружен.";
               progrBStatusExcel.Text = output;
               if( progrBStatus.Text == "Список пользователей загружен." )
               {
                   progrBStatus.Text = "Все списки загружены";
                   progrBStatusExcel.Visibility = System.Windows.Visibility.Hidden;
               }
           }
           ) );

        }
    }

    public class User
    {
        public string FullName = "null";
        public string NameGroup = "";
        public string LoginName = "";
        public string Rank = "";

        public User()
        {
            FullName = "null";
            NameGroup = "";
            LoginName = "";
            Rank = "";
        }
        
    }

    public class MyDict
    {
        public SortedList<string , User> SortD = new SortedList<string , User>( );

        internal int Count()
        {
            return this.SortD.Count;
        }

        internal void RemoveAt( int index )
        {
            this.SortD.RemoveAt( index );
        }

        //public MyDict()
        //{
        //    User uu = new User( );
        //    string key = "null";
        //    SortD.Add( key , uu );
        //}
        

        internal IEnumerable<object> Keys()
        {
            return this.SortD.Keys;
        }

        internal bool ContainsKey( string fullName )
        {
            return this.SortD.ContainsKey( fullName );
        }

        internal void Add( string fullName , User op )
        {
            this.SortD.Add( fullName , op );
        }

        internal void TryGetValue( string io , out User op )
        {
            this.SortD.TryGetValue( io , out op );
        }

        internal void Clear()
        {
            SortD.Clear( );
        }
    }
    
    public class UserListLists
    {
        public List<MyDict> List = new List<MyDict>( );

        public UserListLists( )
        {
            MyDict op = new MyDict( );
            this.Add( op );
        }
        
        internal void Add( MyDict oo )
        {
                List.Add( oo );
        }

        internal void RemoveAt( int index )
        {
            List.RemoveAt( index );
        }

        public MyDict Item
        {
            get {
                return List[ 1 ];
            }
            set {
            }
        }

        internal MyDict ItemGet( int index )
        {
            return List[ index ];
        }

        internal void ItemSet( int index , MyDict chngedUserList )
        {
            this.List[ index ] = chngedUserList;
        }

        internal IEnumerable<MyDict> GetList()
        {
             return this.List;
        }
    }
}
