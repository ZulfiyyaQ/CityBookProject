using CityBookMVCOnionApplication.Abstractions.Repositories.Generic;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionApplication.Abstractions.Repositories
{
    public interface IPlaceRepository : IRepository<Place>
    {
        Task AddReservasion(Reservation item);
        void RemoveReservasion(Reservation item);
        Task AddReview(Review item);
    }
}
