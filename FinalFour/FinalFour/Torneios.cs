//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalFour
{
    using System;
    using System.Collections.Generic;
    
    public partial class Torneios
    {
        public int Id { get; set; }
        public int Id_PrimeiroJogo { get; set; }
        public int Id_SegundoJogo { get; set; }
        public int Id_UltimoJogo { get; set; }
        public System.DateTime InicioTorneio { get; set; }
        public int Id_ClubeVencedor { get; set; }
        public int id_Pais { get; set; }
        public double Pemio { get; set; }
    
        public virtual ClubesFutebol ClubesFutebol { get; set; }
        public virtual Jogos Jogos { get; set; }
        public virtual Jogos Jogos1 { get; set; }
        public virtual Jogos Jogos2 { get; set; }
        public virtual Paises Paises { get; set; }
    }
}
