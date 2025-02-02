using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200000E RID: 14
	public class CircularAutoScrollablePanelWidget : Widget
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000036D8 File Offset: 0x000018D8
		public CircularAutoScrollablePanelWidget(UIContext context) : base(context)
		{
			this.IdleTime = 0.8f;
			this.ScrollRatioPerSecond = 0.25f;
			this.ScrollPixelsPerSecond = 35f;
			this.ScrollType = CircularAutoScrollablePanelWidget.ScrollMovementType.ByPixels;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003718 File Offset: 0x00001918
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this._autoScroll = (this._autoScroll || (base.CurrentState == "Selected" && this.AutoScrollWhenSelected));
			this._maxScroll = 0f;
			Widget innerPanel = this.InnerPanel;
			if (innerPanel != null && innerPanel.Size.Y > 0f)
			{
				Widget clipRect = this.ClipRect;
				if (clipRect != null && clipRect.Size.Y > 0f && this.InnerPanel.Size.Y > this.ClipRect.Size.Y)
				{
					this._maxScroll = this.InnerPanel.Size.Y - this.ClipRect.Size.Y;
				}
			}
			if (this._autoScroll && !this._isIdle)
			{
				this.ScrollToDirection(this._direction, dt);
				if (this._currentScrollValue.ApproximatelyEqualsTo(0f, 1E-05f) || this._currentScrollValue.ApproximatelyEqualsTo(this._maxScroll, 1E-05f))
				{
					this._isIdle = true;
					this._idleTimer = 0f;
					this._direction *= -1;
					return;
				}
			}
			else if (this._autoScroll && this._isIdle)
			{
				this._idleTimer += dt;
				if (this._idleTimer > this.IdleTime)
				{
					this._isIdle = false;
					this._idleTimer = 0f;
					return;
				}
			}
			else if (this._currentScrollValue > 0f)
			{
				this.ScrollToDirection(-1, dt);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000038B0 File Offset: 0x00001AB0
		private void ScrollToDirection(int direction, float dt)
		{
			float num = 0f;
			if (this.ScrollType == CircularAutoScrollablePanelWidget.ScrollMovementType.ByPixels)
			{
				num = this.ScrollPixelsPerSecond;
			}
			else if (this.ScrollType == CircularAutoScrollablePanelWidget.ScrollMovementType.ByRatio)
			{
				num = this.ScrollRatioPerSecond * this._maxScroll;
			}
			this._currentScrollValue += num * (float)direction * dt;
			this._currentScrollValue = MathF.Clamp(this._currentScrollValue, 0f, this._maxScroll);
			this.InnerPanel.ScaledPositionYOffset = -this._currentScrollValue;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000392C File Offset: 0x00001B2C
		protected override void OnMouseScroll()
		{
			base.OnMouseScroll();
			this._autoScroll = false;
			float num = (this.ScrollPixelsPerSecond != 0f) ? (this.ScrollPixelsPerSecond * 0.2f) : 10f;
			float num2 = base.EventManager.DeltaMouseScroll * num;
			this._currentScrollValue += num2;
			this.InnerPanel.ScaledPositionYOffset = -this._currentScrollValue;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003995 File Offset: 0x00001B95
		public void SetScrollMouse()
		{
			this.OnMouseScroll();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000399D File Offset: 0x00001B9D
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			if (!this._autoScroll)
			{
				this._autoScroll = true;
				this._isIdle = false;
				this._direction = 1;
				this._idleTimer = 0f;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000039CD File Offset: 0x00001BCD
		public void SetHoverBegin()
		{
			this.OnHoverBegin();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039D8 File Offset: 0x00001BD8
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			if (this._autoScroll)
			{
				this._autoScroll = false;
				this._direction = -1;
				if (this._isIdle && this._currentScrollValue < 1E-45f)
				{
					this._currentScrollValue = 1f;
				}
				if (this.ShouldResetImmediately)
				{
					this._currentScrollValue = 0f;
					this.InnerPanel.ScaledPositionYOffset = 0f;
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A44 File Offset: 0x00001C44
		public void SetHoverEnd()
		{
			this.OnHoverEnd();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003A4C File Offset: 0x00001C4C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003A54 File Offset: 0x00001C54
		public Widget InnerPanel { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003A5D File Offset: 0x00001C5D
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003A65 File Offset: 0x00001C65
		public Widget ClipRect { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003A6E File Offset: 0x00001C6E
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003A76 File Offset: 0x00001C76
		public float ScrollRatioPerSecond { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003A7F File Offset: 0x00001C7F
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003A87 File Offset: 0x00001C87
		public float ScrollPixelsPerSecond { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003A90 File Offset: 0x00001C90
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003A98 File Offset: 0x00001C98
		public float IdleTime { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003AA1 File Offset: 0x00001CA1
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003AA9 File Offset: 0x00001CA9
		public bool AutoScrollWhenSelected { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003AB2 File Offset: 0x00001CB2
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003ABA File Offset: 0x00001CBA
		public CircularAutoScrollablePanelWidget.ScrollMovementType ScrollType { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003AC3 File Offset: 0x00001CC3
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003ACB File Offset: 0x00001CCB
		public bool ShouldResetImmediately
		{
			get
			{
				return this._shouldResetImmediately;
			}
			set
			{
				if (value != this._shouldResetImmediately)
				{
					this._shouldResetImmediately = value;
				}
			}
		}

		// Token: 0x04000045 RID: 69
		private float _currentScrollValue;

		// Token: 0x04000046 RID: 70
		private bool _autoScroll;

		// Token: 0x04000047 RID: 71
		private bool _isIdle;

		// Token: 0x04000048 RID: 72
		private float _idleTimer;

		// Token: 0x04000049 RID: 73
		private int _direction = 1;

		// Token: 0x0400004A RID: 74
		private float _maxScroll;

		// Token: 0x0400004B RID: 75
		private bool _shouldResetImmediately = true;

		// Token: 0x0200018C RID: 396
		public enum ScrollMovementType
		{
			// Token: 0x04000945 RID: 2373
			ByPixels,
			// Token: 0x04000946 RID: 2374
			ByRatio
		}
	}
}
