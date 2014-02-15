using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using PortalEducacao.Models;
using System.Data.Entity;

namespace PortalEducacao.Models
{
    public class PEContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Ies> Iess { get; set; }
        public DbSet<CategoriaAdministrativa> CategoriasAdministrativas { get; set; }
        public DbSet<OrganizacaoAcademica> OrganizacoesAcademicas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<UF> UFs { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}