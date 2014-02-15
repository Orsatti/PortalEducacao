namespace PortalEducacao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regiaoAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regiao",
                c => new
                    {
                        RegiaoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.RegiaoID);
            
            AddColumn("dbo.UF", "RegiaoID", c => c.Int(nullable: false));
            CreateIndex("dbo.UF", "RegiaoID");
            AddForeignKey("dbo.UF", "RegiaoID", "dbo.Regiao", "RegiaoID");
            DropColumn("dbo.Ies", "CodigoMunicipioCensoSuperior");
            DropColumn("dbo.Ies", "CodigoUFCensoSuperior");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ies", "CodigoUFCensoSuperior", c => c.Int());
            AddColumn("dbo.Ies", "CodigoMunicipioCensoSuperior", c => c.Int());
            DropForeignKey("dbo.UF", "RegiaoID", "dbo.Regiao");
            DropIndex("dbo.UF", new[] { "RegiaoID" });
            DropColumn("dbo.UF", "RegiaoID");
            DropTable("dbo.Regiao");
        }
    }
}
