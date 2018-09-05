using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros
{
    class pixelTransformer
    {
        public pixelTransformer()
        {

        }

        public Color toSepia (Color pixel)
        {
            int r, g, b;

            r = Convert.ToInt32(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
            g = Convert.ToInt32(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
            b = Convert.ToInt32(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);
            
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
            return Color.FromArgb(r, g, b);

        }

        public Color toGrayScale(Color pixel)
        {
            int average= Convert.ToInt32((pixel.R + pixel.G + pixel.B) / 3); 
            return Color.FromArgb(average, average, average);
        }
    }
}
