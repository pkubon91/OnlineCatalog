namespace OnlineCatalog.Common.Validations
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
