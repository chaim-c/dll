using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000BE RID: 190
	public class MoraleArrowBrushWidget : BrushWidget
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001C34C File Offset: 0x0001A54C
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0001C354 File Offset: 0x0001A554
		public bool LeftSideArrow { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001C35D File Offset: 0x0001A55D
		public float BaseHorizontalExtendRange
		{
			get
			{
				return 3.3f;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001C364 File Offset: 0x0001A564
		private float BaseSpeedModifier
		{
			get
			{
				return 13f;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0001C36B File Offset: 0x0001A56B
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0001C373 File Offset: 0x0001A573
		public bool AreMoralesIndependent { get; set; }

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001C37C File Offset: 0x0001A57C
		public MoraleArrowBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001C388 File Offset: 0x0001A588
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				base.Brush.GlobalAlphaFactor = 0f;
				this._initialized = true;
			}
			base.IsVisible = (this._currentFlow > 0 && !this.AreMoralesIndependent);
			if (base.IsVisible)
			{
				float num = this.BaseSpeedModifier * (float)Math.Sqrt((double)this._currentFlow);
				float num2 = this.BaseHorizontalExtendRange * (float)this._currentFlow;
				if (this._currentAnimState == MoraleArrowBrushWidget.AnimStates.FadeIn)
				{
					if (base.ReadOnlyBrush.GlobalAlphaFactor < 1f)
					{
						this.SetGlobalAlphaRecursively(Mathf.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, 1f, dt * num));
					}
					if ((double)base.ReadOnlyBrush.GlobalAlphaFactor >= 0.99)
					{
						this._currentAnimState = MoraleArrowBrushWidget.AnimStates.Move;
					}
				}
				else if (this._currentAnimState == MoraleArrowBrushWidget.AnimStates.Move)
				{
					if (Math.Abs(base.PositionXOffset) < num2)
					{
						int num3 = this.LeftSideArrow ? -1 : 1;
						base.PositionXOffset = Mathf.Lerp(base.PositionXOffset, num2 * (float)num3, dt * num);
					}
					if ((double)Math.Abs(base.PositionXOffset) >= (double)num2 - 0.01)
					{
						this._currentAnimState = MoraleArrowBrushWidget.AnimStates.FadeOut;
					}
				}
				else if (this._currentAnimState == MoraleArrowBrushWidget.AnimStates.FadeOut)
				{
					if (base.ReadOnlyBrush.GlobalAlphaFactor > 0f)
					{
						this.SetGlobalAlphaRecursively(Mathf.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, 0f, dt * num));
					}
					if ((double)base.ReadOnlyBrush.GlobalAlphaFactor <= 0.01)
					{
						this._currentAnimState = MoraleArrowBrushWidget.AnimStates.GoToInitPos;
					}
				}
				else
				{
					base.PositionXOffset = 0f;
					this._currentAnimState = MoraleArrowBrushWidget.AnimStates.FadeIn;
				}
			}
			else
			{
				base.PositionXOffset = 0f;
				this._currentAnimState = MoraleArrowBrushWidget.AnimStates.FadeIn;
			}
			this._timeSinceCreation += dt;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001C552 File Offset: 0x0001A752
		public void SetFlowLevel(int flow)
		{
			this._currentFlow = flow;
			base.IsVisible = (this._currentFlow > 0 && !this.AreMoralesIndependent);
		}

		// Token: 0x04000487 RID: 1159
		private float _timeSinceCreation;

		// Token: 0x04000488 RID: 1160
		private bool _initialized;

		// Token: 0x04000489 RID: 1161
		private int _currentFlow;

		// Token: 0x0400048C RID: 1164
		private MoraleArrowBrushWidget.AnimStates _currentAnimState;

		// Token: 0x020001A8 RID: 424
		private enum AnimStates
		{
			// Token: 0x0400099F RID: 2463
			FadeIn,
			// Token: 0x040009A0 RID: 2464
			Move,
			// Token: 0x040009A1 RID: 2465
			FadeOut,
			// Token: 0x040009A2 RID: 2466
			GoToInitPos
		}
	}
}
