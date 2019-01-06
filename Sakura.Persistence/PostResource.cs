using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakura.Persistence
{
  public class PostResource
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PostId { get; set; }

    public long ViewId { get; set; }

    [Required] public string Message { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
