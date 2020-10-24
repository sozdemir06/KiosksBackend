using System.Threading.Tasks;
using API.Hubs;
using AutoMapper;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Jwt.Encryption;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.SeedData;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace API
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
            // //Add Database connection
            // services.AddDbContext<DataContext>(opt =>
            // {
            //     opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            // });
            services.AddSingleton<KiosksScreenTracker>();
            services.AddSingleton<UserTracker>();
            services.AddDbContext<DataContext>();

            //services.AddMediatR(typeof(ProductListQuery).Assembly);
            services.AddAutoMapper(typeof(AuthManager));
            services.AddSingleton<SeedContext>();
            services.AddControllers();
            services.AddSignalR();





            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwt =>
                    {
                        jwt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                            ValidateAudience = true,
                            ValidAudience = tokenOptions.Audience,
                            ValidateIssuer = true,
                            ValidIssuer = tokenOptions.Issuer,
                            ValidateLifetime = true
                        };

                        jwt.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    path.StartsWithSegments("/hubs/AdminHub"))
                                {
                                    context.Token = accessToken;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddDependencyResolvers(new ICoreModule[]{
                        new CoreModule(),
                    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedContext seedData)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            seedData.SeedAsync();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AdminHub>("/hubs/AdminHub");
                endpoints.MapHub<KiosksHub>("/hubs/KiosksHub");
            });
        }
    }
}
