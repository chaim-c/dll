using System;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001B RID: 27
	public class CircleActionSelectorWidget : Widget
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000A8AD File Offset: 0x00008AAD
		public CircleActionSelectorWidget(UIContext context) : base(context)
		{
			this._activateOnlyWithController = false;
			this._distanceFromCenterModifier = 300f;
			this._directionWidgetDistanceMultiplier = 0.5f;
			this._centerDistanceAnimationTimer = -1f;
			this._centerDistanceAnimationDuration = -1f;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A8E9 File Offset: 0x00008AE9
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.boolPropertyChanged += this.OnChildPropertyChanged;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A904 File Offset: 0x00008B04
		private void OnChildPropertyChanged(PropertyOwnerObject widget, string propertyName, bool value)
		{
			if (propertyName == "IsSelected" && base.EventManager.IsControllerActive && !this._isRefreshingSelection)
			{
				this._mouseDirection = Vec2.Zero;
				this._mouseMoveAccumulated = Vec2.Zero;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A940 File Offset: 0x00008B40
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this.AllowInvalidSelection)
			{
				this._currentSelectedIndex = -1;
			}
			if (base.IsRecursivelyVisible())
			{
				this.UpdateItemPlacement();
				this.AnimateDistanceFromCenter(dt);
				bool flag = this.IsCircularInputEnabled && (!this.ActivateOnlyWithController || base.EventManager.IsControllerActive);
				if (this.DirectionWidget != null)
				{
					this.DirectionWidget.IsVisible = flag;
				}
				if (flag)
				{
					this.UpdateAverageMouseDirection();
					this.UpdateCircularInput();
					return;
				}
			}
			else
			{
				if (this._mouseDirection.X != 0f || this._mouseDirection.Y != 0f)
				{
					this._mouseDirection = default(Vec2);
				}
				if (this.DirectionWidget != null)
				{
					this.DirectionWidget.IsVisible = false;
				}
				this._mouseMoveAccumulated = Vec2.Zero;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AA10 File Offset: 0x00008C10
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

		// Token: 0x06000204 RID: 516 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public void AnimateDistanceFromCenterTo(float distanceFromCenter, float animationDuration)
		{
			this._centerDistanceAnimationTimer = 0f;
			this._centerDistanceAnimationInitialValue = this.DistanceFromCenterModifier;
			this._centerDistanceAnimationDuration = animationDuration;
			this._centerDistanceAnimationTarget = distanceFromCenter;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		private void UpdateAverageMouseDirection()
		{
			IInputContext inputContext = base.EventManager.InputContext;
			bool isMouseActive = inputContext.GetIsMouseActive();
			Vec2 v = isMouseActive ? new Vec2(inputContext.GetMouseMoveX(), inputContext.GetMouseMoveY()) : inputContext.GetControllerRightStickState();
			if (isMouseActive)
			{
				this._mouseMoveAccumulated += v;
				if (this._mouseMoveAccumulated.LengthSquared > 15625f)
				{
					this._mouseMoveAccumulated.Normalize();
					this._mouseMoveAccumulated *= 125f;
				}
				this._mouseDirection = new Vec2(this._mouseMoveAccumulated.X, -this._mouseMoveAccumulated.Y);
				return;
			}
			this._mouseDirection = new Vec2(v.X, v.Y);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000ABA0 File Offset: 0x00008DA0
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

		// Token: 0x06000207 RID: 519 RVA: 0x0000AC59 File Offset: 0x00008E59
		public bool TrySetSelectedIndex(int index)
		{
			if (index >= 0 && index < base.ChildCount)
			{
				this.OnSelectedIndexChanged(index);
				return true;
			}
			return false;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AC74 File Offset: 0x00008E74
		protected virtual void OnSelectedIndexChanged(int selectedIndex)
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				ButtonWidget buttonWidget;
				if ((buttonWidget = (child as ButtonWidget)) != null)
				{
					buttonWidget.IsSelected = (!child.IsDisabled && i == selectedIndex);
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000ACBC File Offset: 0x00008EBC
		private void UpdateCircularInput()
		{
			int currentSelectedIndex = this._currentSelectedIndex;
			if (this._mouseDirection.Length > 0.391f)
			{
				if (base.ChildCount > 0)
				{
					float mouseDirectionAngle = this.AngleFromDir(this._mouseDirection);
					this._currentSelectedIndex = this.GetIndexOfSelectedItemByAngle(mouseDirectionAngle);
				}
			}
			else if (this.AllowInvalidSelection)
			{
				this._currentSelectedIndex = -1;
			}
			if (currentSelectedIndex != this._currentSelectedIndex)
			{
				this._isRefreshingSelection = true;
				this.OnSelectedIndexChanged(this._currentSelectedIndex);
				this._isRefreshingSelection = false;
			}
			if (this.DirectionWidget != null)
			{
				if (this._mouseDirection.LengthSquared > 0f)
				{
					Vec2 vec = this._mouseDirection.Normalized();
					this.DirectionWidget.PositionXOffset = vec.X * (this.DistanceFromCenterModifier * this.DirectionWidgetDistanceMultiplier);
					this.DirectionWidget.PositionYOffset = -vec.Y * (this.DistanceFromCenterModifier * this.DirectionWidgetDistanceMultiplier);
					return;
				}
				this.DirectionWidget.PositionXOffset = 0f;
				this.DirectionWidget.PositionYOffset = 0f;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		private int GetIndexOfSelectedItemByAngle(float mouseDirectionAngle)
		{
			int childCount = base.ChildCount;
			float num = 360f / (float)childCount;
			float num2 = -(num / 2f);
			if (num2 < 0f)
			{
				num2 += 360f;
			}
			for (int i = 0; i < childCount; i++)
			{
				float angle = num * (float)i;
				float angle2 = num * (float)(i + 1);
				float minAngle = this.AddAngle(num2, angle) * 0.017453292f;
				float maxAngle = this.AddAngle(num2, angle2) * 0.017453292f;
				if (this.IsAngleBetweenAngles(mouseDirectionAngle * 0.017453292f, minAngle, maxAngle))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AE4C File Offset: 0x0000904C
		private float AddAngle(float angle1, float angle2)
		{
			float num = angle1 + angle2;
			if (num < 0f)
			{
				num += 360f;
			}
			return num % 360f;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AE78 File Offset: 0x00009078
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

		// Token: 0x0600020D RID: 525 RVA: 0x0000AEF0 File Offset: 0x000090F0
		private float AngleFromDir(Vec2 directionVector)
		{
			if (directionVector.X < 0f)
			{
				return 360f - (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f * -1f;
			}
			return (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AF50 File Offset: 0x00009150
		private Vec2 DirFromAngle(float angle)
		{
			return new Vec2(MathF.Sin(angle), MathF.Cos(angle));
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000AF65 File Offset: 0x00009165
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000AF6D File Offset: 0x0000916D
		public bool AllowInvalidSelection
		{
			get
			{
				return this._allowInvalidSelection;
			}
			set
			{
				if (value != this._allowInvalidSelection)
				{
					this._allowInvalidSelection = value;
					base.OnPropertyChanged(value, "AllowInvalidSelection");
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000AF8B File Offset: 0x0000918B
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000AF93 File Offset: 0x00009193
		public bool ActivateOnlyWithController
		{
			get
			{
				return this._activateOnlyWithController;
			}
			set
			{
				if (value != this._activateOnlyWithController)
				{
					this._activateOnlyWithController = value;
					base.OnPropertyChanged(value, "ActivateOnlyWithController");
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AFB1 File Offset: 0x000091B1
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000AFBC File Offset: 0x000091BC
		public bool IsCircularInputEnabled
		{
			get
			{
				return !this.IsCircularInputDisabled;
			}
			set
			{
				if (value == this.IsCircularInputDisabled)
				{
					this.IsCircularInputDisabled = !value;
					base.OnPropertyChanged(!value, "IsCircularInputEnabled");
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AFE0 File Offset: 0x000091E0
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public bool IsCircularInputDisabled
		{
			get
			{
				return this._isCircularInputDisabled;
			}
			set
			{
				if (value != this._isCircularInputDisabled)
				{
					this._isCircularInputDisabled = value;
					base.OnPropertyChanged(value, "IsCircularInputDisabled");
					if (value)
					{
						this.OnSelectedIndexChanged(-1);
					}
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000B010 File Offset: 0x00009210
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000B018 File Offset: 0x00009218
		public float DistanceFromCenterModifier
		{
			get
			{
				return this._distanceFromCenterModifier;
			}
			set
			{
				if (value != this._distanceFromCenterModifier)
				{
					this._distanceFromCenterModifier = value;
					base.OnPropertyChanged(value, "DistanceFromCenterModifier");
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000B036 File Offset: 0x00009236
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000B03E File Offset: 0x0000923E
		public float DirectionWidgetDistanceMultiplier
		{
			get
			{
				return this._directionWidgetDistanceMultiplier;
			}
			set
			{
				if (value != this._directionWidgetDistanceMultiplier)
				{
					this._directionWidgetDistanceMultiplier = value;
					base.OnPropertyChanged(value, "DirectionWidgetDistanceMultiplier");
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000B05C File Offset: 0x0000925C
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000B064 File Offset: 0x00009264
		public Widget DirectionWidget
		{
			get
			{
				return this._directionWidget;
			}
			set
			{
				if (value != this._directionWidget)
				{
					this._directionWidget = value;
					base.OnPropertyChanged<Widget>(value, "DirectionWidget");
				}
			}
		}

		// Token: 0x040000FA RID: 250
		private int _currentSelectedIndex;

		// Token: 0x040000FB RID: 251
		private const float _mouseMoveMaxDistance = 125f;

		// Token: 0x040000FC RID: 252
		private const float _gamepadDeadzoneLength = 0.391f;

		// Token: 0x040000FD RID: 253
		private const float _mouseMoveMaxDistanceSquared = 15625f;

		// Token: 0x040000FE RID: 254
		private float _centerDistanceAnimationTimer;

		// Token: 0x040000FF RID: 255
		private float _centerDistanceAnimationDuration;

		// Token: 0x04000100 RID: 256
		private float _centerDistanceAnimationInitialValue;

		// Token: 0x04000101 RID: 257
		private float _centerDistanceAnimationTarget;

		// Token: 0x04000102 RID: 258
		private Vec2 _mouseDirection;

		// Token: 0x04000103 RID: 259
		private Vec2 _mouseMoveAccumulated;

		// Token: 0x04000104 RID: 260
		private bool _isRefreshingSelection;

		// Token: 0x04000105 RID: 261
		private bool _allowInvalidSelection;

		// Token: 0x04000106 RID: 262
		private bool _activateOnlyWithController;

		// Token: 0x04000107 RID: 263
		private bool _isCircularInputDisabled;

		// Token: 0x04000108 RID: 264
		private float _distanceFromCenterModifier;

		// Token: 0x04000109 RID: 265
		private float _directionWidgetDistanceMultiplier;

		// Token: 0x0400010A RID: 266
		private Widget _directionWidget;
	}
}
