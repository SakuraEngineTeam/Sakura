using Moq;
using Sakura.App.Commands;
using Sakura.Model;
using Sakura.Persistence;
using Xunit;

namespace Sakura.App.Tests
{
  public class CreatePostTests
  {
    [Fact]
    public void Handle_ValidCommand_ShouldSavePost()
    {
      var repositoryMock = new Mock<IPostRepository>();
      var handler = new CreatePostHandler(repositoryMock.Object);
      var command = new CreatePost("Howdy");

      handler.Handle(command);

      repositoryMock.Verify(repository => repository.Save(It.IsAny<Post>()));
    }

    [Fact]
    public void Handle_EmptyMessage_ShouldThrowValidationException()
    {
      var repositoryMock = new Mock<IPostRepository>();
      var handler = new CreatePostHandler(repositoryMock.Object);
      var command = new CreatePost("");

      Assert.Throws<ValidationException>(() => handler.Handle(command));
    }
  }
}
