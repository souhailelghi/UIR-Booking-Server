using Application.Features.SportCategoryFeature.Commands.AddSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Application.Features.StudentFeature.Commands.AddStudent;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            //sport Category 
            CreateMap<AddSportCategoryCommand, SportCategory>().ReverseMap();
            CreateMap<UpdateSportCategoryCommand, SportCategory>();




            //sport
            CreateMap<AddSportCommand, Sport>().ReverseMap();
            CreateMap<UpdateSportCommand, Sport>();

            //Student
            CreateMap<AddStudentCommand, Student>().ReverseMap();




        }
        
    }
}
