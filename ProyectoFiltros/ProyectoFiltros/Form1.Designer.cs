namespace ProyectoFiltros
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.availableFilters = new System.Windows.Forms.ComboBox();
            this.applyFilter = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.searchImage = new System.Windows.Forms.Button();
            this.imageContainer = new System.Windows.Forms.PictureBox();
            this.previousImage = new System.Windows.Forms.Button();
            this.nextImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // availableFilters
            // 
            this.availableFilters.FormattingEnabled = true;
            this.availableFilters.Location = new System.Drawing.Point(561, 179);
            this.availableFilters.Name = "availableFilters";
            this.availableFilters.Size = new System.Drawing.Size(130, 21);
            this.availableFilters.TabIndex = 0;
            // 
            // applyFilter
            // 
            this.applyFilter.Location = new System.Drawing.Point(561, 230);
            this.applyFilter.Name = "applyFilter";
            this.applyFilter.Size = new System.Drawing.Size(130, 45);
            this.applyFilter.TabIndex = 1;
            this.applyFilter.Text = "Aplicar filtro";
            this.applyFilter.UseVisualStyleBackColor = true;
            this.applyFilter.Click += new System.EventHandler(this.applyFilter_Click);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(338, 19);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(402, 29);
            this.title.TabIndex = 2;
            this.title.Text = "Aplicación de filtros sobre imágenes";
            // 
            // searchImage
            // 
            this.searchImage.Location = new System.Drawing.Point(196, 391);
            this.searchImage.Name = "searchImage";
            this.searchImage.Size = new System.Drawing.Size(130, 45);
            this.searchImage.TabIndex = 3;
            this.searchImage.Text = "Buscar imágenes";
            this.searchImage.UseVisualStyleBackColor = true;
            this.searchImage.Click += new System.EventHandler(this.searchImage_Click);
            // 
            // imageContainer
            // 
            this.imageContainer.Location = new System.Drawing.Point(61, 108);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(401, 258);
            this.imageContainer.TabIndex = 4;
            this.imageContainer.TabStop = false;
            // 
            // previousImage
            // 
            this.previousImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousImage.Location = new System.Drawing.Point(12, 192);
            this.previousImage.Name = "previousImage";
            this.previousImage.Size = new System.Drawing.Size(43, 58);
            this.previousImage.TabIndex = 5;
            this.previousImage.Text = "<";
            this.previousImage.UseVisualStyleBackColor = true;
            this.previousImage.Click += new System.EventHandler(this.previousImage_Click);
            // 
            // nextImage
            // 
            this.nextImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextImage.Location = new System.Drawing.Point(468, 192);
            this.nextImage.Name = "nextImage";
            this.nextImage.Size = new System.Drawing.Size(43, 58);
            this.nextImage.TabIndex = 6;
            this.nextImage.Text = ">";
            this.nextImage.UseVisualStyleBackColor = true;
            this.nextImage.Click += new System.EventHandler(this.nextImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(535, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tipos de filtros";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1009, 548);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextImage);
            this.Controls.Add(this.previousImage);
            this.Controls.Add(this.imageContainer);
            this.Controls.Add(this.searchImage);
            this.Controls.Add(this.title);
            this.Controls.Add(this.applyFilter);
            this.Controls.Add(this.availableFilters);
            this.Name = "Form1";
            this.Text = "mainView";
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox availableFilters;
        private System.Windows.Forms.Button applyFilter;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button searchImage;
        private System.Windows.Forms.PictureBox imageContainer;
        private System.Windows.Forms.Button previousImage;
        private System.Windows.Forms.Button nextImage;
        private System.Windows.Forms.Label label1;
    }
}

