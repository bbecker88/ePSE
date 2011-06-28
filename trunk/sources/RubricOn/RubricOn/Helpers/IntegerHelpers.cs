using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace RubricOn.Helpers
{
    public static class IntegerHelpers
    {
        public static Int32 ToInteger(this String s)
        {
            Int32 integerValue = 0;
            if (s != null)
                Int32.TryParse(s, out integerValue);
            return integerValue;
        }

        public static Boolean IsInteger(this String s)
        {
            if (s == null)
                return false;

            Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
            return regularExpression.Match(s).Success;
        }

        public static Int32 ToInteger(this object s)
        {            
            return s.ToString().ToInteger();
        }

        public static Boolean IsInteger(this object s)
        {
            return s.ToString().IsInteger();
        }
    }
}