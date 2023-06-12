using Jhipster.Crosscutting.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Crosscutting.Exceptions
{
    public class PhoneNumberAlreadyUsedException : BadRequestAlertException
    {
        public PhoneNumberAlreadyUsedException() : base(ErrorConstants.PhoneNumberAlreadyUsedType, "Số điện thoại đã tồn tại trên hệ thống",
            "userManagement", "phonenumberexists")
        {
        }
    }
}
