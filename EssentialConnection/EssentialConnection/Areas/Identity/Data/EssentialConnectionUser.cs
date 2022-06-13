using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EssentialConnection.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EssentialConnectionUser class
public class EssentialConnectionUser : IdentityUser
{
    public string NomeCompleto { get; set; }

    public TipoUsuario Tipo { get; set; }

    public enum TipoUsuario
    {
        Aluno,
        Empresa,
        Curso
    }
}

