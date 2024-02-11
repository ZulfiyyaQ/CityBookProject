using CityBookMVCOnionApplication.Abstractions.Repositories.Generic;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.Abstractions.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task AddComment(Comment item);
        Task AddReply(Reply item);
    }
}
