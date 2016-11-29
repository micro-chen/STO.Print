//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.Business
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WcfWraper
    {
        public DotNetService GetService()
        {
            return new DotNetService();
        }
    }
}
