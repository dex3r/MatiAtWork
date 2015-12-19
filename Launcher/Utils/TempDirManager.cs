using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class TempDirManager : ITempDirManager
    {
        public const string GLOBAL_TEMP_DIR = "NvCorp";

        private string LocalTempDir;
        private string LocalTempPath;
        private string InternalTempPath;
        private string InternalTempDir;

        public string TempDirectoryPath
        {
            get
            {
                return InternalTempPath;
            }
        }

        public TempDirManager(string localPath)
        {
            LocalTempDir = Path.Combine(GLOBAL_TEMP_DIR, localPath);
            LocalTempPath = Path.Combine(Path.GetTempPath(), LocalTempDir);

            do
            {
                string dirName;
                string path = GenerateTempPath(out dirName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                    InternalTempPath = path;
                    InternalTempDir = dirName;

                    break;
                }
            } while (true);
        }

        private string GenerateTempPath(out string dirName)
        {
            Random r = new Random();
            dirName = r.Next(9999, int.MaxValue).ToString();
            string path = Path.Combine(Path.GetTempPath(), LocalTempPath, dirName);

            return path;
        }

        ~TempDirManager()
        {
            try
            {
                if (Directory.Exists(TempDirectoryPath))
                {
                    Directory.Delete(TempDirectoryPath, true);
                }
            }
            catch { }
        }

        public string GetTempDirectoryPath()
        {
            return InternalTempPath;
        }
    }
}
