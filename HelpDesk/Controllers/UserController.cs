using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public UserController(IRepositoryWrapper repository , IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
            
        }

        [HttpPost]
        [Route("[controller]/Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto newUser)
        {

            
            try
            {
                // convert incoming DTO to user model
                var userEntity = _mapper.Map<UserModel>(newUser);
                userEntity.PasswordHash = newUser.Password;

                // saving create user and save user into DB
                _repository.User.CreateUser(userEntity);
                await _repository.Save();

                return Json("User Register Done");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went worng");
            }

        }

        [HttpPost]
        [Route("[controller]/Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model) //not working yet
        {
            //user existing chechk code here ( not dev yet )

            var result = await _repository.User.LoginUser(model);
                

            if (result != null) // demo condition ** will change
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserName", result.UserName.ToString()),
                        new Claim("UserRole", result.UserRole.ToString()),
                        new Claim("UserType", result.UserType.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Json(token);
            }

            return StatusCode(500, "User Not Found");
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]/GetProfile")]
        public async Task<JsonResult> GetProfile()
        {
            String userName = User.Claims.First(c => c.Type == "UserName").Value;
            
            var result = await _repository.User.GetUserByUserName(new Guid(userName));

            var loggedUser = new ProfileDto
            {
                Email = result.Email,
                CompanyId = result.CompanyId,
                UserName = result.UserName,
                UserType = result.UserType,
                FullName = result.FullName,
                Phone = result.Phone,
                UserImage = result.UserImage,
                UserRole = result.UserRole
            };

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repository.User.GetAllUsers();
                return Ok(users);
            }
            catch(Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> GetUserByUserName(Guid userName)
        {
            try
            {
                var user = await _repository.User.GetUserByUserName(userName);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> DeleteUser(Guid userName)
        {
            var user = await _repository.User.GetUserByUserName(userName);
            if(user == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                _repository.User.Delete(user);
                await _repository.Save();
                return Json("User Remove Successfully");
            }
        }
    }
}
