using System;
using System.Collections.Generic;
using System.Linq;
using EasyModbus;
using Scada.Domain.Utils;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.DAL.Modbus
{
    public class ModbusTCPService : IModbusTCPService
    {
        private readonly ModbusClient modbusClient;
        public ModbusTCPService(string ipAddress, int port)
        {
            this.modbusClient = new ModbusClient(ipAddress, port);
            try
            {
                modbusClient.Connect();
            }
            catch (Exception)
            {
            }

        }

        public List<PLCDataDomain> GetData(List<PLCDataDomain> listAddresses)
        {
            foreach (var item in listAddresses)
            {

                try
                {
                    switch (item.TypePLC)
                    {
                        case PLCDataType.Register:
                            item.Data = modbusClient.ReadHoldingRegisters(item.Address, 1).FirstOrDefault().ToString();
                            break;
                        case PLCDataType.Coils:
                            item.Data = modbusClient.ReadCoils(item.Address, 1).FirstOrDefault().ToString();
                            break;
                        default:
                            item.Data = "None data";
                            break;
                    }
                }
                catch (Exception)
                {

                    item.Data = "None data";
                }

            }

            return listAddresses;
        }

        public void SetData(PLCDataDomain pLCData)
        {

            switch (pLCData.TypePLC)
            {
                case PLCDataType.Register:
                    try
                    {
                        modbusClient.WriteSingleRegister(pLCData.Address, int.Parse(pLCData.Data));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                    break;
                case PLCDataType.Coils:
                    try
                    {
                        modbusClient.WriteSingleCoil(pLCData.Address, bool.Parse(pLCData.Data));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
