using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    class ClubeDeFutebol
    {
        private int divicaoClube;

        public int DivicaoClube
        {
            get { return divicaoClube; }
            set 
            {
                if(value < 0 || value > 3)
                {
                    throw new Exception("Divisão de clube inválido!!");
                }
                else
                {
                    divicaoClube = value;
                }
            }
        }

        private string nomeClube;

        public string NomeClube
        {
            get { return nomeClube; }
            set
            {
                if (value.Trim() == string.Empty)
                {
                    throw new Exception("Nome do clube inválido, o nome não pode estar em branco");
                }
                else
                {
                    string[] verificarNome = value.Split(' ');
                    string nomeDireito = "";
                    for (int palavra = 0; palavra < verificarNome.Length; palavra++)
                    {
                        for (int letra = 0; letra < verificarNome[palavra].Length; letra++)
                        {
                            if (!Char.IsLetter(verificarNome[palavra][letra]))
                            {
                                throw new Exception("O nome do clube só pode ter letras e pontos");
                            }
                            if (letra == 0)
                            {
                                nomeDireito += verificarNome[palavra][letra].ToString().ToUpper();
                            }
                            else
                            {
                                nomeDireito += verificarNome[palavra][letra].ToString().ToLower();
                            }
                        }
                        nomeDireito += " ";
                    }
                    nomeClube = nomeDireito.Trim();
                }
            }
        }

        private DateTime dataDeFundacao;

        public DateTime DataDeFundacao
        {
            get { return dataDeFundacao; }
            set
            {
                if (value > DateTime.Now || value.Year < 1880 || DateTime.Now.Year - value.Year < 1)
                {
                    throw new Exception("Data de fundação invalida do clube " + NomeClube);
                }
                else
                {
                    dataDeFundacao = value;
                }
            }
        }

        private string nomeDoEstadio;

        public string NomeDoEstadio
        {
            get { return nomeDoEstadio; }
            set
            {
                if (value.Trim() == string.Empty)
                {
                    throw new Exception("Nome do estadio inválido, o nome do Estadio não pode estar em branco");
                }
                else
                {
                    string[] verificarNome = value.Split(' ');
                    string nomeDireito = "";
                    for (int palavra = 0; palavra < verificarNome.Length; palavra++)
                    {
                        for (int letra = 0; letra < verificarNome[palavra].Length; letra++)
                        {
                            if (!Char.IsLetter(verificarNome[palavra][letra]))
                            {
                                throw new Exception("O nome do clube só pode ter letras e pontos");
                            }
                            if (letra == 0)
                            {
                                nomeDireito += verificarNome[palavra][letra].ToString().ToUpper();
                            }
                            else
                            {
                                nomeDireito += verificarNome[palavra][letra].ToString().ToLower();
                            }
                        }
                        nomeDireito += " ";
                    }
                    nomeDoEstadio = nomeDireito.Trim();
                }
            }
        }

        public ClubeDeFutebol(string nomeClubeValue, DateTime dataFundacaoValue, string nomeEstadioValue, int divisaoClubeValue)
        {
            NomeClube = nomeClubeValue;
            DataDeFundacao = dataFundacaoValue;
            NomeDoEstadio = nomeEstadioValue;
            DivicaoClube = divisaoClubeValue;
        }

        public override string ToString()
        {
            return "Nome do Clube: " + NomeClube + "\nNome do Estadio: " + NomeDoEstadio + "\nData de Fundação: " + DataDeFundacao.ToShortDateString()
                + "\nDivisão do Clube: " + DivicaoClube;
        }
    }
}
