using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030A RID: 778
	internal class MBNetworkPeer : DotNetObject
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000A4A5B File Offset: 0x000A2C5B
		public NetworkCommunicator NetworkPeer { get; }

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A4A63 File Offset: 0x000A2C63
		internal MBNetworkPeer(NetworkCommunicator networkPeer)
		{
			this.NetworkPeer = networkPeer;
		}
	}
}
