using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    enum TipoDir
    {
        Presidente = 0,
        Vice_Presidente = 1
    }

    class Dirigente : Elemento
    {
        private TipoDir tipoDirigente;

        public TipoDir TipoDirigente
        {
            get { return tipoDirigente; }
            set { tipoDirigente = value; }
        }

        private DateTime inicioContrato;

        public DateTime InicioContrato
        {
            get { return inicioContrato; }
            set
            {
                if (value > DateTime.Now || (DateTime.Now - DataNascimento).Days / 365 > 90 || (DateTime.Now - DataNascimento).Days / 365 < 18)
                {
                    throw new Exception("Data de inicio contrato inválida do dirigente " + Nome + " ,não pode ser maior que o dia de hoje" +
                        " e tem de ser entre 18-90");
                }
                else
                {
                    inicioContrato = value;
                }
            }
        }

        private DateTime fimContrato;

        public DateTime FimContrato
        {
            get { return fimContrato; }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new Exception("Data de fim de contrato inválido do dirigente " + Nome + " ,tem de ser superior ao dia de hoje");
                }
                else
                {
                    fimContrato = value;
                }
            }
        }

        private int numCarteira;

        public int NumCarteira
        {
            get { return numCarteira; }
            set
            {
                if (value > 9999 && value < 0)
                {
                    throw new Exception("Número de carteira do " + Nome + " ,inválido tem de estar entre 1-9999");
                }
                else
                {
                    numCarteira = value;
                }
            }
        }

        private ClubeDeFutebol clubeDirigente;

        public ClubeDeFutebol ClubeDirigente
        {
            get { return clubeDirigente; }
            set { clubeDirigente = value; }
        }

        public Dirigente(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue, double salarioValue
          , TipoDir tipoDirigenteValue, DateTime inicioContratoValue, DateTime fimContratoValue, int numCarteiraValue, ClubeDeFutebol clubeValue)
          : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 90) throw new Exception("Fim de contrato inválido não pode exceder os 90 anos");
            TipoDirigente = tipoDirigenteValue;
            InicioContrato = inicioContratoValue;
            FimContrato = fimContratoValue;
            NumCarteira = numCarteiraValue;
            ClubeDirigente = clubeValue;
        }

        public Dirigente(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue, double salarioValue
        , TipoDir tipoDirigenteValue, DateTime inicioContratoValue, DateTime fimContratoValue, int numCarteiraValue)
        : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 90) throw new Exception("Fim de contrato inválido não pode exceder os 90 anos");
            TipoDirigente = tipoDirigenteValue;
            InicioContrato = inicioContratoValue;
            FimContrato = fimContratoValue;
            NumCarteira = numCarteiraValue;
        }


        public override string ToString()
        {
            return base.ToString() + " " + TipoDirigente + " " + NumCarteira;
        }
    }
}
