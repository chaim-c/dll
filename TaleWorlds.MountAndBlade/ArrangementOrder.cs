using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014E RID: 334
	public struct ArrangementOrder
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x000346B5 File Offset: 0x000328B5
		public static int GetUnitSpacingOf(ArrangementOrder.ArrangementOrderEnum a)
		{
			switch (a)
			{
			case ArrangementOrder.ArrangementOrderEnum.Loose:
				return 6;
			case ArrangementOrder.ArrangementOrderEnum.ShieldWall:
			case ArrangementOrder.ArrangementOrderEnum.Square:
				return 0;
			}
			return 2;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000346DA File Offset: 0x000328DA
		public static bool GetUnitLooseness(ArrangementOrder.ArrangementOrderEnum a)
		{
			return a != ArrangementOrder.ArrangementOrderEnum.ShieldWall;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000346E4 File Offset: 0x000328E4
		public ArrangementOrder(ArrangementOrder.ArrangementOrderEnum orderEnum)
		{
			this.OrderEnum = orderEnum;
			this._walkRestriction = null;
			switch (this.OrderEnum)
			{
			case ArrangementOrder.ArrangementOrderEnum.Circle:
				this._runRestriction = new float?(0.5f);
				goto IL_9A;
			case ArrangementOrder.ArrangementOrderEnum.Line:
				this._runRestriction = new float?(0.8f);
				goto IL_9A;
			case ArrangementOrder.ArrangementOrderEnum.Loose:
			case ArrangementOrder.ArrangementOrderEnum.Scatter:
			case ArrangementOrder.ArrangementOrderEnum.Skein:
				this._runRestriction = new float?(0.9f);
				goto IL_9A;
			case ArrangementOrder.ArrangementOrderEnum.ShieldWall:
			case ArrangementOrder.ArrangementOrderEnum.Square:
				this._runRestriction = new float?(0.3f);
				goto IL_9A;
			}
			this._runRestriction = new float?(1f);
			IL_9A:
			this._unitSpacing = ArrangementOrder.GetUnitSpacingOf(this.OrderEnum);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003479C File Offset: 0x0003299C
		public void GetMovementSpeedRestriction(out float? runRestriction, out float? walkRestriction)
		{
			runRestriction = this._runRestriction;
			walkRestriction = this._walkRestriction;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000347B8 File Offset: 0x000329B8
		public IFormationArrangement GetArrangement(Formation formation)
		{
			ArrangementOrder.ArrangementOrderEnum orderEnum = this.OrderEnum;
			if (orderEnum <= ArrangementOrder.ArrangementOrderEnum.Column)
			{
				if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Circle)
				{
					return new CircularFormation(formation);
				}
				if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Column)
				{
					return new ColumnFormation(formation, null, 1);
				}
			}
			else
			{
				if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Skein)
				{
					return new SkeinFormation(formation);
				}
				if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
				{
					return new RectilinearSchiltronFormation(formation);
				}
			}
			return new LineFormation(formation, true);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00034808 File Offset: 0x00032A08
		public void OnApply(Formation formation)
		{
			formation.SetPositioning(null, null, new int?(this.GetUnitSpacing()));
			this.Rearrange(formation);
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Scatter)
			{
				this.TickOccasionally(formation);
				formation.ResetArrangementOrderTickTimer();
			}
			ArrangementOrder.ArrangementOrderEnum orderEnum = this.OrderEnum;
			formation.ApplyActionOnEachUnit(delegate(Agent agent)
			{
				if (agent.IsAIControlled)
				{
					Agent.UsageDirection shieldDirectionOfUnit = ArrangementOrder.GetShieldDirectionOfUnit(formation, agent, orderEnum);
					agent.EnforceShieldUsage(shieldDirectionOfUnit);
				}
				agent.UpdateAgentProperties();
				agent.RefreshBehaviorValues(formation.GetReadonlyMovementOrderReference().OrderEnum, orderEnum);
			}, null);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003489A File Offset: 0x00032A9A
		public void SoftUpdate(Formation formation)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Scatter)
			{
				this.TickOccasionally(formation);
				formation.ResetArrangementOrderTickTimer();
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000348B4 File Offset: 0x00032AB4
		public static Agent.UsageDirection GetShieldDirectionOfUnit(Formation formation, Agent unit, ArrangementOrder.ArrangementOrderEnum orderEnum)
		{
			Agent.UsageDirection result;
			if (unit.IsDetachedFromFormation)
			{
				result = Agent.UsageDirection.None;
			}
			else if (orderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall)
			{
				if (((IFormationUnit)unit).FormationRankIndex == 0)
				{
					result = Agent.UsageDirection.DefendDown;
				}
				else if (formation.Arrangement.GetNeighborUnitOfLeftSide(unit) == null)
				{
					result = Agent.UsageDirection.DefendLeft;
				}
				else if (formation.Arrangement.GetNeighborUnitOfRightSide(unit) == null)
				{
					result = Agent.UsageDirection.DefendRight;
				}
				else
				{
					result = Agent.UsageDirection.AttackEnd;
				}
			}
			else if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || orderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
			{
				if (((IFormationUnit)unit).IsShieldUsageEncouraged)
				{
					if (((IFormationUnit)unit).FormationRankIndex == 0)
					{
						result = Agent.UsageDirection.DefendDown;
					}
					else
					{
						result = Agent.UsageDirection.AttackEnd;
					}
				}
				else
				{
					result = Agent.UsageDirection.None;
				}
			}
			else
			{
				result = Agent.UsageDirection.None;
			}
			return result;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003492F File Offset: 0x00032B2F
		public int GetUnitSpacing()
		{
			return this._unitSpacing;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00034937 File Offset: 0x00032B37
		public void Rearrange(Formation formation)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Column)
			{
				this.RearrangeAux(formation, false);
				return;
			}
			formation.Rearrange(this.GetArrangement(formation));
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00034958 File Offset: 0x00032B58
		public void RearrangeAux(Formation formation, bool isDirectly)
		{
			float num = MathF.Max(1f, MathF.Max(formation.Depth, formation.Width) * 0.8f);
			float lengthSquared = (formation.CurrentPosition - formation.OrderPosition).LengthSquared;
			if (!isDirectly && lengthSquared < num * num)
			{
				ArrangementOrder.TransposeLineFormation(formation);
				formation.OnTick += formation.TickForColumnArrangementInitialPositioning;
				return;
			}
			formation.OnTick -= formation.TickForColumnArrangementInitialPositioning;
			formation.ReferencePosition = null;
			formation.Rearrange(this.GetArrangement(formation));
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000349F0 File Offset: 0x00032BF0
		private unsafe static void TransposeLineFormation(Formation formation)
		{
			formation.Rearrange(new TransposedLineFormation(formation));
			MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
			formation.SetPositioning(new WorldPosition?(movementOrder.CreateNewOrderWorldPosition(formation, WorldPosition.WorldPositionEnforcedCache.None)), null, null);
			formation.ReferencePosition = new Vec2?(formation.OrderPosition);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00034A4C File Offset: 0x00032C4C
		public void OnCancel(Formation formation)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Scatter)
			{
				Team team = formation.Team;
				if (((team != null) ? team.TeamAI : null) != null)
				{
					MBReadOnlyList<StrategicArea> strategicAreas = formation.Team.TeamAI.StrategicAreas;
					for (int i = formation.Detachments.Count - 1; i >= 0; i--)
					{
						IDetachment detachment = formation.Detachments[i];
						foreach (StrategicArea strategicArea in strategicAreas)
						{
							if (detachment == strategicArea)
							{
								formation.LeaveDetachment(detachment);
								break;
							}
						}
					}
				}
			}
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall || this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
			{
				formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					if (agent.IsAIControlled)
					{
						agent.EnforceShieldUsage(Agent.UsageDirection.None);
					}
				}, null);
			}
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Column)
			{
				formation.OnTick -= formation.TickForColumnArrangementInitialPositioning;
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00034B54 File Offset: 0x00032D54
		private static StrategicArea CreateStrategicArea(Scene scene, WorldPosition position, Vec2 direction, float width, int capacity, BattleSideEnum side)
		{
			WorldFrame worldFrame = new WorldFrame(new Mat3
			{
				f = direction.ToVec3(0f),
				u = Vec3.Up
			}, position);
			GameEntity gameEntity = GameEntity.Instantiate(scene, "strategic_area_autogen", worldFrame.ToNavMeshMatrixFrame());
			gameEntity.SetMobility(GameEntity.Mobility.dynamic);
			StrategicArea firstScriptOfType = gameEntity.GetFirstScriptOfType<StrategicArea>();
			firstScriptOfType.InitializeAutogenerated(width, capacity, side);
			return firstScriptOfType;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00034BBC File Offset: 0x00032DBC
		private static IEnumerable<StrategicArea> CreateStrategicAreas(Mission mission, int count, WorldPosition center, float distance, WorldPosition target, float width, int capacity, BattleSideEnum side)
		{
			Scene scene = mission.Scene;
			float distanceMultiplied = distance * 0.7f;
			Func<WorldPosition> func = delegate()
			{
				WorldPosition center2 = center;
				float rotation = MBRandom.RandomFloat * 3.1415927f * 2f;
				center2.SetVec2(center.AsVec2 + Vec2.FromRotation(rotation) * distanceMultiplied);
				return center2;
			};
			WorldPosition[] array = delegate
			{
				float num2 = MBRandom.RandomFloat * 3.1415927f * 2f;
				switch (count)
				{
				case 2:
				{
					WorldPosition center2 = center;
					center2.SetVec2(center.AsVec2 + Vec2.FromRotation(num2) * distanceMultiplied);
					WorldPosition center3 = center;
					center3.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 3.1415927f) * distanceMultiplied);
					return new WorldPosition[]
					{
						center2,
						center3
					};
				}
				case 3:
				{
					WorldPosition center4 = center;
					center4.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 0f) * distanceMultiplied);
					WorldPosition center5 = center;
					center5.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 2.0943952f) * distanceMultiplied);
					WorldPosition center6 = center;
					center6.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 4.1887903f) * distanceMultiplied);
					return new WorldPosition[]
					{
						center4,
						center5,
						center6
					};
				}
				case 4:
				{
					WorldPosition center7 = center;
					center7.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 0f) * distanceMultiplied);
					WorldPosition center8 = center;
					center8.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 1.5707964f) * distanceMultiplied);
					WorldPosition center9 = center;
					center9.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 3.1415927f) * distanceMultiplied);
					WorldPosition center10 = center;
					center10.SetVec2(center.AsVec2 + Vec2.FromRotation(num2 + 4.712389f) * distanceMultiplied);
					return new WorldPosition[]
					{
						center7,
						center8,
						center9,
						center10
					};
				}
				default:
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Orders\\ArrangementOrder.cs", "CreateStrategicAreas", 362);
					return new WorldPosition[0];
				}
			}();
			List<WorldPosition> positions = new List<WorldPosition>();
			WorldPosition[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				WorldPosition worldPosition = array2[i];
				WorldPosition worldPosition2 = worldPosition;
				WorldPosition position = mission.FindPositionWithBiggestSlopeTowardsDirectionInSquare(ref worldPosition2, distance * 0.25f, ref target);
				Func<WorldPosition, bool> func2 = delegate(WorldPosition p)
				{
					float num2;
					if (!positions.Any((WorldPosition wp) => wp.AsVec2.DistanceSquared(p.AsVec2) < 1f) && (scene.GetPathDistanceBetweenPositions(ref center, ref p, 0f, out num2) && num2 < center.AsVec2.Distance(p.AsVec2) * 2f))
					{
						positions.Add(position);
						return true;
					}
					return false;
				};
				if (!func2(position) && !func2(worldPosition))
				{
					int num = 0;
					while (num++ < 10 && !func2(func()))
					{
					}
					if (num >= 10)
					{
						positions.Add(center);
					}
				}
			}
			Vec2 direction = (target.AsVec2 - center.AsVec2).Normalized();
			foreach (WorldPosition position2 in positions)
			{
				yield return ArrangementOrder.CreateStrategicArea(scene, position2, direction, width, capacity, side);
			}
			List<WorldPosition>.Enumerator enumerator = default(List<WorldPosition>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00034C0C File Offset: 0x00032E0C
		private bool IsStrategicAreaClose(StrategicArea strategicArea, Formation formation)
		{
			if (!strategicArea.IsUsableBy(formation.Team.Side))
			{
				return false;
			}
			if (strategicArea.IgnoreHeight)
			{
				return MathF.Abs(strategicArea.GameEntity.GlobalPosition.x - formation.OrderPosition.X) <= strategicArea.DistanceToCheck && MathF.Abs(strategicArea.GameEntity.GlobalPosition.y - formation.OrderPosition.Y) <= strategicArea.DistanceToCheck;
			}
			WorldPosition worldPosition = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
			Vec3 globalPosition = strategicArea.GameEntity.GlobalPosition;
			return worldPosition.DistanceSquaredWithLimit(globalPosition, strategicArea.DistanceToCheck * strategicArea.DistanceToCheck + 1E-05f) < strategicArea.DistanceToCheck * strategicArea.DistanceToCheck;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00034CD8 File Offset: 0x00032ED8
		public void TickOccasionally(Formation formation)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Scatter)
			{
				Team team = formation.Team;
				if (((team != null) ? team.TeamAI : null) != null)
				{
					MBReadOnlyList<StrategicArea> strategicAreas = formation.Team.TeamAI.StrategicAreas;
					foreach (StrategicArea strategicArea in strategicAreas)
					{
						if (this.IsStrategicAreaClose(strategicArea, formation))
						{
							bool flag = false;
							foreach (IDetachment detachment in formation.Detachments)
							{
								if (strategicArea == detachment)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								formation.JoinDetachment(strategicArea);
							}
						}
					}
					for (int i = formation.Detachments.Count - 1; i >= 0; i--)
					{
						IDetachment detachment2 = formation.Detachments[i];
						foreach (StrategicArea strategicArea2 in strategicAreas)
						{
							if (detachment2 == strategicArea2 && !this.IsStrategicAreaClose(strategicArea2, formation))
							{
								formation.LeaveDetachment(detachment2);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00034E2C File Offset: 0x0003302C
		public OrderType OrderType
		{
			get
			{
				switch (this.OrderEnum)
				{
				case ArrangementOrder.ArrangementOrderEnum.Circle:
					return OrderType.ArrangementCircular;
				case ArrangementOrder.ArrangementOrderEnum.Column:
					return OrderType.ArrangementColumn;
				case ArrangementOrder.ArrangementOrderEnum.Line:
					return OrderType.ArrangementLine;
				case ArrangementOrder.ArrangementOrderEnum.Loose:
					return OrderType.ArrangementLoose;
				case ArrangementOrder.ArrangementOrderEnum.Scatter:
					return OrderType.ArrangementScatter;
				case ArrangementOrder.ArrangementOrderEnum.ShieldWall:
					return OrderType.ArrangementCloseOrder;
				case ArrangementOrder.ArrangementOrderEnum.Skein:
					return OrderType.ArrangementVee;
				case ArrangementOrder.ArrangementOrderEnum.Square:
					return OrderType.ArrangementSchiltron;
				default:
					return OrderType.ArrangementLine;
				}
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00034E82 File Offset: 0x00033082
		public ArrangementOrder.ArrangementOrderEnum GetNativeEnum()
		{
			return this.OrderEnum;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00034E8A File Offset: 0x0003308A
		public override bool Equals(object obj)
		{
			return obj is ArrangementOrder && (ArrangementOrder)obj == this;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00034EA7 File Offset: 0x000330A7
		public override int GetHashCode()
		{
			return (int)this.OrderEnum;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00034EAF File Offset: 0x000330AF
		public static bool operator !=(ArrangementOrder a1, ArrangementOrder a2)
		{
			return a1.OrderEnum != a2.OrderEnum;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00034EC2 File Offset: 0x000330C2
		public static bool operator ==(ArrangementOrder a1, ArrangementOrder a2)
		{
			return a1.OrderEnum == a2.OrderEnum;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00034ED4 File Offset: 0x000330D4
		public void OnOrderPositionChanged(Formation formation, Vec2 previousOrderPosition)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Column && formation.Arrangement is TransposedLineFormation)
			{
				Vec2 direction = formation.Direction;
				Vec2 vector = (formation.OrderPosition - previousOrderPosition).Normalized();
				float num = direction.AngleBetween(vector);
				if ((num > 1.5707964f || num < -1.5707964f) && formation.QuerySystem.AveragePosition.DistanceSquared(formation.OrderPosition) < formation.Depth * formation.Depth / 10f)
				{
					formation.ReferencePosition = new Vec2?(formation.OrderPosition);
				}
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00034F6E File Offset: 0x0003316E
		public static int GetArrangementOrderDefensiveness(ArrangementOrder.ArrangementOrderEnum orderEnum)
		{
			if (orderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || orderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall || orderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00034F7E File Offset: 0x0003317E
		public static int GetArrangementOrderDefensivenessChange(ArrangementOrder.ArrangementOrderEnum previousOrderEnum, ArrangementOrder.ArrangementOrderEnum nextOrderEnum)
		{
			if (previousOrderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || previousOrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall || previousOrderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
			{
				if (nextOrderEnum != ArrangementOrder.ArrangementOrderEnum.Circle && nextOrderEnum != ArrangementOrder.ArrangementOrderEnum.ShieldWall && nextOrderEnum != ArrangementOrder.ArrangementOrderEnum.Square)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (nextOrderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || nextOrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall || nextOrderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00034FA8 File Offset: 0x000331A8
		public float CalculateFormationDirectionEnforcingFactorForRank(int formationRankIndex, int rankCount)
		{
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Circle || this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall || this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Square)
			{
				return 1f - MBMath.ClampFloat(((float)formationRankIndex + 1f) / ((float)rankCount * 2f), 0f, 1f);
			}
			if (this.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Column)
			{
				return 0f;
			}
			return 1f - MBMath.ClampFloat(((float)formationRankIndex + 1f) / ((float)rankCount * 0.5f), 0f, 1f);
		}

		// Token: 0x0400042E RID: 1070
		private float? _walkRestriction;

		// Token: 0x0400042F RID: 1071
		private float? _runRestriction;

		// Token: 0x04000430 RID: 1072
		private int _unitSpacing;

		// Token: 0x04000431 RID: 1073
		public readonly ArrangementOrder.ArrangementOrderEnum OrderEnum;

		// Token: 0x04000432 RID: 1074
		public static readonly ArrangementOrder ArrangementOrderCircle = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Circle);

		// Token: 0x04000433 RID: 1075
		public static readonly ArrangementOrder ArrangementOrderColumn = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Column);

		// Token: 0x04000434 RID: 1076
		public static readonly ArrangementOrder ArrangementOrderLine = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Line);

		// Token: 0x04000435 RID: 1077
		public static readonly ArrangementOrder ArrangementOrderLoose = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Loose);

		// Token: 0x04000436 RID: 1078
		public static readonly ArrangementOrder ArrangementOrderScatter = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Scatter);

		// Token: 0x04000437 RID: 1079
		public static readonly ArrangementOrder ArrangementOrderShieldWall = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.ShieldWall);

		// Token: 0x04000438 RID: 1080
		public static readonly ArrangementOrder ArrangementOrderSkein = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Skein);

		// Token: 0x04000439 RID: 1081
		public static readonly ArrangementOrder ArrangementOrderSquare = new ArrangementOrder(ArrangementOrder.ArrangementOrderEnum.Square);

		// Token: 0x0200043D RID: 1085
		public enum ArrangementOrderEnum
		{
			// Token: 0x040018B4 RID: 6324
			Circle,
			// Token: 0x040018B5 RID: 6325
			Column,
			// Token: 0x040018B6 RID: 6326
			Line,
			// Token: 0x040018B7 RID: 6327
			Loose,
			// Token: 0x040018B8 RID: 6328
			Scatter,
			// Token: 0x040018B9 RID: 6329
			ShieldWall,
			// Token: 0x040018BA RID: 6330
			Skein,
			// Token: 0x040018BB RID: 6331
			Square
		}
	}
}
