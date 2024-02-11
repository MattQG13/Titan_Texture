namespace Texturometer {
    partial class ConfiguracaoPrograma {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbCOM = new System.Windows.Forms.ComboBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDistancia = new System.Windows.Forms.ComboBox();
            this.cbForca = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTempo = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCOM
            // 
            this.cbCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCOM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCOM.FormattingEnabled = true;
            this.cbCOM.Location = new System.Drawing.Point(6, 35);
            this.cbCOM.Name = "cbCOM";
            this.cbCOM.Size = new System.Drawing.Size(188, 21);
            this.cbCOM.TabIndex = 0;
            this.cbCOM.SelectedIndexChanged += new System.EventHandler(this.ChangedConfig);
            // 
            // cbBaud
            // 
            this.cbBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaud.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000 "});
            this.cbBaud.Location = new System.Drawing.Point(6, 75);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(188, 21);
            this.cbBaud.TabIndex = 1;
            this.cbBaud.SelectedIndexChanged += new System.EventHandler(this.ChangedConfig);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbBaud);
            this.groupBox1.Controls.Add(this.cbCOM);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSalvar.Location = new System.Drawing.Point(357, 275);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(72, 23);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("SansSerif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCancelar.Location = new System.Drawing.Point(12, 275);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTempo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbDistancia);
            this.groupBox2.Controls.Add(this.cbForca);
            this.groupBox2.Location = new System.Drawing.Point(229, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 143);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unidades de Medida";
            // 
            // cbDistancia
            // 
            this.cbDistancia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDistancia.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbDistancia.FormattingEnabled = true;
            this.cbDistancia.Items.AddRange(new object[] {
            "mm"});
            this.cbDistancia.Location = new System.Drawing.Point(6, 75);
            this.cbDistancia.Name = "cbDistancia";
            this.cbDistancia.Size = new System.Drawing.Size(188, 21);
            this.cbDistancia.TabIndex = 1;
            this.cbDistancia.SelectedIndexChanged += new System.EventHandler(this.ChangedConfig);
            // 
            // cbForca
            // 
            this.cbForca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForca.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbForca.FormattingEnabled = true;
            this.cbForca.Items.AddRange(new object[] {
            "g"});
            this.cbForca.Location = new System.Drawing.Point(6, 35);
            this.cbForca.Name = "cbForca";
            this.cbForca.Size = new System.Drawing.Size(188, 21);
            this.cbForca.TabIndex = 0;
            this.cbForca.SelectedIndexChanged += new System.EventHandler(this.ChangedConfig);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SansSerif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Porta COM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SansSerif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.Location = new System.Drawing.Point(6, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Baudrate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SansSerif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Força";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SansSerif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Distância";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SansSerif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label5.Location = new System.Drawing.Point(6, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "Tempo";
            // 
            // cbTempo
            // 
            this.cbTempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTempo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbTempo.FormattingEnabled = true;
            this.cbTempo.Items.AddRange(new object[] {
            "s"});
            this.cbTempo.Location = new System.Drawing.Point(8, 114);
            this.cbTempo.Name = "cbTempo";
            this.cbTempo.Size = new System.Drawing.Size(188, 21);
            this.cbTempo.TabIndex = 17;
            this.cbTempo.SelectedIndexChanged += new System.EventHandler(this.ChangedConfig);
            // 
            // ConfiguracaoPrograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 310);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracaoPrograma";
            this.Text = "Configurações";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfiguracaoPrograma_FormClosing);
            this.Load += new System.EventHandler(this.ConfiguracaoPrograma_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.ComboBox cbCOM;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbDistancia;
        private System.Windows.Forms.ComboBox cbForca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTempo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}