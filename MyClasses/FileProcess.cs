using System;
using System.IO;

namespace MyClasses
{
    public class FileProcess
    {
        public bool FileExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException();
            }

            return File.Exists(fileName);
        }
    }
}
