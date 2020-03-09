using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTest.Api.V1.Models.Recomendations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

#pragma warning disable 1998

namespace BackendTest.Api.V1.Controllers.Viewers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/viewers/documentaries")]
    public class ViewersDocumentariesController : ControllerBase
    {
        [HttpGet("all-time")]
        [SwaggerOperation(Summary = "Gets All Time Documentaries", Tags = new[] { "Viewers" })]
        [ProducesResponseType(typeof(IEnumerable<DocumentaryRecomendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTime([FromQuery, BindRequired] List<string> topics)
        {
            return NotFound("Under construction...");
        }
    }
}