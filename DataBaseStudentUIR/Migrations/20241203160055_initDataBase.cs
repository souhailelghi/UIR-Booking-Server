using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataBaseStudentUIR.Migrations
{
    /// <inheritdoc />
    public partial class initDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeUIR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "BirthDate", "CIN", "CodeUIR", "Email", "FirstName", "Gender", "LastName", "Password", "Phone" },
                values: new object[,]
                {
                    { new Guid("f76cb5d7-22a5-4a8f-8c05-3dc1f57faffa"), "321 Maple St, Kentira", new DateTime(1995, 7, 12, 9, 20, 0, 0, DateTimeKind.Unspecified), "GSI451479", "UIR5547", "souhail@jobintech-uir.ma", "Souhail", "Male", "Alaoui", "123456", "+212645678901" },
                    { new Guid("f76cb5d7-22a5-4a8f-8c05-3dc1f57faffb"), "321 Maple St, Agadir", new DateTime(1995, 7, 12, 9, 20, 0, 0, DateTimeKind.Unspecified), "GHI456789", "UIR005", "sara.brahimi@jobintech-uir.ma", "Sara", "Female", "Brahimi", "password123", "+212645678901" },
                    { new Guid("f76cb5d7-22a5-5a8f-8c05-3dc1f57faffa"), "321 Maple St, Kentira", new DateTime(1995, 7, 12, 9, 20, 0, 0, DateTimeKind.Unspecified), "GSI451479", "UIR56477", "adam@jobintech-uir.ma", "Adam", "Male", "Alaoui", "123456", "+212645678901" },
                    { new Guid("f76cb5d7-82a5-4a8f-8c05-3dc1f57faffe"), "321 Maple St, Kentira", new DateTime(1995, 7, 12, 9, 20, 0, 0, DateTimeKind.Unspecified), "GSI451479", "UIR57412", "ayoub@jobintech-uir.ma", "Ayoub", "Male", "Grami", "123456", "+212645678901" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
