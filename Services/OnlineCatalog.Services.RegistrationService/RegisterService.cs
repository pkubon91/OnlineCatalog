using System;
using Business.Administration;
using Business.DataAccess.Administration;
using DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Mappings;
using OnlineCatalog.Common.Validations;
//using OnlineCatalog.Services.RegistrationService.MailClient;

namespace OnlineCatalog.Services.RegistrationService
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _validator;
//        private readonly IMailService _mailService;

        public RegisterService(IUserRepository userRepository, IValidator<UserDto> validator)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (validator == null) throw new ArgumentNullException(nameof(validator));
//            if (mailService == null) throw new ArgumentNullException(nameof(mailService));

            _userRepository = userRepository;
            _validator = validator;
//            _mailService = mailService;
        }

        public void RegisterUser(UserDto userForRegistration)
        {
            _validator.Validate(userForRegistration);
            User user = _userRepository.GetUserByLogin(userForRegistration.Login);
            if (user != null) throw new ArgumentException("User with this login already exist in db", nameof(userForRegistration));

            _userRepository.AddToDatabase(userForRegistration.Map());
        }
    }
}
