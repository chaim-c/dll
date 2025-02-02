using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000105 RID: 261
	public class PowerLevelComparerWidget : Widget
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x00026449 File Offset: 0x00024649
		public PowerLevelComparerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00026454 File Offset: 0x00024654
		protected override void OnLateUpdate(float dt)
		{
			if (this.AttackerPowerWidget != null)
			{
				this.AttackerPowerWidget.AlphaFactor = 0.7f;
				this.AttackerPowerWidget.ValueFactor = -70f;
			}
			if (this.DefenderPowerWidget != null)
			{
				this.DefenderPowerWidget.AlphaFactor = 0.7f;
				this.DefenderPowerWidget.ValueFactor = -70f;
			}
			if (this._powerListPanel != null)
			{
				if (this._defenderSideInitialPowerLevelDescription == null)
				{
					this._defenderSideInitialPowerLevelDescription = new ContainerItemDescription();
					this._defenderSideInitialPowerLevelDescription.WidgetId = "DefenderSideInitialPowerLevel";
					this._powerListPanel.AddItemDescription(this._defenderSideInitialPowerLevelDescription);
				}
				if (this._attackerSideInitialPowerLevelDescription == null)
				{
					this._attackerSideInitialPowerLevelDescription = new ContainerItemDescription();
					this._attackerSideInitialPowerLevelDescription.WidgetId = "AttackerSideInitialPowerLevel";
					this._powerListPanel.AddItemDescription(this._attackerSideInitialPowerLevelDescription);
				}
			}
			if (this._defenderPowerListPanel != null)
			{
				if (this._defenderSidePowerLevelDescription == null)
				{
					this._defenderSidePowerLevelDescription = new ContainerItemDescription();
					this._defenderSidePowerLevelDescription.WidgetId = "DefenderSidePowerLevel";
					this._defenderPowerListPanel.AddItemDescription(this._defenderSidePowerLevelDescription);
				}
				if (this._defenderSideEmptyPowerLevelDescription == null)
				{
					this._defenderSideEmptyPowerLevelDescription = new ContainerItemDescription();
					this._defenderSideEmptyPowerLevelDescription.WidgetId = "DefenderSideEmptyPowerLevel";
					this._defenderPowerListPanel.AddItemDescription(this._defenderSideEmptyPowerLevelDescription);
				}
			}
			if (this._attackerPowerListPanel != null)
			{
				if (this._attackerSidePowerLevelDescription == null)
				{
					this._attackerSidePowerLevelDescription = new ContainerItemDescription();
					this._attackerSidePowerLevelDescription.WidgetId = "AttackerSidePowerLevel";
					this._attackerPowerListPanel.AddItemDescription(this._attackerSidePowerLevelDescription);
				}
				if (this._attackerSideEmptyPowerLevelDescription == null)
				{
					this._attackerSideEmptyPowerLevelDescription = new ContainerItemDescription();
					this._attackerSideEmptyPowerLevelDescription.WidgetId = "AttackerSideEmptyPowerLevel";
					this._attackerPowerListPanel.AddItemDescription(this._attackerSideEmptyPowerLevelDescription);
				}
			}
			if (this._defenderSideInitialPowerLevelDescription != null && this._attackerSideInitialPowerLevelDescription != null)
			{
				float num = (float)this.InitialDefenderBattlePower / (float)(this.InitialAttackerBattlePower + this.InitialDefenderBattlePower);
				float num2 = (float)this.InitialAttackerBattlePower / (float)(this.InitialAttackerBattlePower + this.InitialDefenderBattlePower);
				if (this._defenderSideInitialPowerLevelDescription.WidthStretchRatio != num || this._attackerSideInitialPowerLevelDescription.WidthStretchRatio != num2)
				{
					this._defenderSideInitialPowerLevelDescription.WidthStretchRatio = num;
					this._attackerSideInitialPowerLevelDescription.WidthStretchRatio = num2;
					base.SetMeasureAndLayoutDirty();
				}
			}
			if (this._defenderSidePowerLevelDescription != null && this._defenderSideEmptyPowerLevelDescription != null)
			{
				float num3 = 1f - (float)this.DefenderPower / (float)this.InitialDefenderBattlePower;
				float num4 = (float)this.DefenderPower / (float)this.InitialDefenderBattlePower;
				if (this._defenderSideEmptyPowerLevelDescription.WidthStretchRatio != num3 || this._defenderSidePowerLevelDescription.WidthStretchRatio != num4)
				{
					this._defenderSidePowerLevelDescription.WidthStretchRatio = num4;
					this._defenderSideEmptyPowerLevelDescription.WidthStretchRatio = num3;
					base.SetMeasureAndLayoutDirty();
				}
			}
			if (this._attackerSidePowerLevelDescription != null && this._attackerSideEmptyPowerLevelDescription != null)
			{
				float num5 = 1f - (float)this.AttackerPower / (float)this.InitialAttackerBattlePower;
				float num6 = (float)this.AttackerPower / (float)this.InitialAttackerBattlePower;
				if (this._attackerSidePowerLevelDescription.WidthStretchRatio != num6 || this._attackerSideEmptyPowerLevelDescription.WidthStretchRatio != num5)
				{
					this._attackerSidePowerLevelDescription.WidthStretchRatio = num6;
					this._attackerSideEmptyPowerLevelDescription.WidthStretchRatio = num5;
					base.SetMeasureAndLayoutDirty();
				}
			}
			if (this.IsCenterSeperatorEnabled && this.CenterSeperatorWidget != null)
			{
				this.CenterSeperatorWidget.ScaledPositionXOffset = this.AttackerPowerWidget.Size.X - (this.CenterSeperatorWidget.Size.X - this.CenterSpace) / 2f;
			}
			base.OnLateUpdate(dt);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000267AF File Offset: 0x000249AF
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x000267B7 File Offset: 0x000249B7
		[Editor(false)]
		public bool IsCenterSeperatorEnabled
		{
			get
			{
				return this._isCenterSeperatorEnabled;
			}
			set
			{
				if (this._isCenterSeperatorEnabled != value)
				{
					this._isCenterSeperatorEnabled = value;
					base.OnPropertyChanged(value, "IsCenterSeperatorEnabled");
				}
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000267D5 File Offset: 0x000249D5
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x000267DD File Offset: 0x000249DD
		[Editor(false)]
		public float CenterSpace
		{
			get
			{
				return this._centerSpace;
			}
			set
			{
				if (this._centerSpace != value)
				{
					this._centerSpace = value;
					base.OnPropertyChanged(value, "CenterSpace");
				}
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000267FB File Offset: 0x000249FB
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00026803 File Offset: 0x00024A03
		[Editor(false)]
		public double DefenderPower
		{
			get
			{
				return this._defenderPower;
			}
			set
			{
				if (this._defenderPower != value && !double.IsNaN(value))
				{
					this._defenderPower = value;
					base.OnPropertyChanged(value, "DefenderPower");
				}
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00026829 File Offset: 0x00024A29
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00026831 File Offset: 0x00024A31
		[Editor(false)]
		public double AttackerPower
		{
			get
			{
				return this._attackerPower;
			}
			set
			{
				if (this._attackerPower != value && !double.IsNaN(value))
				{
					this._attackerPower = value;
					base.OnPropertyChanged(value, "AttackerPower");
				}
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00026857 File Offset: 0x00024A57
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0002685F File Offset: 0x00024A5F
		[Editor(false)]
		public double InitialAttackerBattlePower
		{
			get
			{
				return this._initialAttackerBattlePower;
			}
			set
			{
				if (this._initialAttackerBattlePower != value && !double.IsNaN(value))
				{
					this._initialAttackerBattlePower = value;
					base.OnPropertyChanged(value, "InitialAttackerBattlePower");
				}
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00026885 File Offset: 0x00024A85
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0002688D File Offset: 0x00024A8D
		[Editor(false)]
		public double InitialDefenderBattlePower
		{
			get
			{
				return this._initialDefenderBattlePower;
			}
			set
			{
				if (this._initialDefenderBattlePower != value && !double.IsNaN(value))
				{
					this._initialDefenderBattlePower = value;
					base.OnPropertyChanged(value, "InitialDefenderBattlePower");
				}
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x000268B3 File Offset: 0x00024AB3
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x000268BB File Offset: 0x00024ABB
		[Editor(false)]
		public Widget AttackerPowerWidget
		{
			get
			{
				return this._attackerPowerWidget;
			}
			set
			{
				if (this._attackerPowerWidget != value)
				{
					this._attackerPowerWidget = value;
					base.OnPropertyChanged<Widget>(value, "AttackerPowerWidget");
				}
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x000268D9 File Offset: 0x00024AD9
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x000268E1 File Offset: 0x00024AE1
		[Editor(false)]
		public Widget DefenderPowerWidget
		{
			get
			{
				return this._defenderPowerWidget;
			}
			set
			{
				if (this._defenderPowerWidget != value)
				{
					this._defenderPowerWidget = value;
					base.OnPropertyChanged<Widget>(value, "DefenderPowerWidget");
				}
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000268FF File Offset: 0x00024AFF
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00026907 File Offset: 0x00024B07
		[Editor(false)]
		public ListPanel PowerListPanel
		{
			get
			{
				return this._powerListPanel;
			}
			set
			{
				if (this._powerListPanel != value)
				{
					this._powerListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "PowerListPanel");
				}
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00026925 File Offset: 0x00024B25
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0002692D File Offset: 0x00024B2D
		[Editor(false)]
		public ListPanel AttackerPowerListPanel
		{
			get
			{
				return this._attackerPowerListPanel;
			}
			set
			{
				if (this._attackerPowerListPanel != value)
				{
					this._attackerPowerListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "AttackerPowerListPanel");
				}
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0002694B File Offset: 0x00024B4B
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x00026953 File Offset: 0x00024B53
		[Editor(false)]
		public ListPanel DefenderPowerListPanel
		{
			get
			{
				return this._defenderPowerListPanel;
			}
			set
			{
				if (this._defenderPowerListPanel != value)
				{
					this._defenderPowerListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "DefenderPowerListPanel");
				}
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00026971 File Offset: 0x00024B71
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00026979 File Offset: 0x00024B79
		[Editor(false)]
		public Widget CenterSeperatorWidget
		{
			get
			{
				return this._centerSeperatorWidget;
			}
			set
			{
				if (this._centerSeperatorWidget != value)
				{
					this._centerSeperatorWidget = value;
					base.OnPropertyChanged<Widget>(value, "CenterSeperatorWidget");
				}
			}
		}

		// Token: 0x04000659 RID: 1625
		private Widget _centerSeperatorWidget;

		// Token: 0x0400065A RID: 1626
		private bool _isCenterSeperatorEnabled;

		// Token: 0x0400065B RID: 1627
		private float _centerSpace;

		// Token: 0x0400065C RID: 1628
		private double _defenderPower;

		// Token: 0x0400065D RID: 1629
		private double _attackerPower;

		// Token: 0x0400065E RID: 1630
		private double _initialAttackerBattlePower;

		// Token: 0x0400065F RID: 1631
		private double _initialDefenderBattlePower;

		// Token: 0x04000660 RID: 1632
		private Widget _defenderPowerWidget;

		// Token: 0x04000661 RID: 1633
		private Widget _attackerPowerWidget;

		// Token: 0x04000662 RID: 1634
		private ListPanel _powerListPanel;

		// Token: 0x04000663 RID: 1635
		private ListPanel _defenderPowerListPanel;

		// Token: 0x04000664 RID: 1636
		private ListPanel _attackerPowerListPanel;

		// Token: 0x04000665 RID: 1637
		private ContainerItemDescription _defenderSideInitialPowerLevelDescription;

		// Token: 0x04000666 RID: 1638
		private ContainerItemDescription _attackerSideInitialPowerLevelDescription;

		// Token: 0x04000667 RID: 1639
		private ContainerItemDescription _defenderSidePowerLevelDescription;

		// Token: 0x04000668 RID: 1640
		private ContainerItemDescription _defenderSideEmptyPowerLevelDescription;

		// Token: 0x04000669 RID: 1641
		private ContainerItemDescription _attackerSidePowerLevelDescription;

		// Token: 0x0400066A RID: 1642
		private ContainerItemDescription _attackerSideEmptyPowerLevelDescription;
	}
}
