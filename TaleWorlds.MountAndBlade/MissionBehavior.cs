using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200025D RID: 605
	public abstract class MissionBehavior : IMissionBehavior
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00071D5E File Offset: 0x0006FF5E
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x00071D66 File Offset: 0x0006FF66
		public Mission Mission { get; internal set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00071D6F File Offset: 0x0006FF6F
		public IInputContext DebugInput
		{
			get
			{
				return Input.DebugInput;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002018 RID: 8216
		public abstract MissionBehaviorType BehaviorType { get; }

		// Token: 0x06002019 RID: 8217 RVA: 0x00071D76 File Offset: 0x0006FF76
		public virtual void OnAfterMissionCreated()
		{
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00071D78 File Offset: 0x0006FF78
		public virtual void OnBehaviorInitialize()
		{
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00071D7A File Offset: 0x0006FF7A
		public virtual void OnCreated()
		{
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00071D7C File Offset: 0x0006FF7C
		public virtual void EarlyStart()
		{
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00071D7E File Offset: 0x0006FF7E
		public virtual void AfterStart()
		{
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00071D80 File Offset: 0x0006FF80
		public virtual void OnMissileHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00071D82 File Offset: 0x0006FF82
		public virtual void OnMeleeHit(Agent attacker, Agent victim, bool isCanceled, AttackCollisionData collisionData)
		{
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00071D84 File Offset: 0x0006FF84
		public virtual void OnMissileCollisionReaction(Mission.MissileCollisionReaction collisionReaction, Agent attackerAgent, Agent attachedAgent, sbyte attachedBoneIndex)
		{
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x00071D86 File Offset: 0x0006FF86
		public virtual void OnMissionScreenPreLoad()
		{
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00071D88 File Offset: 0x0006FF88
		public virtual void OnAgentCreated(Agent agent)
		{
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00071D8A File Offset: 0x0006FF8A
		public virtual void OnAgentBuild(Agent agent, Banner banner)
		{
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00071D8C File Offset: 0x0006FF8C
		public virtual void OnAgentTeamChanged(Team prevTeam, Team newTeam, Agent agent)
		{
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00071D8E File Offset: 0x0006FF8E
		public virtual void OnAgentControllerSetToPlayer(Agent agent)
		{
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00071D90 File Offset: 0x0006FF90
		public virtual void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00071D92 File Offset: 0x0006FF92
		public virtual void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00071D94 File Offset: 0x0006FF94
		public virtual void OnEarlyAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00071D96 File Offset: 0x0006FF96
		public virtual void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00071D98 File Offset: 0x0006FF98
		public virtual void OnAgentDeleted(Agent affectedAgent)
		{
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00071D9A File Offset: 0x0006FF9A
		public virtual void OnAgentFleeing(Agent affectedAgent)
		{
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00071D9C File Offset: 0x0006FF9C
		public virtual void OnAgentPanicked(Agent affectedAgent)
		{
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00071D9E File Offset: 0x0006FF9E
		public virtual void OnFocusGained(Agent agent, IFocusable focusableObject, bool isInteractable)
		{
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00071DA0 File Offset: 0x0006FFA0
		public virtual void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00071DA2 File Offset: 0x0006FFA2
		public virtual void OnAddTeam(Team team)
		{
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00071DA4 File Offset: 0x0006FFA4
		public virtual void AfterAddTeam(Team team)
		{
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00071DA6 File Offset: 0x0006FFA6
		public virtual void OnAgentInteraction(Agent userAgent, Agent agent)
		{
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00071DA8 File Offset: 0x0006FFA8
		public virtual void OnClearScene()
		{
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00071DAA File Offset: 0x0006FFAA
		public virtual void OnEndMissionInternal()
		{
			this.OnEndMission();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00071DB2 File Offset: 0x0006FFB2
		protected virtual void OnEndMission()
		{
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00071DB4 File Offset: 0x0006FFB4
		public virtual void OnRemoveBehavior()
		{
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00071DB6 File Offset: 0x0006FFB6
		public virtual void OnPreMissionTick(float dt)
		{
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00071DB8 File Offset: 0x0006FFB8
		public virtual void OnPreDisplayMissionTick(float dt)
		{
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00071DBA File Offset: 0x0006FFBA
		public virtual void OnMissionTick(float dt)
		{
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00071DBC File Offset: 0x0006FFBC
		public virtual void OnAgentMount(Agent agent)
		{
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00071DBE File Offset: 0x0006FFBE
		public virtual void OnAgentDismount(Agent agent)
		{
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00071DC0 File Offset: 0x0006FFC0
		public virtual bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return false;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00071DC3 File Offset: 0x0006FFC3
		public virtual void OnEntityRemoved(GameEntity entity)
		{
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00071DC5 File Offset: 0x0006FFC5
		public virtual void OnObjectUsed(Agent userAgent, UsableMissionObject usedObject)
		{
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00071DC7 File Offset: 0x0006FFC7
		public virtual void OnObjectStoppedBeingUsed(Agent userAgent, UsableMissionObject usedObject)
		{
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00071DC9 File Offset: 0x0006FFC9
		public virtual void OnRenderingStarted()
		{
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00071DCB File Offset: 0x0006FFCB
		public virtual void OnMissionStateActivated()
		{
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00071DCD File Offset: 0x0006FFCD
		public virtual void OnMissionStateFinalized()
		{
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00071DCF File Offset: 0x0006FFCF
		public virtual void OnMissionStateDeactivated()
		{
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00071DD1 File Offset: 0x0006FFD1
		public virtual List<CompassItemUpdateParams> GetCompassTargets()
		{
			return null;
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00071DD4 File Offset: 0x0006FFD4
		public virtual void OnAssignPlayerAsSergeantOfFormation(Agent agent)
		{
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00071DD6 File Offset: 0x0006FFD6
		public virtual void OnDeploymentFinished()
		{
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00071DD8 File Offset: 0x0006FFD8
		public virtual void OnTeamDeployed(Team team)
		{
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00071DDA File Offset: 0x0006FFDA
		protected internal virtual void OnGetAgentState(Agent agent, bool usedSurgery)
		{
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00071DDC File Offset: 0x0006FFDC
		public virtual void OnAgentAlarmedStateChanged(Agent agent, Agent.AIStateFlag flag)
		{
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00071DDE File Offset: 0x0006FFDE
		protected internal virtual void OnObjectDisabled(DestructableComponent destructionComponent)
		{
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00071DE0 File Offset: 0x0006FFE0
		public virtual void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00071DE2 File Offset: 0x0006FFE2
		protected internal virtual void OnAgentControllerChanged(Agent agent, Agent.ControllerType oldController)
		{
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00071DE4 File Offset: 0x0006FFE4
		public virtual void OnRegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon)
		{
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00071DE6 File Offset: 0x0006FFE6
		public virtual void OnAgentShootMissile(Agent shooterAgent, EquipmentIndex weaponIndex, Vec3 position, Vec3 velocity, Mat3 orientation, bool hasRigidBody, int forcedMissileIndex)
		{
		}
	}
}
