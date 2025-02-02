using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameMenu
{
	// Token: 0x02000146 RID: 326
	public class GameMenuTroopSelectionItemButtonWidget : ButtonWidget
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000301DD File Offset: 0x0002E3DD
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x000301E5 File Offset: 0x0002E3E5
		public ButtonWidget AddButtonWidget { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000301EE File Offset: 0x0002E3EE
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x000301F6 File Offset: 0x0002E3F6
		public ButtonWidget RemoveButtonWidget { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000301FF File Offset: 0x0002E3FF
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00030207 File Offset: 0x0002E407
		public Widget CheckmarkVisualWidget { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00030210 File Offset: 0x0002E410
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x00030218 File Offset: 0x0002E418
		public Widget AddRemoveControls { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00030221 File Offset: 0x0002E421
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00030229 File Offset: 0x0002E429
		public Widget HeroHealthParent { get; set; }

		// Token: 0x06001143 RID: 4419 RVA: 0x00030232 File Offset: 0x0002E432
		public GameMenuTroopSelectionItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00030244 File Offset: 0x0002E444
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.AddButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnAdd));
				this.RemoveButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnRemove));
				this._initialized = true;
			}
			if (this._isDirty)
			{
				this.Refresh();
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000302AD File Offset: 0x0002E4AD
		private void OnRemove(Widget obj)
		{
			base.EventFired("Remove", Array.Empty<object>());
			this.Refresh();
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000302C5 File Offset: 0x0002E4C5
		private void OnAdd(Widget obj)
		{
			base.EventFired("Add", Array.Empty<object>());
			this.Refresh();
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000302DD File Offset: 0x0002E4DD
		protected override void HandleClick()
		{
			base.HandleClick();
			if (this.CurrentAmount == 0)
			{
				base.EventFired("Add", Array.Empty<object>());
			}
			else
			{
				base.EventFired("Remove", Array.Empty<object>());
			}
			this.Refresh();
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00030318 File Offset: 0x0002E518
		private void Refresh()
		{
			if (this.CheckmarkVisualWidget == null || this.AddRemoveControls == null || this.AddButtonWidget == null || this.RemoveButtonWidget == null)
			{
				return;
			}
			if (this.MaxAmount == 0)
			{
				base.DoNotAcceptEvents = false;
				base.DoNotPassEventsToChildren = true;
				this.CheckmarkVisualWidget.IsHidden = (this.CurrentAmount == 0);
				this.AddRemoveControls.IsHidden = true;
				this.AddButtonWidget.IsHidden = true;
				this.RemoveButtonWidget.IsHidden = true;
				base.IsDisabled = true;
				base.UpdateChildrenStates = true;
				base.DominantSelectedState = this.IsLocked;
				this.HeroHealthParent.IsHidden = !this.IsTroopHero;
				if (this.IsLocked)
				{
					base.IsDisabled = (this.CurrentAmount <= 0);
					base.DoNotPassEventsToChildren = true;
					base.DoNotAcceptEvents = true;
				}
			}
			else if (this.MaxAmount == 1)
			{
				base.DoNotAcceptEvents = false;
				base.DoNotPassEventsToChildren = true;
				this.CheckmarkVisualWidget.IsHidden = (this.CurrentAmount == 0);
				this.AddRemoveControls.IsHidden = true;
				this.AddButtonWidget.IsHidden = true;
				this.RemoveButtonWidget.IsHidden = true;
				base.IsDisabled = ((this.IsRosterFull && this.CurrentAmount <= 0) || this.IsLocked);
				base.UpdateChildrenStates = true;
				base.DominantSelectedState = this.IsLocked;
				this.HeroHealthParent.IsHidden = !this.IsTroopHero;
				if (this.IsLocked)
				{
					base.IsDisabled = (this.CurrentAmount <= 0);
					base.DoNotPassEventsToChildren = true;
					base.DoNotAcceptEvents = true;
				}
			}
			else
			{
				base.DoNotAcceptEvents = true;
				base.DoNotPassEventsToChildren = false;
				this.CheckmarkVisualWidget.IsHidden = true;
				this.AddRemoveControls.IsHidden = false;
				this.HeroHealthParent.IsHidden = true;
				this.AddButtonWidget.IsHidden = false;
				this.RemoveButtonWidget.IsHidden = false;
				this.AddButtonWidget.IsDisabled = (this.IsRosterFull || this.CurrentAmount >= this.MaxAmount);
				this.RemoveButtonWidget.IsDisabled = (this.CurrentAmount <= 0);
				base.UpdateChildrenStates = false;
				if (this.IsLocked)
				{
					base.IsDisabled = false;
					base.DoNotPassEventsToChildren = true;
					base.DoNotAcceptEvents = true;
				}
			}
			base.GamepadNavigationIndex = (this.AddRemoveControls.IsVisible ? -1 : 0);
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00030581 File Offset: 0x0002E781
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x00030589 File Offset: 0x0002E789
		public bool IsRosterFull
		{
			get
			{
				return this._isRosterFull;
			}
			set
			{
				if (this._isRosterFull != value)
				{
					this._isRosterFull = value;
					base.OnPropertyChanged(value, "IsRosterFull");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x000305AE File Offset: 0x0002E7AE
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x000305B6 File Offset: 0x0002E7B6
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (this._isLocked != value)
				{
					this._isLocked = value;
					base.OnPropertyChanged(value, "IsLocked");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000305DB File Offset: 0x0002E7DB
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x000305E3 File Offset: 0x0002E7E3
		public bool IsTroopHero
		{
			get
			{
				return this._isTroopHero;
			}
			set
			{
				if (this._isTroopHero != value)
				{
					this._isTroopHero = value;
					base.OnPropertyChanged(value, "IsTroopHero");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00030608 File Offset: 0x0002E808
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x00030610 File Offset: 0x0002E810
		public int CurrentAmount
		{
			get
			{
				return this._currentAmount;
			}
			set
			{
				if (this._currentAmount != value)
				{
					this._currentAmount = value;
					base.OnPropertyChanged(value, "CurrentAmount");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00030635 File Offset: 0x0002E835
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0003063D File Offset: 0x0002E83D
		public int MaxAmount
		{
			get
			{
				return this._maxAmount;
			}
			set
			{
				if (this._maxAmount != value)
				{
					this._maxAmount = value;
					base.OnPropertyChanged(value, "MaxAmount");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x040007EA RID: 2026
		private bool _initialized;

		// Token: 0x040007EB RID: 2027
		private bool _isDirty = true;

		// Token: 0x040007EC RID: 2028
		private int _maxAmount;

		// Token: 0x040007ED RID: 2029
		private int _currentAmount;

		// Token: 0x040007EE RID: 2030
		private bool _isRosterFull;

		// Token: 0x040007EF RID: 2031
		private bool _isLocked;

		// Token: 0x040007F0 RID: 2032
		private bool _isTroopHero;
	}
}
