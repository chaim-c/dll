using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map
{
	// Token: 0x0200010A RID: 266
	public class MobilePartyTrackerItemWidget : Widget
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00026B90 File Offset: 0x00024D90
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00026B98 File Offset: 0x00024D98
		public Widget FrameVisualWidget { get; set; }

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00026BA1 File Offset: 0x00024DA1
		public MobilePartyTrackerItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00026BAC File Offset: 0x00024DAC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.FrameVisualWidget.Sprite = base.Context.SpriteData.GetSprite(this.IsArmy ? "army_track_frame_9" : "party_track_frame_9");
				this._initialized = true;
			}
			this.UpdateScreenPosition();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00026C04 File Offset: 0x00024E04
		private void UpdateScreenPosition()
		{
			this._screenWidth = base.Context.EventManager.PageSize.X;
			this._screenHeight = base.Context.EventManager.PageSize.Y;
			if (!this.IsActive)
			{
				base.IsHidden = true;
				return;
			}
			Vec2 vec = new Vec2(this.Position);
			if (this.IsTracked)
			{
				if (!this.IsBehind && vec.X - base.Size.X / 2f > 0f && vec.x + base.Size.X / 2f < base.Context.EventManager.PageSize.X && vec.y > 0f && vec.y + base.Size.Y < base.Context.EventManager.PageSize.Y)
				{
					base.ScaledPositionXOffset = vec.x - base.Size.X / 2f;
					base.ScaledPositionYOffset = vec.y;
				}
				else
				{
					Vec2 vec2 = new Vec2(base.Context.EventManager.PageSize.X / 2f, base.Context.EventManager.PageSize.Y / 2f);
					vec -= vec2;
					if (this.IsBehind)
					{
						vec *= -1f;
					}
					float radian = Mathf.Atan2(vec.y, vec.x) - 1.5707964f;
					float num = Mathf.Cos(radian);
					float num2 = Mathf.Sin(radian);
					float num3 = num / num2;
					Vec2 vec3 = vec2 * 1f;
					vec = ((num > 0f) ? new Vec2(-vec3.y / num3, vec2.y) : new Vec2(vec3.y / num3, -vec2.y));
					if (vec.x > vec3.x)
					{
						vec = new Vec2(vec3.x, -vec3.x * num3);
					}
					else if (vec.x < -vec3.x)
					{
						vec = new Vec2(-vec3.x, vec3.x * num3);
					}
					vec += vec2;
					base.ScaledPositionXOffset = Mathf.Clamp(vec.x - base.Size.X / 2f, 0f, this._screenWidth - base.Size.X);
					base.ScaledPositionYOffset = Mathf.Clamp(vec.y, 0f, this._screenHeight - (base.Size.Y + 55f));
				}
			}
			else
			{
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
				base.ScaledPositionYOffset = this.Position.y;
			}
			base.IsHidden = ((!this.IsTracked && this.IsBehind) || base.ScaledPositionXOffset > base.Context.TwoDimensionContext.Width || base.ScaledPositionYOffset > base.Context.TwoDimensionContext.Height || base.ScaledPositionXOffset + base.Size.X < 0f || base.ScaledPositionYOffset + base.Size.Y < 0f);
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00026F6B File Offset: 0x0002516B
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00026F73 File Offset: 0x00025173
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00026F96 File Offset: 0x00025196
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x00026F9E File Offset: 0x0002519E
		public bool ShouldShowFullName
		{
			get
			{
				return this._shouldShowFullName;
			}
			set
			{
				if (this._shouldShowFullName != value)
				{
					this._shouldShowFullName = value;
					base.OnPropertyChanged(value, "ShouldShowFullName");
				}
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00026FBC File Offset: 0x000251BC
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00026FC4 File Offset: 0x000251C4
		public bool IsArmy
		{
			get
			{
				return this._isArmy;
			}
			set
			{
				if (this._isArmy != value)
				{
					this._isArmy = value;
					base.OnPropertyChanged(value, "IsArmy");
					this._initialized = false;
				}
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00026FE9 File Offset: 0x000251E9
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00026FF1 File Offset: 0x000251F1
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive != value)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
				}
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0002700F File Offset: 0x0002520F
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00027017 File Offset: 0x00025217
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (this._isBehind != value)
				{
					this._isBehind = value;
					base.OnPropertyChanged(value, "IsBehind");
				}
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00027035 File Offset: 0x00025235
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0002703D File Offset: 0x0002523D
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (this._isTracked != value)
				{
					this._isTracked = value;
					base.OnPropertyChanged(value, "IsTracked");
				}
			}
		}

		// Token: 0x04000672 RID: 1650
		private float _screenWidth;

		// Token: 0x04000673 RID: 1651
		private float _screenHeight;

		// Token: 0x04000674 RID: 1652
		private bool _initialized;

		// Token: 0x04000675 RID: 1653
		private Vec2 _position;

		// Token: 0x04000676 RID: 1654
		private bool _isActive;

		// Token: 0x04000677 RID: 1655
		private bool _isBehind;

		// Token: 0x04000678 RID: 1656
		private bool _isArmy;

		// Token: 0x04000679 RID: 1657
		private bool _isTracked;

		// Token: 0x0400067A RID: 1658
		private bool _shouldShowFullName;
	}
}
