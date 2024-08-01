using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiBlog.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Readers_ReaderId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsItems_Authors_AuthorId",
                table: "NewsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Readers_ReaderId",
                table: "Reactions");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readers",
                table: "Readers");

            migrationBuilder.RenameTable(
                name: "Readers",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_ReaderId",
                table: "Comments",
                column: "ReaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItems_Users_AuthorId",
                table: "NewsItems",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Users_ReaderId",
                table: "Reactions",
                column: "ReaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_ReaderId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsItems_Users_AuthorId",
                table: "NewsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Users_ReaderId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Readers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readers",
                table: "Readers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Readers_ReaderId",
                table: "Comments",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItems_Authors_AuthorId",
                table: "NewsItems",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Readers_ReaderId",
                table: "Reactions",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
