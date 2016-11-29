//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Model
{
	public interface IDataRow
	{
		object this[string name] { get; }
		object this[int i] { get; }

		bool ContainsColumn(string columnName);
	}
}
