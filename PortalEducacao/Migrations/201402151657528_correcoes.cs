namespace PortalEducacao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoriaAdministrativa", "codigoCensoSuperior", c => c.Int(nullable: false));
            AddColumn("dbo.Ies", "AnoCensoSuperior", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ies", "AnoCensoSuperior");
            DropColumn("dbo.CategoriaAdministrativa", "codigoCensoSuperior");
        }
    }
}
