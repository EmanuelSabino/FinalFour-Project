using FinalFour.BaseDados;
using FinalFour.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalFour
{
    public partial class Form1 : Form
    {
        ClubeDeFutebolDB clubes = new ClubeDeFutebolDB();
        DirigenteDB dirigentes = new DirigenteDB();
        JogadorDB jogadores = new JogadorDB();
        TecnicosDB tecnicos = new TecnicosDB();
        TorneioDB torneios = new TorneioDB();
        LigacoesDataGrid dataTable = new LigacoesDataGrid();
        List<ClubeDeFutebol> clubesObjetos = new List<ClubeDeFutebol>();

        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            try
            {
                foreach (Torneio listTorneios in torneios.TodosOsTorneios())
                {
                    comboBox9.Items.Add(listTorneios.InicioDoTorneio.ToShortDateString());
                    comboBox9.AutoCompleteCustomSource.Add(listTorneios.InicioDoTorneio.ToShortDateString());
                }
                //clubes
                foreach (ClubeDeFutebol listClubes in clubes.ListClubes())
                {
                    clubesObjetos.Add(listClubes);
                    comboBox1.Items.Add(listClubes.NomeClube);
                    comboBox1.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox2.Items.Add(listClubes.NomeClube);
                    comboBox2.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox3.Items.Add(listClubes.NomeClube);
                    comboBox3.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox4.Items.Add(listClubes.NomeClube);
                    comboBox4.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox5.Items.Add(listClubes.NomeClube);
                    comboBox5.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox6.Items.Add(listClubes.NomeClube);
                    comboBox6.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox7.Items.Add(listClubes.NomeClube);
                    comboBox7.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox10.Items.Add(listClubes.NomeClube);
                    comboBox10.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox16.Items.Add(listClubes.NomeClube);
                    comboBox16.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox22.Items.Add(listClubes.NomeClube);
                    comboBox22.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox17.Items.Add(listClubes.NomeClube);
                    comboBox17.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox27.Items.Add(listClubes.NomeClube);
                    comboBox27.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox31.Items.Add(listClubes.NomeClube);
                    comboBox31.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox36.Items.Add(listClubes.NomeClube);
                    comboBox36.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox44.Items.Add(listClubes.NomeClube);
                    comboBox44.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox13.Items.Add(listClubes.NomeClube);
                    comboBox13.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox23.Items.Add(listClubes.NomeClube);
                    comboBox23.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox28.Items.Add(listClubes.NomeClube);
                    comboBox28.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox33.Items.Add(listClubes.NomeClube);
                    comboBox33.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox40.Items.Add(listClubes.NomeClube);
                    comboBox40.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox38.Items.Add(listClubes.NomeClube);
                    comboBox38.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                    comboBox45.Items.Add(listClubes.NomeClube);
                    comboBox45.AutoCompleteCustomSource.Add(listClubes.NomeClube);
                }
            }catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   TORNEIO                                                             ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //submenu criar torneio
        private void criarTorneioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = false;
            inicioToolStripMenuItem.Text = "Criar Torneio";
        }
        // Criar torneio ---- Botão de criar
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                //verificar se há 11 jogadores
                torneios.QuantosJogadores(comboBox1.SelectedIndex+1);
                torneios.QuantosJogadores(comboBox2.SelectedIndex+1);
                torneios.QuantosJogadores(comboBox3.SelectedIndex+1);
                torneios.QuantosJogadores(comboBox4.SelectedIndex+1);

                if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1 ||
                    comboBox5.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                {
                    throw new Exception("Clubes inválidos, selecione um clube válido não deixe em branco");
                }
                if (comboBox8.SelectedIndex == -1) throw new Exception("País inválido, não deixe em branco!!");
                if (comboBox7.SelectedIndex == -1) throw new Exception("Clube vencedor inválido");
                
                //três jogos
                Jogo primeiroJogo = new Jogo(clubesObjetos[comboBox1.SelectedIndex], clubesObjetos[comboBox2.SelectedIndex], dateTimePicker1.Value,
                    int.Parse(numericUpDown1.Value.ToString()), int.Parse(numericUpDown2.Value.ToString()));
                Jogo segundoJogo = new Jogo(clubesObjetos[comboBox3.SelectedIndex], clubesObjetos[comboBox4.SelectedIndex], dateTimePicker2.Value,
                    int.Parse(numericUpDown3.Value.ToString()), int.Parse(numericUpDown4.Value.ToString()));
                Jogo ultimoJogo = new Jogo(clubesObjetos[comboBox5.SelectedIndex], clubesObjetos[comboBox6.SelectedIndex], dateTimePicker3.Value,
                    int.Parse(numericUpDown5.Value.ToString()), int.Parse(numericUpDown6.Value.ToString()));
                //torneio
                Torneio newTorneio = new Torneio(primeiroJogo,segundoJogo,ultimoJogo,dateTimePicker5.Value, clubesObjetos[comboBox7.SelectedIndex],
                    (Classes.Paises) comboBox8.SelectedIndex, double.Parse(numericUpDown7.Value.ToString()));
                newTorneio.VerificarTorneio(primeiroJogo,segundoJogo,ultimoJogo, dateTimePicker5.Value, clubesObjetos[comboBox7.SelectedIndex]);
                torneios.AdicionarJogo(primeiroJogo);
                torneios.AdicionarJogo(segundoJogo);
                torneios.AdicionarJogo(ultimoJogo);
                torneios.AdicionarTorneio(newTorneio, comboBox8.SelectedIndex+1);
                ClearAll();
                MessageBox.Show("Registo com sucesso!!");
                comboBox9.Items.Add(newTorneio.InicioDoTorneio.ToShortDateString());
                comboBox9.AutoCompleteCustomSource.Add(newTorneio.InicioDoTorneio.ToShortDateString());
            }
            catch (Exception erroTorneio)
            {
                MessageBox.Show(erroTorneio.Message);
            }
        }
        //criar torneio ------ boatão cancelar
        private void button2_Click_1(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
            ClearAll();
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }
       
        // submenu ver torneio
        private void verTorneioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = false;
            inicioToolStripMenuItem.Text = "Ver Torneio";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //ver torneio --- combobox escolha por pais
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //ver torneio  --- botão cancelar
        private void button1_Click(object sender, EventArgs e)
        {
            ClearAll();
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        // ver torneio  ---- botão reset
        private void button5_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        // ver torneio ---- botão resultado
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar se combobox torneio esta preenchido
                if (comboBox9.SelectedIndex == -1) throw new Exception("Torneio inválido!!");
                DateTime data = DateTime.Parse(comboBox9.Text.ToString());
                dataGridView1.DataSource = dataTable.dataGridJogos(data);
                dataGridView2.DataSource = dataTable.dataGridTorneio(data);
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;

            }
            catch (Exception erroVerTorneio)
            {
                MessageBox.Show(erroVerTorneio.Message);
            }
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        
        //tempo real
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripMenuItem1.Text = DateTime.Now.ToLongTimeString();
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   Menu                                                                ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        }
        
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }
        private void infoModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        //submenu Sair
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   Jogador                                                             ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //menu Ver jogador
        private void verJogadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Ver Jogador";
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = false;
            ClearAll();
        }
        //ver jogador  ----- botão de resultado
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox12.SelectedIndex != -1)
                {
                    comboBox12.Enabled = false;
                    comboBox13.Enabled = false;
                    button7.Visible = false;
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = dataTable.dataGridJogadores(comboBox12.Text);
                }
                else throw new Exception("Jogador inválido!!");
            }catch (Exception erroVerJogador)
            {
                MessageBox.Show(erroVerJogador.Message);
            }
        }
        //ver jogador ---- botão de reset
        private void button6_Click_1(object sender, EventArgs e)
        {
            ClearAll();
            comboBox11.SelectedIndex = -1;
        }
        //ver jogo --------- botão cancelar
        private void button8_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //ver jogador --- combobox Pesquisar por ...
        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox12.Items.Clear();   
            if(comboBox11.SelectedIndex != -1)
            {
                if (comboBox11.SelectedItem.ToString() == "Nome")
                {
                    foreach (Jogador listJogadores in jogadores.ListJogadores())
                    {
                        comboBox12.Items.Add(listJogadores.NomeJogadorCamisola);
                        comboBox12.AutoCompleteCustomSource.Add(listJogadores.NomeJogadorCamisola);
                    }
                }
                if (comboBox11.SelectedItem.ToString() == "NIF")
                {
                    foreach (Jogador listJogadores in jogadores.ListJogadores())
                    {
                        comboBox12.Items.Add(listJogadores.NIF);
                        comboBox12.AutoCompleteCustomSource.Add(listJogadores.NIF);
                    }
                }
                if (comboBox11.SelectedItem.ToString() == "Clube")
                {
                    comboBox13.Visible = true;
                }
                comboBox11.Enabled = false;
            }
            else comboBox11.Enabled = true;
            if (comboBox11.SelectedIndex != 2) comboBox12.Enabled = true;
        }
        //ver jogador ---------- combobox procurar
        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //ver Jogador ------ combobox clubes
        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox12.Enabled = true;
            comboBox12.Items.Clear();
            if (comboBox13.SelectedIndex != -1)
            {
                foreach (Jogador listJogadores in jogadores.JogadoresClube(comboBox13.SelectedIndex+1))
                {
                    if(listJogadores != null)
                    {
                        comboBox12.Items.Add(listJogadores.NomeJogadorCamisola);
                        comboBox12.AutoCompleteCustomSource.Add(listJogadores.NomeJogadorCamisola);
                    }
                }
            }
        }

        //submenu Criar Jogador
        private void criarJogadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = false;
            inicioToolStripMenuItem.Text = "Contratar um Jogador";
        }
        //Criar jogador ----------- botão cancelar
        private void button11_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //criar Jogador ------------ botão contratar
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox14.SelectedIndex == -1) throw new Exception("Nacionalidade inválido!!");
                if (comboBox16.SelectedIndex == -1) throw new Exception("Clube inválido!!");
                if (comboBox15.SelectedIndex == -1) throw new Exception("Posição inválido!!");
                Jogador newJogador = new Jogador(textBox1.Text, dateTimePicker4.Value, textBox3.Text, textBox4.Text, textBox2.Text, Double.Parse(numericUpDown10.Value.ToString()), textBox5.Text, int.Parse(numericUpDown8.Value.ToString()),
                   (TipoPosicao) comboBox15.SelectedIndex, double.Parse(numericUpDown9.Value.ToString()), dateTimePicker6.Value,
                     dateTimePicker7.Value, (Classes.Nacionalidades) comboBox14.SelectedIndex, clubesObjetos[comboBox16.SelectedIndex]);
                jogadores.InserirJogador(newJogador, comboBox16.SelectedIndex + 1, comboBox14.SelectedIndex+1, comboBox15.SelectedIndex+1);
                MessageBox.Show("Contrato Aceite");
                ClearAll();
            }
            catch(Exception erroCriarJogador)
            {
                MessageBox.Show(erroCriarJogador.Message);
            }
        }

        //submenu editarJogador
        private void editarJogadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            button13.Enabled = true;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = false;
            inicioToolStripMenuItem.Text = "Editar Jogador";
            comboBox20.Text = "clube ..";
        }
        //Editar Jogador ------------- Botão cancelar
        private void button12_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        //Editar Jogador ------------------ botão reset
        private void button16_Click(object sender, EventArgs e)
        {
            ClearAll();
            comboBox20.Text = "clube ..";
        }
        //Editar Jogador _------------------- Botão editar
        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox21.SelectedIndex == -1) throw new Exception("Jogador inválido!!");
                comboBox21.Enabled = false;
                comboBox22.Enabled = false;
                button14.Visible = false;
                groupBox9.Visible = true;
                groupBox10.Visible = true;
                button15.Visible = true;
                button13.Visible = true;
                button9.Visible = true;
                textBox10.Enabled = false;
                Jogador jogadorEditar = jogadores.dadosJogador(comboBox21.Text);
                
                comboBox17.SelectedIndex = jogadores.IdClubeJogador(comboBox21.Text)-1;
                textBox10.Text = jogadorEditar.Nome;
                textBox9.Text = jogadorEditar.Email;
                textBox8.Text = jogadorEditar.NIF;
                textBox7.Text = jogadorEditar.Contacto;
                comboBox19.SelectedIndex = numNacionalidade(jogadorEditar.NacionalidadeDoJogador.ToString())-1;
                dateTimePicker10.Value = jogadorEditar.DataNascimento;
                textBox6.Text = jogadorEditar.NomeJogadorCamisola;
                numericUpDown13.Value = jogadorEditar.NumDaCamisola;
                numericUpDown12.Value = decimal.Parse(jogadorEditar.Peso.ToString());
                numericUpDown11.Value = decimal.Parse(jogadorEditar.Salario.ToString());
                comboBox18.SelectedIndex = numPosicao(jogadorEditar.PosicaoJogador.ToString())-1;
                dateTimePicker9.Value = jogadorEditar.InicioContrato;
                dateTimePicker8.Value = jogadorEditar.FimContrato;
            }
            catch (Exception erroEditar)
            {
                MessageBox.Show(erroEditar.Message);
            }
        }
        //Editar Jogador ----------------------- combobox pesquisar por...
        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox21.Items.Clear();
            if (comboBox20.SelectedIndex != -1)
            {
                if (comboBox20.SelectedItem.ToString() == "Nome")
                {
                    foreach (Jogador listJogadores in jogadores.ListJogadores())
                    {
                        comboBox21.Items.Add(listJogadores.NomeJogadorCamisola);
                        comboBox21.AutoCompleteCustomSource.Add(listJogadores.NomeJogadorCamisola);
                    }
                }
                if (comboBox20.SelectedItem.ToString() == "NIF")
                {
                    foreach (Jogador listJogadores in jogadores.ListJogadores())
                    {
                        comboBox21.Items.Add(listJogadores.NIF);
                        comboBox21.AutoCompleteCustomSource.Add(listJogadores.NIF);
                    }
                }
                if (comboBox20.SelectedItem.ToString() == "Clube")
                {
                    comboBox22.Visible = true;
                }
                comboBox20.Enabled = false;
            }
            else comboBox20.Enabled = true;
            if (comboBox20.SelectedIndex != 2) comboBox21.Enabled = true;
        }
        //Editar jogador ----------------------------- comboBox clube ..
        private void comboBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox21.Enabled = true;
            comboBox21.Items.Clear();
            if (comboBox22.SelectedIndex != -1)
            {
                foreach (Jogador listJogadores in jogadores.JogadoresClube(comboBox22.SelectedIndex + 1))
                {
                    if(listJogadores != null)
                    {
                        comboBox21.Items.Add(listJogadores.NomeJogadorCamisola);
                        comboBox21.AutoCompleteCustomSource.Add(listJogadores.NomeJogadorCamisola);
                    }
                }
            }
        }
        //Editar jogador ------------------------------ botão despedir
        private void button15_Click(object sender, EventArgs e)
        {
            jogadores.ApagarJogador(textBox8.Text);
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
            MessageBox.Show("Jogador foi Despedido");
        }
        //editar jogador -------------------- botão Guardar
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                jogadores.AtualizarJogador(textBox7.Text, textBox9.Text, Double.Parse(numericUpDown11.Value.ToString()), textBox6.Text,
                   int.Parse(numericUpDown13.Value.ToString()), comboBox18.SelectedIndex + 1, double.Parse(numericUpDown12.Value.ToString()),
                   dateTimePicker9.Value, dateTimePicker8.Value, textBox8.Text, comboBox17.SelectedIndex + 1);
                inicioToolStripMenuItem.Text = "Inicio";
                panel1.Visible = false;
                MessageBox.Show("Jogador foi atualizado!!");
            }
            catch(Exception erroInserir)
            {
                MessageBox.Show(erroInserir.Message);
            }
    
        }
        //editar jogador --------------------- botão mudar clube
        private void button13_Click(object sender, EventArgs e)
        {
            comboBox17.Enabled = true;
            dateTimePicker9.Enabled = true;
            MessageBox.Show("Campo de clube e inicio contrato ativado");
            button13.Enabled = false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   Dirigente                                                           ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //submenu Ver Dirigente
        private void verDirigenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = false;
            inicioToolStripMenuItem.Text = "Ver Dirigente";
        }
        //ver Dirigente --------------------------- botão resultado
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox24.SelectedIndex != -1)
                {
                    comboBox24.Enabled = false;
                    comboBox23.Enabled = false;
                    button18.Visible = false;

                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = dataTable.dataGridDirigente(comboBox24.Text);
                }
                else throw new Exception("Dirigente inválido!!");
            }
            catch (Exception erroVerDirigente)
            {
                MessageBox.Show(erroVerDirigente.Message);
            }

        }
        //Ver dirigente ------------------- botão cancelar
        private void button19_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //Ver dirigente ------------------- botão reset
        private void button17_Click(object sender, EventArgs e)
        {
            ClearAll();
            comboBox24.SelectedIndex = -1;
        }
        //ver Dirigente --------------------- combobox pesquisar por..
        private void comboBox25_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox24.Items.Clear();
            if (comboBox25.SelectedIndex != -1)
            {
                if (comboBox25.SelectedItem.ToString() == "Nome")
                {
                    foreach (Dirigente listDirigentes in dirigentes.ListDirigentes())
                    {
                        comboBox24.Items.Add(listDirigentes.Nome);
                        comboBox24.AutoCompleteCustomSource.Add(listDirigentes.Nome);
                    }
                }
                if (comboBox25.SelectedItem.ToString() == "NIF")
                {
                    foreach (Dirigente listDirigentes in dirigentes.ListDirigentes())
                    {
                        comboBox24.Items.Add(listDirigentes.NIF);
                        comboBox24.AutoCompleteCustomSource.Add(listDirigentes.NIF);
                    }
                }
                if (comboBox25.SelectedItem.ToString() == "Clube")
                {
                    comboBox23.Visible = true;
                }
                comboBox25.Enabled = false;
            }
            else comboBox25.Enabled = true;
            if (comboBox25.SelectedIndex != 2) comboBox24.Enabled = true;
        }
        // ver Dirigente ........................ combobox clubes
        private void comboBox23_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox24.Enabled = true;
            comboBox24.Items.Clear();
            if (comboBox23.SelectedIndex != -1)
            {
                foreach (Dirigente listDirigentes in dirigentes.DirigenteClube(comboBox23.SelectedIndex + 1))
                {
                    if(listDirigentes != null)
                    {
                        comboBox24.Items.Add(listDirigentes.Nome);
                        comboBox24.AutoCompleteCustomSource.Add(listDirigentes.Nome);
                    }
                }
            }

        }
        private void comboBox24_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Submenu Contratar Dirigente
        private void criarDirigenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = false;
            inicioToolStripMenuItem.Text = "Contratar Dirigente";
        }
        //Contratar Dirigente -------------------- botão cancelar
        private void button21_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //contratar Dirigente --------------------- botão contratar
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox26.SelectedIndex == -1) throw new Exception("Tipo Dirigente inválido!!");
                if (comboBox27.SelectedIndex == -1) throw new Exception("Clube inválido!!");
                
                Dirigente newDirigente = new Dirigente(textBox15.Text, dateTimePicker13.Value, textBox13.Text, textBox12.Text, textBox14.Text,
                    double.Parse(numericUpDown14.Value.ToString()), (TipoDir) comboBox26.SelectedIndex, dateTimePicker12.Value, dateTimePicker11.Value,
                    int.Parse(numericUpDown15.Value.ToString()), clubesObjetos[comboBox27.SelectedIndex]);

                dirigentes.InserirDirigente(newDirigente, comboBox26.SelectedIndex+1, comboBox27.SelectedIndex+1);
                
                MessageBox.Show("Contrato Aceite");
                ClearAll();
            }
            catch (Exception erroCriarJogador)
            {
                MessageBox.Show(erroCriarJogador.Message);
            }
        }

        //submenu Editar dirigente
        private void editarDirigenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = false;
            inicioToolStripMenuItem.Text = "Editar Dirigente";
        }
        //Editar dirigente ------------------ botão cancelar
        private void button25_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        //Editar dirigente ------------------ botão reset
        private void button23_Click(object sender, EventArgs e)
        {
            ClearAll();
            button29.Enabled = true;
        }
        //Editar dirigente ---------------------- comboBox pesquisar por....
        private void comboBox30_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox29.Items.Clear();
            if (comboBox30.SelectedIndex != -1)
            {
                if (comboBox30.SelectedItem.ToString() == "Nome")
                {
                    foreach (Dirigente listDirigentes in dirigentes.ListDirigentes())
                    {
                        comboBox29.Items.Add(listDirigentes.Nome);
                        comboBox29.AutoCompleteCustomSource.Add(listDirigentes.Nome);
                    }
                }
                if (comboBox30.SelectedItem.ToString() == "NIF")
                {
                    foreach (Dirigente listDirigentes in dirigentes.ListDirigentes())
                    {
                        comboBox29.Items.Add(listDirigentes.NIF);
                        comboBox29.AutoCompleteCustomSource.Add(listDirigentes.NIF);
                    }
                }
                if (comboBox30.SelectedItem.ToString() == "Clube")
                {
                    comboBox28.Visible = true;
                    comboBox28.Enabled = true;
                }
                comboBox30.Enabled = false;
            }
            else comboBox30.Enabled = true;
            if (comboBox30.SelectedIndex != 2) comboBox29.Enabled = true;

        }
        //Editar dirigente ------------------------- combobox clubes ..
        private void comboBox28_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox29.Enabled = true;
            comboBox29.Items.Clear();
            if (comboBox28.SelectedIndex != -1)
            {
                foreach (Dirigente listDirigentes in dirigentes.DirigenteClube(comboBox28.SelectedIndex + 1))
                {
                    if(listDirigentes != null)
                    {
                        comboBox29.Items.Add(listDirigentes.Nome);
                        comboBox29.AutoCompleteCustomSource.Add(listDirigentes.Nome);
                    }
                }
            }

        }
        //Editar dirigente -------------------------- botão editar
        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox29.SelectedIndex == -1) throw new Exception("Dirigente inválido!!");
                comboBox29.Enabled = false;
                comboBox28.Enabled = false;
                button24.Visible = false;
                groupBox16.Visible = true;
                groupBox17.Visible = true;
                button22.Visible = true;
                button29.Visible = true;
                button30.Visible = true;
                textBox10.Enabled = false;
                textBox18.Enabled = false;
                textBox16.Enabled = false;
                dateTimePicker16.Enabled = false;
                dateTimePicker15.Enabled = false;
                comboBox31.Enabled = false;
                // prencher campos
                Dirigente dadoDirigente = dirigentes.DadosDirigente(comboBox29.Text);

                comboBox31.SelectedIndex = dirigentes.IdClubeDirigente(comboBox29.Text)-1;
                textBox18.Text = dadoDirigente.Nome;
                textBox17.Text = dadoDirigente.Email;
                textBox16.Text = dadoDirigente.NIF;
                textBox11.Text = dadoDirigente.Contacto;
                dateTimePicker16.Value = dadoDirigente.DataNascimento;
                numericUpDown16.Value = decimal.Parse(dadoDirigente.Salario.ToString());
                dateTimePicker15.Value = dadoDirigente.InicioContrato;
                dateTimePicker14.Value = dadoDirigente.FimContrato;
                numericUpDown17.Value = dadoDirigente.NumCarteira;
                comboBox32.SelectedIndex = numTipoDir(dadoDirigente.TipoDirigente.ToString())-1;
            }
            catch (Exception erroEditar)
            {
                MessageBox.Show(erroEditar.Message);
            }
        }
        //Editar Dirigente --------------------------- botão despedir
        private void button22_Click(object sender, EventArgs e)
        {
            dirigentes.ApagarDirigente(textBox16.Text);
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
            MessageBox.Show("Dirigente foi Despedido");

        }
        //Editar dirigente ---------------------------- botão Mudar clube
        private void button29_Click(object sender, EventArgs e)
        {
            comboBox31.Enabled = true;
            dateTimePicker15.Enabled = true;
            MessageBox.Show("Campo de clube e inicio contrato ativado");
            button29.Enabled = false;
        }
        //Editar Dirigente ------------------------------ botão guardar
        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                dirigentes.AtualizarDirigente(textBox11.Text, textBox17.Text, double.Parse(numericUpDown16.Value.ToString()), comboBox32.SelectedIndex + 1,
                  dateTimePicker15.Value, dateTimePicker14.Value, int.Parse(numericUpDown17.Value.ToString()), comboBox31.SelectedIndex + 1, textBox16.Text);
                inicioToolStripMenuItem.Text = "Inicio";
                panel1.Visible = false;
                MessageBox.Show("Dirigente foi atualizado!!");
            }
            catch (Exception erroAtualizar)
            {
                MessageBox.Show(erroAtualizar.Message);
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   Tecnico                                                             ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //submenu ver tecnico
        private void verTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = false;
            inicioToolStripMenuItem.Text = "Ver Técnico";
        }
        //Ver tecnico -------------------------------- botao cancelar
        private void button28_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //ver tecnico ------------------------------- botao reset
        private void button26_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        //ver tecnico --------------------------------combobox pesquisar por
        private void comboBox35_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox34.Items.Clear();
            if (comboBox35.SelectedIndex != -1)
            {
                if (comboBox35.SelectedItem.ToString() == "Nome")
                {
                    foreach (Tecnico listTecnicos in tecnicos.ListTecnicos())
                    {
                        comboBox34.Items.Add(listTecnicos.Nome);
                        comboBox34.AutoCompleteCustomSource.Add(listTecnicos.Nome);
                    }
                }
                if (comboBox35.SelectedItem.ToString() == "NIF")
                {
                    foreach (Tecnico listTecnicos in tecnicos.ListTecnicos())
                    {
                        comboBox34.Items.Add(listTecnicos.NIF);
                        comboBox34.AutoCompleteCustomSource.Add(listTecnicos.NIF);
                    }
                }
                if (comboBox35.SelectedItem.ToString() == "Clube")
                {
                    comboBox33.Visible = true;
                }
                comboBox35.Enabled = false;
            }
            else comboBox35.Enabled = true;
            if (comboBox35.SelectedIndex != 2) comboBox34.Enabled = true;

        }
        //ver tecnico -------------------------------- combobox Clube
        private void comboBox33_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox34.Enabled = true;
            comboBox34.Items.Clear();
            if (comboBox33.SelectedIndex != -1)
            {
                foreach (Tecnico listTecnicos in tecnicos.TecnicoClube(comboBox33.SelectedIndex + 1))
                {
                    if(listTecnicos != null)
                    {
                        comboBox34.Items.Add(listTecnicos.Nome);
                        comboBox34.AutoCompleteCustomSource.Add(listTecnicos.Nome);
                    }
                }
            }
        }
        //ver Tecnico -------------------------------- botao resultado
        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox34.SelectedIndex != -1)
                {
                    button27.Visible = false;
                    comboBox34.Enabled = false;
                    comboBox33.Enabled = false;
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = dataTable.dataGridTecnico(comboBox34.Text);
                }
                else throw new Exception("Jogador inválido!!");

            }
            catch (Exception erroVerTecnico)
            {
                MessageBox.Show(erroVerTecnico.Message);
            }
        }

        //submenu Contratar tecnico
        private void criarTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = true;
            panel11.Visible = false;
            inicioToolStripMenuItem.Text = "Contratar Técnico";
        }
        //contratar tecnico ------------------------------- botão cancelar
        private void button32_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            ClearAll();
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //contratar tecnico ---------------------------------- botão contratar
        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox37.SelectedIndex == -1) throw new Exception("Tipo Técnico inválido!!");
                if (comboBox36.SelectedIndex == -1) throw new Exception("Clube inválido!!");

                Tecnico newTecnico = new Tecnico(textBox22.Text, dateTimePicker19.Value,textBox20.Text, textBox19.Text, textBox21.Text, int.Parse(numericUpDown18.Value.ToString()),
                    (TipoTec) comboBox37.SelectedIndex, dateTimePicker18.Value, dateTimePicker17.Value, int.Parse(numericUpDown20.Value.ToString()), 
                    int.Parse(numericUpDown19.Value.ToString()), clubesObjetos[comboBox36.SelectedIndex]);
                tecnicos.InserirTecnico(newTecnico, comboBox37.SelectedIndex+1,comboBox36.SelectedIndex+1);

                MessageBox.Show("Contrato Aceite");
                ClearAll();

            }
            catch (Exception erroContratarTecnico)
            {
                MessageBox.Show(erroContratarTecnico.Message);
            }
        }

        //submenu Editar Tecnico
        private void editarTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = true;
            panel11.Visible = true;
            panel12.Visible = false;
            inicioToolStripMenuItem.Text = "Editar Técnico";
        }
        //Editar Tecnico --------------------------- botão cacelar
        private void button38_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //Editar Tecnico --------------------------- botão reset
        private void button36_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        //Editar tecnico --------------------------- combobox pesquisar por ...
        private void comboBox42_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox41.Items.Clear();
            if (comboBox42.SelectedIndex != -1)
            {
                if (comboBox42.SelectedItem.ToString() == "Nome")
                {
                    foreach (Tecnico listTecnicos in tecnicos.ListTecnicos())
                    {
                        comboBox41.Items.Add(listTecnicos.Nome);
                        comboBox41.AutoCompleteCustomSource.Add(listTecnicos.Nome);
                    }
                }
                if (comboBox42.SelectedItem.ToString() == "NIF")
                {
                    foreach (Tecnico listTecnicos in tecnicos.ListTecnicos())
                    {
                        comboBox41.Items.Add(listTecnicos.Nome);
                        comboBox41.AutoCompleteCustomSource.Add(listTecnicos.Nome);
                    }
                }
                if (comboBox42.SelectedItem.ToString() == "Clube")
                {
                    comboBox40.Visible = true;
                    comboBox40.Enabled = true;
                }
                comboBox42.Enabled = false;
            }
            else comboBox42.Enabled = true;
            if (comboBox42.SelectedIndex != 2) comboBox41.Enabled = true;

        }
        //Editar tecnico ----------------------------- combobox clube
        private void comboBox40_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox41.Enabled = true;
            comboBox41.Items.Clear();

            if (comboBox40.SelectedIndex != -1)
            {
                foreach (Tecnico listTecnicos in tecnicos.TecnicoClube(comboBox40.SelectedIndex + 1))
                {
                    if(listTecnicos != null)
                    {
                        comboBox41.Items.Add(listTecnicos.Nome);
                        comboBox41.AutoCompleteCustomSource.Add(listTecnicos.Nome);
                    }
                }
            }
        }
        //Editar tecnico ------------------------------butão Editar
        private void button37_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox41.SelectedIndex == -1) throw new Exception("Técnico inválido!!");
                comboBox41.Enabled = false;
                comboBox38.Enabled = false;
                comboBox40.Enabled = false;
                dateTimePicker21.Enabled = false;
                dateTimePicker22.Enabled = false;
                button37.Visible = false;
                textBox24.Enabled = false;
                textBox26.Enabled = false;
                groupBox21.Visible = true;
                groupBox22.Visible = true;
                button33.Visible = true;
                button34.Visible = true;
                button35.Visible = true;

                // prencher campos
                Tecnico DadosTecnicos = tecnicos.DadosTecnico(comboBox41.Text);
                comboBox38.SelectedIndex = tecnicos.IdClubeTecnico(comboBox41.Text)-1;
                textBox26.Text = DadosTecnicos.Nome;
                textBox25.Text = DadosTecnicos.Email;
                textBox24.Text = DadosTecnicos.NIF;
                textBox23.Text = DadosTecnicos.Contacto;
                dateTimePicker22.Value = DadosTecnicos.DataNascimento;
                numericUpDown22.Value = decimal.Parse(DadosTecnicos.Salario.ToString());
                dateTimePicker21.Value = DadosTecnicos.InicioContrato;
                dateTimePicker20.Value = DadosTecnicos.FimContrato;
                numericUpDown23.Value = DadosTecnicos.NumCarteira;
                comboBox43.SelectedIndex = DadosTecnicos.GrauTecnico-1;
                comboBox39.SelectedIndex = numTipoTec(DadosTecnicos.TipoTecnico.ToString()) - 1;

            }
            catch (Exception erroEditar)
            {
                MessageBox.Show(erroEditar.Message);
            }
        }
        //Editar Tecnico ------------------------------- botao Despedir
        private void button33_Click(object sender, EventArgs e)
        {
            tecnicos.ApagarTecnico(textBox24.Text);
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
            MessageBox.Show("Técnico foi Despedido");

        }
        //Editar Tecnico -------------------------------- botao Mudar clube
        private void button34_Click(object sender, EventArgs e)
        {
            comboBox38.Enabled = true;
            dateTimePicker21.Enabled = true;
            MessageBox.Show("Campo de clube e inicio contrato ativado");
            button34.Enabled = false;
        }
        //editar tecnico .------------------------------- botao guardar
        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                tecnicos.AtualizarTecnico(textBox23.Text, textBox25.Text, double.Parse(numericUpDown22.Value.ToString()),
                    comboBox39.SelectedIndex + 1, dateTimePicker21.Value, dateTimePicker20.Value, int.Parse(numericUpDown23.Value.ToString()),
                     comboBox38.SelectedIndex + 1, comboBox43.SelectedIndex+1, textBox24.Text);
                inicioToolStripMenuItem.Text = "Inicio";
                panel1.Visible = false;
                MessageBox.Show("Técnico foi atualizado!!");
            }
            catch (Exception erroAtualizar)
            {
                MessageBox.Show(erroAtualizar.Message);
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                   Menu                                   Clubes                                                               ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //submenu Ver clube
        private void verClubeDeFutebolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = true;
            panel11.Visible = true;
            panel12.Visible = true;
            panel13.Visible = false;
            inicioToolStripMenuItem.Text = "Ver Clube";
        }
        //Ver Clube ------------------------------------- botão cancelar
        private void button41_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            inicioToolStripMenuItem.Text = "Inicio";
        }
        //ver Clube ---------------------------------------- botão reset
        private void button39_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        //ver Clube ---------------------------------------- botão Resultado
        private void button40_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox44.Enabled = true;
                button40.Visible = false;
                if (comboBox44.SelectedIndex == -1) throw new Exception("Clube inválido");
                dataGridView1.Visible = true;
                dataGridView1.DataSource = dataTable.dataGridClube(comboBox44.Text);
            }
            catch (Exception erroVerTecnico)
            {
                MessageBox.Show(erroVerTecnico.Message);
            }

        }
        private void comboBox44_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //submenu criar Clube
        private void criarClubeDeFutebolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = true;
            panel11.Visible = true;
            panel12.Visible = true;
            panel13.Visible = true;
            panel14.Visible = false;
            inicioToolStripMenuItem.Text = "Criar um Clube";
        }
        //Criar clube ----------------------- botão cancelar
        private void button43_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        //Criar clube -------------------------- datetime data fundação
        private void dateTimePicker26_ValueChanged(object sender, EventArgs e)
        {
        }
        //Criar Clube ---------------------------- botão concluir
        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                ClubeDeFutebol novoClube = new ClubeDeFutebol(textBox34.Text, dateTimePicker26.Value, textBox33.Text, int.Parse(numericUpDown25.Value.ToString()));
                clubes.AdicionarClube(novoClube);

                clubesObjetos.Add(novoClube);
                comboBox1.Items.Add(novoClube.NomeClube);
                comboBox1.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox2.Items.Add(novoClube.NomeClube);
                comboBox2.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox3.Items.Add(novoClube.NomeClube);
                comboBox3.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox4.Items.Add(novoClube.NomeClube);
                comboBox4.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox5.Items.Add(novoClube.NomeClube);
                comboBox5.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox6.Items.Add(novoClube.NomeClube);
                comboBox6.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox7.Items.Add(novoClube.NomeClube);
                comboBox7.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox10.Items.Add(novoClube.NomeClube);
                comboBox10.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox16.Items.Add(novoClube.NomeClube);
                comboBox16.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox22.Items.Add(novoClube.NomeClube);
                comboBox22.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox17.Items.Add(novoClube.NomeClube);
                comboBox17.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox27.Items.Add(novoClube.NomeClube);
                comboBox27.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox31.Items.Add(novoClube.NomeClube);
                comboBox31.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox36.Items.Add(novoClube.NomeClube);
                comboBox36.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox44.Items.Add(novoClube.NomeClube);
                comboBox44.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox13.Items.Add(novoClube);
                comboBox13.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox23.Items.Add(novoClube.NomeClube);
                comboBox23.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox28.Items.Add(novoClube.NomeClube);
                comboBox28.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox33.Items.Add(novoClube.NomeClube);
                comboBox33.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox40.Items.Add(novoClube.NomeClube);
                comboBox40.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox38.Items.Add(novoClube.NomeClube);
                comboBox38.AutoCompleteCustomSource.Add(novoClube.NomeClube);
                comboBox45.Items.Add(novoClube.NomeClube);
                comboBox45.AutoCompleteCustomSource.Add(novoClube.NomeClube);

                panel1.Visible = false;
                inicioToolStripMenuItem.Text = "Inicio";
                MessageBox.Show("Clube adicionado com sucesso");
            }
            catch (Exception erroClube)
            {
                MessageBox.Show(erroClube.Message);
            }
            
        }
        //SUBMENU editar clube
        private void editarClubeDeFutebolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            panel9.Visible = true;
            panel10.Visible = true;
            panel11.Visible = true;
            panel12.Visible = true;
            panel13.Visible = true;
            panel14.Visible = true;
            inicioToolStripMenuItem.Text = "Editar um Clube";
        }
        //editar clube   ------------------------------------- botão cancear
        private void button49_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        //editar clube   ------------------------------------- botão cancear
        private void button44_Click(object sender, EventArgs e)
        {
            inicioToolStripMenuItem.Text = "Inicio";
            panel1.Visible = false;
        }
        //editar clube   ------------------------------------- botão editar
        private void button48_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox45.SelectedIndex == -1) throw new Exception("Clube inválido");
                else
                {
                    groupBox25.Visible = true;
                    button46.Visible = true;
                    button44.Visible = true;
                    button48.Visible = false;
                    textBox29.Text = "";
                    comboBox45.Enabled = false;
                    numericUpDown21.Value = 1;

                    ClubeDeFutebol clubeEditar = clubes.DadosClube(comboBox45.SelectedIndex+1);

                    textBox30.Text = clubeEditar.NomeClube;
                    textBox29.Text = clubeEditar.NomeDoEstadio;
                    numericUpDown21.Value = clubeEditar.DivicaoClube;
                    dateTimePicker23.Value = clubeEditar.DataDeFundacao;
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        //editar clube   ------------------------------------- botão guardar
        private void button46_Click(object sender, EventArgs e)
        {
            try
            {
                clubes.AtualizarClube(textBox30.Text, textBox29.Text, int.Parse(numericUpDown21.Value.ToString()), comboBox45.SelectedIndex + 1);
                panel1.Visible = false;
                inicioToolStripMenuItem.Text = "Inicio";
                MessageBox.Show("Atuaizado com sucesso");
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                                                          Métodos                                                              ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //metodo para apgar os dados todos dos campos que devem ser apagados 
        public void ClearAll()
        {
            //aqui iremos apagar tudo de uma vez(os campos de preencher isto para dar menos trabalho depois)
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            //deixar em branco campos de Editar clube
            groupBox25.Visible = false;
            button46.Visible = false;
            button44.Visible = false;
            button48.Visible = true;
            textBox30.Text = "";
            textBox29.Text = "";
            comboBox45.SelectedIndex = -1;
            comboBox45.Text = "clube ..";
            comboBox45.Enabled = true;
            numericUpDown21.Value = 1;
            //deixar em branco campos de criar um clube 
            textBox34.Text = "";
            textBox33.Text = "";
            numericUpDown25.Value = 1;
            dateTimePicker26.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //deixar em branco os campos ver clube
            button40.Visible = true;
            comboBox44.Enabled = true;
            comboBox44.SelectedIndex = -1;
            comboBox44.Text = "Clube";
            label95.Text = "";
            label96.Text = "";
            //deixar em branco os campos editar o tecnico
            comboBox42.Enabled = true;
            comboBox41.Enabled = false;
            comboBox40.Visible = false;
            comboBox40.SelectedIndex = -1;
            comboBox41.SelectedIndex = -1;
            comboBox42.Text = "Pesquisar por ..";
            comboBox41.Text = "Procurar ..";
            comboBox40.Text = "Clube ..";
            groupBox22.Visible = false;
            groupBox21.Visible = false;
            button33.Visible = false;
            button34.Visible = false;
            button35.Visible = false;
            button37.Visible = true;
            button34.Enabled = true;
            //deixar em branco os campos de contratar tecnico
            textBox22.Text = "";
            textBox21.Text = "";
            textBox20.Text = "";
            textBox19.Text = "";
            numericUpDown18.Value = 600;
            dateTimePicker13.Value = new DateTime((DateTime.Now.Year - 17), DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker11.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker12.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            numericUpDown20.Value = 1;
            numericUpDown19.Value = 1;
            comboBox37.Text = "";
            comboBox36.Text = "";
            comboBox37.SelectedIndex = -1;
            comboBox36.SelectedIndex = -1;
            //deixar em branco os campos de ver tecnico
            comboBox35.SelectedIndex = -1;
            comboBox35.Text = "Pesquisar por ..";
            comboBox35.Enabled = true;
            comboBox33.Enabled = true;
            comboBox33.Visible = false;
            comboBox33.SelectedIndex = -1;
            comboBox33.Text = "Clube ..";
            comboBox34.SelectedIndex = -1;
            comboBox34.Text = "Procurar ..";
            label70.Text = "";
            button27.Visible = true;
            comboBox34.Enabled = false;
            //deixar em branco os campos de editar dirigente
            button24.Visible = true;
            groupBox17.Visible = false;
            groupBox16.Visible = false;
            button22.Visible = false;
            button29.Visible = false;
            button30.Visible = false;
            comboBox29.SelectedIndex = -1;
            comboBox28.SelectedIndex = -1;
            comboBox28.Visible = false;
            comboBox30.SelectedIndex = -1;
            comboBox30.Text = "Pesquisar por ..";
            comboBox30.Enabled = true;
            comboBox29.Enabled = false;
            comboBox30.Text = "Pesquisar por ..";
            comboBox29.Text = "Procurar ..";
            //deixar em branco os campos de Contratar dirigente
            comboBox26.SelectedIndex = -1;
            comboBox26.Text = "";
            comboBox27.SelectedIndex = -1;
            comboBox27.Text = "";
            dateTimePicker13.Value = new DateTime((DateTime.Now.Year - 17), DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker11.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker12.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            textBox15.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            numericUpDown14.Value = 600;
            numericUpDown15.Value = 1;
            //deixar em branco os campos de Ver Dirigente
            label53.Text = "";
            comboBox25.SelectedIndex = -1;
            comboBox25.Enabled = true;
            comboBox25.Text = "Pesquisar por ..";
            comboBox23.Visible = false;
            comboBox23.Enabled = true;
            comboBox23.Text = "Clube ..";
            comboBox23.SelectedIndex = -1;
            comboBox24.Text = "Procurar ..";
            comboBox24.Enabled = false;
            comboBox24.SelectedIndex = -1;
            button18.Visible = true;
            //deixar em branco as partes de editar jogador
            comboBox17.Enabled = false;
            dateTimePicker9.Enabled = false;
            comboBox20.SelectedIndex = -1;
            comboBox20.Enabled = true;
            comboBox20.Text = "Pesquisar por ..";
            comboBox22.Visible = false;
            comboBox22.Text = "Clube ..";
            comboBox22.SelectedIndex = -1;
            comboBox21.Text = "Procurar ..";
            comboBox21.Enabled = false;
            comboBox21.SelectedIndex = -1;
            button14.Visible = true;
            comboBox22.Enabled = true;
            groupBox9.Visible = false;
            groupBox10.Visible = false;
            button15.Visible = false;
            button13.Visible = false;
            button13.Enabled = true;
            button9.Visible = false;
            //deixar em branco as partes de criar jogador
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox14.SelectedIndex = -1;
            comboBox14.Text = "";
            comboBox15.SelectedIndex = -1;
            comboBox15.Text = "";
            comboBox16.SelectedIndex = -1;
            comboBox16.Text = "";
            numericUpDown8.Value = 1;
            numericUpDown10.Value = 600;
            numericUpDown9.Value = 35;
            dateTimePicker4.Value = new DateTime((DateTime.Now.Year - 17), DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker6.Value = DateTime.Now;
            dateTimePicker7.Value = DateTime.Now;
            //deixar em branco as partes de ver jogador
            comboBox12.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
            comboBox13.SelectedIndex = -1;
            comboBox13.Enabled = true;
            label18.Text = "";
            comboBox11.Text = "Pesquisar por ..";
            comboBox12.Text = "Procurar ..";
            comboBox13.Text = "clube ..";
            comboBox13.Visible = false;
            comboBox12.Enabled = false;
            comboBox11.Enabled = true;
            button7.Visible = true;
            //deixar em branco pertence ao ver torneio
            label1.Text = "";
            comboBox9.SelectedIndex = -1;
            comboBox9.Enabled = true;
            comboBox9.Text = "Pesquisar por datas";
            comboBox10.Text = "Pesquisar por Clubes";
            //deixar em branco as partes de criar um torneio
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 500;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
        }

        //para combobox
        public int numNacionalidade(string nacionalidadeValue)
        {
            int contador = 0;
            List<string> nacionalidades = new List<string>();
            nacionalidades.Add("Argentino");
            nacionalidades.Add("Alemão");
            nacionalidades.Add("Australiano");
            nacionalidades.Add("Americano");
            nacionalidades.Add("Brasileiro");
            nacionalidades.Add("Chinês");
            nacionalidades.Add("Japonês");
            nacionalidades.Add("Espanhol");
            nacionalidades.Add("Islandês");
            nacionalidades.Add("Português");
            nacionalidades.Add("Outro");
            
            foreach(string listNacio in nacionalidades)
            {
                if(nacionalidadeValue == listNacio)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
        public int numPosicao(string tipoPosicoValue)
        {
            int contador = 0;
            List<string> tipoPosicao = new List<string>();
            tipoPosicao.Add("Guarda_Redes");
            tipoPosicao.Add("Defesa");
            tipoPosicao.Add("Meio_Campo");
            tipoPosicao.Add("Atacante");

            foreach (string posicao in tipoPosicao)
            {
                if (tipoPosicoValue == posicao)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
        
        public int numTipoTec(string tipoTecValue)
        {
            int contador = 0;
            List<string> tipoTec = new List<string>();
            tipoTec.Add("Treinador");
            tipoTec.Add("Massagista");
            tipoTec.Add("Treinador_Pessoal");
            tipoTec.Add("Treinador_Adjunto");
            tipoTec.Add("Outro");

            foreach (string tiptecnico in tipoTec)
            {
                if (tipoTecValue == tiptecnico)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
        public int numPais(string paisValue)
        {
            int contador = 0;
            List<string> pais = new List<string>();
            pais.Add("Argentina");
            pais.Add("Alemanha");
            pais.Add("Áustria");
            pais.Add("América");
            pais.Add("Brasil");
            pais.Add("China");
            pais.Add("Japão");
            pais.Add("Espanha");
            pais.Add("Irlanda");
            pais.Add("Portugal");
            pais.Add("Inglaterra");
            pais.Add("Outro");

            foreach (string paisT in pais)
            {
                if (paisValue == paisT)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
        public int numTipoDir(string tipoDirValue)
        {
            int contador = 0;
            List<string> tipoDir = new List<string>();
            tipoDir.Add("Presidente");
            tipoDir.Add("Vice_Presidente");

            foreach (string tipoD in tipoDir)
            {
                if (tipoDirValue == tipoD)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
    }
}