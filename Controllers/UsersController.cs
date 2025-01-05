﻿using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            // GET: api/<UsersController>
            [HttpGet]
            //public IEnumerable<User> Get()
            //{
            //    User user = new User();
            //    return user.Read();
            //}

            // GET api/<UsersController>/5
            [HttpGet("{id}")]
            public string Get(int id)
            {
                return "value";
            }

            [HttpPost("Register")]
            public int Register([FromBody] User newUser)
            {
                return newUser.Register();
            }


            [HttpPost("Login")]
            public User Login([FromBody] User userToLogin)
            {
                User user = new User();
                return user.isValidUser(userToLogin.Email, userToLogin.Password);
            }

            //// POST api/<UsersController>
            //[HttpPost]
            //public bool Post([FromBody] User user)
            //{
            //    return user.Insert();
            //}

            // PUT api/<UsersController>/5
            [HttpPut("{id}")]
            public User Put(int id, [FromBody] User user)
            {
                User newUser = new User(id, user.Name,user.Email,user.Password);
                return newUser.updateUserDet(newUser);
            }

            //// DELETE api/<UsersController>/5
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //}
        }
    }

