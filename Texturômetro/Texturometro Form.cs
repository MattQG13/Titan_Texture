using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TexturometroClass;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Drawing;
using Texturometer;
using System.Reflection;
using System.ComponentModel;


namespace Classes {
    public partial class TexturometroForms : Form {

        static Texturometro tex;
        Series series = new Series("Pontos");
        static Timer tick = new Timer();
        static BackgroundWorker bkWork = new BackgroundWorker();

        public TexturometroForms() {
            InitializeComponent();
            tick.Interval = 10;
            tick.Tick +=new EventHandler(atGraph);
            bkWork.DoWork+= bkWork_DoWork;
            typeof(Chart).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });
        }

 

        private void Texturometro_Load(object sender,EventArgs e) {
            DataTest dt = new DataTest();
            tex = new Texturometro(dt);
            tex.setSerial("COM6");

            series.ChartType=SeriesChartType.Spline;
            series.ChartType=SeriesChartType.Line;
            //series.Color = Color.Black;

            Graph.Series.Clear();
            Graph.Series.Add(series);
            Graph.Update();

            tex.iniciaSerial();
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

        private void rodarTesteToolStripMenuItem_Click(object sender,EventArgs e) {
            ConfiguracaoEnsaio FConfig = new ConfiguracaoEnsaio();

            var DialogResult = FConfig.ShowDialog();

            if(DialogResult == DialogResult.OK && FConfig.DadosDeEnsaio != null) {
                tex=new Texturometro(FConfig.DadosDeEnsaio);
            }
            FConfig.Dispose();

        }

        private void calibrarToolStripMenuItem_Click(object sender,EventArgs e) {
            Calibracao FCal = new Calibracao(tex);

            FCal.ShowDialog();
            tick.Start();

        }

        

        private void atGraph(object sender,EventArgs e) {
            if(!bkWork.IsBusy) {
                bkWork.RunWorkerAsync();
            }
        }

        private void bkWork_DoWork(object sender,DoWorkEventArgs e) {
            if(InvokeRequired) {
                this.Invoke(new Action(() => {
                    tick.Stop();
                    Graph.SuspendLayout();
                    try {
                        series.Points.DataBindXY(tex.Produto.Resultado.GetYvalues(),tex.Produto.Resultado.GetXvalues());
                    } catch(Exception ex) { }
                    
                    Graph.Invalidate();
                    Graph.ResumeLayout();;
                    tick.Start();
                }));
            }
        }

        private void btnUP_Click(object sender,EventArgs e) {
            tex.Serial.Write(textBox1.Text);
        }
        private void btnDN_Click(object sender,EventArgs e) {
            tex.Motor.SPVel=5;
            tex.Motor.Start(ModoMotor.Subir);
        }

        private void TexturometroForms_FormClosing(object sender,FormClosingEventArgs e) {
            if(tex.Serial.IsOpen) {
                tex.Serial.Close();
            }
        }
    }
}
