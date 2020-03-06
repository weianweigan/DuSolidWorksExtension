//using stdole;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Media.Imaging;

//namespace Du.SolidWorks.Extension
//{
//    public sealed class IPictureDispHelper : AxHost
//    {

//        private IPictureDispHelper() : base("") { }


//        static public stdole.IPictureDisp ImageToPictureDisp(Image image)
//        {
//            return (stdole.IPictureDisp)GetIPictureDispFromPicture(image);
//        }



//        static public Image PictureDispToImage(stdole.IPictureDisp pictureDisp)
//        {

//            return GetPictureFromIPicture(pictureDisp);

//        }


//        public static BitmapImage GetBitmapFromImage(Image image)
//        {
//            System.IO.MemoryStream ms = new System.IO.MemoryStream();
//            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
//            BitmapImage bi =new BitmapImage();
//            bi.BeginInit();
//            bi.StreamSource = new MemoryStream(ms.ToArray()); // 不要直接使用 ms
//            bi.EndInit();
//            ms.Close();

//            return bi;
//        }
//    }
//}
