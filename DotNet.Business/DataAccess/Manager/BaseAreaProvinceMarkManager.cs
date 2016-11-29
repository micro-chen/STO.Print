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
    /// BaseAreaProvinceMarkManager
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    ///
    ///		2015-07-03 版本：1.1 JiRiGaLa 代码重新构造。
    ///		2015-06-23 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-07-03</date>
    /// </author>
    /// </summary>
    public partial class BaseAreaProvinceMarkManager : BaseManager, IBaseManager
    {
        #region public DataTable GetAreaRouteMarkEdit(string parentId)
        /// <summary>
        /// 获取按省路由大头笔信息（输入）
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <returns>数据表</returns>
        public DataTable GetAreaRouteMarkEdit(string parentId)
        {
            DataTable result = null;

            string commandText = string.Empty;
            commandText = @"SELECT basearea_provincemark.Id
                                    , BaseArea.Id as AreaId
                                    , BaseArea.SORTCODE
                                    , BaseArea.Fullname as AREAID
                                    , BaseArea.Fullname as AREANAME
                                    , basearea_provincemark.MARK
                                    , basearea_provincemark.DESCRIPTION
                                    , basearea_provincemark.createon
                                    , basearea_provincemark.createby
                                    , basearea_provincemark.modifiedon
                                    , basearea_provincemark.modifiedby
                                FROM (SELECT id, code, fullname, SORTCODE FROM basearea
                                WHERE basearea.layer = 4
                                AND basearea.enabled = 1) basearea LEFT OUTER JOIN                     
                      (SELECT * FROM 
                      basearea_provincemark 
                      WHERE   basearea_provincemark.areaid = " + parentId + @" 
                      ) basearea_provincemark
                      ON BaseArea.Id = basearea_provincemark.provinceid   
                      ORDER BY basearea.code ";

            result = this.Fill(commandText);

            return result;
        }
        #endregion

        /// <summary>
        /// 设置按省路由大头笔
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="dtAreaRouteMark">路由设置</param>
        /// <returns>影响行数</returns>
        public int SetAreaRouteMark(string areaId, DataTable dtAreaRouteMark)
        {
            int result = 0;
            string areaName = string.Empty;

            BaseAreaEntity areaEntity = BaseAreaManager.GetObjectByCache(areaId);
            if (areaEntity != null)
            {
                areaName = areaEntity.FullName;
            }

            if (string.IsNullOrEmpty(areaName))
            {
                return result;
            }

            IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            foreach (DataRow dr in dtAreaRouteMark.Rows)
            {
                // BaseAreaProvinceMarkEntity entity = null;
                if (string.IsNullOrWhiteSpace(dr["Id"].ToString()))
                {
                    /*
                    entity = new BaseAreaProvinceMarkEntity();
                    BaseEntity.Create<BaseAreaProvinceMarkEntity>(dr);
                    entity.Province = dr["AREANAME"].ToString();
                    entity.ProvinceId = dr["AREAID"].ToString();
                    entity.Mark = dr["MARK"].ToString();
                    entity.Description = dr["DESCRIPTION"].ToString();
                    entity.CreateBy = this.UserInfo.RealName;
                    entity.CreateUserId = this.UserInfo.Id;
                    entity.CreateOn = DateTime.Now;
                    this.AddObject(entity);
                    */

                    SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
                    sqlBuilder.BeginInsert("BASEAREA_PROVINCEMARK");
                    sqlBuilder.SetFormula("Id", "SEQ_BASEAREA_PROVINCEMARK.NEXTVAL");
                    sqlBuilder.SetValue("MARK", dr["MARK"].ToString());
                    sqlBuilder.SetValue("DESCRIPTION", dr["DESCRIPTION"].ToString());
                    sqlBuilder.SetValue("AREAID", areaId);
                    sqlBuilder.SetValue("AREA", areaName);
                    sqlBuilder.SetValue("ProvinceID", dr["AREAID"].ToString());
                    sqlBuilder.SetValue("Province", dr["AREANAME"].ToString());
                    sqlBuilder.SetValue("CreateUserId", this.UserInfo.Id);
                    sqlBuilder.SetValue("CreateBy", this.UserInfo.RealName);
                    sqlBuilder.SetDBNow("CreateOn");
                    sqlBuilder.SetValue("Enabled", 1);
                    sqlBuilder.EndInsert();
                    // sqlBuilder.PrepareCommand();
                    // dotNetService.AreaService.ExecuteNonQuery(UserInfo, sqlBuilder.CommandText, sqlBuilder.DbParameters, CommandType.Text.ToString());
                }
                else
                {
                    /*
                    if (string.IsNullOrWhiteSpace(dr["MARK"].ToString()) && string.IsNullOrWhiteSpace(dr["DESCRIPTION"].ToString()))
                    {
                        this.Delete(dr["Id"].ToString());
                    }
                    else
                    {
                        entity = this.GetObject(dr["Id"].ToString());
                        // entity = BaseEntity.Create<BaseAreaProvinceMarkEntity>(dr);
                        entity.Mark = dr["MARK"].ToString();
                        entity.Description = dr["DESCRIPTION"].ToString();
                        entity.ModifiedBy = this.UserInfo.RealName;
                        entity.ModifiedUserId = this.UserInfo.Id;
                        entity.ModifiedOn = DateTime.Now;
                        this.Update(entity);
                    }
                    */
                    SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
                    if (string.IsNullOrWhiteSpace(dr["MARK"].ToString()) && string.IsNullOrWhiteSpace(dr["DESCRIPTION"].ToString()))
                    {
                        sqlBuilder.BeginDelete("BASEAREA_PROVINCEMARK");
                        sqlBuilder.SetWhere("Id", dr["Id"].ToString());
                        sqlBuilder.EndDelete();
                        // sqlBuilder.PrepareCommand();
                        // dotNetService.AreaService.ExecuteNonQuery(UserInfo, sqlBuilder.CommandText, sqlBuilder.DbParameters, CommandType.Text.ToString());
                    }
                    else
                    {
                        sqlBuilder.BeginUpdate("BASEAREA_PROVINCEMARK");
                        sqlBuilder.SetValue("MARK", dr["MARK"].ToString());
                        sqlBuilder.SetValue("DESCRIPTION", dr["DESCRIPTION"].ToString());
                        sqlBuilder.SetValue("ModifiedUserId", this.UserInfo.Id);
                        sqlBuilder.SetValue("ModifiedBy", this.UserInfo.RealName);
                        sqlBuilder.SetDBNow("ModifiedOn");
                        sqlBuilder.SetWhere("ID", dr["Id"].ToString());
                        sqlBuilder.EndUpdate();
                        // sqlBuilder.PrepareCommand();
                        // dotNetService.AreaService.ExecuteNonQuery(UserInfo, sqlBuilder.CommandText, sqlBuilder.DbParameters, CommandType.Text.ToString());
                    }
                }
            }

            return result;
        }


        public List<BaseAreaProvinceMarkEntity> GetList()
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldEnabled, 1));
            return this.GetList<BaseAreaProvinceMarkEntity>(parameters);
        }

        public List<BaseAreaProvinceMarkEntity> GetListByArea(int areaId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldAreaId, areaId));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldEnabled, 1));
            return this.GetList<BaseAreaProvinceMarkEntity>(parameters);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="search">查询</param>
        /// <returns>数据表</returns>
        public List<BaseAreaProvinceMarkEntity> SearchByList(string searchValue)
        {
            List<BaseAreaProvinceMarkEntity> result = null;

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * "
                    + "   FROM " + this.CurrentTableName
                    + "  WHERE " + BaseAreaProvinceMarkEntity.FieldEnabled + " = 1 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

            searchValue = searchValue.Trim();
            if (!String.IsNullOrEmpty(searchValue))
            {
                // 六、这里是进行支持多种数据库的参数化查询
                sqlQuery += string.Format(" AND ({0} LIKE {1} ", BaseAreaProvinceMarkEntity.FieldProvince, DbHelper.GetParameter(BaseAreaProvinceMarkEntity.FieldProvince));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaProvinceMarkEntity.FieldArea, DbHelper.GetParameter(BaseAreaProvinceMarkEntity.FieldArea));
                sqlQuery += string.Format(" OR {0} LIKE {1} ", BaseAreaProvinceMarkEntity.FieldMark, DbHelper.GetParameter(BaseAreaProvinceMarkEntity.FieldMark));
                sqlQuery += string.Format(" OR {0} LIKE {1}) ", BaseAreaProvinceMarkEntity.FieldDescription, DbHelper.GetParameter(BaseAreaProvinceMarkEntity.FieldDescription));

                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = string.Format("%{0}%", searchValue);
                }

                dbParameters.Add(DbHelper.MakeParameter(BaseAreaProvinceMarkEntity.FieldProvince, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaProvinceMarkEntity.FieldArea, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaProvinceMarkEntity.FieldMark, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseAreaProvinceMarkEntity.FieldDescription, searchValue));
            }
            sqlQuery += " ORDER BY " + BaseAreaProvinceMarkEntity.FieldProvince;

            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                result = this.GetList<BaseAreaProvinceMarkEntity>(dataReader);
            }

            return result;
        }
    }
}
