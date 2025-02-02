using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options
{
	// Token: 0x0200006F RID: 111
	public class OptionsItemWidget : Widget
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00011BAA File Offset: 0x0000FDAA
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		public Widget BooleanOption { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00011BBB File Offset: 0x0000FDBB
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00011BC3 File Offset: 0x0000FDC3
		public Widget NumericOption { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00011BCC File Offset: 0x0000FDCC
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public Widget StringOption { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00011BDD File Offset: 0x0000FDDD
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00011BE5 File Offset: 0x0000FDE5
		public Widget GameKeyOption { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00011BEE File Offset: 0x0000FDEE
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00011BF6 File Offset: 0x0000FDF6
		public Widget ActionOption { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00011BFF File Offset: 0x0000FDFF
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00011C07 File Offset: 0x0000FE07
		public Widget InputOption { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00011C10 File Offset: 0x0000FE10
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00011C18 File Offset: 0x0000FE18
		public AnimatedDropdownWidget DropdownWidget
		{
			get
			{
				return this._dropdownWidget;
			}
			set
			{
				if (value != this._dropdownWidget)
				{
					this._dropdownWidget = value;
				}
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00011C2A File Offset: 0x0000FE2A
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00011C32 File Offset: 0x0000FE32
		public ButtonWidget BooleanToggleButtonWidget
		{
			get
			{
				return this._booleanToggleButtonWidget;
			}
			set
			{
				if (value != this._booleanToggleButtonWidget)
				{
					this._booleanToggleButtonWidget = value;
				}
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00011C44 File Offset: 0x0000FE44
		public OptionsItemWidget(UIContext context) : base(context)
		{
			this._optionTypeID = -1;
			this._graphicsSprites = new List<Sprite>();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00011C68 File Offset: 0x0000FE68
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.SetCurrentScreenWidget(this.FindScreenWidget(base.ParentWidget));
				if (this.ImageIDs != null)
				{
					for (int i = 0; i < this.ImageIDs.Length; i++)
					{
						if (this.ImageIDs[i] != string.Empty)
						{
							Sprite sprite = base.Context.SpriteData.GetSprite(this.ImageIDs[i]);
							this._graphicsSprites.Add(sprite);
						}
					}
				}
				this.RefreshVisibilityOfSubItems();
				this.ResetNavigationIndices();
				this._initialized = true;
			}
			if (!this._eventsRegistered)
			{
				this.RegisterHoverEvents();
				this._eventsRegistered = true;
			}
			if (this._isEnabledStateDirty)
			{
				Widget currentOptionWidget = this.GetCurrentOptionWidget();
				if (currentOptionWidget != null)
				{
					foreach (Widget widget in currentOptionWidget.AllChildrenAndThis)
					{
						widget.IsEnabled = this.IsOptionEnabled;
					}
				}
				this._isEnabledStateDirty = false;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00011D70 File Offset: 0x0000FF70
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			this.SetCurrentOption(false, false, -1);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00011D81 File Offset: 0x0000FF81
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			this.ResetCurrentOption();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00011D90 File Offset: 0x0000FF90
		private OptionsScreenWidget FindScreenWidget(Widget parent)
		{
			OptionsScreenWidget result;
			if ((result = (parent as OptionsScreenWidget)) != null)
			{
				return result;
			}
			if (parent == null)
			{
				return null;
			}
			return this.FindScreenWidget(parent.ParentWidget);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00011DBC File Offset: 0x0000FFBC
		private void SetCurrentOption(bool fromHoverOverDropdown, bool fromBooleanSelection, int hoverDropdownItemIndex = -1)
		{
			if (this._optionTypeID == 3)
			{
				Sprite newgraphicsSprite;
				if (fromHoverOverDropdown)
				{
					newgraphicsSprite = ((this._graphicsSprites.Count > hoverDropdownItemIndex) ? this._graphicsSprites[hoverDropdownItemIndex] : null);
				}
				else
				{
					newgraphicsSprite = ((this._graphicsSprites.Count > this.DropdownWidget.CurrentSelectedIndex && this.DropdownWidget.CurrentSelectedIndex >= 0) ? this._graphicsSprites[this.DropdownWidget.CurrentSelectedIndex] : null);
				}
				OptionsScreenWidget screenWidget = this._screenWidget;
				if (screenWidget == null)
				{
					return;
				}
				screenWidget.SetCurrentOption(this, newgraphicsSprite);
				return;
			}
			else if (this._optionTypeID == 0)
			{
				int num = this.BooleanToggleButtonWidget.IsSelected ? 0 : 1;
				Sprite newgraphicsSprite2 = (this._graphicsSprites.Count > num) ? this._graphicsSprites[num] : null;
				OptionsScreenWidget screenWidget2 = this._screenWidget;
				if (screenWidget2 == null)
				{
					return;
				}
				screenWidget2.SetCurrentOption(this, newgraphicsSprite2);
				return;
			}
			else
			{
				OptionsScreenWidget screenWidget3 = this._screenWidget;
				if (screenWidget3 == null)
				{
					return;
				}
				screenWidget3.SetCurrentOption(this, null);
				return;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00011EA7 File Offset: 0x000100A7
		public void SetCurrentScreenWidget(OptionsScreenWidget screenWidget)
		{
			this._screenWidget = screenWidget;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00011EB0 File Offset: 0x000100B0
		private void ResetCurrentOption()
		{
			OptionsScreenWidget screenWidget = this._screenWidget;
			if (screenWidget == null)
			{
				return;
			}
			screenWidget.SetCurrentOption(null, null);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00011EC4 File Offset: 0x000100C4
		private void RegisterHoverEvents()
		{
			foreach (Widget widget in base.AllChildren)
			{
				widget.boolPropertyChanged += this.Child_PropertyChanged;
			}
			if (this.OptionTypeID == 0)
			{
				this.BooleanToggleButtonWidget.boolPropertyChanged += this.BooleanOption_PropertyChanged;
				return;
			}
			if (this.OptionTypeID == 3)
			{
				this._dropdownExtensionParentWidget = this.DropdownWidget.DropdownClipWidget;
				foreach (Widget widget2 in this._dropdownExtensionParentWidget.AllChildren)
				{
					widget2.boolPropertyChanged += this.DropdownItem_PropertyChanged1;
				}
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00011FA0 File Offset: 0x000101A0
		private void BooleanOption_PropertyChanged(PropertyOwnerObject childWidget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsSelected")
			{
				this.SetCurrentOption(false, true, -1);
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00011FB8 File Offset: 0x000101B8
		private void Child_PropertyChanged(PropertyOwnerObject childWidget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsHovered")
			{
				if (propertyValue)
				{
					this.SetCurrentOption(false, false, -1);
					return;
				}
				this.ResetCurrentOption();
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00011FDC File Offset: 0x000101DC
		private void DropdownItem_PropertyChanged1(PropertyOwnerObject childWidget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsHovered")
			{
				if (propertyValue)
				{
					Widget widget = childWidget as Widget;
					this.SetCurrentOption(true, false, widget.ParentWidget.GetChildIndex(widget));
					return;
				}
				this.ResetCurrentOption();
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001201C File Offset: 0x0001021C
		private void RefreshVisibilityOfSubItems()
		{
			this.BooleanOption.IsVisible = (this.OptionTypeID == 0);
			this.NumericOption.IsVisible = (this.OptionTypeID == 1);
			this.StringOption.IsVisible = (this.OptionTypeID == 3);
			this.GameKeyOption.IsVisible = (this.OptionTypeID == 2);
			this.InputOption.IsVisible = (this.OptionTypeID == 4);
			if (this.ActionOption != null)
			{
				this.ActionOption.IsVisible = (this.OptionTypeID == 5);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000120AC File Offset: 0x000102AC
		private Widget GetCurrentOptionWidget()
		{
			switch (this.OptionTypeID)
			{
			case 0:
				return this.BooleanOption;
			case 1:
				return this.NumericOption;
			case 2:
				return this.StringOption;
			case 3:
				return this.GameKeyOption;
			case 4:
				return this.InputOption;
			case 5:
				return this.ActionOption;
			default:
				return null;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001210C File Offset: 0x0001030C
		private void ResetNavigationIndices()
		{
			if (base.GamepadNavigationIndex == -1)
			{
				return;
			}
			bool flag = false;
			Widget booleanOption = this.BooleanOption;
			if (booleanOption != null && booleanOption.IsVisible)
			{
				this.BooleanOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
				flag = true;
			}
			else
			{
				Widget numericOption = this.NumericOption;
				if (numericOption != null && numericOption.IsVisible)
				{
					this.NumericOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
					flag = true;
				}
				else
				{
					Widget stringOption = this.StringOption;
					if (stringOption != null && stringOption.IsVisible)
					{
						this.StringOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
						flag = true;
					}
					else
					{
						Widget gameKeyOption = this.GameKeyOption;
						if (gameKeyOption != null && gameKeyOption.IsVisible)
						{
							this.GameKeyOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
							flag = true;
						}
						else
						{
							Widget inputOption = this.InputOption;
							if (inputOption != null && inputOption.IsVisible)
							{
								this.InputOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
								flag = true;
							}
							else
							{
								Widget actionOption = this.ActionOption;
								if (actionOption != null && actionOption.IsVisible)
								{
									this.ActionOption.GamepadNavigationIndex = base.GamepadNavigationIndex;
									flag = true;
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				base.GamepadNavigationIndex = -1;
				return;
			}
			Debug.FailedAssert("No option type is visible for: " + base.GetType().Name, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Options\\OptionsItemWidget.cs", "ResetNavigationIndices", 325);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00012253 File Offset: 0x00010453
		protected override void OnGamepadNavigationIndexUpdated(int newIndex)
		{
			if (this._initialized)
			{
				this.ResetNavigationIndices();
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00012263 File Offset: 0x00010463
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001226B File Offset: 0x0001046B
		public int OptionTypeID
		{
			get
			{
				return this._optionTypeID;
			}
			set
			{
				if (this._optionTypeID != value)
				{
					this._optionTypeID = value;
					base.OnPropertyChanged(value, "OptionTypeID");
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00012289 File Offset: 0x00010489
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00012291 File Offset: 0x00010491
		public bool IsOptionEnabled
		{
			get
			{
				return this._isOptionEnabled;
			}
			set
			{
				if (this._isOptionEnabled != value)
				{
					this._isOptionEnabled = value;
					base.OnPropertyChanged(value, "IsOptionEnabled");
					this._isEnabledStateDirty = true;
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000122B6 File Offset: 0x000104B6
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x000122BE File Offset: 0x000104BE
		public string OptionTitle
		{
			get
			{
				return this._optionTitle;
			}
			set
			{
				if (this._optionTitle != value)
				{
					this._optionTitle = value;
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000122D5 File Offset: 0x000104D5
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x000122DD File Offset: 0x000104DD
		public string[] ImageIDs
		{
			get
			{
				return this._imageIDs;
			}
			set
			{
				if (this._imageIDs != value)
				{
					this._imageIDs = value;
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000122EF File Offset: 0x000104EF
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x000122F7 File Offset: 0x000104F7
		public string OptionDescription
		{
			get
			{
				return this._optionDescription;
			}
			set
			{
				if (this._optionDescription != value)
				{
					this._optionDescription = value;
				}
			}
		}

		// Token: 0x04000294 RID: 660
		private ButtonWidget _booleanToggleButtonWidget;

		// Token: 0x04000295 RID: 661
		private AnimatedDropdownWidget _dropdownWidget;

		// Token: 0x04000296 RID: 662
		private OptionsScreenWidget _screenWidget;

		// Token: 0x04000297 RID: 663
		private Widget _dropdownExtensionParentWidget;

		// Token: 0x04000298 RID: 664
		private bool _eventsRegistered;

		// Token: 0x04000299 RID: 665
		private bool _initialized;

		// Token: 0x0400029A RID: 666
		private List<Sprite> _graphicsSprites;

		// Token: 0x0400029B RID: 667
		private bool _isEnabledStateDirty = true;

		// Token: 0x0400029C RID: 668
		private int _optionTypeID;

		// Token: 0x0400029D RID: 669
		private string _optionDescription;

		// Token: 0x0400029E RID: 670
		private string _optionTitle;

		// Token: 0x0400029F RID: 671
		private string[] _imageIDs;

		// Token: 0x040002A0 RID: 672
		private bool _isOptionEnabled;
	}
}
