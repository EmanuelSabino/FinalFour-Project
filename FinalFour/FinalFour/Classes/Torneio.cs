using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    enum Paises
    {
        Argentina,
        Alemanha,
        Áustria,
        América,
        Brasil,
        China,
        Japão,
        Espanha,
        Irlanda,
        Portugal,
        Inglaterra,
        Outro
    }

    class Torneio
    {
        private Jogo primeiroJogo;

        public Jogo PrimeiroJogo
        {
            get { return primeiroJogo; }
            set { primeiroJogo = value; }
        }

        private Jogo segundoJogo;

        public Jogo SegundoJogo
        {
            get { return segundoJogo; }
            set { segundoJogo = value; }
        }

        private Jogo jogoFinal;

        public Jogo JogoFinal
        {
            get { return jogoFinal; }
            set { jogoFinal = value; }
        }

        private DateTime inicioDoTorneio;

        public DateTime InicioDoTorneio
        {
            get { return inicioDoTorneio; }
            set
            {
                if (value > DateTime.Now) throw new Exception("Torneio inválido, não pode ser superior ao dia de hoje");
                else { inicioDoTorneio = value; }
            }
        }

        private ClubeDeFutebol clubeVencedor;

        public ClubeDeFutebol ClubeVencedor
        {
            get { return clubeVencedor; }
            set { clubeVencedor = value; }
        }

        private Paises paisDoTorneio;

        public Paises PaisDoTorneio
        {
            get { return paisDoTorneio; }
            set { paisDoTorneio = value; }
        }

        private double premio;

        public double Premio
        {
            get { return premio; }
            set
            {
                if (value < 500) throw new Exception("O premio do torneio tem de ser válido e não pode ser negativo");
                else premio = value;
            }
        }


        public Torneio(Jogo primeiroJogoValue, Jogo segundoJogoValue, Jogo ultimoJogoValue, DateTime inicioTorneioValue,
            ClubeDeFutebol clubeVencedorValue, Paises paisTorneioValue, double premioValue)
        {
            PrimeiroJogo = primeiroJogoValue;
            SegundoJogo = segundoJogoValue;
            JogoFinal = ultimoJogoValue;
            InicioDoTorneio = inicioTorneioValue;
            ClubeVencedor = clubeVencedorValue;
            PaisDoTorneio = paisTorneioValue;
            Premio = premioValue;
        }

        public bool VerificarTorneio(Jogo primeiroJogo, Jogo segundoJogo, Jogo ultimoJogoValue, DateTime inicioTorneioValue, ClubeDeFutebol
            clubeVencedorValue)
        {
            if (primeiroJogo.ResultClubeDaCasa == primeiroJogo.ResultClubeVisitente || ultimoJogoValue.ResultClubeDaCasa == 
                ultimoJogoValue.ResultClubeVisitente || segundoJogo.ResultClubeDaCasa == segundoJogo.ResultClubeVisitente)
            {
                throw new Exception("Resultados inválidos não pode haver empate");
            }
            if (primeiroJogo.ClubeDaCasa.DataDeFundacao > inicioTorneioValue || primeiroJogo.ClubeVisitente.DataDeFundacao > inicioTorneioValue
                || segundoJogo.ClubeDaCasa.DataDeFundacao > inicioTorneioValue || segundoJogo.ClubeVisitente.DataDeFundacao > inicioTorneioValue)
            {
                throw new Exception("Torneio inválido, um dos clubes ainda não foi construido nessa epoca de torneio");
            }
            if ((DateTime.Now - inicioTorneioValue).Days / 365 < 1) throw new Exception("Torneio inválido não pode ser a menos de um ano");
            if(clubeVencedorValue.NomeClube != ultimoJogoValue.ClubeDaCasa.NomeClube && ultimoJogoValue.ResultClubeDaCasa > ultimoJogoValue.ResultClubeVisitente ||
                clubeVencedorValue.NomeClube != ultimoJogoValue.ClubeVisitente.NomeClube && ultimoJogoValue.ResultClubeDaCasa < ultimoJogoValue.ResultClubeVisitente)
            {
                throw new Exception("Clube vencedor inválido");
            }
                if (segundoJogo.InicioDoJogo <= inicioTorneioValue || segundoJogo.InicioDoJogo <= inicioTorneioValue ||
                ultimoJogoValue.InicioDoJogo <= inicioTorneioValue)
            {
                throw new Exception("Jogos inválidos, o inicio do Jogo não podera ser inferior ao inicio do Torneio");
            }

            else if (primeiroJogo.InicioDoJogo.Year > inicioTorneioValue.Year + 2 ||
                segundoJogo.InicioDoJogo.Year > inicioTorneioValue.Year + 2 ||
                ultimoJogoValue.InicioDoJogo.Year > inicioTorneioValue.Year + 2)
            {
                throw new Exception("Jogos inválidos, o inicio do Jogo não podera exceder 2 anos desde que o Torneio se iniciou");
            }

            else if (primeiroJogo.InicioDoJogo >= segundoJogo.InicioDoJogo || primeiroJogo.InicioDoJogo >= ultimoJogoValue.InicioDoJogo
                || segundoJogo.InicioDoJogo >= ultimoJogoValue.InicioDoJogo)
            {
                throw new Exception("Datas dos jogos inválidas, a ordem dos jogos tem de ser, primeiro Jogo, segundo Jogo e por fim" +
                    " Jogo final");
            }
            else if (primeiroJogo.ClubeDaCasa != segundoJogo.ClubeDaCasa || primeiroJogo.ClubeDaCasa != segundoJogo.ClubeVisitente ||
                primeiroJogo.ClubeVisitente != segundoJogo.ClubeDaCasa || primeiroJogo.ClubeVisitente != segundoJogo.ClubeVisitente)
            {
                return true;
            }
            else throw new Exception("Não pode haver clubes repetidos no primeiro e segundo jogo");
        }

        public override string ToString()
        {
            return "Inicio Do Torneio: " + InicioDoTorneio.ToShortDateString() + "\nClube Vencedor: " + ClubeVencedor.NomeClube + "\nPais do Torneio: " +
                PaisDoTorneio + "\nPremio: " + Premio + "€\n\nClubes Participantes: \n" + PrimeiroJogo.ClubeDaCasa.NomeClube + "\n" +
                PrimeiroJogo.ClubeVisitente.NomeClube + "\n" + SegundoJogo.ClubeVisitente.NomeClube + "\n" + segundoJogo.ClubeDaCasa.NomeClube;
        }
    }
}
