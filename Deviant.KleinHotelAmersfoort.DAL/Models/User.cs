using System.Collections.Generic;

namespace Deviant.KleinHotelAmersfoort.DAL.Models
{
    public class User
    {
        public User()
        {
            Rights = new List<Right>();
        }

        public string Token { get; set; }

        public string Name { get; set; }

        public IEnumerable<Right> Rights { get; set; }
    }
}
