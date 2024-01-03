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


namespace TexturometroClass {
	public class Texturometro {
		private Motor _motor;
        private Chave _sensorLS;
        private Chave _sensorLI;
        private Ensaio _teste;
		private DataTest _dadosTeste;
		private LoadCell _loadCell;
        private SerialManager _serial;
		private CorpoDeProva _produto;
		private bool _settingZero;
		private Timer _timer;
		private Timer _keybaordTimer;


        public Texturometro(DataTest DadosDoTeste) {
            _dadosTeste = DadosDoTeste;

			_teste = EnsaioFactoryMethod.criarTeste(_dadosTeste.Tipo);

			_loadCell = new LoadCell(_dadosTeste.LoadCellValMax);

            _sensorLS=new Chave();
			_sensorLI=new Chave();


            _serial=new SerialManager("COM1");

            _serial.LSDetected+=_atualizaLS;
			_serial.LIDetected+=_atualizaLI;
			_serial.LoadCellDetected+=_atualizaLoadCell;
			_serial.EncoderDetected+=_atualizaEncoder;

            _motor=new Motor();
            _motor.SPVel= _dadosTeste.VelMotor;
			_motor.SPVelManual= _dadosTeste.VelMotorManual;
			_motor.MotorStarted+=_enviaSerialMotor;
			_motor.MotorStopped+=_enviaSerialMotor;
			_motor.ZeroSeated+=_serial.EnvZeroPosicao;
			_loadCell.ZeroSeated += _serial.EnvZeroLoad;

			_keybaordTimer=new Timer();
			_keybaordTimer.Tick+=_keyboardUper;
        }	

		public void _keyboardUper(object sender, EventArgs e) {

		}

		public void TesteStart(object sender, EventArgs e) {
			try {
				if(_serial.IsOpen) {
					_serial.Close();
				}
				_serial.Open();
			}catch(Exception ex) { 
				MessageBox.Show(ex.Message); 
			}

			if(_teste.ZeroSeated) {
				ExecTeste();
			} else {
				var result = MessageBox.Show("Zero Máquina não definido!\nDefinir Zero Máquina?","Aviso!",MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning);
				if(result == DialogResult.OK) {
                    _motor.Manual = true;
					_settingZero=true;
                }
			}
		}

        #region SetSensores
        private void _atualizaLS(object sender, SerialMessageArgument e) {
			_sensorLS.Estado = e.boolValue;	
					
		}

		private void _atualizaLI(object sender,SerialMessageArgument e) {
            _sensorLI.Estado=e.boolValue;
        }

		private void _atualizaLoadCell(object sender,SerialMessageArgument e) { 
            _loadCell.ValorLoad=e.intValue;

        }

        private void _atualizaEncoder(object sender,SerialMessageArgument e) {
            _motor.Posicao = e.intValue;
        }
        #endregion

        private void ExecTeste() {
			//?????
		}

		private void _settingZeroMaquina() {
            _motor.StartSetZero(ModoMotor.Descer);
			_loadCell.CargaDetected+=_setZero;
        }

		private void _setZero(object sender, EventArgs e) {
			_motor.Stop();
			_teste.ZeroSeated=true;
			_loadCell.CargaDetected-=_setZero;
		}

		private void _enviaSerialMotor(object sender, MotorArgument e) {
			_serial.EnvComandoMotor(e.Modo,e.Vel);
		}
	}
}