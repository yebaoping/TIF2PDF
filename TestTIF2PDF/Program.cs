using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIF2PDF;
using System.IO;
using System.Threading;

namespace TestTIF2PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\PDF\1.pdf"))
                File.Delete(@"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\PDF\1.pdf");

            TIFToPDF tifToPdf = new TIFToPDF(@"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\TIF\1.tif", @"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\PDF\1.pdf");

#region 多线程处理
            /*
            Thread thread = new Thread(() =>
            {
                tifToPdf.ConverTIFToPDFExceptionEvent += (sender, e) =>
                {
                    Console.WriteLine(e.Exception.Message);
                };

                tifToPdf.ConverTIFToPDFCompletedEvent += (send, eventArgs) =>
                {
                    //执行转换后的操作
                    Console.WriteLine("转换成功……");
                };

                tifToPdf.ConverTIFToPDF();
            });
            thread.Start();
             */

            /*
            Thread thread = new Thread(() =>
            {
                TIFToPDF tifToPdf = new TIFToPDF(@"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\TIF\1.tif", @"E:\Code\C#\Temp\TIF2PDF\TestTIF2PDF\PDF\1.pdf");

                tifToPdf.ConverTIFToPDF();

                Console.WriteLine("转换成功……");
            });
            thread.Start();
             */
#endregion

            tifToPdf.ConverTIFToPDF();
            Console.WriteLine("转换成功……");

            Console.Read();
        }
    }
}
