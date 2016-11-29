//-----------------------------------------------------------------------
// <copyright file="BaseLanguageManager.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseLanguageManager
    /// 多语言
    /// 
    /// 修改记录
    /// 
    /// 2015-02-25 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2015-02-25</date>
    /// </author>
    /// </summary>
    public partial class BaseLanguageManager
    {
        #region public string Add(BaseLanguageEntity entity, out string statusCode) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>主键</returns>
        public string Add(BaseLanguageEntity entity, out string statusCode)
        {
            string result = string.Empty;
            // 检查名称是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldLanguageCode, entity.LanguageCode));
            parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldMessageCode, entity.MessageCode));
            parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldDeletionStateCode, 0));
            if (this.Exists(parameters))
            {
                // 名称是否重复
                statusCode = Status.ErrorNameExist.ToString();
            }
            else
            {
                result = this.AddObject(entity);
                // 运行成功
                statusCode = Status.OKAdd.ToString();
            }
            return result;
        }
        #endregion

        #region public int Update(BaseLanguageEntity entity, out string statusCode) 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public int Update(BaseLanguageEntity entity, out string statusCode)
        {
            int result = 0;
            // 检查是否已被其他人修改

            if (DbLogic.IsModifed(DbHelper, this.CurrentTableName, entity.Id, entity.ModifiedUserId, entity.ModifiedOn))
            {
                // 数据已经被修改
                statusCode = Status.ErrorChanged.ToString();
            }
            else
            {
                // 检查名称是否重复
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldLanguageCode, entity.LanguageCode));
                parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldMessageCode, entity.MessageCode));
                parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldDeletionStateCode, 0));
                if (this.Exists(parameters, entity.Id))
                {
                    // 名称已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    result = this.UpdateObject(entity);
                    if (result == 1)
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

        public int SetLanguage(string languageCode, string messageCode, string caption)
        {
            int result = 0;

            // 更新的条件部分
            List<KeyValuePair<string, object>> whereParameters = new List<KeyValuePair<string, object>>();
            whereParameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldLanguageCode, languageCode));
            whereParameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldMessageCode, messageCode));
            whereParameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldDeletionStateCode, 0));
            // 更新的内容部分
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseLanguageEntity.FieldCaption, caption));
            result = this.SetProperty(whereParameters, parameters);

            // 若没能更新就进行新增操作
            if (result == 0)
            {
                BaseLanguageEntity entity = new BaseLanguageEntity();
                entity.LanguageCode = languageCode;
                entity.MessageCode = messageCode;
                entity.Caption = caption;
                entity.DeletionStateCode = 0;
                entity.ModifiedOn = DateTime.Now;
                entity.CreateOn = DateTime.Now;
                this.AddObject(entity);
                result++;
            }
            return result;
        }

        #region public int BatchSave(List<BaseLanguageEntity> entites) 批量保存
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="entites">实体列表</param>
        /// <returns>影响行数</returns>
        public int BatchSave(List<BaseLanguageEntity> entites)
        {
            int result = 0;
            foreach (BaseLanguageEntity entity in entites)
            {
                result += this.UpdateObject(entity);
            }
            return result;
        }
        #endregion
    }
}
