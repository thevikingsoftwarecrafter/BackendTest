using System;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Api.V1.Models;
using BackendTest.Api.V1.Models.BillBoards;
using BackendTest.Api.V1.Models.Pagination;
using BackendTest.Domain.Queries.IntelligentBillboard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

#pragma warning disable 1998

namespace BackendTest.Api.V1.Controllers.Managers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/managers/billboards")]
    public class ManagersBillboardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagersBillboardsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("suggested-billboard")]
        [SwaggerOperation(Summary = "Gets Suggested Billboard", Tags = new[] { "Managers" })]
        [ProducesResponseType(typeof(PagedList<SuggestedBillboardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSuggested([FromQuery, BindRequired] int periodOfTimeInDays, [FromQuery, BindRequired] int screens, [FromQuery] bool city, [FromQuery] PageParameters pageParameters)
        {
            return NotFound("Under construction...");
        }

        [HttpGet("intelligent-billboard")]
        [SwaggerOperation(Summary = "Gets Intelligent Billboard", Tags = new[] { "Managers" })]
        [ProducesResponseType(typeof(PagedList<IntelligentBillboardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIntelligent([FromQuery, BindRequired] int periodOfTimeInDays, [FromQuery, BindRequired] int bigScreens, [FromQuery, BindRequired] int smallScreens, [FromQuery] bool city, [FromQuery] PageParameters pageParameters)
        {
            var request = new GetIntelligentBillboardRequest(periodOfTimeInDays, bigScreens, smallScreens, city);
            var response = await _mediator.Send(request);

            return response.Billboard.Match<IActionResult>(
                some: x =>
                    Ok(new PagedList<IntelligentBillboardResponse>(
                        x.Select(l => l.ToDto())
                            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                            .Take(pageParameters.PageSize).ToList(),
                        x.Count, 
                        pageParameters.PageNumber, 
                        pageParameters.PageSize)),
                none: NotFound );
        }
    }
}