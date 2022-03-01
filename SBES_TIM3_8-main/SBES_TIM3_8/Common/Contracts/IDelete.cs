﻿using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ServiceContract]
    public interface IDelete
    {
        [OperationContract]
        void DeleteFile(FileModel fm);
    }
}
