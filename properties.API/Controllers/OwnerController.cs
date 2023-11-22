using Microsoft.AspNetCore.Mvc;
using properties.Application.Common;
using properties.Application.DTOs;
using properties.Application.Interfaces;

namespace properties.API.Controllers
{
    /// <summary>
    /// Owner Service
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        readonly IOwnerService _ownerService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ownerService"></param>
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Get all Owner
        /// </summary>
        /// <remarks>consult all registered owners</remarks>
        /// <returns>List owners</returns>
        /// <response code="200">Return owners</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<OwnerDTO>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<IEnumerable<OwnerDTO>>))]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ownerService.getAllAsync();
            switch(response.code)
            {
                case 200: return Ok(response);
                case 204: return NoContent();
                default : return BadRequest();
            }
        }


        /// <summary>
        /// Create new owner
        /// </summary>
        /// <remarks>Add new owner</remarks>
        /// <returns>new owner added</returns>
        /// <response code="200">Return owners</response>
        /// <response code="400">Error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OwnerDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<OwnerDTO>))]
        [HttpPost("create")]
        public async Task<IActionResult> Create(OwnerDTO ownerDTO)
        {
            var response = await _ownerService.CreateAsync(ownerDTO);
            switch (response.code)
            {
                case 200: return Ok(response);
                case 400: return BadRequest(response);
                default: return BadRequest();
            }
        }
    }
}
