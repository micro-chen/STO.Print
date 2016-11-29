//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Utilities;

    [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
		protected BaseEntity() { }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        public virtual void GetFromExpand(DataRow dr)
        {
			GetFromExpand(new DRDataRow(dr));
		}

        /// <summary>
        /// 从数据流读取
        /// </summary>
        /// <param name="dataReader">数据流</param>
        public virtual void GetFromExpand(IDataReader dataReader)
        {
			GetFromExpand(new DRDataReader(dataReader));
        }

		/// <summary>
		/// 从自定义数据流读取
		/// </summary>
		/// <param name="dataReader">数据流</param>
		public virtual void GetFromExpand(IDataRow dr)
		{
		}

        /// <summary>
        /// 可以按各种特殊需要获取字符串的长度
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>长度</returns>
        private int GetLength(string text)
        {
            // string text = " 【中文】（12.21）(ァぁ)[En] ";
            //  var String_Len = text.Length;
            // var ASCII_Len = Encoding.ASCII.GetBytes(text).Length;
            // var Default_Len = Encoding.Default.GetBytes(text).Length;
            // var BigEndianUnicode_Len = Encoding.BigEndianUnicode.GetBytes(text).Length;
            // var Unicode_Len = Encoding.Unicode.GetBytes(text).Length;
            // var UTF32_Len = Encoding.UTF32.GetBytes(text).Length;
            // var UTF7_Len = Encoding.UTF7.GetBytes(text).Length;
            // var UTF8_Len = Encoding.UTF8.GetBytes(text).Length;
            // var GB2312_Len = Encoding.GetEncoding("GB2312").GetBytes(text).Length;
            return Encoding.GetEncoding("GB2312").GetBytes(text).Length;
        }

        /// <summary>
        /// 后台输入验证
        /// 2013.06.12 JiRiGaLa 完善
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid(out string message)
        {
            bool returnValue = true;
            message = string.Empty;
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string name = propertyInfo.Name;
                object value = propertyInfo.GetValue(this, null);
                if ((propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.Name.StartsWith("String")) && value != null) 
                {
                    object[] validObject = (this.GetType().GetProperty(name).GetCustomAttributes(typeof(StringLengthAttribute), false));
                    if (validObject != null && validObject.Length > 0)
                    {
                        StringLengthAttribute stringLengthAttribute = (StringLengthAttribute)validObject[0];
                        if (stringLengthAttribute.MaximumLength < GetLength(value.ToString()))
                        {
                            returnValue = false;
                            // name 这个可以是返回的字段
                            // value 这个可以是返回的出错的内容
                            // 这里是返回消息
                            message = stringLengthAttribute.ErrorMessage;
                            break;
                        }
                    }
                }
            }
            return returnValue;
        }

		#region IBaseEntity 成员

		protected abstract BaseEntity GetFrom(IDataRow dr);

		public BaseEntity GetFrom(DataRow dr)
		{
			return GetFrom(new DRDataRow(dr));
		}

		public BaseEntity GetFrom(IDataReader dataReader)
		{
			return GetFrom(new DRDataReader(dataReader));
		}

		public BaseEntity GetSingle(DataTable dt)
		{
			if((dt == null) || (dt.Rows.Count == 0))
			{
				return null;
			}
			foreach(DataRow dr in dt.Rows)
			{
				this.GetFrom(dr);
				break;
			}
			return this;
		}

		#endregion

		public static T Create<T>() where T : BaseEntity, new()
		{
			return new T();
		}

		public static T Create<T>(DataTable dt) where T : BaseEntity, new()
		{
			if((dt == null) || (dt.Rows.Count == 0))
			{
				return null;
			}
			T entity = Create<T>();
			entity.GetFrom(dt.Rows[0]);
			return entity;
		}

		public static T Create<T>(DataRow dr) where T : BaseEntity, new()
		{
			T entity = Create<T>();
			entity.GetFrom(dr);
			return entity;
		}

		public static T Create<T>(IDataReader dataReader) where T : BaseEntity, new()
		{
			T entity = Create<T>();
			entity.GetFrom(dataReader);
			return entity;
		}

		public static List<T> GetList<T>(DataTable dt) where T : BaseEntity, new()
		{
			if((dt == null) || (dt.Rows.Count == 0))
			{
				return new List<T>();
			}
			List<T> entites = new List<T>();
			foreach(DataRow dr in dt.Rows)
			{
				entites.Add(Create<T>(dr));
			}
			return entites;
		}
	}
}