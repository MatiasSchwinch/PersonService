﻿using AutoMapper;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;

namespace Person.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<PersonEntity, PersonEntityDto>().ReverseMap();
            CreateMap<PersonEntity, BasicDataDto>().ReverseMap();
            CreateMap<Coordinate, CoordinateDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Login, LoginDto>().ReverseMap();
            CreateMap<Picture, PictureDto>().ReverseMap();
            CreateMap<Registered, RegisteredDto>().ReverseMap();
            CreateMap<Timezone, TimezoneDto>().ReverseMap();
        }
    }
}
