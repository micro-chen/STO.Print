//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Utilities
{
    [Serializable]

    [XmlType(TypeName = "KeyValue")]

    public class KeyValuePaird<K, V>
    {
        public K Key

        { get; set; }

        public V Value

        { get; set; }

        public KeyValuePaird() { ;}

        public KeyValuePaird(K key, V val)
        {

            Key = key;

            Value = val;

        }

    }

    static void Main(string[] args) 
{
   List<KeyValuePair<string, int>> foobar = new List<KeyValuePair<string, int>>();
   foobar.Add(new KeyValuePair<string, int>("test1", 1));
   foobar.Add(new KeyValuePair<string, int>("test2", 2)); 

   XmlSerializer serializer = new XmlSerializer(typeof(List<KeyValuePair<string, int>>));
   using (Stream stm = File.Create("foo.txt"))
   {
      serializer.Serialize(stm, foobar);
   }
}

}
