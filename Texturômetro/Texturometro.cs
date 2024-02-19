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
		private Timer _timer;
        private EventHandler TargetAcao;

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
			Motor.ZeroSeating+= Serial.EnvZeroMaquina;


            LoadCell.ZeroSet+=Serial.EnvTARA;
			LoadCell.Calibration+=Serial.CalLC;
			LoadCell.ZerarTime+=Serial.EnvZeroTime;

            Produto=new CorpoDeProva(1);

        }	


		public void TesteStart() {
			if(Motor.ZeroSeated) {
                Teste = EnsaioFactoryMethod.criarTeste(DadosTeste.Tipo);
                //Produto=new CorpoDeProva(1);
                ExecTeste(this, new EventArgs());
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
            LoadCell.ValorLoad=e.doubleValue1;
        }
        private void _atualizaEncoder(object sender,SerialMessageArgument e) {
            Motor.Posicao=e.doubleValue;
        }
        #endregion

        public void StartAddResults(object sender,SerialMessageArgument e) {
            if(Produto!=null) {
                Serial.LoadCellDetected+=_atualizaResultadoL;
                Serial.EncoderDetected+=_atualizaResultadoE;
            }
        }
        public void StopAddResults() {
            Serial.LoadCellDetected-=_atualizaResultadoL;
            Serial.EncoderDetected-=_atualizaResultadoE;
        }

        private void _atualizaResultadoL(object sender,SerialMessageArgument e) {
            Produto.Resultado.AddXZvalue(e.doubleValue1,e.doubleValue2);
        }
        private void _atualizaResultadoE(object sender,SerialMessageArgument e) {
            Produto.Resultado.AddYZvalue(e.doubleValue1,e.doubleValue2);
        }

        private void ExecTeste(object sender, EventArgs args) {

            if(true);
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


	}
}