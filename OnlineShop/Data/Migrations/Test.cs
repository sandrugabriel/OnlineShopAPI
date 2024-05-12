using FluentMigrator;

namespace OnlineShop.Data.Migrations
{
    [Migration(46530)]
    public class Test : Migration
    {

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }

        public override void Down()
        {

        }

    }
}
