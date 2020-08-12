using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.DataTransferObjects.User;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        [Authorize]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto newUser)
        {

            
            try
            {
                // convert incoming DTO to user model
                 


                var userEntity = _mapper.Map<UserModel>(newUser);

                // PasswordHasher
                
                string password = newUser.Password;

                var data = Encoding.ASCII.GetBytes(password);
                var sha1 = new SHA1CryptoServiceProvider();
                var hashed = sha1.ComputeHash(data);
                
                userEntity.PasswordHash = System.Text.Encoding.UTF8.GetString(hashed);



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
        [Route("[controller]/CheckAvaibality")]
        public async Task<IActionResult> CheckAvaibality([FromBody] CheckAvaibalityDto data)
        {
            return Ok(await _repository.User.CheckAvaibality(data) );
        }

        [HttpPost]
        [Route("[controller]/Login")]
      
        public async Task<IActionResult> Login([FromBody] UserLoginDto model) //not working yet
        {

            // converting password to hashcode

            string password = model.Password;

            var data = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var hashed = sha1.ComputeHash(data);

            model.Password = System.Text.Encoding.UTF8.GetString(hashed);


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
            var usersList = new List<UserDto>();
            try
            {
                //geting information from JWT decript ....
                var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
                var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;
                
               if(userRole == "Manager")
                {
                    var users = await _repository.User.GetUsersByCondition(userType , userCompanyId);
                    
                    //converting user model list to UserDto List
                    foreach(var user in users)
                    {
                        if(user.CompanyId != null)
                        {
                            usersList.Add( _mapper.Map<UserDto>(user));
                        }
                    
                    }

                    // adding Company Name to each user
                    foreach(var user in usersList)
                    {
                        var tempUserRes = await _repository.Company.GetCompanyById(new Guid(user.CompanyId));
                        
                        if(tempUserRes == null)
                        {
                            user.CompanyName = null ;
                        }

                        user.CompanyName = tempUserRes.CompanyName;
                    }

                    return Ok(usersList);

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
        [Authorize]
        public async Task<IActionResult> GetUserByUserName(string userName)
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
        [Route("[controller]/{userName}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _repository.User.GetUserByUserName(userName);
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
