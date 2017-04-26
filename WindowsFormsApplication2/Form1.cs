using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private int viI = 0;
        public Form1()
        {
            InitializeComponent( );
        }
        private void button1_Click( object sender , EventArgs e )
        {
            button1.Enabled = false;
            timer1.Enabled = true;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync( );
        }
        private bool functionThread2()
        {
            for( int i = 0 ; i <= 100 ; i++ )
            {
                backgroundWorker1.ReportProgress( i );
                Thread.Sleep( 100 );
            }
            return true;
        }
        private void backgroundWorker1_DoWork( object sender , DoWorkEventArgs e )
        {
            e.Result = functionThread2( );
        }
        private void backgroundWorker1_RunWorkerCompleted( object sender , RunWorkerCompletedEventArgs e )
        {
            textBox1.Text += "Completed";
        }
        private void backgroundWorker1_ProgressChanged( object sender , ProgressChangedEventArgs e )
        {
            viI = e.ProgressPercentage;
        }
        private void timer1_Tick( object sender , EventArgs e )
        {
            if( viI != 0 && viI <= 100 )
            {
                textBox1.Text += Convert.ToString( viI ) + Environment.NewLine;
                progressBar1.Value = viI;
            }
            if( viI == 100 )
            {
                timer1.Enabled = false;
                button1.Enabled = true;
                viI = 0;
            }
        }
        private void button1_Click_1( object sender , EventArgs e )
        {
            //Методу  RunWorkerAsync можно передавать объект в качестве параметра
            backgroundWorker1.RunWorkerAsync( );
        }
    }
}