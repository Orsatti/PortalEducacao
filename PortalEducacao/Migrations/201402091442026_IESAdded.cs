namespace PortalEducacao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IESAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaAdministrativa",
                c => new
                    {
                        CategoriaAdministrativaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoriaAdministrativaID);
            
            CreateTable(
                "dbo.Ies",
                c => new
                    {
                        IesID = c.Int(nullable: false, identity: true),
                        CodigoCensoSuperior = c.Int(),
                        Nome = c.String(nullable: false, maxLength: 200),
                        CodigoMantenedoraCensoSuperior = c.Int(),
                        CategoriaAdministrativaID = c.Int(),
                        OrganizacaoAcademicaID = c.Int(),
                        MunicipioID = c.Int(),
                        CodigoMunicipioCensoSuperior = c.Int(),
                        CodigoUFCensoSuperior = c.Int(),
                    })
                .PrimaryKey(t => t.IesID)
                .ForeignKey("dbo.CategoriaAdministrativa", t => t.CategoriaAdministrativaID)
                .ForeignKey("dbo.Municipio", t => t.MunicipioID)
                .ForeignKey("dbo.OrganizacaoAcademica", t => t.OrganizacaoAcademicaID)
                .Index(t => t.CategoriaAdministrativaID)
                .Index(t => t.MunicipioID)
                .Index(t => t.OrganizacaoAcademicaID);
            
            CreateTable(
                "dbo.Municipio",
                c => new
                    {
                        MunicipioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        UFID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MunicipioID)
                .ForeignKey("dbo.UF", t => t.UFID)
                .Index(t => t.UFID);
            
            CreateTable(
                "dbo.UF",
                c => new
                    {
                        UFID = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.UFID);
            
            CreateTable(
                "dbo.OrganizacaoAcademica",
                c => new
                    {
                        OrganizacaoAcademicaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.OrganizacaoAcademicaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ies", "OrganizacaoAcademicaID", "dbo.OrganizacaoAcademica");
            DropForeignKey("dbo.Ies", "MunicipioID", "dbo.Municipio");
            DropForeignKey("dbo.Municipio", "UFID", "dbo.UF");
            DropForeignKey("dbo.Ies", "CategoriaAdministrativaID", "dbo.CategoriaAdministrativa");
            DropIndex("dbo.Ies", new[] { "OrganizacaoAcademicaID" });
            DropIndex("dbo.Ies", new[] { "MunicipioID" });
            DropIndex("dbo.Municipio", new[] { "UFID" });
            DropIndex("dbo.Ies", new[] { "CategoriaAdministrativaID" });
            DropTable("dbo.OrganizacaoAcademica");
            DropTable("dbo.UF");
            DropTable("dbo.Municipio");
            DropTable("dbo.Ies");
            DropTable("dbo.CategoriaAdministrativa");
        }
    }
}
