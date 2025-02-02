using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000047 RID: 71
	public class MouseAnimStage
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000C384 File Offset: 0x0000A584
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000C38C File Offset: 0x0000A58C
		public bool IsCompleted { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000C395 File Offset: 0x0000A595
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000C39D File Offset: 0x0000A59D
		public float AnimTime { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		public Vec2 Direction { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		public MouseAnimStage.AnimTypes AnimType { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public Widget WidgetToManipulate { get; private set; }

		// Token: 0x060003CA RID: 970 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		private MouseAnimStage()
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		internal static MouseAnimStage CreateMovementStage(float movementTime, Vec2 direction, Widget widgetToManipulate)
		{
			return new MouseAnimStage
			{
				AnimTime = movementTime,
				Direction = direction,
				AnimType = MouseAnimStage.AnimTypes.Movement,
				WidgetToManipulate = widgetToManipulate
			};
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C404 File Offset: 0x0000A604
		internal static MouseAnimStage CreateFadeInStage(float fadeInTime, Widget widgetToManipulate, bool isGlobal)
		{
			return new MouseAnimStage
			{
				AnimTime = fadeInTime,
				AnimType = (isGlobal ? MouseAnimStage.AnimTypes.FadeInGlobal : MouseAnimStage.AnimTypes.FadeInLocal),
				WidgetToManipulate = widgetToManipulate
			};
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C426 File Offset: 0x0000A626
		internal static MouseAnimStage CreateFadeOutStage(float fadeOutTime, Widget widgetToManipulate, bool isGlobal)
		{
			return new MouseAnimStage
			{
				AnimTime = fadeOutTime,
				AnimType = (isGlobal ? MouseAnimStage.AnimTypes.FadeOutGlobal : MouseAnimStage.AnimTypes.FadeOutLocal),
				WidgetToManipulate = widgetToManipulate
			};
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000C448 File Offset: 0x0000A648
		internal static MouseAnimStage CreateStayStage(float stayTime)
		{
			return new MouseAnimStage
			{
				AnimTime = stayTime,
				AnimType = MouseAnimStage.AnimTypes.Stay,
				WidgetToManipulate = null
			};
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C464 File Offset: 0x0000A664
		public void Tick(float dt)
		{
			float num = MathF.Clamp(this._totalTime / this.AnimTime, 0f, 1f);
			switch (this.AnimType)
			{
			case MouseAnimStage.AnimTypes.Movement:
				this.WidgetToManipulate.PositionXOffset = ((this.Direction.X != 0f) ? MathF.Lerp(0f, this.Direction.X, num, 1E-05f) : 0f);
				this.WidgetToManipulate.PositionYOffset = ((this.Direction.Y != 0f) ? MathF.Lerp(0f, this.Direction.Y, num, 1E-05f) : 0f);
				this.IsCompleted = (this._totalTime > this.AnimTime);
				break;
			case MouseAnimStage.AnimTypes.FadeInLocal:
				this.WidgetToManipulate.AlphaFactor = num;
				this.IsCompleted = (this.WidgetToManipulate.AlphaFactor > 0.98f);
				break;
			case MouseAnimStage.AnimTypes.FadeOutLocal:
				this.WidgetToManipulate.AlphaFactor = 1f - num;
				this.IsCompleted = (this.WidgetToManipulate.AlphaFactor < 0.02f);
				break;
			case MouseAnimStage.AnimTypes.FadeInGlobal:
				this.WidgetToManipulate.SetGlobalAlphaRecursively(num);
				this.IsCompleted = (this.WidgetToManipulate.AlphaFactor > 0.98f);
				break;
			case MouseAnimStage.AnimTypes.FadeOutGlobal:
				this.WidgetToManipulate.SetGlobalAlphaRecursively(1f - num);
				this.IsCompleted = (this.WidgetToManipulate.AlphaFactor < 0.02f);
				break;
			case MouseAnimStage.AnimTypes.Stay:
				this.IsCompleted = (this._totalTime > this.AnimTime);
				break;
			}
			this._totalTime += dt;
		}

		// Token: 0x0400019E RID: 414
		private float _totalTime;

		// Token: 0x02000198 RID: 408
		public enum AnimTypes
		{
			// Token: 0x04000971 RID: 2417
			Movement,
			// Token: 0x04000972 RID: 2418
			FadeInLocal,
			// Token: 0x04000973 RID: 2419
			FadeOutLocal,
			// Token: 0x04000974 RID: 2420
			FadeInGlobal,
			// Token: 0x04000975 RID: 2421
			FadeOutGlobal,
			// Token: 0x04000976 RID: 2422
			Stay
		}
	}
}
