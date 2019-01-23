using Deviant.KleinHotelAmersfoort.DAL.Models;
using System;
using System.Collections.Generic;

namespace Deviant.KleinHotelAmersfoort.DAL
{
    public interface IReactionsRepository
    {
        IEnumerable<Reaction> List(int count, SortType sort, OrderType order);

        void Save(Reaction reaction);

        void Delete(Guid id);
    }
}
