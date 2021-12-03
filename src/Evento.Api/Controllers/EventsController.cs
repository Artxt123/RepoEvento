using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController : ApiControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public EventsController(IEventService eventService, IMemoryCache cache, ILogger<EventsController> logger)
        {
            _eventService = eventService;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
       //[AllowAnonymous]
        public async Task<IActionResult> Get(string name)
        {
            var events = _cache.Get<IEnumerable<EventDto>>("events"); //key like in Dictionary
            if (events == null)
            {
                //Console.WriteLine("Fetching from service.");
                _logger.LogTrace("Fetching events from service");
                events = await _eventService.BrowseAsync(name);
                _cache.Set("events", events, TimeSpan.FromMinutes(1)); //key; value; how long data will be in cache
            }
            else
            {
                //Console.WriteLine("Fetching from cache.");
                _logger.LogTrace("Fetching events from cache");
            }

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var @event = await _eventService.GetAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            return Json(@event);
        }

        [HttpPost]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            command.EventId = Guid.NewGuid();
            await _eventService.CreateAsync(command.EventId, command.Name, command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketsAsync(command.EventId, command.Amount, command.Price);

                //Location header -> /events/ID
                    //null, bo nie zwracamy żadnego modelu 
            return Created($"/events/{command.EventId}", null);
        }

        //  /events/{id} -> HTTP PUT
        [HttpPut("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody]UpdateEvent command)
        {
            await _eventService.UpdateAsync(eventId, command.Name, command.Description);

                //204
            return NoContent();
        }

        [HttpDelete("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _eventService.DeleteAsync(eventId);

                //204
            return NoContent();
        }
    }
}
