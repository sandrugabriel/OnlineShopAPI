using FluentMigrator;

namespace OnlineShop.Data.Migrations
{
    [Migration(32456)]
    public class CreateTabels : Migration
    {

        public override void Up()
        {
            Create.Table("products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("Category").AsString().NotNullable()
                .WithColumn("Create_date").AsDateTime().NotNullable()
                .WithColumn("Stock").AsInt32().NotNullable();



        }

        public override void Down()
        {

        }

    }
}
