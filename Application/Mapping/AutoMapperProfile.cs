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
using Microsoft.AspNetCore.Http;

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
            CreateMap<UpdateSportCommand, Sport>();
            CreateMap<AddSportCommand, Sport>()
     .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertImageToByteArray(src.ImageUpload)));

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


            CreateMap<TimeRange, TimeRangeDto>();


        }


        // Helper method to convert IFormFile to byte[]
        private static byte[] ConvertImageToByteArray(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
    }
