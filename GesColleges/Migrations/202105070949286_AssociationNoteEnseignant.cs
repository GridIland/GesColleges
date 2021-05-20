namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociationNoteEnseignant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "IdEnseignant", c => c.Int(nullable: false));
            CreateIndex("dbo.Note", "IdEnseignant");
            AddForeignKey("dbo.Note", "IdEnseignant", "dbo.Enseignant", "IdEnseignant", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "IdEnseignant", "dbo.Enseignant");
            DropIndex("dbo.Note", new[] { "IdEnseignant" });
            DropColumn("dbo.Note", "IdEnseignant");
        }
    }
}
