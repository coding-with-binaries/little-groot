using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Services;
using LittleGrootServer.Models;

namespace LittleGrootServer.Controllers {
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase {
        private IPlantsService _plantsService = null;

        public PlantsController(IPlantsService plantsService) {
            this._plantsService = plantsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants() {
            var plants = await _plantsService.GetPlants();
            return Ok(plants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(long id) {
            var plant = await _plantsService.GetPlant(id);

            if (plant == null) {
                return NotFound();
            }

            return Ok(plant);
        }

        [HttpPost]
        public async Task<ActionResult<Plant>> AddPlant(Plant plant) {
            plant = await _plantsService.AddPlant(plant);
            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }

        [HttpPut]
        public async Task<ActionResult<Plant>> UpdatePlant(Plant plant) {
            try {
                plant = await _plantsService.UpdatePlant(plant);
                if (plant == null) {
                    return NotFound();
                }
                return Ok(plant);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Plant>> DeletePlant(long id) {
            var plant = await _plantsService.DeletePlant(id);
            if (plant == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}