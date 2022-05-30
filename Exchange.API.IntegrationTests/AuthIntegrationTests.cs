using Exchange.API.Models;
using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.API.IntegrationTests
{

    public class AuthIntegrationTests : IntegrationTestBase
    {
        [Fact]
        public async Task AuthenticateAPITierUser_WithCorrectCredentials_ReturnsUser()
        {

            var content = JsonConvert.SerializeObject(new AuthenticateRequest() { Password = "159632478A@a", Username = "tierapi@tierapi.com" });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Auth/Authenticate", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(contentTemp);

            user.Role.Should().Be("TierAPI");
        }

        [Fact]
        public async Task AuthenticateDBTierUser_WithCorrectCredentials_ReturnsUser()
        {

            var content = JsonConvert.SerializeObject(new AuthenticateRequest() { Password = "159632478A@a", Username = "tierdb@tierdb.com" });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Auth/Authenticate", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(contentTemp);

            user.Role.Should().Be("TierDB");
        }

        [Fact]
        public async Task AuthenticateFreeTierUser_WithCorrectCredentials_ReturnsUser()
        {

            var content = JsonConvert.SerializeObject(new AuthenticateRequest() { Password = "159632478A@a", Username = "free@free.com" });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Auth/Authenticate", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(contentTemp);

            user.Role.Should().Be("FREE");
        }

        [Fact]
        public async Task AuthenticateAPITierUser_InvalidCredentials_ReturnsError()
        {

            var content = JsonConvert.SerializeObject(new AuthenticateRequest() { Password = "wrongpassword", Username = "tierapi@tierapi.com" });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Auth/Authenticate", bodyContent);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}