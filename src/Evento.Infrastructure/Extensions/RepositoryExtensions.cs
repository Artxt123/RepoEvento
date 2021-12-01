using System;
using System.Linq;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
       public static async Task<Event> GetOrFailAsync (this IEventRepository repository, Guid id)
       {
           var @event = await repository.GetAsync(id);
           if (@event == null)
           {
                throw new Exception($"Event with ID: '{id}' is does not exist.");
           }

           return @event;
       }

       public static async Task<Event> GetOrFailAsync (this IEventRepository repository, string name)
       {
           var @event = await repository.GetAsync(name);
           if (@event != null)
           {
                throw new Exception($"Event named: '{name}' is already exists.");
           }

           return @event;
       }

       public static async Task<User> GetOrFailAsync (this IUserRepository repository, Guid id)
       {
           var user = await repository.GetAsync(id);
           if (user == null)
           {
                throw new Exception($"User with ID: '{id}' is does not exist.");
           }
           
           return user;
       }

       public static async Task<Ticket> GetTicketOrFailAsync (this IEventRepository repository, Guid eventId, Guid ticketId)
       {
           var @event = await repository.GetOrFailAsync(eventId);
           var ticket = @event.Tickets.SingleOrDefault(x => x.Id == ticketId);
           if (ticket == null)
           {
                throw new Exception($"Ticket with ID: '{ticketId}' was not found for event: '{@event.Name}'");
           }

           return ticket;
       }
    }
}
