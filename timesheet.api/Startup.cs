using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using timesheet.business;
using timesheet.data;
using timesheet.model;

namespace timesheet.api
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            Configuration = config;
            HostingEnvironment = hostingEnvironment;

            #region GetConnectionString
            if (HostingEnvironment.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("TimesheetDbConnection");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            #endregion

        }

        string connectionString = string.Empty;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimesheetDb>(options =>
                   options.UseSqlServer(connectionString));

            


            services.AddScoped(typeof(EmployeeService));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                  builder => builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    );
            });

            services.AddDbContext<TimesheetDb>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("TimesheetDbConnection")));

            //services.AddRouting();
            services.AddMvc(option=> {
                option.EnableEndpointRouting = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<TimesheetDb>();
                    context.Database.Migrate();
                }
               
            }

            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

            });

        }
    }
}
