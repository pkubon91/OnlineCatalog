using System;
using Business.Administration;
using Business.DataAccess.Administration;
using DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.General;
using OnlineCatalog.Common.Validations;
using OnlineCatalog.Services.RegistrationService.MailServiceClient;

namespace OnlineCatalog.Services.RegistrationService
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _validator;
        private readonly IMailService _mailClient;

        public RegisterService(IUserRepository userRepository, IValidator<UserDto> validator, IMailService mailClient)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            if (mailClient == null) throw new ArgumentNullException(nameof(mailClient));

            _userRepository = userRepository;
            _validator = validator;
            _mailClient = mailClient;
        }

        public ServiceActionResult RegisterUser(UserDto userForRegistration)
        {
            if (!_validator.Validate(userForRegistration)) return new ServiceActionResult(ActionStatus.NotSuccessfull, "User is not filled correctly");
            try
            {
                User user = _userRepository.GetUserByLogin(userForRegistration.Login);
                if (user != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "User with defined login already exist");
                _userRepository.AddToDatabase(userForRegistration.Map());
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }

            try
            {
                var messageContent = _mailClient.BuildMessage(BuildMailContext(userForRegistration));
                _mailClient.SendMail(messageContent);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        private static MailContext BuildMailContext(UserDto userForRegistration)
        {
            return new MailContext
            {
                EmailAddress = userForRegistration.Address.Email,
                EmailContent = new MailContent(MailMessageType.Registration),
                EmailTitle = "Online catalog registration",
                SenderAddress = "wks@gmail.com"
            };
        }
    }
}
