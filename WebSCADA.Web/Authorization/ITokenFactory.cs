using System.Collections.Generic;
using System.Security.Claims;

namespace WebSCADA.Web.Authorization
{
    public interface ITokenFactory
    {
        string Create(IList<Claim> claims);
    }
}
