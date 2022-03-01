using Common.Certificates;
using Common.Contracts;
using Common.Security;
using Common.WCFServiceHost;
using FIMService.WCFClient;
using IntrusionPrevensionSystem.WCFClient;
using IntrusionPrevensionSystem.WCFService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
namespace IntrusionPreventionSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:6000/IPS";
            string ipsCertName = "ipscert";
            string FMEndpoint = "net.tcp://localhost:7000/FM";

            var myCert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, ipsCertName);

            NetTcpBinding clientConfiguredBinding = BindingFactory.GetConfiguredBinding(Common.Security.SecurityMode.WindowsAuth);
            EndpointAddress clientEndPointAddress = new EndpointAddress(new Uri(FMEndpoint));
            IPSWCFClient wcfClient = new IPSWCFClient(clientConfiguredBinding,clientEndPointAddress);

            IPSWCFService wcfService = new IPSWCFService(wcfClient);

            
            

            NetTcpBinding configuredBinding = BindingFactory.GetConfiguredBinding(Common.Security.SecurityMode.Certificate);

            WCFServiceHostCert<ISendError,IPSWCFService,IPSServiceCertValidator> ipsWcfService = new WCFServiceHostCert<ISendError,
                IPSWCFService, 
                IPSServiceCertValidator>(wcfService,configuredBinding, address, ipsCertName);



            ipsWcfService.Open();
          
            Console.WriteLine("Press enter to exit application");
            Console.ReadLine();
         
        }
    }
}
