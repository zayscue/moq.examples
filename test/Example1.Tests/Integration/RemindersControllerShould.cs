using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Example1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Example1.Tests
{
    public class RemindersControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RemindersControllerShould()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>()
            );
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnFiveReminders()
        {
            // When
            var response = await _client.GetAsync("api/reminders");

            // Then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var reminders = JsonConvert.DeserializeObject<List<Reminder>>(responseString);
            Assert.Equal(5, reminders.Count);
        }
    }
}
