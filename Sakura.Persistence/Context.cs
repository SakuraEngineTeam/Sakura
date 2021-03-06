using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Sakura.Persistence
{
  public class Context : DbContext
  {
    public DbSet<ThreadResource> Threads { get; set; }
    public DbSet<PostResource> Posts { get; set; }

    public Context(DbContextOptions options) : base(options) { }

    protected static string ToSnakeCase(string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }

      Match startUnderscores = Regex.Match(input, @"^_+");
      return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.HasPostgresExtension("uuid-ossp");

      builder.Entity<ThreadResource>().Property(t => t.ThreadId).HasDefaultValueSql("uuid_generate_v4()");
      builder.Entity<PostResource>().Property(p => p.PostId).HasDefaultValueSql("uuid_generate_v4()");
      builder.Entity<PostResource>().HasIndex(p => p.ViewId).IsUnique();

      foreach (var entity in builder.Model.GetEntityTypes())
      {
        entity.Relational().TableName = ToSnakeCase(entity.Relational().TableName);

        foreach (var property in entity.GetProperties())
        {
          property.Relational().ColumnName = ToSnakeCase(property.Name);
        }

        foreach (var key in entity.GetKeys())
        {
          key.Relational().Name = ToSnakeCase(key.Relational().Name);
        }

        foreach (var key in entity.GetForeignKeys())
        {
          key.Relational().Name = ToSnakeCase(key.Relational().Name);
        }

        foreach (var index in entity.GetIndexes())
        {
          index.Relational().Name = ToSnakeCase(index.Relational().Name);
        }
      }
    }
  }
}
