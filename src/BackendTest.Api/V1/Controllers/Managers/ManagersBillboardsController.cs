using System.Threading.Tasks;
using BackendTest.Api.V1.Models.BillBoards;
using BackendTest.Api.V1.Models.Pagination;
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
            return NotFound("Under construction...");
        }
    }
}