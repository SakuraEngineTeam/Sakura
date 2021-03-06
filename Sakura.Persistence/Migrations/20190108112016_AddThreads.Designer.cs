﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sakura.Persistence;

namespace Sakura.Persistence.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190108112016_AddThreads")]
    partial class AddThreads
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Sakura.Persistence.PostResource", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("post_id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnName("message");

                    b.Property<Guid>("ThreadId")
                        .HasColumnName("thread_id");

                    b.Property<long>("ViewId")
                        .HasColumnName("view_id");

                    b.HasKey("PostId")
                        .HasName("pk_posts");

                    b.HasIndex("ThreadId")
                        .HasName("ix_posts_thread_id");

                    b.HasIndex("ViewId")
                        .IsUnique()
                        .HasName("ix_posts_view_id");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("Sakura.Persistence.ThreadResource", b =>
                {
                    b.Property<Guid>("ThreadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("thread_id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("BumpedAt")
                        .HasColumnName("bumped_at");

                    b.HasKey("ThreadId")
                        .HasName("pk_threads");

                    b.ToTable("threads");
                });

            modelBuilder.Entity("Sakura.Persistence.PostResource", b =>
                {
                    b.HasOne("Sakura.Persistence.ThreadResource", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId")
                        .HasConstraintName("fk_posts_threads_thread_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
