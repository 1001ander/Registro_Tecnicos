using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo Se Permiten Letras ")]
    [Required(ErrorMessage = "Campo Nombres Obligatorio")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Campo Direccion Obligatorio")]
    public string? Direccion { get; set; }


    [Required(ErrorMessage = "Campo Fecha Obligatorio")]
    public DateTime FechaIngreso { get; set; }

    [StringLength(11, ErrorMessage = "El RNC No Puede Tener Mas De 11 Numeros")]
    [RegularExpression(@"[0-9]+$", ErrorMessage = "Solo Se Permiten Numeros")]
    [Required(ErrorMessage = "Campo RNC Obligatorio")]
    public int Rnc { get; set; }

    [RegularExpression(@"[0-9]+$", ErrorMessage = "Solo Se Permiten Numeros")]
    [Required(ErrorMessage = "Campo Limite De Credito Obligatorio")]
    public double LimiteCredito { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [ForeignKey("TecnicosId")]

    public int TecnicosId { get; set; }


    ///Instancia
    public Tecnicos? Tecnicos { get; set; }
}
