using AutoMapper;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.DataTransferObjects;
using HelpDesk.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class UserController : Controller
    {
        IRepositoryWrapper _repository;
        IMapper _mapper;
        public UserController(IRepositoryWrapper repository , IMapper mapper )
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("[controller]/Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {

            // convert incoming DTO to user model
            var userEntity = _mapper.Map<UserModel>(user);

            // saving create user and save user into DB
            _repository.User.CreateUser(userEntity);
            await _repository.Save();

            // Create Token
            if (user.TokenAvailable == null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.CompanyId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Json(token);
            }
            else if (user.TokenAvailable != null)
            {
                return Json("User Registration successful");
            }

            return Json("Your Email has Alrady Exist");
        }

        [HttpPost]
        [Route("[controller]/Login")]
        public IActionResult Login([FromBody] UserLoginDto model) //not working yet
        {
            //user existing chechk code here ( not dev yet )


            if (1 == 1) // demo condition ** will change
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // save to token in DB

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Json(token);
            }
            return Json("faild To logIn");
        }


    }
}
