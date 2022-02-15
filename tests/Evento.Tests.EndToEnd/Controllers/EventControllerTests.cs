using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Evento.Api;
using Evento.Infrastructure.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Evento.Tests.EndToEnd.Controllers
{
    //TEN TEST NALEŻY DOSTOSOWOWAĆ, teraz Startup nie zostanie poprawnie użyty!!!
    public class EventControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public EventControllerTests()
        {
            //Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task fetching_events_should_return_not_empty_collection()
        {
           var response = await _client.GetAsync("events");
           var content = await response.Content.ReadAsStringAsync();
           var events = JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);

           response.StatusCode.Should().Be(HttpStatusCode.OK);
           events.Should().NotBeEmpty();
        }
    }
}
