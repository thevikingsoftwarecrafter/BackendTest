using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTest.Api.V1.Models.Pagination;
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
    [Route("api/viewers/movies")]
    public class ViewersMoviesController : ControllerBase
    {
        [HttpGet("all-time")]
        [SwaggerOperation(Summary = "Gets All Time Movies", Tags = new[] { "Viewers" })]
        [ProducesResponseType(typeof(PagedList<MovieRecomendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTime([FromQuery] List<string> keywords, [FromQuery] List<string> genres, [FromQuery] PageParameters pageParameters)
        {
            return NotFound("Under construction...");
        }

        [HttpGet("upcoming")]
        [SwaggerOperation(Summary = "Gets Upcoming Movies", Tags = new[] { "Viewers" })]
        [ProducesResponseType(typeof(PagedList<MovieRecomendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUpcoming([FromQuery, BindRequired] int periodOfTimeInDays, [FromQuery] List<string> keywords, [FromQuery] List<string> genres, [FromQuery] PageParameters pageParameters)
        {
            return NotFound("Under construction...");
        }
    }
}