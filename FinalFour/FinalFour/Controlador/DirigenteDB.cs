using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.BaseDados
{
    class DirigenteDB
    {
        private string caminho;
        private SqlCommand comand;
        private SqlConnection conn;

        public DirigenteDB()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            comand = null;
        }

        public bool InserirDirigente(Dirigente dirigent, int tipoDirigente,int idClube)
        {
            Verifica(dirigent.NIF, dirigent.Contacto, dirigent.NumCarteira, idClube);

            string qry = string.Format("INSERT INTO Dirigentes values('{0}', '{1}',  DATEADD(year, 0, '{2}/{3}/{4}'), '{5}', '{6}', '{7}'," +
                "  DATEADD(year, 0, '{8}/{9}/{10}'),  DATEADD(year, 0, '{11}/{12}/{13}'), {14}, {15}, {16})"
                , dirigent.NIF, dirigent.Nome, dirigent.DataNascimento.Year, dirigent.DataNascimento.Month, dirigent.DataNascimento.Day,
                dirigent.Contacto, dirigent.Email, dirigent.Salario,dirigent.InicioContrato.Year, dirigent.InicioContrato.Month
                , dirigent.InicioContrato.Day, dirigent.FimContrato.Year, dirigent.FimContrato.Month, dirigent.FimContrato.Day, 
                dirigent.NumCarteira, tipoDirigente, idClube);
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool AtualizarDirigente(string contacto, string email, double salario, int tipoDirigente, DateTime inicioContrato, DateTime fimContrato,
            int numCarteira, int idClube, string nif)
        {
            string qry = string.Format("UPDATE Dirigentes SET Contacto = '{0}', Email = '{1}', Salario = {2}, id_tipoDir = {3}," +
                "InicioContrato = DATEADD(year, 0, '{4}/{5}/{6}'), FimContrato = DATEADD(year, 0, '{7}/{8}/{9}'), Ncarteira = {10}," +
                " id_Clube = {11} WHERE Nif = '{12}'", contacto, email, salario, tipoDirigente, inicioContrato.Year, inicioContrato.Month, inicioContrato.Day,
                fimContrato.Year, fimContrato.Month, fimContrato.Day, numCarteira, idClube, nif);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public List<Dirigente> ListDirigentes()
        {
            string qry = @"SELECT * FROM Dirigentes";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            List<Dirigente> dirigentes = new List<Dirigente>();
            while (read.Read())
            {
                dirigentes.Add(new Dirigente(read[1].ToString(),DateTime.Parse(read[2].ToString()), read[0].ToString(), read[3].ToString(), read[4].ToString(),
                    double.Parse(read[5].ToString()), (TipoDir) int.Parse(read[9].ToString())-1, DateTime.Parse(read[6].ToString()), DateTime.Parse(read[7].ToString()),
                    int.Parse(read[8].ToString())));
            }
            conn.Close();
            return dirigentes;
        }

        public List<Dirigente> DirigenteClube(int idClube)
        {
            string qry = @"SELECT * FROM Dirigentes WHERE id_Clube = " + idClube;
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);

            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            List<Dirigente> dirigentes = new List<Dirigente>();
            while (read.Read())
            {
                dirigentes.Add(new Dirigente(read[1].ToString(), DateTime.Parse(read[2].ToString()), read[0].ToString(), read[3].ToString(), read[4].ToString(),
                    double.Parse(read[5].ToString()), (TipoDir)int.Parse(read[9].ToString())-1, DateTime.Parse(read[6].ToString()), DateTime.Parse(read[7].ToString()),
                    int.Parse(read[8].ToString())));
            }
            conn.Close();
            return dirigentes;
        }

        public int IdClubeDirigente(string nomeNif)
        {
            string qry = "SELECT Id_clube FROM Dirigentes WHERE Nif = '" + nomeNif + "' OR Nome = '" + nomeNif + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int Idclube = (int)comand.ExecuteScalar();
            return Idclube;
        }

        public Dirigente DadosDirigente(string valuePesq)
        {
            string qry = "SELECT * FROM Dirigentes WHERE Nif = '" + valuePesq + "' OR Nome = '" + valuePesq + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader read = comand.ExecuteReader();
            while (read.Read())
            {
                Dirigente dirigentes = new Dirigente(read[1].ToString(), DateTime.Parse(read[2].ToString()), read[0].ToString(), read[3].ToString(), read[4].ToString(),
                double.Parse(read[5].ToString()), (TipoDir)int.Parse(read[9].ToString()), DateTime.Parse(read[6].ToString()), DateTime.Parse(read[7].ToString()),
                int.Parse(read[8].ToString()));
                conn.Close();
                return dirigentes;   
            }
            conn.Close();
            throw new Exception("Não existe dirigentes");
        }

        public bool ApagarDirigente(string NifValue)
        {
            string qry = "DELETE FROM Dirigentes WHERE Nif = '" + NifValue + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool Verifica(string nif, string contacto, int numCarteira, int idClube)
        {
            string qryTec = string.Format("SELECT COUNT(*) FROM Tecnicos WHERE Nif = '{0}' OR Contacto = '{1}' OR Ncarteira = {2} AND" +
                " id_Clube = {3} HAVING COUNT(*) > 0"
                , nif, contacto, numCarteira, idClube);
            string qryDir = string.Format("SELECT COUNT(*) FROM Dirigentes WHERE Ncarteira = {0} AND id_Clube = {1} HAVING COUNT(*) > 0", numCarteira, idClube);
            string qryJog = string.Format("SELECT COUNT(*) FROM Jogadores WHERE Nif = {0} OR Contacto = {1} HAVING COUNT(*) > 0", nif, contacto);

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
                throw new Exception("Dirigente inválido, não pode haver dados repetidos");
            }
            conn.Close();
            conn2.Close();
            conn3.Close();
            return true;
        }
    }
}
