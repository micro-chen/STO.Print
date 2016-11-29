//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;

namespace STO.Print.Model
{
    /// <summary>
    /// LoadPrintMarkEntity
    /// 
    /// 修改纪录
    /// 
    /// 2015-08-18 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-08-18</date>
    /// </author>
    /// </summary>
    public class LoadPrintMarkEntity
    {
        /// <summary>
        /// 线程对象
        /// </summary>
        public Thread Looker { get; set; }

        /// <summary>
        /// 线程编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 线程所需要处理的运单实体集合
        /// </summary>
        public List<ZtoPrintBillEntity> PrintBillEntities;
    }
}
