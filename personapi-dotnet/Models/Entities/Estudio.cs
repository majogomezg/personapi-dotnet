using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace personapi_dotnet.Models.Entities
{
    public partial class Estudio
    {
        [Required(ErrorMessage = "El Id de la Profesión es obligatorio.")]
        public int IdProf { get; set; }

        [Required(ErrorMessage = "El Cc de la Persona es obligatorio.")]
        public int CcPer { get; set; }

        public DateOnly? Fecha { get; set; }

        [StringLength(50, ErrorMessage = "El nombre de la universidad no puede tener más de 50 caracteres.")]
        public string? Univer { get; set; }

        [ValidateNever]
        public virtual Persona CcPerNavigation { get; set; } = null!;

        [ValidateNever]
        public virtual Profesion IdProfNavigation { get; set; } = null!;
    }
}
