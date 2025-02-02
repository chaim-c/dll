using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030F RID: 783
	public class NetworkCommunication : INetworkCommunication
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x000A53BB File Offset: 0x000A35BB
		VirtualPlayer INetworkCommunication.MyPeer
		{
			get
			{
				NetworkCommunicator myPeer = GameNetwork.MyPeer;
				if (myPeer == null)
				{
					return null;
				}
				return myPeer.VirtualPlayer;
			}
		}
	}
}
