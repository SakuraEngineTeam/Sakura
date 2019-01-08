using System;
using System.Linq;
using AutoMapper;
using Sakura.Model;

namespace Sakura.Persistence
{
  public interface IRepository<TKey, TModel>
    where TKey : struct
    where TModel : IModel<TKey>
  {
    TModel Get(TKey key);
    TKey Save(TModel item);
  }

  public interface IPostRepository : IRepository<Guid, Post>
  {
    Post GetLastOrDefault();
  }

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
      var resource = Context.Posts.SingleOrDefault(p => p.PostId == key);
      if (resource == null) {
        throw new ModelNotFoundException();
      }

      return Mapper.Map<Post>(resource);
    }

    public Post GetLastOrDefault()
    {
      var resource = Context.Posts.OrderByDescending(p => p.PostId).FirstOrDefault();
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
