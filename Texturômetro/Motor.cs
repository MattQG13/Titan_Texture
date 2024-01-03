using ClassesSuporteTexturometro;
using System;

namespace MotorTexturometro {
	public class Motor {
        private double _SPVel;
        private double _SPVelManual;
		private double _SPVelMan;
        private ModoMotor _modoOper;

        public EventHandler SPVelChanged;
        public EventHandler SPVelManualChanged;
        public EventHandler EstadoMotorChanged;
        public EventHandler<MotorArgument> MotorStarted;
        public EventHandler<MotorArgument> MotorStopped;
        public EventHandler ZeroSeated;

        public bool Manual { get; set; }

        public double SPVel {
			get {
				return _SPVel;
			}
			set {
				_SPVel = value;
                SPVelChanged?.Invoke(this,EventArgs.Empty);
            }
		}
        public double SPVelManual {
			get {
				return _SPVelManual;
			}
			set {
				_SPVelManual = value;
				SPVelManualChanged?.Invoke(this,EventArgs.Empty);
			}
		}

		public Motor() {
			Manual=false;
		}

		public double Posicao {get;set;}

		public ModoMotor ModoMotor {
			get {
				return _modoOper;
			}
		}

        public void Start(ModoMotor modo) {
            MotorArgument args = new MotorArgument();
			args.Modo = modo;
			args.Vel=_SPVel;
            MotorStarted?.Invoke(this,args);
        }

		public void StartManual(ModoMotor modo) {
            MotorArgument args = new MotorArgument();
            args.Modo=modo;
            args.Vel=_SPVelManual;
            MotorStarted?.Invoke(this,args);
		}

		public void StartSetZero(ModoMotor modo) {
			MotorArgument args=new MotorArgument();
			args.Modo=modo;
			args.Vel=_SPVel/5; //Rapaz, tá certo isso? 
			MotorStarted?.Invoke(this,args);
		}

		public void Stop() {
            MotorArgument args = new MotorArgument();
            args.Modo=ModoMotor.Parado;
            args.Vel=0;
            MotorStopped?.Invoke(this,args);
		}

        public void ChangeState(ModoMotor modo) {
			_modoOper = modo;
            EstadoMotorChanged?.Invoke(this, EventArgs.Empty);
	    }


	}

}

