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

        /// <summary>
        /// Instancia de los filtros tradicionales
        /// </summary>
        private TraditionalImageFiltering traditionalFilter;
        /// <summary>
        /// Instancia de filtros obtimizados 
        /// </summary>
        private OptimizedImageFiltering optimizedFilter;
        /// <summary>
        /// Array de imagenes 
        /// </summary>
        private ArrayList images;
        /// <summary>
        /// Indice actual de la imagen
        /// </summary>
        private int imageIndex;
        /// <summary>
        /// Indice del filtro a utilizar
        /// </summary>
        private int filterIndex;    
        /// <summary>
        /// Filtro de tipo susticion de colores (Es especial)
        /// </summary>
        private ColorSubstitutionFilter colorSubstitution;
        /// <summary>
        /// Para ver el porcentaje de CPU utilizado durante la ejecucion del programa
        /// </summary>
        private PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        /// <summary>
        /// No doc
        /// </summary>
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        /// <summary>
        /// Es el manager de las conexiones a servidores remotos 
        /// </summary>
        private ConnectionManager ConnectionManager;
        /// <summary>
        /// Lista de direccione de los servidores 
        /// </summary>
        List<string> servers = new List<string>();


        /// <summary>
        /// Constructor principal del programa
        /// </summary>
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


        /// <summary>
        /// Permite agregar los distintos filtros a utilizar dentro del programa. 
        /// </summary>
        private void setFiltersType()
        {
                               
        }

        /// <summary>
        /// Permite agregar nombres imagenes para ser procesadas
        /// </summary>
        /// <param name="imageNames">Nombre de imagenes</param>
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

        /// <summary>
        /// Es el metodo del boton search Image permite abrir el explorador de archivos para buscar una imagen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Permite ver una imagen anterior si esta existe. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousImage_Click(object sender, EventArgs e)
        {
            if (imageIndex > 0)
            {
                imageIndex--;
                imageContainer.Image = (Image)images[imageIndex];
                imageContainer.Refresh();
            }
        }


        /// <summary>
        /// Permite cambiar a la siguiente imagen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextImage_Click(object sender, EventArgs e)
        {
            if (imageIndex < images.Count - 1)
            {
                imageIndex++;
                imageContainer.Image = (Image)images[imageIndex];
                imageContainer.Refresh();
            }
        }

        /// <summary>
        /// Permite actualizar los textos en pantalla 
        /// </summary>
        /// <param name="code">Numero de texto que se quiere actualizar</param>
        /// <param name="time">Texto para poner en pantalla </param>
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

        /// <summary>
        /// Permite dirigir el programa al filtro que el usuario quiere aplicar. 
        /// </summary>
        private void filterTypeImage()
        {
            MessageBox.Show("    Aplicando filtro    ", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Bitmap imagePixels = new Bitmap(imageContainer.Image);
            Stopwatch watch = new Stopwatch();
            bool local = simpleModeB.Checked;
            updateTime(0," ");
            updateTime(1," ");
            Image newImage; 

            if (local)
            {
                Console.WriteLine(filterIndex);
                switch (filterIndex)
                {
                    case 0:
                        watch.Start();
                        newImage = traditionalFilter.sepiaFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.sepiaFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 1:
                        watch.Start();
                        newImage = traditionalFilter.grayScaleFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.grayScaleFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 2:
                        try
                        {
                            watch.Start();
                            newImage = traditionalFilter.opacityFilter(imagePixels, Convert.ToDouble(filterPercentage.Text));
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            optimizedFilter.opacityFilter(imagePixels, Convert.ToDouble(filterPercentage.Text));
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                            this.imageContainer.Image = newImage;
                        }
                        catch
                        {
                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 3:
                        watch.Start();
                        newImage = traditionalFilter.invertColorsFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.invertColorsFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 4:
                        watch.Start();
                        newImage = traditionalFilter.GaussinBlurFilter(imagePixels, new Rectangle(0, 0, imagePixels.Width, imagePixels.Height), 4);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        GaussianBlur gaussian = new GaussianBlur(imagePixels);
                        gaussian.Process(3);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 5:
                        try
                        {
                            watch.Start();
                            newImage = traditionalFilter.brightFilter(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            optimizedFilter.brightFilter(imagePixels, Convert.ToDouble(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                            this.imageContainer.Image = newImage;
                        }
                        catch
                        {
                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; 
                        }
                        break;
                    case 6:
                        watch.Start();
                        newImage = traditionalFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.colorsBalance(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 7:
                        watch.Start();
                        newImage = traditionalFilter.colorSubstitution(imagePixels, colorSubstitution);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        //esperar un segundo
                        Thread.Sleep(1000);
                        optimizedFilter.colorSubstitution(imagePixels, colorSubstitution);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 8:
                        try
                        {
                            watch.Start();
                            newImage = traditionalFilter.CrudeHighPass(imagePixels, Convert.ToInt32(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                            watch.Start();
                            optimizedFilter.CrudeHighPass(imagePixels, Convert.ToInt32(filterPercentage.Text) / 100);
                            watch.Stop();
                            this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                            this.imageContainer.Image = newImage;
                        }
                        catch
                        {

                            MessageBox.Show("Verifica el dato de entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 9:
                        watch.Start();
                        newImage = traditionalFilter.EdgeFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.EdgeFilter(imagePixels);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 10:
                        watch.Start();
                        newImage = traditionalFilter.solariseFilter(imagePixels, 25, 40, 52);
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        watch.Start();
                        optimizedFilter.solariseFilter(imagePixels, 25, 40, 52);
                        watch.Stop();
                        this.updateTime(2, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
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
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 1:
                        watch.Start();
                        ConnectionManager.FilterName = "GrayScale";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
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
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 3:
                        watch.Start();
                        ConnectionManager.FilterName = "InvertColors";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 4:
                        watch.Start();
                        ConnectionManager.FilterName = "GaussianBlur";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
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
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 6:
                        watch.Start();
                        ConnectionManager.FilterName = "ColorsBalance";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        this.updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;

                    case 7:
                        Color sourceColor = colorSubstitution.getSourceColor();
                        Color newColor = colorSubstitution.getNewColor();
                        int colorThreshold = colorSubstitution.getThreshold();

                        watch.Start();
                        ConnectionManager.FilterName = "ColorSubstitution";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.Colors.Clear(); 
                        ConnectionManager.Colors.Add(sourceColor);
                        ConnectionManager.Colors.Add(newColor);                      
                        ConnectionManager.ParamValue = colorThreshold; 
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 8:
                        watch.Start();
                        ConnectionManager.FilterName = "SolariseFilter";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;
                    case 9:
                        watch.Start();
                        ConnectionManager.FilterName = "EdgeDetection";
                        ConnectionManager.Bitmap = imagePixels;
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;                        
                    case 10:
                        ConnectionManager.FilterName = "CrudeHighPass";
                        double darknessValue = 0;
                        Console.WriteLine(darknessValue);
                        try
                        {
                            darknessValue = double.Parse(filterPercentage.Text);
                            if (darknessValue <= 0 || darknessValue > 255)
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
                        ConnectionManager.ParamValue = darknessValue;
                        ConnectionManager.Bitmap = imagePixels;
                        watch.Start();
                        ConnectionManager.AddConnections(servers);
                        newImage = ConnectionManager.ApplyFilter();
                        watch.Stop();
                        updateTime(1, watch.Elapsed.TotalSeconds.ToString() + " s");
                        this.imageContainer.Image = newImage;
                        break;                              
                    default:
                        break;
                }
            }
        }

      

        /// <summary>
        /// Abre un dialog donde el usuario debe escojer un determinado color. 
        /// </summary>
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

        
        /// <summary>
        /// Inicia un hilo de ejecucion
        /// </summary>
        private void initFilterApplication()
        {

            Thread filterThread = new Thread(this.filterTypeImage);
            filterThread.Start();

        }

        /// <summary>
        /// Metodo para el boton "Aplicar filtro"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// Cierra la ventana 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Dispose();
            
        }

        private void availableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(availableFilters.SelectedIndex==2)
            {
                filterPercentage.Visible = true;
                filterPercentage.Clear();
            }
            else if (availableFilters.SelectedIndex == 5)
            {
                filterPercentage.Clear();
                filterPercentage.Visible = true;            
            }
            else if (availableFilters.SelectedIndex == 8)
            {
                filterPercentage.Clear();
                filterPercentage.Visible = true; 
            }
            else
            {
                filterPercentage.Clear();
                filterPercentage.Visible = false;
            }

        }
    }
}
