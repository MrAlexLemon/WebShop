﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Services.Identity.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        public IActionResult Get() => Ok("WebShop Identity Service");
    }
}
