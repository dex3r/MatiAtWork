using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Host
{
	public partial class MainForm : Form
	{
		HostLogic Logic;

		public MainForm()
		{
			InitializeComponent();
			Logic = new HostLogic();
		}

		private void MainForm_Load( object sender, EventArgs e )
		{
			//Logic.TestConnection();
		}

		private void bScan_Click( object sender, EventArgs e )
		{
			string ipRangeString = tbScanRange.Text;

			IPRange range = new IPRange( ipRangeString );
			IList<IPAddress> ips = range.GetAllIP();

			if( cbIncludeLocalhost.Checked )
			{
				ips.Add( IPAddress.Parse( "127.0.0.1" ) );
			}

			Logic.MaxThreads = ( int )nudMaxThreads.Value;

			Logic.ScanClients( ips );
			RefreshClientsList();
		}

		public void RefreshClientsList()
		{
			lbFoundClients.Items.Clear();

			foreach( var client in Logic.Clients )
			{
				string text = "'" + client.MachineName + "' ping: " + client.MeasurePing().ToString( "0.0" ) + "ms";
				lbFoundClients.Items.Add( text );
			}
		}

		private void bRefresh_Click( object sender, EventArgs e )
		{
			RefreshClientsList();
		}

		private void bSend_Click( object sender, EventArgs e )
		{
			Logic.TargetDirectory = tbTargetDirectory.Text;
			Logic.SyncClientsData();
		}

		private void bLaunch_Click( object sender, EventArgs e )
		{
			Logic.LaunchAppOnClients( tbAppPath.Text );
		}
	}
}
