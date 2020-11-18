using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalFour.BaseDados
{
    class LigacoesDataGrid
    {
        public string caminho;
        public SqlConnection conn = new SqlConnection();
        public SqlCommand comand = new SqlCommand();

        public LigacoesDataGrid()
        {
            caminho = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\emanu\OneDrive\Ambiente de Trabalho\TrabalhoFinalPoo\Testes\FinalFour\FinalFour\LigacaoDB\DataBase.mdf'; Integrated Security = True; Connect Timeout = 30";
            conn = null;
        }

        public DataTable dataGridJogadores(string pesq)
        {
            string query = "SELECT Jogadores.Nif, Jogadores.Nome, Jogadores.DataNascimento, Jogadores.Peso, Jogadores.Contacto, Jogadores.Email, " +
            "Jogadores.Salario, Jogadores.InicioContrato, Jogadores.FimContrato, Jogadores.NomeCamisola, Jogadores.NumeroCamisola, PosicaoJog.TipoPosicao, " +
            "Nacionalidades.NomeNacionalidade, ClubesFutebol.NomeClube FROM Jogadores, PosicaoJog, Nacionalidades, ClubesFutebol WHERE(Jogadores.Nif = '" + 
            pesq + "' OR Jogadores.NomeCamisola = '" + pesq+"') AND " + "PosicaoJog.Id_Pos = Jogadores.id_Posicao AND Nacionalidades.Id_Nac =" +
            " Jogadores.id_Nacionalidade AND ClubesFutebol.Id_Clube = Jogadores.id_Clube";
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }

        public DataTable dataGridTecnico(string pesq)
        {
            string query = "SELECT Tecnicos.Nif, Tecnicos.Nome, Tecnicos.DataNascimento, Tecnicos.Contacto, Tecnicos.Email, Tecnicos.Salario, " +
            "Tecnicos.InicioContrato, Tecnicos.FimContrato, Tecnicos.Ncarteira, Tecnicos.NumGrauTec, ClubesFutebol.NomeClube, TiposTecnicos.TipoTecnico " +
            "FROM Tecnicos, ClubesFutebol, TiposTecnicos WHERE(Tecnicos.Nif = '" + pesq + "' OR" + " Tecnicos.Nome = '" + pesq + "') AND " +
            "ClubesFutebol.Id_Clube = Tecnicos.id_Clube AND TiposTecnicos.Id_Tec = Tecnicos.id_tipoTec";
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }

        public DataTable dataGridDirigente(string pesq)
        {
            string query = "SELECT Dirigentes.Nif, Dirigentes.Nome, Dirigentes.DataNascimento, Dirigentes.Contacto, Dirigentes.Email," +
                " Dirigentes.InicioContrato, Dirigentes.FimContrato, Dirigentes.Ncarteira, ClubesFutebol.NomeClube, " +
                "TiposDirigentes.TipoDir FROM Dirigentes, ClubesFutebol, TiposDirigentes WHERE(Dirigentes.Nif = '" + pesq + "' OR " +
                "Dirigentes.Nome = '" + pesq + "') AND ClubesFutebol.Id_Clube = Dirigentes.id_Clube AND TiposDirigentes.Id_Dir =" +
                " Dirigentes.id_tipoDir";
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }

        public DataTable dataGridClube(string pesq)
        {
            string query = "SELECT NomeClube, NomeEstadio, DataFundacao, NumDivisao FROM ClubesFutebol WHERE NomeClube = '" + pesq +"'";
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }

        public DataTable dataGridTorneio(DateTime pesq)
        {
            string query = "SELECT Torneios.Id_PrimeiroJogo, Torneios.Id_SegundoJogo, Torneios.Id_UltimoJogo, Torneios.InicioTorneio," +
                " ClubesFutebol.NomeClube, Paises.NomePaises, Torneios.Pemio From ClubesFutebol, Torneios, Paises WHERE Torneios.InicioTorneio =" +
                " '"+ pesq.Year + "-" + pesq.Month + "-" + pesq.Day + "' AND ClubesFutebol.Id_Clube = Torneios.Id_ClubeVencedor AND Paises.Id_Pais = " +
                "Torneios.id_Pais";
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }

        public DataTable dataGridJogos(DateTime pesq)
        {
            string query = "SELECT Jogos.Id_ClubeDaCasa, Jogos.Id_ClubeVisitente, Jogos.InicioJogo, Jogos.ResultadoClubeDaCasa, " +
                "Jogos.ResultadoClubeVisitente, Jogos.EstadioDoJogo From Jogos,Torneios WHERE Torneios.InicioTorneio = '" + pesq.Year + "-" + pesq.Month +
                "-" + pesq.Day + "' AND Torneios.Id_PrimeiroJogo = Jogos.Id_Jogo OR Torneios.InicioTorneio = '" + pesq.Year + "-" + pesq.Month 
                + "-"+ pesq.Day + "'" + " AND Torneios.Id_SegundoJogo = Jogos.Id_Jogo OR Torneios.InicioTorneio = '" + pesq.Year + "-" + 
                pesq.Month + "-" + pesq.Day + "' AND Torneios.Id_UltimoJogo = Jogos.Id_Jogo";
            
            conn = new SqlConnection(caminho);
            conn.Open();
            comand = new SqlCommand(query, conn);
            comand.CommandType = CommandType.Text;
            SqlDataAdapter dat = new SqlDataAdapter(comand);
            DataTable table = new DataTable();
            dat.Fill(table);
            conn.Close();
            return table;
        }
    }
}
