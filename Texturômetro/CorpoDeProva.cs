using ClassesSuporteTexturometro;
using System;

namespace ProdutoTexturometro {

    public class CorpoDeProva {
        private double _tamanhoAtual = 0;
        private double _targetDeformation = 0;
        private bool _targetedDeformation = false;
        private double _deformacaoValue;

        public Tabela Resultado { get; set; } = new Tabela();
        public double TamanhoOriginal { get; set; }
        public double TamanhoRecuperacao { get; set; } 
        
        public event EventHandler DeformacaoReached;

        public double TamanhoAtual {
            get {
                return _tamanhoAtual;
            }
            set {
                _tamanhoAtual=value;
                if(TamanhoOriginal>0&&_tamanhoAtual<TamanhoOriginal) {
                    _deformacao=(TamanhoOriginal-_tamanhoAtual)/TamanhoOriginal;
                } else {
                    _deformacao=0;
                }
            } 
        }
        
        private double _deformacao {
            get {
                return _deformacaoValue;
;
            }
            set {
                _deformacaoValue=value;
                if(_targetDeformation<=_deformacaoValue&&_targetedDeformation) {
                    _targetedDeformation= false;    
                    DeformacaoReached?.Invoke(this,EventArgs.Empty);
                }
            }
        }

        public double Deformacao {
            get { return _deformacao; }
        }

        public CorpoDeProva() {
            TamanhoOriginal=0;
            _deformacao=0;
        }

        public void TargetDeformation(double deformacaoAlvo = double.NaN) {
            if(deformacaoAlvo!=0||deformacaoAlvo!=double.NaN) {
                _targetedDeformation=true;
                _targetDeformation=deformacaoAlvo;
            } else {
                _targetedDeformation=false;
                _targetDeformation=0;
            }
        }
    }
}
