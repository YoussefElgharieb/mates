using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mates.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_OtherUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("311e6c7a-4ec3-42a7-acf5-3cf6add0895c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("98d244b8-2f67-45e8-a06a-b9cdbeff0d71"), "m.ha@luftborn.com", "marwan hamed", "$2a$11$PRYh/ESkoB7jNbAsl0z3HefeGscGxvBau7ZL0UBN/FBPuWft7ZC7a", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_OtherUserId",
                table: "Relationships",
                column: "OtherUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_OtherUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98d244b8-2f67-45e8-a06a-b9cdbeff0d71"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("311e6c7a-4ec3-42a7-acf5-3cf6add0895c"), "m.ha@luftborn.com", "marwan hamed", "$2a$11$PRYh/ESkoB7jNbAsl0z3HefeGscGxvBau7ZL0UBN/FBPuWft7ZC7a", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_OtherUserId",
                table: "Relationships",
                column: "OtherUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_UserId",
                table: "Relationships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
