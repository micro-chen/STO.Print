//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Reflection;

namespace DotNet.Model
{
    /// <summary>
    /// FieldDescription
    /// 字段说明。
    /// 
    /// 修改记录
    /// 
    ///		2014.12.01 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.12.01</date>
    /// </author> 
    /// </summary>    
    public static class FieldExtensions
    {
        public static string ToDescription(this string enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(FieldDescription), false);
                if (null != attrs && attrs.Length > 0)
                {
                    return ((FieldDescription)attrs[0]).Text;
                }
            }
            return enumeration.ToString();
        }

        #region 获取指定类别的自定义属性
        /// <summary>
        /// 获取指定类别的自定义属性
        /// </summary>
        /// <param name="entityType">实体类类别</param>
        /// <param name="fieldName">实体类属性名称</param>
        /// <param name="attributeType">自定义类别</param>
        /// <returns></returns>
        public static object GetCustomAttribute(Type entityType, string fieldName, Type attributeType)
        {
            FieldInfo fieldInfo = entityType.GetField(fieldName);
            if (fieldInfo != null)
            {
                var attribute = fieldInfo.GetCustomAttributes(attributeType, false);
                if (attribute.Length > 0)
                {
                    return attribute[0];
                }
            }
            return null;
        }
        #endregion

        #region 获取字段描述
        /// <summary>
        /// 获取字段描述
        /// </summary>
        /// <param name="entityType">实体类类别</param>
        /// <param name="fieldName">实体类属性名称</param>
        /// <returns></returns>
        public static string ToDescription(Type entityType, string fieldName)
        {
            FieldDescription obj = GetCustomAttribute(entityType, fieldName, typeof(FieldDescription)) as FieldDescription;
            return obj != null ? obj.Text : string.Empty;
        }
        #endregion
    }
}