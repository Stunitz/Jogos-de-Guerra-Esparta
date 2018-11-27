namespace JogosDeGuerraModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2211 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElementoDoExercitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Saude = c.Int(nullable: false),
                        posicao_Largura = c.Int(nullable: false),
                        posicao_Altura = c.Int(nullable: false),
                        TabuleiroId = c.Int(nullable: false),
                        ExercitoId = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Exercito_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercitoes", t => t.ExercitoId, cascadeDelete: true)
                .ForeignKey("dbo.Tabuleiroes", t => t.TabuleiroId, cascadeDelete: true)
                .ForeignKey("dbo.Exercitoes", t => t.Exercito_Id)
                .Index(t => t.TabuleiroId)
                .Index(t => t.ExercitoId)
                .Index(t => t.Exercito_Id);
            
            CreateTable(
                "dbo.Exercitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatalhaId = c.Int(),
                        UsuarioId = c.Int(nullable: false),
                        Nacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batalhas", t => t.BatalhaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.BatalhaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Batalhas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TabuleiroId = c.Int(),
                        ExercitoBrancoId = c.Int(),
                        ExercitoPretoId = c.Int(),
                        VencedorId = c.Int(),
                        TurnoId = c.Int(),
                        Estado = c.Int(nullable: false),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercitoes", t => t.ExercitoBrancoId)
                .ForeignKey("dbo.Exercitoes", t => t.ExercitoPretoId)
                .ForeignKey("dbo.Tabuleiroes", t => t.TabuleiroId)
                .ForeignKey("dbo.Exercitoes", t => t.TurnoId)
                .ForeignKey("dbo.Exercitoes", t => t.VencedorId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.TabuleiroId)
                .Index(t => t.ExercitoBrancoId)
                .Index(t => t.ExercitoPretoId)
                .Index(t => t.VencedorId)
                .Index(t => t.TurnoId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Tabuleiroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Largura = c.Int(nullable: false),
                        Altura = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercitoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Batalhas", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.ElementoDoExercitoes", "Exercito_Id", "dbo.Exercitoes");
            DropForeignKey("dbo.Exercitoes", "BatalhaId", "dbo.Batalhas");
            DropForeignKey("dbo.Batalhas", "VencedorId", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "TurnoId", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "TabuleiroId", "dbo.Tabuleiroes");
            DropForeignKey("dbo.ElementoDoExercitoes", "TabuleiroId", "dbo.Tabuleiroes");
            DropForeignKey("dbo.ElementoDoExercitoes", "ExercitoId", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "ExercitoPretoId", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "ExercitoBrancoId", "dbo.Exercitoes");
            DropIndex("dbo.Batalhas", new[] { "Usuario_Id" });
            DropIndex("dbo.Batalhas", new[] { "TurnoId" });
            DropIndex("dbo.Batalhas", new[] { "VencedorId" });
            DropIndex("dbo.Batalhas", new[] { "ExercitoPretoId" });
            DropIndex("dbo.Batalhas", new[] { "ExercitoBrancoId" });
            DropIndex("dbo.Batalhas", new[] { "TabuleiroId" });
            DropIndex("dbo.Exercitoes", new[] { "UsuarioId" });
            DropIndex("dbo.Exercitoes", new[] { "BatalhaId" });
            DropIndex("dbo.ElementoDoExercitoes", new[] { "Exercito_Id" });
            DropIndex("dbo.ElementoDoExercitoes", new[] { "ExercitoId" });
            DropIndex("dbo.ElementoDoExercitoes", new[] { "TabuleiroId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Tabuleiroes");
            DropTable("dbo.Batalhas");
            DropTable("dbo.Exercitoes");
            DropTable("dbo.ElementoDoExercitoes");
        }
    }
}
