using AutoMapper;
using MarketAssets.Domain.Models;
using MarketAssets.Fintacharts.Historical.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketAssets.Api.Mapping
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<FintachartsInstrument, MarketAsset>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.BaseCurrency, opt => opt.MapFrom(src => src.BaseCurrency));
        }
    }
}
