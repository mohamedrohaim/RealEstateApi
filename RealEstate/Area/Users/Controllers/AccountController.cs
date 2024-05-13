using BusinessLayer.Iservices;
using DataAccessLayer.DTOs.Acccount.Password;
using DataAccessLayer.DTOs.Acccount;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using DataAccessLayer;

namespace RealEstate.Area.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(
        IUserService userService

        )
        {
            _userService = userService;

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result is ErrorDTO error)
                {
                    error.Message = error.Message;
                    return BadRequest(error);
                }
                else if (result is RegisterResponseDTO response)
                {
                    response.Message = response.Message;
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, new { message ="Internal Server Error" });
                }

            }

            return UnprocessableEntity(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result is LoginErrorResponseDTO error)
                {

                    error.EmailError = error.EmailError == null ? "" : error.EmailError;
                    error.PasswordError = error.PasswordError == null ? "" : error.PasswordError;
                    return BadRequest(error);
                }
                else if (result is LoginResponseDTO response)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }
            return UnprocessableEntity(ModelState);
        }


        [AllowAnonymous]
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO model)
        {
            if (ModelState.IsValid)
                if (ModelState.IsValid)
                {
                    var result = await _userService.ConfirmEmailAsync(model);
                    if (result is ErrorDTO error)
                    {
                        error.Message = error.Message;
                        return BadRequest(error);
                    }
                    else if (result is LoginResponseDTO response)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(500, new { message = "Internal Server Error" });
                    }

                }

            return UnprocessableEntity(ModelState);
        }


        [AllowAnonymous]
        [HttpPost("sendConfirmEmailToken")]
        public async Task<IActionResult> sendConfirmEmailToken([FromBody] string email)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SendConfirmEmailTokenAsync(email);

                if (result is ErrorDTO error)
                {
                    return BadRequest(error);
                }
                else if (result is RegisterResponseDTO response)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result is SuccessResponseDTO success)
                {
                    success.Message = success.Message;
                    return Ok(success);
                }
                else if (result is ErrorDTO error)
                {
                    error.Message = error.Message;
                    return BadRequest(error);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }

            return UnprocessableEntity(ModelState);

        }

        [HttpPost("sendOtpRestPassword")]
        public async Task<IActionResult> SendOtpForForgetPasswordReset([FromBody] SendOtpForForgetPasswordResetDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SendOtpForForgetPasswordResetAsync(model);

                if (result is SuccessResponseDTO success)
                {
                    success.Message = success.Message;
                    return Ok(success);
                }
                else if (result is ErrorDTO error)
                {
                    error.Message = error.Message;
                    return BadRequest(error);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }

            return UnprocessableEntity(ModelState);

        }

        [HttpPost("verifyOtpForgetPassword")]
        public async Task<IActionResult> VerifyOtpAndEmail([FromBody] VerifyOtpDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.VerifyOtpAndEmailAsync(model);

                if (result is VerifyOtpResponseDTO response)
                {
                    response.Message = response.Message;
                    return Ok(response);
                }
                else if (result is ErrorDTO error)
                {
                    error.Message = error.Message;
                    return BadRequest(error);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("resetForgetPassword")]
        public async Task<IActionResult> ResetForgetPasswordWithOtp([FromBody] ForgetPasswordResetDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetForgetPasswordWithOtpAsync(model);

                if (result is SuccessResponseDTO success)
                {
                    success.Message = success.Message;
                    return Ok(success);
                }
                else if (result is ErrorDTO error)
                {
                    error.Message =error.Message;
                    return BadRequest(error);
                }
                else
                {
                    return StatusCode(500, new { message = "Internal Server Error"});
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

    }
}
