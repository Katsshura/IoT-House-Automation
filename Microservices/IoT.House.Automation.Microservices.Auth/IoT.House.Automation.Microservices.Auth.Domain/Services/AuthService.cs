using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Domain.Services
{
    public class AuthService : IAuth
    {
        private readonly IJwt _jwtService;
        private readonly IAuthRepository _authRepository;
        private readonly IMap _mapper;

        public AuthService(IJwt jwtService, IAuthRepository authRepository, IMap mapper)
        {
            _jwtService = jwtService;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public string Signin(Login login)
        {
            var isValid = _authRepository.IsCredentialsValid(login);

            if (!isValid) return string.Empty;

            var token = _jwtService.CreateToken(login.Username);

            return token;
        }

        public User GetSignedUser(string token)
        {
            var isValid = _jwtService.IsTokenValid(token);

            if (!isValid) return null;

            const string username = "Implement jwt username retriever";
            var rs = _authRepository.GetUserInformation(username);
            var user = _mapper.Map<User>(rs.Tables[0]).FirstOrDefault();
            
            return user;
        }
    }
}
