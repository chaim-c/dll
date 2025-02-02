using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x0200006C RID: 108
	public class OrderSiegeMachineItemButtonWidget : ButtonWidget
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001138F File Offset: 0x0000F58F
		public OrderSiegeMachineItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000113A6 File Offset: 0x0000F5A6
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isVisualsDirty)
			{
				this.MachineIconWidgetChanged();
				this.UpdateRemainingCount();
				this.UpdateMachineIcon();
				this._isVisualsDirty = false;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000113D0 File Offset: 0x0000F5D0
		private void MachineIconWidgetChanged()
		{
			if (this.MachineIconWidget == null)
			{
				return;
			}
			this.MachineIconWidget.RegisterBrushStatesOfWidget();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000113E8 File Offset: 0x0000F5E8
		private void UpdateMachineIcon()
		{
			if (this.MachineIconWidget == null)
			{
				return;
			}
			this._isRemainingCountVisible = true;
			string machineClass = this.MachineClass;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(machineClass);
			if (num <= 1056090379U)
			{
				if (num <= 390431385U)
				{
					if (num <= 6339497U)
					{
						if (num != 0U)
						{
							if (num == 6339497U)
							{
								if (machineClass == "ladder")
								{
									this.MachineIconWidget.SetState("Ladder");
									return;
								}
							}
						}
						else if (machineClass != null)
						{
						}
					}
					else if (num != 354578048U)
					{
						if (num == 390431385U)
						{
							if (machineClass == "bricole")
							{
								this.MachineIconWidget.SetState("Bricole");
								return;
							}
						}
					}
					else if (machineClass == "Mangonel")
					{
						this.MachineIconWidget.SetState("Mangonel");
						return;
					}
				}
				else if (num <= 729368230U)
				{
					if (num != 616782878U)
					{
						if (num == 729368230U)
						{
							if (machineClass == "siege_tower_level1")
							{
								this.MachineIconWidget.SetState("SiegeTower");
								return;
							}
						}
					}
					else if (machineClass == "improved_ram")
					{
						this.MachineIconWidget.SetState("ImprovedRam");
						return;
					}
				}
				else if (num != 808481256U)
				{
					if (num == 1056090379U)
					{
						if (machineClass == "preparations")
						{
							this.MachineIconWidget.SetState("Preparations");
							return;
						}
					}
				}
				else if (machineClass == "fire_ballista")
				{
					this.MachineIconWidget.SetState("FireBallista");
					return;
				}
			}
			else if (num <= 1839032341U)
			{
				if (num <= 1748194790U)
				{
					if (num != 1241455715U)
					{
						if (num == 1748194790U)
						{
							if (machineClass == "fire_catapult")
							{
								this.MachineIconWidget.SetState("FireCatapult");
								return;
							}
						}
					}
					else if (machineClass == "ram")
					{
						this.MachineIconWidget.SetState("Ram");
						return;
					}
				}
				else if (num != 1820818168U)
				{
					if (num == 1839032341U)
					{
						if (machineClass == "trebuchet")
						{
							this.MachineIconWidget.SetState("Trebuchet");
							return;
						}
					}
				}
				else if (machineClass == "fire_onager")
				{
					this.MachineIconWidget.SetState("FireOnager");
					return;
				}
			}
			else if (num <= 1898442385U)
			{
				if (num != 1844264380U)
				{
					if (num == 1898442385U)
					{
						if (machineClass == "catapult")
						{
							this.MachineIconWidget.SetState("Catapult");
							return;
						}
					}
				}
				else if (machineClass == "FireMangonel")
				{
					this.MachineIconWidget.SetState("FireMangonel");
					return;
				}
			}
			else if (num != 2166136261U)
			{
				if (num != 2806198843U)
				{
					if (num == 4036530155U)
					{
						if (machineClass == "ballista")
						{
							this.MachineIconWidget.SetState("Ballista");
							return;
						}
					}
				}
				else if (machineClass == "onager")
				{
					this.MachineIconWidget.SetState("Onager");
					return;
				}
			}
			else if (machineClass != null && machineClass.Length != 0)
			{
			}
			this.MachineIconWidget.SetState("None");
			this._isRemainingCountVisible = false;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00011798 File Offset: 0x0000F998
		private void UpdateRemainingCount()
		{
			if (this.RemainingCountWidget == null)
			{
				return;
			}
			base.IsDisabled = (this.RemainingCount == 0);
			this.RemainingCountWidget.IsVisible = this._isRemainingCountVisible;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000117C3 File Offset: 0x0000F9C3
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x000117CB File Offset: 0x0000F9CB
		[Editor(false)]
		public int RemainingCount
		{
			get
			{
				return this._remainingCount;
			}
			set
			{
				if (this._remainingCount != value)
				{
					this._remainingCount = value;
					base.OnPropertyChanged(value, "RemainingCount");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000117F0 File Offset: 0x0000F9F0
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x000117F8 File Offset: 0x0000F9F8
		[Editor(false)]
		public TextWidget RemainingCountWidget
		{
			get
			{
				return this._remainingCountWidget;
			}
			set
			{
				if (this._remainingCountWidget != value)
				{
					this._remainingCountWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "RemainingCountWidget");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001181D File Offset: 0x0000FA1D
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00011825 File Offset: 0x0000FA25
		[Editor(false)]
		public string MachineClass
		{
			get
			{
				return this._machineClass;
			}
			set
			{
				if (this._machineClass != value)
				{
					this._machineClass = value;
					base.OnPropertyChanged<string>(value, "MachineClass");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001184F File Offset: 0x0000FA4F
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00011857 File Offset: 0x0000FA57
		[Editor(false)]
		public Widget MachineIconWidget
		{
			get
			{
				return this._machineIconWidget;
			}
			set
			{
				if (this._machineIconWidget != value)
				{
					this._machineIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "MachineIconWidget");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x0400027D RID: 637
		private bool _isRemainingCountVisible = true;

		// Token: 0x0400027E RID: 638
		private bool _isVisualsDirty = true;

		// Token: 0x0400027F RID: 639
		private int _remainingCount;

		// Token: 0x04000280 RID: 640
		private TextWidget _remainingCountWidget;

		// Token: 0x04000281 RID: 641
		private string _machineClass;

		// Token: 0x04000282 RID: 642
		private Widget _machineIconWidget;
	}
}
