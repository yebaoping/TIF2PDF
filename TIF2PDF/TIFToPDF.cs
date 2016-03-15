using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TIF2PDF
{
    /// <summary>
    /// tif文件转换成pdf文件类
    /// </summary>
    public class TIFToPDF
    {
        /// <summary>
        /// tif文件转换出现异常事件，用于多线程环境下处理异常，非多线程程序也可使用，但尽量不要使用
        /// </summary>
        public event ThreadExceptionEventHandler ConverTIFToPDFExceptionEvent;

        /// <summary>
        /// tif文件转换完成事件，用于处理多线程环境下转换完成后执行后期操作。多线程程序可以使用AutoResetEvent对象等待线程执行结束之后执行后期操作。非多线程程序也可使用，但尽量不要使用
        /// </summary>
        public event EventHandler ConverTIFToPDFCompletedEvent;

        /// <summary>
        /// pdf文件保存路径
        /// </summary>
        public string PDFFileName { get; set; }

        /// <summary>
        /// 要转换的tif文件路径
        /// </summary>
        public string TIFFileName { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TIFToPDF()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tifFileName">要转换的tif文件路径</param>
        /// <param name="pdfFileName">pdf文件保存路径</param>
        public TIFToPDF(string tifFileName, string pdfFileName)
        {
            this.PDFFileName = pdfFileName;
            this.TIFFileName = tifFileName;
        }

        /// <summary>
        /// tif文件转换成pdf文件
        /// </summary>
        public void ConverTIFToPDF()
        {
            ConverTIFToPDF(this.TIFFileName, this.PDFFileName);
            /*
            FileStream fs = null;
            Document doc = new Document(PageSize.A4);//建立Document对象的实例，并设置Document的大小与边距
            PdfWriter writer = null;

            try
            {
                fs = File.Create(this.PDFFileName);

                Bitmap bm = new Bitmap(this.TIFFileName);//打开TIFF文件

                int pageTotal = bm.GetFrameCount(FrameDimension.Page);//获取当前文件图像个数
                float scalePercent = (doc.PageSize.Width / bm.Width + doc.PageSize.Height / bm.Height) / 2f * 100;

                writer = PdfWriter.GetInstance(doc, fs);//建立一个PdfWriter对象，Writer与document对象关联，通过Writer可以将文档写入到磁盘中

                doc.Open();//打开文档

                PdfContentByte contentByte = writer.DirectContent;

                for (int i = 0; i < pageTotal; ++i)
                {
                    bm.SelectActiveFrame(FrameDimension.Page, i);

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bm, null, true);

                    img.ScalePercent(scalePercent);
                    img.SetAbsolutePosition(0, 0);

                    contentByte.AddImage(img);

                    doc.NewPage();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null && doc.IsOpen())
                    doc.Close();

                if (fs != null)
                    fs.Close();
            }
             */
        }

        /// <summary>
        /// tif文件转换成pdf文件
        /// </summary>
        /// <param name="tifFileName">要转换的tif文件路径</param>
        /// <param name="pdfFileName">pdf文件保存路径</param>
        public void ConverTIFToPDF(string tifFileName, string pdfFileName)
        {
            FileStream fs = null;
            Document doc = new Document(PageSize.A4);//建立Document对象的实例，并设置Document的大小与边距
            PdfWriter writer = null;

            try
            {
                fs = File.Create(pdfFileName);

                Bitmap bmp = new Bitmap(tifFileName);//打开TIFF文件

                int pageTotal = bmp.GetFrameCount(FrameDimension.Page);//获取当前文件图像个数
                float scalePercent = (doc.PageSize.Width / bmp.Width + doc.PageSize.Height / bmp.Height) / 2f * 100;

                writer = PdfWriter.GetInstance(doc, fs);//建立一个PdfWriter对象，Writer与document对象关联，通过Writer可以将文档写入到磁盘中

                doc.Open();//打开文档

                PdfContentByte contentByte = writer.DirectContent;

                for (int i = 0; i < pageTotal; ++i)
                {
                    bmp.SelectActiveFrame(FrameDimension.Page, i);

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bmp, null, true);

                    img.ScalePercent(scalePercent);
                    img.SetAbsolutePosition(0, 0);

                    contentByte.AddImage(img);

                    doc.NewPage();
                }

                if (ConverTIFToPDFCompletedEvent != null)
                    ConverTIFToPDFCompletedEvent(this, EventArgs.Empty);
            }
            catch (System.Exception ex)
            {
                if (ConverTIFToPDFExceptionEvent != null)
                    ConverTIFToPDFExceptionEvent(this, new ThreadExceptionEventArgs(ex));
                else
                    throw ex;
            }
            finally
            {
                if (doc != null && doc.IsOpen())
                    doc.Close();

                if (fs != null)
                    fs.Close();
            }
        }
    }
}
