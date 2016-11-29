//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// 获取实体的委托
    /// </summary>
    /// <param name="dr">数据行</param>
    /// <returns>实体</returns>
    /// public delegate T MapEntity<T>(IDataReader dr);

    public partial class BaseManager : IBaseManager
    {
        public virtual List<T> GetListById<T>(string id) where T : BaseEntity, new()
        {
            return this.GetList<T>(new KeyValuePair<string, object>(BaseBusinessLogic.FieldId, id));
        }

        public virtual List<T> GetList<T>(int topLimit = 0, string order = null) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, null, topLimit, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(string[] ids) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, "*", BaseBusinessLogic.FieldId, ids))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(string name, Object[] values, string order = null) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, "*", name, values, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(KeyValuePair<string, object> parameter, string order) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter);
            
            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parameters, 0, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2, string order) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter1);
            parameters.Add(parameter2);
            
            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parameters, 0, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(KeyValuePair<string, object> parameter, int topLimit = 0, string order = null) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter);
            
            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parameters, topLimit, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(List<KeyValuePair<string, object>> parameters, string order) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parameters, 0, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(List<KeyValuePair<string, object>> parameters, int topLimit = 0, string order = null) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parameters, topLimit, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(params KeyValuePair<string, object>[] parameters) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersList.Add(parameters[i]);
            }
            
            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, parametersList))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList<T>(string where) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader(DbHelper, this.CurrentTableName, where))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public virtual List<T> GetList2<T>(string where, int topLimit = 0, string order = null) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            using (IDataReader dr = DbLogic.ExecuteReader2(DbHelper, this.CurrentTableName, where, topLimit, order))
            {
                result = GetList<T>(dr);
            }

            return result;
        }

        public List<T> GetList<T>(IDataReader dr) where T : BaseEntity, new()
        {
            // 还能继承 IBaseEntity<T>
            List<T> result = new List<T>();

            while (dr.Read())
            {
                // T t = new T();
                // listT.Add(t.GetFrom(dr));
                // T dynTemp = BaseEntity.Create<T>();
                // listT.Add((T)dynTemp.GetFrom(dr));
                result.Add(BaseEntity.Create<T>(dr, false));
            }

            return result;
        }

        /*
        public List<T> GetList<T>(IDataReader dr) where T : new()
        {
            // 还能继承 IBaseEntity<T>
            List<T> listT = new List<T>();
            while (dr.Read())
            {
                listT.Add(mapEntity(dr));
            }
            dr.Close();
            return listT;
        }
        */
    }
}
