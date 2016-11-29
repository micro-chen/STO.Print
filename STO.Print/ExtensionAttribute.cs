//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// 缺少编译器要求的成员“System.Runtime.CompilerServices.ExtensionAttribute..ctor” 解决方案
    /// 命名空间不可修改，类名称不可修改
    /// 错误操作：引用了Newtonsoft.Json.Net20.dll文件导致，需要加上这个类就可以解决问题
    /// 解决方案链接：http://www.cnblogs.com/zihuxinyu/archive/2013/05/06/3063181.html
    ///
    /// 修改纪录
    ///
    ///		  2014-09-26 版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-09-26</date>
    /// </author>
    /// </summary>
    class ExtensionAttribute : Attribute
    {

    }
}
