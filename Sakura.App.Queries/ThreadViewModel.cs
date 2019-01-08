using System;

namespace Sakura.App.Queries
{
  public class ThreadViewModel
  {
    public readonly Guid ThreadId;
    public readonly DateTime BumpedAt;

    public ThreadViewModel(
      Guid threadId,
      DateTime bumpedAt)
    {
      ThreadId = threadId;
      BumpedAt = bumpedAt;
    }
  }
}
