using Common.Contracts;
using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IntrusionPrevensionSystem.WCFClient
{
        public class IPSWCFClient : ChannelFactory<IDelete>, IDelete, IDisposable
        {
            private IDelete Channel;

            public IPSWCFClient(NetTcpBinding binding, EndpointAddress endpoint) : base(binding, endpoint)
            {
                this.Channel = this.CreateChannel();
            }

            public void DeleteFile(FileModel fm)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        this.Channel.DeleteFile(fm);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }

            public void Dispose()
            {

                if (this.Channel != null)
                {
                    this.Channel = null;
                }
                this.Close();
            }
        }

    
}
