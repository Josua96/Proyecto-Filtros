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
            this.filterAmount = new System.Windows.Forms.Label();
            this.filterPercentage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.traditionalMethod = new System.Windows.Forms.Label();
            this.optimizedMethod = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.simpleModeB = new System.Windows.Forms.RadioButton();
            this.distributedModeB = new System.Windows.Forms.RadioButton();
            this.infoConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // availableFilters
            // 
            this.availableFilters.FormattingEnabled = true;
            this.availableFilters.Location = new System.Drawing.Point(561, 179);
            this.availableFilters.Name = "availableFilters";
            this.availableFilters.Size = new System.Drawing.Size(130, 21);
            this.availableFilters.TabIndex = 0;
            this.availableFilters.SelectionChangeCommitted += new System.EventHandler(this.availableFilters_SelectionChangeCommitted);
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
            this.imageContainer.AccessibleDescription = "";
            this.imageContainer.AccessibleName = "";
            this.imageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // filterAmount
            // 
            this.filterAmount.AutoSize = true;
            this.filterAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterAmount.Location = new System.Drawing.Point(747, 124);
            this.filterAmount.Name = "filterAmount";
            this.filterAmount.Size = new System.Drawing.Size(0, 29);
            this.filterAmount.TabIndex = 8;
            // 
            // filterPercentage
            // 
            this.filterPercentage.Location = new System.Drawing.Point(752, 180);
            this.filterPercentage.Name = "filterPercentage";
            this.filterPercentage.Size = new System.Drawing.Size(98, 20);
            this.filterPercentage.TabIndex = 9;
            this.filterPercentage.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(645, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "Duración de procesos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(535, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "Método tradicional";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(805, 395);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 29);
            this.label4.TabIndex = 15;
            this.label4.Text = "Método optimizado";
            // 
            // traditionalMethod
            // 
            this.traditionalMethod.AutoSize = true;
            this.traditionalMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traditionalMethod.Location = new System.Drawing.Point(556, 442);
            this.traditionalMethod.Name = "traditionalMethod";
            this.traditionalMethod.Size = new System.Drawing.Size(0, 29);
            this.traditionalMethod.TabIndex = 16;
            // 
            // optimizedMethod
            // 
            this.optimizedMethod.AutoSize = true;
            this.optimizedMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optimizedMethod.Location = new System.Drawing.Point(840, 442);
            this.optimizedMethod.Name = "optimizedMethod";
            this.optimizedMethod.Size = new System.Drawing.Size(0, 29);
            this.optimizedMethod.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.distributedModeB);
            this.groupBox2.Controls.Add(this.simpleModeB);
            this.groupBox2.Location = new System.Drawing.Point(372, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 30);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // simpleModeB
            // 
            this.simpleModeB.AutoSize = true;
            this.simpleModeB.Location = new System.Drawing.Point(29, 7);
            this.simpleModeB.Name = "simpleModeB";
            this.simpleModeB.Size = new System.Drawing.Size(84, 17);
            this.simpleModeB.TabIndex = 0;
            this.simpleModeB.TabStop = true;
            this.simpleModeB.Text = "Modo simple";
            this.simpleModeB.UseVisualStyleBackColor = true;
            // 
            // distributedModeB
            // 
            this.distributedModeB.AutoSize = true;
            this.distributedModeB.Location = new System.Drawing.Point(202, 9);
            this.distributedModeB.Name = "distributedModeB";
            this.distributedModeB.Size = new System.Drawing.Size(102, 17);
            this.distributedModeB.TabIndex = 1;
            this.distributedModeB.TabStop = true;
            this.distributedModeB.Text = "Modo distribuido";
            this.distributedModeB.UseVisualStyleBackColor = true;
            // 
            // infoConfig
            // 
            this.infoConfig.Location = new System.Drawing.Point(915, 3);
            this.infoConfig.Name = "infoConfig";
            this.infoConfig.Size = new System.Drawing.Size(119, 32);
            this.infoConfig.TabIndex = 19;
            this.infoConfig.Text = "Información";
            this.infoConfig.UseVisualStyleBackColor = true;
            this.infoConfig.Click += new System.EventHandler(this.infoConfig_Click);
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1036, 495);
            this.Controls.Add(this.infoConfig);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.optimizedMethod);
            this.Controls.Add(this.traditionalMethod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterPercentage);
            this.Controls.Add(this.filterAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextImage);
            this.Controls.Add(this.previousImage);
            this.Controls.Add(this.imageContainer);
            this.Controls.Add(this.searchImage);
            this.Controls.Add(this.title);
            this.Controls.Add(this.applyFilter);
            this.Controls.Add(this.availableFilters);
            this.Name = "Form1";
            this.Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Label filterAmount;
        private System.Windows.Forms.TextBox filterPercentage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label traditionalMethod;
        private System.Windows.Forms.Label optimizedMethod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton distributedModeB;
        private System.Windows.Forms.RadioButton simpleModeB;
        private System.Windows.Forms.Button infoConfig;
    }
}

