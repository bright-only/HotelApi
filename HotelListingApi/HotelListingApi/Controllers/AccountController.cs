using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Models;
using HotelListingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListingApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IAuthManager _authManager;
    private readonly UserManager<ApiUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;

    public AccountController(IAuthManager authmanager,UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper)
    {
      _authManager = authmanager;
      _userManager = userManager;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    {
      _logger.LogInformation($"REGISTRATION ATTEMPT FOR {userDTO.Email}");
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var user = _mapper.Map<ApiUser>(userDTO);
        user.UserName=userDTO.Email;
        var result = await _userManager.CreateAsync(user, userDTO.Password);
        if(!result.Succeeded)
        {
          foreach(var error in result.Errors)
          {
            ModelState.AddModelError(error.Code, error.Description);
          }
          return BadRequest(ModelState);
        }
        await _userManager.AddToRolesAsync(user, userDTO.Roles);
        return Accepted();
      }
      catch(Exception ex)
      {
        _logger.LogError(ex, $"SOMETHING WENT WRONG IN THE {nameof(Register)}");
        return Problem($"SOMETHING WENT WRONG IN THE {nameof(Register)}", statusCode:500);
      }
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
    {
      _logger.LogInformation($"LOGIN ATTEMPT FOR {userDTO.Email}");
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        if(!await _authManager.ValidateUser(userDTO))
        {
          return Unauthorized();
        }
        return Accepted(new { Token = await _authManager.CreateToken() });
      }
      catch(Exception ex)
      {
        _logger.LogError(ex, $"SOMETHING WENT WRONG IN THE {nameof(Login)}");
        return Problem($"SOMETHING WENT WRONG IN THE {nameof(Login)}", statusCode: 500);
      }
    }
  }
}
