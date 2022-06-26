using Microsoft.EntityFrameworkCore;
using ProyectoWebApi.BaseService;
using ProyectoWebApi.Repositories;
using ProyectoWebApi.Services;
using System.ComponentModel;

namespace ProyectoWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Bdd Context
            services.AddDbContext<ApplicationDbContext>();

            //repositories Scopes
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();

            //services Scopes
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ICuentaService, CuentaService>();
            services.AddScoped<IMovimientoService, MovimientoService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
