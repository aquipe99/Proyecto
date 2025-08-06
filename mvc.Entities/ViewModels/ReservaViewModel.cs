using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.ViewModels
{
    public class ReservaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string? NombreCliente { get; set; }
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 dígitos numéricos.")]
        public string? DniCliente { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan? HoraInicio { get; set; }
        
        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan? HoraFin { get; set; }
     
        [Required(ErrorMessage = "Debe seleccionar un método de pago.")]
        public int? MetodoPagoId { get; set; }

        public string? TipoPago { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una cancha.")]
        public int? CanchaId { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto total debe ser mayor a cero.")]
        public decimal? MontoTotal { get; set; }
        public decimal? MontoPagado { get; set; }

        [RegularExpression(@"^9\d{8}$", ErrorMessage = "El número de teléfono debe tener 9 dígitos y comenzar con 9.")]
        public string? Telefono { get; set; }

        public ObservableCollection<Cancha> Canchas { get; set; } = new();
        public ObservableCollection<MetodoPago> MetodoPagos { get; set; } = new();

        public Boolean Estado { get; set; }
        public string HoraInicioStr => HoraInicio.HasValue ? HoraInicio.Value.ToString(@"hh\:mm") : "";
        public string HoraFinStr => HoraFin.HasValue ? HoraFin.Value.ToString(@"hh\:mm") : "";
    }
}
