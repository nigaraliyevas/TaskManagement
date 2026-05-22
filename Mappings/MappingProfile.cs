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
                 ? src.Deadline.Value.ToUniversalTime()
                 : (DateTime?)null));

        CreateMap<TaskItem, TaskViewModel>()
            .ForMember(
                dest => dest.Deadline,
                opt => opt.MapFrom(src =>
                    src.Deadline.HasValue
                        ? src.Deadline.Value.ToLocalTime()
                        : (DateTime?)null));
    }
}