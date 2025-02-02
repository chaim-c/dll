using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BB RID: 699
	public class VoiceChatHandler : MissionNetwork
	{
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06002675 RID: 9845 RVA: 0x00090C40 File Offset: 0x0008EE40
		// (remove) Token: 0x06002676 RID: 9846 RVA: 0x00090C78 File Offset: 0x0008EE78
		public event Action OnVoiceRecordStarted;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06002677 RID: 9847 RVA: 0x00090CB0 File Offset: 0x0008EEB0
		// (remove) Token: 0x06002678 RID: 9848 RVA: 0x00090CE8 File Offset: 0x0008EEE8
		public event Action OnVoiceRecordStopped;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06002679 RID: 9849 RVA: 0x00090D20 File Offset: 0x0008EF20
		// (remove) Token: 0x0600267A RID: 9850 RVA: 0x00090D58 File Offset: 0x0008EF58
		public event Action<MissionPeer, bool> OnPeerVoiceStatusUpdated;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x0600267B RID: 9851 RVA: 0x00090D90 File Offset: 0x0008EF90
		// (remove) Token: 0x0600267C RID: 9852 RVA: 0x00090DC8 File Offset: 0x0008EFC8
		public event Action<MissionPeer> OnPeerMuteStatusUpdated;

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x00090DFD File Offset: 0x0008EFFD
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x00090E08 File Offset: 0x0008F008
		private bool IsVoiceRecordActive
		{
			get
			{
				return this._isVoiceRecordActive;
			}
			set
			{
				if (!this._isVoiceChatDisabled)
				{
					this._isVoiceRecordActive = value;
					if (this._isVoiceRecordActive)
					{
						SoundManager.StartVoiceRecording();
						Action onVoiceRecordStarted = this.OnVoiceRecordStarted;
						if (onVoiceRecordStarted == null)
						{
							return;
						}
						onVoiceRecordStarted();
						return;
					}
					else
					{
						SoundManager.StopVoiceRecording();
						Action onVoiceRecordStopped = this.OnVoiceRecordStopped;
						if (onVoiceRecordStopped == null)
						{
							return;
						}
						onVoiceRecordStopped();
					}
				}
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00090E57 File Offset: 0x0008F057
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<SendVoiceToPlay>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventSendVoiceToPlay));
				return;
			}
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<SendVoiceRecord>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventSendVoiceRecord));
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00090E8C File Offset: 0x0008F08C
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (!GameNetwork.IsDedicatedServer)
			{
				this._playerVoiceDataList = new List<VoiceChatHandler.PeerVoiceData>();
				SoundManager.InitializeVoicePlayEvent();
				this._voiceToSend = new Queue<byte>();
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00090EB8 File Offset: 0x0008F0B8
		public override void AfterStart()
		{
			this.UpdateVoiceChatEnabled();
			if (!this._isVoiceChatDisabled)
			{
				MissionPeer.OnTeamChanged += this.MissionPeerOnTeamChanged;
				Mission.Current.GetMissionBehavior<MissionNetworkComponent>().OnClientSynchronizedEvent += this.OnPlayerSynchronized;
			}
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Combine(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00090F40 File Offset: 0x0008F140
		public override void OnRemoveBehavior()
		{
			if (!this._isVoiceChatDisabled)
			{
				MissionPeer.OnTeamChanged -= this.MissionPeerOnTeamChanged;
			}
			if (!GameNetwork.IsDedicatedServer)
			{
				if (this.IsVoiceRecordActive)
				{
					this.IsVoiceRecordActive = false;
				}
				SoundManager.FinalizeVoicePlayEvent();
			}
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Remove(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			base.OnRemoveBehavior();
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00090FC7 File Offset: 0x0008F1C7
		public override void OnPreDisplayMissionTick(float dt)
		{
			if (!GameNetwork.IsDedicatedServer && !this._isVoiceChatDisabled)
			{
				this.VoiceTick(dt);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00090FE0 File Offset: 0x0008F1E0
		private bool HandleClientEventSendVoiceRecord(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			SendVoiceRecord sendVoiceRecord = (SendVoiceRecord)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (sendVoiceRecord.BufferLength > 0 && component.Team != null)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
					if (networkCommunicator.IsSynchronized && component2 != null && component2.Team == component.Team && (sendVoiceRecord.ReceiverList == null || sendVoiceRecord.ReceiverList.Contains(networkCommunicator.VirtualPlayer)) && component2 != component)
					{
						GameNetwork.BeginModuleEventAsServerUnreliable(component2.Peer);
						GameNetwork.WriteMessage(new SendVoiceToPlay(peer, sendVoiceRecord.Buffer, sendVoiceRecord.BufferLength));
						GameNetwork.EndModuleEventAsServerUnreliable();
					}
				}
			}
			return true;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000910BC File Offset: 0x0008F2BC
		private void HandleServerEventSendVoiceToPlay(GameNetworkMessage baseMessage)
		{
			SendVoiceToPlay sendVoiceToPlay = (SendVoiceToPlay)baseMessage;
			if (!this._isVoiceChatDisabled)
			{
				MissionPeer component = sendVoiceToPlay.Peer.GetComponent<MissionPeer>();
				if (component != null && sendVoiceToPlay.BufferLength > 0 && !component.IsMutedFromGameOrPlatform && !CustomGameMutedPlayerManager.IsUserMuted(component.Peer.Id))
				{
					for (int i = 0; i < this._playerVoiceDataList.Count; i++)
					{
						if (this._playerVoiceDataList[i].Peer == component)
						{
							byte[] dataBuffer = new byte[8640];
							int bufferSize;
							this.DecompressVoiceChunk(sendVoiceToPlay.Peer.Index, sendVoiceToPlay.Buffer, sendVoiceToPlay.BufferLength, ref dataBuffer, out bufferSize);
							this._playerVoiceDataList[i].WriteVoiceData(dataBuffer, bufferSize);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0009117E File Offset: 0x0008F37E
		private void CheckStopVoiceRecord()
		{
			if (this._stopRecordingOnNextTick)
			{
				this.IsVoiceRecordActive = false;
				this._stopRecordingOnNextTick = false;
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00091198 File Offset: 0x0008F398
		private void VoiceTick(float dt)
		{
			int num = 120;
			if (this._playedAnyVoicePreviousTick)
			{
				int b = MathF.Ceiling(dt * 1000f);
				num = MathF.Min(num, b);
				this._playedAnyVoicePreviousTick = false;
			}
			foreach (VoiceChatHandler.PeerVoiceData peerVoiceData in this._playerVoiceDataList)
			{
				Action<MissionPeer, bool> onPeerVoiceStatusUpdated = this.OnPeerVoiceStatusUpdated;
				if (onPeerVoiceStatusUpdated != null)
				{
					onPeerVoiceStatusUpdated(peerVoiceData.Peer, peerVoiceData.HasAnyVoiceData());
				}
			}
			int num2 = num * 12;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < this._playerVoiceDataList.Count; j++)
				{
					this._playerVoiceDataList[j].ProcessVoiceData();
				}
			}
			for (int k = 0; k < this._playerVoiceDataList.Count; k++)
			{
				Queue<short> voiceToPlayForTick = this._playerVoiceDataList[k].GetVoiceToPlayForTick();
				if (voiceToPlayForTick.Count > 0)
				{
					int count = voiceToPlayForTick.Count;
					byte[] array = new byte[count * 2];
					for (int l = 0; l < count; l++)
					{
						byte[] bytes = BitConverter.GetBytes(voiceToPlayForTick.Dequeue());
						array[l * 2] = bytes[0];
						array[l * 2 + 1] = bytes[1];
					}
					SoundManager.UpdateVoiceToPlay(array, array.Length, k);
					this._playedAnyVoicePreviousTick = true;
				}
			}
			if (this.IsVoiceRecordActive)
			{
				byte[] array2 = new byte[72000];
				int num3;
				SoundManager.GetVoiceData(array2, 72000, out num3);
				for (int m = 0; m < num3; m++)
				{
					this._voiceToSend.Enqueue(array2[m]);
				}
				this.CheckStopVoiceRecord();
			}
			while (this._voiceToSend.Count > 0 && (this._voiceToSend.Count >= 1440 || !this.IsVoiceRecordActive))
			{
				int num4 = MathF.Min(this._voiceToSend.Count, 1440);
				byte[] array3 = new byte[1440];
				for (int n = 0; n < num4; n++)
				{
					array3[n] = this._voiceToSend.Dequeue();
				}
				if (GameNetwork.IsClient)
				{
					byte[] buffer = new byte[8640];
					int bufferLength;
					this.CompressVoiceChunk(0, array3, ref buffer, out bufferLength);
					GameNetwork.BeginModuleEventAsClientUnreliable();
					GameNetwork.WriteMessage(new SendVoiceRecord(buffer, bufferLength));
					GameNetwork.EndModuleEventAsClientUnreliable();
				}
				else if (GameNetwork.IsServer)
				{
					VoiceChatHandler.<>c__DisplayClass38_0 CS$<>8__locals1 = new VoiceChatHandler.<>c__DisplayClass38_0();
					VoiceChatHandler.<>c__DisplayClass38_0 CS$<>8__locals2 = CS$<>8__locals1;
					NetworkCommunicator myPeer = GameNetwork.MyPeer;
					CS$<>8__locals2.myMissionPeer = ((myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null);
					if (CS$<>8__locals1.myMissionPeer != null)
					{
						this._playerVoiceDataList.Single((VoiceChatHandler.PeerVoiceData x) => x.Peer == CS$<>8__locals1.myMissionPeer).WriteVoiceData(array3, num4);
					}
				}
			}
			if (!this.IsVoiceRecordActive && base.Mission.InputManager.IsGameKeyPressed(33))
			{
				this.IsVoiceRecordActive = true;
			}
			if (this.IsVoiceRecordActive && base.Mission.InputManager.IsGameKeyReleased(33))
			{
				this._stopRecordingOnNextTick = true;
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000914A0 File Offset: 0x0008F6A0
		private void DecompressVoiceChunk(int clientID, byte[] compressedVoiceBuffer, int compressedBufferLength, ref byte[] voiceBuffer, out int bufferLength)
		{
			SoundManager.DecompressData(clientID, compressedVoiceBuffer, compressedBufferLength, voiceBuffer, out bufferLength);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000914AF File Offset: 0x0008F6AF
		private void CompressVoiceChunk(int clientIndex, byte[] voiceBuffer, ref byte[] compressedBuffer, out int compressedBufferLength)
		{
			SoundManager.CompressData(clientIndex, voiceBuffer, 1440, compressedBuffer, out compressedBufferLength);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000914C4 File Offset: 0x0008F6C4
		private VoiceChatHandler.PeerVoiceData GetPlayerVoiceData(MissionPeer missionPeer)
		{
			for (int i = 0; i < this._playerVoiceDataList.Count; i++)
			{
				if (this._playerVoiceDataList[i].Peer == missionPeer)
				{
					return this._playerVoiceDataList[i];
				}
			}
			return null;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0009150C File Offset: 0x0008F70C
		private void AddPlayerToVoiceChat(MissionPeer missionPeer)
		{
			VirtualPlayer peer = missionPeer.Peer;
			this._playerVoiceDataList.Add(new VoiceChatHandler.PeerVoiceData(missionPeer));
			SoundManager.CreateVoiceEvent();
			PlatformServices.Instance.CheckPermissionWithUser(Permission.CommunicateUsingVoice, missionPeer.Peer.Id, delegate(bool hasPermission)
			{
				if (Mission.Current != null && Mission.Current.CurrentState == Mission.State.Continuing)
				{
					VoiceChatHandler.PeerVoiceData playerVoiceData = this.GetPlayerVoiceData(missionPeer);
					if (playerVoiceData != null)
					{
						if (!hasPermission)
						{
							PlayerIdProvidedTypes providedType = missionPeer.Peer.Id.ProvidedType;
							LobbyClient gameClient = NetworkMain.GameClient;
							PlayerIdProvidedTypes? playerIdProvidedTypes = (gameClient != null) ? new PlayerIdProvidedTypes?(gameClient.PlayerID.ProvidedType) : null;
							if (providedType == playerIdProvidedTypes.GetValueOrDefault() & playerIdProvidedTypes != null)
							{
								missionPeer.SetMutedFromPlatform(true);
							}
						}
						playerVoiceData.SetReadyOnPlatform();
					}
				}
			});
			missionPeer.SetMuted(PermaMuteList.IsPlayerMuted(missionPeer.Peer.Id));
			SoundManager.AddSoundClientWithId((ulong)((long)peer.Index));
			Action<MissionPeer> onPeerMuteStatusUpdated = this.OnPeerMuteStatusUpdated;
			if (onPeerMuteStatusUpdated == null)
			{
				return;
			}
			onPeerMuteStatusUpdated(missionPeer);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000915BC File Offset: 0x0008F7BC
		private void RemovePlayerFromVoiceChat(int indexInVoiceDataList)
		{
			VirtualPlayer peer = this._playerVoiceDataList[indexInVoiceDataList].Peer.Peer;
			SoundManager.DeleteSoundClientWithId((ulong)((long)this._playerVoiceDataList[indexInVoiceDataList].Peer.Peer.Index));
			SoundManager.DestroyVoiceEvent(indexInVoiceDataList);
			this._playerVoiceDataList.RemoveAt(indexInVoiceDataList);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x00091613 File Offset: 0x0008F813
		private void MissionPeerOnTeamChanged(NetworkCommunicator peer, Team previousTeam, Team newTeam)
		{
			if (this._localUserInitialized && peer.VirtualPlayer.Id != PlayerId.Empty)
			{
				this.CheckPlayerForVoiceChatOnTeamChange(peer, previousTeam, newTeam);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00091640 File Offset: 0x0008F840
		private void OnPlayerSynchronized(NetworkCommunicator networkPeer)
		{
			if (this._localUserInitialized)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (!component.IsMine && component.Team != null)
				{
					this.CheckPlayerForVoiceChatOnTeamChange(networkPeer, null, component.Team);
					return;
				}
			}
			else if (networkPeer.IsMine)
			{
				NetworkCommunicator myPeer = GameNetwork.MyPeer;
				MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
				this.CheckPlayerForVoiceChatOnTeamChange(GameNetwork.MyPeer, null, missionPeer.Team);
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000916A8 File Offset: 0x0008F8A8
		private void CheckPlayerForVoiceChatOnTeamChange(NetworkCommunicator peer, Team previousTeam, Team newTeam)
		{
			if (GameNetwork.VirtualPlayers[peer.Index] == peer.VirtualPlayer)
			{
				NetworkCommunicator myPeer = GameNetwork.MyPeer;
				MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
				if (missionPeer != null)
				{
					MissionPeer component = peer.GetComponent<MissionPeer>();
					if (missionPeer == component)
					{
						this._localUserInitialized = true;
						for (int i = this._playerVoiceDataList.Count - 1; i >= 0; i--)
						{
							this.RemovePlayerFromVoiceChat(i);
						}
						if (newTeam == null)
						{
							return;
						}
						using (List<NetworkCommunicator>.Enumerator enumerator = GameNetwork.NetworkPeers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								NetworkCommunicator networkCommunicator = enumerator.Current;
								MissionPeer component2 = networkCommunicator.GetComponent<MissionPeer>();
								if (missionPeer != component2 && ((component2 != null) ? component2.Team : null) != null && component2.Team == newTeam && networkCommunicator.VirtualPlayer.Id != PlayerId.Empty)
								{
									this.AddPlayerToVoiceChat(component2);
								}
							}
							return;
						}
					}
					if (this._localUserInitialized && missionPeer.Team != null)
					{
						if (missionPeer.Team == previousTeam)
						{
							for (int j = 0; j < this._playerVoiceDataList.Count; j++)
							{
								if (this._playerVoiceDataList[j].Peer == component)
								{
									this.RemovePlayerFromVoiceChat(j);
									return;
								}
							}
							return;
						}
						if (missionPeer.Team == newTeam)
						{
							this.AddPlayerToVoiceChat(component);
						}
					}
				}
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00091808 File Offset: 0x0008FA08
		private void UpdateVoiceChatEnabled()
		{
			float num = 1f;
			this._isVoiceChatDisabled = (!BannerlordConfig.EnableVoiceChat || num <= 1E-05f || Game.Current.GetGameHandler<ChatBox>().IsContentRestricted);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00091842 File Offset: 0x0008FA42
		private void OnNativeOptionChanged(NativeOptions.NativeOptionsType changedNativeOptionsType)
		{
			if (changedNativeOptionsType == NativeOptions.NativeOptionsType.VoiceChatVolume)
			{
				this.UpdateVoiceChatEnabled();
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0009184E File Offset: 0x0008FA4E
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionType)
		{
			if (changedManagedOptionType == ManagedOptions.ManagedOptionsType.EnableVoiceChat)
			{
				this.UpdateVoiceChatEnabled();
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0009185C File Offset: 0x0008FA5C
		public override void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
			base.OnPlayerDisconnectedFromServer(networkPeer);
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (((component != null) ? component.Team : null) != null && ((missionPeer != null) ? missionPeer.Team : null) != null && component.Team == missionPeer.Team)
			{
				for (int i = 0; i < this._playerVoiceDataList.Count; i++)
				{
					if (this._playerVoiceDataList[i].Peer == component)
					{
						this.RemovePlayerFromVoiceChat(i);
						return;
					}
				}
			}
		}

		// Token: 0x04000E43 RID: 3651
		private const int MillisecondsToShorts = 12;

		// Token: 0x04000E44 RID: 3652
		private const int MillisecondsToBytes = 24;

		// Token: 0x04000E45 RID: 3653
		private const int OpusFrameSizeCoefficient = 6;

		// Token: 0x04000E46 RID: 3654
		private const int VoiceFrameRawSizeInMilliseconds = 60;

		// Token: 0x04000E47 RID: 3655
		public const int VoiceFrameRawSizeInBytes = 1440;

		// Token: 0x04000E48 RID: 3656
		private const int CompressionMaxChunkSizeInBytes = 8640;

		// Token: 0x04000E49 RID: 3657
		private const int VoiceRecordMaxChunkSizeInBytes = 72000;

		// Token: 0x04000E4E RID: 3662
		private List<VoiceChatHandler.PeerVoiceData> _playerVoiceDataList;

		// Token: 0x04000E4F RID: 3663
		private bool _isVoiceChatDisabled = true;

		// Token: 0x04000E50 RID: 3664
		private bool _isVoiceRecordActive;

		// Token: 0x04000E51 RID: 3665
		private bool _stopRecordingOnNextTick;

		// Token: 0x04000E52 RID: 3666
		private Queue<byte> _voiceToSend;

		// Token: 0x04000E53 RID: 3667
		private bool _playedAnyVoicePreviousTick;

		// Token: 0x04000E54 RID: 3668
		private bool _localUserInitialized;

		// Token: 0x0200057B RID: 1403
		private class PeerVoiceData
		{
			// Token: 0x170009AE RID: 2478
			// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000E6017 File Offset: 0x000E4217
			// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000E601F File Offset: 0x000E421F
			public bool IsReadyOnPlatform { get; private set; }

			// Token: 0x060039F2 RID: 14834 RVA: 0x000E6028 File Offset: 0x000E4228
			public PeerVoiceData(MissionPeer peer)
			{
				this.Peer = peer;
				this._voiceData = new Queue<short>();
				this._voiceToPlayInTick = new Queue<short>();
				this._nextPlayDelayResetTime = MissionTime.Now;
			}

			// Token: 0x060039F3 RID: 14835 RVA: 0x000E6058 File Offset: 0x000E4258
			public void WriteVoiceData(byte[] dataBuffer, int bufferSize)
			{
				if (this._voiceData.Count == 0 && this._nextPlayDelayResetTime.IsPast)
				{
					this._playDelayRemainingSizeInBytes = 3600;
				}
				for (int i = 0; i < bufferSize; i += 2)
				{
					short item = (short)((int)dataBuffer[i] | (int)dataBuffer[i + 1] << 8);
					this._voiceData.Enqueue(item);
				}
			}

			// Token: 0x060039F4 RID: 14836 RVA: 0x000E60AF File Offset: 0x000E42AF
			public void SetReadyOnPlatform()
			{
				this.IsReadyOnPlatform = true;
			}

			// Token: 0x060039F5 RID: 14837 RVA: 0x000E60B8 File Offset: 0x000E42B8
			public bool ProcessVoiceData()
			{
				if (this.IsReadyOnPlatform && this._voiceData.Count > 0)
				{
					bool flag = this.Peer.IsMutedFromGameOrPlatform || CustomGameMutedPlayerManager.IsUserMuted(this.Peer.Peer.Id);
					if (this._playDelayRemainingSizeInBytes > 0)
					{
						this._playDelayRemainingSizeInBytes -= 2;
					}
					else
					{
						short item = this._voiceData.Dequeue();
						this._nextPlayDelayResetTime = MissionTime.Now + MissionTime.Milliseconds(300f);
						if (!flag)
						{
							this._voiceToPlayInTick.Enqueue(item);
						}
					}
					return !flag;
				}
				return false;
			}

			// Token: 0x060039F6 RID: 14838 RVA: 0x000E6158 File Offset: 0x000E4358
			public Queue<short> GetVoiceToPlayForTick()
			{
				return this._voiceToPlayInTick;
			}

			// Token: 0x060039F7 RID: 14839 RVA: 0x000E6160 File Offset: 0x000E4360
			public bool HasAnyVoiceData()
			{
				return this.IsReadyOnPlatform && this._voiceData.Count > 0;
			}

			// Token: 0x04001D4F RID: 7503
			private const int PlayDelaySizeInMilliseconds = 150;

			// Token: 0x04001D50 RID: 7504
			private const int PlayDelaySizeInBytes = 3600;

			// Token: 0x04001D51 RID: 7505
			private const float PlayDelayResetTimeInMilliseconds = 300f;

			// Token: 0x04001D52 RID: 7506
			public readonly MissionPeer Peer;

			// Token: 0x04001D54 RID: 7508
			private readonly Queue<short> _voiceData;

			// Token: 0x04001D55 RID: 7509
			private readonly Queue<short> _voiceToPlayInTick;

			// Token: 0x04001D56 RID: 7510
			private int _playDelayRemainingSizeInBytes;

			// Token: 0x04001D57 RID: 7511
			private MissionTime _nextPlayDelayResetTime;
		}
	}
}
