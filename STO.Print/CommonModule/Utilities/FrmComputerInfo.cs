using System;
using STO.Print.AddBillForm;
using STO.Print.Utilities;

namespace STO.Print
{
    public partial class FrmComputerInfo : BaseForm
    {
        public FrmComputerInfo()
        {
            InitializeComponent();
        }

        private void FrmComputerInfo_Load(object sender, EventArgs e)
        {
            //string computerName = STO.Print.Utilities.Computer.GetComputerName();
            //string systemType = STO.Print.Utilities.Computer.GetSystemType();
            //string hardSize = STO.Print.Utilities.HardwareInfoHelper.HDVal();
            //string mac = STO.Print.Utilities.Computer.GetMacAddress();
            //string cpu = STO.Print.Utilities.Computer.GetCpuID();
            //string neicun = STO.Print.Utilities.Computer.GetTotalPhysicalMemory();
            //STO.Print.Utilities.MessageUtil.ShowTips(computerName + 
            //    Environment.NewLine+
            //    systemType + Environment.NewLine + hardSize + Environment.NewLine + mac + Environment.NewLine + cpu + Environment.NewLine + neicun);
            HardwareHandler hard = new HardwareHandler();
            txtComputerInfo.Text = hard.CpuInfo() + hard.DiskDriveInfo() + hard.MainBoardInfo() + hard.NetworkInfo() + hard.OsInfo();
        }
    }

}

