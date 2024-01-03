using System;
using System.Collections.Generic;

namespace LoadCellTexturometro {
	public class LoadCell {

        private double _cargaMax;
        private double _val;
        private const double _g = 9.81;
        private const double _valDeteccao = 0;

        public EventHandler ZeroSeated;
        public EventHandler LoadLimitreached;
        public EventHandler CargaDetected;

        public LoadCell(double valorMax) {
            _cargaMax=valorMax;
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

        public void SetZero() {
            ZeroSeated?.Invoke(this,EventArgs.Empty);
        }

    }

}