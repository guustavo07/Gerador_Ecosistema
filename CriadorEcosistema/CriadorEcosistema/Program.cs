using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;

namespace CriadorEcosistema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe a Empresa: ");
            string empresa = Console.ReadLine();
            Console.WriteLine("Informe a Unidade: ");
            string unidade = Console.ReadLine();

            string caminhoRaiz = "C:\\Exemplo\\Exemplo";
            int aux = caminhoRaiz.LastIndexOf('\\');
            string sigla = "";
            List<string> listAux = new List<string>();
            if (Directory.Exists(caminhoRaiz))
            {
                List<string> listSiglas = new List<string>();

                string[] pastas = Directory.GetDirectories(caminhoRaiz);
                foreach (string pasta in pastas)
                {
                    string nomePasta = Path.GetFileName(pasta);

                    listSiglas.Add(nomePasta);                    
                }

                List<string> listCaminhos = new List<string>();
                List<string> listComponentes = new List<string>();
                for (int i = 0; i < listSiglas.Count; i++)
                {
                   string caminho = "C:\\Exemplo\\Exemplo";
                    caminho = Path.Combine(caminho, pastas[i]);
                    string[] subPastas = Directory.GetDirectories(caminho);
                    

                    foreach (string subPasta in subPastas)
                    {
                        string nmCaminho = subPasta;
                        listCaminhos.Add(nmCaminho);

                        string nomeComponente = Path.GetFileName(subPasta);

                        listComponentes.Add(nomeComponente);
                    }

                }

                if(listComponentes != null)
                {
                    StringBuilder sb = new StringBuilder();


                    sb.AppendLine("Empresa;Unidade;Sigla;Sistema;Caminho_Repositorio;Linguagem;URL_Repositorio");
                    
                    foreach (var lsComponentes in listComponentes)
                    {
                        
                        foreach (var lsCaminho in listCaminhos)
                        {
                           
                            if (!listAux.Contains(lsCaminho)) 
                            {
                                foreach (var lsSigla in listSiglas)
                                {
                                    if (lsCaminho.Contains(lsSigla))
                                    {
                                        //Java
                                        string regexLinguagemJava = @"\b(Sboot|Springboot|Springbath|Java|Sbatch|Javabatch)\b";
                                        bool contemPalavra = Regex.IsMatch(lsComponentes, regexLinguagemJava, RegexOptions.IgnoreCase);
                                        //.net
                                        bool possuiArquivoSln = Directory.GetFiles(caminhoRaiz, "*.sln").Length > 0;


                                        int linguagem = 0;
                                        if (contemPalavra)
                                        {
                                            linguagem = 4;
                                            sb.AppendLine($"{empresa};{unidade};{lsSigla};{lsComponentes};{lsCaminho};{linguagem};");
                                            listAux.Add(lsCaminho);
                                            break;
                                        }
                                        else if(possuiArquivoSln)
                                        {
                                            linguagem = 1;
                                            sb.AppendLine($"{empresa};{unidade};{lsSigla};{lsComponentes};{lsCaminho};{linguagem};");
                                            listAux.Add(lsCaminho);
                                            break;
                                        }
                                        else if (lsComponentes.StartsWith("db-"))
                                        {
                                            linguagem = 7;
                                            sb.AppendLine($"{empresa};{unidade};{lsSigla};{lsComponentes};{lsCaminho};{linguagem};");
                                            listAux.Add(lsCaminho);
                                            break;
                                        }
                                        else
                                        {
                                            linguagem = 0;
                                            sb.AppendLine($"{empresa};{unidade};{lsSigla};{lsComponentes};{lsCaminho};{linguagem};");
                                            listAux.Add(lsCaminho);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                       foreach (string lSiglas in listSiglas) 
                                        {
                                            break;
                                        }
                                    }
                                }
                                break;
                            }                            
                        }
                    }
                    
                    
                    if (aux != -1 && aux < caminhoRaiz.Length - 1)
                    {
                        sigla = caminhoRaiz.Substring(aux + 1);

                        var index = sigla.LastIndexOf("\\");
                        if (index != -1)
                        {
                            sigla = sigla.Substring(0, index);
                        }
                    }
                    using (var stream = new StreamWriter($"C:\\Temp\\EcoSistema{sigla}.csv", false, Encoding.UTF8))
                    {
                        stream.Write(sb.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("O diretório especificado não existe.");
            }



        }
    }
}