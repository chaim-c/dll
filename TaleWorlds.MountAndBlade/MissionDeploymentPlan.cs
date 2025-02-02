using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000211 RID: 529
	public class MissionDeploymentPlan : IMissionDeploymentPlan
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x000660F8 File Offset: 0x000642F8
		public MissionDeploymentPlan(Mission mission)
		{
			this._mission = mission;
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum side = (BattleSideEnum)i;
				this._battleSideDeploymentPlans[i] = new BattleSideDeploymentPlan(mission, side);
				this._playerSpawnFrames[i] = null;
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0006615C File Offset: 0x0006435C
		public void CreateReinforcementPlans()
		{
			for (int i = 0; i < 2; i++)
			{
				this._battleSideDeploymentPlans[i].CreateReinforcementPlans();
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00066184 File Offset: 0x00064384
		public void ClearDeploymentPlanForSide(BattleSideEnum battleSide, DeploymentPlanType planType)
		{
			this._battleSideDeploymentPlans[(int)battleSide].ClearPlans(planType);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00066194 File Offset: 0x00064394
		public bool HasPlayerSpawnFrame(BattleSideEnum battleSide)
		{
			return this._playerSpawnFrames[(int)battleSide] != null;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000661A8 File Offset: 0x000643A8
		public bool GetPlayerSpawnFrame(BattleSideEnum battleSide, out WorldPosition position, out Vec2 direction)
		{
			WorldFrame? worldFrame = this._playerSpawnFrames[(int)battleSide];
			if (worldFrame != null)
			{
				Scene scene = Mission.Current.Scene;
				UIntPtr zero = UIntPtr.Zero;
				WorldFrame value = worldFrame.Value;
				position = new WorldPosition(scene, zero, value.Origin.GetGroundVec3(), false);
				value = worldFrame.Value;
				direction = value.Rotation.f.AsVec2.Normalized();
				return true;
			}
			position = WorldPosition.Invalid;
			direction = Vec2.Invalid;
			return false;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0006623A File Offset: 0x0006443A
		public static bool HasSignificantMountedTroops(int footTroopCount, int mountedTroopCount)
		{
			return (float)mountedTroopCount / Math.Max((float)(mountedTroopCount + footTroopCount), 1f) >= 0.1f;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00066257 File Offset: 0x00064457
		public void ClearAddedTroopsForBattleSide(BattleSideEnum battleSide, DeploymentPlanType planType)
		{
			this._battleSideDeploymentPlans[(int)battleSide].ClearAddedTroops(planType);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00066268 File Offset: 0x00064468
		public void ClearAll()
		{
			for (int i = 0; i < 2; i++)
			{
				this._battleSideDeploymentPlans[i].ClearAddedTroops(DeploymentPlanType.Initial);
				this._battleSideDeploymentPlans[i].ClearPlans(DeploymentPlanType.Initial);
				this._battleSideDeploymentPlans[i].ClearAddedTroops(DeploymentPlanType.Reinforcement);
				this._battleSideDeploymentPlans[i].ClearPlans(DeploymentPlanType.Reinforcement);
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000662B9 File Offset: 0x000644B9
		public void AddTroopsForBattleSide(BattleSideEnum battleSide, DeploymentPlanType planType, FormationClass formationClass, int footTroopCount, int mountedTroopCount)
		{
			this._battleSideDeploymentPlans[(int)battleSide].AddTroops(formationClass, footTroopCount, mountedTroopCount, planType);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000662D0 File Offset: 0x000644D0
		public void SetSpawnWithHorsesForSide(BattleSideEnum battleSide, bool spawnWithHorses)
		{
			this._battleSideDeploymentPlans[(int)battleSide].SetSpawnWithHorses(spawnWithHorses);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000662F0 File Offset: 0x000644F0
		public void PlanBattleDeployment(BattleSideEnum battleSide, DeploymentPlanType planType, float spawnPathOffset = 0f)
		{
			if (!battleSide.IsValid())
			{
				Debug.FailedAssert("Cannot make deployment plan. Battle side is not valid", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "PlanBattleDeployment", 126);
				return;
			}
			BattleSideDeploymentPlan battleSideDeploymentPlan = this._battleSideDeploymentPlans[(int)battleSide];
			if (this._battleSideDeploymentPlans[(int)battleSide].IsPlanMade(planType))
			{
				battleSideDeploymentPlan.ClearPlans(planType);
			}
			if (!this._mission.HasSpawnPath && this._formationSceneSpawnEntries == null)
			{
				this.ReadSpawnEntitiesFromScene(this._mission.IsFieldBattle);
			}
			battleSideDeploymentPlan.PlanBattleDeployment(this._formationSceneSpawnEntries, planType, spawnPathOffset);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00066374 File Offset: 0x00064574
		public bool IsPositionInsideDeploymentBoundaries(BattleSideEnum battleSide, in Vec2 position)
		{
			BattleSideDeploymentPlan battleSideDeploymentPlan = this._battleSideDeploymentPlans[(int)battleSide];
			if (battleSideDeploymentPlan.HasDeploymentBoundaries())
			{
				return battleSideDeploymentPlan.IsPositionInsideDeploymentBoundaries(position);
			}
			Debug.FailedAssert("Cannot check if position is within deployment boundaries as requested battle side does not have deployment boundaries.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "IsPositionInsideDeploymentBoundaries", 155);
			return false;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000663B4 File Offset: 0x000645B4
		public bool IsPositionInsideSiegeDeploymentBoundaries(in Vec2 position)
		{
			bool result = false;
			foreach (ICollection<Vec2> source in this._mission.Boundaries.Values)
			{
				if (MBSceneUtilities.IsPointInsideBoundaries(position, source.ToList<Vec2>(), 0.05f))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00066420 File Offset: 0x00064620
		public Vec2 GetClosestDeploymentBoundaryPosition(BattleSideEnum battleSide, in Vec2 position, bool withNavMesh = false, float positionZ = 0f)
		{
			BattleSideDeploymentPlan battleSideDeploymentPlan = this._battleSideDeploymentPlans[(int)battleSide];
			if (battleSideDeploymentPlan.HasDeploymentBoundaries())
			{
				return battleSideDeploymentPlan.GetClosestDeploymentBoundaryPosition(position, withNavMesh, positionZ);
			}
			Debug.FailedAssert("Cannot retrieve closest deployment boundary position as requested battle side does not have deployment boundaries.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "GetClosestDeploymentBoundaryPosition", 183);
			return position;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00066468 File Offset: 0x00064668
		public int GetTroopCountForSide(BattleSideEnum side, DeploymentPlanType planType)
		{
			return this._battleSideDeploymentPlans[(int)side].GetTroopCount(planType);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00066478 File Offset: 0x00064678
		public float GetSpawnPathOffsetForSide(BattleSideEnum side, DeploymentPlanType planType)
		{
			return this._battleSideDeploymentPlans[(int)side].GetSpawnPathOffset(planType);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00066488 File Offset: 0x00064688
		public IFormationDeploymentPlan GetFormationPlan(BattleSideEnum side, FormationClass fClass, DeploymentPlanType planType)
		{
			return this._battleSideDeploymentPlans[(int)side].GetFormationPlan(fClass, planType);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00066499 File Offset: 0x00064699
		public bool IsPlanMadeForBattleSide(BattleSideEnum side, DeploymentPlanType planType)
		{
			return this._battleSideDeploymentPlans[(int)side].IsPlanMade(planType);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000664A9 File Offset: 0x000646A9
		public bool IsPlanMadeForBattleSide(BattleSideEnum side, out bool isFirstPlan, DeploymentPlanType planType)
		{
			isFirstPlan = false;
			if (this._battleSideDeploymentPlans[(int)side].IsPlanMade(planType))
			{
				isFirstPlan = this._battleSideDeploymentPlans[(int)side].IsFirstPlan(planType);
				return true;
			}
			return false;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000664D1 File Offset: 0x000646D1
		public bool IsInitialPlanSuitableForFormations(BattleSideEnum side, ValueTuple<int, int>[] troopDataPerFormationClass)
		{
			return this._battleSideDeploymentPlans[(int)side].IsInitialPlanSuitableForFormations(troopDataPerFormationClass);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000664E1 File Offset: 0x000646E1
		public bool HasDeploymentBoundaries(BattleSideEnum side)
		{
			return this._battleSideDeploymentPlans[(int)side].HasDeploymentBoundaries();
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000664F0 File Offset: 0x000646F0
		public MatrixFrame GetBattleSideDeploymentFrame(BattleSideEnum side)
		{
			if (this.IsPlanMadeForBattleSide(side, DeploymentPlanType.Initial))
			{
				return this._battleSideDeploymentPlans[(int)side].GetDeploymentFrame();
			}
			Debug.FailedAssert("Cannot retrieve formation deployment frame as deployment plan is not made for this battle side.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "GetBattleSideDeploymentFrame", 237);
			return MatrixFrame.Identity;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00066528 File Offset: 0x00064728
		public Vec3 GetMeanPositionOfPlan(BattleSideEnum side, DeploymentPlanType planType)
		{
			if (this.IsPlanMadeForBattleSide(side, planType))
			{
				return this._battleSideDeploymentPlans[(int)side].GetMeanPositionOfPlan(planType);
			}
			Debug.FailedAssert("Cannot retrieve formation deployment frame as deployment plan is not made for this battle side.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "GetMeanPositionOfPlan", 250);
			return Vec3.Invalid;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00066561 File Offset: 0x00064761
		[return: TupleElementNames(new string[]
		{
			"id",
			"points"
		})]
		public MBReadOnlyList<ValueTuple<string, List<Vec2>>> GetDeploymentBoundaries(BattleSideEnum side)
		{
			if (this.HasDeploymentBoundaries(side))
			{
				return this._battleSideDeploymentPlans[(int)side].DeploymentBoundaries;
			}
			Debug.FailedAssert("Cannot retrieve battle side deployment boundaries as they are not available for this battle side.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\MissionDeploymentPlan.cs", "GetDeploymentBoundaries", 263);
			return null;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00066594 File Offset: 0x00064794
		public void UpdateReinforcementPlan(BattleSideEnum side)
		{
			this._battleSideDeploymentPlans[(int)side].UpdateReinforcementPlans();
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000665A4 File Offset: 0x000647A4
		private void ReadSpawnEntitiesFromScene(bool isFieldBattle)
		{
			for (int i = 0; i < 2; i++)
			{
				this._playerSpawnFrames[i] = null;
			}
			this._formationSceneSpawnEntries = new FormationSceneSpawnEntry[2, 11];
			Scene scene = this._mission.Scene;
			if (isFieldBattle)
			{
				for (int j = 0; j < 2; j++)
				{
					string str = (j == 1) ? "attacker_" : "defender_";
					for (int k = 0; k < 11; k++)
					{
						FormationClass formationClass = (FormationClass)k;
						GameEntity gameEntity = scene.FindEntityWithTag(str + formationClass.GetName().ToLower());
						if (gameEntity == null)
						{
							FormationClass formationClass2 = formationClass.FallbackClass();
							int num = (int)formationClass2;
							GameEntity spawnEntity = this._formationSceneSpawnEntries[j, num].SpawnEntity;
							gameEntity = ((spawnEntity != null) ? spawnEntity : scene.FindEntityWithTag(str + formationClass2.GetName().ToLower()));
							formationClass = ((gameEntity != null) ? formationClass2 : FormationClass.NumberOfAllFormations);
						}
						this._formationSceneSpawnEntries[j, k] = new FormationSceneSpawnEntry(formationClass, gameEntity, gameEntity);
					}
				}
				return;
			}
			GameEntity gameEntity2 = null;
			if (this._mission.IsSallyOutBattle)
			{
				gameEntity2 = scene.FindEntityWithTag("sally_out_ambush_battle_set");
			}
			if (gameEntity2 != null)
			{
				this.ReadSallyOutEntitiesFromScene(scene, gameEntity2);
			}
			else
			{
				this.ReadSiegeBattleEntitiesFromScene(scene, BattleSideEnum.Defender);
			}
			this.ReadSiegeBattleEntitiesFromScene(scene, BattleSideEnum.Attacker);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0006670C File Offset: 0x0006490C
		private void ReadSallyOutEntitiesFromScene(Scene missionScene, GameEntity sallyOutSetEntity)
		{
			int num = 0;
			MatrixFrame globalFrame = sallyOutSetEntity.GetFirstChildEntityWithTag("sally_out_ambush_player").GetGlobalFrame();
			WorldPosition origin = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalFrame.origin, false);
			this._playerSpawnFrames[num] = new WorldFrame?(new WorldFrame(globalFrame.rotation, origin));
			GameEntity firstChildEntityWithTag = sallyOutSetEntity.GetFirstChildEntityWithTag("sally_out_ambush_infantry");
			GameEntity firstChildEntityWithTag2 = sallyOutSetEntity.GetFirstChildEntityWithTag("sally_out_ambush_archer");
			GameEntity firstChildEntityWithTag3 = sallyOutSetEntity.GetFirstChildEntityWithTag("sally_out_ambush_cavalry");
			for (int i = 0; i < 11; i++)
			{
				FormationClass formationClass = (FormationClass)i;
				FormationClass formationClass2 = formationClass.FallbackClass();
				GameEntity gameEntity = null;
				switch (formationClass2)
				{
				case FormationClass.Infantry:
					gameEntity = firstChildEntityWithTag;
					break;
				case FormationClass.Ranged:
					gameEntity = firstChildEntityWithTag2;
					break;
				case FormationClass.Cavalry:
				case FormationClass.HorseArcher:
					gameEntity = firstChildEntityWithTag3;
					break;
				}
				this._formationSceneSpawnEntries[num, i] = new FormationSceneSpawnEntry(formationClass, gameEntity, gameEntity);
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000667F0 File Offset: 0x000649F0
		private void ReadSiegeBattleEntitiesFromScene(Scene missionScene, BattleSideEnum battleSide)
		{
			int num = (int)battleSide;
			string str = battleSide.ToString().ToLower() + "_";
			for (int i = 0; i < 11; i++)
			{
				FormationClass formationClass = (FormationClass)i;
				string text = str + formationClass.GetName().ToLower();
				string tag = text + "_reinforcement";
				GameEntity gameEntity = missionScene.FindEntityWithTag(text);
				GameEntity gameEntity2;
				if (gameEntity == null)
				{
					FormationClass formationClass2 = formationClass.FallbackClass();
					int num2 = (int)formationClass2;
					FormationSceneSpawnEntry formationSceneSpawnEntry = this._formationSceneSpawnEntries[num, num2];
					if (formationSceneSpawnEntry.SpawnEntity != null)
					{
						gameEntity = formationSceneSpawnEntry.SpawnEntity;
						gameEntity2 = formationSceneSpawnEntry.ReinforcementSpawnEntity;
					}
					else
					{
						text = str + formationClass2.GetName().ToLower();
						tag = text + "_reinforcement";
						gameEntity = missionScene.FindEntityWithTag(text);
						gameEntity2 = missionScene.FindEntityWithTag(tag);
					}
					formationClass = ((gameEntity != null) ? formationClass2 : FormationClass.NumberOfAllFormations);
				}
				else
				{
					gameEntity2 = missionScene.FindEntityWithTag(tag);
				}
				if (gameEntity2 == null)
				{
					gameEntity2 = gameEntity;
				}
				this._formationSceneSpawnEntries[num, i] = new FormationSceneSpawnEntry(formationClass, gameEntity, gameEntity2);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00066923 File Offset: 0x00064B23
		bool IMissionDeploymentPlan.IsPositionInsideDeploymentBoundaries(BattleSideEnum battleSide, in Vec2 position)
		{
			return this.IsPositionInsideDeploymentBoundaries(battleSide, position);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0006692D File Offset: 0x00064B2D
		Vec2 IMissionDeploymentPlan.GetClosestDeploymentBoundaryPosition(BattleSideEnum battleSide, in Vec2 position, bool withNavMesh, float positionZ)
		{
			return this.GetClosestDeploymentBoundaryPosition(battleSide, position, withNavMesh, positionZ);
		}

		// Token: 0x0400096B RID: 2411
		public const int NumFormationsWithUnset = 11;

		// Token: 0x0400096C RID: 2412
		private readonly Mission _mission;

		// Token: 0x0400096D RID: 2413
		private readonly BattleSideDeploymentPlan[] _battleSideDeploymentPlans = new BattleSideDeploymentPlan[2];

		// Token: 0x0400096E RID: 2414
		private readonly WorldFrame?[] _playerSpawnFrames = new WorldFrame?[2];

		// Token: 0x0400096F RID: 2415
		private FormationSceneSpawnEntry[,] _formationSceneSpawnEntries;
	}
}
