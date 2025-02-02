using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameMenu
{
	// Token: 0x02000147 RID: 327
	public class GameMenuWidget : Widget
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00030662 File Offset: 0x0002E862
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0003066A File Offset: 0x0002E86A
		public int EncounterModeMenuWidth { get; set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00030673 File Offset: 0x0002E873
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0003067B File Offset: 0x0002E87B
		public int EncounterModeMenuHeight { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00030684 File Offset: 0x0002E884
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003068C File Offset: 0x0002E88C
		public int EncounterModeMenuMarginTop { get; set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00030695 File Offset: 0x0002E895
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0003069D File Offset: 0x0002E89D
		public int NormalModeMenuWidth { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000306A6 File Offset: 0x0002E8A6
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x000306AE File Offset: 0x0002E8AE
		public int NormalModeMenuHeight { get; set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000306B7 File Offset: 0x0002E8B7
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x000306BF File Offset: 0x0002E8BF
		public int NormalModeMenuMarginTop { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000306C8 File Offset: 0x0002E8C8
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x000306D0 File Offset: 0x0002E8D0
		public bool IsOverlayExtended
		{
			get
			{
				return this._isOverlayExtended;
			}
			private set
			{
				if (value != this._isOverlayExtended)
				{
					this._isOverlayExtended = value;
					this.UpdateOverlayState();
				}
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000306E8 File Offset: 0x0002E8E8
		public GameMenuWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00030700 File Offset: 0x0002E900
		protected override void OnLateUpdate(float dt)
		{
			if (!this._firstFrame)
			{
				if (this.IsNight)
				{
					base.Color = Color.Lerp(base.Color, new Color(0.23921569f, 0.4509804f, 0.8f, 1f), dt);
				}
				else
				{
					base.Color = Color.Lerp(base.Color, Color.White, dt);
				}
			}
			else
			{
				if (this.IsNight)
				{
					base.Color = new Color(0.23921569f, 0.4509804f, 0.8f, 1f);
				}
				else
				{
					base.Color = Color.White;
				}
				this._firstFrame = false;
				this.RefreshSize();
			}
			base.OnLateUpdate(dt);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000307AC File Offset: 0x0002E9AC
		private void RefreshSize()
		{
			base.SuggestedWidth = (float)(this.IsEncounterMenu ? this.EncounterModeMenuWidth : this.NormalModeMenuWidth);
			base.SuggestedHeight = (float)(this.IsEncounterMenu ? this.EncounterModeMenuHeight : this.NormalModeMenuHeight);
			base.ScaledSuggestedWidth = base.SuggestedWidth * base._scaleToUse;
			base.ScaledSuggestedHeight = base.SuggestedHeight * base._scaleToUse;
			base.MarginTop = (float)(this.IsEncounterMenu ? this.EncounterModeMenuMarginTop : this.NormalModeMenuMarginTop);
			this.ExtendButtonWidget.MarginTop = base.MarginTop;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00030847 File Offset: 0x0002EA47
		private void OnExtendButtonClick(Widget button)
		{
			this.IsOverlayExtended = !this.IsOverlayExtended;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00030858 File Offset: 0x0002EA58
		public void UpdateOverlayState()
		{
			this.ScopeTargeter.IsScopeEnabled = this._isOverlayExtended;
			string state = this._isOverlayExtended ? "Default" : "Disabled";
			this.Overlay.SetState(state);
			foreach (Style style in this.ExtendButtonArrowWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].HorizontalFlip = !this._isOverlayExtended;
				}
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00030904 File Offset: 0x0002EB04
		private void TitleTextWidget_PropertyChanged(PropertyOwnerObject widget, string propertyName, object propertyValue)
		{
			if (propertyName == "Text")
			{
				this.TitleContainerWidget.IsVisible = !string.IsNullOrEmpty((string)propertyValue);
				this.IsOverlayExtended = true;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00030933 File Offset: 0x0002EB33
		private void OnOptionAdded(Widget parentWidget, Widget childWidget)
		{
			GameMenuItemWidget gameMenuItemWidget = childWidget as GameMenuItemWidget;
			gameMenuItemWidget.OnOptionStateChanged = (Action)Delegate.Combine(gameMenuItemWidget.OnOptionStateChanged, new Action(this.OnOptionStateChanged));
			this.IsOverlayExtended = true;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00030963 File Offset: 0x0002EB63
		public void OnOptionStateChanged()
		{
			this.IsOverlayExtended = true;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003096C File Offset: 0x0002EB6C
		private void OnOptionRemoved(Widget widget, Widget child)
		{
			this.IsOverlayExtended = true;
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00030975 File Offset: 0x0002EB75
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x0003097D File Offset: 0x0002EB7D
		[Editor(false)]
		public NavigationScopeTargeter ScopeTargeter
		{
			get
			{
				return this._scopeTargeter;
			}
			set
			{
				if (this._scopeTargeter != value)
				{
					this._scopeTargeter = value;
					base.OnPropertyChanged<NavigationScopeTargeter>(value, "ScopeTargeter");
				}
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0003099B File Offset: 0x0002EB9B
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x000309A3 File Offset: 0x0002EBA3
		[Editor(false)]
		public TextWidget TitleTextWidget
		{
			get
			{
				return this._titleTextWidget;
			}
			set
			{
				if (this._titleTextWidget != value)
				{
					this._titleTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "TitleTextWidget");
					if (value != null)
					{
						value.PropertyChanged += this.TitleTextWidget_PropertyChanged;
					}
				}
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000309D6 File Offset: 0x0002EBD6
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x000309DE File Offset: 0x0002EBDE
		[Editor(false)]
		public Widget TitleContainerWidget
		{
			get
			{
				return this._titleContainerWidget;
			}
			set
			{
				if (this._titleContainerWidget != value)
				{
					this._titleContainerWidget = value;
					base.OnPropertyChanged<Widget>(value, "TitleContainerWidget");
				}
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x000309FC File Offset: 0x0002EBFC
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x00030A04 File Offset: 0x0002EC04
		[Editor(false)]
		public bool IsNight
		{
			get
			{
				return this._isNight;
			}
			set
			{
				if (this._isNight != value)
				{
					this._isNight = value;
					base.OnPropertyChanged(value, "IsNight");
				}
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00030A22 File Offset: 0x0002EC22
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00030A2A File Offset: 0x0002EC2A
		[Editor(false)]
		public bool IsEncounterMenu
		{
			get
			{
				return this._isEncounterMenu;
			}
			set
			{
				if (this._isEncounterMenu != value)
				{
					this._isEncounterMenu = value;
					base.OnPropertyChanged(value, "IsEncounterMenu");
					this.RefreshSize();
				}
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00030A4E File Offset: 0x0002EC4E
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00030A56 File Offset: 0x0002EC56
		[Editor(false)]
		public Widget Overlay
		{
			get
			{
				return this._overlay;
			}
			set
			{
				if (value != this._overlay)
				{
					this._overlay = value;
					base.OnPropertyChanged<Widget>(value, "Overlay");
				}
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00030A74 File Offset: 0x0002EC74
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00030A7C File Offset: 0x0002EC7C
		[Editor(false)]
		public ButtonWidget ExtendButtonWidget
		{
			get
			{
				return this._extendButtonWidget;
			}
			set
			{
				if (this._extendButtonWidget != value)
				{
					this._extendButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ExtendButtonWidget");
					if (this._extendButtonWidget != null)
					{
						this._extendButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnExtendButtonClick));
					}
				}
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00030AC9 File Offset: 0x0002ECC9
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00030AD1 File Offset: 0x0002ECD1
		[Editor(false)]
		public BrushWidget ExtendButtonArrowWidget
		{
			get
			{
				return this._extendButtonArrowWidget;
			}
			set
			{
				if (value != this._extendButtonArrowWidget)
				{
					this._extendButtonArrowWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "ExtendButtonArrowWidget");
				}
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00030AEF File Offset: 0x0002ECEF
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00030AF8 File Offset: 0x0002ECF8
		[Editor(false)]
		public ListPanel OptionItemsList
		{
			get
			{
				return this._optionItemsList;
			}
			set
			{
				if (value != this._optionItemsList)
				{
					this._optionItemsList = value;
					this._optionItemsList.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnOptionAdded));
					this._optionItemsList.ItemRemoveEventHandlers.Add(new Action<Widget, Widget>(this.OnOptionRemoved));
					base.OnPropertyChanged<ListPanel>(value, "OptionItemsList");
				}
			}
		}

		// Token: 0x040007F1 RID: 2033
		private bool _firstFrame = true;

		// Token: 0x040007F8 RID: 2040
		private const string _extendedState = "Default";

		// Token: 0x040007F9 RID: 2041
		private const string _hiddenState = "Disabled";

		// Token: 0x040007FA RID: 2042
		private bool _isOverlayExtended = true;

		// Token: 0x040007FB RID: 2043
		private NavigationScopeTargeter _scopeTargeter;

		// Token: 0x040007FC RID: 2044
		private TextWidget _titleTextWidget;

		// Token: 0x040007FD RID: 2045
		private Widget _titleContainerWidget;

		// Token: 0x040007FE RID: 2046
		private bool _isNight;

		// Token: 0x040007FF RID: 2047
		private bool _isEncounterMenu;

		// Token: 0x04000800 RID: 2048
		private Widget _overlay;

		// Token: 0x04000801 RID: 2049
		private ButtonWidget _extendButtonWidget;

		// Token: 0x04000802 RID: 2050
		private BrushWidget _extendButtonArrowWidget;

		// Token: 0x04000803 RID: 2051
		private ListPanel _optionItemsList;
	}
}
