using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Common.Contracts;
using Common.DataModels;
using IntrusionPrevensionSystem.SecurityManager;
using IntrusionPrevensionSystem.WCFClient;

namespace IntrusionPrevensionSystem.WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class IPSWCFService : ISendError
    {

        private IPSWCFClient ipsClient;

        public IPSWCFService(IPSWCFClient ipsClient)
        {
            this.ipsClient = ipsClient;

        }

        public void SendError(ErrorModel error)
        {
           
            Audit.LogEvents(error.FileName, error.FilePath, error.ErrorLevel, error.DetectionDateTime);
            if(error.ErrorLevel == ErrorLevel.Critical)
            {
                FileModel fm = new FileModel();
                fm.Path = error.FilePath;
                ipsClient.DeleteFile(fm);

            }
        }
    }
}
