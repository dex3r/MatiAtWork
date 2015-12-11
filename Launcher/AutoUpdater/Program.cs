using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoUpdater
{
	class Program
	{
		static int sourceProcessID;
		static string targetUpdateZip;
		static string targetExeFile;
		static string sourceExeFile;

		static string myPath;
		static string fullZipPath;

		static int Main( string[] args )
		{

			Console.WriteLine( "P name: " + Process.GetCurrentProcess().StartInfo.FileName );
			Console.ReadKey();

			//int expectedArgsCount = 4;
			//if( args.Length != expectedArgsCount )
			//{
			//	Console.WriteLine( "Invalid number of arguments. Got " + args.Length + " expected " + expectedArgsCount );
			//	return -1;
			//}

			//sourceProcessID = int.Parse( args[ 0 ] );
			//targetUpdateZip = args[ 1 ];
			//targetExeFile = args[ 2 ];
			//sourceExeFile = args[ 3 ];

			//myPath = Environment.CurrentDirectory;
			//fullZipPath = Path.Combine( myPath, targetUpdateZip );

			//if( !File.Exists( fullZipPath ) )
			//{
			//	Console.WriteLine( "Could not find update zip under: '" + fullZipPath + "'" );
			//	return -1;
			//}

			//string sourceExeFullPath = Path.Combine( myPath, sourceExeFile );
			//if( !File.Exists( sourceExeFile ) )
			//{
			//	Console.WriteLine( "Could not find source exe file under: " + sourceExeFullPath );
			//	return -1;
			//}

			//Process process = Process.GetProcessById( sourceProcessID );

			//Console.WriteLine( "Waiting for source process to finish..." );
			//// TODO: Add some kind of cross-process system semaphore or EventWaitHandle to tell main thread that everything is working.
			//process.WaitForExit();

			//string[] files = Directory.GetFiles( myPath );

			return 0;
		}
	}
}
