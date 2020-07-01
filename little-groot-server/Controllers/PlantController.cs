using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Data;
using LittleGrootServer.Models;

namespace LittleGrootServer.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase {
        private LittleGrootDbContext _dbContext = null;

        public PlantsController(LittleGrootDbContext context) {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants() {
            return await _dbContext.Plants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id) {
            var plant = await _dbContext.Plants.FindAsync(id);

            if (plant == null) {
                return NotFound();
            }

            return plant;
        }

        [HttpPost]
        public async Task<ActionResult<Plant>> AddPlant(Plant plant) {
            _dbContext.Plants.Add(plant);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Plant>> DeletePlant(int id) {
            var plant = await _dbContext.Plants.FindAsync(id);
            if (plant == null) {
                return NotFound();
            }

            _dbContext.Plants.Remove(plant);
            await _dbContext.SaveChangesAsync();

            return plant;
        }
    }
}