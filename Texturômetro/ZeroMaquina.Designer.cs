namespace Texturometer {
    partial class ZeroMaquina {
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.btnCancel1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbFinalPosition = new System.Windows.Forms.TextBox();
            this.txbVelociadeZero = new System.Windows.Forms.TextBox();
            this.txbCargaLimite = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tab1);
            this.tabs.Controls.Add(this.tab2);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(310, 289);
            this.tabs.TabIndex = 7;
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.Color.Transparent;
            this.tab1.Controls.Add(this.btnCancel1);
            this.tab1.Controls.Add(this.btnNext);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(302, 263);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "tab1";
            // 
            // btnCancel1
            // 
            this.btnCancel1.Location = new System.Drawing.Point(8, 207);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(75, 23);
            this.btnCancel1.TabIndex = 1;
            this.btnCancel1.Text = "Cancelar";
            this.btnCancel1.UseVisualStyleBackColor = true;
            this.btnCancel1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(209, 207);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Próximo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.Color.Transparent;
            this.tab2.Controls.Add(this.label3);
            this.tab2.Controls.Add(this.label2);
            this.tab2.Controls.Add(this.label1);
            this.tab2.Controls.Add(this.txbFinalPosition);
            this.tab2.Controls.Add(this.txbVelociadeZero);
            this.tab2.Controls.Add(this.txbCargaLimite);
            this.tab2.Controls.Add(this.btnCancel2);
            this.tab2.Controls.Add(this.btnOk);
            this.tab2.Controls.Add(this.button4);
            this.tab2.Controls.Add(this.button5);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(302, 263);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "tab2";
            // 
            // btnCancel2
            // 
            this.btnCancel2.Location = new System.Drawing.Point(8, 207);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(75, 23);
            this.btnCancel2.TabIndex = 5;
            this.btnCancel2.Text = "Cancelar";
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(209, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 369);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Back";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(709, 371);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Next";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Posição final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Carga";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Vel";
            // 
            // txbFinalPosition
            // 
            this.txbFinalPosition.Location = new System.Drawing.Point(18, 104);
            this.txbFinalPosition.Name = "txbFinalPosition";
            this.txbFinalPosition.Size = new System.Drawing.Size(100, 20);
            this.txbFinalPosition.TabIndex = 10;
            // 
            // txbVelociadeZero
            // 
            this.txbVelociadeZero.Location = new System.Drawing.Point(18, 30);
            this.txbVelociadeZero.Name = "txbVelociadeZero";
            this.txbVelociadeZero.Size = new System.Drawing.Size(100, 20);
            this.txbVelociadeZero.TabIndex = 9;
            // 
            // txbCargaLimite
            // 
            this.txbCargaLimite.Location = new System.Drawing.Point(18, 66);
            this.txbCargaLimite.Name = "txbCargaLimite";
            this.txbCargaLimite.Size = new System.Drawing.Size(100, 20);
            this.txbCargaLimite.TabIndex = 8;
            // 
            // ZeroMaquina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 289);
            this.Controls.Add(this.tabs);
            this.Name = "ZeroMaquina";
            this.Text = "ZeroMaquina";
            this.tabs.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.Button btnCancel1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbFinalPosition;
        private System.Windows.Forms.TextBox txbVelociadeZero;
        private System.Windows.Forms.TextBox txbCargaLimite;
    }
}