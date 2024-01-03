using System;
using System.Collections.Generic;

namespace ClassesSuporteTexturometro {

    #region Enums
    public enum Acao {
        Parar,
        Descer,
        Subir
    };
    public enum TipoDeTeste {
        Compressao,
        Tracao,
        CicloDuploCompressao,
        CicloDuploTracao
    };

    public enum ModoMotor {
        Parado,
        Subir,
        Descer
    };

    public enum TipoLimite {
        Deformacao,
        Forca,
        Tempo
    }

    public enum TipoProbe{
        Circular,
        Retangular,
        Angular,
        Cisalhamento
    }

    #endregion
    public class MotorArgument : EventArgs {
        public ModoMotor Modo { get; set; }
        public double Vel { get; set; }
    }

    public class SerialMessageArgument : EventArgs {
        public string Objeto { get; set;}
        public string Comando { get; set;}
        public double doubleValue { get; set;}
        public bool boolValue { get; set;}
        public int intValue { get; set;}
    }
    

    public class Chave {
        private bool _state;
        public EventHandler ValueChanged;

        public Chave() {
            _state=false;
        }

        public bool Estado {
            get {
                return _state;
            }
            set {
                _state=value;
                ValueChanged?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public class Coord {
        public double X { get; }
        public double Y { get;}
        public double Z { get;}

        public Coord() {
            X=0;
            Y=0;
            Z=0;
        }
        public Coord(double X,double Y, double Z) {
            this.X=X;
            this.Y=Y;
            this.Z=Z;
        }
    }
    public class Tabela {
        private List<Coord> _table;
        public Tabela()
        {
            _table = new List<Coord>();
        }
        public void Add(double x,double y, double z) {
            _table.Add(new Coord(x,y,z));
        }

        public void Add(Coord xyz) {
            _table.Add(xyz);
        }

        public List<double> GetXvalues() {
            List<double> list = new List<double>();
            foreach(var x in _table) {
                list.Add(x.X);
            }
            return list;
        }
        public List<double> GetYvalues() {
            List<double> list = new List<double>();
            foreach(var y in _table) {
                list.Add(y.Y);
            }
            return list;
        }
        public List<double> GetZvalues() {
            List<double> list = new List<double>();
            foreach(var z in _table) {
                list.Add(z.Z);
            }
            return list;
        }

        public List<Coord> GetTable()
        {
            return _table;
        }
    }

    
}