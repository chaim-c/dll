using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029E RID: 670
	public abstract class MissionMultiplayerGameModeBaseClient : MissionNetwork, ICameraModeLogic
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x000848FF File Offset: 0x00082AFF
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x00084907 File Offset: 0x00082B07
		public MissionLobbyComponent MissionLobbyComponent { get; private set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x00084910 File Offset: 0x00082B10
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x00084918 File Offset: 0x00082B18
		public MissionNetworkComponent MissionNetworkComponent { get; private set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00084921 File Offset: 0x00082B21
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x00084929 File Offset: 0x00082B29
		public MissionScoreboardComponent ScoreboardComponent { get; private set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x00084932 File Offset: 0x00082B32
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x0008493A File Offset: 0x00082B3A
		public MultiplayerGameNotificationsComponent NotificationsComponent { get; private set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x00084943 File Offset: 0x00082B43
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x0008494B File Offset: 0x00082B4B
		public MultiplayerWarmupComponent WarmupComponent { get; private set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00084954 File Offset: 0x00082B54
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x0008495C File Offset: 0x00082B5C
		public IRoundComponent RoundComponent { get; private set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x00084965 File Offset: 0x00082B65
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x0008496D File Offset: 0x00082B6D
		public MultiplayerTimerComponent TimerComponent { get; private set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060023BE RID: 9150
		public abstract bool IsGameModeUsingGold { get; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060023BF RID: 9151
		public abstract bool IsGameModeTactical { get; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00084976 File Offset: 0x00082B76
		public virtual bool IsGameModeUsingCasualGold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060023C1 RID: 9153
		public abstract bool IsGameModeUsingRoundCountdown { get; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x00084979 File Offset: 0x00082B79
		public virtual bool IsGameModeUsingAllowCultureChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x0008497C File Offset: 0x00082B7C
		public virtual bool IsGameModeUsingAllowTroopChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060023C4 RID: 9156
		public abstract MultiplayerGameType GameType { get; }

		// Token: 0x060023C5 RID: 9157
		public abstract int GetGoldAmount();

		// Token: 0x060023C6 RID: 9158 RVA: 0x0008497F File Offset: 0x00082B7F
		public virtual SpectatorCameraTypes GetMissionCameraLockMode(bool lockedToMainPlayer)
		{
			return SpectatorCameraTypes.Invalid;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00084982 File Offset: 0x00082B82
		public bool IsRoundInProgress
		{
			get
			{
				IRoundComponent roundComponent = this.RoundComponent;
				return roundComponent != null && roundComponent.CurrentRoundState == MultiplayerRoundState.InProgress;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x00084998 File Offset: 0x00082B98
		public bool IsInWarmup
		{
			get
			{
				return this.MissionLobbyComponent.IsInWarmup;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000849A5 File Offset: 0x00082BA5
		public float RemainingTime
		{
			get
			{
				return this.TimerComponent.GetRemainingTime(GameNetwork.IsClientOrReplay);
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000849B8 File Offset: 0x00082BB8
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this.MissionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this.MissionNetworkComponent = base.Mission.GetMissionBehavior<MissionNetworkComponent>();
			this.ScoreboardComponent = base.Mission.GetMissionBehavior<MissionScoreboardComponent>();
			this.NotificationsComponent = base.Mission.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
			this.WarmupComponent = base.Mission.GetMissionBehavior<MultiplayerWarmupComponent>();
			this.RoundComponent = base.Mission.GetMissionBehavior<IRoundComponent>();
			this.TimerComponent = base.Mission.GetMissionBehavior<MultiplayerTimerComponent>();
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00084A42 File Offset: 0x00082C42
		public override void EarlyStart()
		{
			this.MissionLobbyComponent.MissionType = this.GameType;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00084A58 File Offset: 0x00082C58
		public bool CheckTimer(out int remainingTime, out int remainingWarningTime, bool forceUpdate = false)
		{
			bool flag = false;
			float f = 0f;
			if (this.WarmupComponent != null && this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.WaitingFirstPlayers)
			{
				flag = !this.WarmupComponent.IsInWarmup;
			}
			else if (this.RoundComponent != null)
			{
				flag = !this.RoundComponent.CurrentRoundState.StateHasVisualTimer();
				f = this.RoundComponent.LastRoundEndRemainingTime;
			}
			if (forceUpdate || !flag)
			{
				if (flag)
				{
					remainingTime = MathF.Ceiling(f);
				}
				else
				{
					remainingTime = MathF.Ceiling(this.RemainingTime);
				}
				remainingWarningTime = this.GetWarningTimer();
				return true;
			}
			remainingTime = 0;
			remainingWarningTime = 0;
			return false;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00084AEC File Offset: 0x00082CEC
		protected virtual int GetWarningTimer()
		{
			return 0;
		}

		// Token: 0x060023CE RID: 9166
		public abstract void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount);

		// Token: 0x060023CF RID: 9167 RVA: 0x00084AEF File Offset: 0x00082CEF
		public virtual bool CanRequestTroopChange()
		{
			return false;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00084AF2 File Offset: 0x00082CF2
		public virtual bool CanRequestCultureChange()
		{
			return false;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00084AF8 File Offset: 0x00082CF8
		public bool IsClassAvailable(MultiplayerClassDivisions.MPHeroClass heroClass)
		{
			FormationClass formationClass;
			if (Enum.TryParse<FormationClass>(heroClass.ClassGroup.StringId, out formationClass))
			{
				return this.MissionLobbyComponent.IsClassAvailable(formationClass);
			}
			Debug.FailedAssert("\"" + heroClass.ClassGroup.StringId + "\" does not match with any FormationClass.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ClientGameModeLogics\\MissionMultiplayerGameModeBaseClient.cs", "IsClassAvailable", 116);
			return false;
		}
	}
}
