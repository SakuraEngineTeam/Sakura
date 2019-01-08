using System;
using System.Collections.Generic;

namespace Sakura.App.Queries
{
  public class ThreadPreviewViewModel
  {
    public readonly Guid ThreadId;
    public readonly Guid Id;
    public readonly long ViewId;
    public readonly string Message;
    public readonly DateTime CreatedAt;
    public readonly DateTime BumpedAt;
    public readonly List<PostViewModel> Posts = new List<PostViewModel>();

    public ThreadPreviewViewModel(
      Guid threadId,
      Guid id,
      long viewId,
      string message,
      DateTime createdAt,
      DateTime bumpedAt)
    {
      ThreadId = threadId;
      Id = id;
      ViewId = viewId;
      Message = message;
      CreatedAt = createdAt;
      BumpedAt = bumpedAt;
    }
  }
}
