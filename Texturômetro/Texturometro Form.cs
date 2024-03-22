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
using System.Collections.Generic;
using EnsaioTextuometro;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using Org.BouncyCastle.Crypto;
using System.Threading.Tasks;

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
            //tex.Serial.EnvCalibration(Properties.Settings.Default.CalLoadCell);
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
                    tex.Motor.Start(ModoMotor.Subir,((double)trackBar1.Value/10));
                    break;
                case ModoMotor.Descer:
                    tex.Motor.Start(ModoMotor.Descer,((double)trackBar1.Value/10));
                    break;
                case ModoMotor.Parado:
                    tex.Motor.Stop();
                    break;
            }
            lbVelSP.Text=((double)trackBar1.Value/10).ToString();
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


            ExportacaoRelatorioPDF.exportaPDF(cp,tex.DadosTeste,getImgGrafico(panelGraph));

            getImgGrafico(panelGraph).Save(@"D:\TestDrawToBitmap.jpeg",ImageFormat.Jpeg);
        }

        private void button1_Click(object sender,EventArgs e) {
            //tex.StopAddResults();
        }

        private void execFimTeste(object sender, EventArgs e) {
            tex.StopAddResults();

            var tb = tex.Produto.Resultado;

            //var cp = Dados.getCP();
            //tb= cp.Resultado;

            //tex.Produto.Resultado=tb;
            //tex.DadosTeste=new DataTest() { ValorDeteccao=3, Tipo = TipoDeTeste.TPA };


            int index0 = Ensaio.Calculo.SearchFirstOccurrence(tb.GetXvalues(),tex.DadosTeste.ValorDeteccao,0,true);
            int index1 = Ensaio.Calculo.SearchFirstOccurrence(tb.GetXvalues(),tex.DadosTeste.ValorDeteccao,index0+10,false);

            int index2 = Ensaio.Calculo.SearchFirstOccurrence(tb.GetXvalues(),tex.DadosTeste.ValorDeteccao,index1+10,true);

            int index3 = Ensaio.Calculo.SearchFirstOccurrence(tb.GetXvalues(),tex.DadosTeste.ValorDeteccao,index2+10,false);

            double max1 = tb.GetXvalues().GetRange(index0,(index1-index0)).Max();
            double max2 = tb.GetXvalues().GetRange(index2,(index3-index2)).Max();

            int indexMax1 = tb.GetXvalues().IndexOf(max1);
            int indexMax2 = tb.GetXvalues().IndexOf(max2);

            double A1 = Ensaio.Calculo.GetArea(tb.GetZvalues(),tb.GetXvalues(),index0,index1);
            double A2 = Ensaio.Calculo.GetArea(tb.GetZvalues(),tb.GetXvalues(),index2,index3);

            double A3 =  Ensaio.Calculo.GetArea(tb.GetZvalues(),tb.GetXvalues(),index1,index2);

            double R1 = Ensaio.Calculo.GetArea(tb.GetZvalues(),tb.GetXvalues(),index0,indexMax1);
            double R2 = Ensaio.Calculo.GetArea(tb.GetZvalues(),tb.GetXvalues(),indexMax1,index1);

            var Hardness = max1;
            var Springiness = tb.GetYvalues()[index2]/tb.GetYvalues()[index0];
            var Cohesiveness = max2/max1;
            var Resilience = R2/R1;
            var Adhesiveness =A3;
            var Gumminess = Hardness*Cohesiveness;
            var Chewiness = Gumminess*Springiness;

            StringBuilder sB = new StringBuilder("");

            sB.AppendLine($"Tamanho do produto: {Math.Round(tb.GetYvalues()[index0],2)} mm");
            sB.AppendLine($"Tipo de teste: {tex.DadosTeste.Tipo }");

            sB.AppendLine("\nResultados:");
            sB.AppendLine($"Dureza: {Math.Round(Hardness,2)} g");
            sB.AppendLine($"Elasticidade: {Math.Round(Springiness*100,2)} %");
            sB.AppendLine($"Coesividade: {Math.Round(Cohesiveness*100,2)} %");
            sB.AppendLine($"Resiliência: {Math.Round(Resilience*100,2)} %");
            sB.AppendLine($"Adesividade : {Math.Round(Adhesiveness,2)} g.seg");
            sB.AppendLine($"Gumosidade : {Math.Round(Gumminess,2)}");
            sB.AppendLine($"Mastigabilidade : {Math.Round(Chewiness,2)}");

            Task.Run(() => {
                this.Invoke(new Action(() => {
                    lbInformations.Text=sB.ToString();

                }));
            });


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
