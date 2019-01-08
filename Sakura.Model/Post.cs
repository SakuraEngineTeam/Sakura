using System;

namespace Sakura.Model
{
  public class Post : Model<Guid>
  {
    public long ViewId { get; protected set; }
    public string Message { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    public Thread Thread { get; protected set; }

    protected Post() { }

    public Post(
      IPostRepository repository,
      Thread thread,
      string message)
    {
      if (thread == null) {
        throw new ValidationException("Post should have parent thread");
      }

      if (string.IsNullOrWhiteSpace(message)) {
        throw new ValidationException("Post message should not be empty");
      }

      Post lastPost = repository.GetLastOrDefault();
      ViewId = (lastPost?.ViewId ?? 0) + 1;
      Message = message;
      CreatedAt = DateTime.Now;
      Thread = thread;
    }
  }
}
