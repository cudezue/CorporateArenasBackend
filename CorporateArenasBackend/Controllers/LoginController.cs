﻿using System.Threading.Tasks;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class LoginController : ApiController
    {
        private static readonly object LoginErrorMessage = new {ErrorMessage = "Invalid Email/Password"};
        private readonly UserRepository _repository;
        private readonly UserManager<User> _userManager;

        public LoginController(UserManager<User> userManager, UserRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return Unauthorized(LoginErrorMessage);

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
                return Unauthorized(LoginErrorMessage);

            return Ok(new LoginResponseModel
            {
                Token = _repository.GenerateJWTToken(user),
                User = user
            });
        }
    }
}