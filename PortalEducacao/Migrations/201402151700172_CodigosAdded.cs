namespace PortalEducacao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigosAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Municipio", "codigoSensoSuperior", c => c.Int());
            AddColumn("dbo.UF", "codigoSensoSuperior", c => c.Int());
            AddColumn("dbo.OrganizacaoAcademica", "codigoCensoSuperior", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganizacaoAcademica", "codigoCensoSuperior");
            DropColumn("dbo.UF", "codigoSensoSuperior");
            DropColumn("dbo.Municipio", "codigoSensoSuperior");
        }
    }
}
