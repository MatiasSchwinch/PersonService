using AutoMapper;
using Person.Domain.PersonAggregate;
using Person.Domain.PersonAggregate.DTO;

namespace Person.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<PersonEntity, PersonEntityDto>().ReverseMap();
            //CreateMap<PersonEntityDto, PersonEntity>()
            //    .ForMember(
            //        source => source.PictureId,
            //        from => from.MapFrom(sel => sel.Picture!.PictureId)
            //    )
            //    .ForMember(
            //        source => source.LocationId,
            //        from => from.MapFrom(sel => sel.Location!.LocationId)
            //    )
            //    .ForMember(
            //        source => source.LoginId,
            //        from => from.MapFrom(sel => sel.Login!.LoginId)
            //    )
            //    .ForMember(
            //        source => source.RegisteredId,
            //        from => from.MapFrom(sel => sel.Registered)
            //    );

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
