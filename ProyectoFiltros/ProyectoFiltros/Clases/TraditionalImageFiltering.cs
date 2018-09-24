using ProyectoFiltros.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros
{
    class TraditionalImageFiltering
    {


        private string toPath;

        public TraditionalImageFiltering()
        {
            
            this.toPath = Directory.GetCurrentDirectory() + "\\outputimages\\" + "image";
        }

        private void saveImage(Bitmap image)
        {
            image.Save(toPath + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg");
        }

        
        public void sepiaFilter(Bitmap image)
        {

            Bitmap bmpNew = new Bitmap(image);

            BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            IntPtr ptr = bmpData.Scan0;


            byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];


            Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

            int limit = byteBuffer.Length;

            float r, b, g;


            for (int i = 0; i < limit ; i += 4)
            {
                r = 0.393f * byteBuffer[i + 2] + 0.769f * byteBuffer[i + 1] + 0.189f * byteBuffer[i];
                g = 0.349f * byteBuffer[i + 2] + 0.686f * byteBuffer[i + 1] + 0.168f * byteBuffer[i];
                b = 0.272f * byteBuffer[i + 2] + 0.534f * byteBuffer[i + 1] + 0.131f * byteBuffer[i];

                if (r > 255)
                {
                    r = 255;
                }

                if (g > 255)
                {
                    g = 255;
                }
                if (b > 255)
                {
                    b = 255;
                }

                byteBuffer[i + 2] = (byte)r;
                byteBuffer[i + 1] = (byte)g;
                byteBuffer[i] = (byte)b;

            }


            Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);


            bmpNew.UnlockBits(bmpData);

            bmpData = null;
            byteBuffer = null;

            saveImage(bmpNew);

            return;

        }

        public void grayScaleFilter(Bitmap image)
        {

            Bitmap bmpNew = new Bitmap(image);

            BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;


            byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];


            Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

            int limit = byteBuffer.Length;
            

            for (int i = 0; i < limit; i += 4)
            {

                byteBuffer[i] = (byte)((byteBuffer[i] + byteBuffer[i + 1] + byteBuffer[i + 2]) / 3f);
                byteBuffer[i + 1] = byteBuffer[i + 2] = byteBuffer[i];
                
            }


            Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);


            bmpNew.UnlockBits(bmpData);

            bmpData = null;
            byteBuffer = null;

            saveImage(bmpNew);

            return;

        }

        public void opacityFilter(Bitmap bmp, double opacity)
        {
            const int bytesPerPixel = 4;

            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format32bppArgb;

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels 
            // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            // Copy the ARGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);


            // Manipulate the bitmap, such as changing the
            // RGB values for all pixels in the the bitmap.
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)

                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;

                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value

                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
            }

            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            saveImage(bmp);
        }

        public void invertColorsFilter(Bitmap image)
        {
            for (int y = 0; (y <= (image.Height - 1)); y++)
            {
                for (int x = 0; (x <= (image.Width - 1)); x++)
                {
                    Color inv = image.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    image.SetPixel(x, y, inv);
                }
            }
            saveImage(image);
        }

        public void GaussinBlurFilter(Bitmap image, Rectangle rectangle, int blurSize)
        {

            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }



            this.saveImage(blurred);
        }

        public void brightFilter(Bitmap image, double bright)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Imaging.BitmapData bmpData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * image.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            double correctionFactortemp = bright;
            if (bright < 0)
            {
                correctionFactortemp = 1 + bright;
            }
            for (int counter = 1; counter < rgbValues.Length; counter++)
            {
                double color = (double)rgbValues[counter];
                if (bright < 0)
                {
                    color *= (int)correctionFactortemp;
                }
                else
                {
                    color = (255 - color) * bright + color;
                }
                rgbValues[counter] = (byte)color;
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            image.UnlockBits(bmpData);
            saveImage(image);
        }    

        public void colorsBalance(Bitmap image)

        {
            Bitmap bmpNew = new Bitmap(image);

            BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            IntPtr ptr = bmpData.Scan0;


            byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];


            Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

            int limit = byteBuffer.Length;

            float blue, green, red;


            for (int i = 0; i < limit; i += 4)
            {
               
                blue = 255.0f / 147f * byteBuffer[i];
                green = 255.0f / 150f * byteBuffer[i + 1];
                red = 255.0f / 127f * byteBuffer[i + 2];

                if (blue > 255)
                {
                    blue = 255;
                }
                else if (blue < 0)
                {
                    blue = 0;
                }

                if (green > 255)
                {
                    green = 255;
                }
                else if (green < 0)
                {
                    green = 0;
                }

                if (red > 255)
                {
                    red = 255;
                }
                else if (red < 0)
                {
                    red = 0;
                }

                byteBuffer[i] = (byte)blue;
                byteBuffer[i + 1] = (byte)green;
                byteBuffer[i + 2] = (byte)red;

            }

            Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);


            bmpNew.UnlockBits(bmpData);

            bmpData = null;
            byteBuffer = null;

            saveImage(bmpNew);

            return;

        }

        public void colorSubstitution(Bitmap image, ColorSubstitutionFilter changer)
        {

            Bitmap bmpNew = new Bitmap(image);

            BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            IntPtr ptr = bmpData.Scan0;


            byte[] resultBuffer = new byte[bmpData.Stride * bmpNew.Height];


            Marshal.Copy(ptr, resultBuffer, 0, resultBuffer.Length);

            int limit = resultBuffer.Length;

            //almacenamiento de variables temporales

            byte sourceRed = 0, sourceGreen = 0, sourceBlue = 0, sourceAlpha = 0;
            int resultRed = 0, resultGreen = 0, resultBlue = 0;

            byte minValue = 0;
            byte maxValue = 255;

            Color sourceColor = changer.getSourceColor();

            Color newColor = changer.getNewColor();

            int colorThreshold = changer.getThreshold();

            

            for (int k = 0; k < limit; k += 4)
            {
                sourceAlpha = resultBuffer[k + 3];

                if (sourceAlpha != 0)
                {
                    //obtener valores del pixel
                    sourceBlue = resultBuffer[k];
                    sourceGreen = resultBuffer[k + 1];
                    sourceRed = resultBuffer[k + 2];


                    if ((sourceBlue < sourceColor.B + colorThreshold &&
                            sourceBlue > sourceColor.B - colorThreshold) &&


                        (sourceGreen < sourceColor.G + colorThreshold &&
                            sourceGreen > sourceColor.G - colorThreshold) &&


                        (sourceRed < sourceColor.R + colorThreshold &&
                            sourceRed > sourceColor.R - colorThreshold))
                    {
                        resultBlue = sourceColor.B - sourceBlue + newColor.B;


                        if (resultBlue > maxValue)
                        { resultBlue = maxValue; }
                        else if (resultBlue < minValue)
                        { resultBlue = minValue; }


                        resultGreen = sourceColor.G - sourceGreen + newColor.G;


                        if (resultGreen > maxValue)
                        { resultGreen = maxValue; }
                        else if (resultGreen < minValue)
                        { resultGreen = minValue; }


                        resultRed = sourceColor.R - sourceRed + newColor.R;


                        if (resultRed > maxValue)
                        { resultRed = maxValue; }
                        else if (resultRed < minValue)
                        { resultRed = minValue; }


                        resultBuffer[k] = (byte)resultBlue;
                        resultBuffer[k + 1] = (byte)resultGreen;
                        resultBuffer[k + 2] = (byte)resultRed;
                        resultBuffer[k + 3] = sourceAlpha;
                    }
                }
            }

            Marshal.Copy(resultBuffer, 0, ptr, resultBuffer.Length);


            bmpNew.UnlockBits(bmpData);

            bmpData = null;
            resultBuffer = null;

            saveImage(bmpNew);

            return;
        }

        public void solariseFilter(Bitmap sourceBitmap, byte blueValue, byte greenValue, byte redValue)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                    sourceBitmap.Width, sourceBitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            byte byte255 = 255;


            for (int k = 0; k < pixelBuffer.Length; k += 4)
            {
                if (pixelBuffer[k] < blueValue)
                {
                    pixelBuffer[k] = (byte)(byte255 - pixelBuffer[k]);
                }


                if (pixelBuffer[k + 1] < greenValue)
                {
                    pixelBuffer[k + 1] = (byte)(byte255 - pixelBuffer[k + 1]);
                }


                if (pixelBuffer[k + 2] < redValue)
                {
                    pixelBuffer[k + 2] = (byte)(byte255 - pixelBuffer[k + 2]);
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                     ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            saveImage(resultBitmap);
        }

        //--------------------------------------------------------------------------------------------------------------
        private Bitmap GrayScale(Bitmap image)
        {
            int width = image.Width;
            int heigth = image.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int grayValue = (pixel.G + pixel.R + pixel.B) / 3;
                    Color newColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    image.SetPixel(x, y, newColor);
                }
            }
            return image;
        }

        public void EdgeFilter(Bitmap image)
        {
            Bitmap grayImage = GrayScale(image);
            int[][] xs = new int[3][];
            xs[0] = new int[3] { -1, 0, 1 };
            xs[1] = new int[3] { -2, 0, 2 };
            xs[2] = new int[3] { -1, 0, 1 };

            int[][] ys = new int[3][];
            ys[0] = new int[3] { 1, 2, 1 };
            ys[1] = new int[3] { 0, 0, 0 };
            ys[2] = new int[3] { -1, -2, -1 };

            Bitmap result = new Bitmap(image);
            for (int x = 1; x < image.Width - 2; x++)
            {
                for (int y = 1; y < image.Height - 2; y++)
                {
                    int pixelColorXS = (xs[0][0] * grayImage.GetPixel(x - 1, y - 1).R) + (xs[0][1] * grayImage.GetPixel(x, y - 1).R) + (xs[0][2] * grayImage.GetPixel(x + 1, y - 1).R) +
                                       (xs[1][0] * grayImage.GetPixel(x - 1, y).R) + (xs[1][1] * grayImage.GetPixel(x, y).R) + (xs[1][2] * grayImage.GetPixel(x + 1, y).R) +
                                       (xs[2][0] * grayImage.GetPixel(x - 1, y + 1).R) + (xs[2][1] * grayImage.GetPixel(x, y + 1).R) + (xs[2][2] * grayImage.GetPixel(x + 1, y + 1).R);

                    int pixelColorYS = (ys[0][0] * grayImage.GetPixel(x - 1, y - 1).R) + (ys[0][1] * grayImage.GetPixel(x, y - 1).R) + (ys[0][2] * grayImage.GetPixel(x + 1, y - 1).R) +
                                    (ys[1][0] * grayImage.GetPixel(x - 1, y).R) + (ys[1][1] * grayImage.GetPixel(x, y).R) + (ys[1][2] * grayImage.GetPixel(x + 1, y).R) +
                                    (ys[2][0] * grayImage.GetPixel(x - 1, y + 1).R) + (ys[2][1] * grayImage.GetPixel(x, y + 1).R) + (ys[2][2] * grayImage.GetPixel(x + 1, y + 1).R);

                    int pixelVa1 = (int)Math.Sqrt((pixelColorXS * pixelColorXS));
                    int pixelVa2 = (int)Math.Sqrt((pixelColorYS * pixelColorYS));

                    if (pixelVa1 > 50)
                    {
                        result.SetPixel(x, y, Color.White);
                    }
                    else if (pixelVa2 > 50)
                    {
                        result.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        result.SetPixel(x, y, Color.Black);
                    }
                }
            }
            saveImage(result);
        }

        public void CrudeHighPass(Bitmap image, int threshold)
        {
            int width = image.Width;
            int height = image.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int nowPixel = (pixel.R + pixel.G + pixel.B) / 3;
                    if (nowPixel <= threshold)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                }
            }
            saveImage(image);
        }   
    }
}
