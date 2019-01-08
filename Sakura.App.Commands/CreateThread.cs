using System;
using Sakura.Model;

namespace Sakura.App.Commands
{
  public class CreateThread : ICommand<Guid>
  {
    public readonly string Message;

    public CreateThread(string message)
    {
      Message = message;
    }
  }

  public class CreateThreadHandler : ICommandHandler<CreateThread, Guid>
  {
    protected readonly IThreadRepository ThreadRepository;
    protected readonly IPostRepository PostRepository;

    public CreateThreadHandler(
      IThreadRepository threadRepository,
      IPostRepository postRepository)
    {
      ThreadRepository = threadRepository;
      PostRepository = postRepository;
    }

    public Guid Handle(CreateThread command)
    {
      Thread thread = new Thread(PostRepository, command.Message);
      return ThreadRepository.Save(thread);
    }
  }
}
