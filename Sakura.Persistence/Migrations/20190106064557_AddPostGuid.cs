using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sakura.Persistence.Migrations
{
  public partial class AddPostGuid : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
        name: "pk_posts",
        table: "posts");

      migrationBuilder.DropColumn(
        name: "post_id",
        table: "posts");

      migrationBuilder.AddColumn<Guid>(
        name: "post_id",
        table: "posts",
        nullable: false,
        defaultValueSql: "uuid_generate_v4()");

      migrationBuilder.AddPrimaryKey(
        name: "pk_posts",
        table: "posts",
        column: "post_id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
        name: "pk_posts",
        table: "posts");

      migrationBuilder.DropColumn(
        name: "post_id",
        table: "posts");

      migrationBuilder.AddColumn<long>(
          name: "post_id",
          table: "posts")
        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

      migrationBuilder.AddPrimaryKey(
        name: "pk_posts",
        table: "posts",
        column: "post_id");
    }
  }
}
