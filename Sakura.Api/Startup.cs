using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sakura.App;
using Sakura.App.Commands;
using Sakura.App.Queries;
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
      services.AddTransient<IConnectionFactory, ConnectionFactory>();

      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

      var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<IPostRepository, PostRepository>();

      services.AddScoped<ICommandDispatcher, CommandDispatcher>();
      services.AddScoped<IQueryDispatcher, QueryDispatcher>();

      services.AddScoped<ICommandHandler<CreatePost, long>, CreatePostHandler>();
      services.AddScoped<IQueryHandler<GetPosts, IEnumerable<PostViewModel>>, GetPostsHandler>();
      services.AddScoped<IQueryHandler<GetPost, PostViewModel>, GetPostHandler>();

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
