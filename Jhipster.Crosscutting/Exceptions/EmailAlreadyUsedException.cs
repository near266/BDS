using Jhipster.Crosscutting.Constants;

namespace Jhipster.Crosscutting.Exceptions
{
    public class EmailAlreadyUsedException : BadRequestAlertException
    {
        public EmailAlreadyUsedException() : base(ErrorConstants.EmailAlreadyUsedType, "Email đã tồn tại trên hệ thống",
            "userManagement", "emailexists")
        {
        }
    }
}
