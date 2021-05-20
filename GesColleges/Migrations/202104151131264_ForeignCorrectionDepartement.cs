namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignCorrectionDepartement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EtudiantMatieres", "Etudiant_IdEtudiant", "dbo.Etudiant");
            DropForeignKey("dbo.EtudiantMatieres", "Matiere_IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.Departement", "College_IdCollege", "dbo.College");
            DropForeignKey("dbo.Enseignant", "Departement_IdDepartement", "dbo.Departement");
            DropForeignKey("dbo.Enseignant", "Matiere_IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.Matiere", "Salle_IdSalle", "dbo.Salle");
            DropIndex("dbo.Departement", new[] { "College_IdCollege" });
            DropIndex("dbo.Enseignant", new[] { "Departement_IdDepartement" });
            DropIndex("dbo.Enseignant", new[] { "Matiere_IdMatiere" });
            DropIndex("dbo.Matiere", new[] { "Salle_IdSalle" });
            DropIndex("dbo.EtudiantMatieres", new[] { "Etudiant_IdEtudiant" });
            DropIndex("dbo.EtudiantMatieres", new[] { "Matiere_IdMatiere" });
            RenameColumn(table: "dbo.Departement", name: "College_IdCollege", newName: "IdCollege");
            RenameColumn(table: "dbo.Enseignant", name: "Departement_IdDepartement", newName: "IdDepartement");
            RenameColumn(table: "dbo.Enseignant", name: "Matiere_IdMatiere", newName: "IdMatiere");
            RenameColumn(table: "dbo.Matiere", name: "Salle_IdSalle", newName: "IdSalle");
            AlterColumn("dbo.Departement", "IdCollege", c => c.Int(nullable: false));
            AlterColumn("dbo.Enseignant", "IdDepartement", c => c.Int(nullable: false));
            AlterColumn("dbo.Enseignant", "IdMatiere", c => c.Int(nullable: false));
            AlterColumn("dbo.Matiere", "IdSalle", c => c.Int(nullable: false));
            CreateIndex("dbo.Departement", "IdCollege");
            CreateIndex("dbo.Enseignant", "IdDepartement");
            CreateIndex("dbo.Enseignant", "IdMatiere");
            CreateIndex("dbo.Matiere", "IdSalle");
            AddForeignKey("dbo.Departement", "IdCollege", "dbo.College", "IdCollege", cascadeDelete: true);
            AddForeignKey("dbo.Enseignant", "IdDepartement", "dbo.Departement", "IdDepartement", cascadeDelete: true);
            AddForeignKey("dbo.Enseignant", "IdMatiere", "dbo.Matiere", "IdMatiere", cascadeDelete: true);
            AddForeignKey("dbo.Matiere", "IdSalle", "dbo.Salle", "IdSalle", cascadeDelete: true);
            DropTable("dbo.EtudiantMatieres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EtudiantMatieres",
                c => new
                    {
                        Etudiant_IdEtudiant = c.Int(nullable: false),
                        Matiere_IdMatiere = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etudiant_IdEtudiant, t.Matiere_IdMatiere });
            
            DropForeignKey("dbo.Matiere", "IdSalle", "dbo.Salle");
            DropForeignKey("dbo.Enseignant", "IdMatiere", "dbo.Matiere");
            DropForeignKey("dbo.Enseignant", "IdDepartement", "dbo.Departement");
            DropForeignKey("dbo.Departement", "IdCollege", "dbo.College");
            DropIndex("dbo.Matiere", new[] { "IdSalle" });
            DropIndex("dbo.Enseignant", new[] { "IdMatiere" });
            DropIndex("dbo.Enseignant", new[] { "IdDepartement" });
            DropIndex("dbo.Departement", new[] { "IdCollege" });
            AlterColumn("dbo.Matiere", "IdSalle", c => c.Int());
            AlterColumn("dbo.Enseignant", "IdMatiere", c => c.Int());
            AlterColumn("dbo.Enseignant", "IdDepartement", c => c.Int());
            AlterColumn("dbo.Departement", "IdCollege", c => c.Int());
            RenameColumn(table: "dbo.Matiere", name: "IdSalle", newName: "Salle_IdSalle");
            RenameColumn(table: "dbo.Enseignant", name: "IdMatiere", newName: "Matiere_IdMatiere");
            RenameColumn(table: "dbo.Enseignant", name: "IdDepartement", newName: "Departement_IdDepartement");
            RenameColumn(table: "dbo.Departement", name: "IdCollege", newName: "College_IdCollege");
            CreateIndex("dbo.EtudiantMatieres", "Matiere_IdMatiere");
            CreateIndex("dbo.EtudiantMatieres", "Etudiant_IdEtudiant");
            CreateIndex("dbo.Matiere", "Salle_IdSalle");
            CreateIndex("dbo.Enseignant", "Matiere_IdMatiere");
            CreateIndex("dbo.Enseignant", "Departement_IdDepartement");
            CreateIndex("dbo.Departement", "College_IdCollege");
            AddForeignKey("dbo.Matiere", "Salle_IdSalle", "dbo.Salle", "IdSalle");
            AddForeignKey("dbo.Enseignant", "Matiere_IdMatiere", "dbo.Matiere", "IdMatiere");
            AddForeignKey("dbo.Enseignant", "Departement_IdDepartement", "dbo.Departement", "IdDepartement");
            AddForeignKey("dbo.Departement", "College_IdCollege", "dbo.College", "IdCollege");
            AddForeignKey("dbo.EtudiantMatieres", "Matiere_IdMatiere", "dbo.Matiere", "IdMatiere", cascadeDelete: true);
            AddForeignKey("dbo.EtudiantMatieres", "Etudiant_IdEtudiant", "dbo.Etudiant", "IdEtudiant", cascadeDelete: true);
        }
    }
}
