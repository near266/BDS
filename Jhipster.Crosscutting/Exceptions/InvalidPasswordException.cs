using Jhipster.Crosscutting.Constants;

namespace Jhipster.Crosscutting.Exceptions
{
    public class InvalidPasswordException : BaseException
    {
        public InvalidPasswordException() : base(ErrorConstants.InvalidPasswordType, "Mật khẩu hiện tại không chính xác")
        {
            //Status = StatusCodes.Status400BadRequest
        }
    }
    public class PasswordException : BaseException
    {
        public PasswordException() : base(ErrorConstants.InvalidPasswordType, "Mật khẩu mới phải khác mật khẩu cũ")
        {
            //Status = StatusCodes.Status400BadRequest
        }
    }

}
