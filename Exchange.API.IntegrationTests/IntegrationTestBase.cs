using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.API.IntegrationTests
{

    public class IntegrationTestBase
    {
        public readonly HttpClient httpClient;        

        public IntegrationTestBase()
        {
            var webFactory = new WebApplicationFactory<Program>();
            httpClient = webFactory.CreateDefaultClient();
        }

        public async Task<string> GetToken(string username)
        {

            var content = JsonConvert.SerializeObject(new AuthenticateRequest() { Password = "159632478A@a", Username = username });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Auth/Authenticate", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(contentTemp);

            return user.Token;
        }

    }

}