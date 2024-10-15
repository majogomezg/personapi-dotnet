using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace personapi_dotnet.Models.Entities
{
    public partial class Telefono
    {
        [Required(ErrorMessage = "El número es obligatorio.")]
        [StringLength(15, ErrorMessage = "El número no puede tener más de 15 caracteres.")]
        public string Num { get; set; } = null!;

        [Required(ErrorMessage = "El operador es obligatorio.")]
        [StringLength(45, ErrorMessage = "El operador no puede tener más de 45 caracteres.")]
        public string Oper { get; set; } = null!;

        [Required(ErrorMessage = "El dueño es obligatorio.")]
        public int Duenio { get; set; }

        [ValidateNever]
        public virtual Persona DuenioNavigation { get; set; } = null!;
    }
}
