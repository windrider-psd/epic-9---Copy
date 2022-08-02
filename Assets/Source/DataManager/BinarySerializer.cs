using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Assets.source.DataManager
{
    public static class BinarySerializer
    {
        private static readonly BinaryFormatter binaryFormatter = new();
        public static byte[] BinarySerialization(this object @object)
        {
            MemoryStream memoryStream = new();
            binaryFormatter.Serialize(memoryStream, @object);
            memoryStream.Close();
            return memoryStream.ToArray();
        }

        public static T BinaryDeserialization<T>(this byte[] byteArray) where T: class
        {

            MemoryStream memoryStream = new(byteArray);
            T @object = binaryFormatter.Deserialize(memoryStream) as T;
            memoryStream.Close();
            return @object;
        }
    }
}
