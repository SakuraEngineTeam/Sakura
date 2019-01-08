using System;
using Moq;
using Sakura.App.Commands;
using Sakura.Model;
using Xunit;

namespace Sakura.App.Tests
{
  public class CreatePostTests
  {
    [Fact]
    public void Handle_ValidCommand_ShouldSavePost()
    {
      var postRepositoryMock = new Mock<IPostRepository>();
      var threadRepositoryMock = new Mock<IThreadRepository>();
      threadRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>()))
        .Returns(new Thread(postRepositoryMock.Object, "Test"));

      var handler = new CreatePostHandler(threadRepositoryMock.Object, postRepositoryMock.Object);
      var command = new CreatePost(Guid.Empty, "Howdy");

      handler.Handle(command);

      threadRepositoryMock.Verify(repository => repository.Save(It.IsAny<Thread>()));
    }

    [Fact]
    public void Handle_EmptyMessage_ShouldThrowValidationException()
    {
      var postRepositoryMock = new Mock<IPostRepository>();

      var threadRepositoryMock = new Mock<IThreadRepository>();
      threadRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>()))
        .Returns(new Thread(postRepositoryMock.Object, "Test"));

      var handler = new CreatePostHandler(threadRepositoryMock.Object, postRepositoryMock.Object);
      var command = new CreatePost(Guid.Empty, "");

      Assert.Throws<ValidationException>(() => handler.Handle(command));
    }
  }
}
