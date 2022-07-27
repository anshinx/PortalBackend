using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalBackend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptedUserId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubscriptionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WhenToSubbed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 100, nullable: false),
                    WhenAttend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhenLeft = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleID = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    UserDTOId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Communities_Users_UserDTOId",
                        column: x => x.UserDTOId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communities_UserDTOId",
                table: "Communities",
                column: "UserDTOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "Subscriments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
