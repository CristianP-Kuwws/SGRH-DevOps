using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRHDevOps.Core.Application.Dtos.Hotel.Floor_and_CategoryRoom;
using SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory;

namespace SGRH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomCategoriesController : ControllerBase
    {
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly ILogger<RoomCategoriesController> _logger;

        public RoomCategoriesController(IRoomCategoryService roomCategoryService, ILogger<RoomCategoriesController> logger)
        {
            _roomCategoryService = roomCategoryService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoomCategoryDto>>> GetAllRoomCategories()
        {
            _logger.LogInformation("Getting all room categories");

            var result = await _roomCategoryService.GetAllListAsync();

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Failed to retrieve room categories: {result.Message}");
                return BadRequest(new { message = result.Message });
            }

            _logger.LogInformation($"Retrieved {result.Data.Count} room categories");
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomCategoryDto>> GetRoomCategoryById(int id)
        {
            _logger.LogInformation($"Getting room category with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning($"Invalid room category ID: {id}");
                return BadRequest(new { message = "Room category ID must be greater than 0" });
            }

            var result = await _roomCategoryService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Room category with ID {id} not found");
                return NotFound(new { message = result.Message });
            }

            _logger.LogInformation($"Room category with ID {id} retrieved");
            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomCategoryDto>> CreateRoomCategory([FromBody] RoomCategoryDto roomCategoryDto)
        {
            _logger.LogInformation("Creating new room category");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for room category creation");
                return BadRequest(ModelState);
            }

            var result = await _roomCategoryService.AddAsync(roomCategoryDto);

            if (!result.IsSuccess)
            {
                _logger.LogError($"Failed to create room category: {result.Message}");
                return BadRequest(new { message = result.Message });
            }

            _logger.LogInformation($"Room category created with ID: {result.Data.CategoryId}");
            return CreatedAtAction(nameof(GetRoomCategoryById), new { id = result.Data.CategoryId }, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoomCategoryDto>> UpdateRoomCategory(int id, [FromBody] RoomCategoryDto roomCategoryDto)
        {
            _logger.LogInformation($"Updating room category with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning($"Invalid room category ID: {id}");
                return BadRequest(new { message = "Room category ID must be greater than 0" });
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for room category update");
                return BadRequest(ModelState);
            }

            var result = await _roomCategoryService.UpdateAsync(id, roomCategoryDto);

            if (!result.IsSuccess)
            {
                _logger.LogError($"Failed to update room category: {result.Message}");
                return BadRequest(new { message = result.Message });
            }

            _logger.LogInformation($"Room category with ID {id} updated");
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRoomCategory(int id)
        {
            _logger.LogInformation($"Deleting room category with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning($"Invalid room category ID: {id}");
                return BadRequest(new { message = "Room category ID must be greater than 0" });
            }

            var result = await _roomCategoryService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogError($"Failed to delete room category: {result.Message}");
                return BadRequest(new { message = result.Message });
            }

            _logger.LogInformation($"Room category with ID {id} deleted");
            return NoContent();
        }
    }
}
