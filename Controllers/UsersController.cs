using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Text.Json;

namespace Server.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : Controller
        {

            [HttpGet("GetUsersAdminList")]
            public List<Object> GetListForAdmin()
            {
                User user = new User();
                return user.GetUsersAList();
            }

        [HttpPost("Register")]
            public int Register([FromBody] User newUser)
            {
                return newUser.Register();
            }


            [HttpPost("Login")]
            public IActionResult Login([FromBody] User userToLogin)
            {
            try
            {
                User user = new User();
                User isValidUser = user.isValidUser(userToLogin.Email, userToLogin.Password);
                return Ok(isValidUser);
            }
            catch(Exception ex)
            {
                if (ex.Message == "User not active.")
                {
                    return BadRequest(new { message = "User not active." });
                }

                return NotFound(new { message = "User not found." });
            }

        }

        [HttpPut("UpdateIsActive")]
        public int updateIsActive([FromBody] User user)
        {
            User newUser = new User();
            return newUser.updateUserActive(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
            public User Put(int id, [FromBody] User user)
            {
                User newUser = new User(id, user.Name,user.Email,user.Password,user.IsActive);
                return newUser.updateUserDet(newUser);
            }
        }
    }

