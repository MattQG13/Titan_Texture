using ClassesSuporteTexturometro;

namespace ProdutoTexturometro {

    public class CorpoDeProva {
        private double _deformacaoLinear;
        private double _deformacao;
        public double DeformacaoLinear {
            get { 
                return _deformacaoLinear;
            }
            set {
                _deformacaoLinear = value;
                _deformacao= _deformacaoLinear / Altura;
            }
        }

        public double Deformacao {
            get { return _deformacao; }
        }

        public double Altura { get; set; }
        public Tabela Resultado { get; set; }

        public CorpoDeProva(double Altura)
        {
            this.Altura = Altura;
        }
    }
}
