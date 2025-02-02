using System;
using System.Numerics;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200006C RID: 108
	public class SliderWidget : ImageWidget
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001E86A File Offset: 0x0001CA6A
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001E872 File Offset: 0x0001CA72
		public bool UpdateValueOnScroll { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001E87B File Offset: 0x0001CA7B
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001E883 File Offset: 0x0001CA83
		public bool DPadMovementEnabled { get; set; } = true;

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001E88C File Offset: 0x0001CA8C
		private float _holdTimeToStartMovement
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001E893 File Offset: 0x0001CA93
		private float _dynamicIncrement
		{
			get
			{
				if (this.MaxValueFloat - this.MinValueFloat <= 2f)
				{
					return 0.1f;
				}
				return 1f;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		public SliderWidget(UIContext context) : base(context)
		{
			this.SliderArea = this;
			this._firstFrame = true;
			base.FrictionEnabled = true;
			base.UsedNavigationMovements = GamepadNavigationTypes.Horizontal;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			bool flag = false;
			base.IsUsingNavigation = false;
			if (!base.IsPressed)
			{
				Widget handle = this.Handle;
				if (handle == null || !handle.IsPressed)
				{
					Widget handleExtension = this.HandleExtension;
					if (handleExtension == null || !handleExtension.IsPressed)
					{
						this._downStartTime = -1f;
						this._handleClickOffset = Vector2.Zero;
						this._handleClicked = false;
						this._valueChangedByMouse = false;
						goto IL_1D5;
					}
				}
			}
			if (base.EventManager.IsControllerActive && base.IsRecursivelyVisible() && base.EventManager.GetIsHitThisFrame())
			{
				float num = 0f;
				if (Input.IsKeyDown(InputKey.ControllerLLeft))
				{
					num = -1f;
				}
				else if (Input.IsKeyDown(InputKey.ControllerLRight))
				{
					num = 1f;
				}
				if (num != 0f)
				{
					num *= (this.IsDiscrete ? ((float)this.DiscreteIncrementInterval) : this._dynamicIncrement);
					if (this._downStartTime == -1f)
					{
						this._downStartTime = base.Context.EventManager.Time;
						this.ValueFloat = MathF.Clamp(this._valueFloat + num, this.MinValueFloat, this.MaxValueFloat);
						flag = true;
					}
					else if (this._holdTimeToStartMovement < base.Context.EventManager.Time - this._downStartTime)
					{
						this.ValueFloat = MathF.Clamp(this._valueFloat + num, this.MinValueFloat, this.MaxValueFloat);
						flag = true;
					}
				}
				else
				{
					this._downStartTime = -1f;
				}
				base.IsUsingNavigation = true;
			}
			if (!this._handleClicked)
			{
				this._handleClicked = true;
				this.UpdateLocalClickPosition();
				this._handleClickOffset = base.EventManager.MousePosition - (this.Handle.GlobalPosition + this.Handle.Size * 0.5f);
			}
			this.HandleMouseMove();
			IL_1D5:
			this.UpdateScrollBar();
			this.UpdateHandleLength();
			Widget handle2 = this.Handle;
			if (handle2 != null)
			{
				handle2.SetState(base.CurrentState);
			}
			if (this._snapCursorToHandle)
			{
				Vector2 vector = this.Handle.GlobalPosition + this.Handle.Size / 2f;
				Input.SetMousePosition((int)vector.X, (int)vector.Y);
				base.EventManager.UpdateMousePosition(vector);
				this._snapCursorToHandle = false;
			}
			if (flag && Input.MouseMoveX == 0f && Input.MouseMoveY == 0f)
			{
				this._snapCursorToHandle = true;
			}
			this._firstFrame = false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001EB78 File Offset: 0x0001CD78
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			if (this.Filler != null)
			{
				float num = 1f;
				if (MathF.Abs(this.MaxValueFloat - this.MinValueFloat) > 1E-45f)
				{
					num = (this._valueFloat - this.MinValueFloat) / (this.MaxValueFloat - this.MinValueFloat);
				}
				this.Filler.HorizontalAlignment = HorizontalAlignment.Left;
				if (this.AlignmentAxis == AlignmentAxis.Horizontal)
				{
					this.Filler.WidthSizePolicy = SizePolicy.Fixed;
					this.Filler.ScaledSuggestedWidth = this.SliderArea.Size.X * num;
				}
				else
				{
					this.Filler.HeightSizePolicy = SizePolicy.Fixed;
					this.Filler.ScaledSuggestedHeight = this.SliderArea.Size.Y * num;
				}
				this.Filler.DoNotAcceptEvents = true;
				this.Filler.DoNotPassEventsToChildren = true;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001EC52 File Offset: 0x0001CE52
		protected internal override void OnMousePressed()
		{
			if (this.Handle != null && this.Handle.IsVisible)
			{
				base.IsPressed = true;
				this.UpdateLocalClickPosition();
				base.OnPropertyChanged<string>("MouseDown", "OnMousePressed");
				this.HandleMouseMove();
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001EC8C File Offset: 0x0001CE8C
		protected internal override void OnMouseReleased()
		{
			if (this.Handle != null)
			{
				base.IsPressed = false;
				if (this.UpdateValueOnRelease)
				{
					base.OnPropertyChanged(this._valueFloat, "ValueFloat");
					base.OnPropertyChanged(this.ValueInt, "ValueInt");
					this.OnValueFloatChanged(this._valueFloat);
				}
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001ECDE File Offset: 0x0001CEDE
		protected internal override void OnMouseMove()
		{
			base.OnMouseMove();
			if (base.IsPressed)
			{
				this.HandleMouseMove();
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		protected internal virtual void OnValueIntChanged(int value)
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001ECF6 File Offset: 0x0001CEF6
		protected internal virtual void OnValueFloatChanged(float value)
		{
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
		private void UpdateScrollBar()
		{
			if (!this._firstFrame && base.IsVisible)
			{
				this.UpdateHandleByValue();
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001ED10 File Offset: 0x0001CF10
		private void UpdateLocalClickPosition()
		{
			Vector2 mousePosition = base.EventManager.MousePosition;
			this._localClickPos = mousePosition - this.Handle.GlobalPosition;
			if (this._localClickPos.X < 0f || this._localClickPos.X > this.Handle.Size.X)
			{
				this._localClickPos.X = this.Handle.Size.X / 2f;
			}
			if (this._localClickPos.Y < -5f)
			{
				this._localClickPos.Y = -5f;
				return;
			}
			if (this._localClickPos.Y > this.Handle.Size.Y + 5f)
			{
				this._localClickPos.Y = this.Handle.Size.Y + 5f;
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
		private void HandleMouseMove()
		{
			if (base.EventManager.IsControllerActive && Input.MouseMoveX == 0f && Input.MouseMoveY == 0f)
			{
				return;
			}
			if (this.Handle != null)
			{
				Vector2 value = base.EventManager.MousePosition - this._localClickPos;
				float num = this.GetValue(value, this.AlignmentAxis);
				float num2;
				float num3;
				if (this.AlignmentAxis == AlignmentAxis.Horizontal)
				{
					float x = base.ParentWidget.GlobalPosition.X;
					num2 = x + base.Left;
					num3 = x + base.Right;
					num3 -= this.Handle.Size.X;
					Widget handleExtension = this.HandleExtension;
					if (handleExtension != null && handleExtension.IsPressed)
					{
						num -= this._handleClickOffset.X;
					}
				}
				else
				{
					float y = base.ParentWidget.GlobalPosition.Y;
					num2 = y + base.Top;
					num3 = y + base.Bottom;
					num3 -= this.Handle.Size.Y;
				}
				if (Mathf.Abs(num3 - num2) < 1E-05f)
				{
					this.ValueFloat = 0f;
					return;
				}
				if (num < num2)
				{
					num = num2;
				}
				if (num > num3)
				{
					num = num3;
				}
				float num4 = (num - num2) / (num3 - num2);
				this._valueChangedByMouse = true;
				this.ValueFloat = this.MinValueFloat + (this.MaxValueFloat - this.MinValueFloat) * num4;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001EF44 File Offset: 0x0001D144
		private void UpdateHandleByValue()
		{
			if (this._valueFloat < this.MinValueFloat)
			{
				this.ValueFloat = this.MinValueFloat;
			}
			if (this._valueFloat > this.MaxValueFloat)
			{
				this.ValueFloat = this.MaxValueFloat;
			}
			float num = 1f;
			if (MathF.Abs(this.MaxValueFloat - this.MinValueFloat) > 1E-45f)
			{
				num = (this._valueFloat - this.MinValueFloat) / (this.MaxValueFloat - this.MinValueFloat);
				if (this.ReverseDirection)
				{
					num = 1f - num;
				}
			}
			if (this.Handle != null)
			{
				if (this.AlignmentAxis == AlignmentAxis.Horizontal)
				{
					this.Handle.HorizontalAlignment = HorizontalAlignment.Left;
					this.Handle.VerticalAlignment = VerticalAlignment.Center;
					float num2 = this.SliderArea.Size.X;
					num2 -= this.Handle.Size.X;
					this.Handle.ScaledPositionXOffset = num2 * num;
					this.Handle.ScaledPositionYOffset = 0f;
				}
				else
				{
					this.Handle.HorizontalAlignment = HorizontalAlignment.Center;
					this.Handle.VerticalAlignment = VerticalAlignment.Bottom;
					float num3 = this.SliderArea.Size.Y;
					num3 -= this.Handle.Size.Y;
					this.Handle.ScaledPositionYOffset = -1f * num3 * (1f - num);
					this.Handle.ScaledPositionXOffset = 0f;
				}
				if (this.HandleExtension != null)
				{
					this.HandleExtension.HorizontalAlignment = this.Handle.HorizontalAlignment;
					this.HandleExtension.VerticalAlignment = this.Handle.VerticalAlignment;
					this.HandleExtension.ScaledPositionXOffset = this.Handle.ScaledPositionXOffset;
					this.HandleExtension.ScaledPositionYOffset = this.Handle.ScaledPositionYOffset;
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001F108 File Offset: 0x0001D308
		private void UpdateHandleLength()
		{
			if (!this.DoNotUpdateHandleSize && this.IsDiscrete && this.Handle.WidthSizePolicy == SizePolicy.Fixed)
			{
				if (this.AlignmentAxis == AlignmentAxis.Horizontal)
				{
					this.Handle.SuggestedWidth = Mathf.Clamp(base.SuggestedWidth / (this.MaxValueFloat + 1f), 50f, base.SuggestedWidth / 2f);
					return;
				}
				if (this.AlignmentAxis == AlignmentAxis.Vertical)
				{
					this.Handle.SuggestedHeight = Mathf.Clamp(base.SuggestedHeight / (this.MaxValueFloat + 1f), 50f, base.SuggestedHeight / 2f);
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001F1B2 File Offset: 0x0001D3B2
		private float GetValue(Vector2 value, AlignmentAxis alignmentAxis)
		{
			if (alignmentAxis == AlignmentAxis.Horizontal)
			{
				return value.X;
			}
			return value.Y;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		protected override bool OnPreviewMouseScroll()
		{
			if (this.UpdateValueOnScroll)
			{
				float num = base.EventManager.DeltaMouseScroll * 0.004f;
				this.ValueFloat = MathF.Clamp(this._valueFloat + this._dynamicIncrement * num, this.MinValueFloat, this.MaxValueFloat);
			}
			return base.OnPreviewMouseScroll();
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001F217 File Offset: 0x0001D417
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001F21F File Offset: 0x0001D41F
		[Editor(false)]
		public bool IsDiscrete
		{
			get
			{
				return this._isDiscrete;
			}
			set
			{
				if (this._isDiscrete != value)
				{
					this._isDiscrete = value;
					base.OnPropertyChanged(this._isDiscrete, "IsDiscrete");
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001F242 File Offset: 0x0001D442
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001F24A File Offset: 0x0001D44A
		[Editor(false)]
		public bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				if (this._locked != value)
				{
					this._locked = value;
					base.OnPropertyChanged(this._locked, "Locked");
				}
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001F26D File Offset: 0x0001D46D
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001F275 File Offset: 0x0001D475
		[Editor(false)]
		public bool UpdateValueOnRelease
		{
			get
			{
				return this._updateValueOnRelease;
			}
			set
			{
				if (this._updateValueOnRelease != value)
				{
					this._updateValueOnRelease = value;
					base.OnPropertyChanged(this._updateValueOnRelease, "UpdateValueOnRelease");
				}
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001F298 File Offset: 0x0001D498
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001F2A3 File Offset: 0x0001D4A3
		[Editor(false)]
		public bool UpdateValueContinuously
		{
			get
			{
				return !this._updateValueOnRelease;
			}
			set
			{
				if (this.UpdateValueContinuously != value)
				{
					this._updateValueOnRelease = !value;
					base.OnPropertyChanged(this._updateValueOnRelease, "UpdateValueContinuously");
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001F2C9 File Offset: 0x0001D4C9
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		[Editor(false)]
		public AlignmentAxis AlignmentAxis { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001F2DA File Offset: 0x0001D4DA
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001F2E2 File Offset: 0x0001D4E2
		[Editor(false)]
		public bool ReverseDirection { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001F2F3 File Offset: 0x0001D4F3
		[Editor(false)]
		public Widget Filler { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001F304 File Offset: 0x0001D504
		[Editor(false)]
		public Widget HandleExtension { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001F30D File Offset: 0x0001D50D
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001F318 File Offset: 0x0001D518
		[Editor(false)]
		public float ValueFloat
		{
			get
			{
				return this._valueFloat;
			}
			set
			{
				if (!this.Locked && MathF.Abs(this._valueFloat - value) > 1E-05f)
				{
					float valueFloat = this._valueFloat;
					if (this.MinValueFloat <= this.MaxValueFloat)
					{
						if (this._valueFloat < this.MinValueFloat)
						{
							this._valueFloat = this.MinValueFloat;
						}
						if (this._valueFloat > this.MaxValueFloat)
						{
							this._valueFloat = this.MaxValueFloat;
						}
						if (this.IsDiscrete)
						{
							if (value >= (float)this.MaxValueInt)
							{
								this._valueFloat = (float)this.MaxValueInt;
							}
							else
							{
								float num = Mathf.Floor((value - (float)this.MinValueInt) / (float)this.DiscreteIncrementInterval);
								this._valueFloat = num * (float)this.DiscreteIncrementInterval + (float)this.MinValueInt;
							}
						}
						else
						{
							this._valueFloat = value;
						}
						this.UpdateHandleByValue();
						if (MathF.Abs(this._valueFloat - valueFloat) > 1E-05f && ((this.UpdateValueOnRelease && !base.IsPressed) || !this.UpdateValueOnRelease))
						{
							base.OnPropertyChanged(this._valueFloat, "ValueFloat");
							base.OnPropertyChanged(this.ValueInt, "ValueInt");
							this.OnValueFloatChanged(this._valueFloat);
						}
					}
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001F44B File Offset: 0x0001D64B
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001F458 File Offset: 0x0001D658
		[Editor(false)]
		public int ValueInt
		{
			get
			{
				return MathF.Round(this.ValueFloat);
			}
			set
			{
				this.ValueFloat = (float)value;
				this.OnValueIntChanged(this.ValueInt);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001F46E File Offset: 0x0001D66E
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001F476 File Offset: 0x0001D676
		[Editor(false)]
		public float MinValueFloat { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001F47F File Offset: 0x0001D67F
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001F487 File Offset: 0x0001D687
		[Editor(false)]
		public float MaxValueFloat { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001F490 File Offset: 0x0001D690
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001F49D File Offset: 0x0001D69D
		[Editor(false)]
		public int MinValueInt
		{
			get
			{
				return MathF.Round(this.MinValueFloat);
			}
			set
			{
				this.MinValueFloat = (float)value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001F4A7 File Offset: 0x0001D6A7
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
		[Editor(false)]
		public int MaxValueInt
		{
			get
			{
				return MathF.Round(this.MaxValueFloat);
			}
			set
			{
				this.MaxValueFloat = (float)value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001F4BE File Offset: 0x0001D6BE
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001F4C6 File Offset: 0x0001D6C6
		public int DiscreteIncrementInterval { get; set; } = 1;

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001F4CF File Offset: 0x0001D6CF
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001F4D7 File Offset: 0x0001D6D7
		[Editor(false)]
		public bool DoNotUpdateHandleSize { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		[Editor(false)]
		public Widget Handle
		{
			get
			{
				return this._handle;
			}
			set
			{
				if (this._handle != value)
				{
					this._handle = value;
					this.UpdateHandleByValue();
					if (this._handle != null)
					{
						this._handle.ExtendCursorAreaLeft = 6f;
						this._handle.ExtendCursorAreaRight = 6f;
						this._handle.ExtendCursorAreaTop = 3f;
						this._handle.ExtendCursorAreaBottom = 3f;
					}
				}
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001F553 File Offset: 0x0001D753
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001F55B File Offset: 0x0001D75B
		[Editor(false)]
		public Widget SliderArea { get; set; }

		// Token: 0x04000349 RID: 841
		private bool _firstFrame;

		// Token: 0x0400034A RID: 842
		public float HandleRatio;

		// Token: 0x0400034B RID: 843
		protected bool _handleClicked;

		// Token: 0x0400034C RID: 844
		protected bool _valueChangedByMouse;

		// Token: 0x0400034D RID: 845
		private float _downStartTime = -1f;

		// Token: 0x0400034E RID: 846
		private Vector2 _handleClickOffset;

		// Token: 0x0400034F RID: 847
		private bool _snapCursorToHandle;

		// Token: 0x04000350 RID: 848
		private bool _locked;

		// Token: 0x04000351 RID: 849
		private bool _isDiscrete;

		// Token: 0x04000352 RID: 850
		private bool _updateValueOnRelease;

		// Token: 0x04000353 RID: 851
		private Vector2 _localClickPos;

		// Token: 0x04000354 RID: 852
		private float _valueFloat;

		// Token: 0x04000355 RID: 853
		private Widget _handle;
	}
}
