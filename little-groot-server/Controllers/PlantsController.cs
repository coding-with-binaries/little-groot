using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Services;
using LittleGrootServer.Dto;

namespace LittleGrootServer.Controllers {
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PlantsController : ControllerBase {
        private IPlantsService _plantsService = null;

        public PlantsController(IPlantsService plantsService) {
            _plantsService = plantsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDto>>> GetPlants() {
            var plants = await _plantsService.GetPlants();
            return Ok(plants);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDto>> GetPlant(long id) {
            var plant = await _plantsService.GetPlant(id);

            if (plant == null) {
                return NotFound();
            }

            return Ok(plant);
        }

        [HttpPost]
        public async Task<ActionResult<PlantDto>> AddPlant(PlantDto plantDto) {
            plantDto = await _plantsService.AddPlant(plantDto);
            return CreatedAtAction("GetPlant", new { id = plantDto.Id }, plantDto);
        }

        [HttpPut]
        public async Task<ActionResult<PlantDto>> UpdatePlant(PlantDto plantDto) {
            try {
                plantDto = await _plantsService.UpdatePlant(plantDto);
                if (plantDto == null) {
                    return NotFound();
                }
                return Ok(plantDto);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PlantDto>> DeletePlant(long id) {
            var plant = await _plantsService.DeletePlant(id);
            if (plant == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}