using System;
using System.Collections.Generic;

namespace Sakura.Model
{
  public class Thread : Model<Guid>
  {
    public DateTime BumpedAt { get; protected set; }

    public readonly List<Post> Posts = new List<Post>();

    protected Thread() { }

    public Thread(IPostRepository repository, string message)
    {
      // Add op-post.
      CreatePost(repository, message);
    }

    public Post CreatePost(IPostRepository repository, string message)
    {
      Post post = new Post(repository, this, message);
      BumpedAt = DateTime.Now;
      Posts.Add(post);
      return post;
    }
  }
}
