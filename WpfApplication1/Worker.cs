using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class Worker
    {
        public static int[] FindPrimes( int fromNumber , int toNumber )
        {
            return FindPrimes( fromNumber , toNumber , null );
        }

        public static int[] FindPrimes( int fromNumber , int toNumber , System.ComponentModel.BackgroundWorker backgroundWorker )
        {
            int[] list = new int[ toNumber - fromNumber ];

            // Создать массив, содержащий все целые числа
            for( int i = 0 ; i < list.Length ; i++ )
            {
                list[ i ] = fromNumber;
                fromNumber += 1;
            }

            // Числа, кратные всем простым числам, меньшим или равным квадратному 
            // корню из максимального числа отмечаем цифрой 0 - это обычные числа.
            // Все остальные отмечаем 1 - это простые числа
            int maxDiv = ( int )Math.Floor( Math.Sqrt( toNumber ) );

            int[] mark = new int[ list.Length ];


            for( int i = 0 ; i < list.Length ; i++ )
            {
                for( int j = 2 ; j <= maxDiv ; j++ )
                {

                    if( ( list[ i ] != j ) && ( list[ i ] % j == 0 ) )
                    {
                        mark[ i ] = 1;
                    }

                }

                int iteration = list.Length / 100;
                if( ( i % iteration == 0 ) && ( backgroundWorker != null ) )
                {
                    if( backgroundWorker.CancellationPending )
                    {
                        // Возврат без какой-либо дополнительной работы
                        return null;
                    }

                    if( backgroundWorker.WorkerReportsProgress )
                    {
                        //float progress = ((float)(i + 1)) / list.Length * 100;
                        backgroundWorker.ReportProgress( i / iteration );
                        //(int)Math.Round(progress));
                    }
                }

            }

            // Cоздать новый массив, который содержит только простые числа, и вернуть этот массив
            int primes = 0;
            for( int i = 0 ; i < mark.Length ; i++ )
            {
                if( mark[ i ] == 0 )
                    primes += 1;

            }

            int[] ret = new int[ primes ];
            int curs = 0;
            for( int i = 0 ; i < mark.Length ; i++ )
            {
                if( mark[ i ] == 0 )
                {
                    ret[ curs ] = list[ i ];
                    curs += 1;
                }
            }

            if( backgroundWorker != null && backgroundWorker.WorkerReportsProgress )
            {
                backgroundWorker.ReportProgress( 100 );
            }

            return ret;

        }
    }
}
