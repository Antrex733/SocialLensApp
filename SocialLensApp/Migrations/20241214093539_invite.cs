using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialLensApp.Migrations
{
    /// <inheritdoc />
    public partial class invite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvitingUser",
                table: "Invites",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitingUser",
                table: "Invites");
        }
    }
}
