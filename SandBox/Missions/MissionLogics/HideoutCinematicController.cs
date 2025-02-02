using System;
using System.Collections.Generic;
using SandBox.Objects.Cinematics;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004E RID: 78
	public class HideoutCinematicController : MissionLogic
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00012D72 File Offset: 0x00010F72
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00012D7A File Offset: 0x00010F7A
		public HideoutCinematicController.HideoutCinematicState State { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00012D83 File Offset: 0x00010F83
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00012D8B File Offset: 0x00010F8B
		public bool InStateTransition { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00012D94 File Offset: 0x00010F94
		public bool IsCinematicActive
		{
			get
			{
				return this.State > HideoutCinematicController.HideoutCinematicState.None;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00012D9F File Offset: 0x00010F9F
		public float CinematicDuration
		{
			get
			{
				return this._cinematicDuration;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00012DA7 File Offset: 0x00010FA7
		public float TransitionDuration
		{
			get
			{
				return this._transitionDuration;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00012DAF File Offset: 0x00010FAF
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Logic;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00012DB4 File Offset: 0x00010FB4
		public HideoutCinematicController()
		{
			this.State = HideoutCinematicController.HideoutCinematicState.None;
			this._cinematicFinishedCallback = null;
			this._transitionCallback = null;
			this._stateChangedCallback = null;
			this.InStateTransition = false;
			this._isBehaviorInit = false;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00012E33 File Offset: 0x00011033
		public void SetStateTransitionCallback(HideoutCinematicController.OnHideoutCinematicStateChanged onStateChanged, HideoutCinematicController.OnHideoutCinematicTransition onTransition)
		{
			this._stateChangedCallback = onStateChanged;
			this._transitionCallback = onTransition;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00012E44 File Offset: 0x00011044
		public void StartCinematic(HideoutCinematicController.OnInitialFadeOutFinished initialFadeOutFinished, HideoutCinematicController.OnHideoutCinematicFinished cinematicFinishedCallback, float transitionDuration = 0.4f, float stateDuration = 0.2f, float cinematicDuration = 8f)
		{
			if (this._isBehaviorInit && this.State == HideoutCinematicController.HideoutCinematicState.None)
			{
				this._cinematicFinishedCallback = cinematicFinishedCallback;
				this._initialFadeOutFinished = initialFadeOutFinished;
				this._preCinematicPhase = HideoutCinematicController.HideoutPreCinematicPhase.InitializeFormations;
				this._postCinematicPhase = HideoutCinematicController.HideoutPostCinematicPhase.MoveAgents;
				this._transitionDuration = transitionDuration;
				this._stateDuration = stateDuration;
				this._cinematicDuration = cinematicDuration;
				this._remainingCinematicDuration = this._cinematicDuration;
				this.BeginStateTransition(HideoutCinematicController.HideoutCinematicState.InitialFadeOut);
				return;
			}
			if (!this._isBehaviorInit)
			{
				Debug.FailedAssert("Hideout cinematic controller is not initialized.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\HideoutCinematicController.cs", "StartCinematic", 195);
				return;
			}
			if (this.State != HideoutCinematicController.HideoutCinematicState.None)
			{
				Debug.FailedAssert("There is already an ongoing cinematic.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\HideoutCinematicController.cs", "StartCinematic", 199);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00012EEC File Offset: 0x000110EC
		public MatrixFrame GetBanditsInitialFrame()
		{
			MatrixFrame result;
			this._hideoutBossFightBehavior.GetBanditsInitialFrame(out result);
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00012F08 File Offset: 0x00011108
		public void GetBossStandingEyePosition(out Vec3 eyePosition)
		{
			Agent agent = this._bossAgentInfo.Agent;
			if (((agent != null) ? agent.Monster : null) != null)
			{
				eyePosition = this._bossAgentInfo.InitialFrame.origin + Vec3.Up * (this._bossAgentInfo.Agent.AgentScale * this._bossAgentInfo.Agent.Monster.StandingEyeHeight);
				return;
			}
			eyePosition = Vec3.Zero;
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\HideoutCinematicController.cs", "GetBossStandingEyePosition", 218);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00012FA0 File Offset: 0x000111A0
		public void GetPlayerStandingEyePosition(out Vec3 eyePosition)
		{
			Agent agent = this._playerAgentInfo.Agent;
			if (((agent != null) ? agent.Monster : null) != null)
			{
				eyePosition = this._playerAgentInfo.InitialFrame.origin + Vec3.Up * (this._playerAgentInfo.Agent.AgentScale * this._playerAgentInfo.Agent.Monster.StandingEyeHeight);
				return;
			}
			eyePosition = Vec3.Zero;
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\HideoutCinematicController.cs", "GetPlayerStandingEyePosition", 231);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00013038 File Offset: 0x00011238
		public void GetScenePrefabParameters(out float innerRadius, out float outerRadius, out float walkDistance)
		{
			innerRadius = 0f;
			outerRadius = 0f;
			walkDistance = 0f;
			if (this._hideoutBossFightBehavior != null)
			{
				innerRadius = this._hideoutBossFightBehavior.InnerRadius;
				outerRadius = this._hideoutBossFightBehavior.OuterRadius;
				walkDistance = this._hideoutBossFightBehavior.WalkDistance;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001308C File Offset: 0x0001128C
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("hideout_boss_fight");
			this._hideoutBossFightBehavior = ((gameEntity != null) ? gameEntity.GetFirstScriptOfType<HideoutBossFightBehavior>() : null);
			this._isBehaviorInit = (gameEntity != null && this._hideoutBossFightBehavior != null);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000130E4 File Offset: 0x000112E4
		public override void OnMissionTick(float dt)
		{
			if (this._isBehaviorInit && this.IsCinematicActive)
			{
				if (this.InStateTransition)
				{
					this.TickStateTransition(dt);
					return;
				}
				switch (this.State)
				{
				case HideoutCinematicController.HideoutCinematicState.InitialFadeOut:
					if (this.TickInitialFadeOut(dt))
					{
						this.BeginStateTransition(HideoutCinematicController.HideoutCinematicState.PreCinematic);
						return;
					}
					break;
				case HideoutCinematicController.HideoutCinematicState.PreCinematic:
					if (this.TickPreCinematic(dt))
					{
						this.BeginStateTransition(HideoutCinematicController.HideoutCinematicState.Cinematic);
						return;
					}
					break;
				case HideoutCinematicController.HideoutCinematicState.Cinematic:
					if (this.TickCinematic(dt))
					{
						this.BeginStateTransition(HideoutCinematicController.HideoutCinematicState.PostCinematic);
						return;
					}
					break;
				case HideoutCinematicController.HideoutCinematicState.PostCinematic:
					if (this.TickPostCinematic(dt))
					{
						this.BeginStateTransition(HideoutCinematicController.HideoutCinematicState.Completed);
						return;
					}
					break;
				case HideoutCinematicController.HideoutCinematicState.Completed:
				{
					HideoutCinematicController.OnHideoutCinematicFinished cinematicFinishedCallback = this._cinematicFinishedCallback;
					if (cinematicFinishedCallback != null)
					{
						cinematicFinishedCallback();
					}
					this.State = HideoutCinematicController.HideoutCinematicState.None;
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00013198 File Offset: 0x00011398
		private void TickStateTransition(float dt)
		{
			this._remainingTransitionDuration -= dt;
			if (this._remainingTransitionDuration <= 0f)
			{
				this.InStateTransition = false;
				HideoutCinematicController.OnHideoutCinematicStateChanged stateChangedCallback = this._stateChangedCallback;
				if (stateChangedCallback != null)
				{
					stateChangedCallback(this.State);
				}
				this._remainingStateDuration = this._stateDuration;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000131EC File Offset: 0x000113EC
		private bool TickInitialFadeOut(float dt)
		{
			this._remainingStateDuration -= dt;
			if (this._remainingStateDuration <= 0f)
			{
				Agent playerAgent = null;
				Agent bossAgent = null;
				List<Agent> playerCompanions = null;
				List<Agent> bossCompanions = null;
				float placementPerturbation = 0.25f;
				float placementAngle = 0.20943952f;
				HideoutCinematicController.OnInitialFadeOutFinished initialFadeOutFinished = this._initialFadeOutFinished;
				if (initialFadeOutFinished != null)
				{
					initialFadeOutFinished(ref playerAgent, ref playerCompanions, ref bossAgent, ref bossCompanions, ref placementPerturbation, ref placementAngle);
				}
				this.ComputeAgentFrames(playerAgent, playerCompanions, bossAgent, bossCompanions, placementPerturbation, placementAngle);
			}
			return this._remainingStateDuration <= 0f;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00013268 File Offset: 0x00011468
		private bool TickPreCinematic(float dt)
		{
			Scene scene = base.Mission.Scene;
			this._remainingStateDuration -= dt;
			switch (this._preCinematicPhase)
			{
			case HideoutCinematicController.HideoutPreCinematicPhase.InitializeFormations:
			{
				this._playerAgentInfo.Agent.Controller = Agent.ControllerType.AI;
				bool isTeleportingAgents = base.Mission.IsTeleportingAgents;
				base.Mission.IsTeleportingAgents = true;
				MatrixFrame matrixFrame;
				this._hideoutBossFightBehavior.GetAlliesInitialFrame(out matrixFrame);
				foreach (Formation formation in base.Mission.Teams.Attacker.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						WorldPosition position = new WorldPosition(scene, matrixFrame.origin);
						formation.SetMovementOrder(MovementOrder.MovementOrderMove(position));
					}
				}
				MatrixFrame matrixFrame2;
				this._hideoutBossFightBehavior.GetBanditsInitialFrame(out matrixFrame2);
				foreach (Formation formation2 in base.Mission.Teams.Defender.FormationsIncludingEmpty)
				{
					if (formation2.CountOfUnits > 0)
					{
						WorldPosition position2 = new WorldPosition(scene, matrixFrame2.origin);
						formation2.SetMovementOrder(MovementOrder.MovementOrderMove(position2));
					}
				}
				foreach (HideoutCinematicController.HideoutCinematicAgentInfo hideoutCinematicAgentInfo in this._hideoutAgentsInfo)
				{
					Agent agent = hideoutCinematicAgentInfo.Agent;
					Vec3 f = hideoutCinematicAgentInfo.InitialFrame.rotation.f;
					agent.LookDirection = f;
					Agent agent2 = agent;
					Vec2 vec = f.AsVec2;
					vec = vec.Normalized();
					agent2.SetMovementDirection(vec);
				}
				base.Mission.IsTeleportingAgents = isTeleportingAgents;
				this._preCinematicPhase = HideoutCinematicController.HideoutPreCinematicPhase.StopFormations;
				break;
			}
			case HideoutCinematicController.HideoutPreCinematicPhase.StopFormations:
				foreach (Formation formation3 in base.Mission.Teams.Attacker.FormationsIncludingEmpty)
				{
					if (formation3.CountOfUnits > 0)
					{
						formation3.SetMovementOrder(MovementOrder.MovementOrderStop);
					}
				}
				foreach (Formation formation4 in base.Mission.Teams.Defender.FormationsIncludingEmpty)
				{
					if (formation4.CountOfUnits > 0)
					{
						formation4.SetMovementOrder(MovementOrder.MovementOrderStop);
					}
				}
				this._preCinematicPhase = HideoutCinematicController.HideoutPreCinematicPhase.InitializeAgents;
				break;
			case HideoutCinematicController.HideoutPreCinematicPhase.InitializeAgents:
			{
				bool isTeleportingAgents2 = base.Mission.IsTeleportingAgents;
				base.Mission.IsTeleportingAgents = true;
				this._cachedAgentFormations = new List<Formation>();
				foreach (HideoutCinematicController.HideoutCinematicAgentInfo hideoutCinematicAgentInfo2 in this._hideoutAgentsInfo)
				{
					Agent agent3 = hideoutCinematicAgentInfo2.Agent;
					this._cachedAgentFormations.Add(agent3.Formation);
					agent3.Formation = null;
					MatrixFrame initialFrame = hideoutCinematicAgentInfo2.InitialFrame;
					WorldPosition worldPosition = new WorldPosition(scene, initialFrame.origin);
					Vec3 f2 = initialFrame.rotation.f;
					agent3.TeleportToPosition(worldPosition.GetGroundVec3());
					agent3.LookDirection = f2;
					Agent agent4 = agent3;
					Vec2 vec = f2.AsVec2;
					vec = vec.Normalized();
					agent4.SetMovementDirection(vec);
				}
				base.Mission.IsTeleportingAgents = isTeleportingAgents2;
				this._preCinematicPhase = HideoutCinematicController.HideoutPreCinematicPhase.MoveAgents;
				break;
			}
			case HideoutCinematicController.HideoutPreCinematicPhase.MoveAgents:
				foreach (HideoutCinematicController.HideoutCinematicAgentInfo hideoutCinematicAgentInfo3 in this._hideoutAgentsInfo)
				{
					Agent agent5 = hideoutCinematicAgentInfo3.Agent;
					MatrixFrame targetFrame = hideoutCinematicAgentInfo3.TargetFrame;
					WorldPosition worldPosition2 = new WorldPosition(scene, targetFrame.origin);
					agent5.SetMaximumSpeedLimit(0.65f, false);
					Agent agent6 = agent5;
					Vec2 vec = targetFrame.rotation.f.AsVec2;
					agent6.SetScriptedPositionAndDirection(ref worldPosition2, vec.RotationInRadians, true, Agent.AIScriptedFrameFlags.None);
				}
				this._preCinematicPhase = HideoutCinematicController.HideoutPreCinematicPhase.Completed;
				break;
			}
			return this._preCinematicPhase == HideoutCinematicController.HideoutPreCinematicPhase.Completed && this._remainingStateDuration <= 0f;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000136E0 File Offset: 0x000118E0
		private bool TickCinematic(float dt)
		{
			this._remainingCinematicDuration -= dt;
			this._remainingStateDuration -= dt;
			return this._remainingCinematicDuration <= 0f && this._remainingStateDuration <= 0f;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001371C File Offset: 0x0001191C
		private bool TickPostCinematic(float dt)
		{
			this._remainingStateDuration -= dt;
			HideoutCinematicController.HideoutPostCinematicPhase postCinematicPhase = this._postCinematicPhase;
			if (postCinematicPhase != HideoutCinematicController.HideoutPostCinematicPhase.MoveAgents)
			{
				if (postCinematicPhase == HideoutCinematicController.HideoutPostCinematicPhase.FinalizeAgents)
				{
					foreach (HideoutCinematicController.HideoutCinematicAgentInfo hideoutCinematicAgentInfo in this._hideoutAgentsInfo)
					{
						Agent agent = hideoutCinematicAgentInfo.Agent;
						agent.DisableScriptedMovement();
						agent.SetMaximumSpeedLimit(-1f, false);
					}
					this._postCinematicPhase = HideoutCinematicController.HideoutPostCinematicPhase.Completed;
				}
			}
			else
			{
				int num = 0;
				foreach (HideoutCinematicController.HideoutCinematicAgentInfo hideoutCinematicAgentInfo2 in this._hideoutAgentsInfo)
				{
					Agent agent2 = hideoutCinematicAgentInfo2.Agent;
					if (!hideoutCinematicAgentInfo2.HasReachedTarget(0.5f))
					{
						MatrixFrame targetFrame = hideoutCinematicAgentInfo2.TargetFrame;
						WorldPosition worldPosition = new WorldPosition(base.Mission.Scene, targetFrame.origin);
						agent2.TeleportToPosition(worldPosition.GetGroundVec3());
						Agent agent3 = agent2;
						Vec2 vec = targetFrame.rotation.f.AsVec2;
						vec = vec.Normalized();
						agent3.SetMovementDirection(vec);
					}
					agent2.Formation = this._cachedAgentFormations[num];
					num++;
				}
				this._postCinematicPhase = HideoutCinematicController.HideoutPostCinematicPhase.FinalizeAgents;
			}
			return this._postCinematicPhase == HideoutCinematicController.HideoutPostCinematicPhase.Completed && this._remainingStateDuration <= 0f;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00013894 File Offset: 0x00011A94
		private void BeginStateTransition(HideoutCinematicController.HideoutCinematicState nextState)
		{
			this.State = nextState;
			this._remainingTransitionDuration = this._transitionDuration;
			this.InStateTransition = true;
			HideoutCinematicController.OnHideoutCinematicTransition transitionCallback = this._transitionCallback;
			if (transitionCallback == null)
			{
				return;
			}
			transitionCallback(this.State, this._remainingTransitionDuration);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000138CC File Offset: 0x00011ACC
		private bool CheckNavMeshValidity(ref Vec3 initial, ref Vec3 target)
		{
			Scene scene = base.Mission.Scene;
			bool result = false;
			bool navigationMeshForPosition = scene.GetNavigationMeshForPosition(ref initial);
			bool navigationMeshForPosition2 = scene.GetNavigationMeshForPosition(ref target);
			if (navigationMeshForPosition && navigationMeshForPosition2)
			{
				WorldPosition position = new WorldPosition(scene, initial);
				WorldPosition destination = new WorldPosition(scene, target);
				result = scene.DoesPathExistBetweenPositions(position, destination);
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00013924 File Offset: 0x00011B24
		private void ComputeAgentFrames(Agent playerAgent, List<Agent> playerCompanions, Agent bossAgent, List<Agent> bossCompanions, float placementPerturbation, float placementAngle)
		{
			this._hideoutAgentsInfo = new List<HideoutCinematicController.HideoutCinematicAgentInfo>();
			MatrixFrame matrixFrame;
			MatrixFrame matrixFrame2;
			this._hideoutBossFightBehavior.GetPlayerFrames(out matrixFrame, out matrixFrame2, placementPerturbation);
			this._playerAgentInfo = new HideoutCinematicController.HideoutCinematicAgentInfo(playerAgent, HideoutCinematicController.HideoutAgentType.Player, ref matrixFrame, ref matrixFrame2);
			this._hideoutAgentsInfo.Add(this._playerAgentInfo);
			List<MatrixFrame> list;
			List<MatrixFrame> list2;
			this._hideoutBossFightBehavior.GetAllyFrames(out list, out list2, playerCompanions.Count, placementAngle, placementPerturbation);
			for (int i = 0; i < playerCompanions.Count; i++)
			{
				matrixFrame = list[i];
				matrixFrame2 = list2[i];
				this._hideoutAgentsInfo.Add(new HideoutCinematicController.HideoutCinematicAgentInfo(playerCompanions[i], HideoutCinematicController.HideoutAgentType.Ally, ref matrixFrame, ref matrixFrame2));
			}
			this._hideoutBossFightBehavior.GetBossFrames(out matrixFrame, out matrixFrame2, placementPerturbation);
			this._bossAgentInfo = new HideoutCinematicController.HideoutCinematicAgentInfo(bossAgent, HideoutCinematicController.HideoutAgentType.Boss, ref matrixFrame, ref matrixFrame2);
			this._hideoutAgentsInfo.Add(this._bossAgentInfo);
			this._hideoutBossFightBehavior.GetBanditFrames(out list, out list2, bossCompanions.Count, placementAngle, placementPerturbation);
			for (int j = 0; j < bossCompanions.Count; j++)
			{
				matrixFrame = list[j];
				matrixFrame2 = list2[j];
				this._hideoutAgentsInfo.Add(new HideoutCinematicController.HideoutCinematicAgentInfo(bossCompanions[j], HideoutCinematicController.HideoutAgentType.Bandit, ref matrixFrame, ref matrixFrame2));
			}
		}

		// Token: 0x0400015D RID: 349
		private const float AgentTargetProximityThreshold = 0.5f;

		// Token: 0x0400015E RID: 350
		private const float AgentMaxSpeedCinematicOverride = 0.65f;

		// Token: 0x0400015F RID: 351
		public const string HideoutSceneEntityTag = "hideout_boss_fight";

		// Token: 0x04000160 RID: 352
		public const float DefaultTransitionDuration = 0.4f;

		// Token: 0x04000161 RID: 353
		public const float DefaultStateDuration = 0.2f;

		// Token: 0x04000162 RID: 354
		public const float DefaultCinematicDuration = 8f;

		// Token: 0x04000163 RID: 355
		public const float DefaultPlacementPerturbation = 0.25f;

		// Token: 0x04000164 RID: 356
		public const float DefaultPlacementAngle = 0.20943952f;

		// Token: 0x04000165 RID: 357
		private HideoutCinematicController.OnInitialFadeOutFinished _initialFadeOutFinished;

		// Token: 0x04000166 RID: 358
		private HideoutCinematicController.OnHideoutCinematicFinished _cinematicFinishedCallback;

		// Token: 0x04000167 RID: 359
		private HideoutCinematicController.OnHideoutCinematicStateChanged _stateChangedCallback;

		// Token: 0x04000168 RID: 360
		private HideoutCinematicController.OnHideoutCinematicTransition _transitionCallback;

		// Token: 0x04000169 RID: 361
		private float _cinematicDuration = 8f;

		// Token: 0x0400016A RID: 362
		private float _stateDuration = 0.2f;

		// Token: 0x0400016B RID: 363
		private float _transitionDuration = 0.4f;

		// Token: 0x0400016C RID: 364
		private float _remainingCinematicDuration = 8f;

		// Token: 0x0400016D RID: 365
		private float _remainingStateDuration = 0.2f;

		// Token: 0x0400016E RID: 366
		private float _remainingTransitionDuration = 0.4f;

		// Token: 0x0400016F RID: 367
		private List<Formation> _cachedAgentFormations;

		// Token: 0x04000170 RID: 368
		private List<HideoutCinematicController.HideoutCinematicAgentInfo> _hideoutAgentsInfo;

		// Token: 0x04000171 RID: 369
		private HideoutCinematicController.HideoutCinematicAgentInfo _bossAgentInfo;

		// Token: 0x04000172 RID: 370
		private HideoutCinematicController.HideoutCinematicAgentInfo _playerAgentInfo;

		// Token: 0x04000173 RID: 371
		private bool _isBehaviorInit;

		// Token: 0x04000174 RID: 372
		private HideoutCinematicController.HideoutPreCinematicPhase _preCinematicPhase;

		// Token: 0x04000175 RID: 373
		private HideoutCinematicController.HideoutPostCinematicPhase _postCinematicPhase;

		// Token: 0x04000176 RID: 374
		private HideoutBossFightBehavior _hideoutBossFightBehavior;

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000BB6 RID: 2998
		public delegate void OnInitialFadeOutFinished(ref Agent playerAgent, ref List<Agent> playerCompanions, ref Agent bossAgent, ref List<Agent> bossCompanions, ref float placementPerturbation, ref float placementAngle);

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x06000BBA RID: 3002
		public delegate void OnHideoutCinematicFinished();

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000BBE RID: 3006
		public delegate void OnHideoutCinematicStateChanged(HideoutCinematicController.HideoutCinematicState state);

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x06000BC2 RID: 3010
		public delegate void OnHideoutCinematicTransition(HideoutCinematicController.HideoutCinematicState nextState, float duration);

		// Token: 0x02000129 RID: 297
		public readonly struct HideoutCinematicAgentInfo
		{
			// Token: 0x06000BC5 RID: 3013 RVA: 0x00052650 File Offset: 0x00050850
			public HideoutCinematicAgentInfo(Agent agent, HideoutCinematicController.HideoutAgentType type, in MatrixFrame initialFrame, in MatrixFrame targetFrame)
			{
				this.Agent = agent;
				this.InitialFrame = initialFrame;
				this.TargetFrame = targetFrame;
				this.Type = type;
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0005267C File Offset: 0x0005087C
			public bool HasReachedTarget(float proximityThreshold = 0.5f)
			{
				return this.Agent.Position.Distance(this.TargetFrame.origin) <= proximityThreshold;
			}

			// Token: 0x04000537 RID: 1335
			public readonly Agent Agent;

			// Token: 0x04000538 RID: 1336
			public readonly MatrixFrame InitialFrame;

			// Token: 0x04000539 RID: 1337
			public readonly MatrixFrame TargetFrame;

			// Token: 0x0400053A RID: 1338
			public readonly HideoutCinematicController.HideoutAgentType Type;
		}

		// Token: 0x0200012A RID: 298
		public enum HideoutCinematicState
		{
			// Token: 0x0400053C RID: 1340
			None,
			// Token: 0x0400053D RID: 1341
			InitialFadeOut,
			// Token: 0x0400053E RID: 1342
			PreCinematic,
			// Token: 0x0400053F RID: 1343
			Cinematic,
			// Token: 0x04000540 RID: 1344
			PostCinematic,
			// Token: 0x04000541 RID: 1345
			Completed
		}

		// Token: 0x0200012B RID: 299
		public enum HideoutAgentType
		{
			// Token: 0x04000543 RID: 1347
			Player,
			// Token: 0x04000544 RID: 1348
			Boss,
			// Token: 0x04000545 RID: 1349
			Ally,
			// Token: 0x04000546 RID: 1350
			Bandit
		}

		// Token: 0x0200012C RID: 300
		public enum HideoutPreCinematicPhase
		{
			// Token: 0x04000548 RID: 1352
			NotStarted,
			// Token: 0x04000549 RID: 1353
			InitializeFormations,
			// Token: 0x0400054A RID: 1354
			StopFormations,
			// Token: 0x0400054B RID: 1355
			InitializeAgents,
			// Token: 0x0400054C RID: 1356
			MoveAgents,
			// Token: 0x0400054D RID: 1357
			Completed
		}

		// Token: 0x0200012D RID: 301
		public enum HideoutPostCinematicPhase
		{
			// Token: 0x0400054F RID: 1359
			NotStarted,
			// Token: 0x04000550 RID: 1360
			MoveAgents,
			// Token: 0x04000551 RID: 1361
			FinalizeAgents,
			// Token: 0x04000552 RID: 1362
			Completed
		}
	}
}
