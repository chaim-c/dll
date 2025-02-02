using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A1 RID: 673
	public class MissionMultiplayerSiegeClient : MissionMultiplayerGameModeBaseClient, ICommanderInfo, IMissionBehavior
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x00085EA2 File Offset: 0x000840A2
		public override bool IsGameModeUsingGold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x00085EA5 File Offset: 0x000840A5
		public override bool IsGameModeTactical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x00085EA8 File Offset: 0x000840A8
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00085EAB File Offset: 0x000840AB
		public override MultiplayerGameType GameType
		{
			get
			{
				return MultiplayerGameType.Siege;
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600241D RID: 9245 RVA: 0x00085EB0 File Offset: 0x000840B0
		// (remove) Token: 0x0600241E RID: 9246 RVA: 0x00085EE8 File Offset: 0x000840E8
		public event Action<BattleSideEnum, float> OnMoraleChangedEvent;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600241F RID: 9247 RVA: 0x00085F20 File Offset: 0x00084120
		// (remove) Token: 0x06002420 RID: 9248 RVA: 0x00085F58 File Offset: 0x00084158
		public event Action OnFlagNumberChangedEvent;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002421 RID: 9249 RVA: 0x00085F90 File Offset: 0x00084190
		// (remove) Token: 0x06002422 RID: 9250 RVA: 0x00085FC8 File Offset: 0x000841C8
		public event Action<FlagCapturePoint, Team> OnCapturePointOwnerChangedEvent;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002423 RID: 9251 RVA: 0x00086000 File Offset: 0x00084200
		// (remove) Token: 0x06002424 RID: 9252 RVA: 0x00086038 File Offset: 0x00084238
		public event Action<GoldGain> OnGoldGainEvent;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002425 RID: 9253 RVA: 0x00086070 File Offset: 0x00084270
		// (remove) Token: 0x06002426 RID: 9254 RVA: 0x000860A8 File Offset: 0x000842A8
		public event Action<int[]> OnCapturePointRemainingMoraleGainsChangedEvent;

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000860DD File Offset: 0x000842DD
		public bool AreMoralesIndependent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000860E0 File Offset: 0x000842E0
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x000860E8 File Offset: 0x000842E8
		public IEnumerable<FlagCapturePoint> AllCapturePoints { get; private set; }

		// Token: 0x0600242A RID: 9258 RVA: 0x000860F4 File Offset: 0x000842F4
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<SiegeMoraleChangeMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleMoraleChangedMessage));
				registerer.RegisterBaseHandler<SyncGoldsForSkirmish>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUpdateGold));
				registerer.RegisterBaseHandler<FlagDominationFlagsRemovedMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleFlagsRemovedMessage));
				registerer.RegisterBaseHandler<FlagDominationCapturePointMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventPointCapturedMessage));
				registerer.RegisterBaseHandler<GoldGain>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventTDMGoldGain));
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00086162 File Offset: 0x00084362
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.MissionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
			this._capturePointOwners = new Team[7];
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>();
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000861A4 File Offset: 0x000843A4
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (flagCapturePoint.GameEntity.HasTag("keep_capture_point"))
				{
					this._masterFlag = flagCapturePoint;
				}
				else if (flagCapturePoint.FlagIndex == 0)
				{
					MatrixFrame globalFrame = flagCapturePoint.GameEntity.GetGlobalFrame();
					this._retreatHornPosition = globalFrame.origin + globalFrame.rotation.u * 3f;
				}
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0008624C File Offset: 0x0008444C
		private void OnMyClientSynchronized()
		{
			this._myRepresentative = GameNetwork.MyPeer.GetComponent<SiegeMissionRepresentative>();
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0008625E File Offset: 0x0008445E
		public override int GetGoldAmount()
		{
			return this._myRepresentative.Gold;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0008626B File Offset: 0x0008446B
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
			if (representative != null && base.MissionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				representative.UpdateGold(goldAmount);
				base.ScoreboardComponent.PlayerPropertiesChanged(representative.MissionPeer);
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00086298 File Offset: 0x00084498
		public void OnNumberOfFlagsChanged()
		{
			Action onFlagNumberChangedEvent = this.OnFlagNumberChangedEvent;
			if (onFlagNumberChangedEvent != null)
			{
				onFlagNumberChangedEvent();
			}
			SiegeMissionRepresentative myRepresentative = this._myRepresentative;
			bool flag;
			if (myRepresentative == null)
			{
				flag = false;
			}
			else
			{
				Team team = myRepresentative.MissionPeer.Team;
				BattleSideEnum? battleSideEnum = (team != null) ? new BattleSideEnum?(team.Side) : null;
				BattleSideEnum battleSideEnum2 = BattleSideEnum.Attacker;
				flag = (battleSideEnum.GetValueOrDefault() == battleSideEnum2 & battleSideEnum != null);
			}
			if (flag)
			{
				Action<GoldGain> onGoldGainEvent = this.OnGoldGainEvent;
				if (onGoldGainEvent == null)
				{
					return;
				}
				onGoldGainEvent(new GoldGain(new List<KeyValuePair<ushort, int>>
				{
					new KeyValuePair<ushort, int>(512, 35)
				}));
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0008632C File Offset: 0x0008452C
		public void OnCapturePointOwnerChanged(FlagCapturePoint flagCapturePoint, Team ownerTeam)
		{
			this._capturePointOwners[flagCapturePoint.FlagIndex] = ownerTeam;
			Action<FlagCapturePoint, Team> onCapturePointOwnerChangedEvent = this.OnCapturePointOwnerChangedEvent;
			if (onCapturePointOwnerChangedEvent != null)
			{
				onCapturePointOwnerChangedEvent(flagCapturePoint, ownerTeam);
			}
			if (ownerTeam != null && ownerTeam.Side == BattleSideEnum.Defender && this._remainingTimeForBellSoundToStop > 8f && flagCapturePoint == this._masterFlag)
			{
				this._bellSoundEvent.Stop();
				this._bellSoundEvent = null;
				this._remainingTimeForBellSoundToStop = float.MinValue;
				this._lastBellSoundPercentage += 0.2f;
			}
			if (this._myRepresentative != null && this._myRepresentative.MissionPeer.Team != null)
			{
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
				if (this._myRepresentative.MissionPeer.Team == ownerTeam)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/flag_captured"), position);
					return;
				}
				MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/flag_lost"), position);
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0008641C File Offset: 0x0008461C
		public void OnMoraleChanged(int attackerMorale, int defenderMorale, int[] capturePointRemainingMoraleGains)
		{
			float num = (float)attackerMorale / 360f;
			float num2 = (float)defenderMorale / 360f;
			SiegeMissionRepresentative myRepresentative = this._myRepresentative;
			if (((myRepresentative != null) ? myRepresentative.MissionPeer.Team : null) != null && this._myRepresentative.MissionPeer.Team.Side != BattleSideEnum.None)
			{
				if ((this._capturePointOwners[this._masterFlag.FlagIndex] == null || this._capturePointOwners[this._masterFlag.FlagIndex].Side != BattleSideEnum.Defender) && this._remainingTimeForBellSoundToStop < 0f)
				{
					if (num2 > this._lastBellSoundPercentage)
					{
						this._lastBellSoundPercentage += 0.2f;
					}
					if (num2 <= 0.4f)
					{
						if (this._lastBellSoundPercentage > 0.4f)
						{
							this._remainingTimeForBellSoundToStop = float.MaxValue;
							this._lastBellSoundPercentage = 0.4f;
						}
					}
					else if (num2 <= 0.6f)
					{
						if (this._lastBellSoundPercentage > 0.6f)
						{
							this._remainingTimeForBellSoundToStop = 8f;
							this._lastBellSoundPercentage = 0.6f;
						}
					}
					else if (num2 <= 0.8f && this._lastBellSoundPercentage > 0.8f)
					{
						this._remainingTimeForBellSoundToStop = 4f;
						this._lastBellSoundPercentage = 0.8f;
					}
					if (this._remainingTimeForBellSoundToStop > 0f)
					{
						BattleSideEnum side = this._myRepresentative.MissionPeer.Team.Side;
						if (side != BattleSideEnum.Defender)
						{
							if (side == BattleSideEnum.Attacker)
							{
								this._bellSoundEvent = SoundEvent.CreateEventFromString("event:/multiplayer/warning_bells_attacker", base.Mission.Scene);
							}
						}
						else
						{
							this._bellSoundEvent = SoundEvent.CreateEventFromString("event:/multiplayer/warning_bells_defender", base.Mission.Scene);
						}
						MatrixFrame globalFrame = this._masterFlag.GameEntity.GetGlobalFrame();
						this._bellSoundEvent.PlayInPosition(globalFrame.origin + globalFrame.rotation.u * 3f);
					}
				}
				if (!this._battleEndingNotificationGiven || !this._battleEndingLateNotificationGiven)
				{
					float num3 = (!this._battleEndingNotificationGiven) ? 0.25f : 0.15f;
					MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
					Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
					if (num <= num3 && num2 > num3)
					{
						MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString((this._myRepresentative.MissionPeer.Team.Side == BattleSideEnum.Attacker) ? "event:/alerts/report/battle_losing" : "event:/alerts/report/battle_winning"), position);
						if (this._myRepresentative.MissionPeer.Team.Side == BattleSideEnum.Attacker)
						{
							MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/multiplayer/retreat_horn_attacker"), this._retreatHornPosition);
						}
						else if (this._myRepresentative.MissionPeer.Team.Side == BattleSideEnum.Defender)
						{
							MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/multiplayer/retreat_horn_defender"), this._retreatHornPosition);
						}
						if (this._battleEndingNotificationGiven)
						{
							this._battleEndingLateNotificationGiven = true;
						}
						this._battleEndingNotificationGiven = true;
					}
					if (num2 <= num3 && num > num3)
					{
						MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString((this._myRepresentative.MissionPeer.Team.Side == BattleSideEnum.Defender) ? "event:/alerts/report/battle_losing" : "event:/alerts/report/battle_winning"), position);
						if (this._battleEndingNotificationGiven)
						{
							this._battleEndingLateNotificationGiven = true;
						}
						this._battleEndingNotificationGiven = true;
					}
				}
			}
			Action<BattleSideEnum, float> onMoraleChangedEvent = this.OnMoraleChangedEvent;
			if (onMoraleChangedEvent != null)
			{
				onMoraleChangedEvent(BattleSideEnum.Attacker, num);
			}
			Action<BattleSideEnum, float> onMoraleChangedEvent2 = this.OnMoraleChangedEvent;
			if (onMoraleChangedEvent2 != null)
			{
				onMoraleChangedEvent2(BattleSideEnum.Defender, num2);
			}
			Action<int[]> onCapturePointRemainingMoraleGainsChangedEvent = this.OnCapturePointRemainingMoraleGainsChangedEvent;
			if (onCapturePointRemainingMoraleGainsChangedEvent == null)
			{
				return;
			}
			onCapturePointRemainingMoraleGainsChangedEvent(capturePointRemainingMoraleGains);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00086780 File Offset: 0x00084980
		public Team GetFlagOwner(FlagCapturePoint flag)
		{
			return this._capturePointOwners[flag.FlagIndex];
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0008678F File Offset: 0x0008498F
		public override void OnRemoveBehavior()
		{
			base.MissionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
			base.OnRemoveBehavior();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000867B0 File Offset: 0x000849B0
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._remainingTimeForBellSoundToStop > 0f)
			{
				this._remainingTimeForBellSoundToStop -= dt;
				if (this._remainingTimeForBellSoundToStop <= 0f || base.MissionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Playing)
				{
					this._remainingTimeForBellSoundToStop = float.MinValue;
					this._bellSoundEvent.Stop();
					this._bellSoundEvent = null;
				}
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00086818 File Offset: 0x00084A18
		public List<ItemObject> GetSiegeMissiles()
		{
			List<ItemObject> list = new List<ItemObject>();
			ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>("grapeshot_fire_projectile");
			list.Add(@object);
			foreach (GameEntity gameEntity in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<RangedSiegeWeapon>())
			{
				RangedSiegeWeapon firstScriptOfType = gameEntity.GetFirstScriptOfType<RangedSiegeWeapon>();
				if (!string.IsNullOrEmpty(firstScriptOfType.MissileItemID))
				{
					ItemObject object2 = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MissileItemID);
					if (!list.Contains(object2))
					{
						list.Add(object2);
					}
				}
			}
			foreach (GameEntity gameEntity2 in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<StonePile>())
			{
				StonePile firstScriptOfType2 = gameEntity2.GetFirstScriptOfType<StonePile>();
				if (!string.IsNullOrEmpty(firstScriptOfType2.GivenItemID))
				{
					ItemObject object3 = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType2.GivenItemID);
					if (!list.Contains(object3))
					{
						list.Add(object3);
					}
				}
			}
			return list;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00086928 File Offset: 0x00084B28
		private void HandleMoraleChangedMessage(GameNetworkMessage baseMessage)
		{
			SiegeMoraleChangeMessage siegeMoraleChangeMessage = (SiegeMoraleChangeMessage)baseMessage;
			this.OnMoraleChanged(siegeMoraleChangeMessage.AttackerMorale, siegeMoraleChangeMessage.DefenderMorale, siegeMoraleChangeMessage.CapturePointRemainingMoraleGains);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00086954 File Offset: 0x00084B54
		private void HandleServerEventUpdateGold(GameNetworkMessage baseMessage)
		{
			SyncGoldsForSkirmish syncGoldsForSkirmish = (SyncGoldsForSkirmish)baseMessage;
			SiegeMissionRepresentative component = syncGoldsForSkirmish.VirtualPlayer.GetComponent<SiegeMissionRepresentative>();
			this.OnGoldAmountChangedForRepresentative(component, syncGoldsForSkirmish.GoldAmount);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00086981 File Offset: 0x00084B81
		private void HandleFlagsRemovedMessage(GameNetworkMessage baseMessage)
		{
			this.OnNumberOfFlagsChanged();
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0008698C File Offset: 0x00084B8C
		private void HandleServerEventPointCapturedMessage(GameNetworkMessage baseMessage)
		{
			FlagDominationCapturePointMessage flagDominationCapturePointMessage = (FlagDominationCapturePointMessage)baseMessage;
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (flagCapturePoint.FlagIndex == flagDominationCapturePointMessage.FlagIndex)
				{
					this.OnCapturePointOwnerChanged(flagCapturePoint, Mission.MissionNetworkHelper.GetTeamFromTeamIndex(flagDominationCapturePointMessage.OwnerTeamIndex));
					break;
				}
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000869FC File Offset: 0x00084BFC
		private void HandleServerEventTDMGoldGain(GameNetworkMessage baseMessage)
		{
			GoldGain obj = (GoldGain)baseMessage;
			Action<GoldGain> onGoldGainEvent = this.OnGoldGainEvent;
			if (onGoldGainEvent == null)
			{
				return;
			}
			onGoldGainEvent(obj);
		}

		// Token: 0x04000D32 RID: 3378
		private const float DefenderMoraleDropThresholdIncrement = 0.2f;

		// Token: 0x04000D33 RID: 3379
		private const float DefenderMoraleDropThresholdLow = 0.4f;

		// Token: 0x04000D34 RID: 3380
		private const float DefenderMoraleDropThresholdMedium = 0.6f;

		// Token: 0x04000D35 RID: 3381
		private const float DefenderMoraleDropThresholdHigh = 0.8f;

		// Token: 0x04000D36 RID: 3382
		private const float DefenderMoraleDropMediumDuration = 8f;

		// Token: 0x04000D37 RID: 3383
		private const float DefenderMoraleDropHighDuration = 4f;

		// Token: 0x04000D38 RID: 3384
		private const float BattleWinLoseAlertThreshold = 0.25f;

		// Token: 0x04000D39 RID: 3385
		private const float BattleWinLoseLateAlertThreshold = 0.15f;

		// Token: 0x04000D3A RID: 3386
		private const string BattleWinningSoundEventString = "event:/alerts/report/battle_winning";

		// Token: 0x04000D3B RID: 3387
		private const string BattleLosingSoundEventString = "event:/alerts/report/battle_losing";

		// Token: 0x04000D3C RID: 3388
		private const float IndefiniteDurationThreshold = 8f;

		// Token: 0x04000D3D RID: 3389
		private Team[] _capturePointOwners;

		// Token: 0x04000D3F RID: 3391
		private FlagCapturePoint _masterFlag;

		// Token: 0x04000D40 RID: 3392
		private SiegeMissionRepresentative _myRepresentative;

		// Token: 0x04000D41 RID: 3393
		private SoundEvent _bellSoundEvent;

		// Token: 0x04000D42 RID: 3394
		private float _remainingTimeForBellSoundToStop = float.MinValue;

		// Token: 0x04000D43 RID: 3395
		private float _lastBellSoundPercentage = 1f;

		// Token: 0x04000D44 RID: 3396
		private bool _battleEndingNotificationGiven;

		// Token: 0x04000D45 RID: 3397
		private bool _battleEndingLateNotificationGiven;

		// Token: 0x04000D46 RID: 3398
		private Vec3 _retreatHornPosition;
	}
}
