using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ServiceContract]
    public interface ICRUD : IDelete
    {
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void CreateFile(string name);
        
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void AddExistingFile(string path);
        
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        void UpdateFile(FileModel fm, string text);
        
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        string ReadFileContent(int id);


        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        ListDTO ReadAllFiles();
    }
}
