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
        public UserController(IRepositoryWrapper repository, IMapper mapper)
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
            return Ok(await _repository.User.CheckAvaibality(data));
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

            return StatusCode(404, "User Not Found");
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

                if (userRole == "Manager")
                {
                    var users = await _repository.User.GetUsersByCondition(userType, userCompanyId);

                    //converting user model list to UserDto List
                    foreach (var user in users)
                    {
                        if (user.CompanyId != null)
                        {
                            usersList.Add(_mapper.Map<UserDto>(user));
                        }

                    }

                    // adding Company Name to each user
                    foreach (var user in usersList)
                    {
                        var tempUserRes = await _repository.Company.GetCompanyById(new Guid(user.CompanyId));

                        if (tempUserRes == null)
                        {
                            user.CompanyName = null;
                        }

                        user.CompanyName = tempUserRes.CompanyName;
                    }

                    return Ok(usersList);

                } else if (userRole == "User")
                {
                    return StatusCode(401, "401 Unauthorized  Access");
                }

                return StatusCode(500, "Something went wrong");

            }
            catch (Exception)
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

        [HttpPost]
        [Route("[controller]/resetPassword")]
        [Authorize]
        
        //check the permissions and resturn user Details if the user have permission -----> reset password phase 01
        public async Task<IActionResult> CheckResetPermissionAndUserAvailability([FromBody] ResetPasswordDto userData)
        {
            // manager information
            var userType = User.Claims.FirstOrDefault(x => x.Type.Equals("UserType", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userRole = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRole", StringComparison.InvariantCultureIgnoreCase)).Value;
            var userCompanyId = User.Claims.FirstOrDefault(x => x.Type.Equals("CompanyId", StringComparison.InvariantCultureIgnoreCase)).Value;

            try
            {
                var user = await _repository.User.GetUserByUserName(userData.UserName);

                if (user != null)
                {
                    if (userRole == "Manager")
                    {
                        if (userType == "HelpDesk")
                        {
                            if (user.UserType == "HelpDesk" && user.CompanyId == userCompanyId)
                            {
                                var com =  await _repository.Company.GetCompanyById(new Guid(user.CompanyId));
                                var _user = _mapper.Map<UserDto>(user);
                                _user.CompanyName = com.CompanyName;
                                return Ok(_user);
                            }
                            else if (user.UserType == "Client" && user.UserRole == "Manager")
                            {

                                var com = await _repository.Company.GetCompanyById(new Guid(user.CompanyId));
                                var _user = _mapper.Map<UserDto>(user);
                                _user.CompanyName = com.CompanyName;
                                return Ok(_user);
                            }
                            else
                            {
                                return StatusCode(401, "401 Unauthorized  Access");
                            }
                        }
                        else if (userType == "Client" && user.CompanyId == userCompanyId)
                        {
                            var com = await _repository.Company.GetCompanyById(new Guid(user.CompanyId));
                            var _user = _mapper.Map<UserDto>(user);
                            _user.CompanyName = com.CompanyName;
                            return Ok(_user);
                        }
                        else
                        {
                            return StatusCode(401, "401 Unauthorized  Access");
                        }
                    }
                    else
                    {
                        return StatusCode(401, "401 Unauthorized  Access");
                    }
                }
                else
                {
                    return StatusCode(404, "404 User Not Found");
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "404 User Not Found");
            }
        }

       [HttpPut]
        [Route("[controller]/newPassword")]
        [Authorize]

        // if reset phase 01 pass then manager can reset password form this function
        public async Task<IActionResult> ResetPassword([FromBody] newPassword updatedData)
        {

            try
            {
                var updatedUser = await _repository.User.GetUserByUserName(updatedData.UserName);

                var data = Encoding.ASCII.GetBytes(updatedData.NewPassword);
                var sha1 = new SHA1CryptoServiceProvider();
                var hashed = sha1.ComputeHash(data);

                updatedUser.PasswordHash = System.Text.Encoding.UTF8.GetString(hashed);

                _repository.User.ResetPassword(updatedUser);
                await _repository.Save();

                return StatusCode(201, "201 Updated");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something Went wrong");
            }

        } 


    }
}
