using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TexturometroClass;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Drawing;

namespace Classes {
    public partial class TexturometroForms : Form {

        static Texturometro tex;

        public TexturometroForms() {
            InitializeComponent();
            
        }

 

        private void Texturometro_Load(object sender,EventArgs e) {
            Series series = new Series("Pontos");
            series.ChartType=SeriesChartType.Spline;
           
            Graph.Series.Clear();
            series.Points.AddXY(1,5);
            series.Points.AddXY(2,10);
            series.Points.AddXY(3,8);
            series.Points.AddXY(4,12);
            Graph.Series.Add(series);

            Tabela novaColecao = new Tabela();

            novaColecao.Add(5,6,9);
            novaColecao.Add(6,10,2);
            novaColecao.Add(7,6,5);
            novaColecao.Add(8,1,3);
            novaColecao.Add(9,3,9);
            series.Points.DataBindXY(novaColecao.GetXvalues(),novaColecao.GetYvalues()); 
            Graph.Update();
        }
       
        protected override bool ProcessCmdKey(ref Message msg,Keys keyData) {
            Key key = KeyInterop.KeyFromVirtualKey((int)keyData);
            KeyEventArgs e = new KeyEventArgs(keyData);

            if(key>0) {
                if(Keyboard.IsKeyDown(key)) {
                }
                if(Keyboard.IsKeyUp(key)) {
                }
            }

            
            return base.ProcessCmdKey(ref msg,keyData);
        }

        private void button1_Click(object sender,EventArgs e) {
            
        }

        private void rodarTesteToolStripMenuItem_Click(object sender,EventArgs e) {
            ConfiguracaoEnsaio FConfig = new ConfiguracaoEnsaio();

            var DialogResult = FConfig.ShowDialog();

            if(DialogResult == DialogResult.OK) {
                tex=new Texturometro(FConfig.DadosDeEnsaio);
            }
            FConfig.Dispose();

        }
    }
}
