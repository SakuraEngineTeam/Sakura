using AutoMapper;
using Sakura.Model;

namespace Sakura.Persistence
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Thread, ThreadResource>()
        .ForMember(dst => dst.ThreadId, opt => opt.MapFrom(src => src.Id));
      CreateMap<ThreadResource, Thread>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ThreadId));

      CreateMap<Post, PostResource>()
        .ForMember(dst => dst.PostId, opt => opt.MapFrom(src => src.Id));
      CreateMap<PostResource, Post>()
        .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.PostId));
    }
  }
}
