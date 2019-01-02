using Sakura.Model;
using Sakura.Persistence;

namespace Sakura.App.Commands
{
  public class CreatePost : ICommand<long>
  {
    public readonly string Message;

    public CreatePost(string message)
    {
      Message = message;
    }
  }

  public class CreatePostHandler : ICommandHandler<CreatePost, long>
  {
    protected readonly Context Context;
    protected readonly PostRepository Repository;

    public CreatePostHandler(Context context, PostRepository repository)
    {
      Context = context;
      Repository = repository;
    }

    public long Handle(CreatePost command)
    {
      var post = new Post(command.Message);
      return Repository.Save(post);
    }
  }
}
