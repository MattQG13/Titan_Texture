using LoadCellTexturometro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexturometroClass;

namespace Texturometer {
    public partial class Calibracao : Form {

        Texturometro tex; 
        public Calibracao(Texturometro tex) {
            this.tex = tex;
            InitializeComponent();
            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;
        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }

        private void btnNext_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
            tabs.SelectedIndex++;
        }

        private void btnOk_Click(object sender,EventArgs e) {
            tex.LoadCell.Cal(Convert.ToDouble(txCal.Text));
            this.Close();
        }
    }
}
