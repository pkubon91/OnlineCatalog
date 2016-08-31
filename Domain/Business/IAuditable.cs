using System;

namespace Business
{
    public interface IAuditable
    {
        DateTime Created { get; }

        DateTime Updated { get; }

        void SetCreatedDate(DateTime dateTime);
        void SetUpdatedDate(DateTime dateTime);
    }
}
