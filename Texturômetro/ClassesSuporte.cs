using System;
using System.Collections.Generic;
using System.Threading;

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
        public double FinalPosition { get; set; }
    }

    public class SerialMessageArgument : EventArgs {
        public string Objeto { get; set;}
        public string Comando { get; set;}
        public double doubleValue { get; set;}
        public double doubleValue1 { get; set; }
        public double doubleValue2 { get; set; }
        public bool boolValue { get; set;}
        public int intValue { get; set;}
        public string stringValue { get; set;}
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
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

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
        public int Count{
            get {
                return _table.Count;
            }
        }
        public void Add(Coord xyz) {
            _table.Add(xyz);
        }

        public void Clear() { 
            _table.Clear();
        }

        public List<double> GetXvalues() {
            List<double> list = new List<double>();
            try {
                foreach(var x in _table) {
                    if(x!=null)list.Add(x.X);
                }
            } finally { }
            return list;
        }
        public List<double> GetYvalues() {
            List<double> list = new List<double>();
            try {
                foreach(var y in _table) {
                   if(y!=null) list.Add(y.Y);
                }
             }finally { }
            return list;
        }
        public List<double> GetZvalues() {
            List<double> list = new List<double>();
            try {
                foreach(var z in _table) {
                    if(z!=null)list.Add(z.Z);
                }
            } finally { }
            return list;
        }

        public List<Coord> GetTable()
        {
            return _table;
        }

        public void AddXZvalue(double X, double Z) {
            int i = _table.FindIndex(lista => lista.Z==Z && lista.X==0);
            if(i!=-1) {
                _table[i].X=X;
            } else {
                Add(X,0,Z);
            }
        }
        public void AddYZvalue(double Y,double Z) {
            int i = _table.FindIndex(lista => lista.Z==Z&&lista.Y==0);
            if(i!=-1) {
                _table[i].Y=Y;
            } else {
                Add(0,Y,Z);
            }
        }
    }

    
}