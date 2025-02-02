using System;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200006A RID: 106
	public class ScrollbarWidget : ImageWidget
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001E100 File Offset: 0x0001C300
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0001E108 File Offset: 0x0001C308
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001E12B File Offset: 0x0001C32B
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0001E133 File Offset: 0x0001C333
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001E156 File Offset: 0x0001C356
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001E15E File Offset: 0x0001C35E
		[Editor(false)]
		public AlignmentAxis AlignmentAxis { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001E167 File Offset: 0x0001C367
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001E16F File Offset: 0x0001C36F
		[Editor(false)]
		public bool ReverseDirection { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001E178 File Offset: 0x0001C378
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001E180 File Offset: 0x0001C380
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
					if (this.MinValue <= this.MaxValue)
					{
						if (this._valueFloat < this.MinValue)
						{
							this._valueFloat = this.MinValue;
						}
						if (this._valueFloat > this.MaxValue)
						{
							this._valueFloat = this.MaxValue;
						}
						if (this.IsDiscrete)
						{
							this._valueFloat = (float)MathF.Round(value);
						}
						else
						{
							this._valueFloat = value;
						}
						this.UpdateHandleByValue();
						if (MathF.Abs(this._valueFloat - valueFloat) > 1E-05f)
						{
							base.OnPropertyChanged(this._valueFloat, "ValueFloat");
							base.OnPropertyChanged(this.ValueInt, "ValueInt");
						}
					}
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001E255 File Offset: 0x0001C455
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001E262 File Offset: 0x0001C462
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
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001E26C File Offset: 0x0001C46C
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001E274 File Offset: 0x0001C474
		[Editor(false)]
		public float MinValue { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001E27D File Offset: 0x0001C47D
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001E285 File Offset: 0x0001C485
		[Editor(false)]
		public float MaxValue { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001E28E File Offset: 0x0001C48E
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001E296 File Offset: 0x0001C496
		[Editor(false)]
		public bool DoNotUpdateHandleSize { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001E29F File Offset: 0x0001C49F
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001E2A7 File Offset: 0x0001C4A7
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
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001E2BF File Offset: 0x0001C4BF
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001E2C7 File Offset: 0x0001C4C7
		[Editor(false)]
		public Widget ScrollbarArea { get; set; }

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		public ScrollbarWidget(UIContext context) : base(context)
		{
			this.ScrollbarArea = this;
			this._firstFrame = true;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
		protected override void OnLateUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.Handle.IsPressed)
			{
				if (!this._handleClicked)
				{
					this._handleClicked = true;
					this._localClickPos = base.EventManager.MousePosition - this.Handle.GlobalPosition;
				}
				this.HandleMouseMove();
			}
			else
			{
				this._handleClicked = false;
			}
			this.UpdateScrollBar();
			this.UpdateHandleLength();
			this._firstFrame = false;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001E35C File Offset: 0x0001C55C
		protected internal override void OnMousePressed()
		{
			if (this.Handle != null)
			{
				base.IsPressed = true;
				Vector2 mousePosition = base.EventManager.MousePosition;
				this._localClickPos = mousePosition - this.Handle.GlobalPosition;
				if (this._localClickPos.X < -5f)
				{
					this._localClickPos.X = -5f;
				}
				else if (this._localClickPos.X > this.Handle.Size.X + 5f)
				{
					this._localClickPos.X = this.Handle.Size.X + 5f;
				}
				if (this._localClickPos.Y < -5f)
				{
					this._localClickPos.Y = -5f;
				}
				else if (this._localClickPos.Y > this.Handle.Size.Y + 5f)
				{
					this._localClickPos.Y = this.Handle.Size.Y + 5f;
				}
				this.HandleMouseMove();
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001E474 File Offset: 0x0001C674
		protected internal override void OnMouseReleased()
		{
			if (this.Handle != null)
			{
				base.IsPressed = false;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001E485 File Offset: 0x0001C685
		public void SetValueForced(float value)
		{
			if (value > this.MaxValue)
			{
				this.MaxValue = value;
			}
			else if (value < this.MinValue)
			{
				this.MinValue = value;
			}
			this.ValueFloat = value;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		private void UpdateScrollBar()
		{
			if (!this._firstFrame)
			{
				this.UpdateHandleByValue();
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		private float GetValue(Vector2 value, AlignmentAxis alignmentAxis)
		{
			if (alignmentAxis == AlignmentAxis.Horizontal)
			{
				return value.X;
			}
			return value.Y;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		private void HandleMouseMove()
		{
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
				}
				else
				{
					if (num < num2)
					{
						num = num2;
					}
					if (num > num3)
					{
						num = num3;
					}
					float num4 = (num - num2) / (num3 - num2);
					this.ValueFloat = this.MinValue + (this.MaxValue - this.MinValue) * num4;
				}
				this.UpdateHandleByValue();
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		private void UpdateHandleLength()
		{
			if (!this.DoNotUpdateHandleSize && this.IsDiscrete && this.Handle.WidthSizePolicy == SizePolicy.Fixed)
			{
				if (this.AlignmentAxis == AlignmentAxis.Horizontal)
				{
					this.Handle.SuggestedWidth = Mathf.Clamp(base.SuggestedWidth / (this.MaxValue + 1f), 50f, base.SuggestedWidth / 2f);
					return;
				}
				if (this.AlignmentAxis == AlignmentAxis.Vertical)
				{
					this.Handle.SuggestedHeight = Mathf.Clamp(base.SuggestedHeight / (this.MaxValue + 1f), 50f, base.SuggestedHeight / 2f);
				}
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001E684 File Offset: 0x0001C884
		private void UpdateHandleByValue()
		{
			if (this._valueFloat < this.MinValue)
			{
				this.ValueFloat = this.MinValue;
			}
			if (this._valueFloat > this.MaxValue)
			{
				this.ValueFloat = this.MaxValue;
			}
			float num = 0f;
			if (MathF.Abs(this.MaxValue - this.MinValue) > 1E-45f)
			{
				num = (this._valueFloat - this.MinValue) / (this.MaxValue - this.MinValue);
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
					float num2 = this.ScrollbarArea.Size.X;
					num2 -= this.Handle.Size.X;
					this.Handle.ScaledPositionXOffset = num2 * num;
					this.Handle.ScaledPositionYOffset = 0f;
					return;
				}
				this.Handle.HorizontalAlignment = HorizontalAlignment.Center;
				this.Handle.VerticalAlignment = VerticalAlignment.Bottom;
				float num3 = this.ScrollbarArea.Size.Y;
				num3 -= this.Handle.Size.Y;
				this.Handle.ScaledPositionYOffset = -1f * num3 * (1f - num);
				this.Handle.ScaledPositionXOffset = 0f;
			}
		}

		// Token: 0x04000336 RID: 822
		private bool _locked;

		// Token: 0x04000337 RID: 823
		private bool _isDiscrete;

		// Token: 0x0400033A RID: 826
		private float _valueFloat;

		// Token: 0x0400033E RID: 830
		public float HandleRatio;

		// Token: 0x0400033F RID: 831
		private Widget _handle;

		// Token: 0x04000341 RID: 833
		private bool _firstFrame;

		// Token: 0x04000342 RID: 834
		private Vector2 _localClickPos;

		// Token: 0x04000343 RID: 835
		private bool _handleClicked;
	}
}
