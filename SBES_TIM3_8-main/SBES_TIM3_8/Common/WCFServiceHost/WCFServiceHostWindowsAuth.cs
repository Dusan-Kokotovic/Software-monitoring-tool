using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Common.WCFServiceHost
{
    public class WCFServiceHostWindowsAuth<IContract, Implementation> : WCFServiceHost<IContract, Implementation>
        where IContract : class
        where Implementation : class
    {
        public WCFServiceHostWindowsAuth(IContract implementation,NetTcpBinding binding, string address,ServiceAuthorizationManager manager,
            IAuthorizationPolicy policy):base(implementation,manager,policy)
        {
            this.AddSecureEndpoint(binding, address);
        }


        public WCFServiceHostWindowsAuth(NetTcpBinding binding,string address, ServiceAuthorizationManager manager,IAuthorizationPolicy policy)
            :base(manager,policy)
        {
            this.AddSecureEndpoint(binding, address);
        }
        public WCFServiceHostWindowsAuth(IContract implementation, NetTcpBinding binding, string address) : base(implementation)
        {
            this.AddSecureEndpoint(binding, address);
        }

        public WCFServiceHostWindowsAuth(NetTcpBinding binding,string address):base()
        {
            this.AddSecureEndpoint(binding, address);
        }

        protected override void AddSecureEndpoint(NetTcpBinding binding, string address)
        {
            this.Host.AddServiceEndpoint(typeof(IContract),binding, address);
        }
    }
}
