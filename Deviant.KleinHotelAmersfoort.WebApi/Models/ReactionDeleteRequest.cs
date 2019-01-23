using System;
using System.ComponentModel.DataAnnotations;

namespace Deviant.KleinHotelAmersfoort.WebApi.Models
{
    public class ReactionDeleteRequest : AuthenticatedRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
