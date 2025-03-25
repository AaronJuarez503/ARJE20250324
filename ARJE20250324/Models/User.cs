using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ARJE20250324.AppWebMVC.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    [StringLength(40, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 50 caracteres.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Notes { get; set; }

    [NotMapped]
    [StringLength(40, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 50 caracteres.")]
    [Display(Name = "Confirmar Contraseña")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string? ConfirmarPassword { get; set; } = null!;
}
