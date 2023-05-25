// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System.Text.RegularExpressions;

namespace Jhipster.Crosscutting.Utilities
{
    public class CheckUtil
    {

    }

    public class PhoneNumberAttribute
    {
        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{2,7})$";
        public static bool IsValid(string value)
        {
            if (value != null) return Regex.IsMatch(value, motif);
            else return false;
        }
    }
}
