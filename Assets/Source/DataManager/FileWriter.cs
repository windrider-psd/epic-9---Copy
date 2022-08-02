using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.DataManager
{
    internal abstract class FileWriter
    {
        public abstract Task CreateFile<T>(string filepath, T data);

        public abstract Task<T> LoadFile<T>(string filepath) where T: class;

        public Task DeleteFile(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                File.Delete(filepath);
            });
        }
    }
}
