using System.Collections.Generic;
using AutoMapper;
using Deviant.KleinHotelAmersfoort.DAL.Models;
using Deviant.KleinHotelAmersfoort.Services;
using Deviant.KleinHotelAmersfoort.Services.Models;
using Deviant.KleinHotelAmersfoort.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Deviant.KleinHotelAmersfoort.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IReactionsService _reactionsService;
        private readonly IMapper _mapper;

        public ReactionsController(IReactionsService reactionsService, IMapper mapper)
        {
            _reactionsService = reactionsService;
            _mapper = mapper;
        }

        [HttpGet("{count=5}/{sort=date}/{order=desc}")]
        public ActionResult<IEnumerable<ReactionView>> Get([FromHeader]string token, int count, SortType sort, OrderType order)
        {
            var response = _reactionsService.List(token, count, sort, order);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReactionSaveRequest request)
        {
            var reaction = _mapper.Map<Reaction>(request);

            try
            {
                _reactionsService.Save(request.Token, reaction);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }

            return Ok("Reaction saved!");
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] ReactionDeleteRequest request)
        {
            try
            {
                _reactionsService.Delete(request.Token, request.Id);
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }

            return Ok("Reaction deleted!");
        }
    }
}