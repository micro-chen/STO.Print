//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	BaseManager
    /// 通用基类部分
    /// 
    /// 总觉得自己写的程序不上档次，这些新技术也玩玩，也许做出来的东西更专业了。
    /// 修改记录
    /// 
    /// 
    ///		2012.03.20 版本：2.0 JiRiGaLa 代码进行简化。
    ///		2012.02.04 版本：1.0 JiRiGaLa 进行提炼，把代码进行分组。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.02.04</date>
    /// </author> 
    /// </summary>
    public partial class BaseManager : IBaseManager
    {
        //
        // 删除数据部分
        //
        public virtual int Delete()
        {
			return MyDelete(null);
			//return DbLogic.Delete(DbHelper, this.CurrentTableName);
        }

        public virtual int Delete(object id)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(this.PrimaryKey, id));
			return MyDelete(parameters);
			//return DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);
        }

        public virtual int Delete(object[] ids)
        {
            int result = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(this.PrimaryKey, ids[i]));
				result += MyDelete(parameters);
				//result += DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);
            }
            return result;
        }

        public virtual int Delete(string name, object[] values)
        {
            int result = 0;
            for (int i = 0; i < values.Length; i++)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(name, values[i]));
				result += MyDelete(parameters);
				//result += DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);
            }
            return result;
        }

        public virtual int Delete(params KeyValuePair<string, object>[] parameters)
        {
            List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersList.Add(parameters[i]);
            }
			return MyDelete(parametersList);
			//return DbLogic.Delete(DbHelper, this.CurrentTableName, parametersList);
        }

        public virtual int Delete(List<KeyValuePair<string, object>> parameters)
        {
			return MyDelete(parameters);
			//return DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);
        }

		private int MyDelete(List<KeyValuePair<string, object>> parameters)
		{
			parameters = GetDeleteExtParam(parameters);
			return DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);
		}

        public virtual int Truncate()
        {
            return DbLogic.Truncate(DbHelper, this.CurrentTableName);
        }

		//
		// 添加删除的附加条件
		//
		protected virtual List<KeyValuePair<string, object>> GetDeleteExtParam(List<KeyValuePair<string, object>> parameters)
		{
			return parameters ?? new List<KeyValuePair<string, object>>();
		}
    }
}