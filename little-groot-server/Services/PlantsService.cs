using AutoMapper;
using LittleGrootServer.Data;
using LittleGrootServer.Dto;
using LittleGrootServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleGrootServer.Services {
    public interface IPlantsService {
        Task<IEnumerable<PlantDto>> GetPlants();
        Task<PlantDto> GetPlant(long id);
        Task<PlantDto> AddPlant(PlantDto plant);
        Task<PlantDto> UpdatePlant(PlantDto plant);
        Task<PlantDto> DeletePlant(long id);
    }

    public class PlantsService : IPlantsService {

        private LittleGrootDbContext _dbContext = null;
        private IMapper _mapper;

        public PlantsService(LittleGrootDbContext context, IMapper mapper) {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlantDto>> GetPlants() {
            var plants = await _dbContext.Plants.ToListAsync();

            return _mapper.Map<IEnumerable<PlantDto>>(plants);
        }

        public async Task<PlantDto> GetPlant(long id) {
            var plant = await _dbContext.Plants.FindAsync(id);

            return _mapper.Map<PlantDto>(plant);
        }

        public async Task<PlantDto> AddPlant(PlantDto plantDto) {
            var plant = _mapper.Map<Plant>(plantDto);
            _dbContext.Plants.Add(plant);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PlantDto>(plant);
        }

        public async Task<PlantDto> UpdatePlant(PlantDto plantDto) {
            var plant = _mapper.Map<Plant>(plantDto);
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
            return _mapper.Map<PlantDto>(plant);
        }

        public async Task<PlantDto> DeletePlant(long id) {
            var plant = await _dbContext.Plants.FindAsync(id);
            if (plant != null) {
                _dbContext.Plants.Remove(plant);
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<PlantDto>(plant);
        }

        private Task<bool> PlantExists(long id) {
            return _dbContext.Plants.AnyAsync(plant => plant.Id == id);
        }
    }
}