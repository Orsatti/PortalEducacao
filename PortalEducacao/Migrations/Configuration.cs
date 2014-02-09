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
            var categoria1 = new CategoriaAdministrativa { Nome = "Pública Federal" };
            var categoria2 = new CategoriaAdministrativa { Nome = "Pública Estadual" };
            var categoria3 = new CategoriaAdministrativa { Nome = "Pública Municipal" };
            var categoria4 = new CategoriaAdministrativa { Nome = "Privada com Fins Lucrativos" };
            var categoria5 = new CategoriaAdministrativa { Nome = "Privada sem Fins Lucrativos" };
            var categoria6 = new CategoriaAdministrativa { Nome = "Especial" };
            context.CategoriasAdministrativas.AddOrUpdate(i => new { i.Nome }, categoria1, categoria2, categoria3, categoria4, categoria5, categoria6);
            context.SaveChanges();
            #endregion

            #region Organização Acadêmica de IES
            var org1 = new OrganizacaoAcademica { Nome = "Universidade" };
            var org2 = new OrganizacaoAcademica { Nome = "Centro Universitário" };
            var org3 = new OrganizacaoAcademica { Nome = "Faculdade" };
            var org4 = new OrganizacaoAcademica { Nome = "Instituto Federal de Educação, Ciência e Tecnologia" };
            var org5 = new OrganizacaoAcademica { Nome = "Centro Federal de Educação Tecnológica" };
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
