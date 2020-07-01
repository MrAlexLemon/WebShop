using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Services.Identity.Application.Services
{
    public interface IPasswordService
    {
        bool IsValid(string hash, string password);
        string Hash(string password);
    }
}
