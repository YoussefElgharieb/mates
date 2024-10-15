using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mates.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relationshipsnavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0cbf7d6d-bf95-44c2-9d80-1c98e1e9273d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("311e6c7a-4ec3-42a7-acf5-3cf6add0895c"), "m.ha@luftborn.com", "marwan hamed", "$2a$11$PRYh/ESkoB7jNbAsl0z3HefeGscGxvBau7ZL0UBN/FBPuWft7ZC7a", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("311e6c7a-4ec3-42a7-acf5-3cf6add0895c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("0cbf7d6d-bf95-44c2-9d80-1c98e1e9273d"), "m.ha@luftborn.com", "marwan hamed", "$2a$11$PRYh/ESkoB7jNbAsl0z3HefeGscGxvBau7ZL0UBN/FBPuWft7ZC7a", 1 });
        }
    }
}
