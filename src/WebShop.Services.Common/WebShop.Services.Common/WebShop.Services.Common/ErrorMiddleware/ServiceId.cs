using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.ErrorMiddleware
{
    public class ServiceId : IServiceId
    {
        private static readonly string UniqueId = $"{Guid.NewGuid():N}";

        public string Id => UniqueId;
    }
}
