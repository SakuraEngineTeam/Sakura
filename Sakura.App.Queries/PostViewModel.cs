using System;

namespace Sakura.App.Queries
{
  public class PostViewModel
  {
    public readonly Guid ThreadId;
    public readonly Guid Id;
    public readonly long ViewId;
    public readonly string Message;
    public readonly DateTime CreatedAt;

    protected PostViewModel() { }

    public PostViewModel(
      Guid threadId,
      Guid id,
      long viewId,
      string message,
      DateTime createdAt)
    {
      ThreadId = threadId;
      Id = id;
      ViewId = viewId;
      Message = message;
      CreatedAt = createdAt;
    }
  }
}
