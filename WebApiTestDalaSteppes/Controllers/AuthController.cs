using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiTestDalaSteppes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly UserManager<User> _userManager;
        //private readonly IEmailSender _emailSender;

        //public AuthController(UserManager<User> userManager, IEmailSender emailSender)
        //{
        //    _userManager = userManager;
        //    _emailSender = emailSender;
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterDto model)
        //{
        //    var user = new User
        //    {
        //        UserName = model.Email,
        //        Email = model.Email,
        //        IsActive = false
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return BadRequest(result.Errors);

        //    await _userManager.AddToRoleAsync(user, "user");

        //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var confirmLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);

        //    await _emailSender.SendEmailAsync(model.Email, "Подтвердите регистрацию", $"Ссылка активации: <a href='{confirmLink}'>подтвердить</a>");

        //    return Ok("Проверьте почту для активации.");
        //}

        //[HttpGet("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return BadRequest("Пользователь не найден.");

        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    if (result.Succeeded)
        //    {
        //        user.IsActive = true;
        //        await _userManager.UpdateAsync(user);
        //        return Ok("Email подтвержден, аккаунт активирован.");
        //    }

        //    return BadRequest("Ошибка подтверждения.");
        //}
    }

}
