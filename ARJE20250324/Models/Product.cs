using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARJE20250324.AppWebMVC.Models;

public partial class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El Nombre deL Producto es obligatorio")]
    [Display(Name = "Nombre Producto")]
    public string ProductName { get; set; } = null!;
    [Display(Name = "Descripcion")]
    public string? Description { get; set; }
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener solo numeros")]
    [Required(ErrorMessage = "El Precio es obligatorio")]
    public decimal Price { get; set; }
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio de compra debe tener solo numeros")]
    [Required(ErrorMessage = "El Precio de compra es obligatorio")]
    [Display(Name = "Precio")]
    public decimal PurchasePrice { get; set; }
    [Display(Name = "Bodega")]
    public int? WarehouseId { get; set; }
    [Display(Name = "Marca")]
    public int? BrandId { get; set; }
    [Display(Name = "Marca")]
    public virtual Brand? Brand { get; set; }
    [Display(Name = "Bodega")]
    public virtual Warehouse? Warehouse { get; set; }
}
