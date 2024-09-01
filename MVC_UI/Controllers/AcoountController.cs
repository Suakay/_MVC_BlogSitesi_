using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVC_Business.DTOs.AuthorDTOs;
using MVC_Business.Services.AuthourServices;
using MVC_Business.Services.MailServices;
using MVC_Domain.Enums;
using MVC_UI.Models.AccountVMs;
using System.Security.Claims;

namespace MVC_UI.Controllers
{
    [AllowAnonymous]
    public class AcoountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService  _mailService;
        private readonly IAuthourService _authorService;
        public AcoountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser>signInManager,IMailService mailService,IAuthourService authourService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _authorService =authourService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MyAccount()
        {
            var userEmail=User.FindFirstValue(ClaimTypes .Email);   
            var user=await _userManager.FindByNameAsync(userEmail); 
            var authorId=await _authorService.GetAuthorIdByEmail(userEmail);
            var userRole = await _userManager.GetRolesAsync(user);
            var result=await _authorService.GetByIdAsync(authorId);
            if(!result.IsSuccess)
            {
                return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });

            }
            return RedirectToAction("Index","Home",new { Ara = userRole[0].ToString()});
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, "Password.1");
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            if (result.Succeeded)
            {
                try
                {
                    var codeToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackURL = Url.Action("ConfirmEmail", "Home", new { userId = user.Id, code = codeToken }, protocol: Request.Scheme);
                    var mailMessage = $"Please confirm your acoount by <a href='{callbackURL}'>clicking here!</a>";
                    await _mailService.SendMailAsync(model.Email, "Confirm your email", mailMessage);
                    await _authorService.AddAsync(model.Adapt<AthorCreateDTO>());
                    return RedirectToAction("Index");
                }
                catch (Exception )
                {
                    return View("Error");

                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>ConfirmEmail(string  userId, string code)
        {
            if(userId==null|| code == null)
            {
                return RedirectToAction("Index");
            }
            var user=await _userManager.FindByIdAsync(userId);
            if (user==null)
            {
                return NotFound("Invalid user");
            }
            var result=await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "Index" : "Error");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVMs model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {

                return View(model);
            }

            var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!checkPassword.Succeeded)
            {

                return View(model);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        }

        // Logs out the user from the system
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }

}

