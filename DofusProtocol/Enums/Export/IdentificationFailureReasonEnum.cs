

// Generated on 12/20/2015 17:38:53
using System;
using System.Collections.Generic;

namespace Stump.DofusProtocol.Enums
{
    public enum IdentificationFailureReasonEnum
    {
        BAD_VERSION = 1,
        WRONG_CREDENTIALS = 2,
        BANNED = 3,
        KICKED = 4,
        IN_MAINTENANCE = 5,
        TOO_MANY_ON_IP = 6,
        TIME_OUT = 7,
        BAD_IPRANGE = 8,
        CREDENTIALS_RESET = 9,
        EMAIL_UNVALIDATED = 10,
        OTP_TIMEOUT = 11,
        SERVICE_UNAVAILABLE = 53,
        EXTERNAL_ACCOUNT_LINK_REFUSED = 61,
        EXTERNAL_ACCOUNT_ALREADY_LINKED = 62,
        UNKNOWN_AUTH_ERROR = 99,
        SPARE = 100,
    }
}