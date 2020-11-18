using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalFour.BaseDados
{
    class JogadorDB
    {
        private string caminho;
        private SqlCommand comand;
        private SqlConnection conn;
        
        public JogadorDB()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            comand = null;
        }


        public bool InserirJogador(Jogador jog, int idClube, int idNacionalidade, int idTipoPosicao)
        {
            Verifica(jog.NIF, jog.Contacto, jog.NumDaCamisola, idClube);

            string qry = string.Format("INSERT INTO Jogadores values('{0}', '{1}',  DATEADD(year, 0, '{2}/{3}/{4}'), {5}, '{6}', '{7}', " +
                "'{8}',  DATEADD(year,0 , '{9}/{10}/{11}'),  DATEADD(year, 0, '{12}/{13}/{14}'), '{15}', " +
                "{16}, {17}, {18}, {19})", jog.NIF, jog.Nome, jog.DataNascimento.Year, jog.DataNascimento.Month, jog.DataNascimento.Day,
                jog.Peso.ToString().Replace(",", "."), jog.Contacto, jog.Email, jog.Salario, jog.InicioContrato.Year, jog.InicioContrato.Month,
                jog.InicioContrato.Day, jog.FimContrato.Year, jog.FimContrato.Month, jog.FimContrato.Day, jog.NomeJogadorCamisola,
                jog.NumDaCamisola, idTipoPosicao, idNacionalidade, idClube);

            conn = new SqlConnection(caminho);

            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool AtualizarJogador(string contacto, string email, double salario, string nomeCamisola, int numCamisola, int posicao, double peso,
            DateTime inicioContrato, DateTime fimContrato, string nif, int idClube)
        {
            Verifica(nif, contacto, numCamisola, idClube);
            string qry = string.Format("UPDATE Jogadores SET Contacto = '{0}', Email = '{1}', Salario = {2}, NumeroCamisola = {3}," +
                "InicioContrato = DATEADD(year, 0,'{4}/{5}/{6}'), FimContrato = DATEADD(year, 0, '{7}/{8}/{9}'), NomeCamisola = '{10}'," +
                " id_Clube = {11}, id_Posicao = {12}, Peso = {13} WHERE Nif = '{14}'", contacto, email, salario, numCamisola,
                inicioContrato.Year, inicioContrato.Month, inicioContrato.Day, fimContrato.Year, fimContrato.Month, fimContrato.Day, nomeCamisola,
                idClube, posicao, peso.ToString().Replace(",", "."), nif);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public List<Jogador> ListJogadores()
        {
            string qry = @"SELECT * FROM Jogadores";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            List<Jogador> jogadores = new List<Jogador>();
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            
            while (read.Read())
            {
                jogadores.Add(new Jogador(read[1].ToString(), DateTime.Parse(read[2].ToString()), read[0].ToString(), read[4].ToString(),
                    read[5].ToString(), double.Parse(read[6].ToString()), read[9].ToString(), int.Parse(read[10].ToString()),
                    (TipoPosicao)int.Parse(read[11].ToString())-1, double.Parse(read[3].ToString()), DateTime.Parse(read[7].ToString()),
                    DateTime.Parse(read[8].ToString()), (Classes.Nacionalidades) int.Parse(read[12].ToString())-1));
            }
            conn.Close();
            return jogadores;
        }

        public int IdClubeJogador(string nomeNif)
        {
            string qry = "SELECT Id_clube FROM Jogadores WHERE Nif = '" + nomeNif + "' OR NomeCamisola = '" + nomeNif + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int Idclube = (int) comand.ExecuteScalar();
            return Idclube;
        }

        public List<Jogador> JogadoresClube(int idClube)
        {
            string qry = @"SELECT * FROM Jogadores WHERE id_Clube = " + idClube;
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);

            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            List<Jogador> jogadores = new List<Jogador>();
            while (read.Read())
            {
                jogadores.Add(new Jogador(read[1].ToString(), DateTime.Parse(read[2].ToString()), read[0].ToString(), read[4].ToString(),
                    read[5].ToString(), double.Parse(read[6].ToString()), read[9].ToString(), int.Parse(read[10].ToString()),
                    (TipoPosicao)int.Parse(read[11].ToString())-1, double.Parse(read[3].ToString()), DateTime.Parse(read[7].ToString()),
                    DateTime.Parse(read[8].ToString()),(Classes.Nacionalidades)int.Parse(read[12].ToString())-1));
            }
            conn.Close();
            return jogadores;
        }

        public Jogador dadosJogador(string valuePesq)
        {
            string qry = "SELECT * FROM Jogadores WHERE Nif = '" + valuePesq + "' OR NomeCamisola = '" + valuePesq + "'";
            string jogClub = "SELECT * FROM WHERE Nif = '" + valuePesq + "' OR NomeCamisola = '" + valuePesq + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();

            while (read.Read())
            {
                Jogador JogValue = new Jogador(read[1].ToString(), DateTime.Parse(read[2].ToString()), read[0].ToString(), read[4].ToString(),
                    read[5].ToString(), double.Parse(read[6].ToString()), read[9].ToString(),int.Parse(read[10].ToString()), 
                    (TipoPosicao) int.Parse(read[11].ToString()), double.Parse(read[3].ToString()),DateTime.Parse(read[7].ToString()),
                    DateTime.Parse(read[8].ToString()), (Classes.Nacionalidades) int.Parse(read[12].ToString()));
                conn.Close();
                return JogValue;
            }
            conn.Close();
            throw new Exception("Não existe esse Jogador");
        }


        public bool ApagarJogador(string NifValue)
        {
            string qry = "DELETE FROM Jogadores WHERE Nif = '" + NifValue + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool Verifica(string nifJog, string contacto, int numCamisola, int idClube)
        {
            string qryDir = string.Format("SELECT COUNT(*) FROM Dirigentes WHERE Nif = '{0}' OR Contacto = '{1}' HAVING COUNT(*) > 0", nifJog, contacto);
            string qryTec = string.Format("SELECT COUNT(*) FROM Tecnicos WHERE Nif = '{0}' OR Contacto = '{1}' HAVING COUNT(*) > 0", nifJog, contacto);
            string qryJog = string.Format("SELECT COUNT(*) FROM Jogadores WHERE NumeroCamisola = {0} AND id_Clube = {1} AND " +
                " Nif != {2} HAVING COUNT(*) > 0", numCamisola, idClube, nifJog);

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
    }
}
