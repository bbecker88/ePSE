using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace RubricOn.Logic
{
    public enum ValidationOption { 
                IsNotEmpty = 1, 
                IsNotNumber = 2, 
                IsNumber = 4, 
                IsNotZero = 8, 
                IsDate = 16, 
                IsBoolean = 32, 
                IsMail = 64, 
                IsNull = 128, 
                IsNotNull = 256,
                IsEmpty = 512
    }

    public class ValidationLogic
    {
        private ModelStateDictionary ModelState;

        public ValidationLogic(ModelStateDictionary ModelState)
        {
            this.ModelState = ModelState;
        }

        public ValidationLogic()
        {
            this.ModelState = null;
        }

        public bool Valid(object parameter, String key, params ValidationOption?[] Options)
        {
            if (Options == null || Options.Count() == 0)
                return true;

            var valid = false; //si OR, entonces false

            foreach (var option in Options)
            {
                var result = Validate(parameter, option);

                if (result == true)
                    return true;
            }

            if (!valid && ModelState != null && key != null && key != String.Empty)
                ModelState.AddModelError(key, "Error" );

            return valid;
        }

        public bool Valid(object parameter, params ValidationOption?[] Options)
        {
            if (Options == null || Options.Count() == 0)
                return true;

            var valid = false; //si OR, entonces false

            foreach (var option in Options)
            {
                var result = Validate(parameter, option);

                if (result == true)
                    return true;
            }

            if (!valid && ModelState != null)
                ModelState.AddModelError("", "Error");

            return valid;
        }

        private bool Validate(object parameter, ValidationOption? Option)
        {
            if (!Option.HasValue)
                return true;

            if (parameter == null)
            {
                if ((Option.Value & ValidationOption.IsNull) != 0)
                    return true;
                return false;
            }

            var internParameter = parameter.ToString();

            if ((Option.Value & ValidationOption.IsNotNull) != 0)
            {
                if (parameter == null)
                    return false;
            }

            if ((Option.Value & ValidationOption.IsNull) != 0)
            {
                if (internParameter!= null)
                    return false;
            }

            if ((Option.Value & ValidationOption.IsNotEmpty) != 0)
            {
                if (internParameter.Trim().Length == 0)
                    return false;
            }

            if ((Option.Value & ValidationOption.IsEmpty) != 0)
            {
                if (internParameter.Trim().Length != 0)
                    return false;
            }

            if ((Option.Value & ValidationOption.IsNotNumber) != 0)
            {
                var number = 0;

                if (Int32.TryParse(internParameter, out number))
                    return false;
            }

            if ((Option.Value & ValidationOption.IsNumber) != 0)
            {
                var number = 0;

                if (!Int32.TryParse(internParameter, out number))
                    return false;
            }

            if ((Option.Value & ValidationOption.IsNotZero) != 0)
            {
                var number = -1;

                if (Int32.TryParse(internParameter, out number))
                {
                    if (number == 0)
                        return false;
                }
            }

            if ((Option.Value & ValidationOption.IsDate) != 0)
            {
                var date = DateTime.Today;

                if (!DateTime.TryParse(internParameter, out date))
                    return false;
            }

            if ((Option.Value & ValidationOption.IsBoolean) != 0)
            {
                var boolean = true;

                if (!Boolean.TryParse(internParameter, out boolean))
                    return false;
            }

            if ((Option.Value & ValidationOption.IsMail) != 0)
            {
                var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(internParameter))
                    return false;
            }

            return true;
        }
    }
}