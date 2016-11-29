//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using STO.Print.Model.ZTOElecBillCountJsonEntityJsonTypes;

namespace STO.Print.Model.ZTOElecBillCountJsonEntityJsonTypes
{

    public class Counter
    {
        public string available { get; set; }
    }

}

namespace STO.Print.Model
{

    /// <summary>
    /// 申通线下电子面单商家ID查询电子面单可用数量接口返回Json解析实体
    /// </summary>
    public class ZTOElecBillCountJsonEntity
    {
        public string result { get; set; }
        public Counter counter { get; set; }
    }

}
