using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL;
using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services.Models;

namespace Deviant.KleinHotelAmersfoort.Services
{
    public class ReactionsService : IReactionsService
    {
        private readonly IReactionsRepository _reactionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public ReactionsService(IReactionsRepository reactionsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _reactionsRepository = reactionsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public IEnumerable<ReactionView> List(string token, int count, SortType sort, OrderType order)
        {
            var queryResults = _reactionsRepository.List(count, sort, order);
            var mappingResults = _mapper.Map<List<ReactionView>>(queryResults);

            var user = _usersRepository.GetByToken(token);

            if (!IsAdmin(user))
            {
                mappingResults
                    .ForEach(r => r.Email = null);
            }

            return mappingResults;
        }

        public void Save(string token, Reaction reaction)
        {
            var user = _usersRepository.GetByToken(token);

            if (user == null)
            {
                throw new UnauthorizedException("User does not have rights to preform this operation.");
            }

            reaction.Name = user.Name;

            _reactionsRepository.Save(reaction);
        }

        public void Delete(string token, Guid id)
        {
            var user = _usersRepository.GetByToken(token);

            if (!IsAdmin(user))
            {
                throw new UnauthorizedException("User does not have rights to preform this operation.");
            }

            _reactionsRepository.Delete(id);
        }

        private bool IsAdmin(User user)
        {
            return user != null &&
                user.Rights.Contains(Right.AllowRemoveReactions);
        }
    }
}
