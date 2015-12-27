using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Host.ServiceReference1;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Host
{
	public class HostLogic
	{
		public readonly string Address = "net.tcp://%ADDRESS%:8733/Design_Time_Addresses/LauncherLib/Service1/";
		public readonly string SeriveConfigName = "NetTcpBinding_IService1";
		public readonly string GlobalTempDir = @"Launcher\Host\";

		public ConcurrentBag<Client> Clients;
		public int MaxThreads = -1;

		public string TargetDirectory;
		public TempDirManager Temp;

		public string TempZipPath;

		public HostLogic()
		{
			Clients = new ConcurrentBag<Client>();
			Temp = new TempDirManager( GlobalTempDir );

			TempZipPath = Path.Combine( Temp.TempDirectoryPath, "targetDir.zip" );
		}

		public void ScanClients( IEnumerable<IPAddress> range )
		{
			ParallelOptions options = new ParallelOptions();

			if( MaxThreads != -1 )
			{
				options.MaxDegreeOfParallelism = MaxThreads;
				options.TaskScheduler = new LimitedConcurrencyLevelTaskScheduler( MaxThreads );
				ThreadPool.SetMaxThreads( MaxThreads, MaxThreads );
				ThreadPool.SetMinThreads( MaxThreads, MaxThreads );
			}

			Parallel.ForEach( range, options, ( ip ) => ScanClient( ip ) );

			//MessageBox.Show("Scanning complete");
		}

		public void ScanClient( IPAddress address )
		{
			string uri = Address.Replace( "%ADDRESS%", address.ToString() );
			ScanClient( new EndpointAddress( uri ) );
		}

		public void ScanClient( EndpointAddress endpoint )
		{
			try
			{
				Service1Client service = new Service1Client( SeriveConfigName, endpoint );

				string res = service.Ping();

				Client myClient = new Client();
				myClient.MachineName = service.GetMachineName();
				myClient.Service = service;

				Clients.Add( myClient );
			}
			catch
			{

			}
		}

		public void SyncClientsData()
		{
			ZipTargetDirectory();

			foreach( var client in Clients )
			{
				SendDataToClient( client );
			}
		}

		public void ZipTargetDirectory()
		{
			if( !Directory.Exists( TargetDirectory ) )
			{
				throw new InvalidDataException( "Given target directory does not exist" );
			}

			if( File.Exists( TempZipPath ) )
			{
				File.Delete( TempZipPath );
			}

			ZipFile.CreateFromDirectory( TargetDirectory, TempZipPath, CompressionLevel.Optimal, false );
		}

		private void SendDataToClient( Client client )
		{
			// try
			{
				using( FileStream file = new FileStream( TempZipPath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					Stopwatch sw = new Stopwatch();
					sw.Start();
					client.Service.SendZippedDirectory( file );
					sw.Stop();

					MessageBox.Show( "Sending stream took: " + sw.Elapsed.Milliseconds );
				}
			}
			//catch (Exception ex)
			{
				//LogException("Exception while sending data to client '" + client.MachineName + "':", ex);
			}
		}

		public void LaunchAppOnClients( string relativeAppPath )
		{
			foreach( var client in Clients )
			{
				LaunchAppOnClient( client, relativeAppPath );
			}
		}

		private void LaunchAppOnClient( Client client, string relativeAppPath )
		{
			try
			{
				client.Service.StartProcessFromZipDir( relativeAppPath, string.Empty );
			}
			catch( Exception ex )
			{
				LogException( "Client threw exception while launching app:", ex );
			}
		}

		public void LogException( string customMessage, Exception exception )
		{
			MessageBox.Show( customMessage + "\n" + exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error );
		}
	}
}
