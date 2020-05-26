using System.Collections.Generic;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Interfaces
{
    public interface ILogsService
    {
        List<LogDomain> Get();
    }
}
