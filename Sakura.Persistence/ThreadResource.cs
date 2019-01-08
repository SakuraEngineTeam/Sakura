using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sakura.Model;

namespace Sakura.Persistence
{
  [Table("threads")]
  public class ThreadResource
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ThreadId { get; set; }

    public DateTime BumpedAt { get; set; }

    [InverseProperty("Thread")]
    public List<PostResource> Posts { get; set; }
  }
}
