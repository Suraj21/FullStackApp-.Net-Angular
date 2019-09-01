using AutoMapper;
using FriendBookApp.API.Dto;
using FriendBookApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendBookApp.API.Helpers
{
    /// <summary>
    /// This uses prorfiles to helps understand the source and the destination what is mapping 
    /// Automapper is a convention based i.e. how we have named the properties
    /// </summary>
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                     opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                     opt.MapFrom(src => src.DateOfBirth.CalculateAge());
                });

            CreateMap<Users, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge());
                }); ;

            CreateMap<Photo, PhotoForDetailedDto>();
        }
    }
}
