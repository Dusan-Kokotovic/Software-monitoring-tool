using System;
using System.Security.Cryptography.X509Certificates;

namespace Common.Certificates
{
    public static class CertManager
    {
        public static X509Certificate2 GetCertficitateBySubjectName(StoreName storeName,StoreLocation storeLocation,string subjectName)
        {
            X509Store certStore = new X509Store(storeName, storeLocation);

            certStore.Open(OpenFlags.ReadOnly);
            var certCollection = certStore.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            foreach(var cert in certCollection)
            {
                if(cert.SubjectName.Name.Equals($"CN={subjectName}"))
                {
                    return cert;
                }
            }

            return null;
        }
    }
}
