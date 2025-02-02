using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000033 RID: 51
	public class ParallaxItemBrushWidget : BrushWidget
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000096FF File Offset: 0x000078FF
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00009707 File Offset: 0x00007907
		public bool IsEaseInOutEnabled { get; set; } = true;

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009710 File Offset: 0x00007910
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00009718 File Offset: 0x00007918
		public float OneDirectionDuration { get; set; } = 1f;

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009721 File Offset: 0x00007921
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00009729 File Offset: 0x00007929
		public float OneDirectionDistance { get; set; } = 1f;

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009732 File Offset: 0x00007932
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000973A File Offset: 0x0000793A
		public ParallaxItemBrushWidget.ParallaxMovementDirection InitialDirection { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009743 File Offset: 0x00007943
		private float _centerOffset
		{
			get
			{
				return this.OneDirectionDuration / 2f;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009751 File Offset: 0x00007951
		private float _localTime
		{
			get
			{
				return base.Context.EventManager.Time + this._centerOffset;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000976A File Offset: 0x0000796A
		public ParallaxItemBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009790 File Offset: 0x00007990
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				this.OneDirectionDuration = MathF.Max(float.Epsilon, this.OneDirectionDuration);
				this._initialized = true;
			}
			if (this.InitialDirection != ParallaxItemBrushWidget.ParallaxMovementDirection.None)
			{
				bool flag = this._localTime % (this.OneDirectionDuration * 4f) > this.OneDirectionDuration * 2f;
				float num2;
				if (this.IsEaseInOutEnabled)
				{
					float num = this._localTime % (this.OneDirectionDuration * 4f);
					float oneDirectionDuration = this.OneDirectionDuration;
					float t = MathF.PingPong(0f, this.OneDirectionDuration * 4f, this._localTime) / (this.OneDirectionDuration * 4f);
					float quadEaseInOut = this.GetQuadEaseInOut(t);
					num2 = MathF.Lerp(-this.OneDirectionDistance, this.OneDirectionDistance, quadEaseInOut, 1E-05f);
				}
				else
				{
					float num3 = MathF.PingPong(0f, this.OneDirectionDuration, this._localTime) / this.OneDirectionDuration;
					num2 = this.OneDirectionDistance * num3;
					num2 = (flag ? (-num2) : num2);
				}
				switch (this.InitialDirection)
				{
				case ParallaxItemBrushWidget.ParallaxMovementDirection.Left:
					base.PositionXOffset = num2;
					return;
				case ParallaxItemBrushWidget.ParallaxMovementDirection.Right:
					base.PositionXOffset = -num2;
					return;
				case ParallaxItemBrushWidget.ParallaxMovementDirection.Up:
					base.PositionYOffset = -num2;
					return;
				case ParallaxItemBrushWidget.ParallaxMovementDirection.Down:
					base.PositionYOffset = num2;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000098E4 File Offset: 0x00007AE4
		private float GetCubicEaseInOut(float t)
		{
			if (t < 0.5f)
			{
				return 4f * t * t * t;
			}
			float num = 2f * t - 2f;
			return 0.5f * num * num * num + 1f;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009924 File Offset: 0x00007B24
		private float GetElasticEaseInOut(float t)
		{
			if (t < 0.5f)
			{
				return (float)(0.5 * Math.Sin(20.420352248333657 * (double)(2f * t)) * Math.Pow(2.0, (double)(10f * (2f * t - 1f))));
			}
			return (float)(0.5 * (Math.Sin(-20.420352248333657 * (double)(2f * t - 1f + 1f)) * Math.Pow(2.0, (double)(-10f * (2f * t - 1f))) + 2.0));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000099DC File Offset: 0x00007BDC
		private float ExponentialEaseInOut(float t)
		{
			if (t == 0f || t == 1f)
			{
				return t;
			}
			if (t < 0.5f)
			{
				return (float)(0.5 * Math.Pow(2.0, (double)(20f * t - 10f)));
			}
			return (float)(-0.5 * Math.Pow(2.0, (double)(-20f * t + 10f)) + 1.0);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009A5C File Offset: 0x00007C5C
		private float GetQuadEaseInOut(float t)
		{
			if (t < 0.5f)
			{
				return 2f * t * t;
			}
			return -2f * t * t + 4f * t - 1f;
		}

		// Token: 0x04000131 RID: 305
		private bool _initialized;

		// Token: 0x02000192 RID: 402
		public enum ParallaxMovementDirection
		{
			// Token: 0x0400095C RID: 2396
			None,
			// Token: 0x0400095D RID: 2397
			Left,
			// Token: 0x0400095E RID: 2398
			Right,
			// Token: 0x0400095F RID: 2399
			Up,
			// Token: 0x04000960 RID: 2400
			Down
		}
	}
}
