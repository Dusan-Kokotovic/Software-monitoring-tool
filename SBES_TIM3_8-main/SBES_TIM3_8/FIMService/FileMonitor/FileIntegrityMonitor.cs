using Common.Certificates;
using Common.DataModels;
using Common.Signatures;
using Database.DataAccess;
using FIMService.WCFClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace FIMService.FileMonitor
{
    public class FileIntegrityMonitor
    {
        private FIMWCFClient Client;
        private SignatureManager SignatureManager;
        private ManualResetEvent stopSignal;
        private readonly int Timeout = 10000;
        private readonly string certName = "signaturecert";

        IFIMDB<FileModel> FilesProvider;

        public FileIntegrityMonitor(IFIMDB<FileModel> getter, FIMWCFClient client,SignatureManager signatureManager,ManualResetEvent stopSignal)
        {
            this.FilesProvider = getter;
            this.Client = client;
            this.SignatureManager = signatureManager;
            this.stopSignal = stopSignal;
        }

        public void Run()
        {
            Task.Factory.StartNew(()=> 
            {
                while(stopSignal.WaitOne())
                {
                    CheckFiles();
                    Thread.Sleep(Timeout);
                }
            });
        }

        private void CheckFiles()
        {
            var cert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, this.certName);
            var files = FilesProvider.GetAll();

            foreach (var file in files)
            {
                if (File.Exists(file.Path))
                {
                    using (var stream = File.OpenRead(file.Path))
                    {
                        if (!SignatureManager.CheckSignature(cert, file.LastKnownSignature, stream))
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            var currentSignature = SignatureManager.GenerateSignature(cert, stream);
                            file.RaiseCriticality();
                            FilesProvider.UpdateLastKnownSignature(new FileModel { Id = file.Id, LastKnownSignature = currentSignature,FileCriticallity=file.FileCriticallity });
                            this.Client.SendError(new ErrorModel(file.Name, file.Path, file.FileCriticallity, System.DateTime.Now));
                        }
                    }
                }
            }
        }
    }
}
