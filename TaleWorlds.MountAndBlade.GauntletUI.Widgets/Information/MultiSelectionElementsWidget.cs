using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information
{
	// Token: 0x0200013A RID: 314
	public class MultiSelectionElementsWidget : Widget
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x0002DFC0 File Offset: 0x0002C1C0
		public MultiSelectionElementsWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._updateRequired)
			{
				this.UpdateElementsList();
				this._updateRequired = false;
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002DFF2 File Offset: 0x0002C1F2
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			if (child is ListPanel)
			{
				this._elementContainer = (child as ListPanel);
				this._elementContainer.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnElementAdded));
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002E02B File Offset: 0x0002C22B
		private void OnElementAdded(Widget parentWidget, Widget addedWidget)
		{
			this._updateRequired = true;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002E034 File Offset: 0x0002C234
		private void UpdateElementsList()
		{
			this._elementsList.Clear();
			for (int i = 0; i < this._elementContainer.ChildCount; i++)
			{
				ButtonWidget item = this._elementContainer.GetChild(i).GetChild(0) as ButtonWidget;
				this._elementsList.Add(item);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0002E086 File Offset: 0x0002C286
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0002E08E File Offset: 0x0002C28E
		[Editor(false)]
		public ButtonWidget DoneButtonWidget
		{
			get
			{
				return this._doneButtonWidget;
			}
			set
			{
				if (this._doneButtonWidget != value)
				{
					this._doneButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "DoneButtonWidget");
				}
			}
		}

		// Token: 0x04000790 RID: 1936
		private bool _updateRequired;

		// Token: 0x04000791 RID: 1937
		private List<ButtonWidget> _elementsList = new List<ButtonWidget>();

		// Token: 0x04000792 RID: 1938
		private ButtonWidget _doneButtonWidget;

		// Token: 0x04000793 RID: 1939
		private ListPanel _elementContainer;
	}
}
