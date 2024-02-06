using CityBookMVCOnionApplication.ViewModels.Category;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using CityBookMVCOnionApplication.ViewModels.Employee;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IEmployeeService
    {
        Task<ICollection<ItemEmployeeVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemEmployeeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Employee, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemEmployeeVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemEmployeeVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetEmployeeVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateEmployeeVM create, ModelStateDictionary model);
        Task<UpdateEmployeeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateEmployeeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
