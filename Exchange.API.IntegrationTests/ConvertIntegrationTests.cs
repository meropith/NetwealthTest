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

    public class ConvertIntegrationTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetCurrenciesAsync()
        {
            var token = await GetToken("free@free.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await httpClient.GetAsync("Convert/GetCurrencies");

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<Dictionary<string, string>>>(contentTemp);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Count.Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task ConvertAsFreeUser()
        {
            var token = await GetToken("free@free.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Fixer",
                ToISO = "USD"
            });

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<decimal>>(contentTemp);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task ConvertAsDBUser()
        {
            var token = await GetToken("tierdb@tierdb.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Fixer",
                ToISO = "USD"
            });

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<decimal>>(contentTemp);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task ConvertAsAPIUser()
        {
            var token = await GetToken("tierapi@tierapi.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Fixer",
                ToISO = "USD"
            });

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<decimal>>(contentTemp);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().BeGreaterThanOrEqualTo(0);

            content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Exchangerate",
                ToISO = "USD"
            });

            bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            contentTemp = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<ApiResponse<decimal>>(contentTemp);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().BeGreaterThanOrEqualTo(0);
        }



        [Fact]
        public async Task Verify_FreeUser_Hit_Cache_AfterDbUserMakesSameCall()
        {
            var watch = new System.Diagnostics.Stopwatch();

            var token = await GetToken("tierdb@tierdb.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Fixer",
                ToISO = "USD"
            });

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            watch.Start();
            var response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            watch.Stop();

            var dbUserExecutionTime = watch.ElapsedMilliseconds;

            token = await GetToken("free@free.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            content = JsonConvert.SerializeObject(new ConvertRequest()
            {
                Amount = 10,
                FromISO = "EUR",
                Provider = "Fixer",
                ToISO = "USD"
            });
            watch = new System.Diagnostics.Stopwatch();
            bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            watch.Start();
            response = await httpClient.PostAsync("Convert/Convert", bodyContent);
            watch.Stop();

            var freeUserExecutionTime = watch.ElapsedMilliseconds;

            freeUserExecutionTime.Should().BeLessThan(dbUserExecutionTime);

        }

    }

}