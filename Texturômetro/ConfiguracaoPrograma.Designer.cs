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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCOM
            // 
            this.cbCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCOM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCOM.FormattingEnabled = true;
            this.cbCOM.Location = new System.Drawing.Point(6, 28);
            this.cbCOM.Name = "cbCOM";
            this.cbCOM.Size = new System.Drawing.Size(188, 21);
            this.cbCOM.TabIndex = 0;
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
            this.cbBaud.Location = new System.Drawing.Point(6, 66);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(188, 21);
            this.cbBaud.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbBaud);
            this.groupBox1.Controls.Add(this.cbCOM);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão";
            // 
            // ConfiguracaoPrograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 310);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfiguracaoPrograma";
            this.Text = "Configurações";
            this.Load += new System.EventHandler(this.ConfiguracaoPrograma_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.ComboBox cbCOM;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}