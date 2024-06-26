namespace Miniblog.Brains.Services
{
    using Miniblog.Brains.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DynamoDBBlogService : IBlogService
    {
        public Task DeletePost(Post post) => throw new NotImplementedException();
        public IAsyncEnumerable<string> GetCategories() => throw new NotImplementedException();
        public Task<Post?> GetPostById(string id) => throw new NotImplementedException();
        public Task<Post?> GetPostBySlug(string slug) => throw new NotImplementedException();
        public IAsyncEnumerable<Post> GetPosts() => throw new NotImplementedException();
        public IAsyncEnumerable<Post> GetPosts(int count, int skip = 0) => throw new NotImplementedException();
        public IAsyncEnumerable<Post> GetPostsByCategory(string category) => throw new NotImplementedException();
        public IAsyncEnumerable<Post> GetPostsByTag(string tag) => throw new NotImplementedException();
        public IAsyncEnumerable<string> GetTags() => throw new NotImplementedException();
        public Task<string> SaveFile(byte[] bytes, string fileName, string? suffix = null) => throw new NotImplementedException();
        public Task SavePost(Post post) => throw new NotImplementedException();
    }
}
