﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Miniblog.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Miniblog.Core.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20240512012823_FixingCommentsDto2")]
    partial class FixingCommentsDto2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Miniblog.Brains.Models.Dto.CommentDto", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("author");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("isadmin");

                    b.Property<string>("PostDtoID")
                        .HasColumnType("text")
                        .HasColumnName("postdtoid");

                    b.Property<DateTime>("PubDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("pubdate");

                    b.HasKey("ID")
                        .HasName("pk_comments");

                    b.HasIndex("PostDtoID")
                        .HasDatabaseName("ix_comments_postdtoid");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Miniblog.Brains.Models.Dto.PostDto", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Excerpt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("excerpt");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean")
                        .HasColumnName("ispublished");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lastmodified");

                    b.Property<DateTime>("PubDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("pubdate");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("slug");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_posts");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("Miniblog.Brains.Models.Dto.CommentDto", b =>
                {
                    b.HasOne("Miniblog.Brains.Models.Dto.PostDto", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostDtoID")
                        .HasConstraintName("fk_comments_posts_postdtoid");
                });

            modelBuilder.Entity("Miniblog.Brains.Models.Dto.PostDto", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
