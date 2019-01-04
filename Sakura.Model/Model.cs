namespace Sakura.Model
{
  public interface IModel<TKey>
    where TKey : struct
  {
    TKey Id { get; }
  }

  public abstract class Model<TKey> : IModel<TKey>
    where TKey : struct
  {
    public TKey Id { get; protected set; }

    protected Model()
    {
      Id = default(TKey);
    }

    public bool IsTransient => Id.Equals(default(TKey));
    public bool IsPersistent => !Id.Equals(default(TKey));
  }
}
