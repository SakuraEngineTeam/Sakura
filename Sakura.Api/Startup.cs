using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Sakura.Persistence;

namespace Sakura.Api
{
  public class Startup
  {
    protected readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = Configuration.GetConnectionString("DefaultConnection");

      services.AddTransient<IDbConnection>(provider => {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
      });

      services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<PostRepository>();

      services.AddMvcCore().AddJsonFormatters();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
