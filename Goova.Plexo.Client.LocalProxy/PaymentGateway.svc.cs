using System.Collections.Generic;
using System.Threading.Tasks;
using Goova.Plexo.Client.SDK;

namespace Goova.Plexo.Client.LocalProxy
{
    public class PaymentGateway : IPaymentGateway
    {
        PaymentGatewayClient _cl = PaymentGatewayClientFactory.GetClient(Local.Proxy.Properties.Settings.Default.ClientName);

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
