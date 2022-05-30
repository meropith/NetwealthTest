using Exchange.API.Helpers;
using Exchange.API.Mediator.Convert.Queries;
using Exchange.API.Models;
using Exchange.API.Models.DTOs;
using Exchange.API.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exchange.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ConvertController : Controller
    {
        private readonly IMediator _mediator;
        public ConvertController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("Convert")]
        public async Task<IActionResult> Convert(ConvertRequest Model)
        {
            try
            {
                var User = (HttpContext.Items["User"] as UserDTO);

                if (User == null)
                {
                    return NotFound(new ApiErrorResponse(HttpStatusCode.NotFound, $"User not found"));
                }
                var query = new ConvertCurrencyQuery
                {
                    FromISO = Model.FromISO,
                    ToISO = Model.ToISO,
                    Provider = (User.Role == "FREE") ? "Fixer" : Model.Provider,
                    Amount = Model.Amount,
                    UserTier = User.Role
                };

                var result = await _mediator.Send(query);
                return result != null ? Ok(result) : BadRequest(new ApiErrorResponse(HttpStatusCode.InternalServerError, $"There was an error during the conversion"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(HttpStatusCode.InternalServerError, $"{ex.Message}"));
            }

        }

        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var query = new GetCurrencyQuery();
                var result = await _mediator.Send(query);
                return result != null ? Ok(result) : BadRequest(new ApiErrorResponse(HttpStatusCode.InternalServerError, $"Currencies list could not be retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(HttpStatusCode.BadRequest, $"{ex.Message}"));
            }
        }
    }
}
