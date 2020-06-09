using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using testwebapicore.HubConfig;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserRepo _db;
        ClientRepo _clientRepo;
        public AuthController(UserRepo db, ClientRepo clientRepo)
        {
            _db = db;
            _clientRepo = clientRepo;
        }


        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Client user)
        {

            Client client = _db.FindClient(user.Mobile, user.Password);
            if (client == null)
            {
                // return BadRequest("Invalid client request");
                User userr = _db.FindUser(user.Mobile, user.Password);
                if (userr == null)
                {
                    return BadRequest("Invalid client request");
                }
                else
                {
                    string rolename = userr.Role.Role1;
                    int UserId = userr.Id;
                    var claims = new[]
               {    new Claim("UserId",UserId.ToString()),
                new Claim("role",rolename),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:50856",
                        audience: "http://localhost:50856",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }


            }



            else
            {
                ChartHub _connectedHub = new ChartHub();
                var ConnectionID = _connectedHub.GetConnectionID();
                string rolename = client.Category.Name;
                int UserId = client.Id;
                _clientRepo.AddClientConnection(client.Id, ConnectionID);
                var claims = new[]
           {    new Claim("UserId",UserId.ToString()),
                new Claim("role",rolename),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:50856",
                    audience: "http://localhost:50856",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            //else
            //{
            //    return Unauthorized();
            //}
        }

        //loginEmployee
        // Old
        [HttpPost, Route("loginUser")]
        public IActionResult LoginUser(string username, string password)
        {

            User user = _db.FindUser(username, password);
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }



            else
            {
                string rolename = user.Role.Role1;
                int id = user.Id;
                var claims = new[]
           {    new Claim("UserId",id.ToString()),
                new Claim("role",rolename),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:50856",
                    audience: "http://localhost:50856",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            //else
            //{
            //    return Unauthorized();
            //}

        }
    }
}