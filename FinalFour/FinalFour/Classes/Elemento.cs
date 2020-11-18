using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    abstract class Elemento
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set
            {
                if (value.Trim() == string.Empty)
                {
                    throw new Exception("Nome inválido por favor não deixe o nome em branco");
                }
                else
                {
                    if (value == null || value.Trim().Split(' ').Length < 2)
                    {
                        throw new Exception("Nome invalido do " + value + " tem de ter pelo menos dois nomes apelido e nome proprio");
                    }
                    else
                    {
                        string[] verificarNome = value.Trim().Split(' ');
                        string nomeDireito = "";
                        for (int palavra = 0; palavra < verificarNome.Length; palavra++)
                        {
                            for (int letra = 0; letra < verificarNome[palavra].Length; letra++)
                            {
                                if (!Char.IsLetter(verificarNome[palavra][letra]))
                                {
                                    throw new Exception("O nome só pode conter letras");
                                }
                                if (letra == 0)
                                {
                                    nomeDireito += verificarNome[palavra][letra].ToString().ToUpper();
                                }
                                else
                                {
                                    nomeDireito += verificarNome[palavra][letra];
                                }
                            }
                            nomeDireito += " ";
                        }
                        nome = nomeDireito.Trim();
                    }
                }
            }
        }

        private DateTime dataNascimento;

        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set
            {
                if (value > DateTime.Now || (DateTime.Now - value).Days / 365 < 17)
                {
                    throw new Exception("Data de nascimento do " + Nome + " ,inválido não pode ser menor de 17.");
                }
                else
                {
                    dataNascimento = value;
                }
            }
        }

        private string nif;

        public string NIF
        {
            get { return nif; }
            set
            {
                if (value.Length == 9)
                {
                    for (int i = value.Length - 1; i > 0; i--)
                    {
                        if (!char.IsNumber(value[i]))
                        {
                            throw new Exception("NIF do " + Nome + " inválido só pode conter números");
                        }
                    }
                    nif = value;
                }
                else
                {
                    throw new Exception("NIF inválido do " + Nome + " tem de ter 9 números");
                }
            }
        }

        private string contacto;

        public string Contacto
        {
            get { return contacto; }
            set
            {
                if (value.Length == 9 && value[0] == '9')
                {
                    for (int i = value.Length - 1; i > 0; i--)
                    {
                        if (!char.IsNumber(value[i]))
                        {
                            throw new Exception("NIF do " + Nome + " inválido ó número tem de ser números");
                        }
                    }
                    contacto = value;
                }
                else
                {
                    throw new Exception("Contacto inválido do " + Nome + " tem de ter 9 números");
                }
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    email = value;
                }
                else
                {
                    throw new Exception("Email do " + Nome + " ,inválido");
                }
            }
        }

        private double salario;

        public double Salario
        {
            get { return salario; }
            set
            {
                if (value < 600)
                {
                    throw new Exception("Salario do " + Nome + " ,inválido tem de ser maior que o salario minimo");
                }
                else
                {
                    salario = Math.Round(value, 2);
                }
            }
        }

        public Elemento(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue, double salarioValue)
        {
            Nome = nomeValue;
            DataNascimento = dtNascimentoValue;
            NIF = nifValue;
            Contacto = contactoValue;
            Email = emailValue;
            Salario = salarioValue;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
