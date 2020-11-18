using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.BaseDados
{
    class ClubeDeFutebolDB
    {
        private string caminho;
        private SqlCommand comand;
        private SqlConnection conn;

        public ClubeDeFutebolDB()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            comand = null;
        }

        public bool AdicionarClube(ClubeDeFutebol clubeValue)
        {
            string qry = string.Format("INSERT INTO ClubesFutebol values({0}, '{1}',  DATEADD(year, 0, '{2}/{3}/{4}'), '{5}', {6})", QuantosClubes() + 1,
                clubeValue.NomeClube, clubeValue.DataDeFundacao.Year, clubeValue.DataDeFundacao.Month, clubeValue.DataDeFundacao.Day,
                clubeValue.NomeDoEstadio, clubeValue.DivicaoClube);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public int QuantosClubes()
        {
            string qry = "SELECT COUNT(*) FROM ClubesFutebol";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int tamanho = (int)comand.ExecuteScalar();
            conn.Close();
            return tamanho;
        }

        public ClubeDeFutebol DadosClube(int idClube)
        {
            string qry = "SELECT * FROM ClubesFutebol WHERE Id_Clube = " + idClube  + "";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            
            while (read.Read())
            {
                ClubeDeFutebol dadosClube = new ClubeDeFutebol(read[1].ToString(),DateTime.Parse(read[2].ToString()),
                    read[3].ToString(),int.Parse(read[4].ToString()));
                conn.Close();
                return dadosClube;
            }
            conn.Close();
            throw new Exception("Não existe nenhum clube");
        }

        public List<ClubeDeFutebol> ListClubes()
        {
            string qry = @"SELECT * FROM ClubesFutebol";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            List<ClubeDeFutebol> clubesList = new List<ClubeDeFutebol>();
            while (read.Read())
            {
                clubesList.Add(new ClubeDeFutebol(read[1].ToString(), DateTime.Parse(read[2].ToString()),
                    read[3].ToString(), int.Parse(read[4].ToString())));
            }
            conn.Close();
            return clubesList;
        }

        public bool AtualizarClube(string nome, string nomeEstadio, int numDivicao, int idClube)
        {
            string qry = string.Format("UPDATE ClubesFutebol SET NomeClube = '{0}', NomeEstadio = '{1}', NumDivisao = {2} WHERE Id_Clube = {3}",
                nome, nomeEstadio, numDivicao, idClube);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }
    }
}
