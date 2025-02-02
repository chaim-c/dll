﻿using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000023 RID: 35
	public class LauncherHintWidget : Widget
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006005 File Offset: 0x00004205
		private float TooltipOffset
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000600C File Offset: 0x0000420C
		public LauncherHintWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006015 File Offset: 0x00004215
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateAlpha(dt);
			if (this.IsActive)
			{
				if (!this._prevIsActive)
				{
					this._frame = 0;
				}
				this.UpdatePosition();
			}
			this._prevIsActive = this.IsActive;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006050 File Offset: 0x00004250
		private void UpdatePosition()
		{
			Vector2 vector;
			if (this._frame < 3)
			{
				this._tooltipPosition = base.EventManager.MousePosition;
				vector = new Vector2(-2000f, -2000f);
			}
			else
			{
				vector = this._tooltipPosition;
			}
			this._frame++;
			float num = 0f;
			HorizontalAlignment horizontalAlignment;
			if ((double)vector.X < (double)base.EventManager.PageSize.X * 0.5)
			{
				horizontalAlignment = HorizontalAlignment.Left;
				num = this.TooltipOffset;
			}
			else
			{
				horizontalAlignment = HorizontalAlignment.Right;
				num -= 0f;
				vector = new Vector2(-(base.EventManager.PageSize.X - vector.X), vector.Y);
			}
			VerticalAlignment verticalAlignment;
			float y;
			if ((double)vector.Y < (double)base.EventManager.PageSize.Y * 0.5)
			{
				verticalAlignment = VerticalAlignment.Top;
				y = this.TooltipOffset;
			}
			else
			{
				verticalAlignment = VerticalAlignment.Bottom;
				y = 0f;
				vector = new Vector2(vector.X, -(base.EventManager.PageSize.Y - vector.Y));
			}
			vector += new Vector2(num, y);
			if (this._frame > 3)
			{
				if (base.Size.Y > base.EventManager.PageSize.Y)
				{
					verticalAlignment = VerticalAlignment.Center;
					vector = new Vector2(vector.X, 0f);
				}
				else
				{
					if (verticalAlignment == VerticalAlignment.Top && vector.Y + base.Size.Y > base.EventManager.PageSize.Y)
					{
						vector += new Vector2(0f, -(vector.Y + base.Size.Y - base.EventManager.PageSize.Y));
					}
					if (verticalAlignment == VerticalAlignment.Bottom && vector.Y - base.Size.Y + base.EventManager.PageSize.Y < 0f)
					{
						vector += new Vector2(0f, -(vector.Y - base.Size.Y + base.EventManager.PageSize.Y));
					}
				}
			}
			base.HorizontalAlignment = horizontalAlignment;
			base.VerticalAlignment = verticalAlignment;
			base.ScaledPositionXOffset = vector.X;
			base.ScaledPositionYOffset = vector.Y;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000062A4 File Offset: 0x000044A4
		private void UpdateAlpha(float dt)
		{
			float valueTo;
			if (this.IsActive)
			{
				valueTo = 1f;
			}
			else
			{
				valueTo = 0f;
			}
			float alphaFactor = MathF.Lerp(base.AlphaFactor, valueTo, dt * 20f, 1E-05f);
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000062E7 File Offset: 0x000044E7
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000062EF File Offset: 0x000044EF
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
				}
			}
		}

		// Token: 0x040000A9 RID: 169
		private int _frame;

		// Token: 0x040000AA RID: 170
		private bool _prevIsActive;

		// Token: 0x040000AB RID: 171
		private Vector2 _tooltipPosition;

		// Token: 0x040000AC RID: 172
		private bool _isActive;
	}
}
