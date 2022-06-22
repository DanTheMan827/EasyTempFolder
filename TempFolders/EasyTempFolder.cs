/*
   Copyright 2022 Daniel Radtke

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.IO;

namespace DanTheMan827.TempFolders
{
    /// <summary>
    /// The EasyTempFolder class creates an empty temp folder on initialization and deletes it on Dispose.
    /// 
    /// Also included are some static methods for convenience.
    /// </summary>
    public class EasyTempFolder : IDisposable
    {
        /// <summary>
        /// The path to the temporary folder.
        /// </summary>
        public string Path { get; private set; }

        private bool disposedValue = false;
        private bool deleteAfter;

        public static implicit operator string(EasyTempFolder a) => a.Path;
        public override string ToString() => Path;


        /// <summary>
        /// The EasyTempFolder class creates an empty temp folder on initialization and deletes it on Dispose.
        /// </summary>
        /// <param name="prefix">The folder name prefix, or a unique UUID if null is passed.</param>
        /// <param name="baseDir">The directory to create the temp folder in, or the temp folder if null is passed.</param>
        /// <param name="deleteAfter">Whether to delete the folder when the class is disposed.</param>
        public EasyTempFolder(string prefix = null, string baseDir = null, bool deleteAfter = true)
        {
            this.deleteAfter = deleteAfter;
            Path = getUniqueTempPath(baseDir, prefix);
            Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// Private dispose method that will delete the temporary folder specified by the class if deleteAfter is true.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    if (deleteAfter && Path != null && Directory.Exists(Path))
                    {
                        try
                        {
                            Directory.Delete(Path, true);
                        }
                        catch (Exception) { }
                    }
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose of the class and delete the temporary folder of the class if deleteAfter is true.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        private static string getUniqueTempPath(string baseDir, string prefix)
        {
            baseDir = baseDir ?? System.IO.Path.GetTempPath();
            prefix = prefix ?? Guid.NewGuid().ToString(); //Assembly.GetEntryAssembly().EntryPoint.ReflectedType.Namespace;

            int counter = 0;
            string path = System.IO.Path.Combine(baseDir, prefix);

            while (Directory.Exists(path))
            {
                counter++;
                path = System.IO.Path.Combine(baseDir, $"{prefix}.{counter}");
            }

            return path;
        }
    }
}