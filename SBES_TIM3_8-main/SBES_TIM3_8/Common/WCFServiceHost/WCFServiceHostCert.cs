using Common.Certificates;
using Common.Security;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Common.WCFServiceHost
{
    public class WCFServiceHostCert<IContract, Implementation, HostCertValidator> : WCFServiceHost<IContract, Implementation>
        where IContract: class
        where Implementation: class
        where HostCertValidator : X509CertificateValidator,new()
    {

        private string hostCertSubjectName;

        public WCFServiceHostCert(NetTcpBinding binding,string address,string hostCertSubjectName):base()
        {
            this.hostCertSubjectName = hostCertSubjectName;
            this.AddSecureEndpoint(binding, address);
        }

        public WCFServiceHostCert(IContract implementation,NetTcpBinding binding,string address,string hostCertSubjectName):base(implementation)
        {
            this.hostCertSubjectName = hostCertSubjectName;
            this.AddSecureEndpoint(binding, address);
        }

        protected override void AddSecureEndpoint(NetTcpBinding binding,string address)
        {
            var hostCert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, hostCertSubjectName);
            Host.Credentials.ClientCertificate.Authentication.CertificateValidationMode =
            System.ServiceModel.Security.X509CertificateValidationMode.Custom;

            Host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new HostCertValidator();

            Host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            Host.Credentials.ServiceCertificate.Certificate = hostCert;

            this.Host.AddServiceEndpoint(typeof(IContract), binding, address);
        }
    }
}
