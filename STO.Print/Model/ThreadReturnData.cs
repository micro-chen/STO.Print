using System.Collections.Generic;
using System.Threading;

namespace STO.Print.Model
{
    public class ThreadReturnData
    {
        public ManualResetEvent manual;

        public List<ZtoPrintBillEntity> res;

        public void ReturnThreadData(object state)
        {
            if (state is List<ZtoPrintBillEntity>)
            {
                res = state as List<ZtoPrintBillEntity>;
            }
            else
            {
                res = new List<ZtoPrintBillEntity>();
            }
            manual.Set();
        }
    }
}
