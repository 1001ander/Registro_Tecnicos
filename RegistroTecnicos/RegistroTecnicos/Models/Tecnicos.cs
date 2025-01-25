using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicosId { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "solo se permiten letras ")]
    [Required(ErrorMessage = "Campo nombres obligatorios")]

    public string? Nombres { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "ingrese un valor mayor a 0")]
    [Required(ErrorMessage = "Campo sueldo obligatorio")]

    public double SueldoHora { get; set; }

  
}
