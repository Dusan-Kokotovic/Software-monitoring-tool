using Common.Contracts;
using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


namespace Client
{
    public class WCFClient : ChannelFactory<ICRUD>, ICRUD, IDisposable
    {
        private ICRUD Channel;

        public WCFClient(NetTcpBinding binding, EndpointAddress endpoint) : base(binding, endpoint)
        {
            this.Channel = this.CreateChannel();
        }


        public void Dispose()
        {

            if (this.Channel != null)
            {
                this.Channel = null;
            }
            this.Close();
        }

        public void AddExistingFile(string path)
        {
            try
            {
                Channel.AddExistingFile(path);
            }
            catch(Exception e)
            {
                Console.WriteLine( $"Exception : {e.Message}");
            }
        }

        public void CreateFile(string name)
        {
            try
            {
                Channel.CreateFile(name);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
            }
        }

        public void DeleteFile(FileModel fm)
        {
            try
            {
                Channel.DeleteFile(fm);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
            }
        }

        public string ReadFileContent(int id)
        {
            try
            {
               return Channel.ReadFileContent(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
                return "";
            }
        }

        public void UpdateFile(FileModel fm, string text)
        {
            try
            {
                Channel.UpdateFile(fm, text);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
            }
        }

        public ListDTO ReadAllFiles()
        {
            ListDTO dto = new ListDTO();
            try
            {
                dto = Channel.ReadAllFiles();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
            }
            return dto;

        }
    }
}
