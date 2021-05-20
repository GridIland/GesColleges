namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.College",
                c => new
                    {
                        IdCollege = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false, maxLength: 20),
                        AdresseSite = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.IdCollege);
            
            CreateTable(
                "dbo.Departement",
                c => new
                    {
                        IdDepartement = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false, maxLength: 20, unicode: false),
                        College_IdCollege = c.Int(),
                    })
                .PrimaryKey(t => t.IdDepartement)
                .ForeignKey("dbo.College", t => t.College_IdCollege)
                .Index(t => t.College_IdCollege);
            
            CreateTable(
                "dbo.Enseignant",
                c => new
                    {
                        IdEnseignant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 20, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Telephone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false),
                        ChefDepartement = c.Boolean(nullable: false),
                        DatePriseFonction = c.DateTime(nullable: false),
                        Departement_IdDepartement = c.Int(),
                        Matiere_IdMatiere = c.Int(),
                    })
                .PrimaryKey(t => t.IdEnseignant)
                .ForeignKey("dbo.Departement", t => t.Departement_IdDepartement)
                .ForeignKey("dbo.Matiere", t => t.Matiere_IdMatiere)
                .Index(t => t.Departement_IdDepartement)
                .Index(t => t.Matiere_IdMatiere);
            
            CreateTable(
                "dbo.Matiere",
                c => new
                    {
                        IdMatiere = c.Int(nullable: false, identity: true),
                        libelle = c.String(nullable: false, maxLength: 30, unicode: false),
                        Salle_IdSalle = c.Int(),
                    })
                .PrimaryKey(t => t.IdMatiere)
                .ForeignKey("dbo.Salle", t => t.Salle_IdSalle)
                .Index(t => t.Salle_IdSalle);
            
            CreateTable(
                "dbo.Etudiant",
                c => new
                    {
                        IdEtudiant = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 20, unicode: false),
                        Prenom = c.String(nullable: false, maxLength: 30, unicode: false),
                        Telephone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false),
                        AnneeEntree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEtudiant);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        IdEtudiant = c.Int(nullable: false),
                        IdMatiere = c.Int(nullable: false),
                        NoteControle = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEtudiant, t.IdMatiere })
                .ForeignKey("dbo.Etudiant", t => t.IdEtudiant, cascadeDelete: true)
                .ForeignKey("dbo.Matiere", t => t.IdMatiere, cascadeDelete: true)
                .Index(t => t.IdEtudiant)
                .Index(t => t.IdMatiere);
            
            CreateTable(
                "dbo.Salle",
                c => new
                    {
                        IdSalle = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 20, unicode: false),
                        Capacite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSalle);
            
            CreateTable(
                "dbo.EtudiantMatieres",
                c => new
                    {
                        Etudiant_IdEtudiant = c.Int(nullable: false),
                        Matiere_IdMatiere = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etudiant_IdEtudiant, t.Matiere_IdMatiere })
                .ForeignKey("dbo.Etudiant", t => t.Etudiant_IdEtudiant, cascadeDelete: true)
                .ForeignKey("dbo.Matiere", t => t.Matiere_IdMatiere, cascadeDelete: true)
                .Index(t => t.Etudiant_IdEtudiant)
                .Index(t => t.Matiere_IdMatiere);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matiere", "Salle_IdSalle", "dbo.Salle");
            DropForeignKey("dbo.Note", "IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.Note", "IdEtudiant", "dbo.Etudiant");
            DropForeignKey("dbo.EtudiantMatieres", "Matiere_IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.EtudiantMatieres", "Etudiant_IdEtudiant", "dbo.Etudiant");
            DropForeignKey("dbo.Enseignant", "Matiere_IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.Enseignant", "Departement_IdDepartement", "dbo.Departement");
            DropForeignKey("dbo.Departement", "College_IdCollege", "dbo.College");
            DropIndex("dbo.EtudiantMatieres", new[] { "Matiere_IdMatiere" });
            DropIndex("dbo.EtudiantMatieres", new[] { "Etudiant_IdEtudiant" });
            DropIndex("dbo.Note", new[] { "IdMatiere" });
            DropIndex("dbo.Note", new[] { "IdEtudiant" });
            DropIndex("dbo.Matiere", new[] { "Salle_IdSalle" });
            DropIndex("dbo.Enseignant", new[] { "Matiere_IdMatiere" });
            DropIndex("dbo.Enseignant", new[] { "Departement_IdDepartement" });
            DropIndex("dbo.Departement", new[] { "College_IdCollege" });
            DropTable("dbo.EtudiantMatieres");
            DropTable("dbo.Salle");
            DropTable("dbo.Note");
            DropTable("dbo.Etudiant");
            DropTable("dbo.Matiere");
            DropTable("dbo.Enseignant");
            DropTable("dbo.Departement");
            DropTable("dbo.College");
        }
    }
}
