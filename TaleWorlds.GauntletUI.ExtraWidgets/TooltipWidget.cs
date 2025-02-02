using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x02000012 RID: 18
	public class TooltipWidget : Widget
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000062AF File Offset: 0x000044AF
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000062B7 File Offset: 0x000044B7
		public TooltipPositioningType PositioningType { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000062C0 File Offset: 0x000044C0
		private float _tooltipOffset
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000062C7 File Offset: 0x000044C7
		public TooltipWidget(UIContext context) : base(context)
		{
			base.HorizontalAlignment = HorizontalAlignment.Left;
			base.VerticalAlignment = VerticalAlignment.Top;
			this._lastCheckedVisibility = true;
			base.IsVisible = true;
			this.PositioningType = TooltipPositioningType.FixedMouseMirrored;
			this.ResetAnimationProperties();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006304 File Offset: 0x00004504
		protected override void RefreshState()
		{
			base.RefreshState();
			if (this._lastCheckedVisibility != base.IsVisible)
			{
				this._lastCheckedVisibility = base.IsVisible;
				if (base.IsVisible)
				{
					this.ResetAnimationProperties();
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006334 File Offset: 0x00004534
		protected override void OnLateUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._animationState == TooltipWidget.AnimationState.NotStarted)
			{
				if (this._animationDelayTimerInFrames >= this._animationDelayInFrames)
				{
					this._animationState = TooltipWidget.AnimationState.InProgress;
				}
				else
				{
					this._animationDelayTimerInFrames++;
					this.SetAlpha(0f);
				}
			}
			if (this._animationState != TooltipWidget.AnimationState.NotStarted)
			{
				if (this._animationState == TooltipWidget.AnimationState.InProgress)
				{
					this._animationProgress += ((this.AnimTime < 1E-05f) ? 1f : (dt / this.AnimTime));
					this.SetAlpha(this._animationProgress);
					if (this._animationProgress >= 1f)
					{
						this._animationState = TooltipWidget.AnimationState.Finished;
					}
				}
				this.UpdatePosition();
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000063E0 File Offset: 0x000045E0
		private void SetAlpha(float alpha)
		{
			base.AlphaFactor = alpha;
			foreach (Widget widget in base.Children)
			{
				widget.UpdateAnimationPropertiesSubTask(base.AlphaFactor);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006440 File Offset: 0x00004640
		private void UpdatePosition()
		{
			if (this.PositioningType == TooltipPositioningType.FixedMouse || this.PositioningType == TooltipPositioningType.FixedMouseMirrored)
			{
				if (MathF.Abs(this._lastCheckedSize.X - base.Size.X) > 0.1f || MathF.Abs(this._lastCheckedSize.Y - base.Size.Y) > 0.1f)
				{
					this._lastCheckedSize = base.Size;
					if (this.PositioningType == TooltipPositioningType.FixedMouse)
					{
						this.SetPosition(base.EventManager.MousePosition);
						return;
					}
					this.SetMirroredPosition(base.EventManager.MousePosition);
					return;
				}
			}
			else
			{
				if (this.PositioningType == TooltipPositioningType.FollowMouse)
				{
					this.SetPosition(base.EventManager.MousePosition);
					return;
				}
				if (this.PositioningType == TooltipPositioningType.FollowMouseMirrored)
				{
					this.SetMirroredPosition(base.EventManager.MousePosition);
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000651C File Offset: 0x0000471C
		private void SetPosition(Vector2 position)
		{
			Vector2 vector = position + new Vector2(this._tooltipOffset, this._tooltipOffset);
			bool flag = base.Size.X > base.EventManager.PageSize.X;
			bool flag2 = base.Size.Y > base.EventManager.PageSize.Y;
			base.ScaledPositionXOffset = (flag ? vector.X : MathF.Clamp(vector.X, 0f, base.EventManager.PageSize.X - base.Size.X));
			base.ScaledPositionYOffset = (flag2 ? vector.Y : MathF.Clamp(vector.Y, 0f, base.EventManager.PageSize.Y - base.Size.Y));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000065F8 File Offset: 0x000047F8
		private void SetMirroredPosition(Vector2 tooltipPosition)
		{
			float x = 0f;
			float y = 0f;
			HorizontalAlignment horizontalAlignment;
			if ((double)tooltipPosition.X < (double)base.EventManager.PageSize.X * 0.5)
			{
				horizontalAlignment = HorizontalAlignment.Left;
				x = this._tooltipOffset;
			}
			else
			{
				horizontalAlignment = HorizontalAlignment.Right;
				tooltipPosition = new Vector2(-(base.EventManager.PageSize.X - tooltipPosition.X), tooltipPosition.Y);
			}
			VerticalAlignment verticalAlignment;
			if ((double)tooltipPosition.Y < (double)base.EventManager.PageSize.Y * 0.5)
			{
				verticalAlignment = VerticalAlignment.Top;
				y = this._tooltipOffset;
			}
			else
			{
				verticalAlignment = VerticalAlignment.Bottom;
				tooltipPosition = new Vector2(tooltipPosition.X, -(base.EventManager.PageSize.Y - tooltipPosition.Y));
			}
			tooltipPosition += new Vector2(x, y);
			if (base.Size.X > base.EventManager.PageSize.X)
			{
				horizontalAlignment = HorizontalAlignment.Left;
				tooltipPosition = new Vector2(0f, tooltipPosition.Y);
			}
			else
			{
				if (horizontalAlignment == HorizontalAlignment.Left && tooltipPosition.X + base.Size.X > base.EventManager.PageSize.X)
				{
					tooltipPosition += new Vector2(-(tooltipPosition.X + base.Size.X - base.EventManager.PageSize.X), 0f);
				}
				if (horizontalAlignment == HorizontalAlignment.Right && tooltipPosition.X - base.Size.X + base.EventManager.PageSize.X < 0f)
				{
					tooltipPosition += new Vector2(-(tooltipPosition.X - base.Size.X + base.EventManager.PageSize.X), 0f);
				}
			}
			if (base.Size.Y > base.EventManager.PageSize.Y)
			{
				verticalAlignment = VerticalAlignment.Top;
				tooltipPosition = new Vector2(tooltipPosition.X, 0f);
			}
			else
			{
				if (verticalAlignment == VerticalAlignment.Top && tooltipPosition.Y + base.Size.Y > base.EventManager.PageSize.Y)
				{
					tooltipPosition += new Vector2(0f, -(tooltipPosition.Y + base.Size.Y - base.EventManager.PageSize.Y));
				}
				if (verticalAlignment == VerticalAlignment.Bottom && tooltipPosition.Y - base.Size.Y + base.EventManager.PageSize.Y < 0f)
				{
					tooltipPosition += new Vector2(0f, -(tooltipPosition.Y - base.Size.Y + base.EventManager.PageSize.Y));
				}
			}
			base.HorizontalAlignment = horizontalAlignment;
			base.VerticalAlignment = verticalAlignment;
			base.ScaledPositionXOffset = tooltipPosition.X - base.EventManager.LeftUsableAreaStart;
			base.ScaledPositionYOffset = tooltipPosition.Y - base.EventManager.TopUsableAreaStart;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000068FC File Offset: 0x00004AFC
		private void ResetAnimationProperties()
		{
			this._animationState = TooltipWidget.AnimationState.NotStarted;
			this._animationProgress = 0f;
			this._animationDelayTimerInFrames = 0;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006917 File Offset: 0x00004B17
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000691F File Offset: 0x00004B1F
		[Editor(false)]
		public float AnimTime
		{
			get
			{
				return this._animTime;
			}
			set
			{
				if (this._animTime != value)
				{
					this._animTime = value;
					base.OnPropertyChanged(value, "AnimTime");
				}
			}
		}

		// Token: 0x0400007D RID: 125
		protected int _animationDelayInFrames;

		// Token: 0x0400007E RID: 126
		private int _animationDelayTimerInFrames;

		// Token: 0x0400007F RID: 127
		private TooltipWidget.AnimationState _animationState;

		// Token: 0x04000080 RID: 128
		private float _animationProgress;

		// Token: 0x04000081 RID: 129
		private bool _lastCheckedVisibility;

		// Token: 0x04000082 RID: 130
		private Vector2 _lastCheckedSize;

		// Token: 0x04000083 RID: 131
		private float _animTime = 0.2f;

		// Token: 0x0200001B RID: 27
		private enum AnimationState
		{
			// Token: 0x040000B7 RID: 183
			NotStarted,
			// Token: 0x040000B8 RID: 184
			InProgress,
			// Token: 0x040000B9 RID: 185
			Finished
		}
	}
}
