using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL;
using Deviant.KleinHotelAmersfoort.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace Deviant.KleinHotelAmersfoort.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterInstance<IReactionsRepository>(new ReactionsListRepository());
            container.RegisterInstance<IUsersRepository>(new UsersListRepository());
            container.RegisterType<IReactionsService, ReactionsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
