using System.Linq;
using AutoMapper;
using Sakura.Model;

namespace Sakura.Persistence
{
  public class PostRepository : IRepository<long, Post>
  {
    protected readonly Context Context;
    protected readonly IMapper Mapper;

    public PostRepository(Context context, IMapper mapper)
    {
      Context = context;
      Mapper = mapper;
    }

    public Post Get(long key)
    {
      var resource = Context.Posts.SingleOrDefault(p => p.PostId == key);
      if (resource == null) {
        throw new ModelNotFoundException();
      }

      return Mapper.Map<Post>(resource);
    }

    public long Save(Post item)
    {
      var resource = Mapper.Map<PostResource>(item);
      Context.Posts.Update(resource);
      Context.SaveChanges();
      return resource.PostId;
    }
  }
}
