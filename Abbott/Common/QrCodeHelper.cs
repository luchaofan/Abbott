using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;

namespace Common
{
    public class QrCodeHelper
    {

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
        public Bitmap ImageWatermark(Bitmap map, string waterpath)
        {
            Image waterimg = Image.FromFile(waterpath);

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
            g.DrawImage(waterimg, new Rectangle(200, 400, 200, 20));
            //g.DrawImage(waterimg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), waterimg.Width, waterimg.Height));
            return map;
        }


        //图片水印处理方法
        public Bitmap ImageWatermarkToRiget(PicCard card, Bitmap map, string waterpath)
        {
            int wid = 80;
            int high = 80;
            Font fontSetting = new Font("宋体", 5, FontStyle.Bold);
            //绘笔颜色
            SolidBrush brush = new SolidBrush(Color.Red);

            Bitmap waterimg = new Bitmap(200, 20);
            Graphics g = Graphics.FromImage(waterimg);
            g.Clear(ColorTranslator.FromHtml("#f0f0f0"));
            //绘制图片
            g.DrawString("中国上海闵行支部 20160101-20160908 中国上海闵行支部 上海周年庆活动广州分站台", fontSetting, brush, new Rectangle(0, 0, 200, 20));
            //g.DrawString(card.Mobile, fontSetting, Brushes.Red, new Rectangle(0, 15, 200, 10));
            //g.DrawString(card.Address, fontSetting, Brushes.Red, new Rectangle(0, 25, 200, 10));
            //g.DrawString(card.Store, fontSetting, Brushes.Red, new Rectangle(0, 30, 35, 10));
            //g.DrawString(card.StartDate.ToString(), fontSetting, Brushes.Red, new Rectangle(0, 45, 200, 10));
            //g.DrawString(card.EndDate.ToString(), fontSetting, Brushes.Red, new Rectangle(0, 55, 200, 10));
            waterimg.Save(waterpath, ImageFormat.Bmp);
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
    }
}
