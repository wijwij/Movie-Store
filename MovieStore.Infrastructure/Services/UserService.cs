using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            /*
             * 1. check if the user is already exists
             * 2. create random salt
             * 3. hash password with salt, using industry tested/tried hashing algorithm
             * 4. Create user object
             * 5. Store to database
             * 6. Generate response
             */
            var dbUser = await _userRepository.GetUserByEmailAsync(requestModel.Email);
            if (dbUser != null)
            {
                // ToDo [refactor ]
                // Throw an exception if user exists.
                throw new Exception("Email address already exists. Please try to login");
            }

            var salt = _cryptoService.GenerateSalt();
            var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };

            var createdUser = await _userRepository.AddAsync(user);
            
            var response = new UserRegisterResponseModel{Id = createdUser.Id, Email = createdUser.Email, FirstName = createdUser.FirstName, LastName = createdUser.LastName};

            return response;
        }

        public async Task<LoginResponseModel> ValidateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                // throw exception if user doesn't exist
                throw new Exception("Please Register first");
            }

            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);

            if (hashedPassword.Equals(user.HashedPassword))
            {
                var response = new LoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return response;
            }

            return null;
        }
    }
}