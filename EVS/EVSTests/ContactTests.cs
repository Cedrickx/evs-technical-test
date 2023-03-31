using EVSBLL;
using EVSBLL.BusinessObjects;
using EVSDAL;
using EVSDAL.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EVSTests
{
    public class ContactTests
    {
        private IContactService _contactService;
        private ICompanyService _companyService;

        [OneTimeSetUp]
        public void Setup()
        {
            EVSDbContext.InMemory = true;

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<EVSDbContext>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ITVAService, TVAService>();

            var serviceProvider = services.BuildServiceProvider();

            _contactService = serviceProvider.GetService<IContactService>();
            _companyService = serviceProvider.GetService<ICompanyService>();

            _companyService.CreateCompany(new CompanyBO
            {
                Id = 11111,
                Name = "EVS",
                HeadQuarters = "Sart-Tilman",
                TVANumber = "123456789012345",
            });
        }

        [Test]
        [TestCase("Cedric", "Herstal", ContactType.Freelancer, "123456789012345")]
        [TestCase("Cedric", "Herstal", ContactType.Employee, null)]
        public void CheckValidContact(string name, string address, ContactType contactType, string tvaNumber)
        {
            Assert.DoesNotThrow(() => _contactService.CreateContact(name, address, new List<int>() { 11111 }, contactType, tvaNumber));
        }

        [Test]
        [TestCase("Cedric", "Herstal", ContactType.Freelancer, null)]
        public void CheckInvalidContact(string name, string address, ContactType contactType, string tvaNumber)
        {
            Assert.Throws<Exception>(() => _contactService.CreateContact(name, address, new List<int>() { 11111 }, contactType, tvaNumber));
        }
    }
}