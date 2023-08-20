using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.repository;
using WebApiService.DTO.request;
using WebApiService.DTO.response;

namespace WebApiService.Service.authenservice
{
    public class AuthenService
    {

        //injection
        private readonly UserRepo _userRepository;
        public AuthenService(UserRepo userRepo)
        {
            _userRepository = userRepo;
        }


        //check login
        public object checkLogin(LoginRequest _userLogin, string jwtIssuer, string jwtAudience, string jwtSecretKey)
        {


            if (_userLogin == null || jwtAudience == null || jwtIssuer == null || jwtSecretKey == null)
            {
                return null;
            }
            else
            {
                User user = Authenticate(_userLogin);
                if (user != null)
                {
                    String strToken = GenerateTokenString(user, jwtSecretKey, jwtAudience, jwtIssuer);
                    return new UserRes
                    {
                        UserName = user.UserName,
                        JwtToken = strToken,
                    };
                }
                return null;
            }


        }


        //authenticae

        public User Authenticate(LoginRequest _userLogin)
        {
            var listUser = _userRepository.GetAll().ToList();
            var currentUser = listUser.FirstOrDefault(
                o => o.UserName.Equals(_userLogin.UserName) && o.Password.Equals(_userLogin.Password));

            if (currentUser != null)
            {
                return currentUser;
            }
            else
            {
                return null;
            }


        }

        string GenerateRandomKey(int byteLength)
        {
            using (var rng = new RSACryptoServiceProvider())
            {
                byte[] keyBytes = new byte[byteLength];
                // rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }


        //generate token
        public string GenerateTokenString(User user, string jwtSecretKey, string jwtIssuer, string jwtAudience)
        {
            jwtSecretKey = GenerateRandomKey(128/8);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
              //other claim type should be researched more  

            };

            // var tokenString = new JwtSecurityToken(jwtIssuer, jwtAudience, claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);

            // return new JwtSecurityTokenHandler().WriteToken(tokenString);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        //generate refreshtoken
    }
}