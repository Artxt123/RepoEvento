using System;
using System.Linq;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Infrastructure.DTO;

namespace Evento.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Event, EventDto>()
                   .ForMember(x => x.TicketsCount, m => m.MapFrom(p => p.Tickets.Count()));
            })
            .CreateMapper();
    }
}
