using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x0200006A RID: 106
	public class OrderSiegeDeploymentItemButtonWidget : ButtonWidget
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00010F23 File Offset: 0x0000F123
		public OrderSiegeDeploymentItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00010F34 File Offset: 0x0000F134
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.IsVisible = (this.IsInsideWindow && this.IsInFront);
			base.IsEnabled = (this.IsPlayerGeneral && this.PointType != 2);
			if (this.preSelectedState != base.IsSelected)
			{
				if (base.IsSelected)
				{
					this.ScreenWidget.SetSelectedDeploymentItem(this);
				}
				this.preSelectedState = base.IsSelected;
			}
			if (this._isVisualsDirty)
			{
				this.UpdateTypeVisuals();
				this._isVisualsDirty = false;
			}
			this.UpdatePosition();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		private void UpdatePosition()
		{
			if (this.IsInsideWindow)
			{
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f - base.EventManager.LeftUsableAreaStart;
				base.ScaledPositionYOffset = this.Position.y - base.Size.Y - base.EventManager.TopUsableAreaStart;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00011038 File Offset: 0x0000F238
		private void UpdateTypeVisuals()
		{
			this.TypeIconWidget.RegisterBrushStatesOfWidget();
			this.BreachedTextWidget.IsVisible = (this.PointType == 2);
			this.TypeIconWidget.IsVisible = (this.PointType != 2);
			if (this.PointType == 0)
			{
				this.TypeIconWidget.SetState("BatteringRam");
				return;
			}
			if (this.PointType == 1)
			{
				this.TypeIconWidget.SetState("TowerLadder");
				return;
			}
			if (this.PointType == 2)
			{
				this.TypeIconWidget.SetState("Breach");
				return;
			}
			if (this.PointType == 3)
			{
				this.TypeIconWidget.SetState("Ranged");
				return;
			}
			this.TypeIconWidget.SetState("Default");
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000110F2 File Offset: 0x0000F2F2
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x000110FA File Offset: 0x0000F2FA
		[Editor(false)]
		public TextWidget BreachedTextWidget
		{
			get
			{
				return this._breachedTextWidget;
			}
			set
			{
				if (this._breachedTextWidget != value)
				{
					this._breachedTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "BreachedTextWidget");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001111F File Offset: 0x0000F31F
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00011127 File Offset: 0x0000F327
		[Editor(false)]
		public Widget TypeIconWidget
		{
			get
			{
				return this._typeIconWidget;
			}
			set
			{
				if (this._typeIconWidget != value)
				{
					this._typeIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "TypeIconWidget");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001114C File Offset: 0x0000F34C
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00011154 File Offset: 0x0000F354
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00011177 File Offset: 0x0000F377
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001117F File Offset: 0x0000F37F
		public int PointType
		{
			get
			{
				return this._pointType;
			}
			set
			{
				if (this._pointType != value)
				{
					this._pointType = value;
					base.OnPropertyChanged(value, "PointType");
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001119D File Offset: 0x0000F39D
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000111A5 File Offset: 0x0000F3A5
		public bool IsInsideWindow
		{
			get
			{
				return this._isInsideWindow;
			}
			set
			{
				if (this._isInsideWindow != value)
				{
					this._isInsideWindow = value;
					base.OnPropertyChanged(value, "IsInsideWindow");
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000111C3 File Offset: 0x0000F3C3
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000111CB File Offset: 0x0000F3CB
		public bool IsInFront
		{
			get
			{
				return this._isInFront;
			}
			set
			{
				if (this._isInFront != value)
				{
					this._isInFront = value;
					base.OnPropertyChanged(value, "IsInFront");
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000111E9 File Offset: 0x0000F3E9
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000111F1 File Offset: 0x0000F3F1
		public bool IsPlayerGeneral
		{
			get
			{
				return this._isPlayerGeneral;
			}
			set
			{
				if (this._isPlayerGeneral != value)
				{
					this._isPlayerGeneral = value;
					base.OnPropertyChanged(value, "IsPlayerGeneral");
				}
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001120F File Offset: 0x0000F40F
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00011217 File Offset: 0x0000F417
		public OrderSiegeDeploymentScreenWidget ScreenWidget
		{
			get
			{
				return this._screenWidget;
			}
			set
			{
				if (this._screenWidget != value)
				{
					this._screenWidget = value;
					base.OnPropertyChanged<OrderSiegeDeploymentScreenWidget>(value, "ScreenWidget");
				}
			}
		}

		// Token: 0x04000270 RID: 624
		private bool preSelectedState;

		// Token: 0x04000271 RID: 625
		private bool _isVisualsDirty = true;

		// Token: 0x04000272 RID: 626
		private Vec2 _position;

		// Token: 0x04000273 RID: 627
		private bool _isInsideWindow;

		// Token: 0x04000274 RID: 628
		private bool _isInFront;

		// Token: 0x04000275 RID: 629
		private bool _isPlayerGeneral;

		// Token: 0x04000276 RID: 630
		private OrderSiegeDeploymentScreenWidget _screenWidget;

		// Token: 0x04000277 RID: 631
		private int _pointType;

		// Token: 0x04000278 RID: 632
		private Widget _typeIconWidget;

		// Token: 0x04000279 RID: 633
		private TextWidget _breachedTextWidget;
	}
}
