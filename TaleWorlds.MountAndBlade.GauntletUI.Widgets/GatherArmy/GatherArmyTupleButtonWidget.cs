using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GatherArmy
{
	// Token: 0x02000142 RID: 322
	public class GatherArmyTupleButtonWidget : ButtonWidget
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x0002FEEA File Offset: 0x0002E0EA
		public GatherArmyTupleButtonWidget(UIContext context) : base(context)
		{
			base.OverrideDefaultStateSwitchingEnabled = true;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0002FEFA File Offset: 0x0002E0FA
		protected override void HandleClick()
		{
			if (!this.IsTransferDisabled && (this.IsInCart || this.IsEligible))
			{
				base.HandleClick();
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0002FF1C File Offset: 0x0002E11C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.IsTransferDisabled || (!this.IsInCart && !this.IsEligible))
			{
				this.SetState("Disabled");
				return;
			}
			if (this.IsInCart)
			{
				this.SetState("Selected");
				return;
			}
			if (base.IsPressed)
			{
				this.SetState("Pressed");
				return;
			}
			if (base.IsHovered)
			{
				this.SetState("Hovered");
				return;
			}
			this.SetState("Default");
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0002FF9B File Offset: 0x0002E19B
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x0002FFA3 File Offset: 0x0002E1A3
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0002FFC1 File Offset: 0x0002E1C1
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x0002FFC9 File Offset: 0x0002E1C9
		[Editor(false)]
		public bool IsEligible
		{
			get
			{
				return this._isEligible;
			}
			set
			{
				if (this._isEligible != value)
				{
					this._isEligible = value;
					base.OnPropertyChanged(value, "IsEligible");
				}
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0002FFE7 File Offset: 0x0002E1E7
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x0002FFEF File Offset: 0x0002E1EF
		[Editor(false)]
		public bool IsTransferDisabled
		{
			get
			{
				return this._isTransferDisabled;
			}
			set
			{
				if (this._isTransferDisabled != value)
				{
					this._isTransferDisabled = value;
					base.OnPropertyChanged(value, "IsTransferDisabled");
				}
			}
		}

		// Token: 0x040007DB RID: 2011
		private bool _isInCart;

		// Token: 0x040007DC RID: 2012
		private bool _isEligible;

		// Token: 0x040007DD RID: 2013
		private bool _isTransferDisabled;
	}
}
