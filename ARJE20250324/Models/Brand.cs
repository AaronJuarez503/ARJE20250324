using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARJE20250324.AppWebMVC.Models;

public partial class Brand
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El Nombre de la Marca es obligatorio")]
    [Display(Name = "Marca")]
    public string BrandName { get; set; } = null!;
    [Required(ErrorMessage = "El Pais de la Marca es obligatorio")]
    [Display(Name = "Pais")]
    public string? Country { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
