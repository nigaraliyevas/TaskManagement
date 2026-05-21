using AutoMapper;
using TaskManagement.Models;
using TaskManagement.ViewModels;

namespace TaskManagement.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskViewModel, TaskItem>()
     .ForMember(
         dest => dest.Deadline,
         opt => opt.MapFrom(src =>
             src.Deadline.HasValue
                 ? src.Deadline.Value.ToUniversalTime()  // local(Baku) → UTC
                 : (DateTime?)null));

        CreateMap<TaskItem, TaskViewModel>()
            .ForMember(
                dest => dest.Deadline,
                opt => opt.MapFrom(src =>
                    src.Deadline.HasValue
                        ? src.Deadline.Value.ToLocalTime()  // UTC → local(Baku)
                        : (DateTime?)null));
    }
}