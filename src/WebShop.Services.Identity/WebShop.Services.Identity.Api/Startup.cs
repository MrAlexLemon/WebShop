using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebShop.Services.Common.Authentication;
using WebShop.Services.Common.ErrorMiddleware;
using WebShop.Services.Common.Mongo;
using WebShop.Services.Common.RabbitMq;
using WebShop.Services.Common.Redis;
using WebShop.Services.Common.Swagger;
using WebShop.Services.Common.Utils;
using WebShop.Services.Identity.Application.Services;
using WebShop.Services.Identity.Core.Entities;
using WebShop.Services.Identity.Core.Repositories;
using WebShop.Services.Identity.Infrastructure.Auth;
using WebShop.Services.Identity.Infrastructure.Mongo.Repositories;

namespace WebShop.Services.Identity.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Autofac.IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
        .AddNewtonsoftJson();
            services.AddRazorPages();
            
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddJwt();
            services.AddRedis();
            services.AddCors();

            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddTransient<IAccessTokenService, AccessTokenService>();
            services.AddTransient<AccessTokenValidatorMiddleware>();

            services.AddTransient<IIdentityService,IdentityService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IClaimsProvider, ClaimsProvider>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddMongo();
            builder.AddMongoRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoRepository<User>("Users");
            builder.AddRabbitMq();
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAccessTokenValidator();

            app.UseServiceId();
            //app.UseMvc();
            app.UseRabbitMq();

            startupInitializer.InitializeAsync();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
