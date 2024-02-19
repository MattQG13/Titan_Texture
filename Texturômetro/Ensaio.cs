using ClassesSuporteTexturometro;
using System.Collections.Generic;
using ProbeTexturometro;
using ProdutoTexturometro;
using System;

namespace EnsaioTextuometro {

    public abstract class Ensaio {
        public TipoDeTeste Tipo { get; set; }
        public List<Acao> Acoes { get; set; }
        private int integrager = 0;


        public Acao AcaoAtual {
            get {
                return Acoes[integrager];
            }
        }

        public void Next() {
            if(integrager>=Acoes.Count) {
                integrager=0;
            }
            integrager++;
        }

    }
    public class EnsaioCompressao : Ensaio{

        public EnsaioCompressao(){
            Tipo=TipoDeTeste.Compressao;
            Acoes = new List<Acao> {
                Acao.Descer,
                Acao.Parar,
                Acao.Subir
            };
        }
    }
    public class EnsaioTracao : Ensaio {
            public EnsaioTracao() {
            Tipo=TipoDeTeste.Tracao;
            Acoes=new List<Acao> {
                Acao.Subir,
                Acao.Parar,
                Acao.Descer,
                Acao.Parar,
            };
        }
    }
    public class EnsaioCicloCompressao : Ensaio {
        public EnsaioCicloCompressao() {
            Tipo=TipoDeTeste.TPA;
            Acoes=new List<Acao> {
                Acao.Descer,
                Acao.Parar,
                Acao.Subir,
                Acao.Parar,
                Acao.Descer,
                Acao.Subir,
                Acao.Parar
            };
        }
    }


    public sealed class EnsaioFactoryMethod {
        public static Ensaio criarTeste(TipoDeTeste tipo) {
            Ensaio teste;
            switch(tipo) {
                case TipoDeTeste.Compressao:
                    teste=new EnsaioCompressao();
                    break;
                case TipoDeTeste.Tracao:
                    teste=new EnsaioTracao();
                    break;
                case TipoDeTeste.TPA:
                    teste=new EnsaioCicloCompressao();
                    break;
                default:
                    teste=null;
                    break;
            }
            return teste;
        }
    }
}