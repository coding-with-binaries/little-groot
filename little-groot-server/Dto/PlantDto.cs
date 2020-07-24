using System.ComponentModel.DataAnnotations;

namespace LittleGrootServer.Dto {
    public class PlantDto {
        public long Id { get; set; }

        [Required]
        public string Family { get; set; }

        [Required]
        public string Type { get; set; }

        public string Description { get; set; }
    }
}