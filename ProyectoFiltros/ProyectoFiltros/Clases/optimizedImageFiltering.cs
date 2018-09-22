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


        public void GaussinBlurFilter(Bitmap image, Rectangle rectangle, int blurSize)
        {
            /**
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

    
            Parallel.For(rectangle.X, rectangle.X + rectangle.Width, xx =>
            {
                Parallel.For(rectangle.Y, rectangle.Y + rectangle.Height, yy =>
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
                            lock (blurred[xx][yy])
                            {

                            }
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));


                });

            });

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    

                    

                    
                }
            }



            this.saveImage(blurred);

            */
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

    }
}
