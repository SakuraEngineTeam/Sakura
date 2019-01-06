using System;

namespace Sakura.App.Queries
{
  public class PostViewModel
  {
    public readonly Guid Id;
    public readonly long ViewId;
    public readonly string Message;
    public readonly DateTime CreatedAt;

    public PostViewModel(Guid id, long viewId, string message, DateTime createdAt)
    {
      Id = id;
      ViewId = viewId;
      Message = message;
      CreatedAt = createdAt;
    }
  }
}
