using System.Collections.Generic;
using WebSCADA.Domain.Models;

namespace WebSCADA.DAL.Interfaces
{
    public interface IModbusTCPService
    {
        List<PLCDataDomain> GetData(List<PLCDataDomain> listAddresses);

        void SetData(PLCDataDomain pLCData);

    }
}
