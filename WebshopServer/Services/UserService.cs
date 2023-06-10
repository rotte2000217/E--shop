using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebshopServer.Dtos;
using WebshopServer.Enums;
using WebshopServer.Exceptions;
using WebshopServer.Infrastructure;
using WebshopServer.Interfaces;
using WebshopServer.Models;

namespace WebshopServer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfigurationSection _secretKey;
        private readonly WebshopDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(IConfiguration config, WebshopDbContext dbContext, IMapper mapper)
        {
            _secretKey = config.GetSection("SecretKey");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserResponseDto> GetAllUsers()
        {
            return _mapper.Map<List<UserResponseDto>>(_dbContext.Users.ToList());
        }

        public UserResponseDto GetUserById(long id)
        {
            UserResponseDto user = _mapper.Map<UserResponseDto>(_dbContext.Users.Find(id));

            if (user == null)
            {
                throw new ResourceNotFoundException("User with specified id doesn't exist!");
            }

            return user;
        }

        public LoginResponseDto LoginUser(LoginRequestDto requestDto)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Email == requestDto.Email);
            
            if (user == null)
            {
                throw new InvalidCredentialsException("Incorrect login credentials!");
            }

            if (!BCrypt.Net.BCrypt.Verify(requestDto.Password, user.Password))
            {
                throw new InvalidCredentialsException("Incorrect login credentials!");
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            if (user.Role == UserRole.Seller && user.VerificationStatus == VerificationStatus.Accepted)
            {
                claims.Add(new Claim("VerificationStatus", user.VerificationStatus.ToString()));
            }

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "http://localhost:44319",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
            );

            LoginResponseDto responseDto = new LoginResponseDto()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            return responseDto;
        }

        public UserResponseDto RegisterUser(RegisterRequestDto requestDto)
        {
            User user = _mapper.Map<User>(requestDto);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
            user.VerificationStatus = user.Role == UserRole.Seller ? VerificationStatus.Pending : null;

            _dbContext.Users.Add(user);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (UniqueConstraintException)
            {
                throw new InvalidCredentialsException("User with specified username and/or email already exists!");
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public UserResponseDto UpdateUser(long id, UserRequestDto requestDto)
        {
            User user = _dbContext.Users.Find(id);

            if (user == null)
            {
                throw new ResourceNotFoundException("User with specified id doesn't exist!");
            }

            _mapper.Map(requestDto, user);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (UniqueConstraintException)
            {
                throw new InvalidCredentialsException("User with specified username and/or email already exists!");
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public VerificationResponseDto VerifyUser(long id, VerificationRequestDto requestDto)
        {
            User user = _dbContext.Users.Find(id);

            if (user == null)
            {
                throw new ResourceNotFoundException("User with specified id doesn't exist!");
            }

            if (user.Role != UserRole.Seller)
            {
                throw new InvalidFieldsException("Only sellers can be verified!");
            }

            _mapper.Map(requestDto, user);

            _dbContext.SaveChanges();

            return _mapper.Map<VerificationResponseDto>(user);
        }
    }
}
