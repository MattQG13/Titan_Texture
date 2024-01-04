using ClassesSuporteTexturometro;
using System;
using System.IO.Ports;
using System.Text;

namespace SerialManagerTexturometro{
    public class SerialManager {
        private SerialPort _serialPort;

        public EventHandler<SerialMessageArgument> MessageInterpreted;
        public EventHandler<SerialMessageArgument> LSDetected;
        public EventHandler<SerialMessageArgument> LIDetected;
        public EventHandler<SerialMessageArgument> LoadCellDetected;
        public EventHandler<SerialMessageArgument> EncoderDetected;
        public EventHandler<SerialMessageArgument> MotorDetected;


        public SerialManager(string portName,int baudRate) {
            _serialPort=new SerialPort(portName,baudRate);
            _serialPort.DataReceived+=_dataReceived;
        }

        public SerialManager(string portName) {
            _serialPort=new SerialPort(portName,115200);
            _serialPort.DataReceived+=_dataReceived;
        }
        public SerialManager() {
            _serialPort=new SerialPort();
            _serialPort.DataReceived+=_dataReceived;
        }

        #region SerialPort

        public void SetCOM (string portName,int baudRate) {
            _serialPort.PortName=portName;
            _serialPort.BaudRate=baudRate;
        }
        public void SetCOM(string portName) {
            _serialPort.PortName=portName;
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
            _serialPort.Open();
        }

        public void Close() {
            _serialPort.Close();
        }

        public void Write(string message) {
            if (IsOpen)
            {
                _serialPort.Write(message);
            }
        }

        private void _dataReceived(object sender,SerialDataReceivedEventArgs e) {
            string mensagem = _serialPort.ReadTo("\r");
            string[] partesDaMensagem = _processaSerial(mensagem);
            _interpretaMensagem(partesDaMensagem);
        }
        #endregion

        private static string[] _processaSerial(string mensagem) {
            mensagem=mensagem.Replace("[",string.Empty).Replace("]",string.Empty);
            string[] partes = mensagem.Split(';');
            return partes;
        }

        private void _interpretaMensagem(string[] partesDaMensagem) {
            //chama os eventos
            SerialMessageArgument args = new SerialMessageArgument();
            args.Objeto=partesDaMensagem[0];

            switch(partesDaMensagem.Length) {
                case 2: //Load cell ou Encoder
                    if(args.Objeto=="LS"||args.Objeto=="LI") {
                        args.boolValue=partesDaMensagem[1]=="1" ? true : false;
                    }
                    if(args.Objeto=="E"||args.Objeto=="L") {
                        args.intValue=int.Parse(partesDaMensagem[1]);
                    }
                    break;
                case 3: //Motor X
                    args.Comando=partesDaMensagem[1];
                    args.doubleValue=double.Parse(partesDaMensagem[2]);
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
            }
        }

        public void EnvComandoMotor(ModoMotor comando,double valor) {
            StringBuilder sB = new StringBuilder("");
            sB.Append("[M;");
            switch(comando) {
                case ModoMotor.Parado:
                    sB.Append("S");
                    break;
                case ModoMotor.Subir:
                    sB.Append("UP");
                    break;
                case ModoMotor.Descer:
                    sB.Append("DW");
                    break;
            }
            sB.Append(valor.ToString());
            sB.Append("]\r");

            Write(sB.ToString());
        }

        public void EnvZeroPosicao(object sender, EventArgs e) {
            Write("[E;ZERO]\r");
        }

        public void EnvZeroLoad(object sender, EventArgs e)
        {
            Write("[L;ZERO]\r");
        }
    }
}