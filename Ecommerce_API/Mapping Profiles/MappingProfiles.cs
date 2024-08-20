using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;

namespace Ecommerce.API.Mapping_Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, ProductDTO>()
                .ForMember(To => To.Category_Name, from => from.MapFrom(x => x.Category != null ? x.Category.Name : null));

            CreateMap<LocalUser, LocalUserDTO>().ReverseMap();
        }
    }
}
