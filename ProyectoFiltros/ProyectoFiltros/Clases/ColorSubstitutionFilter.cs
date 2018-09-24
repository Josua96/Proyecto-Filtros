using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros.Clases
{
    /// <summary>
    /// Clase para implementar el filtro de cambio de color, Esta clase sirve como almacenamiento temporal. 
    /// </summary>
    class ColorSubstitutionFilter
    {

        private int threshold = 40;

        private Color sourceColor;
        private Color newColor;

        private bool correctColorSelection;

        public ColorSubstitutionFilter()
        {
            this.correctColorSelection = false;

        }



        public int getThreshold()
        {
            return this.threshold;
        }

        public void setCorrectColorSelection(bool value)
        {
            this.correctColorSelection = value;
        }

        public bool getCorrectColorSelection()
        {
            return this.correctColorSelection;
        }

        public void SourceColor(Color color)
        {
            this.sourceColor = color;
        }

        public Color getSourceColor()
        {
            return this.sourceColor;
        }

        public void NewColor(Color color)
        {
            this.newColor = color;
        }

        public Color getNewColor()
        {
            return this.newColor;
        }

    }
}
