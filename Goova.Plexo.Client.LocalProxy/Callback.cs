using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace Goova.Plexo.Client.LocalProxy
{
    internal class Callback : ClientBase<ICallback>, ICallback //This will be loaded by reflection
    {
        public Callback()
        {
            int timeout = 120;
            try
            {
                WebHttpBinding binding = new WebHttpBinding();
                binding.OpenTimeout = TimeSpan.FromSeconds(timeout);
                binding.CloseTimeout = TimeSpan.FromSeconds(timeout);
                binding.SendTimeout = TimeSpan.FromSeconds(timeout);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(timeout);
                if (ElSwitch.Local.Proxy.Properties.Settings.Default.CallbackUrl.StartsWith("https"))
                    binding.Security.Mode = WebHttpSecurityMode.Transport;

                ServiceEndpoint svc = new ServiceEndpoint(ContractDescription.GetContract(typeof(ICallback)),
                    binding, new EndpointAddress(ElSwitch.Local.Proxy.Properties.Settings.Default.CallbackUrl));
                WebHttpBehavior behavior = new WebHttpBehavior
                {
                    DefaultBodyStyle = WebMessageBodyStyle.Bare,
                    DefaultOutgoingRequestFormat = WebMessageFormat.Json,
                    DefaultOutgoingResponseFormat = WebMessageFormat.Json,
                    HelpEnabled = true
                };
                svc.Behaviors.Add(behavior);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<ClientResponse> Instrument(IntrumentCallback instrument)
        {
            ClientResponse r=await Channel.Instrument(instrument);
            r.Client = ElSwitch.Local.Proxy.Properties.Settings.Default.ClientName;
            return r;
        }
    }
}