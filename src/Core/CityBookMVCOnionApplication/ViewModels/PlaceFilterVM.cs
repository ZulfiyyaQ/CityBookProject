using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionApplication.ViewModels
{
    public class PlaceFilterVM
    {
        public ICollection<ItemPlaceVM> Places { get; set; }
        public ICollection<IncludeCategoryVM> Categories { get; set; }
        
    }
}
