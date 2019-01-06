using Microsoft.EntityFrameworkCore.Migrations;

namespace Sakura.Persistence.Migrations
{
  public partial class AddPostViewId : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<long>(
        name: "view_id",
        table: "posts",
        nullable: false,
        defaultValue: 0L);

      migrationBuilder.Sql("UPDATE posts SET view_id = post_id");

      migrationBuilder.CreateIndex(
        name: "ix_posts_view_id",
        table: "posts",
        column: "view_id",
        unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
        name: "ix_posts_view_id",
        table: "posts");

      migrationBuilder.DropColumn(
        name: "view_id",
        table: "posts");
    }
  }
}
