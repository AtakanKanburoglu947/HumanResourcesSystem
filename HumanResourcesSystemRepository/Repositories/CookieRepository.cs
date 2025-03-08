using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService.Services
{
    public class CookieRepository : ICookieRepository
    {
        public string Get(HttpRequest request, string name)
        {
            return request.Cookies[name];
        }

        public void Remove(HttpResponse response, string name)
        {
            response.Cookies.Delete(name);
        }

        public void Set(HttpResponse response,string name, string value)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            response.Cookies.Append(name, value, cookieOptions);
        }
    }
}
