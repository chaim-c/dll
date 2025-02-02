using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000048 RID: 72
	public class BattleAgentLogic : MissionLogic
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0001126B File Offset: 0x0000F46B
		public override void AfterStart()
		{
			this._battleObserverMissionLogic = Mission.Current.GetMissionBehavior<BattleObserverMissionLogic>();
			this.CheckPerkEffectsOnTeams();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00011284 File Offset: 0x0000F484
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.Character != null && agent.Origin != null)
			{
				PartyBase partyBase = (PartyBase)agent.Origin.BattleCombatant;
				CharacterObject character = (CharacterObject)agent.Character;
				if (partyBase != null)
				{
					this._troopUpgradeTracker.AddTrackedTroop(partyBase, character);
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			if (affectedAgent.Character != null && affectorAgent != null && affectorAgent.Character != null && affectedAgent.State == AgentState.Active && affectorAgent != null)
			{
				bool flag = affectedAgent.Health - (float)blow.InflictedDamage < 1f;
				bool flag2 = false;
				if (affectedAgent.Team != null && affectorAgent.Team != null)
				{
					flag2 = (affectedAgent.Team.Side == affectorAgent.Team.Side);
				}
				IAgentOriginBase origin = affectorAgent.Origin;
				BasicCharacterObject character = affectedAgent.Character;
				Formation formation = affectorAgent.Formation;
				BasicCharacterObject formationCaptain;
				if (formation == null)
				{
					formationCaptain = null;
				}
				else
				{
					Agent captain = formation.Captain;
					formationCaptain = ((captain != null) ? captain.Character : null);
				}
				int inflictedDamage = blow.InflictedDamage;
				bool isFatal = flag;
				bool isTeamKill = flag2;
				MissionWeapon missionWeapon = attackerWeapon;
				origin.OnScoreHit(character, formationCaptain, inflictedDamage, isFatal, isTeamKill, missionWeapon.CurrentUsageItem);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00011394 File Offset: 0x0000F594
		public override void OnAgentTeamChanged(Team prevTeam, Team newTeam, Agent agent)
		{
			if (prevTeam != null && newTeam != null && prevTeam != newTeam)
			{
				BattleObserverMissionLogic battleObserverMissionLogic = this._battleObserverMissionLogic;
				if (battleObserverMissionLogic == null)
				{
					return;
				}
				IBattleObserver battleObserver = battleObserverMissionLogic.BattleObserver;
				if (battleObserver == null)
				{
					return;
				}
				battleObserver.TroopSideChanged((prevTeam != null) ? prevTeam.Side : BattleSideEnum.None, (newTeam != null) ? newTeam.Side : BattleSideEnum.None, (PartyBase)agent.Origin.BattleCombatant, agent.Character);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent == null)
			{
				return;
			}
			if (affectorAgent.IsMount && affectorAgent.RiderAgent != null)
			{
				affectorAgent = affectorAgent.RiderAgent;
			}
			if (affectorAgent.Character == null || affectedAgent.Character == null)
			{
				return;
			}
			float num = (float)blow.InflictedDamage;
			if (num > affectedAgent.HealthLimit)
			{
				num = affectedAgent.HealthLimit;
			}
			float num2 = num / affectedAgent.HealthLimit;
			this.EnemyHitReward(affectedAgent, affectorAgent, blow.MovementSpeedDamageModifier, shotDifficulty, isSiegeEngineHit, attackerWeapon, blow.AttackType, 0.5f * num2, num);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011474 File Offset: 0x0000F674
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectorAgent == null && affectedAgent.IsMount && agentState == AgentState.Routed)
			{
				return;
			}
			CharacterObject characterObject = (CharacterObject)affectedAgent.Character;
			CharacterObject characterObject2 = (CharacterObject)((affectorAgent != null) ? affectorAgent.Character : null);
			if (affectedAgent.Origin != null)
			{
				PartyBase partyBase = (PartyBase)affectedAgent.Origin.BattleCombatant;
				if (agentState == AgentState.Unconscious)
				{
					affectedAgent.Origin.SetWounded();
				}
				else if (agentState == AgentState.Killed)
				{
					affectedAgent.Origin.SetKilled();
					Hero hero = affectedAgent.IsHuman ? characterObject.HeroObject : null;
					Hero hero2 = (affectorAgent == null) ? null : (affectorAgent.IsHuman ? characterObject2.HeroObject : null);
					if (hero != null && hero2 != null)
					{
						CampaignEventDispatcher.Instance.OnCharacterDefeated(hero2, hero);
					}
					if (partyBase != null)
					{
						this.CheckUpgrade(affectedAgent.Team.Side, partyBase, characterObject);
					}
				}
				else
				{
					affectedAgent.Origin.SetRouted();
				}
			}
			if (affectedAgent.Origin == null || affectorAgent == null || affectorAgent.Origin == null || affectorAgent.Character == null || agentState != AgentState.Killed)
			{
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011570 File Offset: 0x0000F770
		public override void OnAgentFleeing(Agent affectedAgent)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00011572 File Offset: 0x0000F772
		public override void OnMissionTick(float dt)
		{
			this.UpdateMorale();
			if (this._nextMoraleCheckTime.IsPast)
			{
				this._nextMoraleCheckTime = MissionTime.SecondsFromNow(10f);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011597 File Offset: 0x0000F797
		private void CheckPerkEffectsOnTeams()
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00011599 File Offset: 0x0000F799
		private void UpdateMorale()
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001159C File Offset: 0x0000F79C
		private void EnemyHitReward(Agent affectedAgent, Agent affectorAgent, float lastSpeedBonus, float lastShotDifficulty, bool isSiegeEngineHit, WeaponComponentData lastAttackerWeapon, AgentAttackType attackType, float hitpointRatio, float damageAmount)
		{
			CharacterObject affectedCharacter = (CharacterObject)affectedAgent.Character;
			CharacterObject characterObject = (CharacterObject)affectorAgent.Character;
			if (affectedAgent.Origin != null && affectorAgent != null && affectorAgent.Origin != null && affectorAgent.Team != null && affectorAgent.Team.IsValid && affectedAgent.Team != null && affectedAgent.Team.IsValid)
			{
				PartyBase partyBase = (PartyBase)affectorAgent.Origin.BattleCombatant;
				Hero captain = BattleAgentLogic.GetCaptain(affectorAgent);
				Hero hero = (affectorAgent.Team.Leader != null && affectorAgent.Team.Leader.Character.IsHero) ? ((CharacterObject)affectorAgent.Team.Leader.Character).HeroObject : null;
				bool isTeamKill = affectorAgent.Team.Side == affectedAgent.Team.Side;
				bool flag = affectorAgent.MountAgent != null;
				bool isHorseCharge = flag && attackType == AgentAttackType.Collision;
				SkillLevelingManager.OnCombatHit(characterObject, affectedCharacter, (captain != null) ? captain.CharacterObject : null, hero, lastSpeedBonus, lastShotDifficulty, lastAttackerWeapon, hitpointRatio, CombatXpModel.MissionTypeEnum.Battle, flag, isTeamKill, hero != null && affectorAgent.Character != hero.CharacterObject && (hero != Hero.MainHero || affectorAgent.Formation == null || !affectorAgent.Formation.IsAIControlled), damageAmount, affectedAgent.Health < 1f, isSiegeEngineHit, isHorseCharge);
				BattleObserverMissionLogic battleObserverMissionLogic = this._battleObserverMissionLogic;
				if (((battleObserverMissionLogic != null) ? battleObserverMissionLogic.BattleObserver : null) != null && affectorAgent.Character != null)
				{
					if (affectorAgent.Character.IsHero)
					{
						Hero heroObject = characterObject.HeroObject;
						using (IEnumerator<SkillObject> enumerator = this._troopUpgradeTracker.CheckSkillUpgrades(heroObject).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SkillObject skill = enumerator.Current;
								this._battleObserverMissionLogic.BattleObserver.HeroSkillIncreased(affectorAgent.Team.Side, partyBase, characterObject, skill);
							}
							return;
						}
					}
					this.CheckUpgrade(affectorAgent.Team.Side, partyBase, characterObject);
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000117BC File Offset: 0x0000F9BC
		private static Hero GetCaptain(Agent affectorAgent)
		{
			Hero result = null;
			if (affectorAgent.Formation != null)
			{
				Agent captain = affectorAgent.Formation.Captain;
				if (captain != null)
				{
					float captainRadius = Campaign.Current.Models.CombatXpModel.CaptainRadius;
					if (captain.Position.Distance(affectorAgent.Position) < captainRadius)
					{
						result = ((CharacterObject)captain.Character).HeroObject;
					}
				}
			}
			return result;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011820 File Offset: 0x0000FA20
		private void CheckUpgrade(BattleSideEnum side, PartyBase party, CharacterObject character)
		{
			BattleObserverMissionLogic battleObserverMissionLogic = this._battleObserverMissionLogic;
			if (((battleObserverMissionLogic != null) ? battleObserverMissionLogic.BattleObserver : null) != null)
			{
				int num = this._troopUpgradeTracker.CheckUpgradedCount(party, character);
				if (num != 0)
				{
					this._battleObserverMissionLogic.BattleObserver.TroopNumberChanged(side, party, character, 0, 0, 0, 0, 0, num);
				}
			}
		}

		// Token: 0x04000141 RID: 321
		private BattleObserverMissionLogic _battleObserverMissionLogic;

		// Token: 0x04000142 RID: 322
		private TroopUpgradeTracker _troopUpgradeTracker = new TroopUpgradeTracker();

		// Token: 0x04000143 RID: 323
		private const float XpShareForKill = 0.5f;

		// Token: 0x04000144 RID: 324
		private const float XpShareForDamage = 0.5f;

		// Token: 0x04000145 RID: 325
		private MissionTime _nextMoraleCheckTime;
	}
}
