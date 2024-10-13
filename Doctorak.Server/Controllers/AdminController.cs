﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctorak.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterAdmin request)
    {
        var response = await _adminService
            .AdminRegister(
                new Admin
                {
                    Username = request.Username
                },
                request.Password
            );

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] AdminLogin request)
    {
        var response = await _adminService.AdminLogin(request.Username, request.Password);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpGet("fetch-users"), Authorize]
    public async Task<ActionResult> GetUsers()
    {
        var response = await _adminService.GetUsers();

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpDelete("delete-user/{userId:int}"), Authorize]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var response = await _adminService.DeleteUser(userId);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

}