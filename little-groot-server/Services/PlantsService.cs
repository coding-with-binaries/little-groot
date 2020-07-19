using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Data;
using LittleGrootServer.Models;

namespace LittleGrootServer.Services {
    public interface IPlantsService {
        Task<IEnumerable<Plant>> GetPlants();
        Task<Plant> GetPlant(long id);
        Task<Plant> AddPlant(Plant plant);
        Task<Plant> UpdatePlant(Plant plant);
        Task<Plant> DeletePlant(long id);
    }

    public class PlantsService : IPlantsService {

        private LittleGrootDbContext _dbContext = null;

        public PlantsService(LittleGrootDbContext context) {
            this._dbContext = context;
        }

        public async Task<IEnumerable<Plant>> GetPlants() {
            return await _dbContext.Plants.ToListAsync();
        }

        public async Task<Plant> GetPlant(long id) {
            return await _dbContext.Plants.FindAsync(id);
        }

        public async Task<Plant> AddPlant(Plant plant) {
            _dbContext.Plants.Add(plant);
            await _dbContext.SaveChangesAsync();

            return plant;
        }

        public async Task<Plant> UpdatePlant(Plant plant) {
            _dbContext.Entry(plant).State = EntityState.Modified;
            try {
                await _dbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                var plantExists = await PlantExists(plant.Id);
                if (!plantExists) {
                    return null;
                } else {
                    throw;
                }
            }
            return plant;
        }

        public async Task<Plant> DeletePlant(long id) {
            var plant = await _dbContext.Plants.FindAsync(id);
            if (plant != null) {
                _dbContext.Plants.Remove(plant);
                await _dbContext.SaveChangesAsync();
            }

            return plant;
        }

        private Task<bool> PlantExists(long id) {
            return _dbContext.Plants.AnyAsync(plant => plant.Id == id);
        }
    }
}