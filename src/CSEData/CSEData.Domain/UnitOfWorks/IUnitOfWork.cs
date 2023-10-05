namespace CSEData.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
