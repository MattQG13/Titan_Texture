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
using System.Drawing;
using System.Configuration;
using System.IO.Ports;


namespace TexturometroClass {
	public class Texturometro {
        public Motor Motor;
        public Chave SensorLS;
        public Chave SensorLI;
        public Ensaio Teste;
        public DataTest DadosTeste;
        public LoadCell LoadCell;
        public SerialManager Serial;
		public CorpoDeProva Produto;
        private bool _settingZero;
		private Timer _timer;
		private Timer _keybaordTimer;

        public Texturometro(DataTest DadosDoTeste) {
            DadosTeste = DadosDoTeste;

			Teste = EnsaioFactoryMethod.criarTeste(DadosTeste.Tipo);

			LoadCell = new LoadCell(DadosTeste.LoadCellValMax);

            SensorLS=new Chave();
			SensorLI=new Chave();


            Serial=new SerialManager();

            Serial.LSDetected+=_atualizaLS;
			Serial.LIDetected+=_atualizaLI;
			Serial.LoadCellDetected+=_atualizaLoadCell;
			Serial.EncoderDetected+=_atualizaEncoder;
			

            Motor=new Motor();
            Motor.SPVel= DadosTeste.VelMotor;
			Motor.SPVelManual= DadosTeste.VelMotorManual;
			Motor.MotorStarted+=_enviaSerialMotor;
			Motor.MotorStopped+=_enviaSerialMotor;
			Motor.ZeroSeated+=Serial.EnvZeroPosicao;

			LoadCell.ZeroSet+=Serial.EnvTARA;
			LoadCell.Calibration+=Serial.CalLC;

			_keybaordTimer=new Timer();
			_keybaordTimer.Tick+=_keyboardUper;

			Produto=new CorpoDeProva(1);
        }	

		public void _keyboardUper(object sender, EventArgs e) {

		}

		public void TesteStart(object sender, EventArgs e) {
			try {
				if(Serial.IsOpen) {
					Serial.Close();
                }
                Serial.DiscardNull=true;
                Serial.Open();
			}catch(Exception ex) { 
				MessageBox.Show(ex.Message); 
			}

			if(Teste.ZeroSeated) {
				ExecTeste();
			} else {
				var result = MessageBox.Show("Zero Máquina não definido!\nDefinir Zero Máquina?","Aviso!",MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning);
				if(result == DialogResult.OK) {
                    Motor.Manual = true;
					_settingZero=true;
                }
			}
		}

        #region SetSensores
        private void _atualizaLS(object sender, SerialMessageArgument e) {
			SensorLS.Estado = e.boolValue;	
					
		}

		private void _atualizaLI(object sender,SerialMessageArgument e) {
            SensorLI.Estado=e.boolValue;
        }

		private void _atualizaLoadCell(object sender,SerialMessageArgument e) {
			Produto.Resultado.Add(e.doubleValue,Produto.Resultado.Count+1,0);
            LoadCell.ValorLoad=e.intValue;

        }

        private void _atualizaEncoder(object sender,SerialMessageArgument e) {
            Motor.Posicao = e.intValue;
        }
        #endregion

        private void ExecTeste() {
			//?????
		}

		public void setSerial(string com,string baud = "115200") {
			Serial.SetCOM(com,Convert.ToInt32(baud));
			
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


        private void _settingZeroMaquina() {
            Motor.StartSetZero(ModoMotor.Descer);
			LoadCell.CargaDetected+=_setZero;
        }

		private void _setZero(object sender, EventArgs e) {
			Motor.Stop();
			Teste.ZeroSeated=true;
			LoadCell.CargaDetected-=_setZero;
		}

		private void _enviaSerialMotor(object sender, MotorArgument e) {
			Serial.EnvComandoMotor(e.Modo,e.Vel);
		}

	}
}