using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using MessagingToolkit.Barcode.QrCode.Encoders;

using MessagingToolkit.Barcode.Multi;
using MessagingToolkit.Barcode;
using MessagingToolkit.Barcode.Common;

namespace QRCodeTest
{
    class Program
    {
        
        private static MultiFormatReader barcodeReader = new MultiFormatReader();
        private static MultiFormatWriter barcodeWriter = new MultiFormatWriter();

        static void Main(string[] args)
        {
            var b = BarcodeFormat.QrCode;
            var g = Guid.NewGuid().ToString();
            string t = "data=" + g;
            ByteMatrix bt = barcodeWriter.Encode(t, b, 150, 150);
            Image i = ConvertByteMatrixToImage(bt);
            i.Save(".\\"+g+".png", ImageFormat.Png);

            Bitmap bitmap = new Bitmap(i);

            Result rawResult;
            RGBLuminanceSource r = new RGBLuminanceSource(bitmap, 150, 150);
            GlobalHistogramBinarizer x = new GlobalHistogramBinarizer(r);
            BinaryBitmap bitmap2 = new BinaryBitmap(x);
            var hints = new System.Collections.Hashtable();
            //hints.Add(DecodeHintType.PossibleFormats, BarcodeFormat.QrCode);
            int count = 0;
            {
                try
                {
                    rawResult = barcodeReader.Decode(bitmap2, hints);
                    if (rawResult != null)
                    {
                        count++;
                        var y = rawResult.Text;
                        var z = rawResult.BarcodeFormat.ToString();
                    }
                }
                catch (ReaderException e)
                {
                    return;
                }
            }

        }





        public enum RGBcoefficientType
        {
            BT709,
        }
        public class GrayScaleCoef
        {
            public double cr;
            public double cg;
            public double cb;

            public GrayScaleCoef(double cr, double cg, double cb)
            {
                this.cr = cr;
                this.cg = cg;
                this.cb = cb;
            }
        }


        private static unsafe void GetGrayScaleImage(BitmapData sourceData, BitmapData destinationData, GrayScaleCoef coef)
        {
            // get width and height
            int width = sourceData.Width;
            int height = sourceData.Height;


            PixelFormat srcPixelFormat = sourceData.PixelFormat;

            if (
                (srcPixelFormat == PixelFormat.Format24bppRgb) ||
                (srcPixelFormat == PixelFormat.Format32bppRgb) ||
                (srcPixelFormat == PixelFormat.Format32bppArgb))
            {
                int pixelSize = (srcPixelFormat == PixelFormat.Format24bppRgb) ? 3 : 4;
                int srcOffset = sourceData.Stride - width * pixelSize;
                int dstOffset = destinationData.Stride - width;

                // do the job
                byte* src = (byte*)sourceData.Scan0.ToPointer();
                byte* dst = (byte*)destinationData.Scan0.ToPointer();

                // for each line
                for (int y = 0; y < height; y++)
                {
                    // for each pixel
                    for (int x = 0; x < width; x++, src += pixelSize, dst++)
                    {
                        *dst = (byte)(coef.cr * src[2] + coef.cg * src[1] + coef.cb * src[0]);
                    }
                    src += srcOffset;
                    dst += dstOffset;
                }
            }
        }
        public static Bitmap GrayScale(Bitmap image, RGBcoefficientType otype)
        {
            Bitmap dstimage = null;
            try
            {
                GrayScaleCoef coef;
                if (otype == RGBcoefficientType.BT709)
                    coef = new GrayScaleCoef(0.2125, 0.7154, 0.0721);
                else
                    coef = new GrayScaleCoef(0.5, 0.419, 0.081);


                BitmapData sourceData, destinationData;

                sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                dstimage = CreateGrayscaleImage(image.Width, image.Height);
                destinationData = dstimage.LockBits(new Rectangle(0, 0, dstimage.Width, dstimage.Height), ImageLockMode.ReadWrite, dstimage.PixelFormat);
                GetGrayScaleImage(sourceData, destinationData, coef);
                dstimage.UnlockBits(destinationData);
                image.UnlockBits(sourceData);
            }
            catch
            {

            }
            return dstimage;
        }
        public static Bitmap CreateGrayscaleImage(int width, int height)
        {
            // create new image
            Bitmap image = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            // set palette to grayscale
            // get palette
            ColorPalette cp = image.Palette;
            // init palette
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(i, i, i);
            }
            // set palette back
            image.Palette = cp;
            // return new image
            return image;
        }
        private static unsafe Bitmap ConvertByteMatrixToImage(ByteMatrix bm)
        {
            Bitmap image = CreateGrayscaleImage(bm.Width, bm.Height);
            BitmapData sourceData;

            sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            int width = sourceData.Width;
            int height = sourceData.Height;
            int srcOffset = sourceData.Stride - width;
            byte* src = (byte*)sourceData.Scan0.ToPointer();
            for (int y = 0; y < height; y++)
            {
                // for each pixel
                for (int x = 0; x < width; x++, src++)
                {
                    *src = (byte)bm.Array[y][x];
                }
                src += srcOffset;
            }

            image.UnlockBits(sourceData);
            return image;
        }

    }
}
