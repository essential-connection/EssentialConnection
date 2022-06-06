// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using EssentialConnection.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using static EssentialConnection.Areas.Identity.Data.EssentialConnectionUser;
using EssentialConnection.Controllers;
using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EssentialConnection.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EssentialConnectionUser> _signInManager;
        private readonly UserManager<EssentialConnectionUser> _userManager;
        private readonly IUserStore<EssentialConnectionUser> _userStore;
        private readonly IUserEmailStore<EssentialConnectionUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Context _context;

        public RegisterModel(
            UserManager<EssentialConnectionUser> userManager,
            IUserStore<EssentialConnectionUser> userStore,
            SignInManager<EssentialConnectionUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            Context context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(255, ErrorMessage = "O nome deve ter tamanho máximo de 255 caracteres")]
            [Display(Name = "Nome completo")]
            public string NomeCompleto { get; set; }

            [Required]
            [Display(Name = "Tipo")]
            public TipoUsuario Tipo { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirme a senha")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "Digite o telefone no formato(xx) 9xxxx-xxxx")]
            [Display(Name = "Telefone")]
            public string Telefone { get; set; }

            [Required]
            [Display(Name = "Curso")]
            public int CursoId { get; set; }

            [Display(Name = "CNPJ")]
            public string CNPJ { get; set; }

            [StringLength(255, ErrorMessage = "A descrição deve ter tamanho máximo de 255 caracteres")]
            [Display(Name = "Descrição da empresa.")]
            public string Descricao { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["CursoId"] = new SelectList(_context.Curso.OrderBy(c => c.Nome), "CursoID", "Nome");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.NomeCompleto = Input.NomeCompleto;
                user.Tipo = Input.Tipo;
                var telefone = Input.Telefone;
                var cursoId = Input.CursoId;
                var cnpj = Input.CNPJ;
                var descricaoEmpresa = Input.Descricao;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        if (user.Tipo == TipoUsuario.Aluno)
                        {
                            AlunosController aluno = new AlunosController(_context);
                            await aluno.Create(user.NomeCompleto, user.Email, telefone, cursoId, userId);
                        }
                        else if (user.Tipo == TipoUsuario.Professor)
                        {
                            CursosController curso = new CursosController(_context);
                            await curso.Create(userId,user.Email,user.NomeCompleto, telefone);
                        }
                        else if (user.Tipo == TipoUsuario.Empresa)
                        {
                            EmpresasController empresa = new EmpresasController(_context);
                            await empresa.Create(userId, user.Email, user.NomeCompleto, telefone, cnpj, descricaoEmpresa);
                        }
                    }
                    returnUrl = Url.Content("~/PaginaInicial");
                    // If we got this far, something failed, redisplay form
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            returnUrl= Url.Content("~/Identity/Account/Register");
            // If we got this far, something failed, redisplay form
            return LocalRedirect(returnUrl);
        }

        private EssentialConnectionUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<EssentialConnectionUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(EssentialConnectionUser)}'. " +
                    $"Ensure that '{nameof(EssentialConnectionUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<EssentialConnectionUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<EssentialConnectionUser>)_userStore;
        }
    }
}
