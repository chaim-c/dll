using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FC RID: 764
	public static class PeerExtensions
	{
		// Token: 0x060029AF RID: 10671 RVA: 0x000A0D91 File Offset: 0x0009EF91
		public static void SendExistingObjects(this NetworkCommunicator peer, Mission mission)
		{
			MBAPI.IMBPeer.SendExistingObjects(peer.Index, mission.Pointer);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000A0DA9 File Offset: 0x0009EFA9
		public static VirtualPlayer GetPeer(this PeerComponent peerComponent)
		{
			return peerComponent.Peer;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000A0DB1 File Offset: 0x0009EFB1
		public static NetworkCommunicator GetNetworkPeer(this PeerComponent peerComponent)
		{
			return peerComponent.Peer.Communicator as NetworkCommunicator;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000A0DC3 File Offset: 0x0009EFC3
		public static T GetComponent<T>(this NetworkCommunicator networkPeer) where T : PeerComponent
		{
			return networkPeer.VirtualPlayer.GetComponent<T>();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000A0DD0 File Offset: 0x0009EFD0
		public static void RemoveComponent<T>(this NetworkCommunicator networkPeer, bool synched = true) where T : PeerComponent
		{
			networkPeer.VirtualPlayer.RemoveComponent<T>(true);
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000A0DDE File Offset: 0x0009EFDE
		public static void RemoveComponent(this NetworkCommunicator networkPeer, PeerComponent component)
		{
			networkPeer.VirtualPlayer.RemoveComponent(component);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000A0DEC File Offset: 0x0009EFEC
		public static PeerComponent GetComponent(this NetworkCommunicator networkPeer, uint componentId)
		{
			return networkPeer.VirtualPlayer.GetComponent(componentId);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000A0DFA File Offset: 0x0009EFFA
		public static void AddComponent(this NetworkCommunicator networkPeer, Type peerComponentType)
		{
			networkPeer.VirtualPlayer.AddComponent(peerComponentType);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000A0E09 File Offset: 0x0009F009
		public static void AddComponent(this NetworkCommunicator networkPeer, uint componentId)
		{
			networkPeer.VirtualPlayer.AddComponent(componentId);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A0E18 File Offset: 0x0009F018
		public static T AddComponent<T>(this NetworkCommunicator networkPeer) where T : PeerComponent, new()
		{
			if (networkPeer.GetComponent<T>() != null)
			{
				return networkPeer.TellClientToAddComponent<T>();
			}
			return networkPeer.VirtualPlayer.AddComponent<T>();
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000A0E3C File Offset: 0x0009F03C
		public static T TellClientToAddComponent<T>(this NetworkCommunicator networkPeer) where T : PeerComponent, new()
		{
			T component = networkPeer.GetComponent<T>();
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new AddPeerComponent(networkPeer, component.TypeId));
			GameNetwork.EndModuleEventAsServer();
			return component;
		}
	}
}
