using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Recruitment
{
	// Token: 0x02000100 RID: 256
	public class RecruitTroopPanelButtonWidget : ButtonWidget
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x00025747 File Offset: 0x00023947
		public RecruitTroopPanelButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00025750 File Offset: 0x00023950
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.IsTroopEmpty)
			{
				if (this.PlayerHasEnoughRelation)
				{
					this.SetState("EmptyEnoughRelation");
				}
				else
				{
					this.SetState("EmptyNoRelation");
				}
			}
			else if (this.CanBeRecruited)
			{
				this.SetState("Available");
			}
			else
			{
				this.SetState("Unavailable");
			}
			if (!this.PlayerHasEnoughRelation && !this.IsTroopEmpty && this.CharacterImageWidget != null)
			{
				this.CharacterImageWidget.Brush.ValueFactor = -50f;
				this.CharacterImageWidget.Brush.SaturationFactor = -100f;
			}
			if (this.CharacterImageWidget != null)
			{
				this.CharacterImageWidget.IsHidden = this.IsTroopEmpty;
			}
			this.RemoveFromCartButton.SetState(((base.IsHovered || base.IsPressed || base.IsSelected) && ((!this.IsTroopEmpty && this.PlayerHasEnoughRelation) || this.IsInCart)) ? "Hovered" : "Default");
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00025850 File Offset: 0x00023A50
		private bool IsMouseOverWidget()
		{
			Vector2 globalPosition = base.GlobalPosition;
			return this.IsBetween(base.EventManager.MousePosition.X, globalPosition.X, globalPosition.X + base.Size.X) && this.IsBetween(base.EventManager.MousePosition.Y, globalPosition.Y, globalPosition.Y + base.Size.Y);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000258C4 File Offset: 0x00023AC4
		private bool IsBetween(float number, float min, float max)
		{
			return number >= min && number <= max;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000258D3 File Offset: 0x00023AD3
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000258DB File Offset: 0x00023ADB
		[Editor(false)]
		public bool CanBeRecruited
		{
			get
			{
				return this._canBeRecruited;
			}
			set
			{
				if (this._canBeRecruited != value)
				{
					this._canBeRecruited = value;
					base.OnPropertyChanged(value, "CanBeRecruited");
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000258F9 File Offset: 0x00023AF9
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x00025901 File Offset: 0x00023B01
		[Editor(false)]
		public bool IsInCart
		{
			get
			{
				return this._isInCart;
			}
			set
			{
				if (this._isInCart != value)
				{
					this._isInCart = value;
					base.OnPropertyChanged(value, "IsInCart");
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0002591F File Offset: 0x00023B1F
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00025927 File Offset: 0x00023B27
		[Editor(false)]
		public ButtonWidget RemoveFromCartButton
		{
			get
			{
				return this._removeFromCartButton;
			}
			set
			{
				if (this._removeFromCartButton != value)
				{
					this._removeFromCartButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "RemoveFromCartButton");
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00025945 File Offset: 0x00023B45
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0002594D File Offset: 0x00023B4D
		[Editor(false)]
		public ImageIdentifierWidget CharacterImageWidget
		{
			get
			{
				return this._characterImageWidget;
			}
			set
			{
				if (this._characterImageWidget != value)
				{
					this._characterImageWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "CharacterImageWidget");
				}
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0002596B File Offset: 0x00023B6B
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00025973 File Offset: 0x00023B73
		[Editor(false)]
		public bool IsTroopEmpty
		{
			get
			{
				return this._isTroopEmpty;
			}
			set
			{
				if (this._isTroopEmpty != value)
				{
					this._isTroopEmpty = value;
					base.OnPropertyChanged(value, "IsTroopEmpty");
				}
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00025991 File Offset: 0x00023B91
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00025999 File Offset: 0x00023B99
		[Editor(false)]
		public bool PlayerHasEnoughRelation
		{
			get
			{
				return this._playerHasEnoughRelation;
			}
			set
			{
				if (this._playerHasEnoughRelation != value)
				{
					this._playerHasEnoughRelation = value;
					base.OnPropertyChanged(value, "PlayerHasEnoughRelation");
				}
			}
		}

		// Token: 0x04000631 RID: 1585
		private bool _canBeRecruited;

		// Token: 0x04000632 RID: 1586
		private bool _isInCart;

		// Token: 0x04000633 RID: 1587
		private bool _playerHasEnoughRelation;

		// Token: 0x04000634 RID: 1588
		private bool _isTroopEmpty;

		// Token: 0x04000635 RID: 1589
		private ButtonWidget _removeFromCartButton;

		// Token: 0x04000636 RID: 1590
		private ImageIdentifierWidget _characterImageWidget;
	}
}
