namespace Business.DataAccess
{
    public interface IRepository<in T>
    {
        void AddToDatabase(T entity);
    }
}
