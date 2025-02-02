using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200006A RID: 106
	public class WhileEnteringSettlementBattleMissionController : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0001B52A File Offset: 0x0001972A
		public WhileEnteringSettlementBattleMissionController(IMissionTroopSupplier[] suppliers, int numberOfMaxTroopForPlayer, int numberOfMaxTroopForEnemy)
		{
			this._troopSuppliers = suppliers;
			this._numberOfMaxTroopForPlayer = numberOfMaxTroopForPlayer;
			this._numberOfMaxTroopForEnemy = numberOfMaxTroopForEnemy;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001B547 File Offset: 0x00019747
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._battleAgentLogic = Mission.Current.GetMissionBehavior<BattleAgentLogic>();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001B560 File Offset: 0x00019760
		public override void OnMissionTick(float dt)
		{
			if (!this._isMissionInitialized)
			{
				this.SpawnAgents();
				this._isMissionInitialized = true;
				return;
			}
			if (!this._troopsInitialized)
			{
				this._troopsInitialized = true;
				foreach (Agent agent in base.Mission.Agents)
				{
					this._battleAgentLogic.OnAgentBuild(agent, null);
				}
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001B5E4 File Offset: 0x000197E4
		private void SpawnAgents()
		{
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("sp_outside_near_town_main_gate");
			IMissionTroopSupplier[] troopSuppliers = this._troopSuppliers;
			for (int i = 0; i < troopSuppliers.Length; i++)
			{
				foreach (IAgentOriginBase agentOriginBase in troopSuppliers[i].SupplyTroops(this._numberOfMaxTroopForPlayer + this._numberOfMaxTroopForEnemy).ToList<IAgentOriginBase>())
				{
					bool flag = agentOriginBase.IsUnderPlayersCommand || agentOriginBase.Troop.IsPlayerCharacter;
					if ((!flag || this._numberOfMaxTroopForPlayer >= this._playerSideSpawnedTroopCount) && (flag || this._numberOfMaxTroopForEnemy >= this._otherSideSpawnedTroopCount))
					{
						WorldFrame worldFrame = new WorldFrame(gameEntity.GetGlobalFrame().rotation, new WorldPosition(base.Mission.Scene, gameEntity.GetGlobalFrame().origin));
						if (!flag)
						{
							worldFrame.Origin.SetVec2(worldFrame.Origin.AsVec2 + worldFrame.Rotation.f.AsVec2 * 20f);
							worldFrame.Rotation.f = (gameEntity.GetGlobalFrame().origin.AsVec2 - worldFrame.Origin.AsVec2).ToVec3(0f);
							worldFrame.Origin.SetVec2(base.Mission.GetRandomPositionAroundPoint(worldFrame.Origin.GetNavMeshVec3(), 0f, 2.5f, false).AsVec2);
						}
						worldFrame.Rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
						base.Mission.SpawnTroop(agentOriginBase, flag, false, false, false, 0, 0, true, false, false, new Vec3?(worldFrame.Origin.GetGroundVec3()), new Vec2?(worldFrame.Rotation.f.AsVec2), null, null, FormationClass.NumberOfAllFormations, false).Defensiveness = 1f;
						if (flag)
						{
							this._playerSideSpawnedTroopCount++;
						}
						else
						{
							this._otherSideSpawnedTroopCount++;
						}
					}
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001B82C File Offset: 0x00019A2C
		public void StartSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001B82E File Offset: 0x00019A2E
		public void StopSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001B830 File Offset: 0x00019A30
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return false;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001B833 File Offset: 0x00019A33
		public float GetReinforcementInterval()
		{
			return 0f;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001B83A File Offset: 0x00019A3A
		public bool IsSideDepleted(BattleSideEnum side)
		{
			if (side == base.Mission.PlayerTeam.Side)
			{
				return this._troopSuppliers[(int)side].NumRemovedTroops == this._playerSideSpawnedTroopCount;
			}
			return this._troopSuppliers[(int)side].NumRemovedTroops == this._otherSideSpawnedTroopCount;
		}

		// Token: 0x040001CC RID: 460
		private const int GuardSpawnPointAndPlayerSpawnPointPositionDelta = 20;

		// Token: 0x040001CD RID: 461
		private BattleAgentLogic _battleAgentLogic;

		// Token: 0x040001CE RID: 462
		private bool _isMissionInitialized;

		// Token: 0x040001CF RID: 463
		private bool _troopsInitialized;

		// Token: 0x040001D0 RID: 464
		private int _numberOfMaxTroopForPlayer;

		// Token: 0x040001D1 RID: 465
		private int _numberOfMaxTroopForEnemy;

		// Token: 0x040001D2 RID: 466
		private int _playerSideSpawnedTroopCount;

		// Token: 0x040001D3 RID: 467
		private int _otherSideSpawnedTroopCount;

		// Token: 0x040001D4 RID: 468
		private readonly IMissionTroopSupplier[] _troopSuppliers;
	}
}
