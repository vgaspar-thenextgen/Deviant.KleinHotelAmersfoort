using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services.Models;
using Deviant.KleinHotelAmersfoort.WebApi.Models;

namespace Deviant.KleinHotelAmersfoort.WebApi
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<ReactionSaveRequest, Reaction>();
            CreateMap<Reaction, ReactionView>();
        }
    }
}
