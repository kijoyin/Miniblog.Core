namespace Miniblog.Infrastructure
{
    using Microsoft.EntityFrameworkCore;

    using Miniblog.Brains.Models.Dto;

    public interface IBloggingContext
    {
        DbSet<CommentDto> Comments { get; set; }
        DbSet<PostDto> Posts { get; set; }
        Task<int> SaveChangesAsync();
    }
}
