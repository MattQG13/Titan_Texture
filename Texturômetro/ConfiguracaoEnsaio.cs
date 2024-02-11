using System;
using System.Drawing;
using System.Windows.Forms;
using DadosDeEnsaio;

namespace Texturometer {
    public partial class ConfiguracaoEnsaio : Form {
        int i = 0;
        public DataTest DadosDeEnsaio { get; private set; }

        public ConfiguracaoEnsaio() {
            InitializeComponent();
            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;
        }
        private void ConfiguracaoEnsaio_Load(object sender,EventArgs e) {
            cbTipo.SelectedIndex = 0;
            cbTarget.SelectedIndex=0;
            cbTrigger.SelectedIndex=0;
            cbTara.SelectedIndex=0;
        }
        private void btnIniciar_Click(object sender,EventArgs e) {


            if(capturaDados()) {
                DialogResult=DialogResult.OK;
                this.Close();
            } else {
                MessageBox.Show("Por favor, preencha todos os dados","Erro de preenchimento",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }


            
        }

        private bool capturaDados() {



            return false;
        }

        private void txb_KeyPress(object sender,KeyPressEventArgs e) {
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')&&!((sender as TextBox).Text.Length>1)) {
                e.Handled=true;
            }
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar)&&(e.KeyChar!=',')) {
                e.Handled=true;
            }
            if(e.KeyChar==','&&((sender as TextBox).Text.Length==0||(sender as TextBox).SelectionStart==0)) {
                e.Handled=true;
            }
            if((e.KeyChar==',')&&((sender as TextBox).Text.IndexOf(',')>-1)) {
                e.Handled=true;
            }
        }

        private void cb_SelectedIndexChanged(object sender,EventArgs e) {

            switch(cbTarget.SelectedItem){
                case "Distância":
                    lbTarget.Text="Distância:..........................";
                    lbUnTarget.Text="mm"; 
                    break;
                case "Força":
                    lbTarget.Text="Força:...............................";
                    lbUnTarget.Text="g";
                    break;
                default:
                    break;
            }
            switch(cbTrigger.SelectedItem) {
                case "Distância":
                    lbTrigger.Text="Distância de detecção:......";
                    lbUnTrigger.Text="mm";
                    break;
                case "Auto (força)":
                    lbTrigger.Text="Força de detecção...........";
                    lbUnTrigger.Text="g";
                    break;
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender,EventArgs e) {
            this.Close();
        }
    }
}
