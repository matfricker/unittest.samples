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

            System.Diagnostics.Debug.WriteLine(fileName);

            return File.Exists(fileName);
        }
    }
}
