﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeSegundaUnidade
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dgmsvi72o1j1fpnEntities : DbContext
    {
        public dgmsvi72o1j1fpnEntities()
            : base("name=dgmsvi72o1j1fpnEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<funcionario> funcionarios { get; set; }
        public virtual DbSet<registroDaLavagem> registroDaLavagems { get; set; }
        public virtual DbSet<veiculo> veiculoes { get; set; }
    }
}
