using Microsoft.EntityFrameworkCore.Migrations;

namespace WebshopServer.Migrations
{
    public partial class AddVerificationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Pending" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Rejected" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3L, "Accepted" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatusId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Users");
        }
    }
}
