namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementnomCoetnomDep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.College", "nomCo", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Departement", "nomDe", c => c.String(nullable: false, maxLength: 40, unicode: false));
            DropColumn("dbo.College", "nom");
            DropColumn("dbo.Departement", "nom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departement", "nom", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AddColumn("dbo.College", "nom", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Departement", "nomDe");
            DropColumn("dbo.College", "nomCo");
        }
    }
}
