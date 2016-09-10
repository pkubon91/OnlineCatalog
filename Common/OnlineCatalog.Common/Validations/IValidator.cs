namespace OnlineCatalog.Common.Validations
{
    public interface IValidator<T>
    {
        bool Validate(T entity);
    }
}
