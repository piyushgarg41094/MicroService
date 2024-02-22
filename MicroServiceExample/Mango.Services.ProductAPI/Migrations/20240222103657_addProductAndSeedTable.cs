using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductAndSeedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatgoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CatgoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Bajaj", "This is an Fan manufactured by Bajaj", "https://placehold.co/603x403", "Bajaj Fans", 2000.0 },
                    { 2, "Bajaj", "This is an Cooler manufactured by Bajaj", "https://placehold.co/602x402", "Bajaj Cooler", 8000.0 },
                    { 3, "Whirlpool", "This is an Washing Machine manufactured by Whirlpool", "https://placehold.co/601x401", "Whirlpool washing Machine", 17000.0 },
                    { 4, "Hitachi", "This is an AC manufactured by Hitachi", "https://placehold.co/600x400", "Hitachi AC", 25000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
