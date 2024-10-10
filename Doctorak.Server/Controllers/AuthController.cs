﻿using Doctorak.Server.DTOs;
using Doctorak.Server.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Doctorak.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody] UserRegister request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email must be provided.");
        }

        var response = await _authService
            .Register(
                new User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                },
                request.Password
            );

        if (!response.Success)
        {
            return BadRequest(response);
        }


        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody] UserLogin request)
    {
        var response = await _authService.Login(request.Email, request.Password);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail(string email, string code)
    {
        var response = await _authService.VerifyEmail(email, code);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var response = await _authService.ForgotPassword(email);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }

}
