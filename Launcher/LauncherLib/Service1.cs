using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace LauncherLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string Ping()
        {
            return "Pong";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void SendZippedDirectory(System.IO.Stream zipFileStream)
        {
            using (FileStream fs = new FileStream(@"D:\dziala.zip", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Debug.WriteLine("Reciving stream...");
                zipFileStream.CopyTo(fs);
            }

            Debug.WriteLine("Received!");
        }

        public void StartProcessFromZipDir(string path)
        {

        }

        public void KillProcessFromZipDir()
        {

        }
    }
}
