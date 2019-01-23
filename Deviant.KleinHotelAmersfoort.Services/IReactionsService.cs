using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services.Models;
using System;
using System.Collections.Generic;

namespace Deviant.KleinHotelAmersfoort.Services
{
    public interface IReactionsService
    {
        IEnumerable<ReactionView> List(string token, int count, SortType sort, OrderType order);

        void Save(string token, Reaction reaction);

        void Delete(string token, Guid id);
    }
}
