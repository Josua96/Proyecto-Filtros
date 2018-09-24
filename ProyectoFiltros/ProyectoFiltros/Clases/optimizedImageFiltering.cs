using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros.Clases
{
    class OptimizedImageFiltering
    {

        private string toPath;

        public OptimizedImageFiltering()
        {
            this.toPath = Directory.GetCurrentDirectory() + "\\outputimages\\" + "image";
        }

        private void saveImage(Bitmap image)
        {
            image.Save(toPath + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg");
        }

        public void sepiaFilter(Bitmap imageBitMap)
        {

            Bitmap image = new Bitmap(imageBitMap);

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                //cantidad de bytes que tiene un pixel
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;

                //la cantidad de bytes constituidos unicamente por la anchura de la imagen, y el espacio en bytes que ocupa cada pixel
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0; //obtener el primer byte (la primera línea) en el mapa de bits



                Parallel.For(0, heightInPixels, y =>
                {

                    // obtener la posición del primer byte que corresponde al valor de y = int
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {

                        float oldRed = 0.393f * currentLine[x + 2] + 0.769f * currentLine[x + 1] + 0.189f * currentLine[x];
                        float oldGreen = 0.349f * currentLine[x + 2] + 0.686f * currentLine[x + 1] + 0.168f * currentLine[x];
                        float oldBlue = 0.272f * currentLine[x + 2] + 0.534f * currentLine[x + 1] + 0.131f * currentLine[x];

                        if (oldRed > 255)
                        {
                            oldRed = 255;
                        }

                        if (oldGreen > 255)
                        {
                            oldGreen = 255;
                        }
                        if (oldBlue > 255)
                        {
                            oldBlue = 255;
                        }

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;

                    }
                });


                image.UnlockBits(bitmapData);

                saveImage(image);

            }

            return;

        }

        public void grayScaleFilter(Bitmap imageBitMap)
        {

            Bitmap image = new Bitmap(imageBitMap);

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                //cantidad de bytes que tiene un pixel
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;

                //la cantidad de bytes constituidos unicamente por la anchura de la imagen, y el espacio en bytes que ocupa cada pixel
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0; //obtener el primer byte (la primera línea) en el mapa de bits


                //procesa una línea de pixeles (fila de matriz por cada iteración de parallell)
                Parallel.For(0, heightInPixels, y =>
                {

                    // obtener la posición del primer byte que corresponde al valor de y = int
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {

                        currentLine[x] = (byte)((currentLine[x] + currentLine[x + 1] + currentLine[x + 2]) / 3f);
                        currentLine[x + 1] = currentLine[x + 2] = currentLine[x];
                    }
                });


                image.UnlockBits(bitmapData);

                saveImage(image);

            }

            return;

        }

        public void opacityFilter(Bitmap bmp, double opacity)
        {
            const int bytesPerPixel = 4;

            PixelFormat pxf = PixelFormat.Format32bppArgb;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            IntPtr ptr = bmpData.Scan0;

            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

            Parallel.For(argbValues.Length, 0, counter =>
            {
                if (argbValues[counter + bytesPerPixel - 1] != 0)
                {
                    int pos = 0;
                    pos++; // B value
                    pos++; // G value
                    pos++; // R value

                    argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);


            bmp.UnlockBits(bmpData);
            saveImage(bmp);
        }

        public void invertColorsFilter(Bitmap image)
        {
            Parallel.For((image.Height - 1), 0, y => {
                Parallel.For((image.Width - 1), 0, x => {
                    Color inv = image.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    image.SetPixel(x, y, inv);
                });
            });

            saveImage(image);
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
            Parallel.For(rgbValues.Length, 0, counter =>
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
            });

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            image.UnlockBits(bmpData);
            saveImage(image);
        }

        public void colorsBalance(Bitmap imageBitMap)
        {
            Bitmap image = new Bitmap(imageBitMap);

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                //cantidad de bytes que tiene un pixel
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;

                //la cantidad de bytes constituidos unicamente por la anchura de la imagen, y el espacio en bytes que ocupa cada pixel
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0; //obtener el primer byte (la primera línea) en el mapa de bits



                Parallel.For(0, heightInPixels, y =>
                {

                    // obtener la posición del primer byte que corresponde al valor de y = int
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {

                        float blue = 255.0f / 147f * currentLine[x];
                        float green = 255.0f / 150f * currentLine[x + 1];
                        float red = 255.0f / 127f * currentLine[x + 2];

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

                        currentLine[x] = (byte)blue;
                        currentLine[x + 1] = (byte)green;
                        currentLine[x + 2] = (byte)red;

                    }

                });


                image.UnlockBits(bitmapData);

                saveImage(image);

            }

            return;
        }

        public void colorSubstitution(Bitmap imageBitmap, ColorSubstitutionFilter changer)
        {
            Bitmap image = new Bitmap(imageBitmap);

            unsafe
            {

                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                //cantidad de bytes que tiene un pixel
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;

                //la cantidad de bytes constituidos unicamente por la anchura de la imagen, y el espacio en bytes que ocupa cada pixel
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0; //obtener el primer byte (la primera línea) en el mapa de bits

                //almacenamiento de variables temporales

                byte minValue = 0;
                byte maxValue = 255;

                Color sourceColor = changer.getSourceColor();

                Color newColor = changer.getNewColor();

                int colorThreshold = changer.getThreshold();


                Parallel.For(0, heightInPixels, y =>
                {
                    byte sourceRed, sourceGreen, sourceBlue, sourceAlpha;
                    int resultRed = 0, resultGreen = 0, resultBlue = 0;

                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        sourceAlpha = currentLine[x + 3];

                        if (sourceAlpha != 0)
                        {
                            //obtener valores del pixel
                            sourceBlue = currentLine[x];
                            sourceGreen = currentLine[x + 1];
                            sourceRed = currentLine[x + 2];


                            if ((sourceBlue < sourceColor.B + colorThreshold &&
                                    sourceBlue > sourceColor.B - colorThreshold) &&


                                (sourceGreen < sourceColor.G + colorThreshold &&
                                    sourceGreen > sourceColor.G - colorThreshold) &&


                                (sourceRed < sourceColor.R + colorThreshold &&
                                    sourceRed > sourceColor.R - colorThreshold))
                            {
                                resultBlue = sourceColor.B - sourceBlue + newColor.B;


                                if (resultBlue > maxValue)
                                {
                                    resultBlue = maxValue;
                                }
                                else if (resultBlue < minValue)
                                {
                                    resultBlue = minValue;
                                }


                                resultGreen = sourceColor.G - sourceGreen + newColor.G;


                                if (resultGreen > maxValue)
                                {
                                    resultGreen = maxValue;
                                }
                                else if (resultGreen < minValue)
                                {
                                    resultGreen = minValue;
                                }


                                resultRed = sourceColor.R - sourceRed + newColor.R;


                                if (resultRed > maxValue)
                                {
                                    resultRed = maxValue;
                                }
                                else if (resultRed < minValue)
                                {
                                    resultRed = minValue;
                                }


                                currentLine[x] = (byte)resultBlue;
                                currentLine[x + 1] = (byte)resultGreen;
                                currentLine[x + 2] = (byte)resultRed;
                                currentLine[x + 3] = sourceAlpha;
                            }
                        }


                    }

                });

                image.UnlockBits(bitmapData);

                saveImage(image);
            }

            return;
        }

        public void solariseFilter(Bitmap sourceBitmap, byte blueValue, byte greenValue, byte redValue)
        {
            Bitmap image = new Bitmap(sourceBitmap);

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);


                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;


                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;


                byte byte255 = 255;
                Parallel.For(0, heightInPixels, y =>
                {


                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {

                        if (currentLine[x] < blueValue)
                        {
                            currentLine[x] = (byte)(byte255 - currentLine[x]);
                        }


                        if (currentLine[x + 1] < greenValue)
                        {
                            currentLine[x + 1] = (byte)(byte255 - currentLine[x + 1]);
                        }


                        if (currentLine[x + 2] < redValue)
                        {
                            currentLine[x + 2] = (byte)(byte255 - currentLine[x + 2]);
                        }

                    }

                });


                image.UnlockBits(bitmapData);

                saveImage(image);

            }

            return;
        }

        //----------------------------------------------------------------------------------------------------------------------
        public Bitmap GrayScale(Bitmap imageBitMap)
        {
            Bitmap image = new Bitmap(imageBitMap);

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                //cantidad de bytes que tiene un pixel
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;

                int heightInPixels = bitmapData.Height;

                //la cantidad de bytes constituidos unicamente por la anchura de la imagen, y el espacio en bytes que ocupa cada pixel
                int widthInBytes = bitmapData.Width * bytesPerPixel;

                byte* PtrFirstPixel = (byte*)bitmapData.Scan0; //obtener el primer byte (la primera línea) en el mapa de bits



                Parallel.For(0, heightInPixels, y =>
                {

                    // obtener la posición del primer byte que corresponde al valor de y = int
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {

                        currentLine[x] = (byte)((currentLine[x] + currentLine[x + 1] + currentLine[x + 2]) / 3f);
                        currentLine[x + 1] = currentLine[x + 2] = currentLine[x];

                    }
                });
                image.UnlockBits(bitmapData);
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
            Parallel.For((image.Height - 1), 0, y => {
                Parallel.For((image.Width - 1), 0, x => {
                    Color pixel = image.GetPixel(x, y);
                    int nowPixel = (pixel.R + pixel.G + pixel.B) / 3;
                    if (nowPixel <= threshold)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                });
            });
            saveImage(image);
        }

    }
}
