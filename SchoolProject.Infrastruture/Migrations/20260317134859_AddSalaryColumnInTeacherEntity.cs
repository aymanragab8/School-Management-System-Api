using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryColumnInTeacherEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Teachers");
        }
    }
}
