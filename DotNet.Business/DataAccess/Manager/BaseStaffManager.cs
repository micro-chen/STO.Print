//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

	/// <summary>
    ///	BaseStaffManager
	/// 员工的基类
	/// 
	/// 改进了很多次，基本上很好用了。
	/// 
    /// 修改记录
    /// 
    ///     2011.08.01 版本：1.8 张广梁     修改public DataTable GetAddressPageDT(out int recordCount, string organizeId, string searchValue, int pageSize, int pageIndex) 中的错误
    ///     2009.09.29 版本: 1.7 JiRiGaLa   已删除的进行过滤。
    ///     2008.05.07 版本: 1.6 JiRiGaLa   主键进行整理。
    ///     2007.07.19 版本: 1.5 JiRiGaLa   GetListByDepartment 函数改进。
    ///     2007.07.19 版本: 1.5 JiRiGaLa   增加 GetImpersonationList 方法。
    ///     2007.07.12 版本: 1.4 JiRiGaLa   增加 SetProperty,GetList 方法。
    ///		2007.07.02 版本：1.3 JiRiGaLa   添加 GetList。
    ///     2007.01.06 版本：1.2 JiRiGaLa   添加排序方法(Swap)。    
    ///		2006.12.07 版本：1.1 JiRiGaLa   增加排序码重置方法(ResetSortCode)。
	///		2006.02.05 版本：1.1 JiRiGaLa   重新调整主键的规范化。
	///		2004.08.29 版本：1.0 Jirigala   改进了错误提示，变得更友好一些，注意大小写错误取消。
	/// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.05.07</date>
	/// </author> 
	/// </summary>
    public partial class BaseStaffManager : BaseManager
    {
        public BaseStaffEntity GetObjectByCode(string code)
        {
            BaseStaffEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldCode, code));
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseStaffEntity>(dt);
            }
            return entity;
        }

        public BaseStaffEntity GetObjectByRealName(string realName)
        {
            BaseStaffEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldRealName, realName));
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseStaffEntity>(dt);
            }
            return entity;
        }

        #region public BaseUserInfo ConvertToUserInfo(BaseUserInfo userInfo, BaseStaffEntity staffEntity)
        /// <summary>
        /// 员工实体转换为用户实体
        /// </summary>
        /// <param name="result">用户实体</param>
        /// <param name="staffEntity">员工实体</param>
        /// <returns>用户实体</returns>
        public BaseUserInfo ConvertToUserInfo(BaseUserInfo userInfo, BaseStaffEntity staffEntity)
        {
            // result.Id = staffEntity.Id;
            // userInfo.StaffId = staffEntity.Id.ToString();
            userInfo.Code = staffEntity.Code;
            if (string.IsNullOrEmpty(userInfo.UserName))
            {
                userInfo.UserName = staffEntity.UserName;
            }
            if (string.IsNullOrEmpty(userInfo.RealName))
            {
                userInfo.RealName = staffEntity.RealName;
            }
            // 需要修正
            userInfo.CompanyId = staffEntity.CompanyId;
            // result.CompanyCode = staffEntity.CompanyCode;
            // result.CompanyName = staffEntity.CompanyName;
            // result.SubCompanyId = staffEntity.SubCompanyId;
            userInfo.DepartmentId = staffEntity.DepartmentId;
            // result.DepartmentCode = staffEntity.DepartmentCode;
            // result.DepartmentName = staffEntity.DepartmentName;
            // result.WorkgroupId = staffEntity.WorkgroupId;
            // result.WorkgroupCode = staffEntity.WorkgroupCode;
            // result.WorkgroupName = staffEntity.WorkgroupName;
            return userInfo;
        }
        #endregion

        #region public string Add(string DepartmentId, string userName, string code, string fullName, bool isVirtual, bool isDimission, bool enabled, string description)
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="DepartmentId">部门主键</param>
        /// <param name="userName">用户名</param>
        /// <param name="code">编号</param>
        /// <param name="fullName">名称</param>
        /// <param name="canVisit">允许登录</param>
        /// <param name="isVirtual">虚拟用户</param>
        /// <param name="isDimission">离职</param>
        /// <param name="enabled">有效</param>
        /// <param name="description">备注</param>
        /// <returns>主键</returns>
        public string Add(string departmentId, string userName, string code, string fullName, bool isVirtual, bool isDimission, bool enabled, string description)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
            string sequence = sequenceManager.Increment(this.CurrentTableName);
            sqlBuilder.BeginInsert(this.CurrentTableName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldId, sequence);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCode, code);
            sqlBuilder.SetValue(BaseStaffEntity.FieldUserName, userName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldRealName, fullName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDepartmentId, departmentId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldSortCode, sequence);
            sqlBuilder.SetValue(BaseStaffEntity.FieldEnabled, enabled ? 1 : 0);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseStaffEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseStaffEntity.FieldCreateOn, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseStaffEntity.FieldCreateOn);
            string result = sqlBuilder.EndInsert() > 0 ? sequence : string.Empty;
            return result;
        }
        #endregion

        #region public string Add(BaseStaffEntity staffEntity, out string statusCode)
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="staffEntity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>主键</returns>
        public string Add(BaseStaffEntity staffEntity, out string statusCode)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(staffEntity.UserName) && this.Exists(new KeyValuePair<string, object>(BaseStaffEntity.FieldUserName, staffEntity.UserName), new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0)))
            {
                // 名称已重复
                statusCode = Status.ErrorUserExist.ToString();
            }
            else
            {
                // 检查编号是否重复
                if (!string.IsNullOrEmpty(staffEntity.Code) && this.Exists(new KeyValuePair<string, object>(BaseStaffEntity.FieldCode, staffEntity.Code), new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0)))
                {
                    // 编号已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    result = this.AddObject(staffEntity);
                    // 运行成功
                    statusCode = Status.OKAdd.ToString();
                }
            }
            return result;
        }
        #endregion

        #region public int Update(BaseStaffEntity staffEntity, out string statusCode)
        /// <summary>
        /// 更新员工
        /// </summary>
        /// <param name="staffEntity">实体</param>
        /// <param name="isUser">是否用户</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public int Update(BaseStaffEntity staffEntity, out string statusCode)
        {
            int result = 0;
            // 检查编号是否重复
            if (!string.IsNullOrEmpty(staffEntity.Code) && this.Exists(new KeyValuePair<string, object>(BaseStaffEntity.FieldCode, staffEntity.Code), new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0), staffEntity.Id))
            {
                // 编号已重复
                statusCode = Status.ErrorCodeExist.ToString();
            }
            else
            {
                // 用户名是空的，不判断是否重复了
                if (!String.IsNullOrEmpty(staffEntity.UserName) && this.Exists(new KeyValuePair<string, object>(BaseStaffEntity.FieldUserName, staffEntity.UserName), new KeyValuePair<string, object>(BaseStaffEntity.FieldDeletionStateCode, 0), staffEntity.Id))
                {
                    // 名称已重复
                    statusCode = Status.ErrorUserExist.ToString();
                }
                else
                {
                    result = this.UpdateObject(staffEntity);
                    // 按员工的修改信息，把用户信息进行修改
                    this.UpdateUser(staffEntity.Id.ToString());
                    if (result > 0)
                    {
                        statusCode = Status.OKUpdate.ToString();
                    }
                    else
                    {
                        statusCode = Status.ErrorDeleted.ToString();
                    }
                }
            }
            return result;            
        }
        #endregion

        #region public int UpdateAddress(BaseStaffEntity staffEntity, out string statusCode) 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="staffEntity">实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateAddress(BaseStaffEntity staffEntity, out string statusCode)
        {
            int result = 0;
            // 检查是否已被其他人修改            
            //if (DbLogic.IsModifed(DbHelper, BaseStaffEntity.TableName, staffEntity.Id, staffEntity.ModifiedUserId, staffEntity.ModifiedOn))
            //{
            //    // 数据已经被修改
            //    statusCode = StatusCode.ErrorChanged.ToString();
            //}
            //else
            //{
                // 进行更新操作
                result = this.UpdateObject(staffEntity);
                if (result == 1)
                {
                    // 按员工的修改信息，把用户信息进行修改
                    this.UpdateUser(staffEntity.Id.ToString());
                    statusCode = Status.OKUpdate.ToString();
                }
                else
                {
                    // 数据可能被删除
                    statusCode = Status.ErrorDeleted.ToString();
                }
            //}
            return result;
        }
        #endregion

        #region public DataTable GetDataTableByCompany(string companyId)
        /// <summary>
        /// 按公司获取部门员工
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByCompany(string companyId)
        {
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                + " FROM " + BaseStaffEntity.TableName;
            if (!String.IsNullOrEmpty(companyId))
            {
                sqlQuery += " WHERE " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = '" + companyId + "' ";
            }
            sqlQuery += " ORDER BY " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable GetDataTableByOrganizes(string[] ids) 按工作组、部门、公司获取员工列表
        /// <summary>
        /// 按工作组、部门、公司获取员工列表
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByOrganizes(string[] organizeIds)
        {
            string organizeList = string.Join(",", organizeIds);
            string sqlQuery = "    SELECT " + BaseStaffEntity.TableName + ".* "
                                + "  FROM " + BaseStaffEntity.TableName 
                                + " WHERE " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDeletionStateCode + " = 0 "
                                + "        AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldWorkgroupId + " IN ( " + organizeList + ") "
                                + "               OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " IN (" + organizeList + ") "
                                + "               OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSubCompanyId + " IN (" + organizeList + ") "
                                + "               OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " IN (" + organizeList + ")) "
                                + " ORDER BY " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable GetDataTableByOrganize(string organizeId)
        /// <summary>
        /// 获取部门员工
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByOrganize(string organizeId)
        {
            string[] organizeIds = new string[] { organizeId };
            return this.GetDataTableByOrganizes(organizeIds);
        }
        #endregion

        #region public DataTable GetDataTableByDepartment(string departmentId)
        /// <summary>
        /// 按部门获取部门员工
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByDepartment(string departmentId)
        {
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".CompanyId) AS CompanyCode"
                + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".CompanyId) AS CompanyFullname "
                + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " From " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".DepartmentId) AS DepartmentCode"
                + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".DepartmentId) AS DepartmentName "
                + " ,(SELECT " + BaseItemDetailsEntity.FieldItemName + " FROM ItemsDuty WHERE Id = " + BaseStaffEntity.TableName + ".DutyId) AS DutyName "
                + " ,(SELECT " + BaseItemDetailsEntity.FieldItemName + " FROM ItemsTitle WHERE Id = " + BaseStaffEntity.TableName + ".TitleId) AS TitleName "
                + " ,(SELECT " + BaseRoleEntity.FieldRealName + " FROM " + BaseRoleEntity.TableName + " WHERE Id = RoleId) AS RoleName "
                + " FROM " + BaseStaffEntity.TableName + " LEFT OUTER JOIN " + BaseUserEntity.TableName
                + "       ON " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " = " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId;
            if (!String.IsNullOrEmpty(departmentId))
            {
                sqlQuery += " WHERE " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = '" + departmentId + "' ";
            }
            sqlQuery += " ORDER BY " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public string GetStaffCount(IDbHelper dbHelper, string organizeCode)
        /// <summary>
		/// 获取部门的员工个数
		/// </summary>
		/// <param name="organizeCode">部门编码</param>
		/// <returns>员工个数</returns>
		public string GetStaffCount(string organizeCode)
		{
			String sqlQuery			= string.Empty;
			String staffCount		= string.Empty;
			string[] names		= new string[1];
			object[] values	= new object[1];
            sqlQuery = @"SELECT COUNT(Id) AS STAFFCOUNT FROM " + BaseStaffEntity.TableName + " WHERE (ENABLED = 1) AND (ISDIMISSION <> 1) AND (ISSTAFF = 1) AND (DepartmentId IN (SELECT Id FROM " + BaseOrganizeEntity.TableName + " WHERE (LEFT(CODE, LEN(?)) = ?))) ";
            names[0] = BaseStaffEntity.FieldCompanyId;			
			values[0]	= organizeCode;
            object result = DbHelper.ExecuteScalar(sqlQuery, DbHelper.MakeParameters(names, values));
            if (result != null)
			{
                staffCount = result.ToString();
			}
			return staffCount;		
		}
		#endregion

		#region public string GetCategoryCount(IDbHelper dbHelper, string categoryId, string organizeCode, string categoryField)
		/// <summary>
		/// 获得某部门某种属性的人数
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="organizeCode"></param>
		/// <param name="categoryField"></param>
		/// <returns></returns>
		public string GetCategoryCount(IDbHelper dbHelper, string categoryId, string organizeCode, string categoryField)
		{
			String sqlQuery			= string.Empty;
            string staffCount		= string.Empty;
			string[] names		= new string[3];
			object[] values	= new object[3];
            sqlQuery = "SELECT COUNT(Id) AS STAFFCOUNT FROM " + BaseStaffEntity.TableName
                            + " WHERE (" + categoryField + " = ?) AND (ENABLED = 1) AND (ISDIMISSION <> 1) AND (ISSTAFF = 1) AND (DepartmentId IN (SELECT Id FROM " + BaseOrganizeEntity.TableName + " WHERE (LEFT(CODE, LEN(?)) = ?))) ";			
			names[0]	= categoryField;
            names[1] = BaseOrganizeEntity.FieldCode;
			names[2]	= organizeCode;
			values[0]	= categoryId;
			values[1]	= organizeCode;
			values[2]	= organizeCode;
            object result = dbHelper.ExecuteScalar(sqlQuery, DbHelper.MakeParameters(names, values));
            if (result != null)
			{
                staffCount = result.ToString();
			}
			return staffCount;		
		}
		#endregion

        #region public DataTable SearchByOrganizeIds(string[] ids, string userName, string enabled, string role)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userName"></param>
        /// <param name="enabled"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable SearchByOrganizeIds(string[] organizeIds, string userName, string enabled, string role)
        {
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                                + " FROM " + BaseStaffEntity.TableName 
                                + " WHERE 1=1 ";

            // 这里要注意系统安全隐患
            if (organizeIds != null)
            {
                // 可以管理的部门
                string organizes = string.Join(",", organizeIds);
                sqlQuery += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldWorkgroupId + " IN (" + organizes + ") ";
                sqlQuery += "     OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " IN (" + organizes + ") ";
                sqlQuery += "     OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSubCompanyId + " IN (" + organizes + ") ";
                sqlQuery += "     OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " IN (" + organizes + ")) ";
            }
            
            if (!String.IsNullOrEmpty(userName))
            {
                if (userName.IndexOf('%') < 0)
                {
                    userName = "%" + userName + "%";
                }
                userName = DbHelper.SqlSafe(userName);
            }
            if (!String.IsNullOrEmpty(userName))
            {
                userName = userName.ToLower();
                sqlQuery += " AND " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserName + " LIKE ('" + userName + "') ";
            }
            
            if (!String.IsNullOrEmpty(enabled))
            {
                sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = '" + enabled + "'";
            }
            
            if (!String.IsNullOrEmpty(role))
            {
                // sqlQuery += " AND " + BaseUserEntity.FieldRoleId + " = '" + role + "'";
            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable Search(string userName, string enabled, string role) 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="role">类型</param>
        /// <param name="enabled">有效</param>
        /// <returns>数据权限</returns>
        public DataTable Search(string userName, string enabled, string role)
        {
            return this.SearchByOrganizeIds(null, userName, enabled, role);
        }
        #endregion

        #region public DataTable GetAddressDataTable(string organizeId = null, string searchValue = null) 获取打印列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="search">查询字符</param>
        /// <returns>数据表</returns>
        public DataTable GetAddressDataTable(string organizeId = null, string searchValue = null)
        {
            // 因为Access中不支持分页，故此操作
            if (BaseSystemInfo.UserCenterDbType == CurrentDbType.Access)
            {
                var dt = new DataTable();
                string Cmd = "SELECT * from BaseStaff ";

                /*"SELECT A.* ,B.Code AS CompanyCode ,B.FullName AS CompanyName , " +
                        " C.Code AS DepartmentCode ,C.FullName AS DepartmentName ,D.ItemName AS DutyName ," +
                        " F.RealName as RoleName  " +
                        " FROM (((((BaseStaff A LEFT OUTER JOIN BaseOrganize B ON B.Id = A.CompanyId)" +
                        " LEFT OUTER JOIN BaseOrganize C ON C.Id = A.DepartmentId)" +
                        " LEFT OUTER JOIN ItemsDuty D ON D.Id = CInt(iif(IsNull( A.DutyId ), 0, A.DutyId)))" +
                        " LEFT JOIN BaseUser E ON A.Id = E.Id )" +
                        " left join BaseRole F on F.Id = E.RoleId)"; */
                // dbHelper.Fill(DT, Cmd,"List"); DT.Tables["List"];

                return dbHelper.Fill(dt, Cmd);
            }

            searchValue = StringUtil.GetSearchString(searchValue);
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                            + "," + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldCode + " AS CompanyCode "
                            + "," + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldFullName + " AS CompanyName "
                            + "," + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldCode + " AS DepartmentCode "
                            + "," + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldFullName + " AS DepartmentName "
                            + ",ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " AS DutyName "
                            + "," + "OT.RoleName "
                            + " FROM " + BaseStaffEntity.TableName
                            + "      LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " " + BaseOrganizeEntity.TableName + "A ON " + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldId + " = " + BaseStaffEntity.FieldCompanyId
                            + "      LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " " + BaseOrganizeEntity.TableName + "B ON " + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldId + " = " + BaseStaffEntity.FieldDepartmentId
                            + "      LEFT OUTER JOIN ItemsDuty ON ItemsDuty." + BaseItemDetailsEntity.FieldId + " = " + BaseStaffEntity.FieldDutyId
                            + "      ON " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " = OT.Id ";
            if (String.IsNullOrEmpty(organizeId))
            {
                sqlQuery += " WHERE ((" + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1) OR (" + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldIsInnerOrganize + " =1)) ";
            }
            else
            {
                sqlQuery += " WHERE (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = '" + organizeId + "'"
                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = '" + organizeId + "') ";
            }
            if (!String.IsNullOrEmpty(searchValue))
            {
                sqlQuery += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldRealName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldFullName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldFullName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + "ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldOfficePhone + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldExtension + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldMobile + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldShortNumber + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldQQ + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEmail + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDescription + " LIKE '" + searchValue + "'";
                sqlQuery += " OR OT.RoleName LIKE '" + searchValue + "')";
            }
            sqlQuery += " ORDER BY " + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldSortCode
                          + " ," + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable GetAddressDataTableByPage(string organizeId, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 100, string sort = null) 获取打印列表
        /// <summary>
        /// 获取列表 HJC
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="searchValue">查询字符</param>
        /// <param name="pageSize">分页的条数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <returns>数据表</returns>
        public DataTable GetAddressDataTableByPage(string organizeId, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 100, string sort = null)
        {
            // 因为Access中不支持分页，故此操作 假设行 1 //xtzwd
            if (BaseSystemInfo.UserCenterDbType == CurrentDbType.Access)
            {
                recordCount = 1;
                var dt = new DataTable();
                string Cmd = "SELECT * from BaseStaff ";
                    
                    /*"SELECT A.* ,B.Code AS CompanyCode ,B.FullName AS CompanyName , " +
                            " C.Code AS DepartmentCode ,C.FullName AS DepartmentName ,D.ItemName AS DutyName ," +
                            " F.RealName as RoleName  " +
                            " FROM (((((BaseStaff A LEFT OUTER JOIN BaseOrganize B ON B.Id = A.CompanyId)" +
                            " LEFT OUTER JOIN BaseOrganize C ON C.Id = A.DepartmentId)" +
                            " LEFT OUTER JOIN ItemsDuty D ON D.Id = CInt(iif(IsNull( A.DutyId ), 0, A.DutyId)))" +
                            " LEFT JOIN BaseUser E ON A.Id = E.Id )" + 
                             //此处多级查询溢出，有待优化
                            " left join BaseRole F on F.Id = E.RoleId)"; */
                // dbHelper.Fill(DT, Cmd,"List"); DT.Tables["List"];

                return dbHelper.Fill(dt, Cmd);
            }
            
            searchValue = StringUtil.GetSearchString(searchValue);
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                            + " ," + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldCode + " AS CompanyCode "
                            + " ," + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldCode + " AS DepartmentCode "
                            + " ,ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " AS DutyName "
                            + " FROM " + BaseStaffEntity.TableName
                            + "      LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " " + BaseOrganizeEntity.TableName + "A ON " + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldId + " = " + BaseStaffEntity.FieldCompanyId
                            + "      LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " " + BaseOrganizeEntity.TableName + "B ON " + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldId + " = " + BaseStaffEntity.FieldDepartmentId
                            + "      LEFT OUTER JOIN ItemsDuty ON ItemsDuty." + BaseItemDetailsEntity.FieldId + " = " + BaseStaffEntity.FieldDutyId;
            if (String.IsNullOrEmpty(organizeId))
            {
                sqlQuery += " WHERE ((" + BaseOrganizeEntity.TableName + "A." + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1) OR (" + BaseOrganizeEntity.TableName + "B." + BaseOrganizeEntity.FieldIsInnerOrganize + " =1)) ";
            }
            else
            {
                sqlQuery += " WHERE (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = '" + organizeId + "'"
                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = '" + organizeId + "') ";
            }
            if (!String.IsNullOrEmpty(searchValue))
            {
                sqlQuery += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldRealName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "A." + BaseStaffEntity.FieldCompanyName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "B." + BaseStaffEntity.FieldDepartmentName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + "ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldOfficePhone + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldExtension + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldMobile + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldShortNumber + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldQQ + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEmail + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDescription + " LIKE '" + searchValue + "')";
            }
            string orderBy = string.Empty;
            switch (this.DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                    orderBy = BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
                    break;
                case CurrentDbType.DB2:
                    orderBy = BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
                    break;
                default:
                    orderBy = BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
                    break;
            }
            return this.GetDataTableByPage(out recordCount, pageIndex, pageSize, orderBy, "ASC", sqlQuery);
        }
        #endregion

        #region public DataTable Search(string organizeId, string searchValue, bool deletionStateCode) 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="searchValue">查询字符串</param>
        /// <param name="deletionStateCode">删除标志</param>
        /// <returns>数据表</returns>
        public DataTable Search(string organizeId, string searchValue, bool deletionStateCode)
        {
            searchValue = StringUtil.GetSearchString(searchValue);
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                            + "," + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldFullName + " AS DepartmentName "
                            + ",ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " AS DutyName "
                            + " FROM " + BaseStaffEntity.TableName
                            + "      LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " ON " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldId + " = " + BaseStaffEntity.FieldDepartmentId
                            + "      LEFT OUTER JOIN ItemsDuty ON  ItemsDuty." + BaseItemDetailsEntity.FieldId + " = " + BaseStaffEntity.FieldDutyId
                            + " WHERE (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDeletionStateCode + " = " + (deletionStateCode ? 1 : 0) + ")";
            if (!String.IsNullOrEmpty(organizeId))
            {
                sqlQuery += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = '" + organizeId + "' OR " +BaseStaffEntity.FieldCompanyId +" = '" + organizeId + "')";
            }
            if (!String.IsNullOrEmpty(searchValue))
            {
                sqlQuery += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldRealName + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldShortNumber + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldTelephone + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldMobile + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEmail + " LIKE '" + searchValue + "'";
                sqlQuery += " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldQQ + " LIKE '" + searchValue + "'";
                sqlQuery += " OR ItemsDuty." + BaseItemDetailsEntity.FieldItemName + " LIKE '" + searchValue + "')";
            }
            sqlQuery += " ORDER BY " // + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldSortCode
                          // + " ," 
                          + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        public DataTable GetChildrenStaffs(string organizeId)
        {
            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
            string[] organizeIds = null;
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.Access:
                case CurrentDbType.SqlServer:
                    string organizeCode = this.GetCodeById(organizeId);
                    organizeIds = organizeManager.GetChildrensIdByCode(BaseOrganizeEntity.FieldCode, organizeCode);
                    break;
                case CurrentDbType.Oracle:
                    organizeIds = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, organizeId, BaseOrganizeEntity.FieldParentId);
                    break;
            }
            return this.GetDataTableByOrganizes(organizeIds);
        }

        public DataTable GetParentChildrenStaffs(string organizeId)
        {
            string[] organizeIds = null;
            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
            string organizeCode = organizeManager.GetCodeById(organizeId);
            organizeIds = organizeManager.GetChildrensIdByCode(BaseOrganizeEntity.FieldCode, organizeCode);
            return this.GetDataTableByOrganizes(organizeIds);
        }

        #region public DataTable GetDataTable()
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <returns>数据表</returns>
        public DataTable GetDataTable()
        {
            string sqlQuery = "SELECT " + BaseStaffEntity.TableName + ".* "
                + " , " + BaseUserEntity.TableName + ".UserOnLine"
                + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".CompanyId) AS CompanyCode"
                + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".CompanyId) AS CompanyFullname "

                + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " From " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".DepartmentId) AS DepartmentCode"
                + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".DepartmentId) AS DepartmentName "

                + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " From " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".WorkgroupId) AS WorkgroupCode"
                + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = " + BaseStaffEntity.TableName + ".WorkgroupId) AS WorkgroupName "

                + " ,(SELECT " + BaseItemDetailsEntity.FieldItemName + " FROM ItemsDuty WHERE Id = " + BaseStaffEntity.TableName + ".DutyId) AS DutyName "
                
                + " ,(SELECT " + BaseItemDetailsEntity.FieldItemName + " FROM ItemsTitle WHERE Id = " + BaseStaffEntity.TableName + ".TitleId) AS TitleName "
                
                + " ,(SELECT " + BaseRoleEntity.FieldRealName + " FROM " + BaseRoleEntity.TableName + " WHERE Id = RoleId) AS RoleName "
                // + " ,(SELECT COUNT(1) FROM " + BaseUserRoleEntity.TableName + " WHERE " + BaseUserRoleEntity.TableName + ".StaffID = " + BaseStaffEntity.TableName + ".Id) AS RoleCount "
                + " FROM (" + BaseStaffEntity.TableName + " LEFT OUTER JOIN " + BaseUserEntity.TableName
                + " ON " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " = " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + ") "
                + "  LEFT OUTER JOIN " + BaseOrganizeEntity.TableName + " "
                + " ON " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldId
                + " ORDER BY " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldSortCode
                + " , " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable GetDataTable(string fieldName, string fieldValue) 获取打印列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="fieldName">字段</param>
        /// <param name="fieldValue">内容</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string fieldName, string fieldValue)
        {
            string sqlQuery = "SELECT A.* "
                            
                            + " ,(SELECT Code FROM " + BaseOrganizeEntity.TableName + " WHERE " + BaseOrganizeEntity.TableName + ".ID = A.CompanyId) AS CompanyCode"
                            + " ,(SELECT FullName FROM " + BaseOrganizeEntity.TableName + " WHERE " + BaseOrganizeEntity.TableName + ".ID = A.CompanyId) AS CompanyName "
                            
                            + " ,(SELECT Code FROM " + BaseOrganizeEntity.TableName + " WHERE " + BaseOrganizeEntity.TableName + ".ID = A.DepartmentId) AS DepartmentCode"
                            + " ,(SELECT FullName FROM " + BaseOrganizeEntity.TableName + " WHERE " + BaseOrganizeEntity.TableName + ".ID = A.DepartmentId) AS DepartmentName "

                            + " ,(SELECT " + BaseOrganizeEntity.FieldCode + " From " + BaseOrganizeEntity.TableName + " WHERE Id = A.WorkgroupId) AS WorkgroupCode"
                            + " ,(SELECT " + BaseOrganizeEntity.FieldFullName + " FROM " + BaseOrganizeEntity.TableName + " WHERE Id = A.WorkgroupId) AS WorkgroupName "

                            + " ,(SELECT ItemName FROM ItemsDuty WHERE ItemsDuty.Id = A.DutyId) AS DutyName "

                            + " ,(SELECT ItemName FROM ItemsTitle WHERE ItemsTitle.Id = A.TitleId) AS TitleName "

                            + " FROM " + BaseStaffEntity.TableName + " A "
                            + " WHERE " + fieldName + " = " + DbHelper.GetParameter(fieldName)
                            + " ORDER BY A.SortCode";
            return DbHelper.Fill(sqlQuery, new IDbDataParameter[] { DbHelper.MakeParameter(fieldName, fieldValue)});
        }
        #endregion

        #region public int BatchSave(DataTable result) 批量进行保存
        /// <summary>
        /// 批量进行保存
        /// </summary>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseStaffEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.DeleteObject(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseStaffEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
						BaseStaffEntity staffEntity = BaseEntity.Create<BaseStaffEntity>(dr);
                        result += this.UpdateObject(staffEntity);                       
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
					BaseStaffEntity staffEntity = BaseEntity.Create<BaseStaffEntity>(dr);
                    result += this.AddObject(staffEntity).Length > 0 ? 1 : 0;
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
            this.StatusCode = Status.OK.ToString();
            return result;
        }
        #endregion

        #region public int UpdateUser(string staffId) 按员工的修改信息，把用户信息进行修改
        /// <summary>
        /// 按员工的修改信息，把用户信息进行修改
        /// </summary>
        /// <param name="staffId">员工主键</param>
        /// <returns>影响行数</returns>
        public int UpdateUser(string staffId)
        {
            //var result = this.GetDataTable(BaseStaffEntity.FieldId, staffId);
            //BaseStaffEntity staffEntity = new BaseStaffEntity(result);
            //if (staffEntity.UserId > 0)
            //{
            //    // 员工信息改变时，用户信息也跟着改变。
            //    BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
            //    BaseUserEntity userEntity = userManager.GetObject(staffEntity.UserId);
            //    // userEntity.Company = staffEntity.CompanyName;
            //    // userEntity.Department = staffEntity.DepartmentName;
            //    // userEntity.Workgroup = staffEntity.WorkgroupName;
                
            //    userEntity.UserName = staffEntity.UserName;
            //    userEntity.RealName = staffEntity.RealName;
            //    userEntity.Code = staffEntity.Code;

            //    userEntity.Email = staffEntity.Email;
            //    userEntity.Enabled = staffEntity.Enabled;
            //    // userEntity.Duty = staffEntity.DutyName;
            //    // userEntity.Title = staffEntity.TitleName;
            //    userEntity.Gender = staffEntity.Gender;
            //    userEntity.Birthday = staffEntity.Birthday;
            //    userEntity.Mobile = staffEntity.Mobile;
            //}
            return 0;
        }
        #endregion

        #region public int DeleteUser(string staffId) 删除员工关联的用户
        /// <summary>
        /// 删除员工关联的用户
        /// </summary>
        /// <param name="staffId">员工主键</param>
        /// <returns>影响行数</returns>
        public int DeleteUser(string staffId)
        {
            int result = 0;
            string userId = this.GetObject(staffId).UserId.ToString();
            if (!String.IsNullOrEmpty(userId))
            {
                // 删除用户
                BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
                result += userManager.SetDeleted(userId);
            }
            // 将员工的用户设置为空
            result += this.SetProperty(new KeyValuePair<string, object>(BaseStaffEntity.FieldId, staffId), new KeyValuePair<string, object>(BaseStaffEntity.FieldUserId, null));
            return result;
        }
        #endregion

        #region public override int SetDeleted(string id) 删除标志
        /// <summary>
        /// 删除标志
        /// 
        /// 删除员工时，需要把用户也给删除掉
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int SetDeleted(string id)
        {
            // 先把用户设置为删除标记
            string userId = this.GetProperty(id, BaseStaffEntity.FieldUserId);
            if (!String.IsNullOrEmpty(userId))
            {
                BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
                userManager.SetDeleted(userId);
            }
            // 再把员工设置为删除标记
            return this.SetProperty(new KeyValuePair<string, object>(BaseBusinessLogic.FieldId, id), new KeyValuePair<string, object>(BaseBusinessLogic.FieldDeletionStateCode, 1));
        }
        #endregion

        #region public override int ResetSortCode() 重置排序码
        /// <summary>
        /// 重置排序码
        /// </summary>
        public override int ResetSortCode()
        {
            int result = 0;
            var dt = this.GetDataTable();
            string id = string.Empty;
            string sortCode = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                id = dr[BaseStaffEntity.FieldId].ToString();
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
                sortCode = sequenceManager.Increment(BaseStaffEntity.TableName);
                result += this.SetProperty(id, new KeyValuePair<string, object>(BaseStaffEntity.FieldSortCode, sortCode));
            }
            return result;
        }
        #endregion

        #region public int Delete(string id) 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            int result = 0;
            BaseStaffEntity staffEntity = this.GetObject(id);
            if (staffEntity != null)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                // 删除相关的用户数据
                BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
                userManager.DeleteObject(staffEntity.UserId);
                // 删除员工本表
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldId, id));
                result = DbLogic.Delete(DbHelper, BaseStaffEntity.TableName, parameters);
            }
            return result;
        }
        #endregion

        #region public int BatchDelete(string[] ids)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int BatchDelete(string[] ids)
        {
            int result = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                result += this.Delete(ids[i]);
            }
            return result;
        }
        #endregion

        #region public string GetIdByUserId(string userId) 通过用户Id获取员工主键
        /// <summary>
        /// 通过用户Id获取员工主键
        /// </summary>
        /// <param name="code">用户Id</param>
        /// <returns>员工主键</returns>
        public string GetIdByUserId(string userId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseStaffEntity.FieldUserId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseBusinessLogic.FieldDeletionStateCode, 0));
            return DbLogic.GetProperty(DbHelper, this.CurrentTableName, parameters, BaseBusinessLogic.FieldId);
        }
        #endregion
    }
}