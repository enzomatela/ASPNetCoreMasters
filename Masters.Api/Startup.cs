using Masters.Api.Filters;
using Masters.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.ItemRepository;
using Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ASPNetCoreMastersTodoList.API.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Masters.Api.Middleware;
using Microsoft.AspNetCore.Http;
using Autofac;

namespace Masters.Api
{
    public class Startup
    {
        private readonly string _corsGetOnlyPolicy = "CorsGetOnlyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: _corsGetOnlyPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin() //.WithOrigins("https://*",..  // specify sites here
                            .WithHeaders(HeaderNames.ContentType, HeaderNames.Accept)
                            .WithMethods("GET");
                    });
            });

            services.AddControllers(o =>
            {
                o.RespectBrowserAcceptHeader = true;
                o.Filters.Add(new ResourcePerformanceFilter());
            }).AddXmlSerializerFormatters();
            //services.AddScoped<IItemRepository, ItemRepository>();
            //services.AddScoped<IItemService, ItemService>();
            services.Configure<Authentication>(Configuration.GetSection("Authentication"));
            services.AddDbContext<DataDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<DataDBContext>()
              .AddDefaultTokenProviders();

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwt:secret"]));
            services.Configure<JwtOptions>(_ => _.SecurityKey = securityKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateAudience = false,
                      ValidateIssuer = false,
                      IssuerSigningKey = securityKey
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanEditItems", policy => policy.Requirements.Add(new ItemOwnerRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, ItemOwnerHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Masters API",
                    Description = "Final Project of Group 1 on .Net Core Masters"
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ItemRepository>().As<IItemRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ItemService>().As<IItemService>()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<RequestLoggingMiddleware>();  //custom
            app.ConfigureCustomExceptionMiddleware();   //custom

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Masters API v1");
            });
        }
    }
}
