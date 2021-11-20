using AutoMapper;
using People.Domain.DTOs.Requests;
using People.Domain.Entities;

namespace People.Domain.Mappings.DTOEntities
{
    public static class DTOEntityMap
    {
        public static IMapper MapDTOEntity()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TechnologyRequestDTO, TechnologyEntity>();
                cfg.CreateMap<ClientRequestDTO, ClientEntity>();
                cfg.CreateMap<SocialNetworkRequestDTO, SocialNetworkEntity>();
                cfg.CreateMap<TechnicalRatingRequestDTO, TechnicalRatingEntity>();
                cfg.CreateMap<PeopleRequestDTO, PeopleEntity>();
                cfg.CreateMap<PeopleTechnicalRatingRequestDTO, PeopleTechnicalRatingEntity>();
                cfg.CreateMap<PeopleSocialNetworkRequestDTO, PeopleSocialNetworkEntity>();
                cfg.CreateMap<PeopleClientRequestDTO, PeopleClientEntity>();

                cfg.CreateMap<TechnologyEntity, TechnologyRequestDTO>();
                cfg.CreateMap<ClientEntity, ClientRequestDTO>();
                cfg.CreateMap<SocialNetworkEntity, SocialNetworkRequestDTO>();
                cfg.CreateMap<TechnicalRatingEntity, TechnicalRatingRequestDTO>();
                cfg.CreateMap<PeopleEntity, PeopleRequestDTO>();
                cfg.CreateMap<PeopleTechnicalRatingEntity, PeopleTechnicalRatingRequestDTO>();
                cfg.CreateMap<PeopleSocialNetworkEntity, PeopleSocialNetworkRequestDTO>();
                cfg.CreateMap<PeopleClientEntity, PeopleClientRequestDTO>();

                //custom properties example
                /*
                cfg.CreateMap<TechnologyDTO, TechnologyEntity>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id));
                */
            });

            return config.CreateMapper();
        }

        public static IMapper MapRequestDTO()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TechnologyRequestWithoutIdDTO, TechnologyRequestDTO>()
                .ForMember(dst => dst.Id, map => map.MapFrom(src => string.Empty));
            });

            return config.CreateMapper();
        }

    }
}
