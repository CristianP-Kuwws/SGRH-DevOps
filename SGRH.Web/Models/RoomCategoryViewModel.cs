using System.ComponentModel.DataAnnotations;

namespace SGRH.Web.Models
{
    public class RoomCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La descripcion es requerida.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripcion debe tener entre 5 y 500 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Capacidad maxima")]
        [Range(1, 50, ErrorMessage = "La capacidad maxima debe estar entre 1 y 50.")]
        public int MaxCapacity { get; set; }

        [Display(Name = "Amenidades")]
        [StringLength(1000, ErrorMessage = "Las amenidades no pueden exceder 1000 caracteres.")]
        public string Amenities { get; set; } = string.Empty;
    }
}
