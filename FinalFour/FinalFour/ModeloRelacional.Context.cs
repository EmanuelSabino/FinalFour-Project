﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataBaseEntities : DbContext
    {
        public DataBaseEntities()
            : base("name=DataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClubesFutebol> ClubesFutebol { get; set; }
        public virtual DbSet<Dirigentes> Dirigentes { get; set; }
        public virtual DbSet<Jogadores> Jogadores { get; set; }
        public virtual DbSet<Jogos> Jogos { get; set; }
        public virtual DbSet<Nacionalidades> Nacionalidades { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<PosicaoJog> PosicaoJog { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tecnicos> Tecnicos { get; set; }
        public virtual DbSet<TiposDirigentes> TiposDirigentes { get; set; }
        public virtual DbSet<TiposTecnicos> TiposTecnicos { get; set; }
        public virtual DbSet<Torneios> Torneios { get; set; }
    }
}
