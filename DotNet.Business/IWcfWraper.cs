//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.Business
{
    [ServiceContract]
    public interface IWcfWraper
    {
        [OperationContract]
        DotNetService GetService();
    }
}
