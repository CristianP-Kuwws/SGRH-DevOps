using System.ComponentModel.DataAnnotations;

namespace SGRH.Web.Models
{
    public class FloorViewModel
    {
        public int FloorId { get; set; }

        [Display(Name = "Numero de piso")]
        [Range(1, int.MaxValue, ErrorMessage = "El numero de piso debe ser mayor que 0.")]
        public int FloorNumber { get; set; }

        [Display(Name = "Descripcion")]
        [StringLength(500, ErrorMessage = "La descripcion no puede exceder 500 caracteres.")]
        public string? Description { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El estado es requerido.")]
        public string Status { get; set; } = "active";
    }
}
