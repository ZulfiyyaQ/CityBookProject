using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using CityBookMVCOnionPersistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace CityBookMVCOnionPersistence.Implementations.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        private readonly DbSet<Reservation> _dbreservations;
        private readonly DbSet<Review> _dbreview;

        public PlaceRepository(AppDbContext context) : base(context)
        {
            _dbreservations = context.Set<Reservation>();
            _dbreview = context.Set<Review>();
        }
        public async Task AddReservasion(Reservation item)
        {
            await _dbreservations.AddAsync(item);  
        }
        public void RemoveReservasion(Reservation item)
        {
            _dbreservations.Remove(item);
        }
        public async Task AddReview(Review item)
        {
            await _dbreview.AddAsync(item);
        }
    }

}
