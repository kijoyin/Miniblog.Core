using Microsoft.EntityFrameworkCore;

using Miniblog.Brains.Models.Dto;

using System;
using System.Collections.Generic;

namespace Miniblog.Infrastructure
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
        }
        //public DbSet<Blog> Blogs { get; set; }
        public DbSet<PostDto> Posts { get; set; }
        public DbSet<CommentDto> Comments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=blog_dev;Username=demo;Password=demo");
    }
}
