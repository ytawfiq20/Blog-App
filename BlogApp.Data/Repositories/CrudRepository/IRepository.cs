namespace BlogApp.Data.Repositories.BlogRepository
{
    public interface IRepository <T>
    {
        T Add(T item);
        T Edit(T item);
        T GetById(Guid id);
        T Delete(Guid id);
        IEnumerable<T> GetAll();
    }
}
