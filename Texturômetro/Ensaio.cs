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
            } else {
                integrager++;
            }
        }

    }
    public class EnsaioCompressao : Ensaio{

        public EnsaioCompressao(){
            Tipo=TipoDeTeste.Compressao;
            Acoes=new List<Acao> {
                Acao.DescerPreTeste,
                Acao.DescerTeste,
                Acao.SubirTeste,
                Acao.SubirPosTeste,
                Acao.Fim
            };
        }
    }
    public class EnsaioTracao : Ensaio {
            public EnsaioTracao() {
            Tipo=TipoDeTeste.Tracao;
            Acoes=new List<Acao> {
                Acao.SubirPreTeste,
                Acao.SubirTeste,
                Acao.DescerTeste,
                Acao.DescerPosTeste,
                Acao.Fim
            };
        }
    }
    public class EnsaioTPA : Ensaio {
        public EnsaioTPA() {
            Tipo=TipoDeTeste.TPA;
            Acoes=new List<Acao> {
                Acao.DescerPreTeste,
                Acao.DescerTeste,
                Acao.SubirTeste,
                Acao.EsperarAssentamento,
                Acao.DescerTeste,
                Acao.SubirTeste,
                Acao.SubirPosTeste,
                Acao.Fim
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
                    teste=new EnsaioTPA();
                    break;
                default:
                    teste=null;
                    break;
            }
            return teste;
        }
    }
}