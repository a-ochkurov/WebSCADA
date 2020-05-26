using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebSCADA.BLL.Interfaces;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Services
{
    public class ModbusService : IModbusService
    {
        private readonly IModbusTCPService modbusTCP;
        private readonly IRepository<Log> logsRepository;

        public ModbusService(IModbusTCPService modbusTCP, IRepository<Log> logsRepository)
        {
            this.modbusTCP = modbusTCP;
            this.logsRepository = logsRepository;
        }

        public List<PLCDataDomain> GetData(List<PLCDataDomain> pLCDatas)
        {
            var dataLits = modbusTCP.GetData(pLCDatas);
            dataLits = GenerateNotificationMessage(dataLits);

            return dataLits;
        }

        public PLCDataDomain SetData(PLCDataDomain pLCData)
        {
            var log = new Log()
            {
                Date = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                Message = $"Address: {pLCData.Address}, new data: {pLCData.Data}"
            };
            logsRepository.Create(log);

            modbusTCP.SetData(pLCData);

            return pLCData;
        }

        public List<PLCDataDomain> GenerateNotificationMessage(List<PLCDataDomain> pLCDatas)
        {
            foreach (var item in pLCDatas)
            {
                if (!string.IsNullOrEmpty(item.DataEpressionRule))
                {
                    pLCDatas.AsQueryable().Where(item.DataEpressionRule).ToList().ForEach(el =>
                    {
                        el.NotificationMessage = "Error, be careful";
                        var log = new Log()
                        {
                            Date = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                            Message = $"Address: {el.Address}, {el.Data}, {el.NotificationMessage}"
                        };
                        logsRepository.Create(log);
                    });
                }
            }

            return pLCDatas;
        }
    }
}
