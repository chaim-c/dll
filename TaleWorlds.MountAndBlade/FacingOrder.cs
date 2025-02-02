using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014F RID: 335
	public struct FacingOrder
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x00035091 File Offset: 0x00033291
		public static FacingOrder FacingOrderLookAtDirection(Vec2 direction)
		{
			return new FacingOrder(FacingOrder.FacingOrderEnum.LookAtDirection, direction);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003509A File Offset: 0x0003329A
		private FacingOrder(FacingOrder.FacingOrderEnum orderEnum, Vec2 direction)
		{
			this.OrderEnum = orderEnum;
			this._lookAtDirection = direction;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000350AA File Offset: 0x000332AA
		private FacingOrder(FacingOrder.FacingOrderEnum orderEnum)
		{
			this.OrderEnum = orderEnum;
			this._lookAtDirection = Vec2.Invalid;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000350C0 File Offset: 0x000332C0
		private Vec2 GetDirectionAux(Formation f, Agent targetAgent)
		{
			if (f.PhysicalClass.IsMounted() && targetAgent != null && targetAgent.Velocity.LengthSquared > targetAgent.RunSpeedCached * targetAgent.RunSpeedCached * 0.09f)
			{
				return targetAgent.Velocity.AsVec2.Normalized();
			}
			if (this.OrderEnum == FacingOrder.FacingOrderEnum.LookAtDirection)
			{
				return this._lookAtDirection;
			}
			if (f.Arrangement is CircularFormation || f.Arrangement is SquareFormation)
			{
				return f.Direction;
			}
			Vec2 currentPosition = f.CurrentPosition;
			Vec2 weightedAverageEnemyPosition = f.QuerySystem.WeightedAverageEnemyPosition;
			if (!weightedAverageEnemyPosition.IsValid)
			{
				return f.Direction;
			}
			Vec2 vec = (weightedAverageEnemyPosition - currentPosition).Normalized();
			float length = (weightedAverageEnemyPosition - currentPosition).Length;
			int enemyUnitCount = f.QuerySystem.Team.EnemyUnitCount;
			int countOfUnits = f.CountOfUnits;
			Vec2 vec2 = f.Direction;
			bool flag = length >= (float)countOfUnits * 0.2f;
			if (enemyUnitCount == 0 || countOfUnits == 0)
			{
				flag = false;
			}
			float num = (!flag) ? 1f : (MBMath.ClampFloat((float)countOfUnits * 1f / (float)enemyUnitCount, 0.33333334f, 3f) * MBMath.ClampFloat(length / (float)countOfUnits, 0.33333334f, 3f));
			if (flag && MathF.Abs(vec.AngleBetween(vec2)) > 0.17453292f * num)
			{
				vec2 = vec;
			}
			return vec2;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0003522C File Offset: 0x0003342C
		public OrderType OrderType
		{
			get
			{
				if (this.OrderEnum != FacingOrder.FacingOrderEnum.LookAtDirection)
				{
					return OrderType.LookAtEnemy;
				}
				return OrderType.LookAtDirection;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003523B File Offset: 0x0003343B
		public Vec2 GetDirection(Formation f, Agent targetAgent = null)
		{
			return this.GetDirectionAux(f, targetAgent);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00035248 File Offset: 0x00033448
		public override bool Equals(object obj)
		{
			if (obj is FacingOrder)
			{
				FacingOrder f = (FacingOrder)obj;
				return f == this;
			}
			return false;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00035274 File Offset: 0x00033474
		public override int GetHashCode()
		{
			return (int)this.OrderEnum;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003527C File Offset: 0x0003347C
		public static bool operator !=(FacingOrder f1, FacingOrder f2)
		{
			return f1.OrderEnum != f2.OrderEnum;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003528F File Offset: 0x0003348F
		public static bool operator ==(FacingOrder f1, FacingOrder f2)
		{
			return f1.OrderEnum == f2.OrderEnum;
		}

		// Token: 0x0400043A RID: 1082
		public readonly FacingOrder.FacingOrderEnum OrderEnum;

		// Token: 0x0400043B RID: 1083
		private readonly Vec2 _lookAtDirection;

		// Token: 0x0400043C RID: 1084
		public static readonly FacingOrder FacingOrderLookAtEnemy = new FacingOrder(FacingOrder.FacingOrderEnum.LookAtEnemy);

		// Token: 0x02000444 RID: 1092
		public enum FacingOrderEnum
		{
			// Token: 0x040018DF RID: 6367
			LookAtDirection,
			// Token: 0x040018E0 RID: 6368
			LookAtEnemy
		}
	}
}
