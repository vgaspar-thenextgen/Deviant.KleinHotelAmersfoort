using System.Collections.Generic;
using System.Linq;
using Deviant.KleinHotelAmersfoort.DAL.Models;

namespace Deviant.KleinHotelAmersfoort.DAL
{
    public class UsersListRepository : IUsersRepository
    {
        private readonly IList<User> _usersDb;

        public UsersListRepository()
        {
            _usersDb = new List<User>
            {
                new User
                {
                    Name = "Admin",
                    Token = "KFA71KER8SS",
                    Rights = new List<Right> { Right.AllowRemoveReactions }
                },
                new User
                {
                    Name = "Dayenne Visser",
                    Token = "CNY85SYA7DM",
                },
                new User
                {
                    Name = "Randolf de Kruif",
                    Token = "VEJ55OIR4TR"
                },
                new User
                {
                    Name = "Rolina Sabel",
                    Token = "YIJ12YAQ5WC"
                },
                new User
                {
                    Name = "Pim van Herwijnen",
                    Token = "QGV26RZW8OL"
                },
                new User
                {
                    Name = "Pim van Herwijnen",
                    Token = "KVU84DGK9RK"
                }
            };
        }

        public User GetByToken(string token)
        {
            return _usersDb.SingleOrDefault(u => u.Token == token);
        }
    }
}
