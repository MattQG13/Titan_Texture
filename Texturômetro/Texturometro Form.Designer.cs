namespace Classes {
    partial class TexturometroForms {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 4D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 5D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 1D);
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.arquivoStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exibirStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.resultadosStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TAStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rodarTesteStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Graph
            // 
            chartArea2.Name = "ChartArea1";
            this.Graph.ChartAreas.Add(chartArea2);
            this.Graph.Location = new System.Drawing.Point(12, 47);
            this.Graph.Name = "Graph";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.EmptyPointStyle.IsVisibleInLegend = false;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Name = "Series1";
            series2.Points.Add(dataPoint7);
            series2.Points.Add(dataPoint8);
            series2.Points.Add(dataPoint9);
            series2.Points.Add(dataPoint10);
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.SmartLabelStyle.Enabled = false;
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Graph.Series.Add(series2);
            this.Graph.Size = new System.Drawing.Size(388, 374);
            this.Graph.TabIndex = 0;
            this.Graph.Text = "Graph";
            this.Graph.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Graph);
            this.panel1.Controls.Add(this.menuStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoStripMenu,
            this.exibirStripMenu,
            this.resultadosStripMenu,
            this.TAStripMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // arquivoStripMenu
            // 
            this.arquivoStripMenu.Name = "arquivoStripMenu";
            this.arquivoStripMenu.Size = new System.Drawing.Size(61, 20);
            this.arquivoStripMenu.Text = "Arquivo";
            // 
            // exibirStripMenu
            // 
            this.exibirStripMenu.Name = "exibirStripMenu";
            this.exibirStripMenu.Size = new System.Drawing.Size(48, 20);
            this.exibirStripMenu.Text = "Exibir";
            // 
            // resultadosStripMenu
            // 
            this.resultadosStripMenu.Name = "resultadosStripMenu";
            this.resultadosStripMenu.Size = new System.Drawing.Size(76, 20);
            this.resultadosStripMenu.Text = "Resultados";
            // 
            // TAStripMenu
            // 
            this.TAStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rodarTesteStripMenu});
            this.TAStripMenu.Name = "TAStripMenu";
            this.TAStripMenu.Size = new System.Drawing.Size(39, 20);
            this.TAStripMenu.Text = "T.A.";
            // 
            // rodarTesteStripMenu
            // 
            this.rodarTesteStripMenu.Name = "rodarTesteStripMenu";
            this.rodarTesteStripMenu.Size = new System.Drawing.Size(142, 22);
            this.rodarTesteStripMenu.Text = "Rodar teste...";
            this.rodarTesteStripMenu.Click += new System.EventHandler(this.rodarTesteToolStripMenuItem_Click);
            // 
            // TexturometroForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TexturometroForms";
            this.Text = "Texturômetro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Texturometro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem arquivoStripMenu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem exibirStripMenu;
        private System.Windows.Forms.ToolStripMenuItem resultadosStripMenu;
        private System.Windows.Forms.ToolStripMenuItem TAStripMenu;
        private System.Windows.Forms.ToolStripMenuItem rodarTesteStripMenu;
    }
}

