using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023B RID: 571
	public class MissionState : GameState
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0006E660 File Offset: 0x0006C860
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0006E668 File Offset: 0x0006C868
		public IMissionSystemHandler Handler { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0006E671 File Offset: 0x0006C871
		public override bool IsMission
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0006E674 File Offset: 0x0006C874
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x0006E67B File Offset: 0x0006C87B
		public static MissionState Current { get; private set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0006E683 File Offset: 0x0006C883
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x0006E68B File Offset: 0x0006C88B
		public Mission CurrentMission { get; private set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0006E694 File Offset: 0x0006C894
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x0006E69C File Offset: 0x0006C89C
		public string MissionName { get; private set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0006E6A5 File Offset: 0x0006C8A5
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x0006E6AD File Offset: 0x0006C8AD
		public bool Paused { get; set; }

		// Token: 0x06001F18 RID: 7960 RVA: 0x0006E6B6 File Offset: 0x0006C8B6
		protected override void OnInitialize()
		{
			base.OnInitialize();
			MissionState.Current = this;
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0006E6C9 File Offset: 0x0006C8C9
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this.CurrentMission.OnMissionStateFinalize(this.CurrentMission.NeedsMemoryCleanup);
			this.CurrentMission = null;
			MissionState.Current = null;
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0006E6F4 File Offset: 0x0006C8F4
		protected override void OnActivate()
		{
			base.OnActivate();
			this.CurrentMission.OnMissionStateActivate();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006E707 File Offset: 0x0006C907
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this.CurrentMission.OnMissionStateDeactivate();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0006E71A File Offset: 0x0006C91A
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			if (this.CurrentMission != null && this.CurrentMission.CurrentState == Mission.State.Continuing)
			{
				this.CurrentMission.IdleTick(dt);
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0006E748 File Offset: 0x0006C948
		protected override void OnTick(float realDt)
		{
			base.OnTick(realDt);
			if (this._isDelayedDisconnecting && this.CurrentMission != null && this.CurrentMission.CurrentState == Mission.State.Continuing)
			{
				BannerlordNetwork.EndMultiplayerLobbyMission();
			}
			if (this.CurrentMission == null)
			{
				return;
			}
			if (this.CurrentMission.CurrentState == Mission.State.NewlyCreated || this.CurrentMission.CurrentState == Mission.State.Initializing)
			{
				if (this.CurrentMission.CurrentState == Mission.State.NewlyCreated)
				{
					this.CurrentMission.ClearUnreferencedResources(this.CurrentMission.NeedsMemoryCleanup);
				}
				this.TickLoading(realDt);
			}
			else if (this.CurrentMission.CurrentState == Mission.State.Continuing || this.CurrentMission.MissionEnded)
			{
				if (this.MissionFastForwardAmount != 0f)
				{
					this.CurrentMission.FastForwardMission(this.MissionFastForwardAmount, 0.033f);
					this.MissionFastForwardAmount = 0f;
				}
				bool flag = false;
				if (this.MissionEndTime != 0f && this.CurrentMission.CurrentTime > this.MissionEndTime)
				{
					this.CurrentMission.EndMission();
					flag = true;
				}
				if (!flag && (this.Handler == null || this.Handler.RenderIsReady()))
				{
					this.TickMission(realDt);
				}
				if (flag && MBEditor._isEditorMissionOn)
				{
					MBEditor.LeaveEditMissionMode();
					this.TickMission(realDt);
				}
			}
			if (this.CurrentMission.CurrentState == Mission.State.Over)
			{
				if (MBGameManager.Current.IsEnding)
				{
					Game.Current.GameStateManager.CleanStates(0);
					return;
				}
				Game.Current.GameStateManager.PopState(0);
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0006E8C0 File Offset: 0x0006CAC0
		private void TickMission(float realDt)
		{
			if (this._firstMissionTickAfterLoading && this.CurrentMission != null && this.CurrentMission.CurrentState == Mission.State.Continuing)
			{
				if (GameNetwork.IsClient)
				{
					int currentBattleIndex = GameNetwork.GetNetworkComponent<BaseNetworkComponentData>().CurrentBattleIndex;
					MBDebug.Print(string.Format("Client: I finished loading battle with index: {0}. Sending confirmation to server.", currentBattleIndex), 0, Debug.DebugColor.White, 17179869184UL);
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new FinishedLoading(currentBattleIndex));
					GameNetwork.EndModuleEventAsClient();
					GameNetwork.SyncRelevantGameOptionsToServer();
				}
				this._firstMissionTickAfterLoading = false;
			}
			IMissionSystemHandler handler = this.Handler;
			if (handler != null)
			{
				handler.BeforeMissionTick(this.CurrentMission, realDt);
			}
			this.CurrentMission.PauseAITick = false;
			if (GameNetwork.IsSessionActive && this.CurrentMission.ClearSceneTimerElapsedTime < 0f)
			{
				this.CurrentMission.PauseAITick = true;
			}
			float num = realDt;
			if (this.Paused || MBCommon.IsPaused)
			{
				num = 0f;
			}
			else if (this.CurrentMission.FixedDeltaTimeMode)
			{
				num = this.CurrentMission.FixedDeltaTime;
			}
			if (!GameNetwork.IsSessionActive)
			{
				this.CurrentMission.UpdateSceneTimeSpeed();
				float timeSpeed = this.CurrentMission.Scene.TimeSpeed;
				num *= timeSpeed;
			}
			if (this.CurrentMission.ClearSceneTimerElapsedTime < -0.3f && !GameNetwork.IsClientOrReplay)
			{
				this.CurrentMission.ClearAgentActions();
			}
			if (this.CurrentMission.CurrentState == Mission.State.Continuing || this.CurrentMission.MissionEnded)
			{
				if (this.CurrentMission.IsFastForward)
				{
					float num2 = num * 9f;
					while (num2 > 1E-06f)
					{
						if (num2 > 0.1f)
						{
							this.TickMissionAux(0.1f, 0.1f, false, false);
							if (this.CurrentMission.CurrentState == Mission.State.Over)
							{
								break;
							}
							num2 -= 0.1f;
						}
						else
						{
							if (num2 > 0.0033333334f)
							{
								this.TickMissionAux(num2, num2, false, false);
							}
							num2 = 0f;
						}
					}
					if (this.CurrentMission.CurrentState != Mission.State.Over)
					{
						this.TickMissionAux(num, realDt, true, false);
					}
				}
				else
				{
					this.TickMissionAux(num, realDt, true, true);
				}
			}
			if (this.Handler != null)
			{
				this.Handler.AfterMissionTick(this.CurrentMission, realDt);
			}
			this._missionTickCount++;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006EADB File Offset: 0x0006CCDB
		private void TickMissionAux(float dt, float realDt, bool updateCamera, bool asyncAITick)
		{
			this.CurrentMission.Tick(dt);
			if (this._missionTickCount > 2)
			{
				this.CurrentMission.OnTick(dt, realDt, updateCamera, asyncAITick);
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0006EB04 File Offset: 0x0006CD04
		private void TickLoading(float realDt)
		{
			this._tickCountBeforeLoad++;
			if (!this._missionInitializing && this._tickCountBeforeLoad > 0)
			{
				this.LoadMission();
				Utilities.SetLoadingScreenPercentage(0.01f);
				return;
			}
			if (this._missionInitializing && this.CurrentMission.IsLoadingFinished)
			{
				this.FinishMissionLoading();
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0006EB5C File Offset: 0x0006CD5C
		private void LoadMission()
		{
			foreach (MissionBehavior missionBehavior in this.CurrentMission.MissionBehaviors)
			{
				missionBehavior.OnMissionScreenPreLoad();
			}
			Utilities.ClearOldResourcesAndObjects();
			this._missionInitializing = true;
			this.CurrentMission.Initialize();
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006EBC8 File Offset: 0x0006CDC8
		private void CreateMission(MissionInitializerRecord rec)
		{
			this.CurrentMission = new Mission(rec, this);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0006EBD8 File Offset: 0x0006CDD8
		private Mission HandleOpenNew(string missionName, MissionInitializerRecord rec, InitializeMissionBehaviorsDelegate handler, bool addDefaultMissionBehaviors)
		{
			this.MissionName = missionName;
			this.CreateMission(rec);
			IEnumerable<MissionBehavior> enumerable = handler(this.CurrentMission);
			enumerable = from behavior in enumerable
			where behavior != null
			select behavior;
			if (addDefaultMissionBehaviors)
			{
				enumerable = MissionState.AddDefaultMissionBehaviorsTo(this.CurrentMission, enumerable);
			}
			foreach (MissionBehavior missionBehavior in enumerable)
			{
				missionBehavior.OnAfterMissionCreated();
			}
			this.AddBehaviorsToMission(enumerable);
			if (this.Handler != null)
			{
				enumerable = new MissionBehavior[0];
				enumerable = this.Handler.OnAddBehaviors(enumerable, this.CurrentMission, missionName, addDefaultMissionBehaviors);
				this.AddBehaviorsToMission(enumerable);
			}
			if (GameNetwork.IsDedicatedServer)
			{
				GameNetwork.SetServerFrameRate((double)Module.CurrentModule.StartupInfo.ServerTickRate);
			}
			return this.CurrentMission;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0006ECC4 File Offset: 0x0006CEC4
		private void AddBehaviorsToMission(IEnumerable<MissionBehavior> behaviors)
		{
			MissionLogic[] logicBehaviors = (from behavior in behaviors.OfType<MissionLogic>()
			where !(behavior is MissionNetwork)
			select behavior).ToArray<MissionLogic>();
			MissionBehavior[] otherBehaviors = (from behavior in behaviors
			where behavior != null && !(behavior is MissionNetwork) && !(behavior is MissionLogic)
			select behavior).ToArray<MissionBehavior>();
			MissionNetwork[] networkBehaviors = behaviors.OfType<MissionNetwork>().ToArray<MissionNetwork>();
			this.CurrentMission.InitializeStartingBehaviors(logicBehaviors, otherBehaviors, networkBehaviors);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0006ED46 File Offset: 0x0006CF46
		private static bool IsRecordingActive()
		{
			if (GameNetwork.IsServer)
			{
				return MultiplayerOptions.OptionType.EnableMissionRecording.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			}
			return MissionState.RecordMission && Game.Current.GameType.IsCoreOnlyGameMode;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006ED70 File Offset: 0x0006CF70
		public static Mission OpenNew(string missionName, MissionInitializerRecord rec, InitializeMissionBehaviorsDelegate handler, bool addDefaultMissionBehaviors = true, bool needsMemoryCleanup = true)
		{
			Debug.Print(string.Concat(new string[]
			{
				"Opening new mission ",
				missionName,
				" ",
				rec.SceneLevels,
				".\n"
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			if (!GameNetwork.IsClientOrReplay && !GameNetwork.IsServer)
			{
				MBCommon.CurrentGameType = (MissionState.IsRecordingActive() ? MBCommon.GameType.SingleRecord : MBCommon.GameType.Single);
			}
			Game.Current.OnMissionIsStarting(missionName, rec);
			MissionState missionState = Game.Current.GameStateManager.CreateState<MissionState>();
			Mission mission = missionState.HandleOpenNew(missionName, rec, handler, addDefaultMissionBehaviors);
			Game.Current.GameStateManager.PushState(missionState, 0);
			mission.NeedsMemoryCleanup = needsMemoryCleanup;
			return mission;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0006EE1C File Offset: 0x0006D01C
		private static IEnumerable<MissionBehavior> AddDefaultMissionBehaviorsTo(Mission mission, IEnumerable<MissionBehavior> behaviors)
		{
			List<MissionBehavior> list = new List<MissionBehavior>();
			if (GameNetwork.IsSessionActive || GameNetwork.IsReplay)
			{
				list.Add(new MissionNetworkComponent());
			}
			if (MissionState.IsRecordingActive() && !GameNetwork.IsReplay)
			{
				list.Add(new RecordMissionLogic());
			}
			list.Add(new BasicMissionHandler());
			list.Add(new CasualtyHandler());
			list.Add(new AgentCommonAILogic());
			return list.Concat(behaviors);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0006EE8C File Offset: 0x0006D08C
		private void FinishMissionLoading()
		{
			this._missionInitializing = false;
			this.CurrentMission.Scene.SetOwnerThread();
			Utilities.SetLoadingScreenPercentage(0.4f);
			for (int i = 0; i < 2; i++)
			{
				this.CurrentMission.Tick(0.001f);
			}
			Utilities.SetLoadingScreenPercentage(0.42f);
			IMissionSystemHandler handler = this.Handler;
			if (handler != null)
			{
				handler.OnMissionAfterStarting(this.CurrentMission);
			}
			Utilities.SetLoadingScreenPercentage(0.48f);
			this.CurrentMission.AfterStart();
			Utilities.SetLoadingScreenPercentage(0.56f);
			IMissionSystemHandler handler2 = this.Handler;
			if (handler2 != null)
			{
				handler2.OnMissionLoadingFinished(this.CurrentMission);
			}
			Utilities.SetLoadingScreenPercentage(0.62f);
			this.CurrentMission.Scene.ResumeLoadingRenderings();
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0006EF47 File Offset: 0x0006D147
		public void BeginDelayedDisconnectFromMission()
		{
			this._isDelayedDisconnecting = true;
		}

		// Token: 0x04000B68 RID: 2920
		private const int MissionFastForwardSpeedMultiplier = 10;

		// Token: 0x04000B69 RID: 2921
		private bool _missionInitializing;

		// Token: 0x04000B6A RID: 2922
		private bool _firstMissionTickAfterLoading = true;

		// Token: 0x04000B6B RID: 2923
		private int _tickCountBeforeLoad;

		// Token: 0x04000B6C RID: 2924
		public static bool RecordMission;

		// Token: 0x04000B6E RID: 2926
		public float MissionFastForwardAmount;

		// Token: 0x04000B6F RID: 2927
		public float MissionEndTime;

		// Token: 0x04000B74 RID: 2932
		private bool _isDelayedDisconnecting;

		// Token: 0x04000B75 RID: 2933
		private int _missionTickCount;
	}
}
