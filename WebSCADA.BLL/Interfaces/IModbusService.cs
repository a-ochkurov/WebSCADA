using System.Collections.Generic;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Interfaces
{
    public interface IModbusService
    {
        List<PLCDataDomain> GetData(List<PLCDataDomain> pLCDatas);

        PLCDataDomain SetData(PLCDataDomain pLCData);
    }
}
