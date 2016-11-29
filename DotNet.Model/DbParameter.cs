//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd.
//--------------------------------------------------------------------

using System;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;
    
    [DataContract]
	[KnownType(typeof(DBNull))]
	public class DbParameter
	{
		public DbParameter(string name, object value)
			: this(name, value, ParameterDirection.Input)
		{
		}

		public DbParameter(string name, object value, ParameterDirection parameterDirection)
		{
			Name = name;
			Value = value;
			ParameterDirection = parameterDirection;
		}

		public IDbDataParameter GetIDbDataParameter(IDbHelper dbHelper)
		{
			IDbDataParameter result = dbHelper.MakeParameter(Name, Value);
			result.Direction = ParameterDirection;
			return result;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public object Value { get; set; }

		[DataMember]
		public ParameterDirection ParameterDirection { get; set; }
	}
}
