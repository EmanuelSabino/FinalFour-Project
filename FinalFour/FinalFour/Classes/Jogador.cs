using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    public enum Nacionalidades
    {
        Argentino = 0,
        Alemão = 1,
        Australiano = 2,
        Americano = 3,
        Brasileiro = 4,
        Chinês = 5,
        Japonês = 6,
        Espanhol = 7,
        Islandês = 8,
        Português = 9,
        Outro = 10
    }

    enum TipoPosicao
    {
        Guarda_Redes = 0,
        Defesa = 1,
        Meio_Campo = 2,
        Atacante = 3
    }
    
    class Jogador : Elemento
    {
        private TipoPosicao posicaoJogador;

        public TipoPosicao PosicaoJogador
        {
            get { return posicaoJogador; }
            set { posicaoJogador = value; }
        }

        private string nomeJogadorCamisola;

        public string NomeJogadorCamisola
        {
            get { return nomeJogadorCamisola; }
            set
            {
                if (value.Length < 11 && value.Length > 0 && value.Split(' ').Length <= 2)
                {
                    nomeJogadorCamisola = value;
                }
                else
                {
                    throw new Exception("Nome da camisola inválido do jogador " + Nome + ", nome da camisola tem de ter pelo menos 10 letras");
                }
            }
        }

        private int numDaCamisola;

        public int NumDaCamisola
        {
            get { return numDaCamisola; }
            set
            {
                if (value < 0 || value > 99)
                {
                    throw new Exception("Número da camisola inválida do jogador " + Nome + " tem de estar entre 1-99");
                }
                else
                {
                    numDaCamisola = value;
                }
            }
        }

        private double peso;

        public double Peso
        {
            get { return peso; }
            set
            {
                if (value < 35 || value > 60)
                {
                    throw new Exception("Peso inválido do jogador " + Nome + " tem de estar entre 35-60");
                }
                else
                {
                    peso = Math.Round(value, 2);
                }
            }
        }

        private DateTime inicioContrato;

        public DateTime InicioContrato
        {
            get { return inicioContrato; }
            set
            {
                if (value > DateTime.Now || (DateTime.Now - DataNascimento).Days / 365 > 40 || (DateTime.Now - DataNascimento).Days / 365 < 17)
                {
                    throw new Exception("Data de inicio contrato inválida do jogador " + Nome + " ,não pode ser maior que o dia de hoje\n" +
                        " e tem de ser entre 17-90");
                }
                else
                {
                    inicioContrato = value;
                }
            }
        }

        private DateTime fimContrato;

        public DateTime FimContrato
        {
            get { return fimContrato; }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new Exception("Data de fim de contrato inválido do jogador " + Nome + " ,tem de ser superior ao dia de hoje");
                }
                else
                {
                    fimContrato = value;
                }
            }
        }

        private Nacionalidades nacionalidadeDoJogador;

        public Nacionalidades NacionalidadeDoJogador
        {
            get { return nacionalidadeDoJogador; }
            set { nacionalidadeDoJogador = value; }
        }

        private ClubeDeFutebol clubeJogador;

        public ClubeDeFutebol ClubeJogador
        {
            get { return clubeJogador; }
            set { clubeJogador = value; }
        }

        public Jogador(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue, double salarioValue
            , string nomeCamisolaValue, int numCamisolaValue, TipoPosicao posicaoJogadorValue, double pesoValue, DateTime inicioContratoValue, DateTime fimContratoValue,
            Nacionalidades nacionalidadeJogadorValue, ClubeDeFutebol clubeJogadorValue) : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 40) throw new Exception("Fim de contrato inválido do jogador " + Nome + " não pode exceder os 40 anos");
            NomeJogadorCamisola = nomeCamisolaValue;
            NumDaCamisola = numCamisolaValue;
            Peso = pesoValue;
            InicioContrato = inicioContratoValue;
            FimContrato = fimContratoValue;
            NacionalidadeDoJogador = nacionalidadeJogadorValue;
            PosicaoJogador = posicaoJogadorValue;
            ClubeJogador = clubeJogadorValue;
        }

        public Jogador(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue, double salarioValue
            , string nomeCamisolaValue, int numCamisolaValue, TipoPosicao posicaoJogadorValue, double pesoValue, DateTime inicioContratoValue, DateTime fimContratoValue,
            Nacionalidades nacionalidadeJogadorValue) : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 40) throw new Exception("Fim de contrato inválido do jogador " + Nome + " não pode exceder os 40 anos");
            NomeJogadorCamisola = nomeCamisolaValue;
            NumDaCamisola = numCamisolaValue;
            Peso = pesoValue;
            InicioContrato = inicioContratoValue;
            FimContrato = fimContratoValue;
            NacionalidadeDoJogador = nacionalidadeJogadorValue;
            PosicaoJogador = posicaoJogadorValue;
        }

        public override string ToString()
        {
            return NomeJogadorCamisola + "  --  " + NacionalidadeDoJogador;
        }
    }
}
