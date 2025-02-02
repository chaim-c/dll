using System;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001C RID: 28
	public class CircleItemPlacerWidget : Widget
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B082 File Offset: 0x00009282
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000B08A File Offset: 0x0000928A
		public float DistanceFromCenterModifier { get; set; } = 300f;

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000B093 File Offset: 0x00009293
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000B09B File Offset: 0x0000929B
		public Widget DirectionWidget { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000B0A4 File Offset: 0x000092A4
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000B0AC File Offset: 0x000092AC
		public float DirectionWidgetDistanceMultiplier { get; set; } = 0.5f;

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000B0B5 File Offset: 0x000092B5
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000B0BD File Offset: 0x000092BD
		public bool ActivateOnlyWithController { get; set; }

		// Token: 0x06000225 RID: 549 RVA: 0x0000B0C6 File Offset: 0x000092C6
		public CircleItemPlacerWidget(UIContext context) : base(context)
		{
			this._centerDistanceAnimationTimer = -1f;
			this._centerDistanceAnimationDuration = -1f;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B0FB File Offset: 0x000092FB
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsRecursivelyVisible())
			{
				this.UpdateItemPlacement();
				this.AnimateDistanceFromCenter(dt);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B11C File Offset: 0x0000931C
		private void AnimateDistanceFromCenter(float dt)
		{
			if (this._centerDistanceAnimationTimer == -1f || this._centerDistanceAnimationDuration == -1f || this._centerDistanceAnimationTarget == -1f)
			{
				return;
			}
			if (this._centerDistanceAnimationTimer < this._centerDistanceAnimationDuration)
			{
				this.DistanceFromCenterModifier = MathF.Lerp(this._centerDistanceAnimationInitialValue, this._centerDistanceAnimationTarget, this._centerDistanceAnimationTimer / this._centerDistanceAnimationDuration, 1E-05f);
				this._centerDistanceAnimationTimer += dt;
				return;
			}
			this.DistanceFromCenterModifier = this._centerDistanceAnimationTarget;
			this._centerDistanceAnimationTimer = -1f;
			this._centerDistanceAnimationDuration = -1f;
			this._centerDistanceAnimationTarget = -1f;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B1C4 File Offset: 0x000093C4
		public void AnimateDistanceFromCenterTo(float distanceFromCenter, float animationDuration)
		{
			this._centerDistanceAnimationTimer = 0f;
			this._centerDistanceAnimationInitialValue = this.DistanceFromCenterModifier;
			this._centerDistanceAnimationDuration = animationDuration;
			this._centerDistanceAnimationTarget = distanceFromCenter;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B1EC File Offset: 0x000093EC
		private void UpdateItemPlacement()
		{
			if (base.ChildCount > 0)
			{
				int childCount = base.ChildCount;
				float num = 360f / (float)childCount;
				float num2 = -(num / 2f);
				if (num2 < 0f)
				{
					num2 += 360f;
				}
				for (int i = 0; i < base.ChildCount; i++)
				{
					float angle = num * (float)i;
					float num3 = this.AddAngle(num2, angle);
					num3 = this.AddAngle(num3, num / 2f);
					Vec2 vec = this.DirFromAngle(num3 * 0.017453292f);
					Widget child = base.GetChild(i);
					child.PositionXOffset = vec.X * this.DistanceFromCenterModifier;
					child.PositionYOffset = vec.Y * this.DistanceFromCenterModifier * -1f;
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B2A8 File Offset: 0x000094A8
		private float AddAngle(float angle1, float angle2)
		{
			float num = angle1 + angle2;
			if (num < 0f)
			{
				num += 360f;
			}
			return num % 360f;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B2D4 File Offset: 0x000094D4
		private bool IsAngleBetweenAngles(float angle, float minAngle, float maxAngle)
		{
			float num = angle - 3.1415927f;
			float num2 = minAngle - 3.1415927f;
			float num3 = maxAngle - 3.1415927f;
			if (num2 == num3)
			{
				return true;
			}
			float num4 = MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(num3, num2));
			if (num4.ApproximatelyEqualsTo(3.1415927f, 1E-05f))
			{
				return num < num3;
			}
			float num5 = MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(num, num2));
			float num6 = MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(num, num3));
			return num5 < num4 && num6 < num4;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B34C File Offset: 0x0000954C
		private float AngleFromDir(Vec2 directionVector)
		{
			if (directionVector.X < 0f)
			{
				return 360f - (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f * -1f;
			}
			return (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B3AC File Offset: 0x000095AC
		private Vec2 DirFromAngle(float angle)
		{
			return new Vec2(MathF.Sin(angle), MathF.Cos(angle));
		}

		// Token: 0x0400010F RID: 271
		private float _centerDistanceAnimationTimer;

		// Token: 0x04000110 RID: 272
		private float _centerDistanceAnimationDuration;

		// Token: 0x04000111 RID: 273
		private float _centerDistanceAnimationInitialValue;

		// Token: 0x04000112 RID: 274
		private float _centerDistanceAnimationTarget;
	}
}
