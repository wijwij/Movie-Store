using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Services;
using NUnit.Framework;

namespace MovieStore.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserService _sut;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ICryptoService> _mockCryptoService;
        // private IMock<IFavoriteRepository> _mockFavoriteRepository;
        // private IMock<IPurchaseRepository> _mockPurchaseRepository;
        // private IMock<IReviewRepository> _mockReviewRepository;
        private List<User> _users;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _users = new List<User>
            {
                new User{Id = 1, Email = "John@fake.com", FirstName = "John", LastName = "Doe", HashedPassword = "somehashpassword"},
                new User{Id = 2, Email = "Jason@fake.com", FirstName = "Jason", LastName = "Zhang", HashedPassword = "jasonzhangpassword"}
            };
        }

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCryptoService = new Mock<ICryptoService>();
            
            // _mockUserRepository.Setup(u => u.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((string e) => null);
            _mockUserRepository.Setup(u => u.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string e) => _users[0]);
            _mockCryptoService.Setup(c => c.HashPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string password, string salt) => password + "?");
        }
        
        [Test]
        public async Task Test_UserEmailNotExits_ValidateUser()
        {
            _sut = new UserService(_mockUserRepository.Object, null, null, null, null);
            Assert.ThrowsAsync<Exception>(async () => await _sut.ValidateUser("a@gmail.com", "fakepassword"));
        }

        [Test]
        public async Task Test_InvalidPassword_ValidateUser()
        {
            _sut = new UserService(_mockUserRepository.Object, _mockCryptoService.Object, null, null, null);
            var user = await _sut.ValidateUser("John@fake.com", "abc");
            Assert.IsNull(user);
        }
    }
}