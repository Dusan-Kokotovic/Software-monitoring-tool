using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;

namespace Common.Signatures
{
    public class SignatureManager
    {
        public byte[] GenerateSignature(X509Certificate2 cert,FileStream file)
        {
            RSACryptoServiceProvider provider = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] fileHash = this.ComputeHash(file);

            return provider.SignHash(fileHash, CryptoConfig.MapNameToOID("SHA1"));
        }

        public bool CheckSignature(X509Certificate2 cert,byte[] signature,FileStream file)
        {
            RSACryptoServiceProvider provider = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] fileHash = this.ComputeHash(file);

            return provider.VerifyHash(fileHash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }


        public byte[] ComputeHash(FileStream file)
        {
            SHA1Managed sha = new SHA1Managed();
            return sha.ComputeHash(file);
        }
    }
}
