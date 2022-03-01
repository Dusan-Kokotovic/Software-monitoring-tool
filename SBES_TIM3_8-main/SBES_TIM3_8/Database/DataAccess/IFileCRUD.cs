using Common.DataModels;


namespace Database.DataAccess
{
    public interface IFileCRUD : ICRUD<FileModel,int>
    {
        void DeleteByPath(string path);
    }
}
