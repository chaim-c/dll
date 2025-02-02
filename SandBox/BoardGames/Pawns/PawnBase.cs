using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000C7 RID: 199
	public abstract class PawnBase
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0004BB42 File Offset: 0x00049D42
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0004BB49 File Offset: 0x00049D49
		public static int PawnMoveSoundCodeID { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0004BB51 File Offset: 0x00049D51
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0004BB58 File Offset: 0x00049D58
		public static int PawnSelectSoundCodeID { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0004BB60 File Offset: 0x00049D60
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0004BB67 File Offset: 0x00049D67
		public static int PawnTapSoundCodeID { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0004BB6F File Offset: 0x00049D6F
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0004BB76 File Offset: 0x00049D76
		public static int PawnRemoveSoundCodeID { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000A0F RID: 2575
		public abstract bool IsPlaced { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0004BB7E File Offset: 0x00049D7E
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0004BB86 File Offset: 0x00049D86
		public virtual Vec3 PosBeforeMoving
		{
			get
			{
				return this.PosBeforeMovingBase;
			}
			protected set
			{
				this.PosBeforeMovingBase = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0004BB8F File Offset: 0x00049D8F
		public GameEntity Entity { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0004BB97 File Offset: 0x00049D97
		protected List<Vec3> GoalPositions { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0004BB9F File Offset: 0x00049D9F
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0004BBA7 File Offset: 0x00049DA7
		private protected Vec3 CurrentPos { protected get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0004BBB0 File Offset: 0x00049DB0
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0004BBB8 File Offset: 0x00049DB8
		public bool Captured { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0004BBC1 File Offset: 0x00049DC1
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0004BBC9 File Offset: 0x00049DC9
		public bool MovingToDifferentTile { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0004BBD2 File Offset: 0x00049DD2
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0004BBDA File Offset: 0x00049DDA
		public bool Moving { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0004BBE3 File Offset: 0x00049DE3
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0004BBEB File Offset: 0x00049DEB
		public bool PlayerOne { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0004BBF4 File Offset: 0x00049DF4
		public bool HasAnyGoalPosition
		{
			get
			{
				bool result = false;
				if (this.GoalPositions != null)
				{
					result = !this.GoalPositions.IsEmpty<Vec3>();
				}
				return result;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0004BC1C File Offset: 0x00049E1C
		protected PawnBase(GameEntity entity, bool playerOne)
		{
			this.Entity = entity;
			this.PlayerOne = playerOne;
			this.CurrentPos = this.Entity.GetGlobalFrame().origin;
			this.PosBeforeMoving = this.CurrentPos;
			this.Moving = false;
			this._dragged = false;
			this.Captured = false;
			this._movePauseDuration = 0.3f;
			this.GoalPositions = new List<Vec3>();
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0004BC8C File Offset: 0x00049E8C
		public virtual void Reset()
		{
			this.ClearGoalPositions();
			this.Moving = false;
			this.MovingToDifferentTile = false;
			this._movePauseDuration = 0.3f;
			this._movePauseTimer = 0f;
			this._moveTiming = false;
			this._dragged = false;
			this.Captured = false;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0004BCD8 File Offset: 0x00049ED8
		public virtual void AddGoalPosition(Vec3 goal)
		{
			this.GoalPositions.Add(goal);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0004BCE8 File Offset: 0x00049EE8
		public virtual void SetPawnAtPosition(Vec3 position)
		{
			MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
			globalFrame.origin = position;
			this.Entity.SetGlobalFrame(globalFrame);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0004BD18 File Offset: 0x00049F18
		public virtual void MovePawnToGoalPositions(bool instantMove, float speed, bool dragged = false)
		{
			this.PosBeforeMoving = this.Entity.GlobalPosition;
			this._moveSpeed = speed;
			this._currentGoalPos = 0;
			this._movePauseTimer = 0f;
			this._dtCounter = 0f;
			this._moveTiming = false;
			this._dragged = dragged;
			if (this.GoalPositions.Count == 1 && this.PosBeforeMoving.Equals(this.GoalPositions[0]))
			{
				instantMove = true;
			}
			if (instantMove)
			{
				MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
				globalFrame.origin = this.GoalPositions[this.GoalPositions.Count - 1];
				this.Entity.SetGlobalFrame(globalFrame);
				this.ClearGoalPositions();
				return;
			}
			this.Moving = true;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0004BDE9 File Offset: 0x00049FE9
		public virtual void EnableCollisionBody()
		{
			this.Entity.BodyFlag &= ~BodyFlags.Disabled;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0004BDFF File Offset: 0x00049FFF
		public virtual void DisableCollisionBody()
		{
			this.Entity.BodyFlag |= BodyFlags.Disabled;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004BE14 File Offset: 0x0004A014
		public void Tick(float dt)
		{
			if (this._moveTiming)
			{
				this._movePauseTimer += dt;
				if (this._movePauseTimer >= this._movePauseDuration)
				{
					this._moveTiming = false;
					this._movePauseTimer = 0f;
				}
				return;
			}
			if (this.Moving)
			{
				Vec3 vec = new Vec3(0f, 0f, 0f, -1f);
				Vec3 v = this.GoalPositions[this._currentGoalPos] - this.PosBeforeMoving;
				float num = v.Normalize();
				float num2 = num / this._moveSpeed;
				float num3 = this._dtCounter / num2;
				if (this._dtCounter.Equals(0f))
				{
					float x = (this.Entity.GlobalBoxMax - this.Entity.GlobalBoxMin).x;
					float z = (this.Entity.GlobalBoxMax - this.Entity.GlobalBoxMin).z;
					Vec3 v2 = new Vec3(0f, 0f, z / 2f, -1f);
					Vec3 sourcePoint = this.Entity.GetGlobalFrame().origin + v2 + v * (x / 1.8f);
					Vec3 targetPoint = this.GoalPositions[this._currentGoalPos] + v2;
					float num4;
					if (Mission.Current.Scene.RayCastForClosestEntityOrTerrain(sourcePoint, targetPoint, out num4, 0.001f, BodyFlags.None))
					{
						this._freePathToDestination = false;
						num = num4;
					}
					else
					{
						this._freePathToDestination = true;
						if (!this._dragged)
						{
							this.PlayPawnMoveSound();
						}
						else
						{
							this.PlayPawnTapSound();
						}
					}
				}
				if (!this._freePathToDestination)
				{
					float num5 = MathF.Sin(num3 * 3.1415927f);
					float num6 = num / 6f;
					num5 *= num6;
					vec += new Vec3(0f, 0f, num5, -1f);
				}
				float dtCounter = this._dtCounter;
				this._dtCounter += dt;
				if (num3 >= 1f)
				{
					this._dtCounter = 0f;
					this.CurrentPos = this.GoalPositions[this._currentGoalPos];
					vec = Vec3.Zero;
					if (!this._freePathToDestination && this.IsPlaced)
					{
						this.PlayPawnTapSound();
					}
					else if (!this.IsPlaced)
					{
						this.PlayPawnRemovedTapSound();
					}
					Vec3 v3 = this.GoalPositions[this._currentGoalPos];
					bool flag = true;
					while (this._currentGoalPos < this.GoalPositions.Count - 1)
					{
						this._currentGoalPos++;
						Vec3 v4 = this.GoalPositions[this._currentGoalPos];
						if ((v3 - v4).LengthSquared > 0f)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						Action<PawnBase, Vec3, Vec3> onArrivedFinalGoalPosition = this.OnArrivedFinalGoalPosition;
						if (onArrivedFinalGoalPosition != null)
						{
							onArrivedFinalGoalPosition(this, this.PosBeforeMoving, this.CurrentPos);
						}
						this.Moving = false;
						this.ClearGoalPositions();
					}
					else
					{
						Action<PawnBase, Vec3, Vec3> onArrivedIntermediateGoalPosition = this.OnArrivedIntermediateGoalPosition;
						if (onArrivedIntermediateGoalPosition != null)
						{
							onArrivedIntermediateGoalPosition(this, this.PosBeforeMoving, this.CurrentPos);
						}
						this._movePauseDuration = 0.3f;
						this._moveTiming = true;
					}
					this.PosBeforeMoving = this.CurrentPos;
				}
				else
				{
					this.Moving = true;
					this.CurrentPos = MBMath.Lerp(this.PosBeforeMoving, this.GoalPositions[this._currentGoalPos], num3, 0.005f);
				}
				MatrixFrame matrixFrame = new MatrixFrame(this.Entity.GetGlobalFrame().rotation, this.CurrentPos + vec);
				this.Entity.SetGlobalFrame(matrixFrame);
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0004C1A8 File Offset: 0x0004A3A8
		public void MovePawnToGoalPositionsDelayed(bool instantMove, float speed, bool dragged, float delay)
		{
			if (this.GoalPositions.Count > 0)
			{
				if (this.GoalPositions.Count == 1 && this.PosBeforeMoving.Equals(this.GoalPositions[0]))
				{
					this.ClearGoalPositions();
					return;
				}
				this.MovePawnToGoalPositions(instantMove, speed, dragged);
				this._movePauseDuration = delay;
				this._moveTiming = (delay > 0f);
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004C21F File Offset: 0x0004A41F
		public void SetPlayerOne(bool playerOne)
		{
			this.PlayerOne = playerOne;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0004C228 File Offset: 0x0004A428
		public void ClearGoalPositions()
		{
			this.MovingToDifferentTile = false;
			this.GoalPositions.Clear();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004C23C File Offset: 0x0004A43C
		public void UpdatePawnPosition()
		{
			this.PosBeforeMoving = this.Entity.GlobalPosition;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004C24F File Offset: 0x0004A44F
		public void PlayPawnSelectSound()
		{
			Mission.Current.MakeSound(PawnBase.PawnSelectSoundCodeID, this.CurrentPos, true, false, -1, -1);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0004C26A File Offset: 0x0004A46A
		private void PlayPawnTapSound()
		{
			Mission.Current.MakeSound(PawnBase.PawnTapSoundCodeID, this.CurrentPos, true, false, -1, -1);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0004C285 File Offset: 0x0004A485
		private void PlayPawnRemovedTapSound()
		{
			Mission.Current.MakeSound(PawnBase.PawnRemoveSoundCodeID, this.CurrentPos, true, false, -1, -1);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004C2A0 File Offset: 0x0004A4A0
		private void PlayPawnMoveSound()
		{
			Mission.Current.MakeSound(PawnBase.PawnMoveSoundCodeID, this.CurrentPos, true, false, -1, -1);
		}

		// Token: 0x040003D0 RID: 976
		public Action<PawnBase, Vec3, Vec3> OnArrivedIntermediateGoalPosition;

		// Token: 0x040003D1 RID: 977
		public Action<PawnBase, Vec3, Vec3> OnArrivedFinalGoalPosition;

		// Token: 0x040003D2 RID: 978
		protected Vec3 PosBeforeMovingBase;

		// Token: 0x040003D3 RID: 979
		private int _currentGoalPos;

		// Token: 0x040003D4 RID: 980
		private float _dtCounter;

		// Token: 0x040003D5 RID: 981
		private float _movePauseDuration;

		// Token: 0x040003D6 RID: 982
		private float _movePauseTimer;

		// Token: 0x040003D7 RID: 983
		private float _moveSpeed;

		// Token: 0x040003D8 RID: 984
		private bool _moveTiming;

		// Token: 0x040003D9 RID: 985
		private bool _dragged;

		// Token: 0x040003DA RID: 986
		private bool _freePathToDestination;
	}
}
