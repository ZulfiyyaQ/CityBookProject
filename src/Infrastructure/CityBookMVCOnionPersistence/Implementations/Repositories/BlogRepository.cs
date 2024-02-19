using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using CityBookMVCOnionPersistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace CityBookMVCOnionPersistence.Implementations.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly DbSet<Comment> _dbComment;
        private readonly DbSet<Reply> _dbReply;
        private readonly DbSet<BlogTag> _dbTag;

        public BlogRepository(AppDbContext context) : base(context)
        {
            _dbComment = context.Set<Comment>();
            _dbReply = context.Set<Reply>();
            _dbTag = context.Set<BlogTag>();
        }

        public async Task AddComment(Comment item)
        {
            await _dbComment.AddAsync(item);
        }
        public async Task AddReply(Reply item)
        {
            await _dbReply.AddAsync(item);
        }
        public void DeleteTag(BlogTag item)
        {
            _dbTag.Remove(item);
        }
    }
}
