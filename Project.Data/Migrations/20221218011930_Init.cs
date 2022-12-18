using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "CreatedDate", "EndDate", "Name", "StartDate", "Status", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4464), new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teknosa", new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "CreatedDate", "EndDate", "Name", "StartDate", "Status", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4468), new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enoca", new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CompanyId", "CreatedDate", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[] { 1, 1, new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4648), "Telephone", 10.0, 99, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CompanyId", "CreatedDate", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[] { 2, 2, new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4651), "Biligsayar", 10.0, 99, null });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientName", "CreatedDate", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Khaled HAMIDI", new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4681), 1, null },
                    { 2, "Ahmed Ali", new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4683), 2, null },
                    { 3, "Murat Oglu", new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4685), 2, null },
                    { 4, "Ziyat Ziyat", new DateTime(2022, 12, 18, 1, 19, 30, 181, DateTimeKind.Utc).AddTicks(4686), 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Companys");
        }
    }
}
