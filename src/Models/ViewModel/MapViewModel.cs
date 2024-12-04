using TempoMapRepository.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using TempoMapRepository.Models.Identity;

namespace TempoMapRepository.Models.ViewModel
{
    public class MapViewModel() : IViewModel<Map>
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public IFormFile? CoverImage { get; set; }
        public string? Filename { get; set; }
        public string? CoverImageFormat { get; set; }
        public string? UserId { get; set; }
    }

    public class MapDisplayViewModel(Map map) : IViewModel<Map>
    {
        public int Id { get => map.Id; }
        public string Name { get => map.Name; }
        public string Description { get => map.Description; }
        public DateTime CreatedAt { get => map.CreatedAt; }
        public string CoverImage { get => Convert.ToBase64String(map.CoverImage); }
        public string CoverImageFormat { get => map.CoverImageFormat; }
        public string Filename { get => map.Filename; }
        public string Username { get => map?.User?.UserName; }
        public string UserId { get => map.User.Id; }
    }

}
