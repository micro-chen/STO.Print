//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
	public interface IDataRow
	{
		object this[string name] { get; }
		object this[int i] { get; }

		bool ContainsColumn(string columnName);
	}
}
