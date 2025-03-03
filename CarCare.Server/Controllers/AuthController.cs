using CarCare.Application.Authentication;
using CarCare.Domain.DTO;
using CarCare.Shared.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCare.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    #region Proprieties

    AuthApplication _authApplication;

    #endregion

    #region Constructor
    
    public AuthController(AuthApplication authApplication)
    {
        _authApplication = authApplication;
    }
    
    #endregion

    #region Endpoints
    
    [HttpPost(nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<UserAuthInfo>> Login([FromBody] UserAuth userAuth)
    {
        try
        {
            return Ok(await _authApplication.Login(
                email: userAuth.Email, 
                username: userAuth.Username, 
                password: userAuth.Password).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return BadRequest(new UserAuthInfo { Message = ex.Message });
        }
    }
    
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<bool>> Register([FromBody] UserRegisterDto userRegisterInfo)
    {
        try
        {
            return Ok(await _authApplication.Register(userRegisterInfo).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    #endregion
}