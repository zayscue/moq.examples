using Example1.Data;
using Example1.Data.Abstractions;
using Example1.Data.Repositories;
using Example1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example1
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            if (Env.IsEnvironment("Test"))
            {
                services.AddDbContext<TodoContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "TodoDb"));
            }
            else
            {
                services.AddDbContext<TodoContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TodoDb")));
            }
            services.AddTransient<IRepositoryBase<Reminder>, ReminderRepository>();

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
