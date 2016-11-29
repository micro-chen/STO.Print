//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace STO.Print.Synchronous
{
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// Synchronous.cs
    /// 数据同步程序
    /// 
    /// 修改记录
    /// 
    ///     2015.03.23 版本：1.0 YangHengLian  添加项目详细资料功能页面编写。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015.03.23</date>
    /// </author> 
    /// </summary>
    public partial class Synchronous
    {
        public static bool Upgrade20150814()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    #region 创建发件人和收件人使用表

                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_USER] (
                        [Id] integer PRIMARY KEY, 			  
                        [RealName] VARCHAR2(30)  NULL,        
                        [PROVINCE] VARCHAR2(30)  NULL,     		
                        [PROVINCE_ID] VARCHAR2(30)  NULL,     
                        [CITY] VARCHAR2(50)  NULL, 	   	   		
                        [CITY_ID] VARCHAR2(50)  NULL, 	   	  
                        [COUNTY] VARCHAR2(30)  NULL, 	   		
                        [COUNTY_ID] VARCHAR2(30)  NULL, 	  
                        [COMPANY] VARCHAR2(30)  NULL,         
                        [DEPARTMENT] VARCHAR2(30)  NULL,      
                        [TELEPHONE] VARCHAR2(20)  NULL, 	   		
                        [MOBILE] VARCHAR2(20)  NULL, 
                        [POSTCODE] VARCHAR2(20) NULL, 	      
                        [ADDRESS] VARCHAR2(200)  NULL,     		
                        [IsSendOrReceive] VARCHAR2(200)  NULL,
                        [IsDefault] VARCHAR2(200)  NULL,
                        [REMARK] VARCHAR2(100), 				
                        [CREATEUSERNAME] VARCHAR2(30)  NULL,  
                        [CREATEON] DATE  NULL, 				  
                        [MODIFIEDUSERNAME] VARCHAR2(30), 		
                        [MODIFIEDON] DATE						
                        );");
                    #endregion
                    BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.08.13");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20150821()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    #region 创建运单打印表Id索引

                    dbHelper.ExecuteNonQuery(@"CREATE INDEX [I_Id] ON [ZTO_PRINT_BILL] ([Id]);");
                    #endregion
                    BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.08.21");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20150823()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    #region 创建快递公司和模板信息表（包含ID索引）

                    // 快递或者物流公司信息脚本
                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [BASE_EXPRESS] (
                                                [Id] integer PRIMARY KEY, 			    
                                                [NAME] VARCHAR2(30)  NULL,          		-- 名称
                                                [SHORTNAME]  VARCHAR2(30)  NULL,          	-- 简称（STO这样的）
                                                [LAYER] VARCHAR2(30)  NULL,     		 -- 优先级
                                                [PROVINCE_ID] VARCHAR2(30)  NULL,     		 -- 省份ID
                                                [CITY] VARCHAR2(50)  NULL, 	   	   		 -- 城市
                                                [CITY_ID] VARCHAR2(50)  NULL, 	   	   		 -- 城市ID
                                                [COUNTY] VARCHAR2(30)  NULL, 	   		 -- 区县
                                                [COUNTY_ID] VARCHAR2(30)  NULL, 	   		 -- 区县ID
                                                [ADDRESS] VARCHAR2(200)  NULL,     		 -- 详细地址
                                                [REMARK] VARCHAR2(100), 				  			-- 备注	
                                                [CREATEUSERNAME] VARCHAR2(30)  NULL,    		-- 创建人姓名
                                                [CREATEON] DATE  NULL, 				    	-- 创建时间
                                                [MODIFIEDUSERNAME] VARCHAR2(30), 		  			-- 修改人姓名
                                                [MODIFIEDON] DATE							 			-- 修改时间
                                                );
                                                CREATE INDEX [I_Express_Id] ON [BASE_EXPRESS] ([Id]);");
                    // 模板信息脚本
                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [BASE_TEMPLATE] (
                                                [Id] integer PRIMARY KEY, 			    
                                                [NAME] VARCHAR2(30)  NULL,          		 -- 模板名称
                                                [EXPRESS_ID] integer  NULL,                 -- 所属快递公司
                                                [LAYER] VARCHAR2(30)  NULL,     		     -- 优先级
                                                [FILE_PATH] VARCHAR2(200)  NULL,     		 -- 文件路径
                                                [WIDTH] VARCHAR2(50)  NULL, 	   	   		 -- 模板宽度（实际面单的宽度）
                                                [LENGTH] VARCHAR2(50)  NULL, 	   	   		 -- 模板长度（实际面单的长度）
                                                [BACKGROUND_IMAGE_PATH] VARCHAR2(200)  NULL,  --背景图路径（背景图片文件路径）
                                                [REMARK] VARCHAR2(100), 				  	 -- 备注	
                                                [CREATEUSERNAME] VARCHAR2(30) NULL,    		-- 创建人姓名
                                                [CREATEON] DATE  NULL, 				    	-- 创建时间
                                                [MODIFIEDUSERNAME] VARCHAR2(30), 		  	 -- 修改人姓名
                                                [MODIFIEDON] DATE							  -- 修改时间
                                                );
                                                CREATE INDEX [I_TEMPLATE_Id] ON [BASE_TEMPLATE] ([Id]);");
                    #endregion
                    BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.08.23");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20150826()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    #region 删除快递公司和模板信息表
                    // 判断表是否存在，然后再决定是否执行脚本
                    if (Convert.ToInt32(dbHelper.ExecuteScalar(@"SELECT COUNT(*) FROM sqlite_master WHERE type ='table' AND name='BASE_EXPRESS'")) == 1)
                    {
                        dbHelper.ExecuteNonQuery(@"DROP TABLE BASE_EXPRESS;");
                    }
                    // 判断表是否存在，然后再决定是否执行脚本
                    if (Convert.ToInt32(dbHelper.ExecuteScalar(@"SELECT COUNT(*) FROM sqlite_master WHERE type ='table' AND name='BASE_TEMPLATE'")) == 1)
                    {
                        dbHelper.ExecuteNonQuery(@"DROP TABLE BASE_TEMPLATE;");
                    }
                    #endregion
                    BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.08.23");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20150914()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    #region 删除打印记录表
                    // 判断表是否存在，然后再决定是否执行脚本
                    if (Convert.ToInt32(dbHelper.ExecuteScalar(@"SELECT COUNT(*) FROM sqlite_master WHERE type ='table' AND name='ZTO_PRINT_HISTORY'")) == 1)
                    {
                        dbHelper.ExecuteNonQuery(@"DROP TABLE ZTO_PRINT_HISTORY;");
                        dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_PRINT_HISTORY] (
                                                   [Id] integer PRIMARY KEY, 
                                                   [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                                   [RECEIVE_MAN] VARCHAR2(100)  NULL,       			-- 收件人姓名
                                                   [RECEIVE_COMPANY] VARCHAR2(100)  NULL,       			-- 收件人单位
                                                   [CREATEON] DATE  NULL, 				   				-- 创建时间
                                                   [EXPRESS_TYPE] DATE  NULL 				   				-- 快递公司
                                                    );");
                    }
                    else
                    {
                        dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_PRINT_HISTORY] (
                                                   [Id] integer PRIMARY KEY, 
                                                   [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                                   [RECEIVE_MAN] VARCHAR2(100)  NULL,       			-- 收件人姓名
                                                   [RECEIVE_COMPANY] VARCHAR2(100)  NULL,       			-- 收件人单位
                                                   [CREATEON] DATE  NULL, 				   				-- 创建时间
                                                   [EXPRESS_TYPE] DATE  NULL			   				-- 快递公司
                                                    );");
                    }
                    #endregion
                    BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.09.14");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20151015()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(dbHelper);
                    List<ZtoPrintBillEntity> ztoPrintBillEntities = printBillManager.GetList<ZtoPrintBillEntity>();
                    dbHelper.ExecuteNonQuery("DROP TABLE ZTO_PRINT_BILL;");
                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_PRINT_BILL] (
                                            [Id] integer PRIMARY KEY, 
                                            [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                            [SEND_DATE] VARCHAR2(30)  NULL, 				    		-- 发件日期
                                            [SEND_SITE] VARCHAR2(30)  NULL,             		-- 发件网点
                                            [SEND_MAN] VARCHAR2(30)  NULL,          			-- 发件人姓名
                                            [SEND_DEPARTURE] VARCHAR2(30) NULL,        			-- 始发地
                                            [SEND_PROVINCE] VARCHAR2(30)  NULL,     			-- 发件省份
                                            [SEND_CITY] VARCHAR2(50)  NULL, 	   	   			-- 发件城市
                                            [SEND_COUNTY] VARCHAR2(30)  NULL, 	   			-- 发件区县
                                            [SEND_COMPANY] VARCHAR2(30)  NULL,         			-- 发件人单位名称
                                            [SEND_DEPARTMENT] VARCHAR2(30)  NULL,      			-- 发件人单位部门
                                            [SEND_PHONE] VARCHAR2(20)  NULL, 	   			-- 发件人电话或手机
                                            [SEND_POSTCODE] VARCHAR2(20) NULL, 	       			-- 发件人邮编
                                            [SEND_ADDRESS] VARCHAR2(200)  NULL,     			-- 发件人详细地址（上海上海市青浦区华志璐1889号）
                                            [RECEIVE_MAN] VARCHAR2(30)  NULL,       			-- 收件人姓名
                                            [RECEIVE_DESTINATION] VARCHAR2(30) NULL,   			-- 目的地
                                            [RECEIVE_PROVINCE] VARCHAR2(30)  NULL,  			-- 收件省份
                                            [RECEIVE_CITY] VARCHAR2(50)  NULL,      			-- 收件城市
                                            [RECEIVE_COUNTY] VARCHAR2(30)  NULL,    			-- 收件区县
                                            [RECEIVE_PHONE] VARCHAR2(20)  NULL,     			-- 收件人电话或手机	
                                            [RECEIVE_COMPANY] VARCHAR2(30)  NULL,         			-- 收件人单位名称
                                            [RECEIVE_POSTCODE] VARCHAR2(20) NULL, 	   			-- 收件人邮编
                                            [RECEIVE_ADDRESS] VARCHAR2(200)  NULL,  			-- 收件人详细地址（江苏省苏州市平江区现代大厦1001号）
                                            [GOODS_NAME] VARCHAR2(30), 			       			-- 物品类型
                                            [BIG_PEN] VARCHAR2(30), 			       			-- 大头笔
                                            [LENGTH] VARCHAR2(200), 					-- 长
                                            [WIDTH] VARCHAR2(200), 						-- 宽
                                            [HEIGHT] VARCHAR2(200), 					-- 高
                                            [WEIGHT] VARCHAR2(200), 					-- 重量
                                            [PAYMENT_TYPE] VARCHAR2(30) DEFAULT ('现金'),		-- 付款方式
                                            [TRAN_FEE] VARCHAR2(200),        			-- 运费（快递费）
                                            [TOTAL_NUMBER] VARCHAR2(200), 		   		-- 数量
                                            [REMARK] VARCHAR2(100), 				   			-- 备注	
                                            [CREATEUSERNAME] VARCHAR2(30)  NULL,    			-- 创建人姓名
                                            [CREATESITE] VARCHAR2(30)  NULL,        			-- 创建网点
                                            [CREATEON] DATE  NULL, 				   			-- 创建时间
                                            [MODIFIEDUSERNAME] VARCHAR2(30), 		   			-- 修改人姓名
                                            [MODIFIEDSITE] VARCHAR2(30), 			   			-- 修改网点
                                            [MODIFIEDON] DATE						   			-- 修改时间
                                            );
                                            CREATE INDEX [I_Id] ON [ZTO_PRINT_BILL] ([Id]);
                                            ");
                    if (ztoPrintBillEntities.Any())
                    {
                        foreach (var ztoPrintBillEntity in ztoPrintBillEntities)
                        {
                            printBillManager.Add(ztoPrintBillEntity);
                        }
                    }
                    var parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.10.15");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20151025()
        {
            InitZTOTemplateData("申通快递");
            return true;
        }

        public static bool Upgrade20151105()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    dbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD 'ORDER_NUMBER' nvarchar(200); ", ZtoPrintBillEntity.TableName));
                    var parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.11.05");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20151111()
        {
            InitZTOTemplateData("申通快递");
            return true;
        }

        public static bool Upgrade20151212()
        {
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintBackConnectionString))
            {
                try
                {
                    // 加上三个字段
                    BillPrintHelper.DbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD 'TOPAYMENT' DECIMAL(10, 2); ", ZtoPrintBillEntity.TableName));
                    BillPrintHelper.DbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD 'GOODS_PAYMENT' DECIMAL(10, 2); ", ZtoPrintBillEntity.TableName));
                    BillPrintHelper.DbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD 'EXPRESS_ID' nvarchar(200); ", ZtoPrintBillEntity.TableName));
                    var result = Convert.ToInt32(dbHelper.ExecuteScalar("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='ZTO_PRINT_BILL'"));
                    if (result == 0)
                    {
                        // 创建备份库的历史记录表
                        dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_PRINT_BILL] (
                                            [Id] integer PRIMARY KEY, 
                                            [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                            [SEND_DATE] VARCHAR2(30)  NULL, 				    		-- 发件日期
                                            [SEND_SITE] VARCHAR2(30)  NULL,             		-- 发件网点
                                            [SEND_MAN] VARCHAR2(30)  NULL,          			-- 发件人姓名
                                            [SEND_DEPARTURE] VARCHAR2(30) NULL,        			-- 始发地
                                            [SEND_PROVINCE] VARCHAR2(30)  NULL,     			-- 发件省份
                                            [SEND_CITY] VARCHAR2(50)  NULL, 	   	   			-- 发件城市
                                            [SEND_COUNTY] VARCHAR2(30)  NULL, 	   			-- 发件区县
                                            [SEND_COMPANY] VARCHAR2(30)  NULL,         			-- 发件人单位名称
                                            [SEND_DEPARTMENT] VARCHAR2(30)  NULL,      			-- 发件人单位部门
                                            [SEND_PHONE] VARCHAR2(20)  NULL, 	   			-- 发件人电话或手机
                                            [SEND_POSTCODE] VARCHAR2(20) NULL, 	       			-- 发件人邮编
                                            [SEND_ADDRESS] VARCHAR2(200)  NULL,     			-- 发件人详细地址（上海上海市青浦区华志璐1889号）
                                            [RECEIVE_MAN] VARCHAR2(30)  NULL,       			-- 收件人姓名
                                            [RECEIVE_DESTINATION] VARCHAR2(30) NULL,   			-- 目的地
                                            [RECEIVE_PROVINCE] VARCHAR2(30)  NULL,  			-- 收件省份
                                            [RECEIVE_CITY] VARCHAR2(50)  NULL,      			-- 收件城市
                                            [RECEIVE_COUNTY] VARCHAR2(30)  NULL,    			-- 收件区县
                                            [RECEIVE_PHONE] VARCHAR2(20)  NULL,     			-- 收件人电话或手机	
                                            [RECEIVE_COMPANY] VARCHAR2(30)  NULL,         			-- 收件人单位名称
                                            [RECEIVE_POSTCODE] VARCHAR2(20) NULL, 	   			-- 收件人邮编
                                            [RECEIVE_ADDRESS] VARCHAR2(200)  NULL,  			-- 收件人详细地址（江苏省苏州市平江区现代大厦1001号）
                                            [GOODS_NAME] VARCHAR2(30), 			       			-- 物品类型
                                            [BIG_PEN] VARCHAR2(30), 			       			-- 大头笔
                                            [LENGTH] VARCHAR2(200), 					-- 长
                                            [WIDTH] VARCHAR2(200), 						-- 宽
                                            [HEIGHT] VARCHAR2(200), 					-- 高
                                            [WEIGHT] VARCHAR2(200), 					-- 重量
                                            [PAYMENT_TYPE] VARCHAR2(30) DEFAULT ('现金'),		-- 付款方式
                                            [TRAN_FEE] VARCHAR2(200),        			-- 运费（快递费）
                                            [TOTAL_NUMBER] VARCHAR2(200), 		   		-- 数量
                                            [REMARK] VARCHAR2(100), 				   			-- 备注	
                                            [CREATEUSERNAME] VARCHAR2(30)  NULL,    			-- 创建人姓名
                                            [CREATESITE] VARCHAR2(30)  NULL,        			-- 创建网点
                                            [CREATEON] DATE  NULL, 				   			-- 创建时间
                                            [MODIFIEDUSERNAME] VARCHAR2(30), 		   			-- 修改人姓名
                                            [MODIFIEDSITE] VARCHAR2(30), 			   			-- 修改网点
                                            [MODIFIEDON] DATE,				   			-- 修改时间
                                            [EXPRESS_ID] nvarchar(200),				   			-- 快递ID
                                            [TOPAYMENT] DECIMAL(10, 2) DEFAULT (0),                     -- 到付款
                                            [GOODS_PAYMENT] DECIMAL(10, 2) DEFAULT (0),                 -- 代收货款
                                            [ORDER_NUMBER] nvarchar(200));              -- 订单编号
                                            CREATE INDEX [I_Id] ON [ZTO_PRINT_BILL] ([Id]);");
                    }
                    var parameterManager = new BaseParameterManager(BillPrintHelper.DbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.12.12");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static bool Upgrade20160120()
        {
            // Statistics
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    dbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD '{1}' int; ", BaseAreaEntity.TableName, BaseAreaEntity.FieldStatistics));
                    var parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "6.2016.01.20");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 增加回收站功能脚本
        /// </summary>
        /// <returns></returns>
        public static bool Upgrade20160620()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    // 创建回收站表，所有删除的记录都存放在这里
                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_RECYCLE_BILL] (
                                            [Id] integer PRIMARY KEY, 
                                            [DATA_SOURCE] VARCHAR2(20)  NULL, --数据来源，可以是表名称，从哪个表里面删除的，是来自打印记录还是历史记录要记录好，这样如果要恢复回去就可以恢复了
                                            [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                            [SEND_DATE] VARCHAR2(30)  NULL, 				    		-- 发件日期
                                            [SEND_SITE] VARCHAR2(30)  NULL,             		-- 发件网点
                                            [SEND_MAN] VARCHAR2(30)  NULL,          			-- 发件人姓名
                                            [SEND_DEPARTURE] VARCHAR2(30) NULL,        			-- 始发地
                                            [SEND_PROVINCE] VARCHAR2(30)  NULL,     			-- 发件省份
                                            [SEND_CITY] VARCHAR2(50)  NULL, 	   	   			-- 发件城市
                                            [SEND_COUNTY] VARCHAR2(30)  NULL, 	   			-- 发件区县
                                            [SEND_COMPANY] VARCHAR2(30)  NULL,         			-- 发件人单位名称
                                            [SEND_DEPARTMENT] VARCHAR2(30)  NULL,      			-- 发件人单位部门
                                            [SEND_PHONE] VARCHAR2(20)  NULL, 	   			-- 发件人电话或手机
                                            [SEND_POSTCODE] VARCHAR2(20) NULL, 	       			-- 发件人邮编
                                            [SEND_ADDRESS] VARCHAR2(200)  NULL,     			-- 发件人详细地址（上海上海市青浦区华志璐1889号）
                                            [RECEIVE_MAN] VARCHAR2(30)  NULL,       			-- 收件人姓名
                                            [RECEIVE_DESTINATION] VARCHAR2(30) NULL,   			-- 目的地
                                            [RECEIVE_PROVINCE] VARCHAR2(30)  NULL,  			-- 收件省份
                                            [RECEIVE_CITY] VARCHAR2(50)  NULL,      			-- 收件城市
                                            [RECEIVE_COUNTY] VARCHAR2(30)  NULL,    			-- 收件区县
                                            [RECEIVE_PHONE] VARCHAR2(20)  NULL,     			-- 收件人电话或手机	
                                            [RECEIVE_COMPANY] VARCHAR2(30)  NULL,         			-- 收件人单位名称
                                            [RECEIVE_POSTCODE] VARCHAR2(20) NULL, 	   			-- 收件人邮编
                                            [RECEIVE_ADDRESS] VARCHAR2(200)  NULL,  			-- 收件人详细地址（江苏省苏州市平江区现代大厦1001号）
                                            [GOODS_NAME] VARCHAR2(30), 			       			-- 物品类型
                                            [BIG_PEN] VARCHAR2(30), 			       			-- 大头笔
                                            [LENGTH] VARCHAR2(200), 					-- 长
                                            [WIDTH] VARCHAR2(200), 						-- 宽
                                            [HEIGHT] VARCHAR2(200), 					-- 高
                                            [WEIGHT] VARCHAR2(200), 					-- 重量
                                            [PAYMENT_TYPE] VARCHAR2(30) DEFAULT ('现金'),		-- 付款方式
                                            [TRAN_FEE] VARCHAR2(200),        			-- 运费（快递费）
                                            [TOTAL_NUMBER] VARCHAR2(200), 		   		-- 数量
                                            [REMARK] VARCHAR2(100), 				   			-- 备注	
                                            [CREATEUSERNAME] VARCHAR2(30)  NULL,    			-- 创建人姓名
                                            [CREATESITE] VARCHAR2(30)  NULL,        			-- 创建网点
                                            [CREATEON] DATE  NULL, 				   			-- 创建时间
                                            [MODIFIEDUSERNAME] VARCHAR2(30), 		   			-- 修改人姓名
                                            [MODIFIEDSITE] VARCHAR2(30), 			   			-- 修改网点
                                            [MODIFIEDON] DATE,				   			-- 修改时间
                                            [EXPRESS_ID] nvarchar(200),				   			-- 快递ID
                                            [TOPAYMENT] DECIMAL(10, 2) DEFAULT (0),                     -- 到付款
                                            [GOODS_PAYMENT] DECIMAL(10, 2) DEFAULT (0),                 -- 代收货款
                                            [ORDER_NUMBER] nvarchar(200));              -- 订单编号
                                            CREATE INDEX [I1_Id] ON [ZTO_RECYCLE_BILL] ([Id]);");
                    var parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "6.2016.06.20");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 增加打印状态标识
        /// </summary>
        /// <returns></returns>
        public static bool Upgrade20160720()
        {
            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                try
                {
                    dbHelper.ExecuteNonQuery(string.Format(" ALTER TABLE {0} ADD 'PRINT_STATUS' nvarchar(200); ", ZtoPrintBillEntity.TableName));
                    // 创建备份库的历史记录表
                    dbHelper.ExecuteNonQuery(@"CREATE TABLE [ZTO_PRINT_CANCEL] (
                                            [Id] integer PRIMARY KEY, 
                                            [BILL_CODE] VARCHAR2(20)  NULL,             		-- 单号
                                           
                                            [SEND_MAN] VARCHAR2(30)  NULL,          			-- 发件人姓名
                                        
                                            [SEND_PROVINCE] VARCHAR2(30)  NULL,     			-- 发件省份
                                           
                                            [SEND_PHONE] VARCHAR2(20)  NULL, 	   			-- 发件人电话或手机
                                          
                                            [SEND_ADDRESS] VARCHAR2(200)  NULL,     			-- 发件人详细地址（上海上海市青浦区华志璐1889号）
                                            [RECEIVE_MAN] VARCHAR2(30)  NULL,       			-- 收件人姓名
                                          
                                          
                                            [RECEIVE_PHONE] VARCHAR2(20)  NULL,     			-- 收件人电话或手机	
                                          
                                            [RECEIVE_ADDRESS] VARCHAR2(200)  NULL,  			-- 收件人详细地址（江苏省苏州市平江区现代大厦1001号）
                                            
                                            [REMARK] VARCHAR2(100), 				   			-- 备注	
                                            [CREATEUSERNAME] VARCHAR2(30)  NULL,    			-- 创建人姓名
                                            [CREATESITE] VARCHAR2(30)  NULL,        			-- 创建网点
                                            [CREATEON] DATE  NULL, 				   			-- 创建时间
                                            [MODIFIEDUSERNAME] VARCHAR2(30), 		   			-- 修改人姓名
                                            [MODIFIEDSITE] VARCHAR2(30), 			   			-- 修改网点
                                            [MODIFIEDON] DATE,				   			-- 修改时间
                                      
                                            [ORDER_NUMBER] nvarchar(200));              -- 订单编号
                                            CREATE INDEX [I_CANCEL_Id] ON [ZTO_PRINT_CANCEL] ([Id]);");
                    var parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "6.2016.07.20");
                    dbHelper.Dispose();
                    dbHelper.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return false;
                }
            }
        }

        public static void InitExpressData()
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            List<BaseExpressEntity> expressEntities = new List<BaseExpressEntity>();
            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "申通快递",
                ShortName = "zhongtong",
                Layer = "1"
            });
            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "申通快递",
                ShortName = "STO",
                Layer = "2"
            });
            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "圆通快递",
                ShortName = "yuantong",
                Layer = "3"
            });
            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "韵达快递",
                ShortName = "yunda",
                Layer = "4"
            });

            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "宅急送",
                ShortName = "zhaijisong",
                Layer = "5"
            });

            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "如风达",
                ShortName = "rufengda",
                Layer = "6"
            });

            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "全峰快递",
                ShortName = "qfkd",
                Layer = "7"
            });

            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "顺丰快递",
                ShortName = "shunfeng",
                Layer = "8"
            });

            expressEntities.Add(new BaseExpressEntity()
            {
                Name = "天天快递",
                ShortName = "TianTian",
                Layer = "9"
            });
            expressManager.Delete();
            foreach (BaseExpressEntity baseExpressEntity in expressEntities)
            {
                expressManager.Add(baseExpressEntity, true);
            }
            var baseTemplateManager = new BaseTemplateManager(expressManager.DbHelper);
            baseTemplateManager.Delete();
            InitZTOTemplateData("申通快递");
            InitSTOTemplateData("申通快递");
            InitYTOTemplateData("圆通快递");
            InitYUNDATemplateData("韵达快递");
            InitZJSTemplateData("宅急送");
            InitQUANFENGTemplateData("全峰快递");
            InitSFTemplateData("顺丰快递");
            // 少一个如风达的模板，所以没有添加
            InitTianTianTemplateData("天天快递");
        }

        /// <summary>
        /// 初始化申通快递模板
        /// </summary>
        /// <param name="expressName">申通</param>
        public static void InitZTOTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();
            list.Add(new BaseTemplateEntity()
            {
                Name = "主面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZTOMainBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZTOMainBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZTOBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZTOBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "淘宝面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZTOTaoBaoBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZTOTaoBaoBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "COD面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZTOCODBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZTOCODBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "到付面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZTOToPayMentBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZTOToPayMentBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "内部面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZtoInnerBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZtoInnerBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "100*180普通电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto180ElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto180ElecBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "100*180代收电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto180CODElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto180CODElecBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "100*180到付电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto180ToPayMentElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto180ToPayMentElecBill.grf",
                ExpressId = entity.Id
            });
            // 2016年7月20日08:30:44，杨恒连，添加三个模板
            list.Add(new BaseTemplateEntity()
            {
                Name = "76*200普通三联电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto200ElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto200ElecBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "76*200代收三联电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto200CODElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto200CODElecBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "76*200到付三联电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto200ToPayMentElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto200ToPayMentElecBill.grf",
                ExpressId = entity.Id
            });
            // 最老的模板可能要废弃，2016年7月20日08:31:03杨恒连
            list.Add(new BaseTemplateEntity()
            {
                Name = "100*190普通电子面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\Zto190ElecBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\Zto190ElecBill.grf",
                ExpressId = entity.Id
            });
            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            templateManager.Delete(new KeyValuePair<string, object>(BaseTemplateEntity.FieldExpressId, entity.Id));
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化申通快递模板
        /// </summary>
        /// <param name="expressName">申通</param>
        public static void InitSTOTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();

            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\STO.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\STO.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化圆通快递模板
        /// </summary>
        /// <param name="expressName">圆通快递</param>
        public static void InitYTOTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }

            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();

            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\YTO.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\YTO.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化韵达快递模板
        /// </summary>
        /// <param name="expressName">韵达快递</param>
        public static void InitYUNDATemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }

            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();

            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\YUNDA.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\YUNDA.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化宅急送快递模板
        /// </summary>
        /// <param name="expressName">宅急送快递</param>
        public static void InitZJSTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }

            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();

            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\ZJS.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\ZJS.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化全峰快递模板
        /// </summary>
        /// <param name="expressName">全峰快递</param>
        public static void InitQUANFENGTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }

            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();

            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\QF2.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\QF2.grf",
                ExpressId = entity.Id
            });

            list.Add(new BaseTemplateEntity()
            {
                Name = "代收保价面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\QF1.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\QF1.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        /// <summary>
        /// 初始化顺丰快递模板
        /// </summary>
        /// <param name="expressName">顺丰快递</param>
        public static void InitSFTemplateData(string expressName)
        {
            BaseExpressManager expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();
            list.Add(new BaseTemplateEntity()
            {
                Name = "普通面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\SFBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\SFBill.grf",
                ExpressId = entity.Id
            });
            list.Add(new BaseTemplateEntity()
            {
                Name = "顺丰2015面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\SF2015Bill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\SF2015Bill.grf",
                ExpressId = entity.Id
            });
            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }

        public static void InitTianTianTemplateData(string expressName)
        {
            var expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);
            // 根据名称获取到实体
            var entity = expressManager.GetList<BaseExpressEntity>(new KeyValuePair<string, object>(BaseExpressEntity.FieldName, expressName)).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            List<BaseTemplateEntity> list = new List<BaseTemplateEntity>();
            list.Add(new BaseTemplateEntity()
            {
                Name = "主面单",
                BackgroundImagePath = BaseSystemInfo.StartupPath + "\\Images\\SFBill.jpg",
                FilePath = BaseSystemInfo.StartupPath + "\\Template\\SFBill.grf",
                ExpressId = entity.Id
            });

            BaseTemplateManager templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            foreach (BaseTemplateEntity baseTemplateEntity in list)
            {
                templateManager.Add(baseTemplateEntity, true);
            }
        }
    }
}

