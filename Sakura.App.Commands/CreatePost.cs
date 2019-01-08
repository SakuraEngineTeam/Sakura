using System;
using Sakura.Model;

namespace Sakura.App.Commands
{
  public class CreatePost : ICommand<Guid>
  {
    public readonly Guid ThreadId;
    public readonly string Message;

    public CreatePost(Guid threadId, string message)
    {
      ThreadId = threadId;
      Message = message;
    }
  }

  public class CreatePostHandler : ICommandHandler<CreatePost, Guid>
  {
    protected readonly IThreadRepository ThreadRepository;
    protected readonly IPostRepository PostRepository;

    public CreatePostHandler(
      IThreadRepository threadRepository,
      IPostRepository postRepository)
    {
      ThreadRepository = threadRepository;
      PostRepository = postRepository;
    }

    public Guid Handle(CreatePost command)
    {
      Thread thread = ThreadRepository.Get(command.ThreadId);
      thread.CreatePost(PostRepository, command.Message);
      ThreadRepository.Save(thread);
      return PostRepository.GetLastOrDefault()?.Id ?? Guid.Empty;
    }
  }
}
