using Microsoft.AspNetCore.Mvc;
using SGRHDevOps.Core.Application.Dtos.Hotel;
using SGRHDevOps.Core.Application.Interfaces.Hotel.Floor_and_RoomCategory;
using SGRHDevOps.Core.Application.Services.Hotel.Floor_and_RoomCategory;

[ApiController]
[Route("api/[controller]")]
public class FloorController : ControllerBase
{
    private readonly IFloorService _floorService;
    private readonly ILogger<FloorController> _logger;

    public FloorController(IFloorService floorService, ILogger<FloorController> logger)
    {
        _floorService = floorService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<FloorDto>>> GetAllFloors()
    {
        _logger.LogInformation("Getting all floors");

        var result = await _floorService.GetAllListAsync();

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Failed to retrieve floors: {result.Message}");
            return BadRequest(new { message = result.Message });
        }

        _logger.LogInformation($"Retrieved {result.Data.Count} floors");
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FloorDto>> GetFloorById(int id)
    {
        _logger.LogInformation($"Getting floor with ID: {id}");

        if (id <= 0)
        {
            _logger.LogWarning($"Invalid floor ID: {id}");
            return BadRequest(new { message = "Floor ID must be greater than 0" });
        }

        var result = await _floorService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Floor with ID {id} not found");
            return NotFound(new { message = result.Message });
        }

        _logger.LogInformation($"Floor with ID {id} retrieved");
        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FloorDto>> CreateFloor([FromBody] FloorDto floorDto)
    {
        _logger.LogInformation("Creating new floor");

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for floor creation");
            return BadRequest(ModelState);
        }

        var result = await _floorService.AddAsync(floorDto);

        if (!result.IsSuccess)
        {
            _logger.LogError($"Failed to create floor: {result.Message}");
            return BadRequest(new { message = result.Message });
        }

        _logger.LogInformation($"Floor created with ID: {result.Data.FloorId}");
        return CreatedAtAction(nameof(GetFloorById), new { id = result.Data.FloorId }, result.Data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FloorDto>> UpdateFloor(int id, [FromBody] FloorDto floorDto)
    {
        _logger.LogInformation($"Updating floor with ID: {id}");

        if (id <= 0)
        {
            _logger.LogWarning($"Invalid floor ID: {id}");
            return BadRequest(new { message = "Floor ID must be greater than 0" });
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for floor update");
            return BadRequest(ModelState);
        }

        var result = await _floorService.UpdateAsync(id, floorDto);

        if (!result.IsSuccess)
        {
            _logger.LogError($"Failed to update floor: {result.Message}");
            return BadRequest(new { message = result.Message });
        }

        _logger.LogInformation($"Floor with ID {id} updated");
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFloor(int id)
    {
        _logger.LogInformation($"Deleting floor with ID: {id}");

        if (id <= 0)
        {
            _logger.LogWarning($"Invalid floor ID: {id}");
            return BadRequest(new { message = "Floor ID must be greater than 0" });
        }

        var result = await _floorService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            _logger.LogError($"Failed to delete floor: {result.Message}");
            return BadRequest(new { message = result.Message });
        }

        _logger.LogInformation($"Floor with ID {id} deleted");
        return NoContent();
    }
}