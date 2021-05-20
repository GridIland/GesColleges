namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociationCollegeEtudiant1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Etudiant", name: "IdMatiere", newName: "IdCollege");
            RenameIndex(table: "dbo.Etudiant", name: "IX_IdMatiere", newName: "IX_IdCollege");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Etudiant", name: "IX_IdCollege", newName: "IX_IdMatiere");
            RenameColumn(table: "dbo.Etudiant", name: "IdCollege", newName: "IdMatiere");
        }
    }
}
