using System;
using System.Drawing;
using System.Windows.Forms;
using DadosDeEnsaio;

namespace Classes {
    public partial class ConfiguracaoEnsaio : Form {
        int i = 0;
        public DataTest DadosDeEnsaio { get; private set; }

        public ConfiguracaoEnsaio() {
            InitializeComponent();
            tabs.Appearance=TabAppearance.FlatButtons;
            tabs.ItemSize=new Size(0,1);
            tabs.SizeMode=TabSizeMode.Fixed;
        }

        private void button2_Click(object sender,EventArgs e) {

            tabs.TabPages[i].Hide();
            i++;
            if(i>tabs.TabCount-1)
                i=0;
            tabs.TabPages[i].Show();
        }

        private void button3_Click(object sender,EventArgs e) {
            tabs.TabPages[i].Hide();
            i--;
            if(i<0)
                i=tabs.TabCount-1;
            tabs.TabPages[i].Show();
        }

        private void btnOk_Click(object sender,EventArgs e) {

            DialogResult=DialogResult.OK;
             
            this.Close();
        }

    }
}
