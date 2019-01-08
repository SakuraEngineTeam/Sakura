using System;
using System.Collections.Generic;

namespace Sakura.App.Queries
{
  public class ThreadPreviewViewModel
  {
    public readonly Guid ThreadId;
    public readonly DateTime BumpedAt;
    public readonly List<PostViewModel> Posts = new List<PostViewModel>();

    public ThreadPreviewViewModel(
      Guid threadId,
      DateTime bumpedAt)
    {
      ThreadId = threadId;
      BumpedAt = bumpedAt;
    }
  }
}
