//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace STO.Print.Manager
{
    using DotNet.Business;
    using Model;

    /// <summary>
    /// ZtoPrintBillManager
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-07-16 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-07-16</date>
    /// </author>
    ///  </summary>
    public partial class ZtoPrintBillManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 将打印数据添加到备份库里面，方便查询使用
        /// </summary>
        /// <param name="list">打印记录实体集合</param>
        /// <param name="tempEntity">默认快递公司模板实体</param>
        /// <returns>受影响行数</returns>
        public int AddHistory(List<ZtoPrintBillEntity> list, BaseTemplateEntity tempEntity)
        {
            var maxId = this.DbHelper.ExecuteScalar("SELECT MAX(Id) FROM ZTO_PRINT_BILL").ToString();
            var currentId = 0;
            var resultCount = 0;
            if (!string.IsNullOrEmpty(maxId))
            {
                currentId = Convert.ToInt32(maxId);
            }
            // 检查默认模板是否存在，存在就把快递公司ID赋值给打印记录，表示这个打印记录是哪个公司的单子
            if (tempEntity != null)
            {
                //using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintBackConnectionString))
                //{
                //    try
                //    {
                //        dbHelper.BeginTransaction();
                        foreach (ZtoPrintBillEntity printBill in list)
                        {
                            if (this.Exists(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, printBill.BillCode), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldOrderNumber, printBill.OrderNumber)))
                            {
                                continue;
                            }
                            if (!string.IsNullOrEmpty(printBill.BillCode)) // && !string.IsNullOrEmpty(printBill.OrderNumber)
                            {
                                ++currentId;
                                printBill.Id = currentId;
                                printBill.ExpressId = tempEntity.ExpressId.ToString();
                                Add(printBill);
                                ++resultCount;
                            }
                        }
                      //  dbHelper.CommitTransaction();
                    //}
                    //catch (Exception ex)
                    //{
                    //    dbHelper.RollbackTransaction();
                    //}
                //}
            }
            else
            {
                //using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintBackConnectionString))
                //{
                //    try
                //    {
                //        dbHelper.BeginTransaction();
                        // 找不到默认的快递公司就不用赋值了
                        foreach (ZtoPrintBillEntity printBill in list)
                        {
                            if (this.Exists(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, printBill.BillCode), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldOrderNumber, printBill.OrderNumber)))
                            {
                                continue;
                            }
                            if (!string.IsNullOrEmpty(printBill.BillCode) && !string.IsNullOrEmpty(printBill.OrderNumber))
                            {
                                ++currentId;
                                printBill.Id = currentId;
                                Add(printBill);
                                ++resultCount;
                            }
                        }
                    //    dbHelper.CommitTransaction();
                    //}
                    //catch (Exception ex)
                    //{
                    //    dbHelper.RollbackTransaction();
                    //}
                //}
            }
            return resultCount;
        }
    }
}
