using Scada.Domain.Utils;

namespace WebSCADA.Domain.Models
{
    public class PLCDataDomain
    {
        public int Address { get; set; }

        public PLCDataType TypePLC { get; set; }

        public string Data { get; set; }

        public string NotificationMessage { get; set; }

        public string DataEpressionRule { get; set; }
    }
}
