using AASA_Back_End.Models;
using AASA_Back_End.ViewModel.AccountViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.IO;
using System;
using AASA_Back_End.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AASA_Back_End.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            AppUser user = new AppUser 
            { 
                UserName = registerVM.UserName,
                FullName = registerVM.FullName,
                Email = registerVM.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(registerVM);
                }
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token },
                Request.Scheme, Request.Host.ToString());

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fidannm@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Verify Email";
            string body = string.Empty;

            using StreamReader reader = new StreamReader("wwwroot/templates/verify.html");
            body = reader.ReadToEnd();

            body= body.Replace("{{link}}", link);
            body = body.Replace("{{fullname}}", user.FullName);


            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("fidannm@code.edu.az", "W71Rn9h.");
            smtp.Send(email);
            smtp.Disconnect(true);

            //await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction(nameof(VrifyEmail));
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VrifyEmail()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(loginVM);
            }

            //await _signInManager.SignInAsync(user, false);

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        //[Authorize(Roles = "SuperAdmin")]
        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });

                }
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (existUser == null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();

            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = existUser.Id, token },
                Request.Scheme, Request.Host.ToString());

            string path = "wwwroot/templates/verify.html";
            string body = string.Empty;
            string subject = "Verify email";

           

            return View();
        }


        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
