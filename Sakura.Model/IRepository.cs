using System;

namespace Sakura.Model
{
  public interface IRepository<TKey, TModel>
    where TKey : struct
    where TModel : IModel<TKey>
  {
    TModel Get(TKey key);
    TKey Save(TModel item);
  }

  public interface IThreadRepository : IRepository<Guid, Thread> { }

  public interface IPostRepository : IRepository<Guid, Post>
  {
    Post GetLastOrDefault();
  }
}
