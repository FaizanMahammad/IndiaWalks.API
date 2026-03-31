using AutoMapper;
using IndiaWalks.API.Models.Domain;
using IndiaWalks.API.Models.DTO;

namespace IndiaWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
            /*----In case of Different Property names in Source & Destination
        public AutoMapperProfiles()
        {
            CreateMap<UserDto, UserDomain>()  //<Source,Destination>
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
                .ReverseMap();               //ReverseMap
        }

        public class UserDto
        {
            public string FullName { get; set; }
        }
        public class UserDomain
        {
            public string Name { get; set; }
        }
        */
    }
}
