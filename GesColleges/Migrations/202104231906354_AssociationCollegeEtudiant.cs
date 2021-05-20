namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociationCollegeEtudiant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Etudiant", "IdMatiere", c => c.Int(nullable: false));
            CreateIndex("dbo.Etudiant", "IdMatiere");
            AddForeignKey("dbo.Etudiant", "IdMatiere", "dbo.College", "IdCollege", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Etudiant", "IdMatiere", "dbo.College");
            DropIndex("dbo.Etudiant", new[] { "IdMatiere" });
            DropColumn("dbo.Etudiant", "IdMatiere");
        }
    }
}
