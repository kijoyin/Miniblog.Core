namespace Miniblog.Brains.Services
{
    using Microsoft.EntityFrameworkCore;

    using Miniblog.Brains.Models;
    using Miniblog.Brains.Models.Dto;
    using Miniblog.Infrastructure;

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WilderMinds.MetaWeblog;

    public class PresistantBlogService : IBlogService
    {
        private readonly IBloggingContext _bloggingContext;

        public PresistantBlogService(IBloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }
        public async Task DeletePost(Models.Post post)
        {
            var postDto = await _bloggingContext.Posts.FindAsync(post.ID);
            if(postDto !=null)
            {
                _bloggingContext.Posts.Remove(postDto);
                await _bloggingContext.SaveChangesAsync();
            }
        }
        public async IAsyncEnumerable<string> GetCategories()
        {
            var distcintCategories = new List<string>();
            var categories = await _bloggingContext.Posts.Select(p => p.Categories).ToListAsync();
            foreach(var cat in categories)
            {
                var split = cat.Split('|');
                foreach(var item in split)
                {
                    if(!distcintCategories.Contains(item))
                    {
                        distcintCategories.Add(item);
                        yield return item;
                    }
                }
            }

        }
        public async Task<Models.Post?> GetPostById(string id)
        {
            var postDto = await _bloggingContext.Posts.FindAsync(id);
            return MapPostFromDto(postDto);
        }
        public async Task<Models.Post?> GetPostBySlug(string slug)
        {
            var postDto = await _bloggingContext.Posts.FirstOrDefaultAsync(p=>p.Slug == slug);
            return MapPostFromDto(postDto);
        }
        public async IAsyncEnumerable<Models.Post> GetPosts()
        {
            await foreach(PostDto post in  _bloggingContext.Posts)
            {
                yield return MapPostFromDto(post);
            }
        }
        public async IAsyncEnumerable<Models.Post> GetPosts(int count, int skip = 0)
        {
            var posts = await _bloggingContext.Posts.Skip(skip).Take(count).ToListAsync();
            foreach (PostDto post in posts)
            {
                yield return MapPostFromDto(post);
            }
        }
        public async IAsyncEnumerable<Models.Post> GetPostsByCategory(string category)
        { 
            var posts = await _bloggingContext.Posts.Where(p=>p.Categories.ToLower().Contains(category.ToLower())).ToListAsync();
            foreach (PostDto post in posts)
            {
                yield return MapPostFromDto(post);
            }
        }
        public async IAsyncEnumerable<Models.Post> GetPostsByTag(string tag)
        {
            var posts = await _bloggingContext.Posts.Where(p => p.Tags.ToLower().Contains(tag.ToLower())).ToListAsync();
            foreach (PostDto post in posts)
            {
                yield return MapPostFromDto(post);
            }
        }
        public async IAsyncEnumerable<string> GetTags()
        {
            var distinctTags = new List<string>();
            var tags = await _bloggingContext.Posts.Select(p => p.Tags).ToListAsync();
            foreach (var tag in tags)
            {
                var split = tag.Split('|');
                foreach (var item in split)
                {
                    if (!distinctTags.Contains(item))
                    {
                        distinctTags.Add(item);
                        yield return item;
                    }
                }
            }
        }
        public Task<string> SaveFile(byte[] bytes, string fileName, string? suffix = null) => throw new System.NotImplementedException();
        public async Task SavePost(Models.Post post)
        {
            var postDto = new PostDto()
            {
                Categories = string.Join("|",post.Categories),
                Tags = string.Join("|", post.Tags),
                Content = post.Content,
                Excerpt = post.Excerpt,
                ID = post.ID,
                IsPublished = post.IsPublished,
                LastModified = post.LastModified,
                PubDate = post.PubDate,
                Slug = post.Slug,
                Title = post.Title
            };
            foreach(var comment in post.Comments)
            {
                postDto.Comments.Add(new CommentDto()
                {
                    Author = comment.Author,
                    Content = comment.Content,
                    Email = comment.Email,
                    ID = comment.ID,
                    IsAdmin = comment.IsAdmin,
                    PubDate = comment.PubDate
                });
            }
            await _bloggingContext.Posts.AddAsync(postDto);
            await _bloggingContext.SaveChangesAsync();
        }

        private static Models.Post? MapPostFromDto(PostDto? postDto)
        {
            if (postDto != null)
            {
                var post = new Models.Post()
                {
                    Content = postDto.Content,
                    Excerpt = postDto.Excerpt,
                    ID = postDto.ID,
                    IsPublished = postDto.IsPublished,
                    LastModified = postDto.LastModified,
                    PubDate = postDto.PubDate,
                    Slug = postDto.Slug,
                    Title = postDto.Title,
                };
                foreach (var category in postDto.Categories.Split("|"))
                {
                    post.Categories.Add(category);
                }
                foreach (var comment in postDto.Comments)
                {
                    post.Comments.Add(new Comment()
                    {
                        Author = comment.Author,
                        Content = comment.Content,
                        Email = comment.Email,
                        ID = comment.ID,
                        IsAdmin = comment.IsAdmin,
                        PubDate = comment.PubDate
                    });
                }
                foreach(var tag in  postDto.Tags.Split("|"))
                {
                    post.Tags.Add(tag);
                }
                return post;
            }
            return null;
        }
    }
}
