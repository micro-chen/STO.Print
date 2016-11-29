//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Runtime;
using System.Runtime.Serialization;

namespace DotNet.Utilities
{
    [Serializable, DataContract]
    public class PageInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        private int currenetPageIndex;

        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        public int CurrenetPageIndex
        {
            get
            {
                return currenetPageIndex;
            }
            set
            {
                currenetPageIndex = value;
            }
        }

        /// <summary>
        /// 每页显示的记录
        /// </summary>
        private int pageSize;

        /// <summary>
        /// 获取或设置每页显示的记录
        /// </summary>
        public int PageSize
        {
            get 
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        private int recordCount;

        /// <summary>
        /// 获取或设置记录总数
        /// </summary>
        public int RecordCount
        {
            get 
            {
                return recordCount;
            }
            set
            {
                recordCount = value;
            }
        }
    }
}
