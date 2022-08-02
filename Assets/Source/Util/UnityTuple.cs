using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Util
{
    [Serializable]
    public class UnityTuple<K, V>
    {
        public K key;
        public V value;

        public override string ToString()
        {
            return $"{{key: {key}, value: {value}}}";
        }


       
    }
}
