using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TrashTalker.Database;
using TrashTalker.Database.Repositories;
using TrashTalker.Database.Repositories.AlertRepository;
using TrashTalker.Database.Repositories.ContainerRepository;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Database.Repositories.PickingRepository;
using TrashTalker.Database.Repositories.RecycleBinRepository;
using TrashTalker.Database.Repositories.RouteRepository;
using TrashTalker.Database.Repositories.UserRepository;
using TrashTalker.Database.Triggers;
using TrashTalker.Services;

namespace TrashTalker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DBConnection"));
                options.UseTriggers(triggersOptions =>
                {
                    triggersOptions.AddTrigger<AddPickingTrigger>();
                    triggersOptions.AddTrigger<UpdateLastMeasurement>();
                });
            },ServiceLifetime.Transient);

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Config.SECRET);

            services.AddAuthentication(configOptions =>
                {
                    configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddHttpClient();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();
            services.AddScoped<IRecycleBinRepository, RecycleBinRepository>();
            services.AddScoped<IPickingRepository, PickingRepository>();
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();

            //Services
            services.AddScoped<IMeasuramentsService, MeasuramentsService>();
            services.AddScoped<IDistanceMatrixService, DistanceMatrixService>();
            services.AddScoped<IGeoLocationService, GeoLocationService>();
            services.AddScoped<IRouteOptimizationService, RouteOptimizationServiceService>();
            services.AddScoped<IQrCodeService, QrCodeService>();

            services.AddCors();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "TrashTalker", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrashTalker v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Authorization")
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}