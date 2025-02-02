using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000021 RID: 33
	public class GamepadCursorWidget : BrushWidget
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006779 File Offset: 0x00004979
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00006781 File Offset: 0x00004981
		private protected float TransitionTimer { protected get; private set; }

		// Token: 0x0600019C RID: 412 RVA: 0x0000678A File Offset: 0x0000498A
		public GamepadCursorWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006794 File Offset: 0x00004994
		protected override void OnLateUpdate(float dt)
		{
			if (base.IsVisible)
			{
				this.RefreshTarget();
				bool flag = Input.IsKeyDown(InputKey.ControllerRDown);
				if (flag != this._isPressing)
				{
					this._animationRatioTimer = 0f;
					this.TransitionTimer = 0f;
					this._additionalOffsetBeforeStateChange = this._additionalOffset;
				}
				this._isPressing = flag;
				if (this._animationRatioTimer < 1.4f)
				{
					this._animationRatioTimer = MathF.Min(this._animationRatioTimer + dt, 1.4f);
				}
				bool flag2 = !this._targetChangedThisFrame && this._targetPositionChangedThisFrame;
				this._animationRatio = ((this.HasTarget && !this._isPressing) ? MathF.Clamp(17f * dt, 0f, 1f) : MathF.Lerp(this._animationRatio, 1f, this._animationRatioTimer / 1.4f, 1E-05f));
				this.UpdateAdditionalOffsets();
				this.UpdateTargetOffsets(flag2 ? 1f : this._animationRatio);
				if (!flag2)
				{
					this.TransitionTimer += dt;
				}
			}
			this._targetChangedThisFrame = false;
			this._targetPositionChangedThisFrame = false;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000068B0 File Offset: 0x00004AB0
		private void RefreshTarget()
		{
			GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
			Widget widget = (instance != null) ? instance.LastTargetedWidget : null;
			this._targetChangedThisFrame = (this._targetWidget != widget);
			this._targetWidget = widget;
			this.TargetHasAction = GauntletGamepadNavigationManager.Instance.TargetedWidgetHasAction;
			this.HasTarget = (this._targetWidget != null);
			this.CursorParentWidget.HasTarget = this.HasTarget;
			if (this.HasTarget)
			{
				Vector2 globalPosition = this._targetWidget.GlobalPosition;
				Vector2 size = this._targetWidget.Size;
				float num = this._targetWidget.DoNotUseCustomScale ? this._targetWidget.EventManager.Context.Scale : this._targetWidget.EventManager.Context.CustomScale;
				float num2 = this._targetWidget.ExtendCursorAreaLeft * num;
				float num3 = this._targetWidget.ExtendCursorAreaTop * num;
				float num4 = this._targetWidget.ExtendCursorAreaRight * num;
				float num5 = this._targetWidget.ExtendCursorAreaBottom * num;
				this.TargetX = globalPosition.X - num2;
				this.TargetY = globalPosition.Y - num3;
				this.TargetWidth = size.X + num2 + num4;
				this.TargetHeight = size.Y + num3 + num5;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000069F4 File Offset: 0x00004BF4
		private void UpdateTargetOffsets(float ratio)
		{
			Vector2 vector = new Vector2(base.EventManager.LeftUsableAreaStart, base.EventManager.TopUsableAreaStart);
			float num;
			float num2;
			float num3;
			float num4;
			if (this.HasTarget)
			{
				num = this.TargetX - vector.X;
				num2 = this.TargetY - vector.Y;
				num3 = this.TargetWidth;
				num4 = this.TargetHeight;
			}
			else
			{
				num = this.CursorParentWidget.XOffset - (float)MathF.Floor(this.DefaultTargetlessOffset / 2f);
				num2 = this.CursorParentWidget.YOffset - (float)MathF.Floor(this.DefaultTargetlessOffset / 2f);
				num3 = this.DefaultTargetlessOffset;
				num4 = this.DefaultTargetlessOffset;
			}
			num -= this._additionalOffset;
			num2 -= this._additionalOffset;
			float num5 = 45f * base._scaleToUse;
			if (num3 < num5)
			{
				float num6 = num5;
				num += (num3 - num6) / 2f;
				num3 = num6;
			}
			if (num4 < num5)
			{
				float num7 = num5;
				num2 += (num4 - num7) / 2f;
				num4 = num7;
			}
			num3 += this._additionalOffset * 2f;
			num4 += this._additionalOffset * 2f;
			base.ScaledSuggestedWidth = MathF.Lerp(base.ScaledSuggestedWidth, num3, ratio, 1E-05f);
			base.ScaledSuggestedHeight = MathF.Lerp(base.ScaledSuggestedHeight, num4, ratio, 1E-05f);
			if (GauntletGamepadNavigationManager.Instance.IsCursorMovingForNavigation)
			{
				Vector2 vector2 = this.CursorParentWidget.CenterWidget.GlobalPosition + this.CursorParentWidget.CenterWidget.Size / 2f;
				base.ScaledPositionXOffset = vector2.X - base.ScaledSuggestedWidth / 2f - vector.X;
				base.ScaledPositionYOffset = vector2.Y - base.ScaledSuggestedHeight / 2f - vector.Y;
			}
			else
			{
				base.ScaledPositionXOffset = MathF.Lerp(base.ScaledPositionXOffset, num, ratio, 1E-05f);
				base.ScaledPositionYOffset = MathF.Lerp(base.ScaledPositionYOffset, num2, ratio, 1E-05f);
			}
			base.ScaledPositionXOffset = MathF.Clamp(base.ScaledPositionXOffset, 0f, Input.Resolution.X - num3 - vector.X * 2f);
			base.ScaledPositionYOffset = MathF.Clamp(base.ScaledPositionYOffset, 0f, Input.Resolution.Y - num4 - vector.Y * 2f);
			base.ScaledSuggestedWidth = MathF.Min(base.ScaledSuggestedWidth, Input.Resolution.X - base.ScaledPositionXOffset - vector.X * 2f);
			base.ScaledSuggestedHeight = MathF.Min(base.ScaledSuggestedHeight, Input.Resolution.Y - base.ScaledPositionYOffset - vector.Y * 2f);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private void UpdateAdditionalOffsets()
		{
			float num;
			if (this.TargetHasAction && !this._isPressing)
			{
				float amount = (MathF.Sin(this.TransitionTimer / this.ActionAnimationTime * 1.6f) + 1f) / 2f;
				num = MathF.Lerp(this.DefaultOffset, this.HoverOffset, amount, 1E-05f) - this.DefaultOffset;
			}
			else
			{
				num = 0f;
			}
			float num2;
			if (this._isPressing)
			{
				num2 = (this.HasTarget ? this.PressOffset : (this.DefaultTargetlessOffset * 0.7f));
			}
			else if (this.HasTarget)
			{
				num2 = this.DefaultOffset;
			}
			else
			{
				num2 = this.DefaultTargetlessOffset;
			}
			this._additionalOffset = (num2 + num) * base._scaleToUse;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006D7C File Offset: 0x00004F7C
		private void ResetAnimations()
		{
			if (!this._isPressing)
			{
				this.TransitionTimer = 0f;
				this._additionalOffsetBeforeStateChange = this._additionalOffset;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006D9D File Offset: 0x00004F9D
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public GamepadCursorParentWidget CursorParentWidget
		{
			get
			{
				return this._cursorParentWidget;
			}
			set
			{
				if (value != this._cursorParentWidget)
				{
					this._cursorParentWidget = value;
					base.OnPropertyChanged<GamepadCursorParentWidget>(value, "CursorParentWidget");
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006DC3 File Offset: 0x00004FC3
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006DCB File Offset: 0x00004FCB
		public GamepadCursorMarkerWidget TopLeftMarker
		{
			get
			{
				return this._topLeftMarker;
			}
			set
			{
				if (value != this._topLeftMarker)
				{
					this._topLeftMarker = value;
					base.OnPropertyChanged<GamepadCursorMarkerWidget>(value, "TopLeftMarker");
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006DE9 File Offset: 0x00004FE9
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public GamepadCursorMarkerWidget TopRightMarker
		{
			get
			{
				return this._topRightMarker;
			}
			set
			{
				if (value != this._topRightMarker)
				{
					this._topRightMarker = value;
					base.OnPropertyChanged<GamepadCursorMarkerWidget>(value, "TopRightMarker");
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006E0F File Offset: 0x0000500F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006E17 File Offset: 0x00005017
		public GamepadCursorMarkerWidget BottomLeftMarker
		{
			get
			{
				return this._bottomLeftMarker;
			}
			set
			{
				if (value != this._bottomLeftMarker)
				{
					this._bottomLeftMarker = value;
					base.OnPropertyChanged<GamepadCursorMarkerWidget>(value, "BottomLeftMarker");
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006E35 File Offset: 0x00005035
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006E3D File Offset: 0x0000503D
		public GamepadCursorMarkerWidget BottomRightMarker
		{
			get
			{
				return this._bottomRightMarker;
			}
			set
			{
				if (value != this._bottomRightMarker)
				{
					this._bottomRightMarker = value;
					base.OnPropertyChanged<GamepadCursorMarkerWidget>(value, "BottomRightMarker");
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006E5B File Offset: 0x0000505B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006E63 File Offset: 0x00005063
		public bool HasTarget
		{
			get
			{
				return this._hasTarget;
			}
			set
			{
				if (value != this._hasTarget)
				{
					this._hasTarget = value;
					base.OnPropertyChanged(value, "HasTarget");
					this.ResetAnimations();
					this._animationRatioTimer = 0f;
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006E92 File Offset: 0x00005092
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006E9A File Offset: 0x0000509A
		public bool TargetHasAction
		{
			get
			{
				return this._targetHasAction;
			}
			set
			{
				if (value != this._targetHasAction)
				{
					this._targetHasAction = value;
					base.OnPropertyChanged(value, "TargetHasAction");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006EBE File Offset: 0x000050BE
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006EC6 File Offset: 0x000050C6
		public float TargetX
		{
			get
			{
				return this._targetX;
			}
			set
			{
				if (value != this._targetX)
				{
					this._targetX = value;
					base.OnPropertyChanged(value, "TargetX");
					this.ResetAnimations();
					this._targetPositionChangedThisFrame = true;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006EF1 File Offset: 0x000050F1
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006EF9 File Offset: 0x000050F9
		public float TargetY
		{
			get
			{
				return this._targetY;
			}
			set
			{
				if (value != this._targetY)
				{
					this._targetY = value;
					base.OnPropertyChanged(value, "TargetY");
					this.ResetAnimations();
					this._targetPositionChangedThisFrame = true;
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006F24 File Offset: 0x00005124
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00006F2C File Offset: 0x0000512C
		public float TargetWidth
		{
			get
			{
				return this._targetWidth;
			}
			set
			{
				if (value != this._targetWidth)
				{
					this._targetWidth = value;
					base.OnPropertyChanged(value, "TargetWidth");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006F50 File Offset: 0x00005150
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006F58 File Offset: 0x00005158
		public float TargetHeight
		{
			get
			{
				return this._targetHeight;
			}
			set
			{
				if (value != this._targetHeight)
				{
					this._targetHeight = value;
					base.OnPropertyChanged(value, "TargetHeight");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006F7C File Offset: 0x0000517C
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006F84 File Offset: 0x00005184
		public float DefaultOffset
		{
			get
			{
				return this._defaultOffset;
			}
			set
			{
				if (value != this._defaultOffset)
				{
					this._defaultOffset = value;
					base.OnPropertyChanged(value, "DefaultOffset");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006FA8 File Offset: 0x000051A8
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00006FB0 File Offset: 0x000051B0
		public float HoverOffset
		{
			get
			{
				return this._hoverOffset;
			}
			set
			{
				if (value != this._hoverOffset)
				{
					this._hoverOffset = value;
					base.OnPropertyChanged(value, "HoverOffset");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006FD4 File Offset: 0x000051D4
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006FDC File Offset: 0x000051DC
		public float DefaultTargetlessOffset
		{
			get
			{
				return this._defaultTargetlessOffset;
			}
			set
			{
				if (value != this._defaultTargetlessOffset)
				{
					this._defaultTargetlessOffset = value;
					base.OnPropertyChanged(value, "DefaultTargetlessOffset");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007000 File Offset: 0x00005200
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00007008 File Offset: 0x00005208
		public float PressOffset
		{
			get
			{
				return this._pressOffset;
			}
			set
			{
				if (value != this._pressOffset)
				{
					this._pressOffset = value;
					base.OnPropertyChanged(value, "PressOffset");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007034 File Offset: 0x00005234
		public float ActionAnimationTime
		{
			get
			{
				return this._actionAnimationTime;
			}
			set
			{
				if (value != this._actionAnimationTime)
				{
					this._actionAnimationTime = value;
					base.OnPropertyChanged(value, "ActionAnimationTime");
					this.ResetAnimations();
				}
			}
		}

		// Token: 0x040000C0 RID: 192
		private Widget _targetWidget;

		// Token: 0x040000C1 RID: 193
		private bool _targetChangedThisFrame;

		// Token: 0x040000C2 RID: 194
		private bool _targetPositionChangedThisFrame;

		// Token: 0x040000C3 RID: 195
		private float _animationRatio;

		// Token: 0x040000C4 RID: 196
		private float _animationRatioTimer;

		// Token: 0x040000C5 RID: 197
		protected bool _isPressing;

		// Token: 0x040000C6 RID: 198
		protected bool _areBrushesValidated;

		// Token: 0x040000C8 RID: 200
		protected float _additionalOffset;

		// Token: 0x040000C9 RID: 201
		protected float _additionalOffsetBeforeStateChange;

		// Token: 0x040000CA RID: 202
		protected float _leftOffset;

		// Token: 0x040000CB RID: 203
		protected float _rightOffset;

		// Token: 0x040000CC RID: 204
		protected float _topOffset;

		// Token: 0x040000CD RID: 205
		protected float _bottomOffset;

		// Token: 0x040000CE RID: 206
		private GamepadCursorParentWidget _cursorParentWidget;

		// Token: 0x040000CF RID: 207
		private GamepadCursorMarkerWidget _topLeftMarker;

		// Token: 0x040000D0 RID: 208
		private GamepadCursorMarkerWidget _topRightMarker;

		// Token: 0x040000D1 RID: 209
		private GamepadCursorMarkerWidget _bottomLeftMarker;

		// Token: 0x040000D2 RID: 210
		private GamepadCursorMarkerWidget _bottomRightMarker;

		// Token: 0x040000D3 RID: 211
		private bool _hasTarget;

		// Token: 0x040000D4 RID: 212
		private bool _targetHasAction;

		// Token: 0x040000D5 RID: 213
		private float _targetX;

		// Token: 0x040000D6 RID: 214
		private float _targetY;

		// Token: 0x040000D7 RID: 215
		private float _targetWidth;

		// Token: 0x040000D8 RID: 216
		private float _targetHeight;

		// Token: 0x040000D9 RID: 217
		private float _defaultOffset;

		// Token: 0x040000DA RID: 218
		private float _hoverOffset;

		// Token: 0x040000DB RID: 219
		private float _defaultTargetlessOffset;

		// Token: 0x040000DC RID: 220
		private float _pressOffset;

		// Token: 0x040000DD RID: 221
		private float _actionAnimationTime;
	}
}
