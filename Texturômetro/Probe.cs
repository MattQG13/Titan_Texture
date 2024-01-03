//Implementar Factory Method e tipos de probes
using ClassesSuporteTexturometro;
using System;

namespace ProbeTexturometro {
    public abstract class Probe {
        
        public TipoProbe Tipo { get; set; }
        public Probe() {
        }
        
    }

    public class ProbeCircular : Probe {
        public double Diametro { get; set; }
        public double Area { get; set; }
        public ProbeCircular (double Diametro){
            Tipo = TipoProbe.Circular;
            this.Diametro = Diametro;
            Area = (Math.PI*Diametro*Diametro)/4;
        }
    }

    public class ProbeRetangular : Probe
    { 
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Area { get; set; }
        public ProbeRetangular(double Largura, double Comprimento)
        {
            Tipo = TipoProbe.Retangular;
            this.Largura = Largura;
            this.Comprimento = Comprimento;
            Area = Largura*Comprimento;
        }
    }

    public class ProbeCisalhamento : Probe
    {
       public ProbeCisalhamento()
       {
            Tipo = TipoProbe.Cisalhamento;
        }
    }

    public sealed class ProbeFactoryMethod{
        public static Probe ProbeCreate()
        {
            return new ProbeCisalhamento();
        }
        public static Probe ProbeCreate(double Diametro) {
            return new ProbeCircular(Diametro);
        }
        public static Probe ProbeCreate (double Largura, double Comprimento)
        {
            return new ProbeRetangular(Largura, Comprimento);
        }
    }
}
