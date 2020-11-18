using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FinalFour.BaseDados
{
    class TecnicosDB
    {
        private string caminho;
        private SqlCommand comand;
        private SqlConnection conn;

        public TecnicosDB()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            comand = null;
        }


        public bool InserirTecnico(Tecnico tec, int tipoTecnico,int idClube)
        {
            Verifica(tec.NIF, tec.Contacto, tec.NumCarteira, idClube);
            string qry = string.Format("INSERT INTO Tecnicos values('{0}', '{1}', DATEADD(year, 0, '{2}/{3}/{4}'), '{5}', '{6}', '{7}', " +
                " DATEADD(year, 0, '{8}/{9}/{10}'),  DATEADD(year, 0, '{11}/{12}/{13}'), {14}, " +
                "{15}, {16}, {17})", tec.NIF, tec.Nome, tec.DataNascimento.Year, tec.DataNascimento.Month, tec.DataNascimento.Day, tec.Contacto, 
                tec.Email, tec.Salario,tec.InicioContrato.Year, tec.InicioContrato.Month, tec.InicioContrato.Day, tec.FimContrato.Year, 
                tec.FimContrato.Month, tec.FimContrato.Day,tec.NumCarteira, tipoTecnico, tec.GrauTecnico, idClube);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool AtualizarTecnico(string contacto, string email, double salario, int tipoTecnico, DateTime inicioContrato, DateTime fimContrato,
        int numCarteira, int idClube,int numGrau,string nif)
        {
            Verifica(nif, contacto, numCarteira, idClube);
            string qry = string.Format("UPDATE Tecnicos SET Contacto = '{0}', Email = '{1}', Salario = {2}, id_tipoTec = {3}," +
                "InicioContrato = DATEADD(year, 0, '{4}/{5}/{6}'), FimContrato = DATEADD(year, 0, '{7}/{8}/{9}'), Ncarteira = {10}," +
                " NumGrauTec = {11}, id_Clube = {12} WHERE Nif = '{13}'", contacto, email, salario, tipoTecnico, inicioContrato.Year, inicioContrato.Month, inicioContrato.Day,
                fimContrato.Year, fimContrato.Month, fimContrato.Day, numCarteira, numGrau,idClube,nif);

            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public List<Tecnico> ListTecnicos()
        {
            string qry = @"SELECT * FROM Tecnicos";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            List<Tecnico> tecnicos = new List<Tecnico>();
            conn.Open();
            SqlDataReader reader = comand.ExecuteReader();
            
            while (reader.Read())
            {
                tecnicos.Add(new Tecnico(reader[1].ToString(), DateTime.Parse(reader[2].ToString()), reader[0].ToString(), reader[3].ToString(),
                    reader[4].ToString(), double.Parse(reader[5].ToString()), (TipoTec) int.Parse(reader[9].ToString())-1, DateTime.Parse(reader[6].ToString()),
                    DateTime.Parse(reader[7].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[8].ToString())));
            }
            conn.Close();
            return tecnicos;
        }

        public int IdClubeTecnico(string nomeNif)
        {
            string qry = "SELECT Id_clube FROM Tecnicos WHERE Nif = '" + nomeNif + "' OR Nome = '" + nomeNif + "'";
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            conn.Open();
            int Idclube = (int)comand.ExecuteScalar();
            return Idclube;
        }

        public List<Tecnico> TecnicoClube(int idClube)
        {
            string qry = @"SELECT * FROM Tecnicos WHERE id_Clube = " + idClube;
            conn = new SqlConnection(caminho);
            comand = new SqlCommand(qry, conn);
            List<Tecnico> tecnicos = new List<Tecnico>();
            conn.Open();
            SqlDataReader reader = comand.ExecuteReader();
            
            while (reader.Read())
            {
                tecnicos.Add(new Tecnico(reader[1].ToString(), DateTime.Parse(reader[2].ToString()), reader[0].ToString(), reader[3].ToString(),
                    reader[4].ToString(), double.Parse(reader[5].ToString()), (TipoTec)int.Parse(reader[9].ToString()) - 1, DateTime.Parse(reader[6].ToString()),
                    DateTime.Parse(reader[7].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[8].ToString())));
            }
            conn.Close();
            return tecnicos;
        }

        public Tecnico DadosTecnico(string valuePesq)
        {
            string qry = "SELECT * FROM Tecnicos WHERE Nif = '" + valuePesq + "' OR Nome = '" + valuePesq + "'";
            conn = new SqlConnection(caminho);
            SqlCommand comand = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader reader = comand.ExecuteReader();
            
            while (reader.Read())
            {
                Tecnico dadosTecnico = new Tecnico(reader[1].ToString(), DateTime.Parse(reader[2].ToString()), reader[0].ToString(), reader[3].ToString(),
                    reader[4].ToString(), double.Parse(reader[5].ToString()), (TipoTec)int.Parse(reader[9].ToString()), DateTime.Parse(reader[6].ToString()),
                    DateTime.Parse(reader[7].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[8].ToString()));
                conn.Close();
                return dadosTecnico;
            }
            conn.Close();
            throw new Exception("Não existe tecnicos");
        }

        public bool ApagarTecnico(string NifValue)
        {
            string qry = "DELETE FROM Tecnicos WHERE Nif = '" + NifValue + "'";
            conn = new SqlConnection(caminho);
            SqlCommand comand = new SqlCommand(qry, conn);
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public bool Verifica(string nif, string contacto, int numCarteira, int idClube)
        {
            string qryDir = string.Format("SELECT COUNT(*) FROM Dirigentes WHERE Nif = '{0}' OR Contacto = '{1}' OR Ncarteira = {2} AND" +
                " id_Clube = {3} HAVING COUNT(*) > 0"
                , nif, contacto, numCarteira, idClube);
           string qryTec = string.Format("SELECT COUNT(*) FROM Tecnicos WHERE Ncarteira = {0} AND id_Clube = {1} AND Nif != {2} HAVING COUNT(*) > 0", numCarteira, idClube, nif);
            string qryJog = string.Format("SELECT COUNT(*) FROM Jogadores WHERE Nif = '{0}' OR Contacto = {1} HAVING COUNT(*) > 0", nif, contacto);

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
