using Common.DataModels;
using Database.DataAccess;
using Database.DataAccess.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Database.Service
{
    public class FileRepo : IFileCRUD
    {
        private static IFileCRUD fileAccess = new FileDbDataAccess();

        public int Count()
        {
            return fileAccess.Count();
        }

        public IEnumerable<FileModel> GetAll()
        {
            return fileAccess.GetAll().ToList();
        }
        public FileModel GetById(int id)
        {
            return fileAccess.GetById(id);
        }

        public void Insert(FileModel model)
        {
            fileAccess.Insert(model);
        }

        public void Update(FileModel model)
        {
            fileAccess.Update(model);
        }

        public bool Exists(int id)
        {
            return fileAccess.Exists(id);
        }

        public void Delete(FileModel model)
        {
            fileAccess.Delete(model);
        }

        public void DeleteByPath(string path)
        {
            fileAccess.DeleteByPath(path);
        }

        public void UpdateLastKnownSignature(FileModel entity)
        {
            fileAccess.UpdateLastKnownSignature(entity);
        }
    }
}
