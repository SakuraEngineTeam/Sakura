using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sakura.Persistence.Migrations
{
  public partial class AddThreads : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<Guid>(
        name: "thread_id",
        table: "posts",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

      migrationBuilder.CreateTable(
        name: "threads",
        columns: table => new {
          thread_id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
          bumped_at = table.Column<DateTime>(nullable: false)
        },
        constraints: table => { table.PrimaryKey("pk_threads", x => x.thread_id); });

      migrationBuilder.CreateIndex(
        name: "ix_posts_thread_id",
        table: "posts",
        column: "thread_id");

      // Create new thread and assign all posts to it to ensure foreign key constraint.
      migrationBuilder.Sql("INSERT INTO threads(thread_id, bumped_at) VALUES (uuid_generate_v4(), now())");
      migrationBuilder.Sql("UPDATE posts SET thread_id = (SELECT thread_id FROM threads LIMIT 1)");

      migrationBuilder.AddForeignKey(
        name: "fk_posts_threads_thread_id",
        table: "posts",
        column: "thread_id",
        principalTable: "threads",
        principalColumn: "thread_id",
        onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "fk_posts_threads_thread_id",
        table: "posts");

      migrationBuilder.DropTable(
        name: "threads");

      migrationBuilder.DropIndex(
        name: "ix_posts_thread_id",
        table: "posts");

      migrationBuilder.DropColumn(
        name: "thread_id",
        table: "posts");
    }
  }
}
