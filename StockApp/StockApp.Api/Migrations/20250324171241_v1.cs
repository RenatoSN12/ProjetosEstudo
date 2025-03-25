using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<short>(type: "SMALLINT", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<short>(type: "SMALLINT", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    IsActive = table.Column<short>(type: "SMALLINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomId = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    IsActive = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemStock",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    MinimumStockLevel = table.Column<int>(type: "INT", nullable: false),
                    MaximumStockLevel = table.Column<int>(type: "INT", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    ItemId1 = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStock_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemStock_Item_ItemId1",
                        column: x => x.ItemId1,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemStock_Location_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId_Id",
                table: "Category",
                columns: new[] { "UserId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CustomId",
                table: "Item",
                column: "CustomId",
                unique: true,
                filter: "[CustomId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStock_ItemId",
                table: "ItemStock",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStock_ItemId1",
                table: "ItemStock",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStock_LocationId_ItemId",
                table: "ItemStock",
                columns: new[] { "LocationId", "ItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemStock");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
