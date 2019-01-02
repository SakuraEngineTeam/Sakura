using System;
using System.ComponentModel.DataAnnotations;

namespace Sakura.Persistence
{
  public class PostResource
  {
    [Key] public long PostId { get; set; }

    [Required] public string Message { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
