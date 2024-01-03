using ClassesSuporteTexturometro;
using ProbeTexturometro;



namespace DadosDeEnsaio
{
    public class DataTest {
        public TipoDeTeste Tipo { get; set; }
        public Probe PontaDeTeste { get; set; }
        public double VelMotor { get; set; }
        public double VelMotorManual { get; set; }
        public TipoLimite Limite { get; set; }
        public double LoadCellValMax { get;set;}
    }
}
