using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CadastroUsuario.Data.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ec6e250-f7a6-49fe-b21e-015675db1688");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "beb1f03f-1d2b-4105-8e14-40e667c299b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1a37a3f-2a3e-4efb-8caa-0216a7ac23cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79a97722-087f-4f76-96f1-8c47319f04fc", null, "Admin", "ADMIN" },
                    { "b67fcba3-7de4-4d7f-926b-5296015f9452", null, "Vendedor", "VENDEDOR" },
                    { "e3ed03f8-e6dd-4907-b2af-81675e9ca79b", null, "Gerente", "GERENTE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79a97722-087f-4f76-96f1-8c47319f04fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b67fcba3-7de4-4d7f-926b-5296015f9452");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ed03f8-e6dd-4907-b2af-81675e9ca79b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ec6e250-f7a6-49fe-b21e-015675db1688", null, "Gerente", "GERENTE" },
                    { "beb1f03f-1d2b-4105-8e14-40e667c299b2", null, "Admin", "ADMIN" },
                    { "e1a37a3f-2a3e-4efb-8caa-0216a7ac23cc", null, "Vendedor", "VENDEDOR" }
                });
        }
    }
}
