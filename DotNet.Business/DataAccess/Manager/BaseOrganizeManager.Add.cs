//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager
    /// 组织机构
    ///
    /// 修改记录
    /// 
    ///		2016.02.29 版本：1.1 JiRiGaLa	添加方法优化。
    ///		2016.01.28 版本：1.0 JiRiGaLa	进行独立。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.29</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager //, IBaseOrganizeManager
    {
        public string Add(BaseOrganizeEntity entity)
        {
            string result = string.Empty;

            // 检查是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, entity.ParentId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, entity.FullName));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

            if (this.Exists(parameters))
            {
                // 名称已重复
                this.StatusCode = Status.ErrorNameExist.ToString();
                this.StatusMessage = Status.ErrorNameExist.ToDescription();
            }
            else
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

                if (entity.Code.Length > 0 && this.Exists(parameters))
                {
                    // 编号已重复
                    this.StatusCode = Status.ErrorCodeExist.ToString();
                    this.StatusMessage = Status.ErrorCodeExist.ToDescription();
                }
                else
                {
                    result = this.AddObject(entity);
                    // 运行成功
                    this.StatusCode = Status.OKAdd.ToString();
                    this.StatusMessage = Status.OKAdd.ToDescription();
                    AfterAdd(entity);
                }
            }

            return result;
        }

        public string AddByDetail(string parentId, string code, string fullName, string categoryCode, string outerPhone, string innerPhone, string fax, bool enabled)
        {
            BaseOrganizeEntity entity = new BaseOrganizeEntity();
            entity.ParentId = parentId;
            entity.Code = code;
            entity.FullName = fullName;
            entity.CategoryCode = categoryCode;
            entity.OuterPhone = outerPhone;
            entity.InnerPhone = innerPhone;
            entity.Fax = fax;
            entity.Enabled = enabled ? 1 : 0;
            return this.Add(entity);
        }
    }
}