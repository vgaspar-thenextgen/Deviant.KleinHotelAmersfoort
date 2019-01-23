using Deviant.KleinHotelAmersfoort.DAL.Models;

namespace Deviant.KleinHotelAmersfoort.DAL
{
    public interface IUsersRepository
    {
        User GetByToken(string token);
    }
}
