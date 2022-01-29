using Microsoft.Extensions.DependencyInjection;
using api.conevento.ActionFilter;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using dal.conevento.Repository;
using Microsoft.AspNetCore.Builder;
using biz.conevento.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using biz.conevento.Repository.Catalogs;
using dal.conevento.Repository.Catalogs;

namespace api.conevento.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //        //.WithOrigins("http://localhost:4201")
            //        //.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials());
            //});

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost")
                .WithOrigins("http://localhost:4200")
                .WithOrigins("http://34.237.214.147")
                .WithOrigins("http://localhost:63410")
                .WithOrigins("https://conevento.com")
                .WithOrigins("https://www.conevento.com")
                .WithOrigins("http://www.conevento.com")
                .WithOrigins("http://www.conevento.com/Wizard")
                .AllowCredentials();
            }));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICat_categoria_productosRepository, Cat_categoria_productosRepository>();
            services.AddTransient<Icat_productos_serviciosRepository, cat_productos_serviciosRespository>();
            services.AddTransient<IEventosRepository, EventosRepository>();
            services.AddTransient<IcatSubcategoriaProductosRepository, catSubcategoriaProductosRepository>();
        }
        
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }
    }
}
