using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000209 RID: 521
	public class DeploymentPlan
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00064EB4 File Offset: 0x000630B4
		public bool SpawnWithHorses
		{
			get
			{
				return this._spawnWithHorses;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00064EBC File Offset: 0x000630BC
		public int PlanCount
		{
			get
			{
				return this._planCount;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00064EC4 File Offset: 0x000630C4
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x00064ECC File Offset: 0x000630CC
		public bool IsPlanMade { get; private set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00064ED5 File Offset: 0x000630D5
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x00064EDD File Offset: 0x000630DD
		public float SpawnPathOffset { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00064EE6 File Offset: 0x000630E6
		public bool IsSafeToDeploy
		{
			get
			{
				return this.SafetyScore >= 50f;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00064EF8 File Offset: 0x000630F8
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x00064F00 File Offset: 0x00063100
		public float SafetyScore { get; private set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00064F0C File Offset: 0x0006310C
		public int FootTroopCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < 11; i++)
				{
					num += this._formationFootTroopCounts[i];
				}
				return num;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00064F34 File Offset: 0x00063134
		public int MountedTroopCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < 11; i++)
				{
					num += this._formationMountedTroopCounts[i];
				}
				return num;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00064F5C File Offset: 0x0006315C
		public int TroopCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < 11; i++)
				{
					num += this._formationFootTroopCounts[i] + this._formationMountedTroopCounts[i];
				}
				return num;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00064F8D File Offset: 0x0006318D
		public Vec3 MeanPosition
		{
			get
			{
				return this._meanPosition;
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00064F95 File Offset: 0x00063195
		public static DeploymentPlan CreateInitialPlan(Mission mission, BattleSideEnum side)
		{
			return new DeploymentPlan(mission, side, DeploymentPlanType.Initial, SpawnPathData.Invalid);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00064FA4 File Offset: 0x000631A4
		public static DeploymentPlan CreateReinforcementPlan(Mission mission, BattleSideEnum side)
		{
			return new DeploymentPlan(mission, side, DeploymentPlanType.Reinforcement, SpawnPathData.Invalid);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00064FB3 File Offset: 0x000631B3
		public static DeploymentPlan CreateReinforcementPlanWithSpawnPath(Mission mission, BattleSideEnum side, SpawnPathData spawnPathData)
		{
			return new DeploymentPlan(mission, side, DeploymentPlanType.Reinforcement, spawnPathData);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00064FC0 File Offset: 0x000631C0
		private DeploymentPlan(Mission mission, BattleSideEnum side, DeploymentPlanType type, SpawnPathData spawnPathData)
		{
			this._mission = mission;
			this._planCount = 0;
			this.Side = side;
			this.Type = type;
			this.SpawnPathData = spawnPathData;
			this._formationPlans = new FormationDeploymentPlan[11];
			this._formationFootTroopCounts = new int[11];
			this._formationMountedTroopCounts = new int[11];
			this._meanPosition = Vec3.Zero;
			this.IsPlanMade = false;
			this.SpawnPathOffset = 0f;
			this.SafetyScore = 100f;
			for (int i = 0; i < this._formationPlans.Length; i++)
			{
				FormationClass fClass = (FormationClass)i;
				this._formationPlans[i] = new FormationDeploymentPlan(fClass);
			}
			for (int j = 0; j < 4; j++)
			{
				this._deploymentFlanks[j] = new SortedList<FormationDeploymentOrder, FormationDeploymentPlan>(FormationDeploymentOrder.GetComparer());
			}
			this.ClearAddedTroops();
			this.ClearPlan();
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006509F File Offset: 0x0006329F
		public void SetSpawnWithHorses(bool value)
		{
			this._spawnWithHorses = value;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000650A8 File Offset: 0x000632A8
		public void ClearAddedTroops()
		{
			for (int i = 0; i < 11; i++)
			{
				this._formationFootTroopCounts[i] = 0;
				this._formationMountedTroopCounts[i] = 0;
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000650D4 File Offset: 0x000632D4
		public void ClearPlan()
		{
			FormationDeploymentPlan[] formationPlans = this._formationPlans;
			for (int i = 0; i < formationPlans.Length; i++)
			{
				formationPlans[i].Clear();
			}
			SortedList<FormationDeploymentOrder, FormationDeploymentPlan>[] deploymentFlanks = this._deploymentFlanks;
			for (int i = 0; i < deploymentFlanks.Length; i++)
			{
				deploymentFlanks[i].Clear();
			}
			this.IsPlanMade = false;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00065124 File Offset: 0x00063324
		public void AddTroops(FormationClass formationClass, int footTroopCount, int mountedTroopCount)
		{
			if (footTroopCount + mountedTroopCount > 0 && formationClass < (FormationClass)11)
			{
				this._formationFootTroopCounts[(int)formationClass] += footTroopCount;
				this._formationMountedTroopCounts[(int)formationClass] += mountedTroopCount;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00065160 File Offset: 0x00063360
		public void PlanBattleDeployment(FormationSceneSpawnEntry[,] formationSceneSpawnEntries, float spawnPathOffset = 0f)
		{
			this.SpawnPathOffset = spawnPathOffset;
			this.PlanFormationDimensions();
			if (this._mission.HasSpawnPath)
			{
				this.PlanFieldBattleDeploymentFromSpawnPath(spawnPathOffset);
			}
			else if (this._mission.IsFieldBattle)
			{
				this.PlanFieldBattleDeploymentFromSceneData(formationSceneSpawnEntries);
			}
			else
			{
				this.PlanBattleDeploymentFromSceneData(formationSceneSpawnEntries);
			}
			this.ComputeMeanPosition();
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000651B3 File Offset: 0x000633B3
		public FormationDeploymentPlan GetFormationPlan(FormationClass fClass)
		{
			return this._formationPlans[(int)fClass];
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000651C0 File Offset: 0x000633C0
		public bool GetFormationDeploymentFrame(FormationClass fClass, out MatrixFrame frame)
		{
			FormationDeploymentPlan formationPlan = this.GetFormationPlan(fClass);
			if (formationPlan.HasFrame())
			{
				frame = formationPlan.GetGroundFrame();
				return true;
			}
			frame = MatrixFrame.Identity;
			return false;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000651F8 File Offset: 0x000633F8
		public bool IsPlanSuitableForFormations(ValueTuple<int, int>[] troopDataPerFormationClass)
		{
			if (troopDataPerFormationClass.Length == 11)
			{
				for (int i = 0; i < 11; i++)
				{
					FormationClass fClass = (FormationClass)i;
					FormationDeploymentPlan formationPlan = this.GetFormationPlan(fClass);
					ValueTuple<int, int> valueTuple = troopDataPerFormationClass[i];
					if (formationPlan.PlannedFootTroopCount != valueTuple.Item1 || formationPlan.PlannedMountedTroopCount != valueTuple.Item2)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0006524C File Offset: 0x0006344C
		public void UpdateSafetyScore()
		{
			if (this._mission.Teams == null)
			{
				return;
			}
			float num = 100f;
			Team team = (this.Side == BattleSideEnum.Attacker) ? this._mission.Teams.Defender : ((this.Side == BattleSideEnum.Defender) ? this._mission.Teams.Attacker : null);
			if (team != null)
			{
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						float num2 = this._meanPosition.AsVec2.Distance(formation.QuerySystem.AveragePosition);
						if (num >= num2)
						{
							num = num2;
						}
					}
				}
			}
			team = ((this.Side == BattleSideEnum.Attacker) ? this._mission.Teams.DefenderAlly : ((this.Side == BattleSideEnum.Defender) ? this._mission.Teams.AttackerAlly : null));
			if (team != null)
			{
				foreach (Formation formation2 in team.FormationsIncludingEmpty)
				{
					if (formation2.CountOfUnits > 0)
					{
						float num3 = this._meanPosition.AsVec2.Distance(formation2.QuerySystem.AveragePosition);
						if (num >= num3)
						{
							num = num3;
						}
					}
				}
			}
			this.SafetyScore = num;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000653CC File Offset: 0x000635CC
		public WorldFrame GetFrameFromFormationSpawnEntity(GameEntity formationSpawnEntity, float depthOffset = 0f)
		{
			MatrixFrame globalFrame = formationSpawnEntity.GetGlobalFrame();
			globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			WorldPosition worldPosition = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalFrame.origin, false);
			WorldPosition origin = worldPosition;
			if (depthOffset != 0f)
			{
				origin.SetVec2(origin.AsVec2 - depthOffset * globalFrame.rotation.f.AsVec2);
				if (!origin.IsValid || origin.GetNavMesh() == UIntPtr.Zero)
				{
					origin = worldPosition;
				}
			}
			return new WorldFrame(globalFrame.rotation, origin);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00065468 File Offset: 0x00063668
		public ValueTuple<float, float> GetFormationSpawnWidthAndDepth(FormationClass formationNo, int troopCount, bool hasMountedTroops, bool considerCavalryAsInfantry = false)
		{
			bool flag = !considerCavalryAsInfantry && hasMountedTroops;
			float defaultUnitDiameter = Formation.GetDefaultUnitDiameter(flag);
			int unitSpacingOf = ArrangementOrder.GetUnitSpacingOf(ArrangementOrder.ArrangementOrderEnum.Line);
			float num = flag ? Formation.CavalryInterval(unitSpacingOf) : Formation.InfantryInterval(unitSpacingOf);
			float num2 = flag ? Formation.CavalryDistance(unitSpacingOf) : Formation.InfantryDistance(unitSpacingOf);
			float num3 = (float)MathF.Max(0, troopCount - 1) * (num + defaultUnitDiameter) + defaultUnitDiameter;
			float num4 = flag ? 18f : 9f;
			int num5 = (int)(num3 / MathF.Sqrt(num4 * (float)troopCount + 1f));
			num5 = MathF.Max(1, num5);
			float num6 = (float)troopCount / (float)num5;
			float item = MathF.Max(0f, num6 - 1f) * (num + defaultUnitDiameter) + defaultUnitDiameter;
			float item2 = (float)(num5 - 1) * (num2 + defaultUnitDiameter) + defaultUnitDiameter;
			return new ValueTuple<float, float>(item, item2);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00065524 File Offset: 0x00063724
		private void PlanFieldBattleDeploymentFromSpawnPath(float pathOffset = 0f)
		{
			for (int i = 0; i < this._formationPlans.Length; i++)
			{
				int num = this._formationFootTroopCounts[i] + this._formationMountedTroopCounts[i];
				FormationDeploymentPlan formationDeploymentPlan = this._formationPlans[i];
				FormationDeploymentFlank defaultFlank = formationDeploymentPlan.GetDefaultFlank(this._spawnWithHorses, num, this.FootTroopCount);
				FormationClass formationClass = (FormationClass)i;
				int offset = (num > 0 || formationClass == FormationClass.NumberOfRegularFormations) ? 0 : 1;
				FormationDeploymentOrder flankDeploymentOrder = formationDeploymentPlan.GetFlankDeploymentOrder(offset);
				this._deploymentFlanks[(int)defaultFlank].Add(flankDeploymentOrder, formationDeploymentPlan);
			}
			float horizontalCenterOffset = this.ComputeHorizontalCenterOffset();
			SpawnPathData spawnPathData;
			if (this.Type == DeploymentPlanType.Initial)
			{
				spawnPathData = this._mission.GetInitialSpawnPathDataOfSide(this.Side);
			}
			else
			{
				spawnPathData = this.SpawnPathData;
			}
			Vec2 deployPosition;
			Vec2 deployDirection;
			spawnPathData.GetOrientedSpawnPathPosition(out deployPosition, out deployDirection, pathOffset);
			this.DeployFlanks(deployPosition, deployDirection, horizontalCenterOffset);
			SortedList<FormationDeploymentOrder, FormationDeploymentPlan>[] deploymentFlanks = this._deploymentFlanks;
			for (int j = 0; j < deploymentFlanks.Length; j++)
			{
				deploymentFlanks[j].Clear();
			}
			this.IsPlanMade = true;
			this._planCount++;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006562C File Offset: 0x0006382C
		private void PlanFieldBattleDeploymentFromSceneData(FormationSceneSpawnEntry[,] formationSceneSpawnEntries)
		{
			if (formationSceneSpawnEntries == null || formationSceneSpawnEntries.GetLength(0) != 2 || formationSceneSpawnEntries.GetLength(1) != this._formationPlans.Length)
			{
				return;
			}
			int side = (int)this.Side;
			int num = (this.Side == BattleSideEnum.Attacker) ? 0 : 1;
			Dictionary<GameEntity, float> dictionary = new Dictionary<GameEntity, float>();
			bool flag = this.Type == DeploymentPlanType.Initial;
			for (int i = 0; i < this._formationPlans.Length; i++)
			{
				FormationDeploymentPlan formationDeploymentPlan = this._formationPlans[i];
				FormationSceneSpawnEntry formationSceneSpawnEntry = formationSceneSpawnEntries[side, i];
				FormationSceneSpawnEntry formationSceneSpawnEntry2 = formationSceneSpawnEntries[num, i];
				GameEntity gameEntity = flag ? formationSceneSpawnEntry.SpawnEntity : formationSceneSpawnEntry.ReinforcementSpawnEntity;
				GameEntity gameEntity2 = flag ? formationSceneSpawnEntry2.SpawnEntity : formationSceneSpawnEntry2.ReinforcementSpawnEntity;
				if (gameEntity != null && gameEntity2 != null)
				{
					WorldFrame frame = this.ComputeFieldBattleDeploymentFrameForFormation(formationDeploymentPlan, gameEntity, gameEntity2, ref dictionary);
					formationDeploymentPlan.SetFrame(frame);
				}
				else
				{
					formationDeploymentPlan.SetFrame(WorldFrame.Invalid);
				}
				formationDeploymentPlan.SetSpawnClass(formationSceneSpawnEntry.FormationClass);
			}
			this.IsPlanMade = true;
			this._planCount++;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00065748 File Offset: 0x00063948
		private void PlanBattleDeploymentFromSceneData(FormationSceneSpawnEntry[,] formationSceneSpawnEntries)
		{
			if (formationSceneSpawnEntries == null || formationSceneSpawnEntries.GetLength(0) != 2 || formationSceneSpawnEntries.GetLength(1) != this._formationPlans.Length)
			{
				return;
			}
			int side = (int)this.Side;
			Dictionary<GameEntity, float> dictionary = new Dictionary<GameEntity, float>();
			bool flag = this.Type == DeploymentPlanType.Initial;
			for (int i = 0; i < this._formationPlans.Length; i++)
			{
				FormationDeploymentPlan formationDeploymentPlan = this._formationPlans[i];
				FormationSceneSpawnEntry formationSceneSpawnEntry = formationSceneSpawnEntries[side, i];
				GameEntity gameEntity = flag ? formationSceneSpawnEntry.SpawnEntity : formationSceneSpawnEntry.ReinforcementSpawnEntity;
				if (gameEntity != null)
				{
					float andUpdateSpawnDepth = this.GetAndUpdateSpawnDepth(ref dictionary, gameEntity, formationDeploymentPlan);
					formationDeploymentPlan.SetFrame(this.GetFrameFromFormationSpawnEntity(gameEntity, andUpdateSpawnDepth));
				}
				else
				{
					formationDeploymentPlan.SetFrame(WorldFrame.Invalid);
				}
				formationDeploymentPlan.SetSpawnClass(formationSceneSpawnEntry.FormationClass);
			}
			this.IsPlanMade = true;
			this._planCount++;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00065824 File Offset: 0x00063A24
		private void PlanFormationDimensions()
		{
			for (int i = 0; i < this._formationPlans.Length; i++)
			{
				int num = this._formationFootTroopCounts[i];
				int num2 = this._formationMountedTroopCounts[i];
				int num3 = num + num2;
				if (num3 > 0)
				{
					FormationDeploymentPlan formationDeploymentPlan = this._formationPlans[i];
					bool hasMountedTroops = MissionDeploymentPlan.HasSignificantMountedTroops(num, num2);
					ValueTuple<float, float> formationSpawnWidthAndDepth = this.GetFormationSpawnWidthAndDepth(formationDeploymentPlan.Class, num3, hasMountedTroops, !this._spawnWithHorses);
					float item = formationSpawnWidthAndDepth.Item1;
					float item2 = formationSpawnWidthAndDepth.Item2;
					formationDeploymentPlan.SetPlannedDimensions(item, item2);
					formationDeploymentPlan.SetPlannedTroopCount(num, num2);
				}
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000658B0 File Offset: 0x00063AB0
		private void DeployFlanks(Vec2 deployPosition, Vec2 deployDirection, float horizontalCenterOffset)
		{
			ValueTuple<float, float> valueTuple = this.PlanFlankDeployment(FormationDeploymentFlank.Front, deployPosition, deployDirection, 0f, horizontalCenterOffset);
			float item = valueTuple.Item1;
			float num = valueTuple.Item2;
			num += 3f;
			float item2 = this.PlanFlankDeployment(FormationDeploymentFlank.Rear, deployPosition, deployDirection, num, horizontalCenterOffset).Item1;
			float num2 = MathF.Max(item, item2);
			float num3 = this.ComputeFlankDepth(FormationDeploymentFlank.Front, true);
			num3 += 3f;
			float num4 = this.ComputeFlankWidth(FormationDeploymentFlank.Left);
			float horizontalOffset = horizontalCenterOffset + 2f + 0.5f * (num2 + num4);
			this.PlanFlankDeployment(FormationDeploymentFlank.Left, deployPosition, deployDirection, num3, horizontalOffset);
			float num5 = this.ComputeFlankWidth(FormationDeploymentFlank.Right);
			float horizontalOffset2 = horizontalCenterOffset - (2f + 0.5f * (num2 + num5));
			this.PlanFlankDeployment(FormationDeploymentFlank.Right, deployPosition, deployDirection, num3, horizontalOffset2);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006596C File Offset: 0x00063B6C
		[return: TupleElementNames(new string[]
		{
			"flankWidth",
			"flankDepth"
		})]
		private ValueTuple<float, float> PlanFlankDeployment(FormationDeploymentFlank flankFlank, Vec2 deployPosition, Vec2 deployDirection, float verticalOffset = 0f, float horizontalOffset = 0f)
		{
			Mat3 identity = Mat3.Identity;
			identity.RotateAboutUp(deployDirection.RotationInRadians);
			float num = 0f;
			float num2 = 0f;
			Vec2 v = deployDirection.LeftVec();
			WorldPosition worldPosition = new WorldPosition(this._mission.Scene, UIntPtr.Zero, deployPosition.ToVec3(0f), false);
			foreach (KeyValuePair<FormationDeploymentOrder, FormationDeploymentPlan> keyValuePair in this._deploymentFlanks[(int)flankFlank])
			{
				FormationDeploymentPlan value = keyValuePair.Value;
				Vec2 destination = worldPosition.AsVec2 - (num2 + verticalOffset) * deployDirection + horizontalOffset * v;
				Vec3 lastPointOnNavigationMeshFromWorldPositionToDestination = this._mission.Scene.GetLastPointOnNavigationMeshFromWorldPositionToDestination(ref worldPosition, destination);
				WorldPosition origin = new WorldPosition(this._mission.Scene, UIntPtr.Zero, lastPointOnNavigationMeshFromWorldPositionToDestination, false);
				WorldFrame frame = new WorldFrame(identity, origin);
				value.SetFrame(frame);
				float num3 = value.PlannedDepth + 3f;
				num2 += num3;
				num = MathF.Max(num, value.PlannedWidth);
			}
			num2 = MathF.Max(num2 - 3f, 0f);
			return new ValueTuple<float, float>(num, num2);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00065AB8 File Offset: 0x00063CB8
		private WorldFrame ComputeFieldBattleDeploymentFrameForFormation(FormationDeploymentPlan formationPlan, GameEntity formationSceneEntity, GameEntity counterSideFormationSceneEntity, ref Dictionary<GameEntity, float> spawnDepths)
		{
			Vec3 globalPosition = formationSceneEntity.GlobalPosition;
			Vec2 asVec = (counterSideFormationSceneEntity.GlobalPosition - globalPosition).AsVec2;
			asVec.Normalize();
			float andUpdateSpawnDepth = this.GetAndUpdateSpawnDepth(ref spawnDepths, formationSceneEntity, formationPlan);
			WorldPosition origin = new WorldPosition(this._mission.Scene, UIntPtr.Zero, globalPosition, false);
			origin.SetVec2(origin.AsVec2 - andUpdateSpawnDepth * asVec);
			Mat3 identity = Mat3.Identity;
			identity.RotateAboutUp(asVec.RotationInRadians);
			return new WorldFrame(identity, origin);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00065B48 File Offset: 0x00063D48
		private float ComputeFlankWidth(FormationDeploymentFlank flank)
		{
			float num = 0f;
			foreach (KeyValuePair<FormationDeploymentOrder, FormationDeploymentPlan> keyValuePair in this._deploymentFlanks[(int)flank])
			{
				num = MathF.Max(num, keyValuePair.Value.PlannedWidth);
			}
			return num;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00065BAC File Offset: 0x00063DAC
		private float ComputeFlankDepth(FormationDeploymentFlank flank, bool countPositiveNumTroops = false)
		{
			float num = 0f;
			foreach (KeyValuePair<FormationDeploymentOrder, FormationDeploymentPlan> keyValuePair in this._deploymentFlanks[(int)flank])
			{
				if (!countPositiveNumTroops)
				{
					num += keyValuePair.Value.PlannedDepth + 3f;
				}
				else if (keyValuePair.Value.PlannedTroopCount > 0)
				{
					num += keyValuePair.Value.PlannedDepth + 3f;
				}
			}
			num -= 3f;
			return num;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00065C44 File Offset: 0x00063E44
		private void ComputeMeanPosition()
		{
			this._meanPosition = Vec3.Zero;
			Vec2 vec = Vec2.Zero;
			int num = 0;
			foreach (FormationDeploymentPlan formationDeploymentPlan in this._formationPlans)
			{
				if (formationDeploymentPlan.HasFrame())
				{
					vec += formationDeploymentPlan.GetGroundPosition().AsVec2;
					num++;
				}
			}
			if (num > 0)
			{
				vec = new Vec2(vec.X / (float)num, vec.Y / (float)num);
				float z = 0f;
				Mission.Current.Scene.GetHeightAtPoint(vec, BodyFlags.None, ref z);
				this._meanPosition = new Vec3(vec, z, -1f);
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00065CF0 File Offset: 0x00063EF0
		private float ComputeHorizontalCenterOffset()
		{
			float num = MathF.Max(this.ComputeFlankWidth(FormationDeploymentFlank.Front), this.ComputeFlankWidth(FormationDeploymentFlank.Rear));
			float num2 = this.ComputeFlankWidth(FormationDeploymentFlank.Left);
			float num3 = this.ComputeFlankWidth(FormationDeploymentFlank.Right);
			float num4 = num / 2f + num2 + 2f;
			return (num / 2f + num3 + 2f - num4) / 2f;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00065D48 File Offset: 0x00063F48
		private float GetAndUpdateSpawnDepth(ref Dictionary<GameEntity, float> spawnDepths, GameEntity spawnEntity, FormationDeploymentPlan formationPlan)
		{
			float num;
			bool flag = spawnDepths.TryGetValue(spawnEntity, out num);
			float num2 = formationPlan.HasDimensions ? (formationPlan.PlannedDepth + 3f) : 0f;
			if (!flag)
			{
				num = 0f;
				spawnDepths[spawnEntity] = num2;
			}
			else if (formationPlan.HasDimensions)
			{
				spawnDepths[spawnEntity] = num + num2;
			}
			return num;
		}

		// Token: 0x04000944 RID: 2372
		public const float VerticalFormationGap = 3f;

		// Token: 0x04000945 RID: 2373
		public const float HorizontalFormationGap = 2f;

		// Token: 0x04000946 RID: 2374
		public const float MaxSafetyScore = 100f;

		// Token: 0x04000947 RID: 2375
		public readonly BattleSideEnum Side;

		// Token: 0x04000948 RID: 2376
		public readonly DeploymentPlanType Type;

		// Token: 0x04000949 RID: 2377
		public readonly SpawnPathData SpawnPathData;

		// Token: 0x0400094D RID: 2381
		private readonly Mission _mission;

		// Token: 0x0400094E RID: 2382
		private int _planCount;

		// Token: 0x0400094F RID: 2383
		private bool _spawnWithHorses;

		// Token: 0x04000950 RID: 2384
		private readonly int[] _formationMountedTroopCounts;

		// Token: 0x04000951 RID: 2385
		private readonly int[] _formationFootTroopCounts;

		// Token: 0x04000952 RID: 2386
		private readonly FormationDeploymentPlan[] _formationPlans;

		// Token: 0x04000953 RID: 2387
		private Vec3 _meanPosition;

		// Token: 0x04000954 RID: 2388
		private readonly SortedList<FormationDeploymentOrder, FormationDeploymentPlan>[] _deploymentFlanks = new SortedList<FormationDeploymentOrder, FormationDeploymentPlan>[4];
	}
}
