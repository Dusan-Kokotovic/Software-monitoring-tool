using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;

namespace Common.Security
{
    public static class ChannelSecurityConfigurator
    {
        
        public static void ConfigureClientCertSecurity<IContract,ClientCertValidator>(ChannelFactory<IContract> factory, X509Certificate2 clientCert) 
            where ClientCertValidator: X509CertificateValidator, new()
        {
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = 
                System.ServiceModel.Security.X509CertificateValidationMode.Custom;

            factory.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode =
                System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;

            factory.Credentials.ClientCertificate.Certificate = clientCert;
        }

        public static void ConfigureHostCertSecurity<ServiceCertValidator>(ServiceHost host, X509Certificate2 hostCert)
            where ServiceCertValidator: X509CertificateValidator, new()
        {
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode =
                System.ServiceModel.Security.X509CertificateValidationMode.Custom;

            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = hostCert;
        }
    }
}
