using Application.Features.PlanningFeature.Commands.AddPlanning;
using Application.Features.ReservationFeature.Commands.AddReservation;
using Application.Features.SportCategoryFeature.Commands.AddSportCategory;
using Application.Features.SportCategoryFeature.Commands.UpdateSportCategory;
using Application.Features.SportFeature.Commands.AddSport;
using Application.Features.SportFeature.Commands.UpdateSport;
using Application.Features.StudentFeature.Commands.AddStudent;
using AutoMapper;
using Domain.Dtos;
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

            //Reservation
            CreateMap<AddReservationCommand, Reservation>().ReverseMap();

            //Planning Adding 
            // Mapping between PlanningDto and Planning entity
            CreateMap<PlanningDto, Planning>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id in mapping
                .ForMember(dest => dest.TimeRanges, opt => opt.MapFrom(src => src.TimeRanges));

            CreateMap<TimeRangeDto, TimeRange>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id in mapping





        }
        
    }
}
