using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Common.WCFServiceHost
{
    public abstract class WCFServiceHost<IContract,Implementation>
        where IContract : class
        where Implementation : class
    {

        protected ServiceHost Host;


        public WCFServiceHost(ServiceAuthorizationManager manager,IAuthorizationPolicy policy)
        {
            this.Host = new ServiceHost(typeof(Implementation));
            AddPolicy(policy);
            AddCustomAuthManager(manager);

        }
        public WCFServiceHost()
        {
            this.Host = new ServiceHost(typeof(Implementation));
        }

        public WCFServiceHost(IContract singletonImplementation)
        {
            this.Host = new ServiceHost(singletonImplementation);
        }

        public WCFServiceHost(IContract implementation,ServiceAuthorizationManager manager, IAuthorizationPolicy policy)
        {
            this.Host = new ServiceHost(implementation);
            AddPolicy(policy);
            AddCustomAuthManager(manager);
        }

        private void AddPolicy(IAuthorizationPolicy policy)
        {
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(policy);
            this.Host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
        }

        private void AddCustomAuthManager(ServiceAuthorizationManager manager)
        {
            this.Host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            this.Host.Authorization.ServiceAuthorizationManager = manager;

        }

        protected abstract void AddSecureEndpoint(NetTcpBinding binding,string address);

        public void Open()
        {
            try
            {
                this.Host.Open();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Close()
        {
            try
            {
                if(this.Host.State != CommunicationState.Faulted && this.Host.State != CommunicationState.Closed)
                {
                    this.Host.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
