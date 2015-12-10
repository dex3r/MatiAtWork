using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherLib;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;

namespace Client
{
    public class ClientController
    {
        public ServiceHost Service;

        public void StartService()
        {
            //// Step 1 Create a URI to serve as the base address.
            //Uri baseAddress = new Uri("net.tcp://localhost:8733/Design_Time_Addresses/LauncherLib/Service1/");

            Service1 service = new Service1();

            //// Step 2 Create a ServiceHost instance
            //Service = new ServiceHost(service, baseAddress);

            Service = new ServiceHost(service);

            ////try
            //{
            //    NetTcpBinding binding = new NetTcpBinding("NewBinding0");

            //    // Step 3 Add a service endpoint.
            //    Service.AddServiceEndpoint(typeof(LauncherLib.IService1), binding, "");

            //    // Step 4 Enable metadata exchange.
            //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //    //smb.ExternalMetadataLocation = new Uri("mex", UriKind.Relative);
            //    //Debug.WriteLine("External location: " + smb.ExternalMetadataLocation);
            //    Service.Description.Behaviors.Add(smb);

            //    var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
            //    Service.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

                // Step 5 Start the service.
                Service.Open();

                // Close the ServiceHostBase to shutdown the service.
                // selfHost.Close();
           // }
            //catch (CommunicationException ce)
            // {
            //    Console.WriteLine("An exception occurred: {0}", ce.Message);
            //     Service.Abort();
            // }
        }

        ~ClientController()
        {
            try
            {
                if (Service != null)
                {
                    //Service.Close();
                }
            }
            catch { }
        }
    }
}
