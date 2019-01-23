using System.ComponentModel.DataAnnotations;

namespace Deviant.KleinHotelAmersfoort.WebApi.Models
{
    public class ReactionSaveRequest : AuthenticatedRequest
    {
        /// <summary>
        /// The given reaction.
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// (Optional) The score that was given to the hotel.
        /// If set, the minimum is 1 star and the maximum is 5 stars.
        /// </summary>
        [Range(1, 5)]
        public int? Score { get; set; }

        /// <summary>
        /// (Optional) The email of the person that placed this reaction.
        /// Is only visible by admins.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
