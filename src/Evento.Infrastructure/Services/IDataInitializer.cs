using System;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
