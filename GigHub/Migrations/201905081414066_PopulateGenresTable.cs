namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres(ID,Name) values(1,'Jazz')");
            Sql("Insert into Genres(ID,Name) values(2,'Blues')");
            Sql("Insert into Genres(ID,Name) values(3,'Rock')");
            Sql("Insert into Genres(ID,Name) values(4,'Country')");

        }
        
        public override void Down()
        {
            Sql("Delete from Genres where ID in(1,2,3,4)");
        }
    }
}
