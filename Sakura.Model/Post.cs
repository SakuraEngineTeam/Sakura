using System;

namespace Sakura.Model
{
  public class Post : Model<Guid>
  {
    public long ViewId { get; protected set; }
    public string Message { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    protected Post() { }

    public Post(long viewId, string message) : this()
    {
      if (string.IsNullOrWhiteSpace(message)) {
        throw new ValidationException("Post message should not be empty");
      }

      ViewId = viewId;
      Message = message;
      CreatedAt = DateTime.Now;
    }
  }
}
