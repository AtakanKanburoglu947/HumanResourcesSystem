﻿using HumanResourcesSystemCore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface ITokenRepository
    {
        string Generate(IEnumerable<Claim> claims);
        bool Validate(string token);
        AccountDto Decode();
    }
}
