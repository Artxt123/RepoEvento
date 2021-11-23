using System;

namespace Evento.Infrastructure.Commands.Events
{
    public class CreateEvent
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
