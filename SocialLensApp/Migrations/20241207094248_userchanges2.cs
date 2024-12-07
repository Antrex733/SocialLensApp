using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialLensApp.Migrations
{
    /// <inheritdoc />
    public partial class userchanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockedList",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "FollowedList",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "FollowersList",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "FriendsList",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowedList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowersList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FriendsList",
                table: "Users");
        }
    }
}
