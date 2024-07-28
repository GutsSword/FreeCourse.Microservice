using AutoMapper;
using FreeCourse.CatologService.Dtos;
using FreeCourse.CatologService.Dtos.CategoryDtos;
using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.CatologService.Entities;

namespace FreeCourse.CatologService.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Category, CreateCourseDto>();
            CreateMap<Category, CourseDto>();
            CreateMap<Category, UpdateCourseDto>();

            CreateMap<Category, FeatureDto>();
            
        }
    }
}
