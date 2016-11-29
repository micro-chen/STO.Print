using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 枚举操作公共类
    /// </summary>
    public class EnumHelper
    {
        #region 通过字符串获取枚举成员实例
        /// <summary>
        /// 通过字符串获取枚举成员实例
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        /// <param name="member">枚举成员的常量名或常量值,
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"或"0"获取 Enum1.A 枚举类型</param>
        public static T GetInstance<T>(string member)
        {
            return ConvertHelper.ConvertTo<T>(Enum.Parse(typeof(T), member, true));
        }
        #endregion

        #region 获取枚举成员名称和成员值的键值对集合
        /// <summary>
        /// 获取枚举成员名称和成员值的键值对集合
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        public static Dictionary<string, object> GetMemberKeyValue<T>()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //获取枚举所有成员名称
            string[] memberNames = GetMemberNames<T>();

            //遍历枚举成员
            foreach (string memberName in memberNames)
            {
                dic.Add(memberName, GetMemberValue<T>(memberName));
            }


            //返回哈希表
            return dic;
        }
        #endregion

        #region 获取枚举所有成员名称
        /// <summary>
        /// 获取枚举所有成员名称
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        public static string[] GetMemberNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }
        #endregion

        #region 获取枚举成员的名称
        /// <summary>
        /// 获取枚举成员的名称
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        /// <param name="member">枚举成员实例或成员值,
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入Enum1.A或0,获取成员名称"A"</param>
        public static string GetMemberName<T>(object member)
        {
            //转成基础类型的成员值
            Type underlyingType = GetUnderlyingType(typeof(T));
            object memberValue = ConvertHelper.ConvertTo(member, underlyingType);

            //获取枚举成员的名称
            return Enum.GetName(typeof(T), memberValue);
        }
        #endregion

        #region 获取枚举所有成员值
        /// <summary>
        /// 获取枚举所有成员值
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        public static Array GetMemberValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }
        #endregion

        #region 获取枚举成员的值
        /// <summary>
        /// 获取枚举成员的值
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        /// <param name="memberName">枚举成员的常量名,
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"获取0</param>
        public static object GetMemberValue<T>(string memberName)
        {
            //获取基础类型
            Type underlyingType = GetUnderlyingType(typeof(T));

            //获取枚举实例
            T instance = GetInstance<T>(memberName);

            //获取枚举成员的值
            return ConvertHelper.ConvertTo(instance, underlyingType);
        }
        #endregion

        #region 获取枚举的基础类型
        /// <summary>
        /// 获取枚举的基础类型
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        public static Type GetUnderlyingType(Type enumType)
        {
            //获取基础类型
            return Enum.GetUnderlyingType(enumType);
        }
        #endregion

        #region 检测枚举是否包含指定成员
        /// <summary>
        /// 检测枚举是否包含指定成员
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        /// <param name="member">枚举成员名或成员值</param>
        public static bool IsDefined<T>(string member)
        {
            return Enum.IsDefined(typeof(T), member);
        }
        #endregion

        /// <summary>
        /// 返回指定枚举类型的指定值的描述
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="v">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(System.Type t, object v)
        {
            try
            {
                FieldInfo fi = t.GetField(GetName(t, v));
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        private static string GetName(System.Type t, object v)
        {
            try
            {
                return Enum.GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        public static SortedList GetStatus(System.Type t)
        {
            SortedList list = new SortedList();

            Array a = Enum.GetValues(t);
            for (int i = 0; i < a.Length; i++)
            {
                string enumName = a.GetValue(i).ToString();
                int enumKey = (int)System.Enum.Parse(t, enumName);
                string enumDescription = GetDescription(t, enumKey);
                list.Add(enumKey, enumDescription);
            }
            return list;
        }
    }
}
