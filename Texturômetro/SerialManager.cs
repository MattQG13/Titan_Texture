using ClassesSuporteTexturometro;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialManagerTexturometro{
    public class SerialManager {
        private SerialPort _serialPort;

        public EventHandler<SerialMessageArgument> MessageInterpreted;
        public EventHandler<SerialMessageArgument> LSDetected;
        public EventHandler<SerialMessageArgument> LIDetected;
        public EventHandler<SerialMessageArgument> LoadCellDetected;
        public EventHandler<SerialMessageArgument> EncoderDetected;
        public EventHandler<SerialMessageArgument> MotorDetected;
        public EventHandler<SerialMessageArgument> TimeSeted;
        public EventHandler<SerialMessageArgument> ZeroSeated;
        private CultureInfo culture = new CultureInfo("en-US"); //CultureInfo.InvariantCulture;
        private char endChar = '!';

        public SerialManager(string portName,int baudRate) {
            _serialPort=new SerialPort(portName,baudRate);
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;
        }

        public SerialManager(string portName) {
            _serialPort=new SerialPort(portName,115200);
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;

        }
        public SerialManager() {
            _serialPort=new SerialPort();
            _serialPort.DataReceived+=_dataReceived;
            _serialPort.ReadTimeout=2048;
            _serialPort.WriteTimeout=2048;
        }

        #region SerialPort

        public void SetCOM (string portName,int baudRate) {
            if(!string.IsNullOrEmpty(portName))_serialPort.PortName=portName;
                _serialPort.BaudRate=baudRate;
        }
        public void SetCOM(string portName) {
            if(!string.IsNullOrEmpty(portName))_serialPort.PortName=portName;
        }

        public void SetCOM(int baudRate) {
                _serialPort.BaudRate=baudRate;
        }

        public bool IsOpen {
            get {
                return _serialPort.IsOpen;
            }
        }

        public bool DiscardNull {
            get {
                return _serialPort.DiscardNull;
            }
            set {
                _serialPort.DiscardNull=value;
            }
        }


        public void Open() {
            if(!string.IsNullOrEmpty(_serialPort.PortName)) {
                try {
                    _serialPort.Open();
                    _serialPort.DiscardInBuffer();

                } catch(Exception e) {
                    MessageBox.Show("Erro de conexão com texturômetro!","ERRO",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        public void Close() {
            /*LSDetected=null;
            LIDetected=null;
            LoadCellDetected=null;
            EncoderDetected=null;
            MotorDetected=null;
            MessageInterpreted=null;*/
            _serialPort.Close();
        }

        public void DiscardInBuffer() {
            if(IsOpen) {
                _serialPort.DiscardInBuffer();
            }
        }

        public void Write(string message) {
            if (IsOpen)
            {
                _serialPort.Write(message+endChar.ToString(culture));
            }
        }

        private void _dataReceived(object sender,SerialDataReceivedEventArgs e) {
            while(_serialPort.BytesToRead>0) {
                try {
                    string mensagem = _serialPort.ReadTo("!");
                    if(mensagem.Contains("Load"))
                        mensagem=mensagem;
                    string[] partesDaMensagem = _processaSerial(mensagem);
                    _interpretaMensagem(partesDaMensagem);
                } catch(TimeoutException) { _serialPort.Close(); }
            }
        }

        #endregion

        private static string[] _processaSerial(string mensagem) {
            mensagem=mensagem.Replace("[",string.Empty).Replace("]",string.Empty);
            string[] partes = mensagem.Split(';');
            return partes;
        }

        private void _interpretaMensagem(string[] partesDaMensagem) {
            SerialMessageArgument args = new SerialMessageArgument();
            args.Objeto=partesDaMensagem[0];

            switch(partesDaMensagem.Length) {
                case 2: //Load cell ou Encoder
                    if(args.Objeto=="LS"||args.Objeto=="LI") {
                        args.boolValue=partesDaMensagem[1]=="1" ? true : false;
                    }
                    if(args.Objeto=="E"){
                        args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                    }
                    if(args.Objeto=="CAL") {
                        args.doubleValue=double.Parse(partesDaMensagem[1],culture);
                    }
                    if(args.Objeto=="L") {
                        args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                    }
                    break;
                case 3: //Motor
                    if(args.Objeto=="M") {
                        args.Comando=partesDaMensagem[1];
                        args.doubleValue=double.Parse(partesDaMensagem[2],culture);
                    }

                    if(args.Objeto=="L") {
                        args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                        args.doubleValue2=double.Parse(partesDaMensagem[2],culture);
                    }
                    if(args.Objeto=="E") {
                        args.doubleValue1=double.Parse(partesDaMensagem[1],culture);
                        args.doubleValue2=double.Parse(partesDaMensagem[2],culture);
                    }
                    break;
                default:
                    break;
            }
            MessageInterpreted?.Invoke(this,args);

            switch (args.Objeto) { 
                case "LS": 
                        LSDetected?.Invoke(this, args);         
                        break; 
                case "LI": 
                        LIDetected?.Invoke(this, args);  
                        break; 
                case "L": 
                        LoadCellDetected?.Invoke(this, args);  
                        break; 
                case "E": 
                        EncoderDetected?.Invoke(this, args);  
                        break; 
                case "M": 
                        MotorDetected?.Invoke(this, args);  
                        break;
                case "INITIME":
                        TimeSeted?.Invoke(this, args);
                        break;
                case "ZERO":
                        ZeroSeated?.Invoke(this, args);
                    break;
            }
        }

        public void EnvComandoMotor(ModoMotor comando,double vel) {
            StringBuilder sB = new StringBuilder("");
            sB.Append("[M;");
            switch(comando) {
                case ModoMotor.Parado:
                    sB.Append("S;");
                    break;
                case ModoMotor.Subir:
                    sB.Append("UP;");
                    break;
                case ModoMotor.Descer:
                    sB.Append("DN;");
                    break;
            }
            sB.Append(vel.ToString(culture));
            sB.Append("]");

            Write(sB.ToString());
        }
        public void EnvComandoMotor(ModoMotor comando,double vel,double finalPosition) {
            StringBuilder sB = new StringBuilder("");
            sB.Append("[M;");
            switch(comando) {
                case ModoMotor.Parado:
                    sB.Append("S;");
                    break;
                case ModoMotor.Subir:
                    sB.Append("UP;");
                    break;
                case ModoMotor.Descer:
                    sB.Append("DN;");
                    break;
            }
            sB.Append(vel.ToString(culture));
            sB.Append(";");
            sB.Append(finalPosition.ToString(culture));
            sB.Append("]");

            Write(sB.ToString());
        }

        public void CalLC(object sender,SerialMessageArgument e) {
            string s = "[CAL;"+e.doubleValue.ToString(culture) +"]";
            Write(s);
        }

        public void EnvZeroMaquina(object sender,SerialMessageArgument e) {
            string s = "[ZERAR;"+e.doubleValue1.ToString(culture)+";"+e.doubleValue2.ToString(culture)+"]";
            Write(s);
        }

        public void EnvTARA(object sender,EventArgs e) {
            string s = "[TARA]";
            Write(s);
        }

        public void EnvZeroTime(object sender, EventArgs e) {
            string s = "[INITIME]";
            Write(s);
        }
    }
}