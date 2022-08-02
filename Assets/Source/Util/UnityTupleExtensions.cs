using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Util
{
    
    public static class UnityTupleExtensions
    {

        public static V GetUnityTupleValue<K, V>(this List<UnityTuple<K,V>> list, K key)
        {
            foreach (var kv in list)
            {
                
                if (kv.key.Equals(key))
                {
                    
                    return kv.value;
                }
            }
            return default;
        }
       
    }
}
