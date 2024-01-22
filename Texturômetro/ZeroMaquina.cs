using ClassesSuporteTexturometro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexturometroClass;

namespace Texturometer {
    public partial class ZeroMaquina : Form {
        Texturometro tex;
       // private CultureInfo culture = new CultureInfo("en-US"); 


        public ZeroMaquina(Texturometro tex) {
            InitializeComponent();
            this.tex=tex;

            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;

            tex.Serial.ZeroSeated+=goToFinalPosition;
        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }

        private void btnNext_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
            tabs.SelectedIndex++;
        }

        private void btnOk_Click(object sender,EventArgs e) {
            tex.Motor.ZerarPosicao(Convert.ToDouble(txbVelociadeZero.Text.Trim()),Convert.ToDouble(txbCargaLimite.Text.Trim()));
        }

        private void goToFinalPosition(object sender,SerialMessageArgument e) {
            tex.Serial.EnvComandoMotor(ModoMotor.Subir,Convert.ToDouble(txbVelociadeZero.Text),Convert.ToDouble(txbFinalPosition.Text));
        }
    }
}
