using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using MovieStore.Infrastructure.Services;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using Moq;

namespace MovieStore.UnitTests.Services
{
    [TestFixture]
    public class MovieServiceTest
    {
        // SUT: system under test
        private MovieService _sut;
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<IFavoriteRepository> _mockFavoriteRepository;
        private Mock<IPurchaseRepository> _mockPuchaseRepository;
        private List<Movie> _movies;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Alternatively putting these test data into the json file.
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie 
                {
                    Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000
                },
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie
                {
                    Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000
                },
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
        }

        [SetUp]
        public void SetUp()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockFavoriteRepository = new Mock<IFavoriteRepository>();
            _mockPuchaseRepository = new Mock<IPurchaseRepository>();
            
            _mockMovieRepository.Setup(m => m.GetHighestRevenueMovies()).ReturnsAsync(_movies);
            _mockMovieRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int m) => _movies.First(x => x.Id == m));
        }
        
        /**
         * Unit test should ideally not touch external database or resources, they should be tested independently
         * Purpose of unit test is to run as many as unit tests as possible very fast. You might have 500 unit tests methods..
         * Before deploying, you need to run the all unit tests.
         * we always do unit tests with in memory fake data...
         *
         * Name should be descriptive.
         * 
         * AAA: arrange, act and assert
         * Arrange: Initializes objects, creates mocks with arguments that are passed to the method under test and adds expectations.
         * Act: Invokes the method or property under test with the arranged parameters.
         * Assert: Verifies that the action of the method under test behaves as expected.
         */
        [Test]
        public async Task Test_GetHighestGrossingMovies_FromFakeData()
        {
            // arrange
            _sut = new MovieService(_mockMovieRepository.Object, _mockFavoriteRepository.Object, _mockPuchaseRepository.Object);
            // act
            var movies = await _sut.GetHighestGrossingMovies();
            // assert
            Assert.NotNull(movies);
            Assert.AreEqual(16, movies.Count());
            CollectionAssert.AllItemsAreInstancesOfType(movies, typeof(MovieCardResponseModel));
        }

        [Test]
        public async Task Test_MovieName_GetMovieById()
        {
            _sut = new MovieService(_mockMovieRepository.Object, _mockFavoriteRepository.Object, _mockPuchaseRepository.Object);
            var movie = await _sut.GetMovieById(10);
            Assert.AreEqual("The Dark Knight", movie.Title);
        }

        [Test]
        public async Task Test_Exception_GetMovieById()
        {
            _sut = new MovieService(_mockMovieRepository.Object, _mockFavoriteRepository.Object, _mockPuchaseRepository.Object);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.GetMovieById(20));
        }
    }
}