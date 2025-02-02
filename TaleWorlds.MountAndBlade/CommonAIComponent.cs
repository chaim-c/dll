using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000104 RID: 260
	public class CommonAIComponent : AgentComponent
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0001662C File Offset: 0x0001482C
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x00016634 File Offset: 0x00014834
		public bool IsPanicked { get; private set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0001663D File Offset: 0x0001483D
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00016645 File Offset: 0x00014845
		public bool IsRetreating { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0001664E File Offset: 0x0001484E
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00016656 File Offset: 0x00014856
		public int ReservedRiderAgentIndex { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0001665F File Offset: 0x0001485F
		public float InitialMorale
		{
			get
			{
				return this._initialMorale;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00016667 File Offset: 0x00014867
		public float RecoveryMorale
		{
			get
			{
				return this._recoveryMorale;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0001666F File Offset: 0x0001486F
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00016677 File Offset: 0x00014877
		public float Morale
		{
			get
			{
				return this._morale;
			}
			set
			{
				this._morale = MBMath.ClampFloat(value, 0f, 100f);
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00016690 File Offset: 0x00014890
		public CommonAIComponent(Agent agent) : base(agent)
		{
			this._fadeOutTimer = new Timer(Mission.Current.CurrentTime, 0.5f + MBRandom.RandomFloat * 0.1f, true);
			float num = agent.Monster.BodyCapsuleRadius * 2f * 7.5f;
			this._retreatDistanceSquared = num * num;
			this.ReservedRiderAgentIndex = -1;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000166FE File Offset: 0x000148FE
		public override void Initialize()
		{
			base.Initialize();
			this.InitializeMorale();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0001670C File Offset: 0x0001490C
		private void InitializeMorale()
		{
			int num = MBRandom.RandomInt(30);
			float num2 = this.Agent.Components.Sum((AgentComponent c) => c.GetMoraleAddition());
			float num3 = 35f + (float)num + num2;
			num3 = MissionGameModels.Current.BattleMoraleModel.GetEffectiveInitialMorale(this.Agent, num3);
			num3 = MBMath.ClampFloat(num3, 15f, 100f);
			this._initialMorale = num3;
			this._recoveryMorale = this._initialMorale * 0.5f;
			this.Morale = this._initialMorale;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000167AC File Offset: 0x000149AC
		public override void OnTickAsAI(float dt)
		{
			base.OnTickAsAI(dt);
			if (!this.IsRetreating && this._morale < 0.01f)
			{
				if (this.CanPanic())
				{
					this.Panic();
				}
				else
				{
					this.Morale = 0.01f;
				}
			}
			if (!this.IsPanicked && this._morale < this._recoveryMorale)
			{
				this.Morale = Math.Min(this._morale + 0.4f * dt, this._recoveryMorale);
			}
			if (Mission.Current.CanAgentRout(this.Agent) && this._fadeOutTimer.Check(Mission.Current.CurrentTime) && !this.Agent.IsFadingOut())
			{
				Vec3 position = this.Agent.Position;
				WorldPosition retreatPos = this.Agent.GetRetreatPos();
				if ((retreatPos.AsVec2.IsValid && retreatPos.AsVec2.DistanceSquared(position.AsVec2) < this._retreatDistanceSquared && retreatPos.GetGroundVec3().DistanceSquared(position) < this._retreatDistanceSquared) || !this.Agent.Mission.IsPositionInsideBoundaries(position.AsVec2) || position.DistanceSquared(this.Agent.Mission.GetClosestBoundaryPosition(position.AsVec2).ToVec3(0f)) < this._retreatDistanceSquared)
				{
					this.Agent.StartFadingOut();
				}
			}
			if (this.IsPanicked && this.Agent.Mission.MissionEnded)
			{
				MissionResult missionResult = this.Agent.Mission.MissionResult;
				if (this.Agent.Team != null && missionResult != null && ((missionResult.PlayerVictory && (this.Agent.Team.IsPlayerTeam || this.Agent.Team.IsPlayerAlly)) || (missionResult.PlayerDefeated && !this.Agent.Team.IsPlayerTeam && !this.Agent.Team.IsPlayerAlly)) && this.Agent != Agent.Main && this.Agent.IsActive())
				{
					this.StopRetreating();
				}
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000169D7 File Offset: 0x00014BD7
		public void Panic()
		{
			if (this.IsPanicked)
			{
				return;
			}
			this.IsPanicked = true;
			this.Agent.Mission.OnAgentPanicked(this.Agent);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00016A00 File Offset: 0x00014C00
		public void Retreat(bool useCachingSystem = false)
		{
			if (!this.IsRetreating)
			{
				this.IsRetreating = true;
				this.Agent.EnforceShieldUsage(Agent.UsageDirection.None);
				WorldPosition worldPosition = WorldPosition.Invalid;
				if (useCachingSystem)
				{
					worldPosition = this.Agent.Formation.RetreatPositionCache.GetRetreatPositionFromCache(this.Agent.Position.AsVec2);
				}
				if (!worldPosition.IsValid)
				{
					worldPosition = this.Agent.Mission.GetClosestFleePositionForAgent(this.Agent);
					if (useCachingSystem)
					{
						this.Agent.Formation.RetreatPositionCache.AddNewPositionToCache(this.Agent.Position.AsVec2, worldPosition);
					}
				}
				this.Agent.Retreat(worldPosition);
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00016AB8 File Offset: 0x00014CB8
		public void StopRetreating()
		{
			if (!this.IsRetreating)
			{
				return;
			}
			this.IsRetreating = false;
			this.IsPanicked = false;
			float morale = MathF.Max(0.02f, this.Morale);
			this.Agent.SetMorale(morale);
			this.Agent.StopRetreating();
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00016B04 File Offset: 0x00014D04
		public bool CanPanic()
		{
			if (!MissionGameModels.Current.BattleMoraleModel.CanPanicDueToMorale(this.Agent))
			{
				return false;
			}
			TeamAISiegeComponent teamAISiegeComponent;
			if (Mission.Current.IsSiegeBattle && this.Agent.Team.Side == BattleSideEnum.Attacker && (teamAISiegeComponent = (this.Agent.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				int currentNavigationFaceId = this.Agent.GetCurrentNavigationFaceId();
				if (currentNavigationFaceId % 10 == 1)
				{
					return false;
				}
				if (teamAISiegeComponent.IsPrimarySiegeWeaponNavmeshFaceId(currentNavigationFaceId))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00016B83 File Offset: 0x00014D83
		public override void OnHit(Agent affectorAgent, int damage, in MissionWeapon affectorWeapon)
		{
			base.OnHit(affectorAgent, damage, affectorWeapon);
			if (damage >= 1 && this.Agent.IsMount && this.Agent.IsAIControlled && this.Agent.RiderAgent == null)
			{
				this.Panic();
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public override void OnAgentRemoved()
		{
			base.OnAgentRemoved();
			if (this.Agent.IsMount && this.Agent.RiderAgent == null)
			{
				Agent agent = this.FindReservingAgent();
				if (agent != null)
				{
					agent.HumanAIComponent.UnreserveMount(this.Agent);
				}
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00016C08 File Offset: 0x00014E08
		public override void OnComponentRemoved()
		{
			base.OnComponentRemoved();
			if (this.Agent.IsMount && this.Agent.RiderAgent == null)
			{
				Agent agent = this.FindReservingAgent();
				if (agent != null)
				{
					agent.HumanAIComponent.UnreserveMount(this.Agent);
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00016C50 File Offset: 0x00014E50
		internal void OnMountReserved(int riderAgentIndex)
		{
			this.ReservedRiderAgentIndex = riderAgentIndex;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00016C59 File Offset: 0x00014E59
		internal void OnMountUnreserved()
		{
			this.ReservedRiderAgentIndex = -1;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00016C64 File Offset: 0x00014E64
		private Agent FindReservingAgent()
		{
			Agent result = null;
			if (this.ReservedRiderAgentIndex >= 0)
			{
				foreach (Agent agent in Mission.Current.Agents)
				{
					if (agent.Index == this.ReservedRiderAgentIndex)
					{
						result = agent;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x040002D1 RID: 721
		private const float MoraleThresholdForPanicking = 0.01f;

		// Token: 0x040002D2 RID: 722
		private const float MaxRecoverableMoraleMultiplier = 0.5f;

		// Token: 0x040002D3 RID: 723
		private const float MoraleRecoveryPerSecond = 0.4f;

		// Token: 0x040002D7 RID: 727
		private float _recoveryMorale;

		// Token: 0x040002D8 RID: 728
		private float _initialMorale;

		// Token: 0x040002D9 RID: 729
		private float _morale = 50f;

		// Token: 0x040002DA RID: 730
		private readonly Timer _fadeOutTimer;

		// Token: 0x040002DB RID: 731
		private readonly float _retreatDistanceSquared;
	}
}
