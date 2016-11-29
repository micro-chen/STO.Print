using System;
using System.Data;
using System.Data.Common;

namespace WHC.OrderWater.Commons
{
	/// <summary> 
	/// 根据各种不同数据库生成不同分页语句的辅助类 
	/// </summary> 
	public class PagerHelper
	{
		#region 成员变量

		private string tableName;//表名
		private string fieldsToReturn = "*";//需要返回的列
		private string fieldNameToSort = string.Empty;//排序字段名称
		private int pageSize = 10;//页尺寸,就是一页显示多少条记录
		private int pageIndex = 1;//当前的页码
		private bool isDescending = false;//是否以降序排列
		private string strwhere = string.Empty;//检索条件(注意: 不要加 where)

		#endregion

		#region 属性对象

		/// <summary>
		/// 表名
		/// </summary>
		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

		/// <summary>
		/// 需要返回的列
		/// </summary>
		public string FieldsToReturn
		{
			get { return fieldsToReturn; }
			set { fieldsToReturn = value; }
		}

		/// <summary>
		/// 排序字段名称
		/// </summary>
		public string FieldNameToSort
		{
			get { return fieldNameToSort; }
			set { fieldNameToSort = value; }
		}

		/// <summary>
		/// 页尺寸,就是一页显示多少条记录
		/// </summary>
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value; }
		}

		/// <summary>
		/// 当前的页码
		/// </summary>
		public int PageIndex
		{
			get { return pageIndex; }
			set { pageIndex = value; }
		}

		/// <summary>
		/// 是否以降序排列结果
		/// </summary>
		public bool IsDescending
		{
			get { return isDescending; }
			set { isDescending = value; }
		}

		/// <summary>
		/// 检索条件(注意: 不要加 where)
		/// </summary>
		public string StrWhere
		{
			get { return strwhere; }
			set { strwhere = value; }
		}

		#endregion

		#region 构造函数

        /// <summary>
        /// 默认构造函数，其他通过属性设置
        /// </summary>
        public PagerHelper()
        {
        }

		/// <summary>
		/// 完整的构造函数,可以包含条件,返回记录字段等条件
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="fieldsToReturn">需要返回的列</param>
		/// <param name="fieldNameToSort">排序字段名称</param>
		/// <param name="pageSize">页尺寸</param>
		/// <param name="pageIndex">当前的页码</param>
		/// <param name="isDescending">是否以降序排列</param>
		/// <param name="strwhere">检索条件</param>
		/// <param name="connectionString">连接字符串</param>
		public PagerHelper(string tableName, string fieldsToReturn, string fieldNameToSort, 
			int pageSize, int pageIndex, bool isDescending, string strwhere)
		{
			this.tableName = tableName;
			this.fieldsToReturn = fieldsToReturn;
			this.fieldNameToSort = fieldNameToSort;
			this.pageSize = pageSize;
			this.pageIndex = pageIndex;
			this.isDescending = isDescending;
			this.strwhere = strwhere;
		}

		#endregion

        /// <summary>
        /// 不依赖于存储过程的分页(Oracle)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
        /// <returns></returns>
        private string GetOracleSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//执行总数统计
            {
                sql = string.Format("select count(*) as Total from {0} Where {1} ", this.tableName, this.strwhere);
            }
            else
            {
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");
                
                int minRow = pageSize * (pageIndex - 1);
                int maxRow = pageSize * pageIndex;
                string selectSql = string.Format("select {0} from {1} Where {2} {3}", fieldsToReturn, tableName, this.strwhere, strOrder);
                sql = string.Format(@"select b.* from
                           (select a.*, rownum as rowIndex from({2}) a) b
                           where b.rowIndex > {0} and b.rowIndex <= {1}", minRow, maxRow, selectSql);
            }

            return sql;
        }

        /// <summary>
        /// 不依赖于存储过程的分页(SqlServer)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
        /// <returns></returns>
        private string GetSqlServerSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//执行总数统计
            {
                sql = string.Format("select count(*) as Total from [{0}] Where {1} ", this.tableName, this.strwhere);
            }
            else
            {
                string strTemp = string.Empty;
                string strOrder = string.Empty;
                if (this.isDescending)
                {
                    strTemp = "<(select min";
                    strOrder = string.Format(" order by [{0}] desc", this.fieldNameToSort);
                }
                else
                {
                    strTemp = ">(select max";
                    strOrder = string.Format(" order by [{0}] asc", this.fieldNameToSort);
                }

                sql = string.Format("select top {0} {1} from [{2}] ", this.pageSize, this.fieldsToReturn, this.tableName);

                //如果是第一页就执行以上代码，这样会加快执行速度
                if (this.pageIndex == 1)
                {
                    sql += string.Format(" Where {0} ", this.strwhere);
                    sql += strOrder;
                }
                else
                {
                    sql += string.Format(" Where [{0}] {1} ([{0}]) from (select top {2} [{0}] from [{3}] where {5} {4} ) as tblTmp) and {5} {4}",
                        this.fieldNameToSort, strTemp, (this.pageIndex - 1) * this.pageSize, this.tableName, strOrder, this.strwhere);
                }
            }

            return sql;
        }
                
        /// <summary>
        /// 不依赖于存储过程的分页(Access)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
        /// <returns></returns>
        private string GetAccessSql(bool isDoCount)
        {
            return GetSqlServerSql(isDoCount);
        }

        /// <summary>
        /// 不依赖于存储过程的分页(MySql)
        /// </summary>
        /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
        /// <returns></returns>
        private string GetMySqlSql(bool isDoCount)
        {
            string sql = "";
            if (string.IsNullOrEmpty(this.strwhere))
            {
                this.strwhere = " (1=1) ";
            }

            if (isDoCount)//执行总数统计
            {
                sql = string.Format("select count(*) as Total from [{0}] Where {1} ", this.tableName, this.strwhere);
            }
            else
            {
                //SELECT * FROM 表名称 LIMIT M,N 
                string strOrder = string.Format(" order by {0} {1}", this.fieldNameToSort, this.isDescending ? "DESC" : "ASC");

                int minRow = pageSize * (pageIndex - 1);
                int maxRow = pageSize * pageIndex;
                sql = string.Format("select {0} from {1} Where {2} {3} LIMIT {4},{5}", 
                    fieldsToReturn, tableName, this.strwhere, strOrder, maxRow, minRow);
            }

            return sql;
        }

        /// <summary>
        /// 获取对应数据库的分页语句
        /// </summary>
        /// <param name="dbType">数据库类型枚举</param>
        /// <param name="isDoCount">如果isDoCount为True，返回总数统计Sql；否则返回分页语句Sql</param>
        public string GetPagingSql(DatabaseType dbType, bool isDoCount)
        {
            string sql = "";
            switch (dbType)
            {
                case DatabaseType.Access:
                    sql = GetAccessSql(isDoCount);
                    break;
                case DatabaseType.SqlServer:
                    sql = GetSqlServerSql(isDoCount);
                    break;
                case DatabaseType.Oracle:
                    sql = GetOracleSql(isDoCount);
                    break;
                case DatabaseType.MySql:
                    sql = GetMySqlSql(isDoCount);
                    break;
            }
            return sql;
        }
	}

    public enum DatabaseType{ SqlServer,Oracle,Access,MySql }
} 
