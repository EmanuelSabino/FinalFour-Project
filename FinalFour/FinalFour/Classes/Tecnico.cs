using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFour.Classes
{
    enum TipoTec
    {
        Treinador = 0,
        Massagista = 1,
        Treinador_Pessoal = 2,
        Treinador_Adjunto = 3,
        Outro = 4
    } 

    class Tecnico : Elemento
    {
        private TipoTec tipoTecnico;

        public TipoTec TipoTecnico
        {
            get { return tipoTecnico; }
            set { tipoTecnico = value; }
        }

        private DateTime inicioContrato;

        public DateTime InicioContrato
        {
            get { return inicioContrato; }
            set
            {
                if (value > DateTime.Now || (DateTime.Now - DataNascimento).Days / 365 < 18 || (DateTime.Now - DataNascimento).Days / 365 > 70)
                {
                    throw new Exception("Data de inicio contrato inválida do tecnico " + Nome + " ,não pode ser maior que o dia de hoje" +
                        " e tem de ser entre 18-70");
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
                    throw new Exception("Data de fim de contrato inválida do tecnico " + Nome + " ,tem de ser superior ao dia de hoje");
                }
                else
                {
                    fimContrato = value;
                }
            }
        }

        private int grauTecnico;

        public int GrauTecnico
        {
            get { return grauTecnico; }
            set
            {
                if(value < 0 || value > 5)
                {
                    throw new Exception("Grau de Tecnico inválido");
                }
                else
                {
                    grauTecnico = value;
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

        private ClubeDeFutebol clubeTecnico;

        public ClubeDeFutebol ClubeTecnico
        {
            get { return clubeTecnico; }
            set { clubeTecnico = value; }
        }

        public Tecnico(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue,
            double salarioValue, TipoTec tipoTecnicoValue, DateTime incioContratoValue, DateTime fimContratoValue, int numGrauValue,
            int numCarteiraValue, ClubeDeFutebol clubeValue) : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 70) throw new Exception("Fim de contrato inválido não pode exceder os 70 anos");
            TipoTecnico = tipoTecnicoValue;
            InicioContrato = incioContratoValue;
            FimContrato = fimContratoValue;
            GrauTecnico = numGrauValue;
            NumCarteira = numCarteiraValue;
            ClubeTecnico = clubeValue;
        }

        public Tecnico(string nomeValue, DateTime dtNascimentoValue, string nifValue, string contactoValue, string emailValue,
           double salarioValue, TipoTec tipoTecnicoValue, DateTime incioContratoValue, DateTime fimContratoValue, int numGrauValue,
           int numCarteiraValue) : base(nomeValue, dtNascimentoValue, nifValue, contactoValue, emailValue, salarioValue)
        {
            if ((fimContratoValue - dtNascimentoValue).Days / 365 > 70) throw new Exception("Fim de contrato inválido não pode exceder os 70 anos");
            TipoTecnico = tipoTecnicoValue;
            InicioContrato = incioContratoValue;
            FimContrato = fimContratoValue;
            GrauTecnico = numGrauValue;
            NumCarteira = numCarteiraValue;
        }


        public override string ToString()
        {
            return base.ToString() + " " + TipoTecnico + " " + GrauTecnico + " " + NumCarteira;
        }
    }
}
