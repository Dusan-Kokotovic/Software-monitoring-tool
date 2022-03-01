﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FIMService.WCFClient
{
    public class FIMClientCertValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            if(certificate.Issuer.Equals(certificate.Subject))
            {
                throw new Exception("Service certificate is self-signed");
            }
        }
    }
}
