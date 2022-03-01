using Common.Certificates;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IntrusionPrevensionSystem.WCFService
{
    public class IPSServiceCertValidator : X509CertificateValidator
    {

        public override void Validate(X509Certificate2 certificate)
        {
            string ipsCertName = "ipscert";
            var myCert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, ipsCertName);

            if (!certificate.Issuer.Equals(myCert.Issuer)) { 
           
                throw new Exception("Client is not published by same issuer");
            }
        }
    }
}
