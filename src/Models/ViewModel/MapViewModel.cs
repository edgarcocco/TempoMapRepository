using TempoMapRepository.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace TempoMapRepository.Models.ViewModel
{
    public class MapViewModel : IViewModel<Map>
    {
        private Map _model;
        public Map Model { get => _model; }
        public int Id { get => _model.Id; }
        public string Name { get => _model.Name; } 
        public string Description { get => _model.Description; }
        public DateTime CreatedAt { get => _model.CreatedAt; }
        public string CoverImage { get; }

        public MapViewModel(Map map)
        {
            _model = map;
            CoverImage = Convert.ToBase64String(map.CoverImage);
        }
    }

}
