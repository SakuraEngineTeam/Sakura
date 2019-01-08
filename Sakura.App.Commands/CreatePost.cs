using System;
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
    protected readonly IPostRepository Repository;

    public CreatePostHandler(IPostRepository repository)
    {
      Repository = repository;
    }

    public Guid Handle(CreatePost command)
    {
      Post last = Repository.GetLastOrDefault();
      long viewId = last != null ? Repository.GetLastOrDefault().ViewId + 1 : 1;
      var post = new Post(viewId, command.Message);
      return Repository.Save(post);
    }
  }
}
