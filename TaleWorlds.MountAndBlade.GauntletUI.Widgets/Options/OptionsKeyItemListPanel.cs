using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options
{
	// Token: 0x02000070 RID: 112
	public class OptionsKeyItemListPanel : ListPanel
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0001230E File Offset: 0x0001050E
		public OptionsKeyItemListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00012318 File Offset: 0x00010518
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._screenWidget == null)
			{
				this._screenWidget = (base.EventManager.Root.GetChild(0).FindChild("Options") as OptionsScreenWidget);
			}
			if (!this._eventsRegistered)
			{
				this.RegisterHoverEvents();
				this._eventsRegistered = true;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001236F File Offset: 0x0001056F
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			this.SetCurrentOption(false, false, -1);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00012380 File Offset: 0x00010580
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			this.ResetCurrentOption();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001238E File Offset: 0x0001058E
		private void SetCurrentOption(bool fromHoverOverDropdown, bool fromBooleanSelection, int hoverDropdownItemIndex = -1)
		{
			OptionsScreenWidget screenWidget = this._screenWidget;
			if (screenWidget == null)
			{
				return;
			}
			screenWidget.SetCurrentOption(this, null);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000123A2 File Offset: 0x000105A2
		private void ResetCurrentOption()
		{
			OptionsScreenWidget screenWidget = this._screenWidget;
			if (screenWidget == null)
			{
				return;
			}
			screenWidget.SetCurrentOption(null, null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000123B8 File Offset: 0x000105B8
		private void RegisterHoverEvents()
		{
			foreach (Widget widget in base.AllChildren)
			{
				widget.boolPropertyChanged += this.Child_PropertyChanged;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00012410 File Offset: 0x00010610
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00012432 File Offset: 0x00010632
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001243A File Offset: 0x0001063A
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00012451 File Offset: 0x00010651
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00012459 File Offset: 0x00010659
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

		// Token: 0x040002A1 RID: 673
		private OptionsScreenWidget _screenWidget;

		// Token: 0x040002A2 RID: 674
		private bool _eventsRegistered;

		// Token: 0x040002A3 RID: 675
		private string _optionDescription;

		// Token: 0x040002A4 RID: 676
		private string _optionTitle;
	}
}
