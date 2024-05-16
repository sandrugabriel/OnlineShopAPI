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

            Create.Table("orders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CustomerId").AsInt32().NotNullable()
                .WithColumn("Ammount").AsDouble().NotNullable()
                .WithColumn("OrderAddress").AsString().NotNullable()
                .WithColumn("OrderDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsString().NotNullable();

            Create.Table("customers")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("FullName").AsString().NotNullable()
                .WithColumn("PhoneNumber").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Country").AsString().NotNullable();

            Create.Table("orderdetails")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("OrderId").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("Price").AsDouble().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable();


            Create.Table("options")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsDouble().NotNullable();


            Create.Table("productoptions")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("IdProduct").AsInt32().NotNullable()
                .WithColumn("IdOption").AsInt32().NotNullable();

            /*   public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }*/

        }

        public override void Down()
        {

        }

    }
}
