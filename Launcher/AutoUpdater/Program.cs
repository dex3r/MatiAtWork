using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoUpdater
{
    enum Stage : int
    {
        Start = 0,
        Unzip = 1,
        Backup,
        Delete,
        CopyNewFiles,
        FindExecutable,

        Finished,
    }

    class Program
    {
        static int sourceProcessID;
        static string targetUpdateZip;
        static string targetExeFile;
        static string sourceExeFile;

        static string myPath;
        static string fullZipPath;
        static string backupPath;

        static TempDirManager temp;
        static Stage lastStage = Stage.Start;

        static void Main(string[] args)
        {
            temp = new TempDirManager("Launcher\\Updater");

            try
            {
                Update(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while updating:\n" + ex.ToString());
            }

            try
            {
                Rollback(lastStage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while rollbacking:\n" + ex.ToString());
            }
            
            // TODO: Execute old exe if updating failed
        }

        private static bool Update(string[] args)
        {
            TempDirManager temp = new TempDirManager("Launcher\\Updater");

            int expectedArgsCount = 4;
            if (args.Length != expectedArgsCount)
            {
                Console.WriteLine("Invalid number of arguments. Got " + args.Length + " expected " + expectedArgsCount);
                return false;
            }

            sourceProcessID = int.Parse(args[0]);
            targetUpdateZip = args[1];
            targetExeFile = args[2];
            sourceExeFile = args[3];

            string myExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            myPath = Path.GetPathRoot(myExePath);
            fullZipPath = Path.Combine(myPath, targetUpdateZip);

            if (!File.Exists(fullZipPath))
            {
                Console.WriteLine("Could not find update zip under: '" + fullZipPath + "'");
                return false;
            }

            string sourceExeFullPath = Path.Combine(myPath, sourceExeFile);
            if (!File.Exists(sourceExeFile))
            {
                Console.WriteLine("Could not find source exe file under: " + sourceExeFullPath);
                return false;
            }

            lastStage = Stage.Unzip;

            string tempUnzipTarget = Path.Combine(temp.TempDirectoryPath, "UpdateData");
            ZipFile.ExtractToDirectory(fullZipPath, tempUnzipTarget);

            Process process = Process.GetProcessById(sourceProcessID);

            Console.WriteLine("Waiting for source process to finish...");
            // TODO: Add some kind of cross-process system semaphore or EventWaitHandle to tell main thread that everything is working.
            process.WaitForExit();

            string[] orgFiles = Directory.GetFiles(myPath).Where(x => x != myExePath).ToArray();

            lastStage = Stage.Backup;

            backupPath = Path.Combine(temp.TempDirectoryPath, "Backup");

            for (int i = 0; i < orgFiles.Length; i++)
            {
                string orgPath = orgFiles[i];
                string localPath = orgPath.Replace(myPath, "");
                string finalPath = Path.Combine(backupPath, localPath);

                File.Copy(orgPath, finalPath, true);
            }

            lastStage = Stage.Delete;
            for (int i = 0; i < orgFiles.Length; i++)
            {
                string path = orgFiles[i];

                File.Delete(path);
            }

            lastStage = Stage.CopyNewFiles;

            string[] newFiles = Directory.GetFiles(tempUnzipTarget);
            for (int i = 0; i < newFiles.Length; i++)
            {
                string orgPath = newFiles[i];
                string localPath = orgPath.Replace(tempUnzipTarget, "");
                string finalPath = Path.Combine(myPath, localPath);

                File.Copy(orgPath, finalPath, true);
            }

            // TODO: Add checking for exe and executing stages

            return true;
        }

        private static void Rollback(Stage finalStage)
        {
            if (finalStage == Stage.Finished)
            {
                return;
            }

            //TODO: Implement rollback for each state
        }
    }
}
