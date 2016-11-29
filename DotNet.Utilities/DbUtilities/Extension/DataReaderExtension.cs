//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DotNet.Utilities
{
    public static class DataReaderExtension
    {
        public static List<T> ToList<T>(this IDataReader dataReader) where T : new()
        {
            List<T> entites = new List<T>();
            if ((dataReader == null))
            {
                return entites;
            }
            using (dataReader)
            {
                while (dataReader.Read())
                {
                    entites.Add(dataReader.ToObject<T>());
                }
            }
            return entites;
        }

        public static T ToObject<T>(this IDataReader dr) where T : new()
        {
            dynamic dynTemp = new T();
            return dynTemp.GetFrom(dr);
        }
    }
}
