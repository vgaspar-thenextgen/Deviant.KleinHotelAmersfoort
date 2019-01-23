using System;
using System.Collections.Generic;
using System.Linq;
using Deviant.KleinHotelAmersfoort.DAL.Models;

namespace Deviant.KleinHotelAmersfoort.DAL
{
    public class ReactionsListRepository : IReactionsRepository
    {
        private readonly IList<Reaction> _reactionsDb;

        public ReactionsListRepository()
        {
            _reactionsDb = new List<Reaction>
            {
                new Reaction
                {
                    Id = Guid.NewGuid(),
                    Name = "Dayenne Visser",
                    Text = "Suspendisse id tempus erat. Donec ac diam at sem faucibus ornare vitae quis nisl.",
                    Date = DateTime.Today.AddMonths(-3),
                    Email = "DayenneVisser@teleworm.us",
                    Score = 3
                },
                new Reaction
                {
                    Id = Guid.NewGuid(),
                    Name = "Randolf de Kruif",
                    Text = "Aenean vitae condimentum magna. Aenean ligula nibh, dapibus vitae pretium at, convallis a dolor. Nunc venenatis ex quis ante feugiat, nec blandit ipsum dignissim.",
                    Date = DateTime.Today.AddDays(20),
                    Email = "RandolfdeKruif@jourrapide.com",
                    Score = 4
                },
                new Reaction
                {
                    Id = Guid.NewGuid(),
                    Name = "Rolina Sabel",
                    Text = "nteger sit amet ornare neque, ac dignissim lectus. Mauris mattis sagittis sapien, scelerisque vulputate ligula.",
                    Date = DateTime.Today.AddMonths(-2),
                    Email = "RolinaSabel@rhyta.com",
                    Score = null
                }

            };
        }

        public IEnumerable<Reaction> List(int count, SortType sort, OrderType order)
        {
            IEnumerable<Reaction> results = null;

            if (sort == SortType.Date)
            {
                results = _reactionsDb.OrderBy(r => r.Date);
            }
            else
            {
                results = _reactionsDb.OrderBy(r => r.Score);
            }

            if (order == OrderType.Desc)
            {
                results = results.Reverse();
            }

            return results.Take(count);
        }

        public void Save(Reaction reaction)
        {
            if (reaction.Id == Guid.Empty)
            {
                reaction.Id = Guid.NewGuid();
            }
            reaction.Date = DateTime.Now;

            if (_reactionsDb.Any(r => r.Id == reaction.Id))
            {
                Delete(reaction.Id);
            }

            _reactionsDb.Add(reaction);
        }

        public void Delete(Guid id)
        {
            var reaction = _reactionsDb.SingleOrDefault(r => r.Id == id);
            if (reaction != null)
            {
                _reactionsDb.Remove(reaction);
            }
        }
    }
}
