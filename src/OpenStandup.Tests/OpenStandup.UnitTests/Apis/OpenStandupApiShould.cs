using System.Threading.Tasks;
using Xunit;

namespace OpenStandup.Mobile.Infrastructure.UnitTests.Apis
{
    public class OpenStandupApiShould
    {
        [Fact]
        public async Task HandleFallbackOnMultipleTimeouts()
        {
          /*  var mockOrderApi = new Mock<IOrderApi>(MockBehavior.Strict);
            mockOrderApi
                .SetupSequence(x => x.SaveOrder(It.IsAny<Order>()))
                .Throws(TestHelper.CreateRefitException(HttpMethod.Post, HttpStatusCode.RequestTimeout))
                .Throws(TestHelper.CreateRefitException(HttpMethod.Post, HttpStatusCode.RequestTimeout))
                .Throws(TestHelper.CreateRefitException(HttpMethod.Post, HttpStatusCode.RequestTimeout));

            var orderService = new OrderService(mockOrderApi.Object, _mockQueueService.Object);

            await orderService.SaveOrder(TestHelper.CreateFakeOrder(3)).ConfigureAwait(false);

            mockOrderApi.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Exactly(4));
            _mockQueueService.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Once); */
        }
    }
}
