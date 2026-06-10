using AutoMapper;
using ECommerce.UserService.Core.Dtos;
using ECommerce.UserService.Core.Entities;
using ECommerce.UserService.Core.RepositoryContracts;
using ECommerce.UserService.Core.ServiceContracts;

namespace ECommerce.UserService.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse?> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAndPassword(email, password);

            if (user is null)
            {
                return null;
            }

            //return new AuthenticationResponse(user.UserId,
            //    user.Email,
            //    user.Name ?? string.Empty,
            //    user.Gender ?? string.Empty,
            //    "DummyTokenForNow",
            //    true
            //);

            //return _mapper.Map<AuthenticationResponse>(user, opt =>
            //{
            //    opt.AfterMap((obj, res) =>
            //        {
            //            //res.Token = "Token from mapper";
            //        });
            //});

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Token = "Dummy Token For Now",
                Success = true
            };
        }

        public async Task<AuthenticationResponse> RegisterUser(RegisterUserRequest request)
        {
            //var userObj = new ApplicationUser()
            //{ 
            //    Email = request.Email, 
            //    Password = request.Password, 
            //    Name = request.Name ,
            //    Gender = request.Gender.ToString()
            //};

            var userObj = _mapper.Map<ApplicationUser>(request);
            var registeredUser = await _userRepository.AddUser(userObj);

            if (registeredUser is null)
            {
                return null;
            }

            //return new AuthenticationResponse(registeredUser.UserId,
            //    registeredUser.Email,
            //    registeredUser.Name ?? string.Empty,
            //    registeredUser.Gender ?? string.Empty,
            //    "DummyTokenForNow",
            //    true);

            return _mapper.Map<AuthenticationResponse>(registeredUser) with
            {
                Token = "Dummy Token For Now",
                Success = true
            };
        }
    }
}
