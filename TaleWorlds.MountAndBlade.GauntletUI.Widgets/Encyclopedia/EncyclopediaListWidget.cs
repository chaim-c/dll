using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014F RID: 335
	public class EncyclopediaListWidget : Widget
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x0003120E File Offset: 0x0002F40E
		public EncyclopediaListWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00031218 File Offset: 0x0002F418
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isListSizeInitialized && this.ItemListScroll != null && this.ItemListScroll.Size.Y != 0f)
			{
				this._isListSizeInitialized = true;
				this._isDirty = true;
			}
			if (this._isDirty)
			{
				this._isDirty = false;
				this.UpdateScrollPosition();
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00031278 File Offset: 0x0002F478
		private void UpdateScrollPosition()
		{
			if (!string.IsNullOrEmpty(this.LastSelectedItemId) && this.ItemList != null && this.ItemListScroll != null)
			{
				Widget widget = this.ItemList.AllChildren.FirstOrDefault(delegate(Widget x)
				{
					EncyclopediaListItemButtonWidget encyclopediaListItemButtonWidget;
					return (encyclopediaListItemButtonWidget = (x as EncyclopediaListItemButtonWidget)) != null && encyclopediaListItemButtonWidget.ListItemId == this.LastSelectedItemId;
				});
				if (widget != null && widget.IsVisible)
				{
					float num = widget.ScaledSuggestedHeight + widget.ScaledMarginTop + widget.ScaledMarginBottom - 2f * base._scaleToUse;
					int visibleSiblingIndex = widget.GetVisibleSiblingIndex();
					float valueForced = num * (float)visibleSiblingIndex - this.ItemListScroll.Size.Y / 2f;
					this.ItemListScroll.SetValueForced(valueForced);
				}
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003131C File Offset: 0x0002F51C
		private void OnListItemAdded(Widget widget, string eventName, object[] eventArgs)
		{
			if (eventName == "ItemAdd" && eventArgs.Length != 0 && eventArgs[0] is EncyclopediaListItemButtonWidget)
			{
				this._isDirty = true;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00031340 File Offset: 0x0002F540
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00031348 File Offset: 0x0002F548
		[Editor(false)]
		public string LastSelectedItemId
		{
			get
			{
				return this._lastSelectedItemId;
			}
			set
			{
				if (this._lastSelectedItemId != value)
				{
					this._lastSelectedItemId = value;
					base.OnPropertyChanged<string>(value, "LastSelectedItemId");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00031372 File Offset: 0x0002F572
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0003137A File Offset: 0x0002F57A
		public ListPanel ItemList
		{
			get
			{
				return this._itemList;
			}
			set
			{
				if (this._itemList != value)
				{
					this._itemList = value;
					this._isDirty = true;
					this._itemList.EventFire += this.OnListItemAdded;
				}
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000313AA File Offset: 0x0002F5AA
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x000313B2 File Offset: 0x0002F5B2
		public ScrollbarWidget ItemListScroll
		{
			get
			{
				return this._itemListScroll;
			}
			set
			{
				if (this._itemListScroll != value)
				{
					this._itemListScroll = value;
					this._isDirty = true;
				}
			}
		}

		// Token: 0x04000819 RID: 2073
		private bool _isDirty;

		// Token: 0x0400081A RID: 2074
		private bool _isListSizeInitialized;

		// Token: 0x0400081B RID: 2075
		private string _lastSelectedItemId;

		// Token: 0x0400081C RID: 2076
		private ListPanel _itemList;

		// Token: 0x0400081D RID: 2077
		private ScrollbarWidget _itemListScroll;
	}
}
