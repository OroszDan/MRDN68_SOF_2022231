using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRDN68_SOF_2022231.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workplaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WorkedYears = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workplaces_Resumes_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dc8e2a13-fd8a-4577-84fa-58d2f4484206", 0, "95799728-8ad8-4a1b-8cb8-e4c0adeab4cf", "dani01@gmail.com", true, false, null, null, "DANI01@GMAIL.COM", "AQAAAAEAACcQAAAAEFFblGc0jPyYn5wUaPXzD+pzPOkyaPI7s++NjdUF1+6m0hexWOGDVLSh9Pkyvk611g==", null, false, "f34f6ee1-428e-4f1a-856e-6b0ad5cc3cc7", false, "dani01@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "dc8e2a13-fd8a-4577-84fa-58d2f4484206" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Age", "ContentType", "Data", "Description", "FirstName", "LastName", "OwnerId" },
                values: new object[] { "66781176-c014-4dcd-8e11-e987f83a6cb1", 55, null, null, "Én egy nagyon motivált ember vagyok aki már nagyon sok munkahelyen megfordult...", "Pista", "Kiss", "dc8e2a13-fd8a-4577-84fa-58d2f4484206" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Age", "ContentType", "Data", "Description", "FirstName", "LastName", "OwnerId" },
                values: new object[] { "b6e8f971-8f6d-4ab0-a88b-877a7092c3e0", 45, null, null, "Sokáig nem álltam munkába mivel fontosnak tartottam minél több diploma megszerzését...", "Géza", "Nagy", "dc8e2a13-fd8a-4577-84fa-58d2f4484206" });

            migrationBuilder.InsertData(
                table: "Workplaces",
                columns: new[] { "Id", "City", "CompanyName", "OwnerId", "Role", "WorkedYears" },
                values: new object[] { "3fbbcff9-4365-42b5-81d2-c73a8ce4e8e4", "München", "Bosch", "66781176-c014-4dcd-8e11-e987f83a6cb1", "Mechanical engineer", 6 });

            migrationBuilder.InsertData(
                table: "Workplaces",
                columns: new[] { "Id", "City", "CompanyName", "OwnerId", "Role", "WorkedYears" },
                values: new object[] { "66a9229e-ec71-4e1d-80ab-ce7f61d69a11", "Miskolc", "Evosoft", "66781176-c014-4dcd-8e11-e987f83a6cb1", "Software engineer", 1 });

            migrationBuilder.InsertData(
                table: "Workplaces",
                columns: new[] { "Id", "City", "CompanyName", "OwnerId", "Role", "WorkedYears" },
                values: new object[] { "e150928b-ca8b-4c27-a8ee-62e7d1400fbb", "Budapest", "Epam", "b6e8f971-8f6d-4ab0-a88b-877a7092c3e0", "Business analyst", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_OwnerId",
                table: "Resumes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_OwnerId",
                table: "Workplaces",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workplaces");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "dc8e2a13-fd8a-4577-84fa-58d2f4484206" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc8e2a13-fd8a-4577-84fa-58d2f4484206");
        }
    }
}
