using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Common.Authentication
{
    public class AdminAuth : JwtAuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}
