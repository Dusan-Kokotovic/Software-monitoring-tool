
namespace Database.DataAccess
{
    public interface ICRUD<T,ID>:IFIMDB<T>
    {
        int Count();
        T GetById(ID id);
        void Insert(T entity);
        bool Exists(ID id);
        void Delete(T entity);
    
    }
}
