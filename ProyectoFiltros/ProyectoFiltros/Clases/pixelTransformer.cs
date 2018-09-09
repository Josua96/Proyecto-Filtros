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

        private float r, g, b;

        public pixelTransformer()
        {

        }

        public void toSepia ( byte[] pixelsData, ref int i)
        {
            
            r= 0.393f * pixelsData[i+2] + 0.769f * pixelsData[i+1] + 0.189f * pixelsData[i];
            g = 0.349f * pixelsData[i+2] + 0.686f * pixelsData[i + 1] + 0.168f * pixelsData[i];
            b = 0.272f * pixelsData[i+2] + 0.534f * pixelsData[i + 1] + 0.131f * pixelsData[i];

           
        
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

            pixelsData[i+2] = (byte) r;
            pixelsData[i+1] = (byte)g;
            pixelsData[i] = (byte)b;
            return;

        }

        public void toGrayScale(byte[] pixelsData, ref int i)
        {
            pixelsData [i] = (byte) ((pixelsData[i] + pixelsData[i+1] + pixelsData[i+2] ) / 3f);
            pixelsData[i + 1] = pixelsData[i + 2] = pixelsData[i];
            return;
        }
    }
}
