//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseAreaManager 
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    ///
    ///     2015-06-09 版本：1.0 PanQiMin  添加获取所有城市包含省份的方法
    ///     2015.04.09 版本：1.0 PanQiMin  添加记录修改日志方法。
    ///		2014.02.11 版本：1.0 JiRiGaLa  表中添加是否可删除，可修改字段。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.02.11</date>
    /// </author>
    /// </summary>
    public partial class BaseAreaManager : BaseManager
    {
        public bool HasProvincePermission(string userId, string permissionId, string provinceId)
        {
            string[] areaIds = GetAreas(userId, permissionId);
            return StringUtil.Exists(areaIds, provinceId);
        }

        public bool HasCityPermission(string userId, string permissionId, string cityId)
        {
            string[] areaIds = GetAreas(userId, permissionId);
            return StringUtil.Exists(areaIds, cityId);
        }

        public string[] GetAreas(string userId, string permissionId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            return permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
        }

        /// <summary>
        /// 按区域分割省、市、县、街道
        /// </summary>
        /// <param name="areaIds">区域主键</param>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <param name="district">县</param>
        /// <param name="street">街道</param>
        public void SplitArea(string[] areaIds, out string[] province, out string[] city, out string[] district, out string[] street)
        {
            List<string> list = new List<string>();
            province = null;
            // 获取省，110000
            list = new List<string>();
            for (int i = 0; i < areaIds.Length; i++)
            {
                if (areaIds[i] != null && areaIds[i].EndsWith("0000"))
                {
                    list.Add(areaIds[i]);
                }
            }
            province = list.ToArray();

            city = null;
            // 获取市，330300
            list = new List<string>();
            for (int i = 0; i < areaIds.Length; i++)
            {
                if (areaIds[i] == null)
                {
                    continue;
                }
                if (areaIds[i] != null && !areaIds[i].EndsWith("0000"))
                {
                    continue;
                }
                if (areaIds[i] != null && areaIds[i].Length == 9)
                {
                    continue;
                }
                // 县没能过滤，因为有省直属的县
            }
            city = list.ToArray();

            // 获得县，131024
            district = null;
            list = new List<string>();
            for (int i = 0; i < areaIds.Length; i++)
            {
                if (areaIds[i] != null && !areaIds[i].EndsWith("00"))
                {
                    list.Add(areaIds[i]);
                }
            }
            district = list.ToArray();

            // 获得街道， 110112117
            street = null;
            list = new List<string>();
            for (int i = 0; i < areaIds.Length; i++)
            {
                if (areaIds[i] != null && areaIds[i].Length == 9)
                {
                    list.Add(areaIds[i]);
                }
            }
            street = list.ToArray();
        }

        /// <summary>
        /// 获取用户有权限的区域的管理公司数组
        /// </summary>
        /// <param name="result">数据权限主键</param>
        /// <returns>管理公司数组</returns>
        public string[] GetUserManageCompanyIds(string userId, string permissionId)
        {
            string[] result = null;

            // 用户有权限的省？获取省的管理公司？
            // 用户有权限的市？市的管理公司？
            // 用户有权限的县？县的管理公司？
            // 用户有权限的街道？街道的管理公司？

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            string[] areaIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
            if (areaIds != null && areaIds.Length > 0)
            {
                string sqlQuery = string.Empty;
                sqlQuery = "   SELECT DISTINCT(" + BaseAreaEntity.FieldManageCompanyId + ") "
                          + "              FROM " + this.CurrentTableName
                          + "             WHERE " + BaseAreaEntity.FieldLayer + " < 7 AND " + BaseAreaEntity.FieldManageCompanyId + " IS NOT NULL "
                          + "        START WITH " + BaseAreaEntity.FieldId + " IN (" + string.Join(",", areaIds) + ")"
                          + "  CONNECT BY PRIOR " + BaseAreaEntity.FieldId + " = " + BaseAreaEntity.FieldParentId;
                DataTable dt = dbHelper.Fill(sqlQuery);
                result = BaseBusinessLogic.FieldToArray(dt, BaseAreaEntity.FieldManageCompanyId);
            }

            return result;
        }

        /// <summary>
        /// 获取管理网点列表
        /// </summary>
        /// <returns>返回序列化</returns>
        public List<BaseOrganizeEntity> GetUserManageCompanes(string userId, string permissionId)
        {
            List<BaseOrganizeEntity> result = null;
            string[] manageCompanyIds = GetUserManageCompanyIds(userId, permissionId);
            if (manageCompanyIds != null && manageCompanyIds.Length < 1)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldId, manageCompanyIds));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
                BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
                result = organizeManager.GetList<BaseOrganizeEntity>(parameters);
            }
            return result;
        }

        /// <summary>
        /// 获取用户的管理网点
        /// </summary>
        /// <param name="result">数据权限主键</param>
        /// <returns>管理网点数组</returns>
        public string[] GetUserCompanyIds(string userId, string permissionId)
        {
            string[] result = null;

            // 用户有权限的省？获取省的网点？
            // 用户有权限的市？市的网点？
            // 用户有权限的县？县的网点？
            // 用户有权限的街道？街道的网点？
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            string[] areaIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);

            // 按区域分割省、市、县、街道
            string[] province = null;
            string[] city = null;
            string[] district = null;
            string[] street = null;
            SplitArea(areaIds, out province, out city, out district, out street);

            string[] areaCompanyIds = null;
            if (areaIds != null && areaIds.Length > 0)
            {
                string commandText = " SELECT " + BaseOrganizeEntity.FieldId
                                    + "  FROM " + BaseOrganizeEntity.TableName
                                    + " WHERE " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                                    + "       AND " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                                    + "       AND (";
                if (province != null && province.Length > 0)
                {
                    commandText += BaseOrganizeEntity.FieldProvinceId + " IN (" + BaseBusinessLogic.ObjectsToList(province, "'") + ")";
                }
                if (city != null && city.Length > 0)
                {
                    if (province != null && province.Length > 0)
                    {
                        commandText += "  OR ";
                    }
                    commandText += BaseOrganizeEntity.FieldCityId + " IN (" + BaseBusinessLogic.ObjectsToList(city, "'") + ")";
                }
                if (district != null && district.Length > 0)
                {
                    if ((province != null && province.Length > 0) || (city != null && city.Length > 0))
                    {
                        commandText += "  OR ";
                    }
                    commandText += BaseOrganizeEntity.FieldDistrictId + " IN (" + BaseBusinessLogic.ObjectsToList(district, "'") + ")";
                }
                if (street != null && street.Length > 0)
                {
                    if ((province != null && province.Length > 0) || (city != null && city.Length > 0) || (district != null && district.Length > 0))
                    {
                        commandText += "  OR ";
                    }
                    commandText += BaseOrganizeEntity.FieldStreetId + " IN (" + BaseBusinessLogic.ObjectsToList(areaIds, "'") + ")";
                }
                commandText += ")";

                BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                DataTable dt = organizeManager.Fill(commandText);
                areaCompanyIds = BaseBusinessLogic.FieldToArray(dt, BaseOrganizeEntity.FieldId);
            }

            // 用户直接有权限的网点
            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));
            string[] companyIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);

            result = StringUtil.Concat(companyIds, areaCompanyIds);
            return result;
        }

        /// <summary>
        /// 获取用户能显示的省？查看的范围
        /// 由于底层数据可以看，所以需要能选上层的省才可以
        /// </summary>
        /// <returns>省列表</returns>
        public List<BaseAreaEntity> GetUserProvince(string userId, string permissionId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            string[] areaIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
            for (int i = 0; i < areaIds.Length; i++)
            {
                areaIds[i] = areaIds[i].Substring(0, 2) + "0000";
            }

            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, null));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldId, areaIds));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        /// <summary>
        /// 获取用户能显示的市？查看的范围
        /// 由于底层数据可以市，所以需要能选上层的省才可以
        /// </summary>
        /// <returns>市列表</returns>
        public List<BaseAreaEntity> GetUserCity(string userId, string provinceId, string permissionId)
        {
            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            provinceId = SecretUtil.SqlSafe(provinceId);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            string[] areaIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
            for (int i = 0; i < areaIds.Length; i++)
            {
                areaIds[i] = areaIds[i].Substring(0, 4) + "00";
            }

            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, provinceId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldId, areaIds));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        /// <summary>
        /// 获取用户能显示的县？查看的范围
        /// 由于底层数据可以县，所以需要能选上层的省才可以
        /// </summary>
        /// <returns>县列表</returns>
        public List<BaseAreaEntity> GetUserDistrict(string userId, string cityId, string permissionId)
        {
            string tableName = this.UserInfo.SystemCode + "PermissionScope";
            cityId = SecretUtil.SqlSafe(cityId);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo, tableName);
            string[] areaIds = permissionScopeManager.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);

            string where = BaseAreaEntity.FieldId + " IN (" + BaseBusinessLogic.ObjectsToList(areaIds) + ") AND ((ParentId = '" + cityId + "' AND Layer = 6) OR (Id = '" + cityId + "' AND Layer = 6)) AND Enabled = 1 AND DeletionStateCode = 0 ";
            return this.GetList<BaseAreaEntity>(where);
        }

        public string Add(BaseAreaEntity entity, out string statusCode)
        {
            string result = string.Empty;
            // 检查是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, entity.ParentId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, entity.FullName));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));

            //注意Access 的时候，类型不匹配，会出错故此将 Id 传入
            if (BaseSystemInfo.UserCenterDbType == CurrentDbType.Access)
            {
                if (this.Exists(parameters, entity.Id))
                {
                    // 名称已重复
                    statusCode = Status.ErrorNameExist.ToString();
                }
                else
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldCode, entity.Code));
                    parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
                    if (entity.Code.Length > 0 && this.Exists(parameters))
                    {
                        // 编号已重复
                        statusCode = Status.ErrorCodeExist.ToString();
                    }
                    else
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
                        result = this.AddObject(entity);
                        // 运行成功
                        statusCode = Status.OKAdd.ToString();
                    }
                }
            }
            else if (this.Exists(parameters))
            {
                // 名称已重复
                statusCode = Status.ErrorNameExist.ToString();
            }
            else
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
                if (entity.Code.Length > 0 && this.Exists(parameters))
                {
                    // 编号已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    result = this.AddObject(entity);
                    // 运行成功
                    statusCode = Status.OKAdd.ToString();
                }
            }
            return result;
        }

        public List<BaseAreaEntity> GetListByPermission(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool childrens = true)
        {
            List<BaseAreaEntity> result = null;

            // 先获取有权限的主键
            string tableName = UserInfo.SystemCode + "PermissionScope";
            // string tableName = "Base" + "PermissionScope";
            var manager = new BasePermissionScopeManager(dbHelper, UserInfo, tableName);
            string[] ids = manager.GetTreeResourceScopeIds(systemCode, userId, BaseAreaEntity.TableName, permissionCode, childrens);
            // 然后再获取地区表，获得所有的列表
            if (ids != null && ids.Length > 0)
            {
                result = this.GetList<BaseAreaEntity>(ids).OrderBy(t => t.SortCode).ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取有权限的省份列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">操作权限编号</param>
        /// <param name="childrens">是否包含子节点</param>
        /// <returns>地区列表</returns>
        public List<BaseAreaEntity> GetProvinceListByPermission(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool childrens = true)
        {
            List<BaseAreaEntity> result = GetListByPermission(systemCode, userId, permissionCode, childrens);
            result = result.Where<BaseAreaEntity>(t => t.Province != null && t.City == null && t.District == null).ToList();
            return result;
        }

        /// <summary>
        /// 获取有权限的城市列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">操作权限编号</param>
        /// <param name="childrens">是否包含子节点</param>
        /// <returns>地区列表</returns>
        public List<BaseAreaEntity> GetCityListByPermission(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool childrens = true)
        {
            List<BaseAreaEntity> result = GetListByPermission(systemCode, userId, permissionCode, childrens);
            result = result.Where<BaseAreaEntity>(t => t.City != null && t.District == null).ToList();
            return result;
        }

        /// <summary>
        /// 获取有权限的县区列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">操作权限编号</param>
        /// <param name="childrens">是否包含子节点</param>
        /// <returns>地区列表</returns>
        public List<BaseAreaEntity> GetDistrictListByPermission(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool childrens = true)
        {
            List<BaseAreaEntity> result = GetListByPermission(systemCode, userId, permissionCode, childrens);
            result = result.Where<BaseAreaEntity>(t => t.District != null).ToList();
            return result;
        }

        public List<BaseAreaEntity> GetListByParent(string parentId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, parentId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        public List<BaseAreaEntity> GetProvince()
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            // 省份的下拉框，层级应该是4，执行效率会高
            // parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, null));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldLayer, 4));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        public List<BaseAreaEntity> GetCity(string provinceId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, provinceId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        public List<BaseAreaEntity> GetDistrict(string cityId)
        {
            cityId = SecretUtil.SqlSafe(cityId);
            // string commandText = "SELECT * FROM BaseArea WHERE ((ParentId = '" + cityId + "' AND Layer = 3) OR (Id = '" + cityId + "' AND Layer = 3)) AND Enabled = 1 AND DeletionStateCode = 0 ORDER BY SortCode";
            // string where = "((ParentId = '" + cityId + "' AND Layer = 3) OR (Id = '" + cityId + "' AND Layer = 3)) AND Enabled = 1 AND DeletionStateCode = 0";
            // return this.GetList<BaseAreaEntity>(where);

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldParentId, cityId));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseItemDetailsEntity.FieldSortCode);
        }

        public List<BaseAreaEntity> GetStreet(string districtId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, districtId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetList<BaseAreaEntity>(parameters, BaseAreaEntity.FieldSortCode);
        }

        /// <summary>
        /// 获取所有市
        /// </summary>
        /// <returns>城市数据表</returns>
        public DataTable GetAllCity()
        {
            /*
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldLayer, 5));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters);
            */

            string commandText = " SELECT * FROM " +
                                 " (SELECT * FROM BASEAREA WHERE PARENTID IN (SELECT ID FROM BASEAREA WHERE PARENTID IS NULL) " +
                                 " UNION " +
                                 " SELECT * FROM BASEAREA WHERE PARENTID IS NULL) " +
                                 " WHERE DELETIONSTATECODE = 0 " +
                                   " AND ENABLED = 1 ";
            return dbHelper.Fill(commandText);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父级主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(string id, string parentId)
        {
            return this.SetProperty(id, new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, parentId));
        }

        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseAreaEntity entity = new BaseAreaEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseAreaEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.DeleteObject(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseAreaEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        entity.GetFrom(dr);
                        result += this.UpdateObject(entity);
                    }
                }
                // 添加状态, 远程接口调用序列化时都会变成添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    entity.GetFrom(dr);
                    if (!string.IsNullOrEmpty(entity.Id))
                    {
                        if (this.UpdateObject(entity) > 0)
                        {
                            result++;
                        }
                        else
                        {
                            result += this.AddObject(entity).Length > 0 ? 1 : 0;
                        }
                    }
                    else
                    {
                        result += this.AddObject(entity).Length > 0 ? 1 : 0;
                    }
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
        /// 查询列表
        /// </summary>
        /// <param name="search">查询</param>
        /// <returns>数据表</returns>
        public DataTable Search(string searchValue)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * "
                    + "   FROM " + this.CurrentTableName
                    + "  WHERE " + BaseAreaEntity.FieldDeletionStateCode + " = 0 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

            searchValue = searchValue.Trim();
            if (!String.IsNullOrEmpty(searchValue))
            {
                // 六、这里是进行支持多种数据库的参数化查询
                sqlQuery += string.Format(" AND ({0} LIKE {1} ", BaseAreaEntity.FieldFullName, DbHelper.GetParameter(BaseAreaEntity.FieldFullName));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaEntity.FieldShortName, DbHelper.GetParameter(BaseAreaEntity.FieldShortName));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaEntity.FieldMark, DbHelper.GetParameter(BaseAreaEntity.FieldMark));
                sqlQuery += string.Format(" OR {0} LIKE {1}) ", BaseAreaEntity.FieldDescription, DbHelper.GetParameter(BaseAreaEntity.FieldDescription));

                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = string.Format("%{0}%", searchValue);
                }

                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldFullName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldShortName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldMark, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldDescription, searchValue));
            }
            sqlQuery += " ORDER BY " + BaseAreaEntity.FieldSortCode + " DESC ";

            return DbHelper.Fill(sqlQuery, dbParameters.ToArray());
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="search">查询</param>
        /// <returns>数据表</returns>
        public List<BaseAreaEntity> SearchByList(string searchValue)
        {
            List<BaseAreaEntity> result = null;

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * "
                    + "   FROM " + this.CurrentTableName
                    + "  WHERE " + BaseAreaEntity.FieldDeletionStateCode + " = 0 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

            searchValue = searchValue.Trim();
            if (!String.IsNullOrEmpty(searchValue))
            {
                // 六、这里是进行支持多种数据库的参数化查询
                sqlQuery += string.Format(" AND ({0} LIKE {1} ", BaseAreaEntity.FieldFullName, DbHelper.GetParameter(BaseAreaEntity.FieldFullName));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaEntity.FieldShortName, DbHelper.GetParameter(BaseAreaEntity.FieldShortName));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaEntity.FieldMark, DbHelper.GetParameter(BaseAreaEntity.FieldMark));
                sqlQuery += string.Format(" OR {0} LIKE {1}) ", BaseAreaEntity.FieldDescription, DbHelper.GetParameter(BaseAreaEntity.FieldDescription));

                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = string.Format("%{0}%", searchValue);
                }

                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldFullName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldShortName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldMark, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaEntity.FieldDescription, searchValue));
            }
            sqlQuery += " ORDER BY " + BaseAreaEntity.FieldSortCode + " DESC ";

            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                result = this.GetList<BaseAreaEntity>(dataReader);
            }

            return result;
        }

        public int GetPinYin()
        {
            int result = 0;
            var list = this.GetList<BaseAreaEntity>();
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

        /// <summary>
        /// 根据SQL从数据获取数据,将DataTable中的数据保存到指定文件夹下保存成.csv文件格式
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void ExportToCsv(string fileName)
        {
            string commandText = string.Format(@"SELECT {0} 全称
                                                        ,{1} 省
                                                        ,{2} 市
                                                        ,{3} 县
                                                        ,{4} 大头笔
                                                        ,{5} 延迟天数
                                                        ,{6} 机打大头笔
                                                        ,{7} 允许代收 
                                                        ,{8} 允许到付 
                                                        ,{9} 管理网点 
                                                        ,{10} 最大代收款 
                                                        ,{11} 最大到付款 
                                                        ,{12} 网络订单 
                                                        ,{13} 开通业务 
                                                        ,{14} 超区 
                                                        ,{15} 揽收 
                                                        ,{16} 发件 
                                                        ,{17} 全境派送
                                                        ,{18} 层级 
                                                    FROM {19} 
                                                   WHERE {20} = 1 
                                                     AND {21} = 0
                                                     AND {22} != 0 
                                                ORDER BY {23}
                                                        ,{24}
                                                        ,{25} "
                , BaseAreaEntity.FieldFullName
                , BaseAreaEntity.FieldProvince
                , BaseAreaEntity.FieldCity
                , BaseAreaEntity.FieldDistrict
                , BaseAreaEntity.FieldMark
                , BaseAreaEntity.FieldDelayDay
                , BaseAreaEntity.FieldPrintMark
                , BaseAreaEntity.FieldAllowGoodsPay
                , BaseAreaEntity.FieldAllowToPay
                , BaseAreaEntity.FieldManageCompany
                , BaseAreaEntity.FieldMaxGoodsPayment
                , BaseAreaEntity.FieldMaxToPayment
                , BaseAreaEntity.FieldNetworkOrders
                , BaseAreaEntity.FieldOpening
                , BaseAreaEntity.FieldOutOfRange
                , BaseAreaEntity.FieldReceive
                , BaseAreaEntity.FieldSend
                , BaseAreaEntity.FieldWhole
                , BaseAreaEntity.FieldLayer
                , BaseAreaEntity.TableName
                , BaseAreaEntity.FieldEnabled
                , BaseAreaEntity.FieldDeletionStateCode
                , BaseAreaEntity.FieldLayer
                , BaseAreaEntity.FieldProvince
                , BaseAreaEntity.FieldCity
                , BaseAreaEntity.FieldDistrict);

            IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            dbHelper.Open();
            using (IDataReader dataReader = DbHelper.ExecuteReader(commandText))
            {
                BaseExportCSV.ExportCSV(dataReader, fileName);
            }
            dbHelper.Close();
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseAreaEntity GetObjectByCache(string id)
        {
            BaseAreaEntity result = null;
            string cacheKey = "Area:";
            if (!string.IsNullOrEmpty(id))
            {
                cacheKey = cacheKey + id;
            }

            result = GetCache(cacheKey);
            if (result == null || string.IsNullOrWhiteSpace(result.Id))
            {
                BaseAreaManager manager = new DotNet.Business.BaseAreaManager();
                result = manager.GetObject(id);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetCache(result);
                }
            }

            return result;
        }

        public static string GetNameByByCache(string id)
        {
            string result = string.Empty;

            BaseAreaEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.FullName;
            }

            return result;
        }

        public static List<BaseAreaEntity> GetListByParentByCache(string parentId, bool fefreshCache = false)
        {
            List<BaseAreaEntity> result = null;

            string key = "AreaList:";
            if (!string.IsNullOrEmpty(parentId))
            {
                key = key + parentId;
            }

            if (!fefreshCache)
            {
                result = GetListCache(key);
            }

            if (result == null)
            {
                BaseAreaManager manager = new DotNet.Business.BaseAreaManager();
                result = manager.GetListByParent(parentId);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetListCache(key, result);
                }
            }

            return result;
        }

        public static List<BaseAreaEntity> GetProvinceByCache()
        {
            List<BaseAreaEntity> result = null;

            string key = "AreaProvince:";

            result = GetListCache(key);
            if (result == null)
            {
                BaseAreaManager manager = new DotNet.Business.BaseAreaManager();
                result = manager.GetProvince();
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetListCache(key, result);
                }
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
            BaseAreaManager manager = new BaseAreaManager();
            using (IDataReader dataReader = manager.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    BaseAreaEntity entity = BaseEntity.Create<BaseAreaEntity>(dataReader, false);
                    if (entity != null && entity.Layer < 7)
                    {
                        BaseAreaManager.SetCache(entity);
                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + entity.FullName);
                        // 把列表缓存起来
                        BaseAreaManager.GetListByParentByCache(entity.Id, true);
                        System.Console.WriteLine(result.ToString() + " : " + entity.Id + " " + entity.FullName + " List");
                    }
                }
                dataReader.Close();
            }

            return result;
        }

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
        }
        #endregion
    }
}