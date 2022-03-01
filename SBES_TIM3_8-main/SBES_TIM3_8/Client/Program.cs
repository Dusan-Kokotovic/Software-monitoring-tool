using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Common;
using Common.Security;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = BindingFactory.GetConfiguredBinding(Common.Security.SecurityMode.WindowsAuth);
            string address = "net.tcp://localhost:7000/FM";
            EndpointAddress clientEndPointAddress = new EndpointAddress(new Uri(address));
            WCFClient client = new WCFClient(binding, clientEndPointAddress);
            Menu menu = new Menu(client);
            menu.Run();
        }
    }
}
