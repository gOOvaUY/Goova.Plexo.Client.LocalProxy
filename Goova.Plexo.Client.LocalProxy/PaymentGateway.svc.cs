using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Goova.ElSwitch.Client.SDK;

namespace Goova.ElSwitch.Local.Proxy
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PaymentGateway : IPaymentGateway
    {
        PaymentGatewayClient _cl = PaymentGatewayClientFactory.GetClient(Properties.Settings.Default.ClientName);

        public Task<ServerResponse<string>> Authorize(Authorization authorization)
        {
            return _cl.Authorize(authorization);
        }

        public Task<ServerResponse<List<IssuerInfo>>> GetSupportedIssuers()
        {
            return _cl.GetSupportedIssuers();
        }

        public Task<ServerResponse<Transaction>> Purchase(PaymentRequest payment)
        {
            return _cl.Purchase(payment);
        }

        public Task<ServerResponse<Transaction>> Cancel(CancelRequest payment)
        {
            return _cl.Cancel(payment);
        }
    }
}
