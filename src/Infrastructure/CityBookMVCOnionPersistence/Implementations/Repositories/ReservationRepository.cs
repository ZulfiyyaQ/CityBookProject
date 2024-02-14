using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Repositories.Generic;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using CityBookMVCOnionPersistence.Implementations.Repositories.Generic;

namespace CityBookMVCOnionPersistence.Implementations.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context) { }
    }
}
