//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;

namespace DotNet.Model
{
    public class DRDataReader : IDataRow
    {
        private IDataReader dr;

        public DRDataReader(IDataReader dr)
        {
            this.dr = dr;
        }

        #region IDataRow 成员
        public object this[string name]
        {
            get
            {
                return dr[name];
            }
        }

        public object this[int i]
        {
            get
            {
                return dr[i];
            }
        }

        /// <summary>
        /// 判断是否含有指定字段
        /// </summary>
        /// <param name="columnName">字段名字</param>
        /// <returns></returns>
        public bool ContainsColumn(string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                // 2015-09-15 吉日嘎拉 这个需要大小写问题注意，Oracle 里会会自动变成大写
                if (dr.GetName(i).Equals(columnName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
