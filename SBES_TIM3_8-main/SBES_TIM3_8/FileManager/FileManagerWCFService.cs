using Common.Contracts;
using Common.DataModels;
using System.ServiceModel;
using System.Threading;

namespace FileManager
{
    [ServiceBehavior(IncludeExceptionDetailInFaults =true)]

    public class FileManagerWCFService : ICRUD
    {

        //[PrincipalPermission(SecurityAction.Demand, Role = "Create")]
        public void AddExistingFile(string path)
        {
            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Create"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");
            CRUD.AddExistingFile(path);
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Create")]
        public void CreateFile(string name)
        {
            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Create"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");

            CRUD.CreateFile(name);
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public void DeleteFile(FileModel fm)
        {
            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Delete"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");

            CRUD.DeleteFile(fm);
        }

        public ListDTO ReadAllFiles()
        {

            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Read"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");


            ListDTO listDTO = new ListDTO();
            listDTO.lista = CRUD.ReadAllFiles();
            return listDTO;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public string ReadFileContent(int id)
        {

            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Read"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");

            return CRUD.ReadFileContent(id);
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public void UpdateFile(FileModel fm, string text)
        {
            var principal = Thread.CurrentPrincipal;

            if (principal == null || !principal.IsInRole("Modify"))
                throw new FaultException<SecurityException>(new SecurityException(), "Nemate prava pristupa");

            CRUD.UpdateFile(fm, text);
        }
    }

}
