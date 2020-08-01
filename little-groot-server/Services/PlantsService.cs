using AutoMapper;
using LittleGrootServer.Data;
using LittleGrootServer.Dto;
using LittleGrootServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LittleGrootServer.Services {
    public interface IPlantsService {
        IEnumerable<PlantDto> GetPlants();
        PlantDto GetPlant(long id);
        PlantDto AddPlant(PlantDto plant);
        PlantDto UpdatePlant(PlantDto plant);
        PlantDto DeletePlant(long id);
    }

    public class PlantsService : IPlantsService {

        private LittleGrootDbContext _dbContext = null;
        private IMapper _mapper;

        public PlantsService(LittleGrootDbContext context, IMapper mapper) {
            _dbContext = context;
            _mapper = mapper;
        }

        public IEnumerable<PlantDto> GetPlants() {
            var plants = _dbContext.Plants.ToList();

            return _mapper.Map<IEnumerable<PlantDto>>(plants);
        }

        public PlantDto GetPlant(long id) {
            var plant = _dbContext.Plants.Find(id);

            return _mapper.Map<PlantDto>(plant);
        }

        public PlantDto AddPlant(PlantDto plantDto) {
            var plant = _mapper.Map<Plant>(plantDto);
            _dbContext.Plants.Add(plant);
            _dbContext.SaveChanges();

            return _mapper.Map<PlantDto>(plant);
        }

        public PlantDto UpdatePlant(PlantDto plantDto) {
            var plant = _mapper.Map<Plant>(plantDto);
            _dbContext.Entry(plant).State = EntityState.Modified;
            try {
                _dbContext.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                var plantExists = PlantExists(plant.Id);
                if (!plantExists) {
                    return null;
                } else {
                    throw;
                }
            }
            return _mapper.Map<PlantDto>(plant);
        }

        public PlantDto DeletePlant(long id) {
            var plant = _dbContext.Plants.Find(id);
            if (plant != null) {
                _dbContext.Plants.Remove(plant);
                _dbContext.SaveChanges();
            }

            return _mapper.Map<PlantDto>(plant);
        }

        private bool PlantExists(long id) {
            return _dbContext.Plants.Any(plant => plant.Id == id);
        }
    }
}