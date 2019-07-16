using System;
using System.Collections.Generic;
using System.Text;

namespace Rice.SDK.Exceptions.Api
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
            
        }
    }
}
