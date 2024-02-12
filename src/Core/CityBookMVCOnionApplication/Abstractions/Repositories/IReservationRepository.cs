using CityBookMVCOnionApplication.Abstractions.Repositories.Generic;
using CityBookMVCOnionDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionApplication.Abstractions.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
    }
    
}
