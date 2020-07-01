﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Services.Identity.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool IsAdmin => User.IsInRole("admin");

        protected Guid UserId => 
            string.IsNullOrWhiteSpace(User?.Identity?.Name) ? 
            Guid.Empty :
            Guid.Parse(User.Identity.Name);
    }
}
