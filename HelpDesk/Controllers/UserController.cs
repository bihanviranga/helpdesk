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

                var registredUser = _mapper.Map<UserDto>(userEntity);

                return Ok(registredUser);
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
                        new Claim("CompanyId", result.CompanyId.ToString()),
                        new Claim("Email", result.Email.ToString()),
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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;
                
               if(userRole == "Manager")
                {
                    var users = await _repository.User.GetUsersByCondition(userType , userCompanyId);
                    ///var usersDto = _mapper.Map<UserDto>(users);
                    return Ok(users);
                }else if (userRole == "User")
                {
                    return StatusCode(401, "401 Unauthorized  Access");
                }

                return StatusCode(500, "Something went wrong");
               
            }
            catch(Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpGet]
        [Route("[controller]/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            try
            {

                var user = await _repository.User.GetUserByUserName(new Guid(userName));
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }

        [HttpDelete]
        [Route("[controller]/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _repository.User.GetUserByUserName(new Guid(userName));
            if(user == null)
            {
                return StatusCode(500, "User Not Found");
            }
            else
            {
                var deletedUser = _mapper.Map<UserDto>(user);
                _repository.User.Delete(user);
                await _repository.Save();
                return Ok(deletedUser);
            }
        }
    }
}
