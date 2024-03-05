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
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Linq;

namespace Texturometer {
    public partial class TexturometroForms : Form {

        static Texturometro tex = new Texturometro();
        Series series = new Series("Pontos");
        Series series2 = new Series("XZ");

        static Timer tick = new Timer();
        static BackgroundWorker bkWork = new BackgroundWorker();
        CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();

        public TexturometroForms() {
            InitializeComponent();
            tick.Interval = 10;
            tick.Tick +=new EventHandler(atGraph);
            tick.Enabled=true;
            bkWork.DoWork+= bkWork_DoWork;
            bkWork.WorkerSupportsCancellation = true;
            typeof(Chart).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });
        }

        public void VelAt(object sender,SerialMessageArgument e) {
                if(lbVel.InvokeRequired) {
                lbVel.BeginInvoke((MethodInvoker)delegate {
                    lbVel.Text=e.doubleValue.ToString()+" mm";
                });
            } else {
                lbVel.Text=e.doubleValue.ToString()+" mm";
            }
        }
 

        private void Texturometro_Load(object sender,EventArgs e) {

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

            tex.Serial.LoadCellDetected+=atualizaLbLoad;
            tex.Serial.EncoderDetected+=atualizaLbPosition;
            tex.Serial.LoadCalibrated+=LoadCelCalibrated;
            tex.LoadCell.ZeroTime();
            tex.Serial.EnvCalibration(Properties.Settings.Default.CalLoadCell);
            tex.Serial.DiscardInBuffer();
            tex.Produto.Resultado.Clear();

            tex.Serial._Vel+=VelAt;
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
            ConfiguracaoEnsaio ConfigEnsaio = new ConfiguracaoEnsaio();

            var DialogResult = ConfigEnsaio.ShowDialog();

            if(DialogResult == DialogResult.OK &&ConfigEnsaio.DadosDeEnsaio != null) {
                if(tex.Motor.ZeroSeated) {
                    tex.DadosTeste=ConfigEnsaio.DadosDeEnsaio;
                    tex.TesteStart();
                } else {
                    var result = MessageBox.Show("Zero Máquina não definido!\nDefinir Zero Máquina?","Aviso!",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                    if(result==DialogResult.OK) {

                        zeroMáquinaToolStripMenuItem_Click(this,e);
                    }
                }
            }    
            ConfigEnsaio.Dispose();
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
                    Graph.SuspendLayout();
                    try {
                       series.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetXvalues());
                        series2.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetYvalues());

                    } finally { }
                    if(Graph.ChartAreas[0].AxisX.Maximum<=120) {
                        var tempo = tex.Produto.Resultado.GetZvalues();
                       
                        if(tempo.Last()>=120) {
                            Graph.ChartAreas[0].AxisX.Maximum = Double.NaN;
                            Graph.ChartAreas[0].RecalculateAxesScale();
                        }
                    }
                    Graph.Invalidate();
                    Graph.ResumeLayout();;
                }));
            }
        }

        private void btnUP_Click(object sender,EventArgs e) {
            tex.Motor.ModoMotor=ModoMotor.Subir;
            tex.Motor.SPVelManual=Properties.Settings.Default.VelManual;
            tex.Serial.EnvComandoMotor(ModoMotor.Subir,Properties.Settings.Default.VelManual);

        }
        private void btnDN_Click(object sender,EventArgs e) {
            tex.Motor.ModoMotor= ModoMotor.Descer;
            tex.Serial.EnvComandoMotor(ModoMotor.Descer,Properties.Settings.Default.VelManual);
        }
        private void btnStop_Click(object sender,EventArgs e) {
            tex.Motor.ModoMotor=ModoMotor.Parado;
            tex.Serial.EnvComandoMotor(ModoMotor.Parado,0);
        }

        private void btnFast_Click(object sender,EventArgs e) {
            switch(tex.Motor.ModoMotor) {
                case ModoMotor.Subir:
                    tex.Serial.EnvComandoMotor(ModoMotor.Subir,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Descer:
                    tex.Serial.EnvComandoMotor(ModoMotor.Descer,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Parado:
                    tex.Serial.EnvComandoMotor(ModoMotor.Parado,0);
                    break;
            }
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

            try {

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
            } finally { }
        }

        private void LoadCelCalibrated(object sender, SerialMessageArgument args) {
            Properties.Settings.Default.CalLoadCell=args.doubleValue;
            Properties.Settings.Default.Save();
        }

        private void trackBar1_Scroll(object sender,EventArgs e) {
            switch(tex.Motor.ModoMotor) {
                case ModoMotor.Subir:
                    tex.Serial.EnvComandoMotor(ModoMotor.Subir,((double)trackBar1.Value/10));
                    break;
                case ModoMotor.Descer:
                    tex.Serial.EnvComandoMotor(ModoMotor.Descer,((double)trackBar1.Value/10));
                    break;
                case ModoMotor.Parado:
                    tex.Serial.EnvComandoMotor(ModoMotor.Parado,0);
                    break;
            }
            lbVel.Text=((double)trackBar1.Value/10).ToString();
        }
    }
}
