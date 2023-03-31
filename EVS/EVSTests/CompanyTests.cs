using EVSBLL;
using EVSBLL.BusinessObjects;
using EVSDAL;
using Microsoft.Extensions.DependencyInjection;

namespace EVSTests
{
    public class CompanyTests
    {
        private ICompanyService _companyService;

        [OneTimeSetUp]
        public void Setup()
        {
            EVSDbContext.InMemory = true;

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<EVSDbContext>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ITVAService, TVAService>();

            var serviceProvider = services.BuildServiceProvider();

            _companyService = serviceProvider.GetService<ICompanyService>();
        }

        [Test]
        [TestCase("EVS", "Sart-Tilman", "123456789012345")]
        public void CheckValidCompany(string name, string headquarters, string tvaNumber)
        {
            CompanyBO company = new CompanyBO
            {
                Name = name,
                HeadQuarters = headquarters,
                TVANumber = tvaNumber
            };

            Assert.DoesNotThrow(() => _companyService.CreateCompany(company));
        }

        [Test]
        [TestCase(null, "Sart-Tilman", "123456789012345")]
        [TestCase("EVS", null, "123456789012345")]
        [TestCase("", "Sart-Tilman", "123456789012345")]
        [TestCase("EVS", "", "123456789012345")]
        [TestCase("EVS", "Sart-Tilman", "123456789045")]
        [TestCase("EVS", "Sart-Tilman", null)]
        [TestCase("EVS", "Sart-Tilman", "")]
        public void CheckInvalidCompany(string name, string headquarters, string tvaNumber)
        {
            CompanyBO company = new CompanyBO
            {
                Name = name,
                HeadQuarters = headquarters,
                TVANumber = tvaNumber
            };

            Assert.Throws<Exception>(() => _companyService.CreateCompany(company));
        }
    }
}