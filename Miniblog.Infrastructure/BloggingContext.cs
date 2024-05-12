using Microsoft.EntityFrameworkCore;

using Miniblog.Brains.Models.Dto;

using System.Collections.Generic;

namespace Miniblog.Infrastructure
{
    public class BloggingContext : DbContext
    {
        //public DbSet<Blog> Blogs { get; set; }
        public DbSet<PostDto> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}
