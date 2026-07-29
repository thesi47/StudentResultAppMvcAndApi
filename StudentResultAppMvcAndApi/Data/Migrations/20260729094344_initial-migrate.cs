using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentResultAppMvcAndApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StudentAge = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    TotalMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    AverageMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    LtrGrade = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Gpa = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_students_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    ResultId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhysicsMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ChemistryMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MathMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_results_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_results_StudentId",
                table: "results",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_DepartmentId",
                table: "students",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
