﻿using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Identity_Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper,
            SignInManager<AppUser> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            return Ok(new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppUserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email has already been used" } });

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded is false)
                return BadRequest(new ApiResponse(400));

            return Ok(new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = model.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
