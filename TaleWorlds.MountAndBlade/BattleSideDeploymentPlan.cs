using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000206 RID: 518
	public class BattleSideDeploymentPlan
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00063CD3 File Offset: 0x00061ED3
		public bool SpawnWithHorses
		{
			get
			{
				return this._spawnWithHorses;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00063CDB File Offset: 0x00061EDB
		[TupleElementNames(new string[]
		{
			"id",
			"points"
		})]
		public MBReadOnlyList<ValueTuple<string, List<Vec2>>> DeploymentBoundaries
		{
			[return: TupleElementNames(new string[]
			{
				"id",
				"points"
			})]
			get
			{
				return this._deploymentBoundaries;
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00063CE4 File Offset: 0x00061EE4
		public BattleSideDeploymentPlan(Mission mission, BattleSideEnum side)
		{
			this._mission = mission;
			this.Side = side;
			this._spawnWithHorses = false;
			this._initialPlan = DeploymentPlan.CreateInitialPlan(this._mission, side);
			this._reinforcementPlans = new List<DeploymentPlan>();
			this._reinforcementPlansCreated = false;
			this._currentReinforcementPlan = this._initialPlan;
			this._deploymentBoundaries.Clear();
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00063D54 File Offset: 0x00061F54
		public void CreateReinforcementPlans()
		{
			if (!this._reinforcementPlansCreated)
			{
				if (this._mission.HasSpawnPath)
				{
					foreach (SpawnPathData spawnPathData in this._mission.GetReinforcementPathsDataOfSide(this.Side))
					{
						DeploymentPlan item = DeploymentPlan.CreateReinforcementPlanWithSpawnPath(this._mission, this.Side, spawnPathData);
						this._reinforcementPlans.Add(item);
					}
					this._currentReinforcementPlan = this._reinforcementPlans[0];
				}
				else
				{
					DeploymentPlan deploymentPlan = DeploymentPlan.CreateReinforcementPlan(this._mission, this.Side);
					this._reinforcementPlans.Add(deploymentPlan);
					this._currentReinforcementPlan = deploymentPlan;
				}
				this._reinforcementPlansCreated = true;
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00063E24 File Offset: 0x00062024
		public void SetSpawnWithHorses(bool value)
		{
			this._spawnWithHorses = value;
			this._initialPlan.SetSpawnWithHorses(value);
			foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
			{
				deploymentPlan.SetSpawnWithHorses(value);
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00063E88 File Offset: 0x00062088
		public void PlanBattleDeployment(FormationSceneSpawnEntry[,] formationSceneSpawnEntries, DeploymentPlanType planType, float spawnPathOffset)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				if (!this._initialPlan.IsPlanMade)
				{
					this._initialPlan.PlanBattleDeployment(formationSceneSpawnEntries, spawnPathOffset);
				}
				this.PlanDeploymentZone();
				return;
			}
			if (planType == DeploymentPlanType.Reinforcement)
			{
				foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
				{
					if (!deploymentPlan.IsPlanMade)
					{
						deploymentPlan.PlanBattleDeployment(formationSceneSpawnEntries, 0f);
					}
				}
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00063F10 File Offset: 0x00062110
		public void UpdateReinforcementPlans()
		{
			if (!this._reinforcementPlansCreated || this._reinforcementPlans.Count <= 1)
			{
				return;
			}
			foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
			{
				deploymentPlan.UpdateSafetyScore();
			}
			if (!this._currentReinforcementPlan.IsSafeToDeploy)
			{
				this._currentReinforcementPlan = this._reinforcementPlans.MaxBy((DeploymentPlan plan) => plan.SafetyScore);
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00063FB4 File Offset: 0x000621B4
		public void ClearPlans(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				this._initialPlan.ClearPlan();
				return;
			}
			if (planType == DeploymentPlanType.Reinforcement)
			{
				foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
				{
					deploymentPlan.ClearPlan();
				}
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00064018 File Offset: 0x00062218
		public void ClearAddedTroops(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				this._initialPlan.ClearAddedTroops();
				return;
			}
			foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
			{
				deploymentPlan.ClearAddedTroops();
			}
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00064078 File Offset: 0x00062278
		public void AddTroops(FormationClass formationClass, int footTroopCount, int mountedTroopCount, DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				this._initialPlan.AddTroops(formationClass, footTroopCount, mountedTroopCount);
				return;
			}
			foreach (DeploymentPlan deploymentPlan in this._reinforcementPlans)
			{
				deploymentPlan.AddTroops(formationClass, footTroopCount, mountedTroopCount);
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000640E0 File Offset: 0x000622E0
		public bool IsFirstPlan(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.PlanCount == 1;
			}
			return this._reinforcementPlansCreated && this._currentReinforcementPlan.PlanCount == 1;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006410C File Offset: 0x0006230C
		public bool IsPlanMade(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.IsPlanMade;
			}
			return this._reinforcementPlansCreated && this._currentReinforcementPlan.IsPlanMade;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00064132 File Offset: 0x00062332
		public float GetSpawnPathOffset(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.SpawnPathOffset;
			}
			if (!this._reinforcementPlansCreated)
			{
				return 0f;
			}
			return this._currentReinforcementPlan.SpawnPathOffset;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006415C File Offset: 0x0006235C
		public int GetTroopCount(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.TroopCount;
			}
			if (!this._reinforcementPlansCreated)
			{
				return 0;
			}
			return this._currentReinforcementPlan.TroopCount;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00064182 File Offset: 0x00062382
		public MatrixFrame GetDeploymentFrame()
		{
			return this._deploymentFrame;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006418A File Offset: 0x0006238A
		public bool HasDeploymentBoundaries()
		{
			return !this._deploymentBoundaries.IsEmpty<ValueTuple<string, List<Vec2>>>();
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006419A File Offset: 0x0006239A
		public IFormationDeploymentPlan GetFormationPlan(FormationClass fClass, DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.GetFormationPlan(fClass);
			}
			return this._currentReinforcementPlan.GetFormationPlan(fClass);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000641B8 File Offset: 0x000623B8
		public Vec3 GetMeanPositionOfPlan(DeploymentPlanType planType)
		{
			if (planType == DeploymentPlanType.Initial)
			{
				return this._initialPlan.MeanPosition;
			}
			return this._currentReinforcementPlan.MeanPosition;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x000641D4 File Offset: 0x000623D4
		public bool IsInitialPlanSuitableForFormations(ValueTuple<int, int>[] troopDataPerFormationClass)
		{
			return this._initialPlan.IsPlanSuitableForFormations(troopDataPerFormationClass);
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000641E4 File Offset: 0x000623E4
		public bool IsPositionInsideDeploymentBoundaries(in Vec2 position)
		{
			bool result = false;
			foreach (ValueTuple<string, List<Vec2>> valueTuple in this._deploymentBoundaries)
			{
				List<Vec2> item = valueTuple.Item2;
				if (MBSceneUtilities.IsPointInsideBoundaries(position, item, 0.05f))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006424C File Offset: 0x0006244C
		public Vec2 GetClosestDeploymentBoundaryPosition(in Vec2 position, bool withNavMesh = false, float positionZ = 0f)
		{
			Vec2 result = position;
			float num = float.MaxValue;
			foreach (ValueTuple<string, List<Vec2>> valueTuple in this._deploymentBoundaries)
			{
				List<Vec2> item = valueTuple.Item2;
				if (item.Count > 2)
				{
					Vec2 vec;
					float num2;
					if (withNavMesh)
					{
						num2 = MBSceneUtilities.FindClosestPointWithNavMeshToBoundaries(position, positionZ, item, out vec);
					}
					else
					{
						num2 = MBSceneUtilities.FindClosestPointToBoundaries(position, item, out vec);
					}
					if (num2 < num)
					{
						num = num2;
						result = vec;
					}
				}
			}
			return result;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000642DC File Offset: 0x000624DC
		private void PlanDeploymentZone()
		{
			if (this._mission.HasSpawnPath || this._mission.IsFieldBattle)
			{
				this.ComputeDeploymentZone();
				return;
			}
			if (this._mission.IsSiegeBattle)
			{
				this.SetDeploymentZoneFromMissionBoundaries();
				return;
			}
			this._deploymentBoundaries.Clear();
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006432C File Offset: 0x0006252C
		private void ComputeDeploymentZone()
		{
			this._initialPlan.GetFormationDeploymentFrame(FormationClass.Infantry, out this._deploymentFrame);
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < 10; i++)
			{
				FormationClass fClass = (FormationClass)i;
				FormationDeploymentPlan formationPlan = this._initialPlan.GetFormationPlan(fClass);
				if (formationPlan.HasFrame())
				{
					MatrixFrame matrixFrame = this._deploymentFrame.TransformToLocal(formationPlan.GetGroundFrame());
					num = Math.Max(matrixFrame.origin.y, num);
					num2 = Math.Max(Math.Abs(matrixFrame.origin.x), num2);
				}
			}
			num += 10f;
			this._deploymentFrame.Advance(num);
			this._deploymentBoundaries.Clear();
			float num3 = 2f * num2 + 1.5f * (float)this._initialPlan.TroopCount;
			num3 = Math.Max(num3, 100f);
			foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this._mission.Boundaries)
			{
				string key = keyValuePair.Key;
				List<Vec2> item = BattleSideDeploymentPlan.ComputeDeploymentBoundariesFromMissionBoundaries(keyValuePair.Value, ref this._deploymentFrame, num3);
				this._deploymentBoundaries.Add(new ValueTuple<string, List<Vec2>>(key, item));
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00064478 File Offset: 0x00062678
		private void SetDeploymentZoneFromMissionBoundaries()
		{
			this._deploymentBoundaries.Clear();
			foreach (ValueTuple<string, List<Vec2>, bool> valueTuple in MBSceneUtilities.GetDeploymentBoundaries(this.Side))
			{
				List<Vec2> item = new List<Vec2>(valueTuple.Item2);
				MBSceneUtilities.RadialSortBoundary(ref item);
				MBSceneUtilities.FindConvexHull(ref item);
				this._deploymentBoundaries.Add(new ValueTuple<string, List<Vec2>>(valueTuple.Item1, item));
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00064508 File Offset: 0x00062708
		private static List<Vec2> ComputeDeploymentBoundariesFromMissionBoundaries(ICollection<Vec2> missionBoundaries, ref MatrixFrame deploymentFrame, float desiredWidth)
		{
			List<Vec2> list = new List<Vec2>();
			float maxLength = desiredWidth / 2f;
			if (missionBoundaries.Count > 2)
			{
				Vec2 asVec = deploymentFrame.origin.AsVec2;
				Vec2 vec = deploymentFrame.rotation.s.AsVec2.Normalized();
				Vec2 v = deploymentFrame.rotation.f.AsVec2.Normalized();
				List<Vec2> boundaries = missionBoundaries.ToList<Vec2>();
				List<ValueTuple<Vec2, Vec2>> list2 = new List<ValueTuple<Vec2, Vec2>>();
				Vec2 vec2 = BattleSideDeploymentPlan.ClampRayToMissionBoundaries(boundaries, asVec, vec, maxLength);
				BattleSideDeploymentPlan.AddDeploymentBoundaryPoint(list, vec2);
				Vec2 vec3 = BattleSideDeploymentPlan.ClampRayToMissionBoundaries(boundaries, asVec, -vec, maxLength);
				BattleSideDeploymentPlan.AddDeploymentBoundaryPoint(list, vec3);
				Vec2 vec4;
				if (MBMath.IntersectRayWithBoundaryList(vec2, -v, boundaries, out vec4) && (vec4 - vec2).Length > 0.1f)
				{
					list2.Add(new ValueTuple<Vec2, Vec2>(vec4, vec2));
					BattleSideDeploymentPlan.AddDeploymentBoundaryPoint(list, vec4);
				}
				list2.Add(new ValueTuple<Vec2, Vec2>(vec2, vec3));
				Vec2 vec5;
				if (MBMath.IntersectRayWithBoundaryList(vec3, -v, boundaries, out vec5) && (vec5 - vec3).Length > 0.1f)
				{
					list2.Add(new ValueTuple<Vec2, Vec2>(vec3, vec5));
					BattleSideDeploymentPlan.AddDeploymentBoundaryPoint(list, vec5);
				}
				foreach (Vec2 vec6 in missionBoundaries)
				{
					bool flag = true;
					foreach (ValueTuple<Vec2, Vec2> valueTuple in list2)
					{
						Vec2 vec7 = vec6 - valueTuple.Item1;
						Vec2 vec8 = valueTuple.Item2 - valueTuple.Item1;
						if (vec8.x * vec7.y - vec8.y * vec7.x <= 0f)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						BattleSideDeploymentPlan.AddDeploymentBoundaryPoint(list, vec6);
					}
				}
				MBSceneUtilities.RadialSortBoundary(ref list);
			}
			return list;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00064728 File Offset: 0x00062928
		private static void AddDeploymentBoundaryPoint(List<Vec2> deploymentBoundaries, Vec2 point)
		{
			if (!deploymentBoundaries.Exists((Vec2 boundaryPoint) => boundaryPoint.Distance(point) <= 0.1f))
			{
				deploymentBoundaries.Add(point);
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00064764 File Offset: 0x00062964
		private static Vec2 ClampRayToMissionBoundaries(List<Vec2> boundaries, Vec2 origin, Vec2 direction, float maxLength)
		{
			if (Mission.Current.IsPositionInsideBoundaries(origin))
			{
				Vec2 vec = origin + direction * maxLength;
				if (Mission.Current.IsPositionInsideBoundaries(vec))
				{
					return vec;
				}
			}
			Vec2 result;
			if (MBMath.IntersectRayWithBoundaryList(origin, direction, boundaries, out result))
			{
				return result;
			}
			return origin;
		}

		// Token: 0x04000930 RID: 2352
		public const float DeployZoneMinimumWidth = 100f;

		// Token: 0x04000931 RID: 2353
		public const float DeployZoneForwardMargin = 10f;

		// Token: 0x04000932 RID: 2354
		public const float DeployZoneExtraWidthPerTroop = 1.5f;

		// Token: 0x04000933 RID: 2355
		public readonly BattleSideEnum Side;

		// Token: 0x04000934 RID: 2356
		private readonly Mission _mission;

		// Token: 0x04000935 RID: 2357
		private readonly DeploymentPlan _initialPlan;

		// Token: 0x04000936 RID: 2358
		private bool _spawnWithHorses;

		// Token: 0x04000937 RID: 2359
		private bool _reinforcementPlansCreated;

		// Token: 0x04000938 RID: 2360
		private readonly List<DeploymentPlan> _reinforcementPlans;

		// Token: 0x04000939 RID: 2361
		private DeploymentPlan _currentReinforcementPlan;

		// Token: 0x0400093A RID: 2362
		[TupleElementNames(new string[]
		{
			"id",
			"points"
		})]
		private readonly MBList<ValueTuple<string, List<Vec2>>> _deploymentBoundaries = new MBList<ValueTuple<string, List<Vec2>>>();

		// Token: 0x0400093B RID: 2363
		private MatrixFrame _deploymentFrame;
	}
}
