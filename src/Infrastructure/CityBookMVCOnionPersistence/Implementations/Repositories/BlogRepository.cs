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

        public BlogRepository(AppDbContext context) : base(context)
        {

            _dbComment = context.Set<Comment>();
            _dbReply = context.Set<Reply>();
        }

        public async Task AddComment(Comment item)
        {
            await _dbComment.AddAsync(item);
        }
        public async Task AddReply(Reply item)
        {
            await _dbReply.AddAsync(item);
        }
    }
}
