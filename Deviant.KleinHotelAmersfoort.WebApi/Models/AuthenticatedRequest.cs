using System.ComponentModel.DataAnnotations;

namespace Deviant.KleinHotelAmersfoort.WebApi.Models
{
    public abstract class AuthenticatedRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
