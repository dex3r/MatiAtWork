using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using Utils;
using System.IO.Compression;

namespace LauncherLib
{
	public class Service1 : IService1
	{
		private ClientLogic Logic;

		public Service1()
		{
			Logic = ClientLogic.Instance;
		}

		public string GetData( int value )
		{
			return string.Format( "You entered: {0}", value );
		}

		public string Ping()
		{
			return "Pong";
		}

		public CompositeType GetDataUsingDataContract( CompositeType composite )
		{
			if( composite == null )
			{
				throw new ArgumentNullException( "composite" );
			}
			if( composite.BoolValue )
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}

		public string GetMachineName()
		{
			return Environment.MachineName;
		}

		[OperationBehavior( TransactionAutoComplete = true, TransactionScopeRequired = true )]
		public void SendZippedDirectory( Stream zipFileStream )
		{
			Logic.SendZippedDirectory( zipFileStream );
		}

		public void StartProcessFromZipDir( string path, string arguments )
		{
			Logic.StartProcessFromZipDir( path, arguments );
		}

		public void KillProcessFromZipDir()
		{
			Logic.KillProcessFromZipDir();
		}
	}
}
