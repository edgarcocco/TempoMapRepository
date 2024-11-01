using TempoMapRepository.Models.Identity;

namespace TempoMapRepository.Models.Domain
{
    public record Map(int Id,
        User? User,
        ICollection<MapDataset> Datasets,
        string Name,
        string Description,
        byte[] CoverImage,
        string Filename,
        DateTime CreatedAt,
        DateTime? UpdatedAt)
    {
        public Map() : this(0, null, new List<MapDataset>(), "", "", Array.Empty<byte>(), "", DateTime.Now, null)
        {
        }
    }
}
