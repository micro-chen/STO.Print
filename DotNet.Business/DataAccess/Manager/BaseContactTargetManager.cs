//-----------------------------------------------------------------------
// <copyright file="BaseContactTargetManager.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseContactTargetManager
    /// 内部邮件明细表
    ///
    /// 修改记录
    ///
    ///     2015-09-02 版本：1.0 Jirigala  给网点的、给省、市、县的？
    ///     2015-09-02 版本：1.0 Jirigala  创建。 
    ///
    /// 版本：1.1
    ///
    /// <author>
    ///		<name>Jirigala</name>
    ///		<date>2015-09-02</date>
    /// </author>
    /// </summary>
    public partial class BaseContactTargetManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 联络单可以按省看？
        /// </summary>
        /// <param name="contactId">联络单主键</param>
        /// <param name="areaId">省份主键</param>
        /// <returns>影响行数</returns>
        public void AddArea(string contactId, string areaId)
        {
            BaseContactTargetEntity entity = new BaseContactTargetEntity();
            entity.Id = Guid.NewGuid().ToString("N");
            entity.ContactId = contactId;
            entity.Category = "Area";
            entity.ReceiverId = areaId;
            entity.ReceiverRealName = BaseAreaManager.GetNameByByCache(areaId);
            this.Add(entity);
        }

        public void AddArea(string contactId, string[] areaIds)
        {
            foreach (string id in areaIds)
            {
                AddArea(contactId, id);
            }
        }

        public int RemoveArea(string contactId, string areaId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldCategory, "Area"));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldContactId, contactId));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldReceiverId, areaId));
            return this.Delete(parameters);
        }

        public int RemoveArea(string contactId, string[] areaIds)
        {
            int result = 0;
            foreach (string id in areaIds)
            {
                result += RemoveArea(contactId, id);
            }
            return result;
        }

        public string[] GetArea(string contactId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldCategory, "Area"));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldContactId, contactId));
            return this.GetProperties(parameters, BaseContactTargetEntity.FieldReceiverId);
        }


        public void AddCompany(string contactId, string companyId)
        {
            BaseContactTargetEntity entity = new BaseContactTargetEntity();
            entity.Id = Guid.NewGuid().ToString("N");
            entity.ContactId = contactId;
            entity.Category = "Company";
            entity.ReceiverId = companyId;
            entity.ReceiverRealName = BaseOrganizeManager.GetNameByCache(companyId);
            this.Add(entity);
        }

        public void AddCompany(string contactId, string[] companyIds)
        {
            foreach (string id in companyIds)
            {
                AddCompany(contactId, id);
            }
        }

        public int RemoveCompany(string contactId, string companyId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldCategory, "Company"));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldContactId, contactId));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldReceiverId, companyId));
            return this.Delete(parameters);
        }

        public int RemoveCompany(string contactId, string[] companyIds)
        {
            int result = 0;
            foreach (string id in companyIds)
            {
                result += RemoveCompany(contactId, id);
            }
            return result;
        }

        public string[] GetCompany(string contactId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldCategory, "Company"));
            parameters.Add(new KeyValuePair<string, object>(BaseContactTargetEntity.FieldContactId, contactId));
            return this.GetProperties(parameters, BaseContactTargetEntity.FieldReceiverId);
        }
    }
}
