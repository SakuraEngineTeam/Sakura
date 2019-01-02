using System;

namespace Sakura.App.Queries
{
  public class PostViewModel
  {
    public readonly long Id;
    public readonly string Message;
    public readonly DateTime CreatedAt;

    public PostViewModel(long id, string message, DateTime createdAt)
    {
      Id = id;
      Message = message;
      CreatedAt = createdAt;
    }
  }
}
