using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030D RID: 781
	public sealed class NetworkCommunicator : ICommunicator
	{
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06002A5D RID: 10845 RVA: 0x000A4D3C File Offset: 0x000A2F3C
		// (remove) Token: 0x06002A5E RID: 10846 RVA: 0x000A4D70 File Offset: 0x000A2F70
		public static event Action<PeerComponent> OnPeerComponentAdded;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06002A5F RID: 10847 RVA: 0x000A4DA4 File Offset: 0x000A2FA4
		// (remove) Token: 0x06002A60 RID: 10848 RVA: 0x000A4DD8 File Offset: 0x000A2FD8
		public static event Action<NetworkCommunicator> OnPeerSynchronized;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002A61 RID: 10849 RVA: 0x000A4E0C File Offset: 0x000A300C
		// (remove) Token: 0x06002A62 RID: 10850 RVA: 0x000A4E40 File Offset: 0x000A3040
		public static event Action<NetworkCommunicator> OnPeerAveragePingUpdated;

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x000A4E73 File Offset: 0x000A3073
		public VirtualPlayer VirtualPlayer { get; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000A4E7B File Offset: 0x000A307B
		// (set) Token: 0x06002A65 RID: 10853 RVA: 0x000A4E83 File Offset: 0x000A3083
		public PlayerConnectionInfo PlayerConnectionInfo { get; private set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000A4E8C File Offset: 0x000A308C
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x000A4E94 File Offset: 0x000A3094
		public bool QuitFromMission { get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000A4E9D File Offset: 0x000A309D
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x000A4EA5 File Offset: 0x000A30A5
		public int SessionKey { get; internal set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000A4EAE File Offset: 0x000A30AE
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x000A4EB6 File Offset: 0x000A30B6
		public bool JustReconnecting { get; private set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x000A4EBF File Offset: 0x000A30BF
		// (set) Token: 0x06002A6D RID: 10861 RVA: 0x000A4EC7 File Offset: 0x000A30C7
		public double AveragePingInMilliseconds
		{
			get
			{
				return this._averagePingInMilliseconds;
			}
			private set
			{
				if (value != this._averagePingInMilliseconds)
				{
					this._averagePingInMilliseconds = value;
					Action<NetworkCommunicator> onPeerAveragePingUpdated = NetworkCommunicator.OnPeerAveragePingUpdated;
					if (onPeerAveragePingUpdated == null)
					{
						return;
					}
					onPeerAveragePingUpdated(this);
				}
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x000A4EE9 File Offset: 0x000A30E9
		// (set) Token: 0x06002A6F RID: 10863 RVA: 0x000A4EF1 File Offset: 0x000A30F1
		public double AverageLossPercent
		{
			get
			{
				return this._averageLossPercent;
			}
			private set
			{
				if (value != this._averageLossPercent)
				{
					this._averageLossPercent = value;
				}
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x000A4F03 File Offset: 0x000A3103
		public bool IsMine
		{
			get
			{
				return GameNetwork.MyPeer == this;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x000A4F0D File Offset: 0x000A310D
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x000A4F15 File Offset: 0x000A3115
		public bool IsAdmin { get; private set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000A4F1E File Offset: 0x000A311E
		public int Index
		{
			get
			{
				return this.VirtualPlayer.Index;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000A4F2B File Offset: 0x000A312B
		public string UserName
		{
			get
			{
				return this.VirtualPlayer.UserName;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000A4F38 File Offset: 0x000A3138
		// (set) Token: 0x06002A76 RID: 10870 RVA: 0x000A4F40 File Offset: 0x000A3140
		public Agent ControlledAgent
		{
			get
			{
				return this._controlledAgent;
			}
			set
			{
				this._controlledAgent = value;
				if (GameNetwork.IsServer)
				{
					Mission mission = (value != null) ? value.Mission : null;
					UIntPtr missionPointer = (mission != null) ? mission.Pointer : UIntPtr.Zero;
					int agentIndex = (value == null) ? -1 : value.Index;
					MBAPI.IMBPeer.SetControlledAgent(this.Index, missionPointer, agentIndex);
				}
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x000A4F98 File Offset: 0x000A3198
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x000A4FA0 File Offset: 0x000A31A0
		public bool IsMuted { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000A4FA9 File Offset: 0x000A31A9
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x000A4FB1 File Offset: 0x000A31B1
		public int ForcedAvatarIndex { get; set; } = -1;

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000A4FBA File Offset: 0x000A31BA
		public bool IsNetworkActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000A4FBD File Offset: 0x000A31BD
		public bool IsConnectionActive
		{
			get
			{
				return GameNetwork.VirtualPlayers[this.Index] == this.VirtualPlayer && MBAPI.IMBPeer.IsActive(this.Index);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x000A4FE5 File Offset: 0x000A31E5
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x000A501C File Offset: 0x000A321C
		public bool IsSynchronized
		{
			get
			{
				if (GameNetwork.IsServer)
				{
					return GameNetwork.VirtualPlayers[this.Index] == this.VirtualPlayer && MBAPI.IMBPeer.GetIsSynchronized(this.Index);
				}
				return this._isSynchronized;
			}
			set
			{
				if (value != this._isSynchronized || this.JustReconnecting)
				{
					if (GameNetwork.IsServer)
					{
						MBAPI.IMBPeer.SetIsSynchronized(this.Index, value);
					}
					this._isSynchronized = value;
					if (this._isSynchronized)
					{
						this.JustReconnecting = false;
						Action<NetworkCommunicator> onPeerSynchronized = NetworkCommunicator.OnPeerSynchronized;
						if (onPeerSynchronized != null)
						{
							onPeerSynchronized(this);
						}
					}
					if (GameNetwork.IsServer && !this.IsServerPeer)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SynchronizingDone(this, value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeTargetPlayer, this);
						GameNetwork.BeginModuleEventAsServer(this);
						GameNetwork.WriteMessage(new SynchronizingDone(this, value));
						GameNetwork.EndModuleEventAsServer();
						if (value)
						{
							MBDebug.Print("Server: " + this.UserName + " is now synchronized.", 0, Debug.DebugColor.White, 17179869184UL);
							return;
						}
						MBDebug.Print("Server: " + this.UserName + " is not synchronized.", 0, Debug.DebugColor.White, 17179869184UL);
					}
				}
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000A510D File Offset: 0x000A330D
		public bool IsServerPeer
		{
			get
			{
				return this._isServerPeer;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000A5115 File Offset: 0x000A3315
		// (set) Token: 0x06002A81 RID: 10881 RVA: 0x000A511D File Offset: 0x000A331D
		public ServerPerformanceState ServerPerformanceProblemState
		{
			get
			{
				return this._serverPerformanceProblemState;
			}
			private set
			{
				if (value != this._serverPerformanceProblemState)
				{
					this._serverPerformanceProblemState = value;
				}
			}
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000A512F File Offset: 0x000A332F
		private NetworkCommunicator(int index, string name, PlayerId playerID)
		{
			this.VirtualPlayer = new VirtualPlayer(index, name, playerID, this);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000A5150 File Offset: 0x000A3350
		internal static NetworkCommunicator CreateAsServer(PlayerConnectionInfo playerConnectionInfo, int index, bool isAdmin)
		{
			NetworkCommunicator networkCommunicator = new NetworkCommunicator(index, playerConnectionInfo.Name, playerConnectionInfo.PlayerID);
			networkCommunicator.PlayerConnectionInfo = playerConnectionInfo;
			networkCommunicator.IsAdmin = isAdmin;
			MBNetworkPeer data = new MBNetworkPeer(networkCommunicator);
			MBAPI.IMBPeer.SetUserData(index, data);
			return networkCommunicator;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000A5190 File Offset: 0x000A3390
		internal static NetworkCommunicator CreateAsClient(string name, int index)
		{
			return new NetworkCommunicator(index, name, PlayerId.Empty);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000A51A0 File Offset: 0x000A33A0
		void ICommunicator.OnAddComponent(PeerComponent component)
		{
			if (GameNetwork.IsServer)
			{
				if (!this.IsServerPeer)
				{
					GameNetwork.BeginModuleEventAsServer(this);
					GameNetwork.WriteMessage(new AddPeerComponent(this, component.TypeId));
					GameNetwork.EndModuleEventAsServer();
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new AddPeerComponent(this, component.TypeId));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeTargetPlayer | GameNetwork.EventBroadcastFlags.AddToMissionRecord, this);
			}
			Action<PeerComponent> onPeerComponentAdded = NetworkCommunicator.OnPeerComponentAdded;
			if (onPeerComponentAdded == null)
			{
				return;
			}
			onPeerComponentAdded(component);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000A5208 File Offset: 0x000A3408
		void ICommunicator.OnRemoveComponent(PeerComponent component)
		{
			if (GameNetwork.IsServer)
			{
				if (!this.IsServerPeer && (this.IsSynchronized || !this.JustReconnecting))
				{
					GameNetwork.BeginModuleEventAsServer(this);
					GameNetwork.WriteMessage(new RemovePeerComponent(this, component.TypeId));
					GameNetwork.EndModuleEventAsServer();
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new RemovePeerComponent(this, component.TypeId));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeTargetPlayer | GameNetwork.EventBroadcastFlags.AddToMissionRecord, this);
			}
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000A526E File Offset: 0x000A346E
		void ICommunicator.OnSynchronizeComponentTo(VirtualPlayer peer, PeerComponent component)
		{
			GameNetwork.BeginModuleEventAsServer(peer);
			GameNetwork.WriteMessage(new AddPeerComponent(this, component.TypeId));
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000A528C File Offset: 0x000A348C
		internal void SetServerPeer(bool serverPeer)
		{
			this._isServerPeer = serverPeer;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000A5295 File Offset: 0x000A3495
		internal double RefreshAndGetAveragePingInMilliseconds()
		{
			this.AveragePingInMilliseconds = MBAPI.IMBPeer.GetAveragePingInMilliseconds(this.Index);
			return this.AveragePingInMilliseconds;
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000A52B3 File Offset: 0x000A34B3
		internal void SetAveragePingInMillisecondsAsClient(double pingValue)
		{
			this.AveragePingInMilliseconds = pingValue;
			Agent controlledAgent = this.ControlledAgent;
			if (controlledAgent == null)
			{
				return;
			}
			controlledAgent.SetAveragePingInMilliseconds(this.AveragePingInMilliseconds);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000A52D2 File Offset: 0x000A34D2
		internal double RefreshAndGetAverageLossPercent()
		{
			this.AverageLossPercent = MBAPI.IMBPeer.GetAverageLossPercent(this.Index);
			return this.AverageLossPercent;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000A52F0 File Offset: 0x000A34F0
		internal void SetAverageLossPercentAsClient(double lossValue)
		{
			this.AverageLossPercent = lossValue;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000A52F9 File Offset: 0x000A34F9
		internal void SetServerPerformanceProblemStateAsClient(ServerPerformanceState serverPerformanceProblemState)
		{
			this.ServerPerformanceProblemState = serverPerformanceProblemState;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000A5302 File Offset: 0x000A3502
		public void SetRelevantGameOptions(bool sendMeBloodEvents, bool sendMeSoundEvents)
		{
			MBAPI.IMBPeer.SetRelevantGameOptions(this.Index, sendMeBloodEvents, sendMeSoundEvents);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000A5316 File Offset: 0x000A3516
		public uint GetHost()
		{
			return MBAPI.IMBPeer.GetHost(this.Index);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000A5328 File Offset: 0x000A3528
		public uint GetReversedHost()
		{
			return MBAPI.IMBPeer.GetReversedHost(this.Index);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000A533A File Offset: 0x000A353A
		public ushort GetPort()
		{
			return MBAPI.IMBPeer.GetPort(this.Index);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000A534C File Offset: 0x000A354C
		public void UpdateConnectionInfoForReconnect(PlayerConnectionInfo playerConnectionInfo, bool isAdmin)
		{
			this.PlayerConnectionInfo = playerConnectionInfo;
			this.IsAdmin = isAdmin;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000A535C File Offset: 0x000A355C
		public void UpdateIndexForReconnectingPlayer(int newIndex)
		{
			this.JustReconnecting = true;
			this.VirtualPlayer.UpdateIndexForReconnectingPlayer(newIndex);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000A5371 File Offset: 0x000A3571
		public void UpdateForJoiningCustomGame(bool isAdmin)
		{
			this.IsAdmin = isAdmin;
		}

		// Token: 0x04001053 RID: 4179
		private double _averagePingInMilliseconds;

		// Token: 0x04001054 RID: 4180
		private double _averageLossPercent;

		// Token: 0x04001056 RID: 4182
		private Agent _controlledAgent;

		// Token: 0x04001057 RID: 4183
		private bool _isServerPeer;

		// Token: 0x04001058 RID: 4184
		private bool _isSynchronized;

		// Token: 0x0400105B RID: 4187
		private ServerPerformanceState _serverPerformanceProblemState;
	}
}
