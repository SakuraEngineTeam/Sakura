using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sakura.Model;

namespace Sakura.Persistence
{
  public class ThreadRepository : IThreadRepository
  {
    protected readonly Context Context;
    protected readonly IMapper Mapper;

    public ThreadRepository(Context context, IMapper mapper)
    {
      Context = context;
      Mapper = mapper;
    }

    public Thread Get(Guid key)
    {
      var resource = Context.Threads.AsNoTracking().SingleOrDefault(p => p.ThreadId == key);
      if (resource == null) {
        throw new ModelNotFoundException();
      }

      return Mapper.Map<Thread>(resource);
    }

    public Guid Save(Thread item)
    {
      var resource = Mapper.Map<ThreadResource>(item);
      Context.Threads.Update(resource);
      Context.SaveChanges();
      return resource.ThreadId;
    }
  }
}
