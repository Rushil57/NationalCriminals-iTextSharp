using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalCriminals.Helpers
{
    public class ValidationRegEx
    {/// <summary>
        /// RegEx for username.
        /// </summary>
        public const string Username = @"^((?=.{8,30}$)[a-zA-Z](?![_.])(?!.*[_.]{2})[a-zA-Z0-9]+[._]*[a-zA-Z0-9]+[._]*[a-zA-Z0-9]+|[A-Za-z0-9_\+-]+(\.[A-Za-z0-9_\+-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.([A-Za-z]{2,4}))$";
    }
}