namespace Sakura.Model
{
  public interface IRepository<TKey, TModel>
    where TKey : struct
    where TModel : IModel<TKey>
  {
    TModel Get(TKey key);
    TKey Save(TModel item);
  }
}
