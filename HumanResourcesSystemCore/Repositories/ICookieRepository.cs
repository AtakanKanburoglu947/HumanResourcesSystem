using HumanResourcesSystemCore.AuthModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface ICookieRepository
    {
        string Get(HttpRequest request, string name);
        void Set(HttpResponse response, string name, string value);
        void Remove(HttpResponse response, string name);
    }
}
