//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DotNet.Business
{
    using DotNet.Utilities;
    
    public class ServiceParameter
	{
		private BaseUserInfo userInfo = null;

		private MethodBase currentMethod = null;

		private string appMessage = string.Empty;

		private string serviceName = string.Empty;

		private bool isAddLog = true;

		public static ServiceParameter CreateWithMessage(BaseUserInfo userInfo, MethodBase currentMethod, string serviceName, string appMessage)
		{
			return new ServiceParameter(userInfo, currentMethod) 
				{ ServiceName = serviceName, AppMessage = appMessage };
		}

		public static ServiceParameter CreateWithOutMessage(BaseUserInfo userInfo, MethodBase currentMethod, string serviceName)
		{
			return new ServiceParameter(userInfo, currentMethod) 
				{ ServiceName = serviceName };
		}

		public static ServiceParameter CreateWithLog(BaseUserInfo userInfo, MethodBase currentMethod)
		{
			return new ServiceParameter(userInfo, currentMethod) { IsAddLog = false };
		}

		private ServiceParameter(BaseUserInfo userInfo, MethodBase currentMethod)
		{
			this.userInfo = userInfo;
			this.currentMethod = currentMethod;
		}

		public string AppMessage
		{
			set { appMessage = value; }
			get { return appMessage; }
		}

		public string ServiceName
		{
			set { serviceName = value; }
			get { return serviceName; }
		}

		public bool IsAddLog
		{
			set { isAddLog = value; }
			get { return isAddLog; }
		}

		public BaseUserInfo UserInfo
		{
			set { userInfo = value; }
			get { return userInfo; }
		}

		public MethodBase CurrentMethod
		{
			set { currentMethod = value; }
			get { return currentMethod; }
		}
	}
}
