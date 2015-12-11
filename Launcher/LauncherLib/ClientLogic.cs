using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using System.ServiceModel;

namespace LauncherLib
{
	public sealed class ClientLogic : IService1
	{
		public const string GlobalTempDir = @"Launcher\Client\";

		public TempDirManager Temp;
		public string HostDataPath;
		public string HostDataZip;
		public Process HostProcess;

		public ClientLogic()
		{
			Temp = new TempDirManager( GlobalTempDir );
			HostDataPath = Path.Combine( Temp.TempDirectoryPath, "HostData" );
			HostDataZip = Path.Combine( Temp.TempDirectoryPath, "HostData.zip" );
		}

		#region IService logic methods

		public void SendZippedDirectory( Stream zipFileStream )
		{
			using( FileStream fs = new FileStream( HostDataZip, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				Debug.WriteLine( "Receiving data..." );
				zipFileStream.CopyTo( fs );
			}

			KillProcessFromZipDir();

			Debug.WriteLine( "Unpacking..." );

			if( Directory.Exists( HostDataPath ) )
			{
				Directory.Delete( HostDataPath, true );
			}

			ZipFile.ExtractToDirectory( HostDataZip, HostDataPath );

			Debug.WriteLine( "Unpacked." );
		}

		public void StartProcessFromZipDir( string path, string arguments )
		{
			string finalPath = Path.Combine( HostDataPath, path );

			if( !File.Exists( finalPath ) )
			{
				throw new Exception( "Could not find given file: '" + finalPath + "'" );
			}

			KillProcessFromZipDir();

			HostProcess = new Process();
			HostProcess.StartInfo = new ProcessStartInfo( "cmd.exe", "/K start /D \"" + HostDataPath + "\"" + path + "" );
			//HostProcess.StartInfo.WorkingDirectory = Path.GetPathRoot( finalPath );
			HostProcess.Start();
		}

		public void KillProcessFromZipDir()
		{
			if( HostProcess != null && !HostProcess.HasExited )
			{
				HostProcess.Kill();
			}
		}

		#endregion IService logic methods

		#region IService service methods
		public string GetData( int value )
		{
			throw new NotImplementedException();
		}

		public CompositeType GetDataUsingDataContract( CompositeType composite )
		{
			throw new NotImplementedException();
		}

		public string GetMachineName()
		{
			throw new NotImplementedException();
		}

		public string Ping()
		{
			throw new NotImplementedException();
		}

		#endregion IService service methods

		private static ClientLogic _Instance;
		public static ClientLogic Instance
		{
			get
			{
				if( _Instance == null )
				{
					_Instance = new ClientLogic();
				}

				return _Instance;
			}
		}
	}
}
