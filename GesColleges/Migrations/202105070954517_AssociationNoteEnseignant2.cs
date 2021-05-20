namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociationNoteEnseignant2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Note");
            AddPrimaryKey("dbo.Note", new[] { "IdEtudiant", "IdMatiere", "IdEnseignant" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Note");
            AddPrimaryKey("dbo.Note", new[] { "IdEtudiant", "IdMatiere" });
        }
    }
}
