using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiBlog.Migrations
{
    /// <inheritdoc />
    public partial class ControllersFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_AuthorId",
                table: "NewsItems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsItems_Authors_AuthorId",
                table: "NewsItems",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsItems_Authors_AuthorId",
                table: "NewsItems");

            migrationBuilder.DropIndex(
                name: "IX_NewsItems_AuthorId",
                table: "NewsItems");
        }
    }
}
