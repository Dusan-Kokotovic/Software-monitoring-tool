using Common.Contracts;
using Common.Security;
using Common.WCFServiceHost;
using Database.Service;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Security.Policy;
using System.ServiceModel;
using System.Threading.Tasks;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {

            FileRepo repo = new FileRepo();

            ServiceAuthorizationManager manager = new CustomAuthorizationManager();
            IAuthorizationPolicy policy = new CustomAuthorizationPolicy();
            string address = "net.tcp://localhost:7000/FM";


            WCFServiceHostWindowsAuth<ICRUD, FileManagerWCFService> host = new WCFServiceHostWindowsAuth<ICRUD, FileManagerWCFService>(
                BindingFactory.GetConfiguredBinding(Common.Security.SecurityMode.WindowsAuth), address,manager,policy);

            host.Open();
            Console.WriteLine("Exit");
            Console.ReadKey();
            host.Close();
        }
    }
}
