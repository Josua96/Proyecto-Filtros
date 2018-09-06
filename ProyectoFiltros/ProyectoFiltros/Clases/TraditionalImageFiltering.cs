using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros
{
    class TraditionalImageFiltering
    {

        private pixelTransformer pixelManager;

        private string toPath;

        public TraditionalImageFiltering()
        {
            this.pixelManager = new pixelTransformer();
            this.toPath = Directory.GetCurrentDirectory() + "\\outputimages\\" + "image";
        }

        private void saveImage(Bitmap image)
        {
            image.Save(toPath + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg");
        }

        public void sepiaFilter(Bitmap image)
        {
            int width = image.Width;
            int heigth = image.Height;
            Color pixel;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, pixelManager.toSepia(pixel));
                }

            }

            saveImage(image);

        }

        public void grayScaleFilter(Bitmap image)
        {
            int width = image.Width;
            int heigth = image.Height;



            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, pixelManager.toGrayScale(pixel));
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
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    Int32 avgR = 0, avgG = 0, avgB = 0;
                    Int32 blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (Int32 x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (Int32 y = yy; (y < yy + blurSize && y < image.Height); y++)
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
                    for (Int32 x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (Int32 y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            this.saveImage(blurred);

        }
    }
}
