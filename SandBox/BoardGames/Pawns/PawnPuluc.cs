using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000CA RID: 202
	public class PawnPuluc : PawnBase
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0004C369 File Offset: 0x0004A569
		public float Height
		{
			get
			{
				if (PawnPuluc._height == 0f)
				{
					PawnPuluc._height = (base.Entity.GetBoundingBoxMax() - base.Entity.GetBoundingBoxMin()).z;
				}
				return PawnPuluc._height;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0004C3A1 File Offset: 0x0004A5A1
		public override Vec3 PosBeforeMoving
		{
			get
			{
				return this.PosBeforeMovingBase - new Vec3(0f, 0f, this.Height * (float)this.PawnsBelow.Count, -1f);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0004C3D5 File Offset: 0x0004A5D5
		public override bool IsPlaced
		{
			get
			{
				return (this.InPlay || this.IsInSpawn) && this.IsTopPawn;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0004C3EF File Offset: 0x0004A5EF
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0004C3F7 File Offset: 0x0004A5F7
		public int X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
				if (value >= 0 && value < 11)
				{
					this.IsInSpawn = false;
					return;
				}
				this.IsInSpawn = true;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0004C418 File Offset: 0x0004A618
		public List<PawnPuluc> PawnsBelow { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0004C420 File Offset: 0x0004A620
		public bool InPlay
		{
			get
			{
				return this.X >= 0 && this.X < 11;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0004C437 File Offset: 0x0004A637
		public PawnPuluc(GameEntity entity, bool playerOne) : base(entity, playerOne)
		{
			this.PawnsBelow = new List<PawnPuluc>();
			this.SpawnPos = base.CurrentPos;
			this.X = -1;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004C46D File Offset: 0x0004A66D
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.State = PawnPuluc.MovementState.MovingForward;
			this.IsTopPawn = true;
			this.IsInSpawn = true;
			this.CapturedBy = null;
			this.PawnsBelow.Clear();
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0004C4A4 File Offset: 0x0004A6A4
		public override void AddGoalPosition(Vec3 goal)
		{
			if (this.IsTopPawn)
			{
				goal.z += this.Height * (float)this.PawnsBelow.Count;
				int count = this.PawnsBelow.Count;
				for (int i = 0; i < count; i++)
				{
					this.PawnsBelow[i].AddGoalPosition(goal - new Vec3(0f, 0f, (float)(i + 1) * this.Height, -1f));
				}
			}
			base.GoalPositions.Add(goal);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0004C534 File Offset: 0x0004A734
		public override void MovePawnToGoalPositions(bool instantMove, float speed, bool dragged = false)
		{
			if (base.GoalPositions.Count == 0)
			{
				return;
			}
			base.MovePawnToGoalPositions(instantMove, speed, dragged);
			if (this.IsTopPawn)
			{
				foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
				{
					pawnPuluc.MovePawnToGoalPositions(instantMove, speed, dragged);
				}
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		public override void SetPawnAtPosition(Vec3 position)
		{
			base.SetPawnAtPosition(position);
			if (this.IsTopPawn)
			{
				int num = 1;
				foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
				{
					pawnPuluc.SetPawnAtPosition(new Vec3(position.x, position.y, position.z - this.Height * (float)num, -1f));
					num++;
				}
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0004C634 File Offset: 0x0004A834
		public override void EnableCollisionBody()
		{
			base.EnableCollisionBody();
			foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
			{
				pawnPuluc.Entity.BodyFlag &= ~BodyFlags.Disabled;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0004C698 File Offset: 0x0004A898
		public override void DisableCollisionBody()
		{
			base.DisableCollisionBody();
			foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
			{
				pawnPuluc.Entity.BodyFlag |= BodyFlags.Disabled;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0004C6FC File Offset: 0x0004A8FC
		public void MovePawnBackToSpawn(bool instantMove, float speed, bool fake = false)
		{
			this.X = -1;
			this.State = PawnPuluc.MovementState.MovingForward;
			this.IsTopPawn = true;
			this.IsInSpawn = true;
			base.Captured = false;
			this.CapturedBy = null;
			this.PawnsBelow.Clear();
			if (!fake)
			{
				this.AddGoalPosition(this.SpawnPos);
				this.MovePawnToGoalPositions(instantMove, speed, false);
			}
		}

		// Token: 0x040003E7 RID: 999
		public PawnPuluc.MovementState State;

		// Token: 0x040003E8 RID: 1000
		public PawnPuluc CapturedBy;

		// Token: 0x040003E9 RID: 1001
		public Vec3 SpawnPos;

		// Token: 0x040003EA RID: 1002
		public bool IsInSpawn = true;

		// Token: 0x040003EB RID: 1003
		public bool IsTopPawn = true;

		// Token: 0x040003EC RID: 1004
		private static float _height;

		// Token: 0x040003ED RID: 1005
		private int _x;

		// Token: 0x020001C3 RID: 451
		public enum MovementState
		{
			// Token: 0x0400077B RID: 1915
			MovingForward,
			// Token: 0x0400077C RID: 1916
			MovingBackward,
			// Token: 0x0400077D RID: 1917
			ChangingDirection
		}
	}
}
