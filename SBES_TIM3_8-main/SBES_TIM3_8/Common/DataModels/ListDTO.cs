using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataModels
{
    [DataContract]
    public class ListDTO
    {
        [DataMember]
        public List<FileModel> lista;
        public ListDTO()
        {
            lista = new List<FileModel>();
        }
    }
}
