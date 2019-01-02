namespace Sakura.Model
{
  public interface IModel<TKey>
    where TKey : struct
  {
    TKey Id { get; }
  }
}
