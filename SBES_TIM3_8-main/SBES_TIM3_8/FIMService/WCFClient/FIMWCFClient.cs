using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.Contracts;
using Common.DataModels;
using System.Threading;
using System.Threading.Tasks;
using Common.Security;
using System.Security.Cryptography.X509Certificates;

namespace FIMService.WCFClient
{
    public class FIMWCFClient : ChannelFactory<ISendError>, ISendError, IDisposable
    {
        private ISendError Channel;

        public FIMWCFClient(NetTcpBinding binding,EndpointAddress endpoint,X509Certificate2 clientCert):base(binding,endpoint)
        {
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode =
                            System.ServiceModel.Security.X509CertificateValidationMode.Custom;

            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new FIMClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode =
                System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = clientCert;

            this.Channel = this.CreateChannel();
        }

        public void SendError(ErrorModel error)
        {
            try
            {
                this.Channel.SendError(error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {

            if(this.Channel != null)
            {
                this.Channel = null;
            }
            this.Close();
        }
    }
}
