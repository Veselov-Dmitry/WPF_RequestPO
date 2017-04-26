using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main( string[] args )
        {
            Word.Application word = new Word.Application( ); //создаем COM-объект Word
            Word.Document wordDocument;
            try
            {
                word.Visible = false;

                Object patternFile = @"D:\ЗАЯВКА.dot";
                //Object patternFile = @"C:\Users\52758\Desktop\ЗАЯВКА.dot";
                word.Documents.Open( ref patternFile );
                wordDocument = word.Documents.Application.ActiveDocument;
                Word.Table tableType = wordDocument.Shapes[ 1 ].TextFrame.TextRange.Tables[ 1 ];
                Word.Table tableContent = wordDocument.Tables[ 1 ];
                Word.Table tableStamp = wordDocument.Shapes[ 1 ].TextFrame.TextRange.Tables[ 2 ];
                tableType.Cell( 1 , 1 ).Range.Text = "Test";
                tableContent.Cell( 1 , 1 ).Range.Text = "Test";
                tableStamp.Cell( 1 , 1 ).Range.Text = "Test";

                Object fileName = @"C:\Users\52758\Desktop\ЗАЯВКА.doc";
                wordDocument.SaveAs2( ref fileName );

            }
            catch( Exception e ) {
                Console.WriteLine( e.StackTrace);
            }
            finally
            {
                word.Quit( );
                Console.Write( "______________________________________\n\rDone!\n\rPress any key...");
                Console.ReadKey( );
            }

        }
    }
}
