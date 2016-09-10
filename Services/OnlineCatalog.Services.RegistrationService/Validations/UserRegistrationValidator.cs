using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.Extensions;
using OnlineCatalog.Common.Validations;

namespace OnlineCatalog.Services.RegistrationService.Validations
{
    public class UserRegistrationValidator : IValidator<UserDto>
    {
        public bool Validate(UserDto entity)
        {
            if(entity == null) return false;
            if (entity.Login.IsNullOrEmpty()) return false;
            if (entity.Password.IsNullOrEmpty()) return false;
            if (entity.Address == null) return false;
            if (entity.Address.Email.IsNullOrEmpty()) return false;
            return true;
        }
    }
}