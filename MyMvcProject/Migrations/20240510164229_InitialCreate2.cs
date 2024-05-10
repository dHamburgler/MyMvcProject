using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestParty");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUserParty",
                columns: table => new
                {
                    GuestsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserParty", x => new { x.GuestsId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserParty_AspNetUsers_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserParty_Party_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserParty_PartiesId",
                table: "ApplicationUserParty",
                column: "PartiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserParty");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestParty",
                columns: table => new
                {
                    GuestsId = table.Column<int>(type: "int", nullable: false),
                    PartiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestParty", x => new { x.GuestsId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_GuestParty_Guest_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Guest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestParty_Party_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestParty_PartiesId",
                table: "GuestParty",
                column: "PartiesId");
        }
    }
}
