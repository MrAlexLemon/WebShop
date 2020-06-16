using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public virtual string Code { get; }

        protected DomainException(string message) : base(message)
        {
        }
    }
}
