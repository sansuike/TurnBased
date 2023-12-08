using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class DictionaryUtil
    {
        public static V getOrDefault<K, V>(K key, V value, Dictionary<K, V> dic)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return value;
            }
        }

        public static object[] Dic2Arr(IDictionary dic)
        {
            if (dic == null)
            {
                return null;
            }
            object[] arr = new object[dic.Count];
            foreach (DictionaryEntry kv in dic)
            {
                arr[Convert.ToInt32(kv.Key) - 1] = kv.Value;
            }
            return arr;
        }
    }
