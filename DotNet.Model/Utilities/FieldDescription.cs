//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

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
    public class FieldDescription : Attribute
    {
        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
        }

        /// <summary>
        /// 是否需要记录日志
        /// </summary>
        private bool _needLog;

        public bool NeedLog
        {
            get
            {
                return _needLog;
            }
        }

        public FieldDescription(string text,bool needLog = true)
        {
            _text = text;
            _needLog = needLog;
        }
    }
}