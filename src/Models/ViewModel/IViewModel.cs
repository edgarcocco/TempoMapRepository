namespace TempoMapRepository.Models.ViewModel
{
    public interface IViewModel<T> where T : class
    {
        public T Model { get; }
    }
}
