namespace Texturometer {
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 4D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 5D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 3D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 1D);
            this.pnBackground = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPosition = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLoad = new System.Windows.Forms.Label();
            this.btnDN = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbYAxe = new System.Windows.Forms.Label();
            this.lbXAxe = new System.Windows.Forms.Label();
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.arquivoStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exibirStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.resultadosStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TAStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rodarTesteStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tararToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnBackground.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBackground
            // 
            this.pnBackground.Controls.Add(this.tableLayoutPanel1);
            this.pnBackground.Controls.Add(this.menuStrip);
            this.pnBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBackground.Location = new System.Drawing.Point(0, 0);
            this.pnBackground.Name = "pnBackground";
            this.pnBackground.Size = new System.Drawing.Size(800, 450);
            this.pnBackground.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 426);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbPosition);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbLoad);
            this.panel2.Controls.Add(this.btnDN);
            this.panel2.Controls.Add(this.btnUP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 144);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label2.Location = new System.Drawing.Point(302, 19);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Posição";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPosition
            // 
            this.lbPosition.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPosition.Font = new System.Drawing.Font("SansSerif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbPosition.Location = new System.Drawing.Point(302, 37);
            this.lbPosition.Name = "lbPosition";
            this.lbPosition.Size = new System.Drawing.Size(120, 46);
            this.lbPosition.TabIndex = 5;
            this.lbPosition.Text = "–";
            this.lbPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.Location = new System.Drawing.Point(142, 17);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Carga";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLoad
            // 
            this.lbLoad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLoad.Font = new System.Drawing.Font("SansSerif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbLoad.Location = new System.Drawing.Point(142, 35);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(120, 46);
            this.lbLoad.TabIndex = 0;
            this.lbLoad.Text = "–";
            this.lbLoad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDN
            // 
            this.btnDN.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnDN.Location = new System.Drawing.Point(9, 89);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(73, 46);
            this.btnDN.TabIndex = 3;
            this.btnDN.TabStop = false;
            this.btnDN.Text = "DESCER";
            this.btnDN.UseVisualStyleBackColor = true;
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
            // 
            // btnUP
            // 
            this.btnUP.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUP.Location = new System.Drawing.Point(9, 37);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(73, 46);
            this.btnUP.TabIndex = 2;
            this.btnUP.TabStop = false;
            this.btnUP.Text = "SUBIR";
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 270);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbYAxe);
            this.panel1.Controls.Add(this.lbXAxe);
            this.panel1.Controls.Add(this.Graph);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(203, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 264);
            this.panel1.TabIndex = 0;
            // 
            // lbYAxe
            // 
            this.lbYAxe.AutoSize = true;
            this.lbYAxe.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbYAxe.Location = new System.Drawing.Point(0, 0);
            this.lbYAxe.Name = "lbYAxe";
            this.lbYAxe.Size = new System.Drawing.Size(31, 15);
            this.lbYAxe.TabIndex = 2;
            this.lbYAxe.Text = "F(g)";
            this.lbYAxe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbXAxe
            // 
            this.lbXAxe.AutoSize = true;
            this.lbXAxe.Font = new System.Drawing.Font("SansSerif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbXAxe.Location = new System.Drawing.Point(548, 240);
            this.lbXAxe.Name = "lbXAxe";
            this.lbXAxe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbXAxe.Size = new System.Drawing.Size(26, 15);
            this.lbXAxe.TabIndex = 3;
            this.lbXAxe.Text = "t(s)";
            this.lbXAxe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Graph
            // 
            this.Graph.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.Maximum = 600D;
            chartArea1.AxisX.MaximumAutoSize = 90F;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Format = "0.0";
            chartArea1.AxisY.MaximumAutoSize = 90F;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.ScaleView.SmallScrollMinSize = 10D;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            chartArea1.Name = "ChartArea1";
            this.Graph.ChartAreas.Add(chartArea1);
            this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph.Location = new System.Drawing.Point(0, 0);
            this.Graph.Name = "Graph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.IsVisibleInLegend = false;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Graph.Series.Add(series1);
            this.Graph.Size = new System.Drawing.Size(588, 264);
            this.Graph.TabIndex = 5;
            this.Graph.Text = "Graph";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 264);
            this.panel3.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoStripMenu,
            this.exibirStripMenu,
            this.resultadosStripMenu,
            this.TAStripMenu,
            this.configuraçõesToolStripMenuItem,
            this.sobreToolStripMenuItem});
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
            this.rodarTesteStripMenu,
            this.calibrarToolStripMenuItem,
            this.tararToolStripMenuItem});
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
            // calibrarToolStripMenuItem
            // 
            this.calibrarToolStripMenuItem.Name = "calibrarToolStripMenuItem";
            this.calibrarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.calibrarToolStripMenuItem.Text = "Calibrar...";
            this.calibrarToolStripMenuItem.Click += new System.EventHandler(this.calibrarToolStripMenuItem_Click);
            // 
            // tararToolStripMenuItem
            // 
            this.tararToolStripMenuItem.Name = "tararToolStripMenuItem";
            this.tararToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.tararToolStripMenuItem.Text = "Tarar...";
            this.tararToolStripMenuItem.Click += new System.EventHandler(this.tararToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.configuraçõesToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // TexturometroForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnBackground);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TexturometroForms";
            this.Text = "Texturômetro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TexturometroForms_FormClosing);
            this.Load += new System.EventHandler(this.Texturometro_Load);
            this.SizeChanged += new System.EventHandler(this.TexturometroForms_SizeChanged);
            this.pnBackground.ResumeLayout(false);
            this.pnBackground.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnBackground;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem arquivoStripMenu;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.ToolStripMenuItem exibirStripMenu;
        private System.Windows.Forms.ToolStripMenuItem resultadosStripMenu;
        private System.Windows.Forms.ToolStripMenuItem TAStripMenu;
        private System.Windows.Forms.ToolStripMenuItem rodarTesteStripMenu;
        private System.Windows.Forms.Button btnDN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem calibrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tararToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.Label lbYAxe;
        private System.Windows.Forms.Label lbXAxe;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPosition;
        private System.Windows.Forms.Label label1;
    }
}

