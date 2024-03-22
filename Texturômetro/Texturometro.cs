/*
* Nome do Arquivo: Classes.cs
* Autor: Mateus Quintino
* Data de Início: 01/07/2023
* Descrição: Este código possui classes para serem usadas
* no controlador do texturômetro desenvolvido para projeto de TCC
*/

/*
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxxxddddddddxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxddol:,,,'.'',',,;:codxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxddlc;..................',coxxxxxxxxxxxxxxxxxx
xxxxxxxxdoc;'......................''',cdxxxxxxxxxxxxxxx
xxxxxxdc,...............................':oxxxxxxxxxxxxx
xxxxdd;.........;,.........................;oxxxxxxxxxxx
xxxdl...'.......';,..........................:xxxxxxxxxx
xxdl.............;;'..................     ...,dxxxxxxxx
xdl.  ..........'::,...............          ..'oxxxxxxx
do'   ..........';:,...............             .oxxxxxx
d,     .........':ol;,'............              'dxxxxx
c.     ....''',:ldxxxdllllllllc:,'...             ;xxxxx
,       ..';:cdxO0000KKKKK000KK00Okkdl:,.. .      .lxxxx
.      ..,:oddO0KKKKKKKKKKKKKKKKKK000OOxxxc,.      'dxxx
.     .,:oxxdOKKKKKKKKKKKKKKKKKK00000OOOkOkd:.     .cxxx
.    .:dxkkkO0KXXKKKKKKKKKKKKKKKKK00OOOOOOOkdc.....',dxx
l.  .:dxkkOO0KXXXKKKKKKKKKKKKKKKK000OOOOOOOkkd,..lxl:lxx
ol,..:ddxkkOO0KKKKK00KKKKK000000OOOOOOOOOOkkkkc.cxxl:;xx
dol;.,ddkkkkkkOOOO000000K0000KK0OO0OOOOOkkkkkkd;lxxxl'ox
dd:c:;dkkOkkkkO000OOOOOOO0000000kkkkkkxxddxkkkxc;coxo.:x
ddolocddddxxxdxkO0OOkdxOOxdxdodoolloxkkkkkkOOkxlclcd;.'d
dddoocooooodddlcclc;;:dkkkddol;,:;.':ldxkOOOOkxo:coo...o
dddllcoxxxdol;'.,c,;clxkOOkkkkdooooodxO000OOOkxolod:...c
dool::okOkkdoollolloxxkO00OkOOOOkkkOO00K00OOOkxdddc....:
oolcc.ckOO0000OOOOOOkkk0KKOOOOO0K000000KK0OOOkxc;,.....,
oclll..dkO0KXKKKK00Okkk0KKOOOOOOO000000KK0OOOkd;.......'
lllll'.:xkOKKK0000OkkkkO0K0OOOkkxdkkOOOOOOOOkkd,........
olcll' .okkOOOOOkxodkkkk0K0OOOOkOxoldxkkkkkkxxd'........
lllcc' .,dxxkkkxoclxkOkkO00OOOkkOOkxoclodddxxxl.........
llllc'...,looooc:cdxxxkO000Okkkkkkkkkxolcdxxxx,.........
lcllc.....'clc:;cdxxxdddddxkxkOOOkkkkxxdoxkxxl..........
c:c:,......'loolllodxxxkkkOOkkxxdd::dkkdxxxdo...........
.........   .:ddooooc:lllodooocolodkkkkxxdol. .'..'.....
.......      .'codxxxdodoododdxkOkkkkkkxddl.   ...'.....
.......      . .'lddddxkOOOOOkkxxdxxkkxdoc.    .........
.......      ..   'ldddollooooddxkkkkxol,'.   .....'....
........  .. ..     .lxxkkkOOOOOOOkxdl;...     ..'.'....
..................    'codxxxxxxxdlc;,.....   ..........
..............'.  .   ..';::::::;;;;,... ..  ....... ...
.........''....  ..   ...',;,,,,,,'..... .' ........ ...
.........'......  .........,;;'............... ....   ..
.........'.....  . .. ...,,';:;'...........   ....    .
  ...............  .. .  .,;;:;,'..........  ...
 */

using ClassesSuporteTexturometro;
using LoadCellTexturometro;
using MotorTexturometro;
using ProdutoTexturometro;
using SerialManagerTexturometro;
using System;
using System.Windows.Forms;
using EnsaioTextuometro;
using DadosDeEnsaio;
using EncoderMotor;
using System.Reflection;
using System.Drawing;
using System.Timers;
using Timer = System.Timers.Timer;
//using Timer = System.Windows.Forms.Timer;

namespace TexturometroClass {
	public class Texturometro {
        public Motor Motor;
        public Encoder Encoder;
        public Chave SensorLS;
        public Chave SensorLI;
        public Ensaio Teste;
        public DataTest DadosTeste;
        public LoadCell LoadCell;
        public SerialManager Serial;
		public CorpoDeProva Produto;
		private static Timer _timer;

        public EventHandler fimTeste;
        
        public Texturometro() {
			LoadCell = new LoadCell();

            SensorLS=new Chave();
			SensorLI=new Chave();


            Serial=new SerialManager();

            Serial.LSDetected+=_atualizaLS;
			Serial.LIDetected+=_atualizaLI;
			Serial.LoadCellDetected+=_atualizaLoadCell;
			Serial.EncoderDetected+=_atualizaEncoder;
			Serial.TimeSeted+=StartAddResults;


            Motor=new Motor();
			Motor.MotorStarted+=_enviaSerialMotor;
			Motor.MotorStopped+=_enviaSerialMotor;
            Motor.MotorGoTo+=_enviaSerialMotorGoTo;
			Motor.ZeroSeating+= Serial.EnvZeroMaquina;
            

            LoadCell.ZeroSet+=Serial.EnvTARA;
			LoadCell.Calibration+=Serial.CalLC;
			LoadCell.ZerarTime+=Serial.EnvZeroTime;

            Encoder=new Encoder();
            Produto = new CorpoDeProva();
            _timer = new Timer();
            _timer.Elapsed+=AutoStop;
            //_timer.Tick+=AutoStop;
            _timer.Enabled=true;
        }	


		public void TesteStart(DataTest DadosDoTeste) {
            DadosTeste = DadosDoTeste;    
            Teste = EnsaioFactoryMethod.criarTeste(DadosTeste.Tipo);
            Produto=new CorpoDeProva();
            LoadCell.ZeroTime();
            Produto.Resultado.Clear();

            ExecTeste(this, new EventArgs());
		}

        #region SetSensores
        private void _atualizaLS(object sender, SerialMessageArgument e) {
			SensorLS.Estado = e.boolValue;			
		}

		private void _atualizaLI(object sender,SerialMessageArgument e) {
            SensorLI.Estado=e.boolValue;
        }

		private void _atualizaLoadCell(object sender,SerialMessageArgument e) {
            LoadCell.ValorLoad=e.doubleValue1;
        }
        private void _atualizaEncoder(object sender,SerialMessageArgument e) {
            Encoder.Position=e.doubleValue1;
        }
        private void _atualizaTamanho(object sender,SerialMessageArgument e) {
            Produto.TamanhoAtual=e.doubleValue1;
        }
        #endregion

        static bool ContainsHandler(EventHandler<SerialMessageArgument> eventDelegate,EventHandler<SerialMessageArgument> handler) {
            if(eventDelegate==null)
                return false;

            foreach(Delegate existingHandler in eventDelegate.GetInvocationList()) {
                if(existingHandler==handler) {
                    return true;
                }
            }

            return false;
        }

        private void AutoStop(object sender, EventArgs args) {
            _timer.Stop();
        }

        public void StartAddResults(object sender,SerialMessageArgument e) {
            if(!ContainsHandler(Serial.LoadCellDetected,_atualizaResultadoL)&&!ContainsHandler(Serial.EncoderDetected,_atualizaResultadoE)) {
                Serial.LoadCellDetected+=_atualizaResultadoL;
                Serial.EncoderDetected+=_atualizaResultadoE;
            }
        }
        public void StopAddResults() {
            for(int i = 0;i<10;i++)
                try {
                Serial.LoadCellDetected-=_atualizaResultadoL;
                Serial.EncoderDetected-=_atualizaResultadoE;
                
            } finally { }
        }

        private void _atualizaResultadoL(object sender,SerialMessageArgument e) {
            Produto.Resultado.AddXZvalue(e.doubleValue1,e.doubleValue2);
        }
        private void _atualizaResultadoE(object sender,SerialMessageArgument e) {
            Produto.Resultado.AddYZvalue(e.doubleValue1,e.doubleValue2);
        }

        private void ExecTeste(object sender, EventArgs args) {
            RemoveEvento(sender);

            Acao Action = Teste.AcaoAtual;
            Teste.Next();
            switch(Action) {

                case Acao.DescerPreTeste: //Desce pre teste até deteccao
                    Motor.Start(ModoMotor.Descer,DadosTeste.VelPreTeste);
                    switch(DadosTeste.TipoDeteccao) {
                        case TipoTrigger.Forca:
                            LoadCell.DetectLoad(DadosTeste.ValorDeteccao);
                            LoadCell.CargaDetected+=ExecTeste;
                            break;
                        default:
                            break;
                    }
                    break;
                
                case Acao.DescerTeste:  //Desce para teste até limite

                    if(Produto.TamanhoOriginal==0) {
                        Produto.TamanhoOriginal=Encoder.Position;
                        Serial.EncoderDetected+=_atualizaTamanho;
                    }else if(Produto.TamanhoRecuperacao==0) {
                        Produto.TamanhoRecuperacao = Encoder.Position;
                    }

                    StartAddResults(this,new SerialMessageArgument());
                    Motor.Start(ModoMotor.Descer,DadosTeste.VelTeste);

                    switch(DadosTeste.TipoLimite) {
                        case TipoTarget.Deformacao:
                            Produto.TargetDeformation(DadosTeste.ValorLimite);
                            Produto.DeformacaoReached+=ExecTeste;
                            break;
                        default :
                            break;
                    }
                    break;

                case Acao.SubirTeste: //Sobe em velocidade de teste
                    Motor.Start(ModoMotor.Subir,DadosTeste.VelTeste);
                    Encoder.TargetPosition(Produto.TamanhoOriginal,Encoder.Position);
                    Encoder.positionReached +=ExecTeste;
                    break;

                case Acao.EsperarAssentamento: //Aguarda tempo de assentamento
                    Motor.Stop();
                    _timer.Interval=Convert.ToInt32(DadosTeste.Tempo*1000);
                    _timer.Elapsed+=ExecTeste;
                    //_timer.Tick+=AutoStop;
                    _timer.Start();
                    break;
                case Acao.SubirPosTeste:
                    Motor.Start(ModoMotor.Subir,DadosTeste.VelTeste);
                    Encoder.TargetPosition(DadosTeste.PosInicial,Encoder.Position);
                    Encoder.positionReached+=ExecTeste;
                    break;
                case Acao.Fim:
                    Motor.Stop();
                    fimTeste?.Invoke(this,EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }

        private void RemoveEvento(object sender) {
            EventInfo[] eventos = sender.GetType().GetEvents();
            foreach(EventInfo evento in eventos) {
                try {
                    if(evento.Name!="Elapsed") {
                        evento.RemoveEventHandler(sender,new EventHandler(ExecTeste));
                    } else {
                        evento.RemoveEventHandler(sender,new ElapsedEventHandler(ExecTeste));
                    }

                } finally { }
            }
        }

        public void setSerial(string com,int baud = 115200) {
			Serial.SetCOM(com,baud);
		}

        public void iniciaSerial() {
            try {
                if(Serial.IsOpen) {
                    Serial.Close();
                }
                Serial.Open();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

		private void _enviaSerialMotor(object sender, MotorArgument e) {
			Serial.EnvComandoMotor(e.Modo,e.Vel);
		}
        private void _enviaSerialMotorGoTo(object sender, MotorArgument e) {
            Serial.EnvComandoMotor(e.Modo,e.Vel,e.FinalPosition);
        }
	}
}