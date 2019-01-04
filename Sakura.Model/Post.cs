using System;

namespace Sakura.Model
{
  public class Post : Model<long>
  {
    public string Message { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    protected Post() { }

    public Post(string message) : this()
    {
      if (string.IsNullOrWhiteSpace(message)) {
        throw new ValidationException("Post message should not be empty");
      }

      Message = message;
      CreatedAt = DateTime.Now;
    }
  }
}
