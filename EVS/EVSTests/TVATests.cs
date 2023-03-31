using EVSBLL;
using Microsoft.Extensions.DependencyInjection;

namespace EVSTests
{
    public class TVATests
    {
        private ITVAService _tvaService;

        [SetUp]
        public void Setup()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<ITVAService, TVAService>();

            var serviceProvider = services.BuildServiceProvider();

            _tvaService = serviceProvider.GetService<ITVAService>();
        }

        [Test]
        [TestCase("123456789012345")]
        public void CheckValidNumber(string validNumber)
        {
            Assert.IsTrue(_tvaService.IsValid(validNumber));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aa345sdffqcxqfd")]
        [TestCase("1234567890")]
        public void CheckInvalidNumber(string invalidNumber)
        {
            Assert.IsFalse(_tvaService.IsValid(invalidNumber));
        }
    }
}