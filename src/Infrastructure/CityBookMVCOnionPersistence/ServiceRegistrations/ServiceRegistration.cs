using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionPersistence.Contexts;
using CityBookMVCOnionPersistence.Implementations.Repositories;
using CityBookMVCOnionPersistence.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityBookMVCOnionPersistence.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IHomeReviewRepository, HomeReviewRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBTagRepository, BTagRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IHomeReviewService, HomeReviewService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBTagService, BTagService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<AppDbContextInitializer>();

            return services;
        }
    }
}
