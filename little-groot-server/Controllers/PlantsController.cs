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
        public ActionResult<IEnumerable<PlantDto>> GetPlants() {
            var plants = _plantsService.GetPlants();
            return Ok(plants);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<PlantDto> GetPlant(long id) {
            var plant = _plantsService.GetPlant(id);

            if (plant == null) {
                return NotFound();
            }

            return Ok(plant);
        }

        [HttpPost]
        public ActionResult<PlantDto> AddPlant(PlantDto plantDto) {
            plantDto = _plantsService.AddPlant(plantDto);
            return CreatedAtAction("GetPlant", new { id = plantDto.Id }, plantDto);
        }

        [HttpPut]
        public ActionResult<PlantDto> UpdatePlant(PlantDto plantDto) {
            try {
                plantDto = _plantsService.UpdatePlant(plantDto);
                if (plantDto == null) {
                    return NotFound();
                }
                return Ok(plantDto);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<PlantDto> DeletePlant(long id) {
            var plant = _plantsService.DeletePlant(id);
            if (plant == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}