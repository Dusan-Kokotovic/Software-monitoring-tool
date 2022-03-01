using System;
using System.ServiceModel;

namespace Common.Security
{
    public enum SecurityMode
    {
        Certificate,
        WindowsAuth
    }

    public static class BindingFactory
    {
        public static NetTcpBinding GetConfiguredBinding(SecurityMode securityMode)
        {
            NetTcpBinding binding = new NetTcpBinding();
            switch(securityMode)
            {
                case SecurityMode.Certificate:
                    ConfigureCertificateSecurity(ref binding);
                    break;
                case SecurityMode.WindowsAuth:
                    ConfigureWindowsAuthSecurity(ref binding);
                    break;
                default:
                    Console.WriteLine($"Wrong security mode:{securityMode.ToString()}");
                    break;
            }

            return binding;
        }

        private static void ConfigureCertificateSecurity(ref NetTcpBinding binding)
        {
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
        }

        private static void ConfigureWindowsAuthSecurity(ref NetTcpBinding binding)
        {
            binding.Security.Mode = System.ServiceModel.SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
        }
    }
}
