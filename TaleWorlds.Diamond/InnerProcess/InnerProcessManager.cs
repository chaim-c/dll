using System;
using System.Collections.Generic;

namespace TaleWorlds.Diamond.InnerProcess
{
	// Token: 0x02000051 RID: 81
	public class InnerProcessManager
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000588E File Offset: 0x00003A8E
		public InnerProcessManager()
		{
			this._activeServers = new Dictionary<int, IInnerProcessServer>();
			this._connectionRequests = new List<InnerProcessConnectionRequest>();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000058AC File Offset: 0x00003AAC
		internal void Activate(IInnerProcessServer server, int port)
		{
			this._activeServers.Add(port, server);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000058BB File Offset: 0x00003ABB
		internal void RequestConnection(IInnerProcessClient client, int port)
		{
			this._connectionRequests.Add(new InnerProcessConnectionRequest(client, port));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000058D0 File Offset: 0x00003AD0
		public void Update()
		{
			for (int i = 0; i < this._connectionRequests.Count; i++)
			{
				InnerProcessConnectionRequest innerProcessConnectionRequest = this._connectionRequests[i];
				IInnerProcessClient client = innerProcessConnectionRequest.Client;
				int port = innerProcessConnectionRequest.Port;
				IInnerProcessServer innerProcessServer;
				if (this._activeServers.TryGetValue(port, out innerProcessServer))
				{
					this._connectionRequests.RemoveAt(i);
					i--;
					InnerProcessServerSession innerProcessServerSession = innerProcessServer.AddNewConnection(client);
					client.HandleConnected(innerProcessServerSession);
					innerProcessServerSession.HandleConnected(client);
				}
			}
			foreach (IInnerProcessServer innerProcessServer2 in this._activeServers.Values)
			{
				innerProcessServer2.Update();
			}
		}

		// Token: 0x040000A9 RID: 169
		private Dictionary<int, IInnerProcessServer> _activeServers;

		// Token: 0x040000AA RID: 170
		private List<InnerProcessConnectionRequest> _connectionRequests;
	}
}
