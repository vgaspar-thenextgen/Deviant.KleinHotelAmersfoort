using System;

namespace Deviant.KleinHotelAmersfoort.Services.Models
{
    public class ReactionView
    {
        /// <summary>
        /// The Id of this reaction.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the person that placed the reaction.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The given reaction.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The date the reaction was placed on.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// (Optional) The score that was given to the hotel.
        /// If set, the minimum is 1 star and the maximum is 5 stars.
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// (Optional) The email of the person that placed this reaction.
        /// Is only visible by admins.
        /// </summary>
        public string Email { get; set; }
    }
}
