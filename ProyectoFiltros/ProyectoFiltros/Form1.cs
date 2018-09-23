using ProyectoFiltros.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFiltros
{
    public partial class Form1 : Form
    {

        private TraditionalImageFiltering traditionalFilter;
        private OptimizedImageFiltering optimizedFilter;
        private ArrayList images;
        private int imageIndex;
        private int filterIndex;
        private ColorSubstitutionFilter colorSubstitution;
        private ConnectionManager ConnectionManager; 
        public Form1()
        {
            InitializeComponent();
            
            traditionalFilter = new TraditionalImageFiltering();
            this.ConnectionManager = new ConnectionManager();
            optimizedFilter = new OptimizedImageFiltering();

            colorSubstitution = new ColorSubstitutionFilter();

            images =new ArrayList();
            setFiltersType();
            availableFilters.SelectedIndex = 0;
            imageContainer.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void setFiltersType()
        {
            availableFilters.Items.Add("Sepia");
            availableFilters.Items.Add("Escala de grises");
            availableFilters.Items.Add("Opacidad");
            availableFilters.Items.Add("Invertir colores");
            availableFilters.Items.Add("Desenfoque Gaussiano");
            availableFilters.Items.Add("Ajuste de brillo");
            availableFilters.Items.Add("Compresión");
            availableFilters.Items.Add("Segmentación");
            availableFilters.Items.Add("Textura");
            availableFilters.Items.Add("Balance de colores");
            availableFilters.Items.Add("Remplazo de color");

        }

        private void setImages(string[] imageNames)
        {
            images.Clear();
            foreach (String image in imageNames)
            {
                images.Add(Image.FromFile(image));
            }
            imageContainer.Image = (Image)images[0];
            imageIndex = 0;

        }


        private void searchImage_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
            fileExplorer.Title = "Explorador de imágenes";
            fileExplorer.Multiselect = true; //permitir seleccionar varias imágenes

            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {

                this.setImages(fileExplorer.FileNames);

            }
        }

        private void previousImage_Click(object sender, EventArgs e)
        {
            if (imageIndex > 0)
            {
                imageIndex--;
                imageContainer.Image = (Image)images[imageIndex];
                imageContainer.Refresh();

            }
        }

        private void nextImage_Click(object sender, EventArgs e)
        {
            if (imageIndex < images.Count - 1)
            {
                imageIndex++;
                imageContainer.Image = (Image)images[imageIndex];
                imageContainer.Refresh();

            }
        }

        private void updateTime (int code,string time)
        {
            if (code==1)
            {
                this.BeginInvoke(new Action(() =>
                {

                    traditionalMethod.Text = time;

                }));
            }

            else
            {
                this.BeginInvoke(new Action(() =>
                {
                    
                    optimizedMethod.Text = time;

                }));
            }
        }

        private void filterTypeImage()
        {


            Bitmap imagePixels = new Bitmap(imageContainer.Image);

            Stopwatch watch = new Stopwatch();
            bool local = true; 
            if(local)
            {
                switch (filterIndex)
                {
                    case 0:
                        watch.Start();
                        traditionalFilter.sepiaFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.sepiaFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 1:
                        watch.Start();
                        traditionalFilter.grayScaleFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.grayScaleFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 2:
                        watch.Start();
                        try { traditionalFilter.opacityFilter(imagePixels, Convert.ToDouble(filterPercentage.Text)); }
                        catch { traditionalFilter.opacityFilter(imagePixels, 55); }
                        watch.Stop();
                        break;
                    case 3:
                        watch.Start();
                        traditionalFilter.invertColorsFilter(imagePixels);
                        watch.Stop();
                        break;
                    case 4:
                        watch.Start();
                        traditionalFilter.GaussinBlurFilter(imagePixels, new Rectangle(0, 0, imagePixels.Width, imagePixels.Height), 4);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        Thread.Sleep(1000);
                        //optimizedFilter.gaussianBlurFilter2(imagePixels, new Rectangle(0, 0, imagePixels.Width, imagePixels.Height), 4);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");

                        break;

                    case 5:
                        watch.Start();
                        try { traditionalFilter.brightFilter(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100); }
                        catch { traditionalFilter.brightFilter(imagePixels, 0.5); }
                        watch.Stop();
                        break;

                    case 9:
                        watch.Start();
                        traditionalFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;

                    case 10:
                        watch.Start();
                        traditionalFilter.colorSubstitution(imagePixels, colorSubstitution);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        //esperar un segundo
                        Thread.Sleep(1000);
                        optimizedFilter.colorSubstitution(imagePixels, colorSubstitution);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;


                    default:
                        break;

                }
            }
            else{

                switch (filterIndex)
                {
                    case 0:
                        watch.Start();
                        ConnectionManager.FilterName = "Sepia";
                        ConnectionManager.Bitmap = imagePixels;
                        List<string> servers = new List<string>();
                        servers.Add("http://172.24.107.139:89/ImageProcessingWebService/ApplyGeneralFilter");
                        servers.Add("http://172.24.107.139:89/ImageProcessingWebService/ApplyGeneralFilter");
                        servers.Add("http://172.24.107.139:89/ImageProcessingWebService/ApplyGeneralFilter");
                        servers.Add("http://172.24.107.139:89/ImageProcessingWebService/ApplyGeneralFilter");

                        ConnectionManager.AddConnections(servers);  // recorreria una lista de conecciones (dadas por el usuario)
                        ConnectionManager.ApplyFilter(); 
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");                      
                        break;                  
                    default:
                        break;
                }
            }
           

            
 
        }


      
        private void colorSelection()
        {
            
            ColorDialog colorSelector = new ColorDialog();
           
            if (colorSelector.ShowDialog() == DialogResult.OK)
            {
                colorSubstitution.SourceColor(colorSelector.Color);
                colorSelector.Color = Color.White;

                if (colorSelector.ShowDialog() == DialogResult.OK)
                {
                    colorSubstitution.NewColor(colorSelector.Color);
                    colorSelector.Color = Color.White;
                    colorSubstitution.setCorrectColorSelection(true);

                }
                else
                {
                    colorSubstitution.setCorrectColorSelection(false);
                }

            }

            else
            {
                colorSubstitution.setCorrectColorSelection(false);
            }

            return; 

        }

        private void initFilterApplication()
        {

            Thread filterThread = new Thread(this.filterTypeImage);
            filterThread.Start();

        }

        private void applyFilter_Click(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                filterIndex = availableFilters.SelectedIndex;

                if (filterIndex== 10)
                {
                    this.colorSelection();
                    if (this.colorSubstitution.getCorrectColorSelection()==true)
                    {
                        initFilterApplication();
                    }
                }
                else
                {
                    initFilterApplication();
                }
                
               
            }
        }

        private void availableFilters_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterAmount.Text = "";
            filterAmount.Visible = false;
            filterPercentage.Text = "";
            filterPercentage.Visible = false;
            conPerdida.Visible = false;
            sinPerdida.Visible = false;

            if (availableFilters.SelectedIndex == 2)
            {
                filterAmount.Text = "Porcentaje de opacidad";
                filterAmount.Visible = true;
                filterPercentage.Visible = true;
            }
            else if (availableFilters.SelectedIndex == 5)
            {
                filterAmount.Text = "Porcentaje de brillo";
                filterAmount.Visible = true;
                filterPercentage.Visible = true;

            }
            else if (availableFilters.SelectedIndex == 6)
            {
                filterAmount.Text = "Pérdida";
                filterAmount.Visible = true;
                groupBox1.Visible = true;
                conPerdida.Visible = true;
                sinPerdida.Visible = true;
            }
        }

        private void availableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
