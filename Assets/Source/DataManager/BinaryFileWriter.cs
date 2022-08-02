using Assets.source.DataManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.DataManager
{
    internal class BinaryFileWriter : FileWriter
    {
        public override Task CreateFile<T>(string filepath, T data)
        {
            if (data == null)
            {
                byte[] bytes = { };
                return File.WriteAllBytesAsync(filepath, bytes);
            }
            else
            {
                byte[] bytes = BinarySerializer.BinarySerialization(data);

                return File.WriteAllBytesAsync(filepath, bytes);
            }
        }

        public override async Task<T> LoadFile<T>(string filepath)
        {
            if (File.Exists(filepath))
            {

               /*using(Stream file = File.Open(filepath, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(file) as T;
                }*/

                byte[] bytes = await File.ReadAllBytesAsync(filepath);

                var output = bytes.BinaryDeserialization<T>();
                return output;


            }
            else
            {
                return null;
            }
        }
    }
}
