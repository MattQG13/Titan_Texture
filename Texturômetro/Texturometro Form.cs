using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TexturometroClass;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using System.Windows;
using Point = System.Drawing.Point;

namespace Texturometer {
    public partial class TexturometroForms : Form {

        static Texturometro tex;
        Series series = new Series("Pontos");
        Series series2 = new Series("XZ");

        static Timer tick = new Timer();
        static BackgroundWorker bkWork = new BackgroundWorker();
        CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();

        public TexturometroForms() {
            InitializeComponent();
            tick.Interval = 10;
            tick.Tick +=new EventHandler(atGraph);
            bkWork.DoWork+= bkWork_DoWork;
            bkWork.WorkerSupportsCancellation = true;
            typeof(Chart).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });
        }

 

        private void Texturometro_Load(object sender,EventArgs e) {
            DataTest dt = new DataTest();
            tex = new Texturometro(dt);
            tex.setSerial(Properties.Settings.Default.PortaCOM,Properties.Settings.Default.Baudrate);

            series.ChartType=SeriesChartType.Line;
            series2.ChartType=SeriesChartType.Line;
            series.BorderWidth = 2;

            Graph.Series.Clear();
            Graph.Series.Add(series);
            Graph.Series.Add(series2);
            Graph.Update();
            Graph.MouseMove+=Graph_MouseMove;
            Graph.ChartAreas[0].Position.X=0;
            Graph.ChartAreas[0].Position.Y = 0;
            Graph.ChartAreas[0].Position.Width=100;
            Graph.ChartAreas[0].Position.Height=100;
            tick.Start();

            tex.iniciaSerial();
            tex.Serial.Write("[LIMPAMEMORIA]!");
            tex.Serial.Write("[LIMPADENOVO]!");
            tex.Serial.Write("[LIMPADENOVO]!");
            tex.Serial.Write("[LIMPADENOVO]!");
            
            tex.LoadCell.ZeroTime();
            tex.Serial.LoadCellDetected+=atualizaLbLoad;
            tex.Serial.EncoderDetected+=atualizaLbPosition;

            tex.Serial.DiscardInBuffer();
            tex.Produto.Resultado.Clear();

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
                       series.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetXvalues());
                        series2.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetYvalues());

                    } catch(Exception ex) { }
                    
                    Graph.Invalidate();
                    Graph.ResumeLayout();;
                    tick.Start();
                }));
            }
        }

        private void btnUP_Click(object sender,EventArgs e) {
        }
        private void btnDN_Click(object sender,EventArgs e) {
            tex.Serial.EnvComandoMotor(ModoMotor.Subir,2,20);
        }

        private void TexturometroForms_FormClosing(object sender,FormClosingEventArgs e) {
            try {
                tex.Serial.LoadCellDetected=null;
                tex.Serial.EncoderDetected=null;
                tex.Serial.MotorDetected=null;
                tick.Stop();
                bkWork.CancelAsync();

                if(tex.Serial.IsOpen) {
                    tex.Serial.DiscardInBuffer();
                    tex.Serial.Close();
                }
            } finally { }
        }

        private void sobreToolStripMenuItem_Click(object sender,EventArgs e) {
            Sobre sb = new Sobre();
            sb.ShowDialog();

        }

        private void tararToolStripMenuItem_Click(object sender,EventArgs e) {
            tex.LoadCell.Tarar();
        }

        private void atualizaLbLoad(object sender,SerialMessageArgument e) {
            if(lbLoad.InvokeRequired) {
                lbLoad.BeginInvoke((MethodInvoker)delegate {
                    lbLoad.Text=e.doubleValue1.ToString()+" g";
                });
            } else {
                lbLoad.Text=e.doubleValue1.ToString()+" g";
            }
        }

        private void atualizaLbPosition(object sender,SerialMessageArgument e) {
            /*Task.Run(() => {
                this.Invoke(new Action(() => {
                    if(!cancelTokenSrc.Token.IsCancellationRequested) {
                        lbPosition.Text=e.doubleValue1.ToString()+" mm";
                    }
                }));
            },cancelTokenSrc.Token);*/

            if(lbPosition.InvokeRequired) {
                lbPosition.BeginInvoke((MethodInvoker)delegate {
                    lbPosition.Text=e.doubleValue1.ToString()+" mm";
                });
            } else {
                lbPosition.Text=e.doubleValue1.ToString()+" mm";
            }

        }

        private void configuraçõesToolStripMenuItem_Click(object sender,EventArgs e) {
            ConfiguracaoPrograma confP = new ConfiguracaoPrograma(this);
            confP.ShowDialog();
        }

        public void reconfigura() {
            try {
                tex.Serial.LoadCellDetected-=atualizaLbLoad;
                tex.Serial.EncoderDetected-=atualizaLbPosition;

                cancelTokenSrc.Cancel();
                tick.Stop();
                bkWork.CancelAsync();

                if(tex.Serial.IsOpen) {
                    tex.Serial.DiscardInBuffer();
                    tex.Serial.Close();
                }
            } finally { }

            tex.setSerial(Properties.Settings.Default.PortaCOM,Properties.Settings.Default.Baudrate);
            tex.Serial.LoadCellDetected+=atualizaLbLoad;
            tex.Serial.EncoderDetected+=atualizaLbPosition;

            cancelTokenSrc=new CancellationTokenSource();
            tex.iniciaSerial();
            tick.Start();

        }

        private void TexturometroForms_SizeChanged(object sender,EventArgs e) {
            lbXAxe.Location= new Point(Graph.Size.Width-lbXAxe.Size.Width-20,Graph.Size.Height-lbXAxe.Size.Height);
        }

        private void zeroMáquinaToolStripMenuItem_Click(object sender,EventArgs e) {
            ZeroMaquina zm = new ZeroMaquina(tex);
            zm.ShowDialog();
        }

        private void Graph_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {        

             if(Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X)>Graph.ChartAreas[0].AxisX.Minimum&&
                Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X)<Graph.ChartAreas[0].AxisX.Maximum&&
                Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y)>Graph.ChartAreas[0].AxisY.Minimum&&
                Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y)<Graph.ChartAreas[0].AxisY.Maximum) {
                
                double mouseX = Graph.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                double mouseY = Graph.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                lxy.Visible=true;

                Graph.ChartAreas[0].CursorX.Position=mouseX;
                Graph.ChartAreas[0].CursorY.Position=mouseY;

                lxy.Text=$"x:{mouseX:F1}  y:{mouseY:F1}";
                lxy.Location=new Point(e.X+5,e.Y-20);
            } else {
                lxy.Visible=false;
            }
        }
    }
}
