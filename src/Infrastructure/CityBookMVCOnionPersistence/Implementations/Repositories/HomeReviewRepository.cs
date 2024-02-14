using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using CityBookMVCOnionPersistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionPersistence.Implementations.Repositories
{
    public class HomeReviewRepository : Repository<HomeReview>, IHomeReviewRepository
    {
        public HomeReviewRepository(AppDbContext context) : base(context) { }
    }
   
}
