using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.BaseDados
{
    class TorneioDB
    {
        ClubeDeFutebolDB clubeValue = new ClubeDeFutebolDB();
        private string caminho;
        private SqlCommand comand;
        private SqlConnection conn;

        public TorneioDB()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            comand = null;
        }

        public bool AdicionarJogo(Jogo JogoValue)
        {
            string qry = string.Format("INSERT INTO Jogos values({0}, {1}, {2},  DATEADD(year, 0, '{3}/{4}/{5}'), {6}, {7}, '{8}')", QuantosJogos() + 1, 
                IdClube(JogoValue.ClubeDaCasa.NomeClube), IdClube(JogoValue.ClubeVisitente.NomeClube),JogoValue.InicioDoJogo.Year,
                JogoValue.InicioDoJogo.Month, JogoValue.InicioDoJogo.Day, JogoValue.ResultClubeDaCasa, JogoValue.ResultClubeVisitente, JogoValue.ClubeDaCasa.NomeDoEstadio);
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool AdicionarTorneio(Torneio TorneioValue, int IdPais)
        {
            string qry = string.Format("INSERT INTO Torneios values({0}, {1}, {2}, {3},  DATEADD(year, 0, '{4}/{5}/{6}'), {7}, {8},{9})", QuantosTorneios() + 1,
                QuantosJogos() - 2, QuantosJogos() - 1, QuantosJogos(),TorneioValue.InicioDoTorneio.Year, TorneioValue.InicioDoTorneio.Month
                , TorneioValue.InicioDoTorneio.Day, IdClube(TorneioValue.ClubeVencedor.NomeClube), IdPais, TorneioValue.Premio.ToString().Replace(",","."));
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public int QuantosJogos()
        {
            string qry = "SELECT COUNT(*) FROM Jogos";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int quant = (int)comand.ExecuteScalar();
            conn.Close();
            return quant;
        }

        public int QuantosTorneios()
        {
            string qry = "SELECT COUNT(*) FROM Torneios";
            conn = new SqlConnection(caminho);
            SqlCommand comand = new SqlCommand(qry, conn);
            conn.Open();
            int quant = (int)comand.ExecuteScalar();
            conn.Close();
            return quant;
        }

        public bool Verifica(string nifJog, string contacto, int numCamisola, int idClube)
        {
            string qryDir = string.Format("SELECT COUNT(*) FROM Dirigentes WHERE Nif = '{0}' OR Contacto = '{1}' HAVING COUNT(*) > 0", nifJog, contacto);
            string qryTec = string.Format("SELECT COUNT(*) FROM Tecnicos WHERE Nif = '{0}' OR Contacto = '{1}' HAVING COUNT(*) > 0", nifJog, contacto);
            string qryJog = string.Format("SELECT COUNT(*) FROM Jogadores WHERE NumeroCamisola = {0} AND id_Clube = {1} HAVING COUNT(*) > 0", numCamisola, idClube);

            conn = new SqlConnection(caminho);
            SqlConnection conn2 = new SqlConnection(caminho);
            SqlConnection conn3 = new SqlConnection(caminho);
            comand = new SqlCommand(qryDir, conn);
            SqlCommand comandDir = new SqlCommand(qryTec, conn2);
            SqlCommand comandTec = new SqlCommand(qryJog, conn3);
            conn.Open();
            conn2.Open();
            conn3.Open();
            SqlDataReader verificarDir = comand.ExecuteReader();
            SqlDataReader verificarTec = comandDir.ExecuteReader();
            SqlDataReader verificarJog = comandTec.ExecuteReader();

            while (verificarDir.Read() || verificarTec.Read() || verificarJog.Read())
            {
                throw new Exception("Jogador inválido, não pode haver dados repetidos");
            }
            conn.Close();
            conn2.Close();
            conn3.Close();
            return true;
        }

        public bool QuantosJogadores(int idClube)
        {
            string qry = "SELECT COUNT(*) FROM Jogadores WHERE id_Clube = " + idClube + "";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int quant = (int)comand.ExecuteScalar();
            conn.Close();
            if (quant < 11) throw new Exception("Clube inválido tem de ter pelo menos 11 jogadores");
            else return true;
        }

        public Jogo JogoValue(int idJogo)
        {
            string qry = string.Format("SELECT * FROM Jogos WHERE Id_Jogo = {0}", idJogo);
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader reader = comand.ExecuteReader();
            
            while (reader.Read())
            {
                ClubeDeFutebol clubeCasa = clubeValue.DadosClube(int.Parse(reader[1].ToString()));
                ClubeDeFutebol clubeVisitente = clubeValue.DadosClube(int.Parse(reader[2].ToString()));
                Jogo jogoValue = new Jogo(clubeCasa, clubeVisitente, DateTime.Parse(reader[3].ToString()), int.Parse(reader[4].ToString())
                    , int.Parse(reader[5].ToString()));
                conn.Close();
                return jogoValue;
            }
            throw new Exception("Algo esta errado no Id do jogo");
        }

        public List<Torneio> TodosOsTorneios()
        {
            string qry = "SELECT * FROM Torneios";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);

            conn.Open();
            SqlDataReader reader = comand.ExecuteReader();
            List<Torneio> torneios = new List<Torneio>();
            while (reader.Read())
            {
                torneios.Add(new Torneio(JogoValue(int.Parse(reader[1].ToString())), JogoValue(int.Parse(reader[2].ToString())),
                    JogoValue(int.Parse(reader[3].ToString())), DateTime.Parse(reader[4].ToString()), clubeValue.DadosClube(int.Parse(reader[5].ToString()))
                    , (Classes.Paises) int.Parse(reader[6].ToString())+1, double.Parse(reader[7].ToString())));
            }
            conn.Close();
            return torneios;
        }

        public int IdClube(string nomeClube)
        {
            string qry = "SELECT Id_Clube FROM ClubesFutebol WHERE NomeClube = '" + nomeClube + "'";
            SqlConnection conn = new SqlConnection(caminho);
            SqlCommand comandcount = new SqlCommand(qry, conn);
            conn.Open();
            int id = (int)comandcount.ExecuteScalar();
            conn.Close();
            return id;
        }
    }
}
