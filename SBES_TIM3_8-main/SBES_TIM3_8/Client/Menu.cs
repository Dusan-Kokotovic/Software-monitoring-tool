using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Menu
    {
        WCFClient WcfClient;

        public Menu(WCFClient client)
        {
            WcfClient = client;            
        }

        public void Run()
        {
            int choice;

            while ((choice = PrintMenu()) != 6)
            {
                switch (choice)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Update();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        Read();
                        break;
                    case 5:
                        AddExistingFile();
                        break;
                    case 6:
                        break;
                }

            }

        }

        private void AddExistingFile()
        {
            Console.WriteLine("Putanja do fajla:");
            string path = Console.ReadLine();
            WcfClient.AddExistingFile(path);
        }

        private void Create()
        {
            Console.WriteLine("Naziv fajla : ");
            string naziv = Console.ReadLine();
            WcfClient.CreateFile(naziv);
        }

        private void Delete()
        {
            var fileModel = GetFileModel();
            if(fileModel == null)
            {
                return;
            }
            WcfClient.DeleteFile(fileModel);
        }


        private void Update()
        {
            var fileModel = GetFileModel();
            if (fileModel == null)
            {
                return;
            }
            Console.WriteLine("Unesite novi tekst fajla:");
            string text = Console.ReadLine();
            WcfClient.UpdateFile(fileModel, text);
        }

        private void Read()
        {
            var fileModel = GetFileModel();
            if (fileModel == null)
            {
                return;
            }
            Console.WriteLine(WcfClient.ReadFileContent(fileModel.Id));
        }


        private int PrintMenu()
        {

            int x = -1;
            do
            {
                Console.WriteLine("UNESITE KOMANDU");
                Console.WriteLine("1. Kreiraj fajl");
                Console.WriteLine("2. Izmeni fajl");
                Console.WriteLine("3. Obrisi fajl");
                Console.WriteLine("4. Procitaj fajl");
                Console.WriteLine("5. Dodaj postojeci fajl");
                Console.WriteLine("6. Izadji");

                if (!int.TryParse(Console.ReadLine(), out x))
                    x = -1;
            
            } while (x == -1 || (x < 1 || x > 6));

            return x;
        }


        private FileModel GetFileModel()
        {
            var allfiles = WcfClient.ReadAllFiles().lista;
            int index = PrintFiles(allfiles);
            if(index == -1)
            {
                return null;
            }
            else
            {
                return allfiles[index - 1];
            }
        }

        private int PrintFiles(List<FileModel> files)
        {
            if(files.Count == 0)
            {
                Console.WriteLine("Lista je prazna");
                return -1;
            }


            for(int i = 0;i<files.Count;i++)
            {
                Console.WriteLine($"{i+1}. {files[i].Name}");
            }


            int choice = -1;
            do
            {
                Console.WriteLine("Unesite redni broj fajla:");
                if (!int.TryParse(Console.ReadLine(), out choice))
                    choice = -1;


            } while (choice == -1 || (choice < 1 || choice > files.Count));


            return choice;
        }

    }
}
