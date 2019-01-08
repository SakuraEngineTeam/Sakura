using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sakura.Model;

namespace Sakura.Persistence
{
  public class PostRepository : IPostRepository
  {
    protected readonly Context Context;
    protected readonly IMapper Mapper;

    public PostRepository(Context context, IMapper mapper)
    {
      Context = context;
      Mapper = mapper;
    }

    public Post Get(Guid key)
    {
      var resource = Context.Posts.AsNoTracking().SingleOrDefault(p => p.PostId == key);
      if (resource == null) {
        throw new ModelNotFoundException();
      }

      return Mapper.Map<Post>(resource);
    }

    public Post GetLastOrDefault()
    {
      var resource = Context.Posts.OrderByDescending(p => p.ViewId).FirstOrDefault();
      return resource != null ? Mapper.Map<Post>(resource) : null;
    }

    public Guid Save(Post item)
    {
      var resource = Mapper.Map<PostResource>(item);
      Context.Posts.Update(resource);
      Context.SaveChanges();
      return resource.PostId;
    }
  }
}
