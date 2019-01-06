using System;
using System.Linq;
using Sakura.Model;
using Sakura.Persistence;

namespace Sakura.App.Commands
{
  public class CreatePost : ICommand<Guid>
  {
    public readonly string Message;

    public CreatePost(string message)
    {
      Message = message;
    }
  }

  public class CreatePostHandler : ICommandHandler<CreatePost, Guid>
  {
    protected readonly Context Context;
    protected readonly IPostRepository Repository;

    public CreatePostHandler(Context context, IPostRepository repository)
    {
      Context = context;
      Repository = repository;
    }

    public Guid Handle(CreatePost command)
    {
      long viewId = Context.Posts.Max(p => p.ViewId) + 1;
      var post = new Post(viewId, command.Message);
      return Repository.Save(post);
    }
  }
}
