using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x0200005D RID: 93
	public class PartyHeaderToggleWidget : ToggleButtonWidget
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000F004 File Offset: 0x0000D204
		public bool AutoToggleTransferButtonState { get; set; } = true;

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F00D File Offset: 0x0000D20D
		public PartyHeaderToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000F024 File Offset: 0x0000D224
		protected override void OnClick(Widget widget)
		{
			if (!this.BlockInputsWhenDisabled || this._listPanel == null || this._listPanel.ChildCount > 0)
			{
				base.OnClick(widget);
				this.UpdateCollapseIndicator();
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F051 File Offset: 0x0000D251
		private void OnListSizeChange(Widget widget)
		{
			this.UpdateSize();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F059 File Offset: 0x0000D259
		private void OnListSizeChange(Widget parentWidget, Widget addedWidget)
		{
			this.UpdateSize();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F061 File Offset: 0x0000D261
		public override void SetState(string stateName)
		{
			if (!this.BlockInputsWhenDisabled || this._listPanel == null || this._listPanel.ChildCount > 0)
			{
				base.SetState(stateName);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F088 File Offset: 0x0000D288
		private void UpdateSize()
		{
			if (this.TransferButtonWidget != null && this.AutoToggleTransferButtonState)
			{
				this.TransferButtonWidget.IsEnabled = (this._listPanel.ChildCount > 0);
			}
			if (this.IsRelevant)
			{
				base.IsVisible = true;
				if (this._listPanel.ChildCount > 0)
				{
					this._listPanel.IsVisible = true;
				}
				if (this._listPanel.ChildCount > this._latestChildCount && !base.WidgetToClose.IsVisible)
				{
					this.OnClick();
				}
			}
			else
			{
				this._listPanel.IsVisible = false;
			}
			this._latestChildCount = this._listPanel.ChildCount;
			this.UpdateCollapseIndicator();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000F134 File Offset: 0x0000D334
		private void ListPanelUpdated()
		{
			if (this.TransferButtonWidget != null)
			{
				this.TransferButtonWidget.IsEnabled = false;
			}
			this._listPanel.ItemAfterRemoveEventHandlers.Add(new Action<Widget>(this.OnListSizeChange));
			this._listPanel.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnListSizeChange));
			this.UpdateSize();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000F193 File Offset: 0x0000D393
		private void TransferButtonUpdated()
		{
			this.TransferButtonWidget.IsEnabled = false;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		private void CollapseIndicatorUpdated()
		{
			this.CollapseIndicator.AddState("Collapsed");
			this.CollapseIndicator.AddState("Expanded");
			this.UpdateCollapseIndicator();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		private void UpdateCollapseIndicator()
		{
			if (base.WidgetToClose != null && this.CollapseIndicator != null)
			{
				if (base.WidgetToClose.IsVisible)
				{
					this.CollapseIndicator.SetState("Expanded");
					return;
				}
				this.CollapseIndicator.SetState("Collapsed");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000F209 File Offset: 0x0000D409
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000F211 File Offset: 0x0000D411
		[Editor(false)]
		public ListPanel ListPanel
		{
			get
			{
				return this._listPanel;
			}
			set
			{
				if (this._listPanel != value)
				{
					this._listPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "ListPanel");
					this.ListPanelUpdated();
				}
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000F235 File Offset: 0x0000D435
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000F23D File Offset: 0x0000D43D
		[Editor(false)]
		public ButtonWidget TransferButtonWidget
		{
			get
			{
				return this._transferButtonWidget;
			}
			set
			{
				if (this._transferButtonWidget != value)
				{
					this._transferButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "TransferButtonWidget");
					this.TransferButtonUpdated();
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000F261 File Offset: 0x0000D461
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0000F269 File Offset: 0x0000D469
		[Editor(false)]
		public BrushWidget CollapseIndicator
		{
			get
			{
				return this._collapseIndicator;
			}
			set
			{
				if (this._collapseIndicator != value)
				{
					this._collapseIndicator = value;
					base.OnPropertyChanged<BrushWidget>(value, "CollapseIndicator");
					this.CollapseIndicatorUpdated();
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000F28D File Offset: 0x0000D48D
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0000F295 File Offset: 0x0000D495
		[Editor(false)]
		public bool IsRelevant
		{
			get
			{
				return this._isRelevant;
			}
			set
			{
				if (this._isRelevant != value)
				{
					this._isRelevant = value;
					if (!this._isRelevant)
					{
						base.IsVisible = false;
					}
					this.UpdateSize();
					base.OnPropertyChanged(value, "IsRelevant");
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		[Editor(false)]
		public bool BlockInputsWhenDisabled
		{
			get
			{
				return this._blockInputsWhenDisabled;
			}
			set
			{
				if (this._blockInputsWhenDisabled != value)
				{
					this._blockInputsWhenDisabled = value;
					base.OnPropertyChanged(value, "BlockInputsWhenDisabled");
				}
			}
		}

		// Token: 0x0400021C RID: 540
		private int _latestChildCount;

		// Token: 0x0400021E RID: 542
		private ListPanel _listPanel;

		// Token: 0x0400021F RID: 543
		private ButtonWidget _transferButtonWidget;

		// Token: 0x04000220 RID: 544
		private BrushWidget _collapseIndicator;

		// Token: 0x04000221 RID: 545
		private bool _isRelevant = true;

		// Token: 0x04000222 RID: 546
		private bool _blockInputsWhenDisabled;
	}
}
