using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sakura.App;
using Sakura.App.Commands;
using Sakura.App.Queries;
using Sakura.Model;
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

      AddScoped(services);

      services.AddMvcCore().AddJsonFormatters();
      services.AddCors();
    }

    private void AddScoped(IServiceCollection services)
    {
      services.AddScoped<IThreadRepository, ThreadRepository>();
      services.AddScoped<IPostRepository, PostRepository>();

      services.AddScoped<ICommandDispatcher, CommandDispatcher>();
      services.AddScoped<IQueryDispatcher, QueryDispatcher>();

      services.AddScoped<ICommandHandler<CreateThread, Guid>, CreateThreadHandler>();
      services.AddScoped<ICommandHandler<CreatePost, Guid>, CreatePostHandler>();

      services.AddScoped<IQueryHandler<GetThreadPreviews, IEnumerable<ThreadPreviewViewModel>>, GetThreadPreviewsHandler>();
      services.AddScoped<IQueryHandler<GetThreads, IEnumerable<ThreadViewModel>>, GetThreadsHandler>();
      services.AddScoped<IQueryHandler<GetThread, ThreadViewModel>, GetThreadHandler>();
      services.AddScoped<IQueryHandler<GetPosts, IEnumerable<PostViewModel>>, GetPostsHandler>();
      services.AddScoped<IQueryHandler<GetPost, PostViewModel>, GetPostHandler>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();

        app.UseSpa(spa =>
        {
          spa.Options.SourcePath = "../ClientApp";
          spa.UseAngularCliServer(npmScript: "dev");
        });
      }
      else
      {
        app.UseStatusCodePagesWithReExecute("/api/errors/{0}");
      }

      app.UseCors(builder => {
          builder.AllowAnyMethod();
          builder.SetIsOriginAllowed(s => true);
          builder.AllowAnyHeader();
        }
      );
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
