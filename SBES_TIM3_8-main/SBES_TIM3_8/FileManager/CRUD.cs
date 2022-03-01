using System;
using System.IO;
using Database.Service;
using Common.DataModels;
using Common.Certificates;
using Common.Signatures;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;

namespace FileManager
{
    public static class CRUD
    {
        private static string directory;
        private static FileRepo fileRepo;
        private static SignatureManager signatureManager;

        static CRUD()
        {
            directory = Environment.CurrentDirectory + "/../../Container/";
            CheckFolder();
            signatureManager = new SignatureManager();
            fileRepo = new FileRepo();
        }


        private static string CreatePath(string name)
        {
           string path = directory + name + ".txt";
           return path;
        }

        private static void CheckFolder()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }


        public static void CreateFile(string name)
        {
            var cert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, "signaturecert");

            string fileName = CreatePath(name);
            if (!File.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                    Console.WriteLine("Fajl uspjesno kreiran");
                    using(StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine("DUUULE");
                    }
                }

                using(FileStream fs = File.OpenRead(fileName))
                {
                    var sig = signatureManager.GenerateSignature(cert, fs);
                    fileRepo.Insert(new FileModel(0, name, fileName, sig, ErrorLevel.NoError,sig));
                }
            }
        }


        public static void AddExistingFile(string path)
        {
            var cert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, "signaturecert");

            if (!File.Exists(path))
            {
                Console.WriteLine($"Fajl ne postoji:{path}");
                return;
            }


            using(FileStream fs = File.OpenRead(path))
            {
                var signature = signatureManager.GenerateSignature(cert, fs);
                fileRepo.Insert(new FileModel(0, FileModel.GetFileNameFromPath(path), path, signature, ErrorLevel.NoError,signature));
            }

            Console.WriteLine("Fajl je uspesno dodat");
        }

        public static void UpdateFile(FileModel fm, string text)
        {
            var cert = CertManager.GetCertficitateBySubjectName(StoreName.My, StoreLocation.LocalMachine, "signaturecert");

            try
            {
                using (StreamWriter writer = new StreamWriter(fm.Path))
                {
                    writer.WriteLine(text);                   
                    Console.WriteLine("Fajl uspjesno azuriran");
                }

                using (FileStream fs = File.OpenRead(fm.Path))
                {
                    var sig = signatureManager.GenerateSignature(cert, fs);
                    FileModel updated = new FileModel(fm.Id, fm.Name, fm.Path, sig, fm.FileCriticallity,sig);
                    fileRepo.Update(updated);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }
        }

        public static string ReadFileContent(int id)
        {
            try
            {
                var fm = fileRepo.GetById(id);
                string ret = File.ReadAllText(fm.Path);
                return ret;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
                return null;
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public static void DeleteFile(FileModel fm)
        {
            try
            {
                File.Delete(fm.Path);
                fileRepo.DeleteByPath(fm.Path);
                Console.WriteLine("Fajl je obrisan");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }
        }

        public static List<FileModel> ReadAllFiles()
        {
            try
            {
                return fileRepo.GetAll().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
                return null;
            }
        }
    }
}
