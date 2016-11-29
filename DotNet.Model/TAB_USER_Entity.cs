//-----------------------------------------------------------------------
// <copyright file="TAB_USER_Entity.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// TAB_USER_Entity
    /// 用户
    /// 
    /// 修改纪录
    /// 
    /// 2013-11-23 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-11-23</date>
    /// </author>
    /// </summary>
    public partial class TAB_USER_Entity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 主键
        /// </summary>
        public Decimal ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private String comput_id = string.Empty;
        /// <summary>
        /// 与该用户相应的机器ID，以此限制一个用户只能在第一次登录的电脑上使用
        /// </summary>
        public String COMPUT_ID
        {
            get
            {
                return comput_id;
            }
            set
            {
                comput_id = value;
            }
        }

        private String position_name = string.Empty;
        /// <summary>
        /// 用户的职位
        /// </summary>
        public String POSITION_NAME
        {
            get
            {
                return position_name;
            }
            set
            {
                position_name = value;
            }
        }

        private String real_name = string.Empty;
        /// <summary>
        /// 用户的真实姓名
        /// </summary>
        public String REAL_NAME
        {
            get
            {
                return real_name;
            }
            set
            {
                real_name = value;
            }
        }

        private String employee_code = string.Empty;
        /// <summary>
        /// 员工编号
        /// </summary>
        public String EMPLOYEE_CODE
        {
            get
            {
                return employee_code;
            }
            set
            {
                employee_code = value;
            }
        }

        private String owner_role = string.Empty;
        /// <summary>
        /// 所属角色名称
        /// </summary>
        public String OWNER_ROLE
        {
            get
            {
                return owner_role;
            }
            set
            {
                owner_role = value;
            }
        }

        private Decimal bl_rec_placard;
        /// <summary>
        /// 该用户是否能接收公告
        /// </summary>
        public Decimal BL_REC_PLACARD
        {
            get
            {
                return bl_rec_placard;
            }
            set
            {
                bl_rec_placard = value;
            }
        }

        private Decimal bl_check_name;
        /// <summary>
        /// 是否验证通过
        /// </summary>
        public Decimal BL_CHECK_NAME
        {
            get
            {
                return bl_check_name;
            }
            set
            {
                bl_check_name = value;
            }
        }

        private String user_site = string.Empty;
        /// <summary>
        /// 此用户能查询监控的网点
        /// </summary>
        public String USER_SITE
        {
            get
            {
                return user_site;
            }
            set
            {
                user_site = value;
            }
        }

        private Decimal roleid;
        /// <summary>
        /// ROLEID
        /// </summary>
        public Decimal ROLEID
        {
            get
            {
                return roleid;
            }
            set
            {
                roleid = value;
            }
        }

        private String modifier = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public String MODIFIER
        {
            get
            {
                return modifier;
            }
            set
            {
                modifier = value;
            }
        }

        private Decimal user_webpower;
        /// <summary>
        /// 客户自己添加的字段
        /// </summary>
        public Decimal USER_WEBPOWER
        {
            get
            {
                return user_webpower;
            }
            set
            {
                user_webpower = value;
            }
        }

        private String owner_center = string.Empty;
        /// <summary>
        /// 所属财务中心
        /// </summary>
        public String OWNER_CENTER
        {
            get
            {
                return owner_center;
            }
            set
            {
                owner_center = value;
            }
        }

        private String modifier_code = string.Empty;
        /// <summary>
        /// 修改编号
        /// </summary>
        public String MODIFIER_CODE
        {
            get
            {
                return modifier_code;
            }
            set
            {
                modifier_code = value;
            }
        }

        private DateTime modifier_date;
        /// <summary>
        /// 修改编号
        /// </summary>
        public DateTime MODIFIER_DATE
        {
            get
            {
                return modifier_date;
            }
            set
            {
                modifier_date = value;
            }
        }

        private DateTime check_name_date;
        /// <summary>
        /// 修改编号
        /// </summary>
        public DateTime CHECK_NAME_DATE
        {
            get
            {
                return check_name_date;
            }
            set
            {
                check_name_date = value;
            }
        }

        private DateTime user_date;
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime USER_DATE
        {
            get
            {
                return user_date;
            }
            set
            {
                user_date = value;
            }
        }

        private String create_site = string.Empty;
        /// <summary>
        /// 创建此用户的站点名称
        /// </summary>
        public String CREATE_SITE
        {
            get
            {
                return create_site;
            }
            set
            {
                create_site = value;
            }
        }

        private String im_name = string.Empty;
        /// <summary>
        /// 即时沟通工具帐号名
        /// </summary>
        public String IM_NAME
        {
            get
            {
                return im_name;
            }
            set
            {
                im_name = value;
            }
        }

        private String user_part = string.Empty;
        /// <summary>
        /// 客户自己添加的字段
        /// </summary>
        public String USER_PART
        {
            get
            {
                return user_part;
            }
            set
            {
                user_part = value;
            }
        }

        private Decimal bl_check_computer;
        /// <summary>
        /// 登陆是否检查机器码(1：表示检查)
        /// </summary>
        public Decimal BL_CHECK_COMPUTER
        {
            get
            {
                return bl_check_computer;
            }
            set
            {
                bl_check_computer = value;
            }
        }

        private String dept_name = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        public String DEPT_NAME
        {
            get
            {
                return dept_name;
            }
            set
            {
                dept_name = value;
            }
        }

        //private String user_password = string.Empty;
        ///// <summary>
        ///// 用户密码
        ///// </summary>
        //public String USER_PASSWORD
        //{
        //    get
        //    {
        //        return user_password;
        //    }
        //    set
        //    {
        //        user_password = value;
        //    }
        //}

        private String user_passwd = string.Empty;
        /// <summary>
        /// Md5加密后密码
        /// </summary>
        public String USER_PASSWD
        {
            get
            {
                return user_passwd;
            }
            set
            {
                user_passwd = value;
            }
        }

        private String call_center_id = string.Empty;
        /// <summary>
        /// 对应的呼叫中心座席号
        /// </summary>
        public String CALL_CENTER_ID
        {
            get
            {
                return call_center_id;
            }
            set
            {
                call_center_id = value;
            }
        }

        private String employee_name = string.Empty;
        /// <summary>
        /// 员工名称
        /// </summary>
        public String EMPLOYEE_NAME
        {
            get
            {
                return employee_name;
            }
            set
            {
                employee_name = value;
            }
        }

        private Decimal deletionstatecode;
        /// <summary>
        /// DELETIONSTATECODE
        /// </summary>
        public Decimal DELETIONSTATECODE
        {
            get
            {
                return deletionstatecode;
            }
            set
            {
                deletionstatecode = value;
            }
        }

        private String owner_group = string.Empty;
        /// <summary>
        /// 所属组名
        /// </summary>
        public String OWNER_GROUP
        {
            get
            {
                return owner_group;
            }
            set
            {
                owner_group = value;
            }
        }

        private String modify_site = string.Empty;
        /// <summary>
        /// 修改站点
        /// </summary>
        public String MODIFY_SITE
        {
            get
            {
                return modify_site;
            }
            set
            {
                modify_site = value;
            }
        }

        private Decimal bl_lock_flag;
        /// <summary>
        /// 1启用0不启用
        /// </summary>
        public Decimal BL_LOCK_FLAG
        {
            get
            {
                return bl_lock_flag;
            }
            set
            {
                bl_lock_flag = value;
            }
        }

        private String owner_site = string.Empty;
        /// <summary>
        /// 所属网点(如果是角色那么此值就是角色,如果BL_TYPE是2此值为系统管理员)
        /// </summary>
        public String OWNER_SITE
        {
            get
            {
                return owner_site;
            }
            set
            {
                owner_site = value;
            }
        }

        private DateTime create_date;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CREATE_DATE
        {
            get
            {
                return create_date;
            }
            set
            {
                create_date = value;
            }
        }

        private String create_user = string.Empty;
        /// <summary>
        /// 创建此用户的用户名称
        /// </summary>
        public String CREATE_USER
        {
            get
            {
                return create_user;
            }
            set
            {
                create_user = value;
            }
        }

        private String user_name = string.Empty;
        
        /// <summary>
        /// 用户名
        /// </summary>
         [Display(Name = "k8登录用户名")] 
        public String USER_NAME
        {
            get
            {
                return user_name;
            }
            set
            {
                user_name = value;
            }
        }

        private String remark = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
       [Display(Name = "备注")] 
        public String REMARK
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        private Decimal bl_ao;
        /// <summary>
        /// 1进入AO  0禁止
        /// </summary>
        [Display(Name = "进入AO标识")] 
        public Decimal BL_AO
        {
            get
            {
                return bl_ao;
            }
            set
            {
                bl_ao = value;
            }
        }

        private Decimal bl_type;
        /// <summary>
        /// 0表示一般用户1表示角色2表示系统管理员(取用户的时候不取2的数据：即系统管理员不由程序操作，而是直接在数据库操作)
        /// </summary>
        public Decimal BL_TYPE
        {
            get
            {
                return bl_type;
            }
            set
            {
                bl_type = value;
            }
        }

        private String loginname = string.Empty;
        /// <summary>
        /// 登陆名
        /// </summary>
        public String LOGINNAME
        {
            get
            {
                return loginname;
            }
            set
            {
                loginname = value;
            }
        }


        private String mobile = string.Empty;
        /// <summary>
        /// 登陆名
        /// </summary>
        public String MOBILE
        {
            get
            {
                return mobile;
            }
            set
            {
                mobile = value;
            }
        }

        private String only_user_name = string.Empty;
        /// <summary>
        /// 登陆名
        /// </summary>
        public String ONLY_USER_NAME
        {
            get
            {
                return only_user_name;
            }
            set
            {
                only_user_name = value;
            }
        }


        private String id_card = string.Empty;
        /// <summary>
        /// 登陆名
        /// </summary>
        public String ID_CARD
        {
            get
            {
                return id_card;
            }
            set
            {
                id_card = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            ID = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_ID]);
            COMPUT_ID = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_COMPUT_ID]);
            EMPLOYEE_CODE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_EMPLOYEE_CODE]);
            OWNER_ROLE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_OWNER_ROLE]);
            BL_REC_PLACARD = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_REC_PLACARD]);
            USER_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_USER_SITE]);
            ROLEID = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_ROLEID]);
            MODIFIER = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_MODIFIER]);
            USER_WEBPOWER = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_USER_WEBPOWER]);
            OWNER_CENTER = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_OWNER_CENTER]);
            MODIFIER_CODE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_MODIFIER_CODE]);
            CREATE_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_CREATE_SITE]);
            IM_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_IM_NAME]);
            USER_PART = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_USER_PART]);
            BL_CHECK_COMPUTER = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_CHECK_COMPUTER]);
            DEPT_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_DEPT_NAME]);
            //USER_PASSWORD = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_USER_PASSWORD]);
            USER_PASSWD = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_USER_PASSWD]);
            CALL_CENTER_ID = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_CALL_CENTER_ID]);
            EMPLOYEE_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_EMPLOYEE_NAME]);
            DELETIONSTATECODE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_DELETIONSTATECODE]);
            OWNER_GROUP = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_OWNER_GROUP]);
            MODIFY_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_MODIFY_SITE]);
            BL_LOCK_FLAG = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_LOCK_FLAG]);
            OWNER_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_OWNER_SITE]);
            CREATE_DATE = BaseBusinessLogic.ConvertToDateTime(dr[TAB_USER_Entity.Field_CREATE_DATE]);
            CREATE_USER = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_CREATE_USER]);
            USER_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_USER_NAME]);
            REMARK = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_REMARK]);
            BL_AO = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_AO]);
            BL_TYPE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_TYPE]);
            LOGINNAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_LOGINNAME]);
            OWNER_GROUP = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_OWNER_GROUP]);
            MOBILE = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_MOBILE]);
            ONLY_USER_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_ONLY_USER_NAME]);
            ID_CARD = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_ID_CARD]);
            MODIFIER_DATE = BaseBusinessLogic.ConvertToDateTime(dr[TAB_USER_Entity.Field_MODIFIER_DATE]);


            CHECK_NAME_DATE = BaseBusinessLogic.ConvertToDateTime(dr[TAB_USER_Entity.Field_CHECK_NAME_DATE]);
            BL_CHECK_NAME = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USER_Entity.Field_BL_CHECK_NAME]);
            POSITION_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_POSITION_NAME]);
            REAL_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USER_Entity.Field_REAL_NAME]);
            USER_DATE = BaseBusinessLogic.ConvertToDateTime(dr[TAB_USER_Entity.Field_USER_DATE]);
            return this;
        }

        ///<summary>
        /// 用户
        ///</summary>
        public static string TableName = "TAB_USER";

        ///<summary>
        /// 主键
        ///</summary>
        public static string Field_ID = "ID";

        ///<summary>
        /// 与该用户相应的机器ID，以此限制一个用户只能在第一次登录的电脑上使用
        ///</summary>
        public static string Field_COMPUT_ID = "COMPUT_ID";

        ///<summary>
        /// 员工编号
        ///</summary>
        public static string Field_EMPLOYEE_CODE = "EMPLOYEE_CODE";

        ///<summary>
        /// 所属角色名称
        ///</summary>
        public static string Field_OWNER_ROLE = "OWNER_ROLE";

        ///<summary>
        /// 该用户是否能接收公告
        ///</summary>
        public static string Field_BL_REC_PLACARD = "BL_REC_PLACARD";

        ///<summary>
        /// 此用户能查询监控的网点
        ///</summary>
        public static string Field_USER_SITE = "USER_SITE";

        ///<summary>
        /// ROLEID
        ///</summary>
        public static string Field_ROLEID = "ROLEID";

        ///<summary>
        /// 修改人
        ///</summary>
        public static string Field_MODIFIER = "MODIFIER";

        ///<summary>
        /// 客户自己添加的字段
        ///</summary>
        public static string Field_USER_WEBPOWER = "USER_WEBPOWER";

        ///<summary>
        /// 所属财务中心
        ///</summary>
        public static string Field_OWNER_CENTER = "OWNER_CENTER";

        ///<summary>
        /// 修改编号
        ///</summary>
        public static string Field_MODIFIER_CODE = "MODIFIER_CODE";

        ///<summary>
        /// 创建此用户的站点名称
        ///</summary>
        public static string Field_CREATE_SITE = "CREATE_SITE";

        ///<summary>
        /// 即时沟通工具帐号名
        ///</summary>
        public static string Field_IM_NAME = "IM_NAME";

        ///<summary>
        /// 客户自己添加的字段
        ///</summary>
        public static string Field_USER_PART = "USER_PART";

        ///<summary>
        /// 登陆是否检查机器码(1：表示检查)
        ///</summary>
        public static string Field_BL_CHECK_COMPUTER = "BL_CHECK_COMPUTER";

        ///<summary>
        /// 部门名称
        ///</summary>
        public static string Field_DEPT_NAME = "DEPT_NAME";

        ///<summary>
        /// 用户密码
        ///</summary>
        public static string Field_USER_PASSWORD = "USER_PASSWORD";

        ///<summary>
        /// 对应的呼叫中心座席号
        ///</summary>
        public static string Field_CALL_CENTER_ID = "CALL_CENTER_ID";

        ///<summary>
        /// 员工名称
        ///</summary>
        public static string Field_EMPLOYEE_NAME = "EMPLOYEE_NAME";

        ///<summary>
        /// DELETIONSTATECODE
        ///</summary>
        public static string Field_DELETIONSTATECODE = "DELETIONSTATECODE";

        ///<summary>
        /// 所属组名
        ///</summary>
        public static string Field_OWNER_GROUP = "OWNER_GROUP";

        ///<summary>
        /// 修改站点
        ///</summary>
        public static string Field_MODIFY_SITE = "MODIFY_SITE";

        ///<summary>
        /// 1启用0不启用
        ///</summary>
        public static string Field_BL_LOCK_FLAG = "BL_LOCK_FLAG";

        ///<summary>
        /// 所属网点(如果是角色那么此值就是角色,如果BL_TYPE是2此值为系统管理员)
        ///</summary>
        public static string Field_OWNER_SITE = "OWNER_SITE";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string Field_CREATE_DATE = "CREATE_DATE";

        ///<summary>
        /// 创建此用户的用户名称
        ///</summary>
        public static string Field_CREATE_USER = "CREATE_USER";

        ///<summary>
        /// 用户名
        ///</summary>
        public static string Field_USER_NAME = "USER_NAME";

        ///<summary>
        /// 备注
        ///</summary>
        public static string Field_REMARK = "REMARK";

        ///<summary>
        /// 1进入AO  0禁止
        ///</summary>
        public static string Field_BL_AO = "BL_AO";

        ///<summary>
        /// 0表示一般用户1表示角色2表示系统管理员(取用户的时候不取2的数据：即系统管理员不由程序操作，而是直接在数据库操作)
        ///</summary>
        public static string Field_BL_TYPE = "BL_TYPE";

        ///<summary>
        /// 登陆名
        ///</summary>
        public static string Field_LOGINNAME = "LOGINNAME";

        public static string Field_USER_PASSWD = "USER_PASSWD";

        /// <summary>
        ///手机号码
        /// </summary>
        public static string Field_MOBILE = "MOBILE";

        /// <summary>
        ///全网唯一用户名
        /// </summary>
        public static string Field_ONLY_USER_NAME = "ONLY_USER_NAME";

        /// <summary>
        ///身份证号
        /// </summary>
        public static string Field_ID_CARD = "ID_CARD";

        /// <summary>
        ///修改时间
        /// </summary>
        public static string Field_MODIFIER_DATE = "MODIFIER_DATE";

        /// <summary>
        ///修改时间
        /// </summary>
        public static string Field_USER_DATE = "USER_DATE";

        /// <summary>
        ///审核姓名时间
        /// </summary>
        public static string Field_CHECK_NAME_DATE = "CHECK_NAME_DATE";

        /// <summary>
        ///是否验证
        /// </summary>
        public static string Field_BL_CHECK_NAME = "BL_CHECK_NAME";

        /// <summary>
        ///职位名称
        /// </summary>
        public static string Field_POSITION_NAME = "POSITION_NAME";

        /// <summary>
        ///真实姓名
        /// </summary>
        public static string Field_REAL_NAME = "REAL_NAME";
    }
}
