using System;
using System.Diagnostics;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000154 RID: 340
	public abstract class OrderComponent
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x00037514 File Offset: 0x00035714
		public Vec2 GetDirection(Formation f)
		{
			Vec2 vec = this.Direction(f);
			if (f.IsAIControlled && vec.DotProduct(this._previousDirection) > 0.87f)
			{
				vec = this._previousDirection;
			}
			else
			{
				this._previousDirection = vec;
			}
			return vec;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003755B File Offset: 0x0003575B
		protected void CopyPositionAndDirectionFrom(OrderComponent order)
		{
			this.Position = order.Position;
			this.Direction = order.Direction;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00037575 File Offset: 0x00035775
		protected OrderComponent(float tickTimerDuration = 0.5f)
		{
			this._tickTimer = new Timer(Mission.Current.CurrentTime, tickTimerDuration, true);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001154 RID: 4436
		public abstract OrderType OrderType { get; }

		// Token: 0x06001155 RID: 4437 RVA: 0x0003759F File Offset: 0x0003579F
		internal bool Tick(Formation formation)
		{
			bool flag = this._tickTimer.Check(Mission.Current.CurrentTime);
			if (flag)
			{
				this.TickOccasionally(formation, this._tickTimer.PreviousDeltaTime);
			}
			return flag;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000375CB File Offset: 0x000357CB
		[Conditional("DEBUG")]
		protected virtual void TickDebug(Formation formation)
		{
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000375CD File Offset: 0x000357CD
		protected internal virtual void TickOccasionally(Formation formation, float dt)
		{
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000375CF File Offset: 0x000357CF
		protected internal virtual void OnApply(Formation formation)
		{
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000375D1 File Offset: 0x000357D1
		protected internal virtual void OnCancel(Formation formation)
		{
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000375D3 File Offset: 0x000357D3
		protected internal virtual void OnUnitJoinOrLeave(Agent unit, bool isJoining)
		{
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000375D5 File Offset: 0x000357D5
		protected internal virtual bool IsApplicable(Formation formation)
		{
			return true;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000375D8 File Offset: 0x000357D8
		protected internal virtual bool CanStack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000375DB File Offset: 0x000357DB
		protected internal virtual bool CancelsPreviousDirectionOrder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000375DE File Offset: 0x000357DE
		protected internal virtual bool CancelsPreviousArrangementOrder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000375E1 File Offset: 0x000357E1
		protected internal virtual MovementOrder GetSubstituteOrder(Formation formation)
		{
			return MovementOrder.MovementOrderCharge;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000375E8 File Offset: 0x000357E8
		protected internal virtual void OnArrangementChanged(Formation formation)
		{
		}

		// Token: 0x04000459 RID: 1113
		private readonly Timer _tickTimer;

		// Token: 0x0400045A RID: 1114
		protected Func<Formation, Vec3> Position;

		// Token: 0x0400045B RID: 1115
		protected Func<Formation, Vec2> Direction;

		// Token: 0x0400045C RID: 1116
		private Vec2 _previousDirection = Vec2.Invalid;
	}
}
