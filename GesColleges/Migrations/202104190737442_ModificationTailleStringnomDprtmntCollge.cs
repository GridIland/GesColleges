namespace GesColleges.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificationTailleStringnomDprtmntCollge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.College", "nom", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Departement", "nom", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departement", "nom", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.College", "nom", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
