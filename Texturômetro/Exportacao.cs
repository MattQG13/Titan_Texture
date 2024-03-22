using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using CsvHelper;
using ProdutoTexturometro;
using ClassesSuporteTexturometro;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.Win32;
using System.IO.Packaging;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EnsaioTextuometro;
using System.Collections.Generic;
using System.Linq;
using DadosDeEnsaio;
using Image = System.Drawing.Image;

namespace ExportacaoResultado {
    public class ExportacaoExcel {


        public ExportacaoExcel() {
            
        }

        public static void exportarExcel(in List<Coord> cp) {
            ExcelPackage.LicenseContext=LicenseContext.NonCommercial;

            //var cp=corpoDeProva.Resultado.GetTable();
            //for(int i = 0; i < 10;i++)
            //cp.Add(new Coord(1*i,2*i,3*i));
            var dados = cp;
            
           
            using(var package = new ExcelPackage()) {

                SaveFileDialog SalvarArquivo = new SaveFileDialog();
                SalvarArquivo.Filter="Excel Files|*.xlsx";
                SalvarArquivo.DefaultExt="*.xlsx";
                SalvarArquivo.Title="Exportar Dados";

                var worksheet = package.Workbook.Worksheets.Add("Dados do Ensaio");

                worksheet.Cells[1,1].Value="Tempo (s)";
                worksheet.Cells[1,2].Value="Carga (g)";
                worksheet.Cells[1,3].Value="Posicao (mm)";

                int linha = 2;

                foreach(var row in dados) {
                    worksheet.Cells[linha,1].Value=row.Z;
                    worksheet.Cells[linha,2].Value=row.X;
                    worksheet.Cells[linha,3].Value=row.Y;

                    linha++;
                }
                var res = SalvarArquivo.ShowDialog();
                
                if (res == DialogResult.OK) {
                    FileInfo file = new FileInfo(SalvarArquivo.FileName);
                    package.SaveAs(file);
                }
                
            }
        }
    }


    public class ExportacaoRelatorioPDF {
        public ExportacaoRelatorioPDF() { }


        public static void exportaPDF(in CorpoDeProva corpoDeProva,in DataTest tipoDeTeste,Image image) {

        }
    }

    public class ExportacaoCSV {
        public ExportacaoCSV() { 
        
        }

        public static void exportarCSV(in List<Coord> cp) {

            //Tabela cp = new Tabela();

            //var cp =corpoDeProva.Resultado.GetTable();

            var dados = cp;

            var config = new CsvConfiguration(CultureInfo.CurrentCulture);
            config.HasHeaderRecord=true;

            SaveFileDialog SalvarArquivo = new SaveFileDialog();
            SalvarArquivo.Filter="CSV Files|*.csv";
            SalvarArquivo.DefaultExt="*.csv";
            SalvarArquivo.Title="Gerar arquivo de cadastro";

            var res = SalvarArquivo.ShowDialog();

            if(res==DialogResult.OK) {
                using(var writer = new StreamWriter(SalvarArquivo.FileName,false,Encoding.Default))
                using(var csv = new CsvWriter(writer,config)) {
                    // Escreve o cabeçalho

                    csv.WriteField("Tempo (s)");
                    csv.WriteField("Carga (g)");
                    csv.WriteField("Posicao (mm)");
                    csv.NextRecord();

                    // Escreve os dados
                    foreach(var dado in dados) {
                        csv.WriteField(dado.Z);
                        csv.WriteField(dado.X);
                        csv.WriteField(dado.Y);

                        csv.NextRecord();
                    }

                }
            }

        }
    }
}
