using Microsoft.AspNetCore.Mvc;
using People.Domain.Contants;
using People.Domain.DTOs.Requests;
using People.Domain.Interfaces;
using People.Domain.Interfaces.Services;
using People.Domain.Mappings.DTOEntities;
using System.Net.Mime;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TechnologyController : BaseController
    {
        private readonly IApiNotification apiNotification;
        private readonly ILogService logService;
        private readonly ITechnologyService tecnologyService;
        public TechnologyController(IApiNotification apiNotification, ILogService logService, ITechnologyService tecnologyService) : base(apiNotification, logService)
        {
            this.apiNotification = apiNotification;
            this.logService = logService;
            this.tecnologyService = tecnologyService;
        }

        [HttpGet("")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TechnologyResponseDTO), contentType: Constants.contentTypeJson)]
        public async Task<ActionResult> List()
        {
            var response = await tecnologyService.FindAll();
            return HandleResponse(response);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TechnologyResponseDTO), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<ActionResult> FindById([FromRoute] string id)
        {
            var response = await tecnologyService.FindById(id);
            return HandleResponse(response);
        }

        [HttpPost("")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TechnologyResponseDTO), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: 422)]
        public async Task<ActionResult> Create(TechnologyRequestWithoutIdDTO request)
        {
            var mapper = DTOEntityMap.MapRequestDTO();
            var dto = mapper.Map<TechnologyRequestDTO>(request);

            var response = await tecnologyService.Create(dto);
            return HandleResponse(response);
        }

        [HttpPut("")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TechnologyResponseDTO), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: 422)]
        public async Task<ActionResult> Update(TechnologyRequestDTO request)
        {
            var response = await tecnologyService.Update(request);
            return HandleResponse(response);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TechnologyResponseDTO), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ProblemDetails), contentType: Constants.contentTypeJson)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var response = await tecnologyService.Delete(id);
            return HandleResponse(response);
        }

    }
}
