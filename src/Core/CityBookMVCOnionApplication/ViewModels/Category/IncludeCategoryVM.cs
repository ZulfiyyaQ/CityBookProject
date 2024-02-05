using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionApplication.ViewModels.Category
{
    public record  IncludeCategoryVM
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
    }
}
