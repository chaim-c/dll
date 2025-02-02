using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003B RID: 59
	public class SelectorWidget : Widget
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000A96D File Offset: 0x00008B6D
		public SelectorWidget(UIContext context) : base(context)
		{
			this._listSelectionHandler = new Action<Widget>(this.OnSelectionChanged);
			this._listItemRemovedHandler = new Action<Widget, Widget>(this.OnListChanged);
			this._listItemAddedHandler = new Action<Widget, Widget>(this.OnListChanged);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public void OnListChanged(Widget widget)
		{
			this.RefreshSelectedItem();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public void OnListChanged(Widget parentWidget, Widget addedWidget)
		{
			this.RefreshSelectedItem();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A9BC File Offset: 0x00008BBC
		public void OnSelectionChanged(Widget widget)
		{
			this.CurrentSelectedIndex = this.ListPanelValue;
			this.RefreshSelectedItem();
			base.OnPropertyChanged(this.CurrentSelectedIndex, "CurrentSelectedIndex");
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		private void RefreshSelectedItem()
		{
			this.ListPanelValue = this.CurrentSelectedIndex;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000A9EF File Offset: 0x00008BEF
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000AA06 File Offset: 0x00008C06
		[Editor(false)]
		public int ListPanelValue
		{
			get
			{
				if (this.Container != null)
				{
					return this.Container.IntValue;
				}
				return -1;
			}
			set
			{
				if (this.Container != null && this.Container.IntValue != value)
				{
					this.Container.IntValue = value;
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000AA2A File Offset: 0x00008C2A
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000AA32 File Offset: 0x00008C32
		[Editor(false)]
		public int CurrentSelectedIndex
		{
			get
			{
				return this._currentSelectedIndex;
			}
			set
			{
				if (this._currentSelectedIndex != value && value >= 0)
				{
					this._currentSelectedIndex = value;
					this.RefreshSelectedItem();
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000AA4E File Offset: 0x00008C4E
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000AA58 File Offset: 0x00008C58
		[Editor(false)]
		public Container Container
		{
			get
			{
				return this._container;
			}
			set
			{
				if (this._container != null)
				{
					this._container.SelectEventHandlers.Remove(this._listSelectionHandler);
					this._container.ItemAddEventHandlers.Remove(this._listItemAddedHandler);
					this._container.ItemRemoveEventHandlers.Remove(this._listItemRemovedHandler);
				}
				this._container = value;
				if (this._container != null)
				{
					this._container.SelectEventHandlers.Add(this._listSelectionHandler);
					this._container.ItemAddEventHandlers.Add(this._listItemAddedHandler);
					this._container.ItemRemoveEventHandlers.Add(this._listItemRemovedHandler);
				}
				this.RefreshSelectedItem();
			}
		}

		// Token: 0x04000160 RID: 352
		private int _currentSelectedIndex;

		// Token: 0x04000161 RID: 353
		private Action<Widget> _listSelectionHandler;

		// Token: 0x04000162 RID: 354
		private Action<Widget, Widget> _listItemRemovedHandler;

		// Token: 0x04000163 RID: 355
		private Action<Widget, Widget> _listItemAddedHandler;

		// Token: 0x04000164 RID: 356
		private Container _container;
	}
}
