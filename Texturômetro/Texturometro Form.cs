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
using ExportacaoResultado;
using ProdutoTexturometro;
using EnsaioTextuometro;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Texturometer {
    public partial class TexturometroForms : Form {

        static Texturometro tex = new Texturometro();
        Series series = new Series("Pontos");

        static Timer tick = new Timer();
        static BackgroundWorker bkWork = new BackgroundWorker();
        CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();

        public TexturometroForms() {
            InitializeComponent();
            tick.Interval =16;
            tick.Tick +=new EventHandler(atGraph);
            tick.Enabled=true;
            bkWork.DoWork+= bkWork_DoWork;
            bkWork.WorkerSupportsCancellation = true;

            typeof(Chart).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.SetProperty,
            null,Graph,new object[] { true });

            typeof(Label).InvokeMember("DoubleBuffered",
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
            series.BorderWidth = 2;

            Graph.Series.Clear();
            Graph.Series.Add(series);
            Series s = new Series("0");
            s.ChartType=SeriesChartType.Spline;
            List<double> ly = new List<double>();
            List<double> lx = new List<double>();

            ly.Add(0);
            ly.Add(3);
            lx.Add(0);
            lx.Add(0.01);

            s.Points.DataBindXY(lx,ly);
            Graph.Series.Add(s);
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
            Thread.Sleep(1000);
            tex.LoadCell.ZeroTime();
            tex.Serial.EnvCalibration(Properties.Settings.Default.CalLoadCell); //Verificar
            tex.Serial.DiscardInBuffer();
            tex.Produto.Resultado.Clear();

            tex.Serial.VelDetected+=VelAt;

            tex.fimTeste+=execFimTeste;
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
                if(true){//if(tex.Motor.ZeroSeated) {
                ConfigEnsaio.DadosDeEnsaio.PosInicial=tex.Encoder.Position;
                    tex.TesteStart(ConfigEnsaio.DadosDeEnsaio);
                    AtInfoLabel(ConfigEnsaio.DadosDeEnsaio);
                } else {
                    var result = MessageBox.Show("Zero Máquina não definido!\nDefinir Zero Máquina?","Aviso!",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                    if(result==DialogResult.OK) {

                        zeroMáquinaToolStripMenuItem_Click(this,e);
                    }
                }
            }    
            ConfigEnsaio.Dispose();
        }
        private void AtInfoLabel(DataTest DadosDeEnsaio) {
            
            String un;
            lbInformations.Clear();
            WriteLineLabel("Informações do Teste:","\n");

            WriteLineLabel("Tipo de Ensaio: ",$"{DadosDeEnsaio.Tipo}");
            WriteLineLabel("Velocidade de pré-teste: ",$"{ DadosDeEnsaio.VelPreTeste} mm/s");
            WriteLineLabel("Velocidade de Teste: ",$"{DadosDeEnsaio.VelTeste} mm/s");
            WriteLineLabel("Tipo de Alvo: ",$"{DadosDeEnsaio.TipoLimite}");
            
            un=DadosDeEnsaio.TipoLimite==TipoTarget.Distancia ? "mm" : DadosDeEnsaio.TipoLimite==TipoTarget.Deformacao ? "%" : "g";
            var valAlvo = DadosDeEnsaio.TipoLimite==TipoTarget.Deformacao ? DadosDeEnsaio.ValorLimite*100 : DadosDeEnsaio.ValorLimite;

            WriteLineLabel("Valor Alvo: ",$"{valAlvo} {un}");
            WriteLineLabel("Tempo de Intervalo: ",$"{DadosDeEnsaio.Tempo} s");
            WriteLineLabel("Tipo de Detecção: ",$"{DadosDeEnsaio.TipoDeteccao}");

            un=DadosDeEnsaio.TipoDeteccao==TipoTrigger.Distancia ? "mm" : DadosDeEnsaio.TipoDeteccao==TipoTrigger.Forca ? "g" : String.Empty;

            WriteLineLabel("Valor de Detecção: ",$"{DadosDeEnsaio.ValorDeteccao} {un}");
            WriteLineLabel("Tipo de Tara: ",$"{DadosDeEnsaio.TipoTara}");
            WriteLineLabel("Tipo de Probe: ",$"{DadosDeEnsaio.PontaDeTeste.Tipo}");
            WriteLineLabel("Dimensões da Probe: ",$"{DadosDeEnsaio.PontaDeTeste.getDimin()}");
            lbInformations.AppendText($"\n");

        }

        private void WriteLineLabel(String text1,String text2) {
            lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Bold);

            lbInformations.AppendText(text1);
            flipLbStyle();
            lbInformations.AppendText(text2+"\n");
            flipLbStyle();
        }
        private string flipLbStyle() {
            if(!lbInformations.SelectionFont.Bold) {
                lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Bold); // Definir a fonte em negrito
            } else {
                lbInformations.SelectionFont=new Font(lbInformations.Font,FontStyle.Regular); // Definir a fonte em itálico
            }

            return String.Empty;

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
                    GC.Collect();
                    Graph.SuspendLayout();
                    try {
                       series.Points.DataBindXY(tex.Produto.Resultado.GetZvalues(),tex.Produto.Resultado.GetXvalues());

                    } finally { }

                    if(Graph.ChartAreas[0].AxisX.Maximum<=100&&tex.testRunning) {
                        var tempo = tex.Produto.Resultado.GetZvalues();
                        if(tempo.Count!=0) {
                            var lTempo = tempo.Last();
                            if(lTempo>=100) {
                                Graph.ChartAreas[0].AxisX.Maximum=Double.NaN;
                                Graph.ChartAreas[0].RecalculateAxesScale();
                            } else {
                                Graph.ChartAreas[0].AxisX.Maximum=100;
                                Graph.ChartAreas[0].RecalculateAxesScale();
                            }
                        } else {
                            Graph.ChartAreas[0].AxisX.Maximum=100;
                            Graph.ChartAreas[0].RecalculateAxesScale();
                        }
                    }
                 


                    Graph.Invalidate();
                    Graph.ResumeLayout();
                }));
            }
        }

        private void btnUP_Click(object sender,EventArgs e) {
            tex.Motor.ModoMotor=ModoMotor.Subir;
            tex.Motor.Start(ModoMotor.Subir,Properties.Settings.Default.VelManual);
        }
        private void btnDN_Click(object sender,EventArgs e) {
            tex.Motor.Start(ModoMotor.Descer,Properties.Settings.Default.VelManual);
        }
        private void btnStop_Click(object sender,EventArgs e) {
            tex.Motor.Stop();
        }

        private void btnFast_Click(object sender,EventArgs e) {
            switch(tex.Motor.ModoMotor) {
                case ModoMotor.Subir:
                    tex.Motor.Start(ModoMotor.Subir,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Descer:
                    tex.Motor.Start(ModoMotor.Descer,Properties.Settings.Default.VelManualRapida);
                    break;
                case ModoMotor.Parado:
                    tex.Motor.Stop();
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
                    Graph.ChartAreas[0].CursorX.Position=double.NaN;
                    Graph.ChartAreas[0].CursorY.Position=double.NaN;
                }
            } finally { }
        }

        private void LoadCelCalibrated(object sender, SerialMessageArgument args) {
            Properties.Settings.Default.CalLoadCell=args.doubleValue;
            Properties.Settings.Default.Save();
        }

        private void ToolStripMenuExportCSV_Click(object sender,EventArgs e) {
            ExportacaoCSV.exportarCSV(tex.Produto.Resultado.GetTable());
        }

        private void ToolStripMenuExportExcel_Click(object sender,EventArgs e) {
            ExportacaoExcel.exportarExcel(tex.Produto.Resultado.GetTable());
        }

        private void ToolStripMenuExportPDF_Click(object sender,EventArgs e) {
            for(int i = 0; i < 10;i++)
                tex.Produto.Resultado.Add(new Coord(1*i,2*i,3*i));
            
            CorpoDeProva cp = Dados.getCP();
            tex.Produto.Resultado = cp.Resultado;
            
            ExportacaoRelatorioPDF.exportaPDF(cp,tex.DadosTeste,getImgGrafico(panelGraph));
        }

        private void execFimTeste(object sender, EventArgs e) {
            tex.StopAddResults();

            var tb = tex.Produto.Resultado;

            
                var cp = Dados.getCP();
                tb=cp.Resultado;
                tex.testRunning=false;
                tex.Produto.Resultado=tb;
                tex.DadosTeste=new DataTest() { ValorDeteccao=3,Tipo=TipoDeTeste.TPA };
            

            if(tex.DadosTeste.Tipo==TipoDeTeste.TPA) {

                ResultadosTPA res = ResultadosTPA.CalcTPA(tb,tex.DadosTeste.ValorDeteccao);
                
                Task.Run(() => {
                    this.Invoke(new Action(() => {
                        Graph.ChartAreas[0].AxisX.Maximum =Math.Round(tex.Produto.Resultado.GetZvalues().Last());
                        Graph.ChartAreas[0].RecalculateAxesScale();



                        WriteLineLabel("Resultados:","\n");
                        WriteLineLabel("Tamanho do produto: ",$"{Math.Round(res.TamProd,2)} mm");
                        WriteLineLabel("Dureza: ",$"{Math.Round(res.Hardness,2)} g");
                        WriteLineLabel("Elasticidade: ",$"{Math.Round(res.Springiness*100,2)} %");
                        WriteLineLabel("Coesividade: ",$"{Math.Round(res.Cohesiveness*100,2)} %");
                        WriteLineLabel("Resiliência: ",$"{Math.Round(res.Resilience*100,2)} %");
                        WriteLineLabel("Adesividade : ",$"{Math.Round(res.Adhesiveness,2)} g.s");
                        WriteLineLabel("Gumosidade: ",$"{Math.Round(res.Gumminess,2)}");
                        WriteLineLabel("Mastigabilidade: ",$"{Math.Round(res.Chewiness,2)}");

                    }));
                });
            }

            

        }

        private static Image getImgGrafico(Panel panel ){
            Image img;

            Bitmap bitmap = new Bitmap(panel.Width,panel.Height);
            panel.DrawToBitmap(bitmap,new Rectangle(0,0,panel.Width,panel.Height));

            using(Graphics g = Graphics.FromImage(bitmap)) {
                foreach(Control control in panel.Controls) {
                    if(control!=null) {
                        using(Bitmap controlBitmap = new Bitmap(control.Width,control.Height)) {
                            control.DrawToBitmap(controlBitmap,new Rectangle(Point.Empty,control.Size));
                            g.DrawImage(controlBitmap,control.Location);
                        }
                    }
                }
            }

            img = bitmap;   

            return img;
        }

        private void button1_Click_1(object sender,EventArgs e) {
            execFimTeste(this,EventArgs.Empty);
        }

    }
}
