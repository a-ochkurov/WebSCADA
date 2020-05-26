using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Scada.Domain.Utils;
using WebSCADA.BLL.Services;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Tests.Services
{
    [TestFixture]
    public class ModbusServiceTests
    {
        private Mock<IRepository<Log>> mockLogsRepository;
        private Mock<IModbusTCPService> mockModbusTCPService;

        [Test]
        public void GetData_GetDataFromModbusService()
        {
            // Arrange
            mockLogsRepository = new Mock<IRepository<Log>>();
            mockModbusTCPService = new Mock<IModbusTCPService>();
            var pLCDatas = new List<PLCDataDomain>()
            {
                new PLCDataDomain
                {
                    Address = 1,
                    Data = "true",
                    TypePLC = PLCDataType.Coils,
                    DataEpressionRule = string.Empty,
                    NotificationMessage = string.Empty
                }
            };
            mockModbusTCPService.Setup(m => m.GetData(pLCDatas)).Returns(pLCDatas);

            var modbusService = new ModbusService(mockModbusTCPService.Object, mockLogsRepository.Object);

            // Act
            var data = modbusService.GetData(pLCDatas);

            // Assert
            mockModbusTCPService.Verify(s => s.GetData(It.IsAny<List<PLCDataDomain>>()), Times.Once);
            mockLogsRepository.Verify(x => x.Create(It.IsAny<Log>()), Times.Never);
        }

        [Test]
        public void GenerateNotificationMessage_SetNotification()
        {
            // Arrange
            mockLogsRepository = new Mock<IRepository<Log>>();
            mockModbusTCPService = new Mock<IModbusTCPService>();
            var pLCDatas = new List<PLCDataDomain>()
            {
                new PLCDataDomain
                {
                    Address = 1,
                    Data = "true",
                    TypePLC = PLCDataType.Coils,
                    DataEpressionRule = "x => bool.Parse(x.Data) == true",
                    NotificationMessage = string.Empty
                }
            };

            var modbusService = new ModbusService(mockModbusTCPService.Object, mockLogsRepository.Object);

            // Act
            var data = modbusService.GenerateNotificationMessage(pLCDatas);

            // Assert
            Assert.AreEqual("Error, be careful", data[0].NotificationMessage);
            mockLogsRepository.Verify(x => x.Create(It.IsAny<Log>()), Times.Once);
        }
    }
}
