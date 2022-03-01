using Common.Certificates;
using Common.DataModels;
using Common.Security;
using Common.Signatures;
using Database.DataAccess;
using Database.Service;
using FIMService.FileMonitor;
using FIMService.WCFClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;

namespace FIMService
{
    class Program
    {
        static void Main(string[] args)
        {
            //premestiti u nekakav konfiguracioni fajl i citati iz adrese endpointa i imena sertifikata

            string ipsEndpoint = "net.tcp://localhost:6000/IPS";
            string ipsCertName = "ipscert";
            string myCertName = "fimservice";



            var ipsCert = CertManager.GetCertficitateBySubjectName(StoreName.TrustedPeople, StoreLocation.LocalMachine, ipsCertName);
            var myCert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, myCertName);

            NetTcpBinding configuredBinding = BindingFactory.GetConfiguredBinding(Common.Security.SecurityMode.Certificate);
            EndpointAddress endpoint = new EndpointAddress(new Uri(ipsEndpoint), EndpointIdentity.CreateX509CertificateIdentity(ipsCert));
            
            FIMWCFClient wcfClient = new FIMWCFClient(configuredBinding, endpoint,myCert);
            SignatureManager signatureManager = new SignatureManager();

            ManualResetEvent stopSignal = new ManualResetEvent(true);
            IFIMDB<FileModel> dbProvider = new FileRepo();

            FileIntegrityMonitor fileIntegrityMonitor = new FileIntegrityMonitor(
                dbProvider,
                client: wcfClient,
                signatureManager: signatureManager,
                stopSignal:stopSignal);


            fileIntegrityMonitor.Run();

            Console.WriteLine("Press [Enter] to exit FIM application");
            Console.ReadLine();
            stopSignal.Reset();

        }
    }
}
