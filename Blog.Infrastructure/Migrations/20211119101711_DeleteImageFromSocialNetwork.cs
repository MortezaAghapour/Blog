using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.Migrations
{
    public partial class DeleteImageFromSocialNetwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SocialNetworks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SocialNetworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
