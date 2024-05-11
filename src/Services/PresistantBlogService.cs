namespace Miniblog.Core.Services
{
    using Miniblog.Core.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PresistantBlogService : IBlogService
    {
        public Task DeletePost(Post post) => throw new System.NotImplementedException();
        public IAsyncEnumerable<string> GetCategories() => throw new System.NotImplementedException();
        public Task<Post?> GetPostById(string id) => throw new System.NotImplementedException();
        public Task<Post?> GetPostBySlug(string slug) => throw new System.NotImplementedException();
        public IAsyncEnumerable<Post> GetPosts() => throw new System.NotImplementedException();
        public IAsyncEnumerable<Post> GetPosts(int count, int skip = 0) => throw new System.NotImplementedException();
        public IAsyncEnumerable<Post> GetPostsByCategory(string category) => throw new System.NotImplementedException();
        public IAsyncEnumerable<Post> GetPostsByTag(string tag) => throw new System.NotImplementedException();
        public IAsyncEnumerable<string> GetTags() => throw new System.NotImplementedException();
        public Task<string> SaveFile(byte[] bytes, string fileName, string? suffix = null) => throw new System.NotImplementedException();
        public Task SavePost(Post post) => throw new System.NotImplementedException();
    }
}
