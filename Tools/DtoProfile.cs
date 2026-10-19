using AutoMapper;
using BEFESkeleton.Dtos;
using BEFESkeleton.Models;

namespace BEFESkeleton.Tools
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            //ide minden kell, amiről engedünk mappelést
            CreateMap<CarCreateDto, Car>();
            CreateMap<Car, CarViewDto>()
                .AfterMap((model, dto) =>
                {
                    dto.BrandAndModel = model.Brand + " " + model.Model;
                });
        }
    }
}
