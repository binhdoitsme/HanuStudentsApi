using HanuEdmsApi.EF;
using HanuEdmsApi.Helpers;
using HanuEdmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;

namespace HanuEdmsApi.Controllers
{
    [Route("/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly HanuEdmsContext Context;

        public SessionController(HanuEdmsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public bool CheckJwtValidity([FromQuery] string authToken)
        {
            string username = JwtUtils.ValidateJWT(authToken);
            return Context.UserAccount.Any(ua => ua.Username == username);
        }

        [HttpPost]
        public IActionResult StartSession([FromBody] JsonElement body)
        {
            string username = body.GetProperty("username").GetString();
            string password = body.GetProperty("password").GetString();

            UserAccount ua = Context.UserAccount
                                .Where(ua => ua.Username == username 
                                    && ua.Password == PasswordHasher.SHA256Hashing(password))
                                .FirstOrDefault();
            if (ua == null)
            {
                return Unauthorized();
            }
            else
            {
                return new JsonResult(JwtUtils.GenerateJWT(username));
            }
        }

        [HttpDelete]
        public IActionResult TerminateSession(string authToken)
        {
            if (CheckJwtValidity(authToken))
            {
                JwtUtils.InvalidateJWT(authToken);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}