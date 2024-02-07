using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkTutorial.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfVolumes",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVolumes",
                table: "Books");
        }
    }
}
