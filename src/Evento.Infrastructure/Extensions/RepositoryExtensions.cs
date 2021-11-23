using System;
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
    }
}
