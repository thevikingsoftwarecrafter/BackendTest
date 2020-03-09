using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTest.Api.V1.Models.Recomendations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

#pragma warning disable 1998

namespace BackendTest.Api.V1.Controllers.Managers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/managers/movies")]
    public class ManagersDocumentariesController : ControllerBase
    {
        [HttpGet("upcoming")]
        [SwaggerOperation(Summary = "Gets Upcoming Movies", Tags = new[] { "Managers" })]
        [ProducesResponseType(typeof(IEnumerable<MovieRecomendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTime([FromQuery, BindRequired] int periodOfTimeInDays, [FromQuery, BindRequired] string ageRate, [FromQuery] List<string> genres)
        {
            return NotFound("Under construction...");
        }
    }
}