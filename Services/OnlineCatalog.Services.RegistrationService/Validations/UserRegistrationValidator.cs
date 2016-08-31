using System;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.Extensions;
using OnlineCatalog.Common.Validations;

namespace OnlineCatalog.Services.RegistrationService.Validations
{
    public class UserRegistrationValidator : IValidator<UserDto>
    {
        public void Validate(UserDto entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity), "User cannot be null");
            if(entity.Login.IsNullOrEmpty()) throw new ArgumentNullException(nameof(entity.Login), "Login cannot be null or empty");
            if(entity.Password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(entity.Password), "Password cannot be null or empty");
            if(entity.Address == null) throw new ArgumentNullException(nameof(entity.Address), "Address cannot be null");
        }
    }
}