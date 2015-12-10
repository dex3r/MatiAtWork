using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Host.ServiceReference1;
using System.Diagnostics;

namespace Host
{
    public class Client
    {
        public IPEndPoint EndPoint;
        public Service1Client Service;
        public string MachineName;

        public float MeasurePing()
        {
            Stopwatch sw = new Stopwatch();

            Service.Ping();

            sw.Start();

            try
            {
                Service.Ping();
                sw.Stop();

                return (float)sw.Elapsed.TotalMilliseconds;
            }
            catch { }

            return float.PositiveInfinity;
        }
    }
}
