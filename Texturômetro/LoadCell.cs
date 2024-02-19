using System;
using System.Collections.Generic;
using ClassesSuporteTexturometro;

namespace LoadCellTexturometro {
	public class LoadCell {

        private double _cargaMax;
        private double _val;
        private const double _g = 9.81;
        public double _valDeteccao;

        public EventHandler ZeroSet;
        public EventHandler LoadLimitreached;
        public EventHandler CargaDetected;
        public EventHandler<SerialMessageArgument> Calibration;
        public EventHandler ZerarTime;

        public LoadCell(double valorMax) {
            _cargaMax=valorMax;
        }

        public LoadCell() {
        }

        public bool CargaLimitada { get; set; }
        public double CargaLimite { get; set; }
        public double Scale { get; set; }

        public double ValorLoad {
            get {
                return _val;
            }
            set {
                _val=(double)value;
                if(_val>_valDeteccao) {
                    CargaDetected?.Invoke(this,EventArgs.Empty);
                }
                if(_val>=CargaLimite&&CargaLimitada) {
                    LoadLimitreached?.Invoke(this,EventArgs.Empty);
                }
            }
        }



        public double cargaKg {
            get {
                return _val/1000d;
            }
        }

        public double cargaG {
            get {
                return _val;
            }
        }

        public double cargaN {
            get {
                return (cargaKg*_g);
            }
        }

        public void Tarar() {
            ZeroSet?.Invoke(this,EventArgs.Empty);
        }

        public void Cal(double loadCal) {
            SerialMessageArgument args = new SerialMessageArgument();
            args.doubleValue=loadCal;
            Calibration?.Invoke(this,args);
        }
        public void ZeroTime() {
            ZerarTime?.Invoke(this,EventArgs.Empty);
        }
    }

}