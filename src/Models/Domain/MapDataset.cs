namespace TempoMapRepository.Models.Domain
{
    public record MapDataset(int Id,
        Map Map,
        int Tempo,
        string Data
        )
    {
        public MapDataset() : this(0, new Map(), 0, "")
        {
        }
    }
}
