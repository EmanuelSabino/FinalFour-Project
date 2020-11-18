using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    class Jogo
    {
        private ClubeDeFutebol clubeDaCasa;

        public ClubeDeFutebol ClubeDaCasa
        {
            get { return clubeDaCasa; }
            set
            {
                clubeDaCasa = value;
            }
        }

        private ClubeDeFutebol clubeVisitente;

        public ClubeDeFutebol ClubeVisitente
        {
            get { return clubeVisitente; }
            set
            {
                clubeVisitente = value;
            }
        }

        private DateTime inicioDoJogo;

        public DateTime InicioDoJogo
        {
            get { return inicioDoJogo; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new Exception("Data inválida do jogo não pode ser superior ao dia de hoje");
                }
                else
                {
                    inicioDoJogo = value;
                }
            }
        }

        private int resultClubeDaCasa;

        public int ResultClubeDaCasa
        {
            get { return resultClubeDaCasa; }
            set
            {
                if (value > 25 || value < 0)
                {
                    throw new Exception("Resultado da equipa da casa inválido não pode ser menor que 0");
                }
                else
                {
                    resultClubeDaCasa = value;
                }
            }
        }

        private int resultClubeVisitente;

        public int ResultClubeVisitente
        {
            get { return resultClubeVisitente; }
            set
            {
                if (value > 25 || value < 0)
                {
                    throw new Exception("Resultado da equipa visitente inválido não pode ser menor que 0");
                }
                else
                {
                    resultClubeVisitente = value;
                }
            }
        }

        private string estadioDoJogo;

        public string EstadioDoJogo
        {
            get { return estadioDoJogo; }
            set { estadioDoJogo = clubeDaCasa.NomeDoEstadio; }
        }

        public Jogo(ClubeDeFutebol clubeDaCasaValue, ClubeDeFutebol clubeVisitenteValue, DateTime iniciodoJogoValue, int resultClubCasaValue
            , int resultClubeVisitentValue)
        {
            if (clubeDaCasaValue.DataDeFundacao > iniciodoJogoValue || clubeVisitenteValue.DataDeFundacao > iniciodoJogoValue) throw new Exception("" +
                 "Esse jogo não pode acontecer, um dos clubes ainda não foi construido");
            if (clubeDaCasaValue.NomeClube == clubeVisitenteValue.NomeClube) throw new Exception("Clubes inválidos não podem ser iguais os clubes num jogo");
            if (resultClubCasaValue == resultClubeVisitentValue) throw new Exception("Resultados inválidos não pode haver empates");

            ClubeDaCasa = clubeDaCasaValue;
            ClubeVisitente = clubeVisitenteValue;
            InicioDoJogo = iniciodoJogoValue;
            ResultClubeDaCasa = resultClubCasaValue;
            ResultClubeVisitente = resultClubeVisitentValue;
        }

        public override string ToString()
        {
            return ClubeDaCasa + " " + ClubeVisitente + " " + InicioDoJogo.ToShortDateString() + " " + ResultClubeDaCasa + " " + ResultClubeVisitente;
        }
    }
}
