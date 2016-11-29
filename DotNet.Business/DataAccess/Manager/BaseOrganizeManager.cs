//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager（程序OK）
    /// 组织机构、部门表
    ///
    /// 修改记录
    /// 
    ///     2015-09-10 版本：3.4 JiRiGaLa   缓存预热,强制重新缓存。
    ///     2015-05-06 版本：3.3 PanQiMin   添加根据城市Id获取外网展示网点的方法。
    ///     2015-04-09 版本：3.3 PanQiMin   添加记录修改日志方法。
    ///     2007.12.02 版本：3.3 JiRiGaLa   增加 SetObject 方法，优化主键。
    ///     2007.05.31 版本：3.2 JiRiGaLa   OKAdd，OKUpdate，OKDelete 状态进行改进整理。
    ///     2007.05.29 版本：3.1 JiRiGaLa   ErrorDeleted，ErrorChanged 状态进行改进整理。
    ///	    2007.05.29 版本：3.1 JiRiGaLa   BatchSave，ErrorDataRelated，force 进行改进整理。
    ///	    2007.05.29 版本：3.1 JiRiGaLa   StatusCode，StatusMessage 进行改进。
    ///	    2007.05.29 版本：3.1 JiRiGaLa   BatchSave 进行改进。
    ///		2007.04.18 版本：3.0 JiRiGaLa	重新整理主键。
    ///		2007.01.17 版本：2.0 JiRiGaLa	重新整理主键。
    ///		2006.02.06 版本：1.1 JiRiGaLa	重新调整主键的规范化。
    ///		2003.12.29 版本：1.0 JiRiGaLa	最后修改，改进成以后可以扩展到多种数据库的结构形式
    ///		2004.08.16 版本：1.0 JiRiGaLa	更新部分主键
    ///		2004.09.06 版本：1.0 JiRiGaLa	更新一些获得子部门，上级部门等的主键部分
    ///		2004.11.11 版本：1.0 JiRiGaLa	整理主键
    ///		2004.11.12 版本：1.0 JiRiGaLa	有些思想进行了改进。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.12.02</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager //, IBaseOrganizeManager
    {
        // 当前的锁
        // private static object locker = new Object();

        public static string GetNames(List<BaseOrganizeEntity> list)
        {
            string result = string.Empty;

            foreach (BaseOrganizeEntity entity in list)
            {
                result += "," + entity.FullName;
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(1);
            }

            return result;
        }

        /// <summary>
        /// 按编号获取实体
        /// </summary>
        /// <param name="code">编号</param>
        public BaseOrganizeEntity GetObjectByCode(string code)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCode, code));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            return BaseEntity.Create<BaseOrganizeEntity>(this.GetDataTable(parameters));
        }

        /// <summary>
        /// 按名称获取实体
        /// </summary>
        /// <param name="fullName">名称</param>
        public BaseOrganizeEntity GetObjectByName(string fullName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, fullName));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            return BaseEntity.Create<BaseOrganizeEntity>(this.ExecuteReader(parameters));
        }

        public DataTable GetInnerOrganize(string organizeId = null)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!String.IsNullOrEmpty(organizeId))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, organizeId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldIsInnerOrganize, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
        }

        public DataTable GetCompanyDT(string organizeId = null)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!String.IsNullOrEmpty(organizeId))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, organizeId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCategoryCode, "Company"));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
        }

        public DataTable GetFullNameDepartment(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                BaseOrganizeEntity subCompanyNameEntity = this.GetObject(dr[BaseOrganizeEntity.FieldParentId].ToString());
                dr[BaseOrganizeEntity.FieldFullName] = subCompanyNameEntity.FullName.ToString() + "--" + dr[BaseOrganizeEntity.FieldFullName].ToString();
                BaseOrganizeEntity companyEntity = this.GetObject(subCompanyNameEntity.ParentId);
                dr[BaseOrganizeEntity.FieldFullName] = companyEntity.FullName.ToString() + "--" + dr[BaseOrganizeEntity.FieldFullName].ToString();
            }
            return dt;
        }

        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseOrganizeEntity entity = new BaseOrganizeEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseOrganizeEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.DeleteObject(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseOrganizeEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        entity.GetFrom(dr);
                        result += this.UpdateObject(entity);
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    entity.GetFrom(dr);
                    result += this.AddObject(entity).Length > 0 ? 1 : 0;
                }
                if (dr.RowState == DataRowState.Unchanged)
                {
                    continue;
                }
                if (dr.RowState == DataRowState.Detached)
                {
                    continue;
                }
            }
            return result;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父级主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(string id, string parentId)
        {
            return this.SetProperty(id, new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, parentId));
        }

        /// <summary>
        /// 获取错误数据表
        /// </summary>
        /// <param name="parentId">上级网点</param>
        /// <returns>错误数据表</returns>
        public DataTable GetErrorDataTable(string parentId)
        {
            DataTable result = null;
            string sqlQuery = "  SELECT * "
                        + "        FROM " + BaseOrganizeEntity.TableName
                        + "       WHERE  (" + BaseOrganizeEntity.FieldProvinceId + " IS NULL OR "
                        + BaseOrganizeEntity.FieldCityId + " IS NULL OR "
                        + BaseOrganizeEntity.FieldDistrictId + " IS NULL ) AND " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                        + "             AND " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 ";
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                sqlQuery += "  START WITH Id = " + parentId + " "
                   + "  CONNECT BY PRIOR " + BaseOrganizeEntity.FieldId + " = " + BaseOrganizeEntity.FieldParentId;
            }
            sqlQuery += "    ORDER BY " + BaseOrganizeEntity.FieldSortCode;
            result = this.DbHelper.Fill(sqlQuery);
            result.TableName = BaseOrganizeEntity.TableName;
            return result;
        }

        /// <summary>
        /// 部门缓存表
        /// </summary>
        private DataTable DTOrganize = null;

        /// <summary>
        ///  部门名称前缀
        /// </summary>
        private string head = "|";

        /// <summary>
        /// 部门绑定表
        /// </summary>
        private DataTable organizeTable = new DataTable(BaseOrganizeEntity.TableName);

        #region public DataTable GetOrganizeTree(DataTable dtOrganize = null) 绑定下拉筐数据,组织机构树表
        /// <summary>
        /// 绑定下拉筐数据,组织机构树表
        /// </summary>
        /// <param name="dtOrganize">组织机构</param>
        /// <returns>组织机构树表</returns>
        public DataTable GetOrganizeTree(DataTable dtOrganize = null)
        {
            if (dtOrganize != null)
            {
                DTOrganize = dtOrganize;
            }
            // 初始化部门表
            if (organizeTable.Columns.Count == 0)
            {
                // 建立表的列，不能重复建立
                organizeTable.Columns.Add(new DataColumn(BaseOrganizeEntity.FieldId, System.Type.GetType("System.Int32")));
                organizeTable.Columns.Add(new DataColumn(BaseOrganizeEntity.FieldFullName, System.Type.GetType("System.String")));
            }
            if (DTOrganize == null)
            {
                // 绑定部门
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldIsInnerOrganize, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

                DTOrganize = this.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
            }
            // 查找子部门
            for (int i = 0; i < DTOrganize.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(DTOrganize.Rows[i][BaseOrganizeEntity.FieldParentId].ToString()))
                {
                    DataRow dr = organizeTable.NewRow();
                    dr[BaseOrganizeEntity.FieldId] = DTOrganize.Rows[i][BaseOrganizeEntity.FieldId].ToString();
                    dr[BaseOrganizeEntity.FieldFullName] =
                        DTOrganize.Rows[i][BaseOrganizeEntity.FieldFullName].ToString();
                    organizeTable.Rows.Add(dr);
                    this.GetSubOrganize(DTOrganize.Rows[i][BaseOrganizeEntity.FieldId].ToString());
                }
            }
            return organizeTable;
        }
        #endregion

        #region public void GetSubOrganize(string parentId)
        /// <summary>
        /// 获取子部门
        /// </summary>
        /// <param name="parentId">父节点主键</param>
        public void GetSubOrganize(string parentId)
        {
            head += "--";
            for (int i = 0; i < DTOrganize.Rows.Count; i++)
            {
                if (DTOrganize.Rows[i][BaseOrganizeEntity.FieldParentId].ToString().StartsWith(parentId))
                {
                    DataRow dr = organizeTable.NewRow();
                    dr[BaseOrganizeEntity.FieldId] = DTOrganize.Rows[i][BaseOrganizeEntity.FieldId].ToString();
                    dr[BaseOrganizeEntity.FieldFullName] = head + DTOrganize.Rows[i][BaseOrganizeEntity.FieldFullName].ToString();
                    organizeTable.Rows.Add(dr);
                    this.GetSubOrganize(DTOrganize.Rows[i][BaseOrganizeEntity.FieldId].ToString());
                }
            }
            // 子部门遍历完成后，退到上一级
            head = head.Substring(0, head.Length - 2);
        }
        #endregion

        /// <summary>
        /// 获取孩子节点属性
        /// </summary>
        /// <param name="parentId">上级主键</param>
        /// <param name="field">选择的字段</param>
        /// <returns>孩子属性数组</returns>
        public string[] GetChildrenProperties(string parentId, string field)
        {
            string[] result = null;
            string sqlQuery = "  SELECT " + field
                        + "        FROM " + BaseOrganizeEntity.TableName
                        + "       WHERE " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                        + "             AND " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                        + "  START WITH Id = " + parentId + " "
                        + "  CONNECT BY PRIOR " + BaseOrganizeEntity.FieldId + " = " + BaseOrganizeEntity.FieldParentId
                        + "    ORDER BY " + BaseOrganizeEntity.FieldSortCode;
            DataTable dt = this.DbHelper.Fill(sqlQuery);
            result = BaseBusinessLogic.FieldToArray(dt, field);
            return result;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="parentId">上级主键</param>
        /// <param name="childrens">包含树形子节点</param>
        /// <returns>数据表</returns>
        public DataTable GetDepartmentDT(string parentId = null, bool childrens = false)
        {
            DataTable result = null;
            if (!string.IsNullOrEmpty(parentId) && childrens)
            {
                string sqlQuery = string.Empty;
                sqlQuery = "     SELECT * "
                        + "        FROM " + BaseOrganizeEntity.TableName
                        + "       WHERE " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                        + "             AND " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                        + "             AND (" + BaseOrganizeEntity.FieldCategoryCode + "= 'Department' OR " + BaseOrganizeEntity.FieldCategoryCode + "= 'SubDepartment')"
                        + "  START WITH Id = " + parentId + " "
                        + "  CONNECT BY PRIOR " + BaseOrganizeEntity.FieldId + " = " + BaseOrganizeEntity.FieldParentId
                        + "    ORDER BY " + BaseOrganizeEntity.FieldSortCode;
                result = this.DbHelper.Fill(sqlQuery);
            }
            else
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                if (!String.IsNullOrEmpty(parentId))
                {
                    parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, parentId));
                }
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCategoryCode, "Department"));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
                result = this.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
            }
            result.TableName = BaseOrganizeEntity.TableName;
            return result;
        }

        /// <summary>
        /// 获取有权限的组织机构列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">操作权限</param>
        /// <returns>组织机构列表</returns>
        public List<BaseOrganizeEntity> GetListByPermission(string userId, string permissionCode = "Resource.ManagePermission")
        {
            List<BaseOrganizeEntity> result = null;

            // 先获取有权限的主键
            // string tableName = UserInfo.SystemCode + "PermissionScope";
            string tableName = "Base" + "PermissionScope";
            var manager = new BasePermissionScopeManager(dbHelper, UserInfo, tableName);
            string[] ids = manager.GetResourceScopeIds(UserInfo.SystemCode, userId, BaseAreaEntity.TableName, permissionCode);
            // 然后再获取地区表，获得所有的列表
            if (ids != null && ids.Length > 0)
            {
                result = this.GetList<BaseOrganizeEntity>(ids);
            }

            return result;
        }

        public int GetPinYin()
        {
            int result = 0;
            var list = this.GetList<BaseOrganizeEntity>();
            foreach (var entity in list)
            {
                if (string.IsNullOrEmpty(entity.QuickQuery))
                {
                    // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                    entity.QuickQuery = StringUtil.GetPinyin(entity.FullName).ToLower();
                }
                if (string.IsNullOrEmpty(entity.SimpleSpelling))
                {
                    // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                    entity.SimpleSpelling = StringUtil.GetSimpleSpelling(entity.FullName).ToLower();
                }
                result += this.UpdateObject(entity);
            }
            return result;
        }

        #region public DataTable Search(string searchValue, string parentId = null, bool isInnerOrganize = true, bool childrens = false) 搜索组织机构
        /// <summary>
        /// 搜索组织机构
        /// </summary>
        /// <param name="searchValue">查询内容</param>
        /// <param name="parentId">上级组织机构</param>
        /// <param name="isInnerOrganize">内部组织机构</param>
        /// <param name="childrens">包含子结点</param>
        /// <returns>数据表</returns>
        public DataTable Search(string searchValue, string parentId = null, bool? isInnerOrganize = null, bool? childrens = null)
        {
            string sqlQuery = string.Empty;
            List<IDbDataParameter> dbParameters;
            if (!childrens.HasValue || (childrens.HasValue && childrens.Value == false))
            {
                sqlQuery = "SELECT * "
                        + " FROM " + this.CurrentTableName
                        + " WHERE " + BaseOrganizeEntity.FieldDeletionStateCode + " =  0 ";
                if (isInnerOrganize.HasValue)
                {
                    string innerOrganize = isInnerOrganize.Value == true ? "1" : "0";
                    sqlQuery += string.Format(" AND {0} = {1}", BaseOrganizeEntity.FieldIsInnerOrganize, innerOrganize);
                }
                if (!string.IsNullOrEmpty(parentId))
                {
                    sqlQuery += string.Format(" AND {0} = {1}", BaseOrganizeEntity.FieldParentId, parentId);
                }

                dbParameters = new List<IDbDataParameter>();
                searchValue = searchValue.Trim().ToLower();
                if (!String.IsNullOrEmpty(searchValue))
                {
                    sqlQuery += string.Format(" AND ({0} LIKE {1}", BaseOrganizeEntity.FieldFullName, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    sqlQuery += string.Format(" OR {0} LIKE {1}", BaseOrganizeEntity.FieldSimpleSpelling, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    sqlQuery += string.Format(" OR {0} LIKE {1} )", BaseOrganizeEntity.FieldQuickQuery, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    if (searchValue.IndexOf("%") < 0)
                    {
                        searchValue = string.Format("%{0}%", searchValue);
                    }
                    dbParameters.Add(DbHelper.MakeParameter(BaseOrganizeEntity.FieldFullName, searchValue));
                }
                sqlQuery += " ORDER BY " + BaseOrganizeEntity.FieldSortCode;
                return DbHelper.Fill(sqlQuery, dbParameters.ToArray());
            }
            else
            {
                sqlQuery = "     SELECT * "
                         + "        FROM " + BaseOrganizeEntity.TableName
                         + "       WHERE " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                         + "             AND " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 ";

                dbParameters = new List<IDbDataParameter>();
                searchValue = searchValue.Trim();
                if (!String.IsNullOrEmpty(searchValue))
                {
                    sqlQuery += string.Format(" AND ({0} LIKE {1}", BaseOrganizeEntity.FieldFullName, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    sqlQuery += string.Format(" OR {0} LIKE {1}", BaseOrganizeEntity.FieldSimpleSpelling, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    sqlQuery += string.Format(" OR {0} LIKE {1} )", BaseOrganizeEntity.FieldQuickQuery, DbHelper.GetParameter(BaseOrganizeEntity.FieldFullName));
                    if (searchValue.IndexOf("%") < 0)
                    {
                        searchValue = string.Format("%{0}%", searchValue);
                    }
                    dbParameters.Add(DbHelper.MakeParameter(BaseOrganizeEntity.FieldFullName, searchValue));
                }

                if (!string.IsNullOrEmpty(parentId))
                {
                    sqlQuery += "  START WITH Id = " + parentId + " "
                             + "  CONNECT BY PRIOR " + BaseOrganizeEntity.FieldId + " = " + BaseOrganizeEntity.FieldParentId;
                }

                sqlQuery += " ORDER BY " + BaseOrganizeEntity.FieldSortCode;
                return this.DbHelper.Fill(sqlQuery, dbParameters.ToArray());
            }
        }
        #endregion

        #region 根据城市Id获取外网展示网点 public DataTable GetWebOrganizeByDistrictId(string districtId)
        /// <summary>
        /// 根据城市Id获取外网展示网点
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public DataTable GetWebOrganizeList(string cityId)
        {
            string commandText = @"SELECT o.Id
                                         ,o.Code
                                         ,o.OuterPhone
                                         ,o.Manager
                                         ,b.WebEnabled AS Enabled
                                         ,b.WebsiteName
                                         ,c.Longitude
                                         ,c.Latitude
                                   FROM BASEORGANIZE o 
                              LEFT JOIN BASEORGANIZE_EXPRESS b ON o.id = b.id 
                              LEFT JOIN BaseArea a ON o.ProvinceId = a.id 
                              LEFT JOIN BASEORGANIZEGIS c ON o.id = c.id
                                  WHERE c.WebShowEnable = 1 
                                        AND o.DeletionStateCode = 0 
                                        AND o.CityId=" + dbHelper.GetParameter("CityId");
            var dbParameters = new List<KeyValuePair<string, object>>(); //查询条件参数集合
            dbParameters.Add(new KeyValuePair<string, object>("CityId", cityId));
            return dbHelper.Fill(commandText, dbHelper.MakeParameters(dbParameters));
        }
        #endregion

        #region 根据网点Id获取外网展示网点 public DataTable GetWebOrganizeById(string Id)

        /// <summary>
        /// 根据网点Id获取外网展示网点
        /// </summary>
        /// <param name="id">网点Id</param>
        /// <param name="code">网点编号</param>
        /// <returns></returns>
        public DataTable GetWebOrganize(string id, string code)
        {
            // id可以是多个网点Id
            var ids = DbLogic.SqlSafe(id).Split(',');
            var codes = DbLogic.SqlSafe(code).Split(',');
            var dbParameters = new List<KeyValuePair<string, object>>(); //查询条件参数集合
            var listCondition = new List<string>();

            // 网点Id查询条件
            string idCondition = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                idCondition += string.Format(" o.ID IN (");
                for (var i = 0; i < ids.Length; i++)
                {
                    if (i == 0)
                    {
                        idCondition += dbHelper.GetParameter("I" + i);
                    }
                    else
                    {
                        idCondition += ",";
                        idCondition += dbHelper.GetParameter("I" + i);
                    }
                    dbParameters.Add(new KeyValuePair<string, object>("I" + i, ids[i]));
                }
                idCondition += string.Format(")");
                listCondition.Add(idCondition);
            }

            // 网点编号查询条件
            string codeCondition = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                codeCondition += string.Format(" o.Code IN (");
                for (var i = 0; i < codes.Length; i++)
                {
                    if (i == 0)
                    {
                        codeCondition += dbHelper.GetParameter("C" + i);
                    }
                    else
                    {
                        codeCondition += ",";
                        codeCondition += dbHelper.GetParameter("C" + i);
                    }
                    dbParameters.Add(new KeyValuePair<string, object>("C" + i, codes[i]));
                }
                codeCondition += string.Format(")");
                listCondition.Add(codeCondition);
            }
            string conditions = string.Empty;
            if (listCondition.Count > 0)
            {
                // 构建查询条件
                conditions = string.Join(" AND ", listCondition);
            }
            string commandText = @"SELECT o.Id
                                         ,o.Code
                                         ,o.ParentName
                                         ,o.CategoryCode
                                         ,o.OuterPhone
                                         ,o.Manager
                                         ,o.ProvinceId
                                         ,o.Province
                                         ,a.FullName ProvinceFullName
                                         ,o.City
                                         ,o.District
                                         ,o.Area
                                         ,o.Fax
                                         ,o.Description
                                         ,o.MasterMobile
                                         ,o.SortCode
                                         ,b.WebEnabled AS Enabled
                                         ,o.SendFee
                                         ,o.LevelTwoTransferFee
                                         ,o.FullName
                                         ,o.Modifiedon
                                         ,b.WebsiteName
                                         ,b.Allow_Topayment
                                         ,b.Not_Dispatch_Range
                                         ,b.Dispatch_Time_Limit
                                         ,b.Allow_Agent_Money
                                         ,b.Public_Remark
                                         ,b.Private_Remark
                                         ,b.Dispatch_Range
                                         ,c.Longitude
                                         ,c.Latitude
                                         ,o.Address
                                   FROM BASEORGANIZE o 
                              LEFT JOIN BASEORGANIZE_EXPRESS b ON o.id = b.id 
                              LEFT JOIN BaseArea a ON o.ProvinceId = a.id 
                              LEFT JOIN BASEORGANIZEGIS c ON o.id = c.id
                                  WHERE c.WebShowEnable = 1 
                                        AND o.DeletionStateCode = 0 AND "
                                        + conditions;
            return dbHelper.Fill(commandText, dbHelper.MakeParameters(dbParameters));
        }
        #endregion

        /// <summary>
        /// 快速查找上级用
        /// 2016-01-06 吉日嘎拉
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>上级主键</returns>
        public static string GetParentIdByCache(string id)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(id))
            {
                BaseOrganizeEntity entity = GetObjectByCache(id);
                if (entity != null)
                {
                    result = entity.ParentId;
                    // 2016-01-06 吉日嘎拉 这里防止死循环的处理，跳出循环
                    if (id.Equals(result))
                    {
                        result = string.Empty;
                    }
                }
            }

            return result;
        }

        public static string GetIdByNameByCache(string fullName)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(fullName))
            {
                BaseOrganizeEntity entity = GetObjectByNameByCache(fullName);
                if (entity != null)
                {
                    result = entity.Id;
                }
            }
            return result;
        }

        public static string GetNameByCodeByCache(string code)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                BaseOrganizeEntity entity = GetObjectByCodeByCache(code);
                if (entity != null)
                {
                    result = entity.FullName;
                }
            }
            return result;
        }

        public static string GetIdByCodeByCache(string code)
        {
            string result = string.Empty;

            BaseOrganizeEntity entity = GetObjectByCodeByCache(code);
            if (entity != null)
            {
                result = entity.Id.ToString();
            }

            return result;
        }

        #region public static string GetNameByCache(string id) 通过编号获取选项的显示内容
        /// <summary>
        /// 通过编号获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetNameByCache(string id)
        {
            string result = string.Empty;

            BaseOrganizeEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.FullName;
            }

            return result;
        }
        #endregion

        public static string GetCodeByCache(string id)
        {
            string result = string.Empty;

            BaseOrganizeEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.Code;
            }

            return result;
        }

        /// <summary>
        /// 重新设置缓存（重新强制设置缓存）可以提供外部调用的
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>用户信息</returns>
        public static BaseOrganizeEntity SetCache(string id)
        {
            BaseOrganizeEntity result = null;

            BaseOrganizeManager manager = new BaseOrganizeManager();
            result = manager.GetObject(id);

            if (result != null)
            {
                SetCache(result);
            }

            return result;
        }

        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheating()
        {
            int result = 0;

            // 把所有的组织机构都缓存起来的代码
            BaseOrganizeManager manager = new BaseOrganizeManager();
            using (IDataReader dataReader = manager.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    BaseOrganizeEntity entity = BaseEntity.Create<BaseOrganizeEntity>(dataReader, false);
                    if (entity != null)
                    {
                        BaseOrganizeManager.SetCache(entity);
                        string[] systemCodes = BaseSystemManager.GetSystemCodes();
                        for (int i = 0; i < systemCodes.Length; i++)
                        {
                            // 重置权限缓存数据
                            BaseOrganizePermissionManager.ResetPermissionByCache(systemCodes[i], entity.Id);
                        }
                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + entity.FullName);
                    }
                }
                dataReader.Close();
            }

            return result;
        }

        /// <summary>
        /// 根据SQL从数据获取数据,将DataTable中的数据保存到指定文件夹下保存成.csv文件格式
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void ExportToExcel(string fileName)
        {
            string commandText = string.Format(@"SELECT {0} 网点编号
                                                        ,CASE {1}  
                                                            WHEN 1 THEN '启用'  
                                                            WHEN 0 THEN '停用'  
                                                            ELSE '停用' END AS 启用
                                                        ,{2} 网点名称
                                                        ,{3} 所属网点
                                                        ,{4} 片区
                                                        ,{5} 类型
                                                        ,{6} 加盟方式
                                                        ,{7} 财务中心
                                                        ,{8} 结算中心 
                                                        ,{9} 一级网点 
                                                        ,{10} 二级中转费结算中心 
                                                        ,{11} 省级网点 
                                                        ,{12} 面单结算网点 
                                                        ,{13} 统计网点 
                                                        ,{14} 有偿派费费率 
                                                        ,{15} 二级中转费费率 
                                                        ,{16} 抛重费率 
                                                        ,{17} 省份 
                                                        ,{18} 城市
                                                        ,{19} 区县 
                                                    FROM {20} 
                                                   WHERE {21} = 0
                                                ORDER BY {22}
                                                        ,{23}
                                                        ,{24} "
                , BaseOrganizeEntity.FieldCode
                , BaseOrganizeEntity.FieldEnabled
                , BaseOrganizeEntity.FieldFullName
                , BaseOrganizeEntity.FieldParentName
                , BaseOrganizeEntity.FieldArea
                , BaseOrganizeEntity.FieldCategoryCode
                , BaseOrganizeEntity.FieldJoiningMethods
                , BaseOrganizeEntity.FieldFinancialCenter
                , BaseOrganizeEntity.FieldCostCenter
                , BaseOrganizeEntity.FieldCompanyName

                , BaseOrganizeEntity.FieldLevelTwoTransferCenter
                , BaseOrganizeEntity.FieldProvinceSite
                , BaseOrganizeEntity.FieldBillBalanceSite
                , BaseOrganizeEntity.FieldStatisticalName
                , BaseOrganizeEntity.FieldSendFee
                , BaseOrganizeEntity.FieldLevelTwoTransferFee
                , BaseOrganizeEntity.FieldWeightRatio

                , BaseOrganizeEntity.FieldProvince
                , BaseOrganizeEntity.FieldCity
                , BaseOrganizeEntity.FieldDistrict
                , BaseOrganizeEntity.TableName
                , BaseOrganizeEntity.FieldDeletionStateCode
                , BaseOrganizeEntity.FieldProvince
                , BaseOrganizeEntity.FieldCity
                , BaseOrganizeEntity.FieldDistrict);

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
            {
                DataTable organizeDataTable = new DataTable();
                dbHelper.Fill(organizeDataTable, commandText);
                BaseExportExcel.ExportXlsByNPOI(organizeDataTable, null, fileName);
            }
        }

        public static BaseOrganizeEntity GetObjectByCache(string id, bool fefreshCache = false)
        {
            BaseOrganizeEntity result = null;

            if (!string.IsNullOrEmpty(id))
            {
                string cacheKey = "O:";
                cacheKey = cacheKey + id;

                if (!fefreshCache)
                {
                    result = GetCacheByKey(cacheKey);
                }

                if (result == null)
                {
                    BaseOrganizeManager manager = new DotNet.Business.BaseOrganizeManager();
                    result = manager.GetObject(id);
                    // 若是空的不用缓存，继续读取实体
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseOrganizeEntity GetObjectByCache(IRedisClient redisClient, string id)
        {
            BaseOrganizeEntity result = null;

            if (!string.IsNullOrEmpty(id))
            {
                //string cacheKey = "Organize";
                string cacheKey = "O:";
            
                cacheKey = cacheKey + id;
                result = GetCacheByKey(redisClient, cacheKey);
                if (result == null)
                {
                    BaseOrganizeManager manager = new DotNet.Business.BaseOrganizeManager();
                    result = manager.GetObject(id);
                    // 若是空的不用缓存，继续读取实体
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

            return result;
        }
    }
}