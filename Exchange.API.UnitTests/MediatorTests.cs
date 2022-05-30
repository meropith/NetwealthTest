using Exchange.API.DAL.Services;
using Exchange.API.Mediator.Behaviours;
using Exchange.API.Mediator.Convert.Queries;
using Exchange.API.Models;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Exchange.API.UnitTests
{
    public class MediatorTests
    {

        private readonly Mock<IConvertService> mockedConvertService = new Mock<IConvertService>();
        private readonly Mock<IMemoryCache> mockedMemoryCache = new Mock<IMemoryCache>();
        private readonly Mock<ICacheable> mockedRequest = new Mock<ICacheable>();

        [Fact]
        public async Task Mediator_Convert_WithValid_Values_Return_Correct_Exchage()
        {

            mockedConvertService.Setup(ser => ser.Convert("EUR", "USD", "Fixer", "TierAPI", 10)).ReturnsAsync(11);
            var dummyQuerry = new ConvertCurrencyQuery() { Amount = 10, FromISO = "EUR", ToISO = "USD", Provider = "Fixer", UserTier = "TierAPI" };

            ConvertCurrency handler = new ConvertCurrency(mockedConvertService.Object);
            var result = await handler.Handle(dummyQuerry, new CancellationToken());

            var value = result as ApiResponse<decimal>;
            value.Data.Should().Be(11);

        }
    }
}
