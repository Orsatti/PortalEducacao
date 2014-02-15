namespace PortalEducacao.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using PortalEducacao.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PortalEducacao.Models.PEContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PortalEducacao.Models.PEContext context)
        {
            #region usuarios de teste
            SeedMembership();
            #endregion

            #region Categoria Administrativa de IES
            var categoria1 = new CategoriaAdministrativa { codigoCensoSuperior = 1, Nome = "Pública Federal" };
            var categoria2 = new CategoriaAdministrativa { codigoCensoSuperior = 2, Nome = "Pública Estadual" };
            var categoria3 = new CategoriaAdministrativa { codigoCensoSuperior = 3, Nome = "Pública Municipal" };
            var categoria4 = new CategoriaAdministrativa { codigoCensoSuperior = 4, Nome = "Privada com Fins Lucrativos" };
            var categoria5 = new CategoriaAdministrativa { codigoCensoSuperior = 5, Nome = "Privada sem Fins Lucrativos" };
            var categoria6 = new CategoriaAdministrativa { codigoCensoSuperior = 6, Nome = "Especial" };
            context.CategoriasAdministrativas.AddOrUpdate(i => new { i.Nome }, categoria1, categoria2, categoria3, categoria4, categoria5, categoria6);
            context.SaveChanges();
            #endregion

            #region Organização Acadêmica de IES
            var org1 = new OrganizacaoAcademica { codigoCensoSuperior = 1, Nome = "Universidade" };
            var org2 = new OrganizacaoAcademica { codigoCensoSuperior = 2, Nome = "Centro Universitário" };
            var org3 = new OrganizacaoAcademica { codigoCensoSuperior = 3, Nome = "Faculdade" };
            var org4 = new OrganizacaoAcademica { codigoCensoSuperior = 4, Nome = "Instituto Federal de Educação, Ciência e Tecnologia" };
            var org5 = new OrganizacaoAcademica { codigoCensoSuperior = 5, Nome = "Centro Federal de Educação Tecnológica" };
            context.OrganizacoesAcademicas.AddOrUpdate(i => new { i.Nome }, org1, org2, org3, org4, org5);
            context.SaveChanges();
            #endregion

        }

        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("PEContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            var userID = membership.GetUser("admin",false);
            if (userID == null)
            {
                membership.CreateUserAndAccount("admin", "1234");
            }

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }

            if (!roles.IsUserInRole("admin", "Admin"))
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
        }
    }
}
