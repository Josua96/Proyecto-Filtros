using ProyectoFiltros.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
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
        private PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private ConnectionManager ConnectionManager;
        List<string> servers = new List<string>();
        public Form1()
        {
            InitializeComponent();
            servers.Add("http://172.24.65.181:89/ImageProcessingWebService");
            servers.Add("http://172.24.65.181:89/ImageProcessingWebService");
            servers.Add("http://172.24.65.181:89/ImageProcessingWebService");
            servers.Add("http://172.24.65.181:89/ImageProcessingWebService");
            traditionalFilter = new TraditionalImageFiltering();
            this.ConnectionManager = new ConnectionManager();
            optimizedFilter = new OptimizedImageFiltering();

            colorSubstitution = new ColorSubstitutionFilter();

            images =new ArrayList();
            setFiltersType();
            availableFilters.SelectedIndex = 0;
            imageContainer.SizeMode = PictureBoxSizeMode.StretchImage;

            CPUusage.Text = CPUCounter.NextValue() + "%";
            t.Interval = 750;
            t.Enabled = true;
            timer1_Tick(null, null);

            t.Tick += new EventHandler(timer1_Tick);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CPUusage.Text = CPUCounter.NextValue() + "%";
        }

        private void setFiltersType()
        {
            availableFilters.Items.Add("Sepia");
            availableFilters.Items.Add("Escala de grises");
            availableFilters.Items.Add("Opacidad");
            availableFilters.Items.Add("Invertir colores");
            availableFilters.Items.Add("Desenfoque Gaussiano");
            availableFilters.Items.Add("Ajuste de brillo");
            availableFilters.Items.Add("Balance de colores");
            availableFilters.Items.Add("Remplazo de color");
            availableFilters.Items.Add("Oscurecer");
            availableFilters.Items.Add("Bordes");
            availableFilters.Items.Add("Solarizado");                      
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
            MessageBox.Show("Aplicando filtro", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Bitmap imagePixels = new Bitmap(imageContainer.Image);
            Stopwatch watch = new Stopwatch();
            bool local = simpleModeB.Checked;
            updateTime(0," ");
            updateTime(1," ");

            if (local)
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
                        try
                        {
                            watch.Start();
                            traditionalFilter.opacityFilter(imagePixels, Convert.ToDouble(filterPercentage.Text));
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            optimizedFilter.opacityFilter(imagePixels, Convert.ToDouble(filterPercentage.Text));
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        }
                        catch
                        {
                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 3:
                        watch.Start();
                        traditionalFilter.invertColorsFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.invertColorsFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 4:
                        watch.Start();
                        traditionalFilter.GaussinBlurFilter(imagePixels, new Rectangle(0, 0, imagePixels.Width, imagePixels.Height), 4);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        GaussianBlur gaussian = new GaussianBlur(imagePixels);
                        gaussian.Process(3);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 5:
                        try
                        {
                            watch.Start();
                            traditionalFilter.brightFilter(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            optimizedFilter.brightFilter(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        }
                        catch
                        {
                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; 
                        }
                        break;
                    case 6:
                        watch.Start();
                        traditionalFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 7:
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
                    case 8:
                        try
                        {
                            watch.Start();
                            traditionalFilter.CrudeHighPass(imagePixels, Convert.ToInt32(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            //optimizedFilter.CrudeHighPass(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        }
                        catch
                        {

                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 9:
                        watch.Start();
                        traditionalFilter.EdgeFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.EdgeFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 10:
                        watch.Start();
                        traditionalFilter.solariseFilter(imagePixels, 25, 40, 52);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.solariseFilter(imagePixels, 25, 40, 52);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;                  
                    default:
                        break;
                }
            }
            else
            {

                switch (filterIndex)
                {
                    case 0:
                        watch.Start();
                        ConnectionManager.FilterName = "Sepia";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 1:
                        watch.Start();
                        ConnectionManager.FilterName = "GrayScale";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 2: 
                        double opacityValue = 0;
                        try
                        {
                            opacityValue = double.Parse(filterPercentage.Text);
                            if (opacityValue <= 0 || opacityValue > 255)
                            {
                                MessageBox.Show("Error: el numero debe ser un entero de 0 a 255", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error: el numero debe ser un entero de 0 a 255", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        watch.Start();
                        ConnectionManager.ParamValue = opacityValue; 
                        ConnectionManager.FilterName = "Opacity";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 3:
                        watch.Start();
                        ConnectionManager.FilterName = "InvertColors";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 4:
                        watch.Start();
                        ConnectionManager.FilterName = "GaussianBlur";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 5: 
                        watch.Start();
                       
                        double BrightValue = 0;
                        try
                        {
                            BrightValue = double.Parse(filterPercentage.Text);
                            if (BrightValue < 0 || BrightValue > 255)
                            {
                                MessageBox.Show("Error: Debe ser un numero entero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; 
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error: el numero debe ser un entero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        ConnectionManager.ParamValue = BrightValue; 
                        ConnectionManager.FilterName = "Bright";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 6:
                        watch.Start();
                        ConnectionManager.FilterName = "ColorsBalance";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;

                    case 7:
                        Color sourceColor = colorSubstitution.getSourceColor();
                        Color newColor = colorSubstitution.getNewColor();
                        int colorThreshold = colorSubstitution.getThreshold();

                        watch.Start();
                        ConnectionManager.FilterName = "none";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.Colors.Clear(); 
                        ConnectionManager.Colors.Add(sourceColor);
                        ConnectionManager.Colors.Add(newColor);                      
                        ConnectionManager.ParamValue = colorThreshold; 
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;
                    case 8:
                     
                        ConnectionManager.FilterName = "CrudeHighPass";
                        double darknessValue=0;
                        Console.WriteLine(darknessValue); 
                        try
                        {
                            darknessValue = double.Parse(filterPercentage.Text);
                            if(darknessValue <= 0 || darknessValue > 255)
                            {
                                MessageBox.Show("Error: el numero debe ser un entero de 0 a 255", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; 
                            }
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Error: el numero debe ser un entero de 0 a 255", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; 
                        }
                        ConnectionManager.ParamValue = darknessValue; 
                        ConnectionManager.Bitmap = imagePixels;
                        watch.Start();
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");                        
                        break;
                    case 9:
                        watch.Start();
                        ConnectionManager.FilterName = "EdgeDetection";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        break;                        
                    case 10:
                        watch.Start();
                        ConnectionManager.FilterName = "SolariseFilter";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
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

                if (filterIndex== 7)
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
            else if (availableFilters.SelectedIndex == 8)
            {
                filterAmount.Text = "Seleccione un numero de 0 a 255";
                filterAmount.Visible = true;
                filterPercentage.Visible = true;

            }
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Dispose();
            
        }

        private void availableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
