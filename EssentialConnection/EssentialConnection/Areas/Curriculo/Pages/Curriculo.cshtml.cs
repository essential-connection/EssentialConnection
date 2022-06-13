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

namespace EssentialConnection.Areas.Curriculo.Pages
{
    public class CurriculoModel : PageModel
    {
        private readonly Context _context;
        public InputModel Input { get; set; }
        public CurriculoModel (Context context)
        {
            _context = context;
        }
        public class InputModel
        {
            [Required]
            [Display(Name = "Descrição do curriculo")]
            public string? DescricaoCurriculo { get; set; }
            
            [Required]
            [Display(Name = "Compentencias")]
            public string? Compentencias { get; set; }
            
            [Required]
            [Display(Name = "Tipo de formação")]
            public string? TipoFormacao { get; set; }
            
            [Required]
            [Display(Name = "Nome da formação")]
            public string? Nome { get; set; }
            
            [Required]
            [Display(Name = "Descrição formação")]
            public string? Descricao { get; set; }
            
            [Required]
            [Display(Name = "Data de Inicio")]
            public DateTime? DataInicio { get; set; }
            
            [Required]
            [Display(Name = "Data que finalizou")]
            public DateTime? DataFim{ get; set; }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ItensCurriculo currilo = new ItensCurriculo();

                currilo.Nome = Input.Nome;
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public IActionResult CriarDescricao()
        {
            return Page();
        }

    }
}
