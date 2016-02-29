using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Common
{
    public class SignInCodeHelper
    {

        public Bitmap GetSignInCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                code += "\n";
            QRCodeWriter writer = new QRCodeWriter();
            ByteMatrix matrix;

            int size = 420;
            matrix = writer.encode(code, BarcodeFormat.QR_CODE, size, size, null);

            Bitmap img = new Bitmap(matrix.Width, matrix.Height);
            System.Drawing.Color Color = Color.FromArgb(0, 0, 0);

            for (int y = 0; y < img.Height; ++y)
            {
                for (int x = 0; x < img.Width; ++x)
                {
                    System.Drawing.Color pixelColor = img.GetPixel(x, y);

                    //Find the colour of the dot
                    if (matrix.get_Renamed(x, y) == -1)
                    {
                        img.SetPixel(x, y, System.Drawing.Color.White);
                    }
                    else
                    {
                        img.SetPixel(x, y, System.Drawing.Color.Black);
                    }
                }
            }
            return img;

        }

        //包含名片的二维码
        public Bitmap CreateCardCode(PicCard card, int width)
        {
            string code = string.Format(@"Name:{0};Mobile:{1};Address:{2};Store:{3};StartDate:{4};EndDate:{5};",
                card.Name, card.Mobile, card.Address, card.Store, card.StartDate, card.EndDate);
            return CreateQRCode(code, width, true);
        }

        public Bitmap CreateQRCode(string code, int size, bool autoPadding)
        {
            QRCodeWriter writer = new QRCodeWriter();
            ByteMatrix matrix;

            if (autoPadding)
                matrix = writer.encode(code + "\n", BarcodeFormat.QR_CODE, size, size, null, autoPadding);
            else
                matrix = writer.encode(code + "\n", BarcodeFormat.QR_CODE, size * 3, size * 3, null, autoPadding);

            Bitmap img = new Bitmap(matrix.Width, matrix.Height);
            for (int y = 0; y < matrix.Height; ++y)
                for (int x = 0; x < matrix.Width; ++x)
                    img.SetPixel(x, y,
                                 matrix.get_Renamed(x, y) == -1
                                     ? System.Drawing.Color.White
                                     : System.Drawing.Color.Black);
            if (matrix.Width > size)
            {
                System.Drawing.Bitmap final_image = new System.Drawing.Bitmap(size, size);
                System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White),
                                      new System.Drawing.Rectangle(0, 0, size, size));
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(img, 0, 0, size, size);

                return final_image;
            }
            return img;
        }

        //图片水印处理方法
        public Bitmap ImageWatermark(string map, string waterpath)
        {
            Image waterimg = Image.FromFile(waterpath);//水印图片

            //添加水印
            Graphics g;
            Bitmap bmp;
            using (Image img = Image.FromFile(map)) //二维码图片
            {
                //如果原图片是索引像素格式之列的，则需要转换
                if (IsPixelFormatIndexed(img.PixelFormat))
                {
                    bmp = new Bitmap(img.Width + 140, img.Height + 140, PixelFormat.Format24bppRgb);//Format32bppArgb
                    g = Graphics.FromImage(bmp);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(img, 0, 0);

                }
                else //否则直接操作
                {
                    bmp = new Bitmap(img.Width + 140, img.Height + 140, PixelFormat.Format32bppArgb);
                    g = Graphics.FromImage(bmp);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(img, 0, 0);
                }
                //获取水印位置设置
                ArrayList loca = new ArrayList();
                int x = 0;
                int y = 0;
                x = img.Width / 2 - waterimg.Width / 2;
                y = img.Height / 2 - waterimg.Height / 2;
                loca.Add(x);
                loca.Add(y);
                g.DrawImage(waterimg, new Rectangle(370, 550, 200, 20));
                //g.DrawImage(waterimg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), waterimg.Width, waterimg.Height));
                return bmp;
            }
        }

        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
            PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed
        };


        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

        //图片水印处理方法
        public Bitmap ImageWatermark(Bitmap map, string waterpath, string projectName)
        {

            Image waterimg = Image.FromFile(waterpath);
            int height = 25;
            if (projectName.Length == 4)
            {
                height = 60;
            }
            //添加水印
            Graphics g = Graphics.FromImage(map);

            //获取水印位置设置
            ArrayList loca = new ArrayList();
            int x = 0;
            int y = 0;
            x = map.Width / 2 - waterimg.Width / 2;
            y = map.Height / 2 - waterimg.Height / 2;
            loca.Add(x);
            loca.Add(y);
            g.DrawImage(waterimg, new Rectangle(180, 190, 68, height));
            //g.DrawImage(waterimg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), waterimg.Width, waterimg.Height));
            g.Dispose();
            waterimg.Dispose();
            return map;
        }

        //图片水印处理方法
        public Bitmap StringWatermark(PicCard card, Bitmap map)
        {
            Font fontSetting = new Font("微软雅黑", 14, FontStyle.Bold);
            //SolidBrush brush = new SolidBrush(Color.DarkOrange);
            HatchBrush brush = new HatchBrush(HatchStyle.DarkHorizontal,
                Color.DarkOrange, Color.Aquamarine);
            //添加水印
            Graphics g = Graphics.FromImage(map);

            g.DrawString(card.Name, fontSetting, brush, new Rectangle(200, 310, 200, 10));
            g.DrawString(card.Mobile, fontSetting, Brushes.Red, new Rectangle(200, 320, 200, 10));
            g.DrawString(card.Address, fontSetting, Brushes.Red, new Rectangle(200, 330, 200, 10));
            g.DrawString(card.Store, fontSetting, Brushes.Red, new Rectangle(200, 340, 200, 10));
            g.DrawString(card.StartDate.ToString(), fontSetting, Brushes.Red, new Rectangle(200, 350, 200, 10));
            g.DrawString(card.EndDate.ToString(), fontSetting, Brushes.Red, new Rectangle(200, 360, 200, 10));
            g.Dispose();
            return map;
        }

        //图片水印处理方法
        public string FontToImg(PicCard card, string filePath)
        {
            int wid = 150;
            int high = 80;
            Font font = new Font("Arial", 48, FontStyle.Bold);
            //绘笔颜色
            SolidBrush brush = new SolidBrush(Color.White);

            Bitmap image = new Bitmap(wid, high);
            Graphics g = Graphics.FromImage(image);
            g.Clear(ColorTranslator.FromHtml("#f0f0f0"));
            RectangleF rect = new RectangleF(5, 2, wid, high);
            string name = string.Format(@"Name:{0};\n Mobile:{1}; \n Address:{2}; \n Store:{3}; \nStartDate:{4}; \n EndDate:{5};",
                card.Name, card.Mobile, card.Address, card.Store, card.StartDate, card.EndDate);
            //绘制图片
            g.DrawString(name, font, brush, rect);
            //保存图片
            image.Save(filePath, ImageFormat.Jpeg);
            //释放对象
            g.Dispose();
            image.Dispose();

            return filePath;
        }

        //图片水印处理方法
        public Bitmap ImageWatermarkToRiget(string name, Bitmap map, string waterpath)
        {
            int wid = 150;
            int high = 150;
            int erheight = 25;
            int nameLen = name.Length;

            if (nameLen == 4)
            {
                erheight = 60;
                name = name.Substring(0, 2) + "\n" + name.Substring(2, 2);
            }

            Font fontSetting = new Font("微软雅黑", 14, FontStyle.Bold);
            //绘笔颜色
            SolidBrush brush = new SolidBrush(Color.Blue);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Bitmap waterimg = new Bitmap(68, erheight);
            Graphics g = Graphics.FromImage(waterimg);
            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));
            //绘制图片
            g.DrawString(name, fontSetting, brush, new Rectangle(0, 0, 68, erheight), sf);
            //g.DrawString(card.Mobile, fontSetting, Brushes.Red, new Rectangle(0, 15, 200, 10));
            //g.DrawString(card.Address, fontSetting, Brushes.Red, new Rectangle(0, 25, 200, 10));
            //g.DrawString(card.Store, fontSetting, Brushes.Red, new Rectangle(0, 30, 35, 10));
            //g.DrawString(card.StartDate.ToString(), fontSetting, Brushes.Red, new Rectangle(0, 45, 200, 10));
            //g.DrawString(card.EndDate.ToString(), fontSetting, Brushes.Red, new Rectangle(0, 55, 200, 10));
            waterimg.Save(waterpath, ImageFormat.Jpeg);
            g.Dispose();
            sf.Dispose();
            return waterimg;

        }

        public class ImageCut
        {

            /// <summary>
            /// 剪裁 -- 用GDI+
            /// </summary>
            /// <param name="b">原始Bitmap</param>
            /// <param name="StartX">开始坐标X</param>
            /// <param name="StartY">开始坐标Y</param>
            /// <param name="iWidth">宽度</param>
            /// <param name="iHeight">高度</param>
            /// <returns>剪裁后的Bitmap</returns>
            public Bitmap KiCut(Bitmap b)
            {
                if (b == null)
                {
                    return null;
                }
                int w = b.Width;
                int h = b.Height;
                int intWidth = 0;
                int intHeight = 0;
                if (h * Width / w > Height)
                {
                    intWidth = Width;
                    intHeight = h * Width / w;
                }
                else if (h * Width / w < Height)
                {
                    intWidth = w * Height / h;
                    intHeight = Height;

                }
                else
                {
                    intWidth = Width;
                    intHeight = Height;
                }
                Bitmap bmpOut_b = new System.Drawing.Bitmap(b, intWidth, intHeight);
                w = bmpOut_b.Width;
                h = bmpOut_b.Height;

                if (X >= w || Y >= h)
                {
                    return null;
                }
                if (X + Width > w)
                {
                    Width = w - X;
                }
                else
                {
                    X = (w - Width) / 2;
                }
                if (Y + Height > h)
                {
                    Height = h - Y;
                }


                try
                {
                    Bitmap bmpOut = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(bmpOut);
                    g.DrawImage(bmpOut_b, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, Width, Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    return bmpOut;
                }
                catch
                {
                    return null;
                }
            }

            public int X = 0;
            public int Y = 0;
            public int Width = 120;
            public int Height = 120;
            public ImageCut(int x, int y, int width, int heigth)
            {
                X = x;
                Y = y;
                Width = width;
                Height = heigth;
            }
        }

        public class PicCard
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string Store { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

        }

        ////(实心刷)
        //Rectangle myrect1 = new Rectangle(20, 80, 250, 100);
        //SolidBrush mysbrush1 = new SolidBrush(Color.DarkOrchid);
        //SolidBrush mysbrush2 = new SolidBrush(Color.Aquamarine);
        ////(梯度刷)
        //LinearGradientBrush mylbrush5 = new LinearGradientBrush(rect1,
        //Color.DarkOrange, Color.Aquamarine,
        //LinearGradientMode.BackwardDiagonal);

        ////(阴影刷)
        //HatchBrush myhbrush5 = new HatchBrush(HatchStyle.DiagonalCross,
        // Color.DarkOrange, Color.Aquamarine);
        //HatchBrush myhbrush2 = new HatchBrush(HatchStyle.DarkVertical,
        //Color.DarkOrange, Color.Aquamarine);
        //HatchBrush myhbrush3 = new HatchBrush(HatchStyle.LargeConfetti,
        //Color.DarkOrange, Color.Aquamarine);

        ////(纹理刷)
        //TextureBrush textureBrush = new TextureBrush(new Bitmap(@"e:\123.jpg"));

    }

}
