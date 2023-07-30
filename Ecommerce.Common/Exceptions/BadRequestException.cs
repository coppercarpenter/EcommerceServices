﻿namespace Ecommerce.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        #region Constructor

        public BadRequestException(string message) : base($"{message}")
        {
        }

        #endregion Constructor
    }
}