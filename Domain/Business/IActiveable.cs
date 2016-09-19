namespace Business
{
    public interface IActiveable
    {
        void SetActive(bool isActive);

        void SetDeleted(bool isDeleted);

        bool IsActive { get; }

        bool IsDeleted { get; }
    }
}
