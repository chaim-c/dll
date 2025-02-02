﻿using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000012 RID: 18
	public class ContainerPageControlWidget : Widget
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003F9D File Offset: 0x0000219D
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003FA5 File Offset: 0x000021A5
		public int PageCount { get; private set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000CB RID: 203 RVA: 0x00003FB0 File Offset: 0x000021B0
		// (remove) Token: 0x060000CC RID: 204 RVA: 0x00003FE8 File Offset: 0x000021E8
		public event Action OnPageCountChanged;

		// Token: 0x060000CD RID: 205 RVA: 0x00004020 File Offset: 0x00002220
		public ContainerPageControlWidget(UIContext context) : base(context)
		{
			this._nextPageClickedHandler = new Action<Widget>(this.NextPageClicked);
			this._previousPageClickedHandler = new Action<Widget>(this.PreviousPageClicked);
			this._onContainerChildRemovedHandler = new Action<Widget>(this.OnContainerChildRemoved);
			this._onContainerChildAddedHandler = new Action<Widget, Widget>(this.OnContainerChildAdded);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000407C File Offset: 0x0000227C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				int pageCount = this.PageCount;
				this.PageCount = MathF.Ceiling((float)this.Container.ChildCount / (float)this.ItemPerPage);
				if (pageCount != this.PageCount)
				{
					Action onPageCountChanged = this.OnPageCountChanged;
					if (onPageCountChanged != null)
					{
						onPageCountChanged();
					}
				}
				this._currentPageIndex = ((this.PageCount > 1) ? ((int)MathF.Clamp((float)this._currentPageIndex, 0f, (float)(this.PageCount - 1))) : 0);
				this.UpdateControlElements();
				this.UpdateContainerItems();
				this._isInitialized = true;
				this.OnInitialized();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004120 File Offset: 0x00002320
		private void NextPageClicked(Widget widget)
		{
			int num = this._currentPageIndex + 1;
			if (num >= this.PageCount)
			{
				num = (this.LoopNavigation ? 0 : (this.PageCount - 1));
			}
			if (num != this._currentPageIndex)
			{
				this._currentPageIndex = num;
				this.UpdateContainerItems();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000416C File Offset: 0x0000236C
		private void PreviousPageClicked(Widget widget)
		{
			int num = this._currentPageIndex - 1;
			if (num < 0)
			{
				num = (this.LoopNavigation ? (this.PageCount - 1) : 0);
			}
			if (num != this._currentPageIndex)
			{
				this._currentPageIndex = num;
				this.UpdateContainerItems();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000041B0 File Offset: 0x000023B0
		private void OnContainerChildAdded(Widget parentWidget, Widget addedWidget)
		{
			this._isInitialized = false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000041B9 File Offset: 0x000023B9
		private void OnContainerChildRemoved(Widget widget)
		{
			this._isInitialized = false;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000041C4 File Offset: 0x000023C4
		private void UpdateContainerItems()
		{
			int childCount = this.Container.ChildCount;
			int num = this._currentPageIndex * this.ItemPerPage;
			int num2 = (this._currentPageIndex + 1) * this.ItemPerPage;
			for (int i = 0; i < childCount; i++)
			{
				this.Container.GetChild(i).IsVisible = (i >= num && i < num2);
			}
			this.UpdatePageText();
			this.OnContainerItemsUpdated();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004230 File Offset: 0x00002430
		private void UpdateControlElements()
		{
			if (this.NextPageButton != null)
			{
				this.NextPageButton.IsVisible = (this.PageCount > 1);
			}
			if (this.PreviousPageButton != null)
			{
				this.PreviousPageButton.IsVisible = (this.PageCount > 1);
			}
			if (this.PageText != null)
			{
				this.PageText.IsVisible = (this.PageCount > 1);
			}
			if (this.PageButtonsContext != null)
			{
				this.PageButtonsContext.IsScopeEnabled = (this.PageCount > 1);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000042AD File Offset: 0x000024AD
		private void UpdatePageText()
		{
			if (this.PageText != null)
			{
				this.PageText.Text = this._currentPageIndex + 1 + "/" + this.PageCount;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000042E4 File Offset: 0x000024E4
		protected virtual void OnInitialized()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000042E6 File Offset: 0x000024E6
		protected virtual void OnContainerItemsUpdated()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000042E8 File Offset: 0x000024E8
		protected void GoToPage(int index)
		{
			if (index >= 0 && index < this.PageCount && index != this._currentPageIndex)
			{
				this._currentPageIndex = index;
				this.UpdateContainerItems();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000430D File Offset: 0x0000250D
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004315 File Offset: 0x00002515
		public NavigationScopeTargeter PageButtonsContext { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000431E File Offset: 0x0000251E
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004326 File Offset: 0x00002526
		[Editor(false)]
		public int ItemPerPage
		{
			get
			{
				return this._itemPerPage;
			}
			set
			{
				if (this._itemPerPage != value)
				{
					this._itemPerPage = value;
					base.OnPropertyChanged(value, "ItemPerPage");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004344 File Offset: 0x00002544
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000434C File Offset: 0x0000254C
		[Editor(false)]
		public bool LoopNavigation
		{
			get
			{
				return this._loopNavigation;
			}
			set
			{
				if (this._loopNavigation != value)
				{
					this._loopNavigation = value;
					base.OnPropertyChanged(value, "LoopNavigation");
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000436A File Offset: 0x0000256A
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004374 File Offset: 0x00002574
		[Editor(false)]
		public Container Container
		{
			get
			{
				return this._container;
			}
			set
			{
				if (this._container != value)
				{
					Container container = this._container;
					if (container != null)
					{
						container.ItemAddEventHandlers.Remove(this._onContainerChildAddedHandler);
					}
					Container container2 = this._container;
					if (container2 != null)
					{
						container2.ItemAfterRemoveEventHandlers.Remove(this._onContainerChildRemovedHandler);
					}
					this._container = value;
					Container container3 = this._container;
					if (container3 != null)
					{
						container3.ItemAddEventHandlers.Add(this._onContainerChildAddedHandler);
					}
					Container container4 = this._container;
					if (container4 != null)
					{
						container4.ItemAfterRemoveEventHandlers.Add(this._onContainerChildRemovedHandler);
					}
					base.OnPropertyChanged<Container>(value, "Container");
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004412 File Offset: 0x00002612
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000441C File Offset: 0x0000261C
		[Editor(false)]
		public ButtonWidget NextPageButton
		{
			get
			{
				return this._nextPageButton;
			}
			set
			{
				if (this._nextPageButton != value)
				{
					ButtonWidget nextPageButton = this._nextPageButton;
					if (nextPageButton != null)
					{
						nextPageButton.ClickEventHandlers.Remove(this._nextPageClickedHandler);
					}
					this._nextPageButton = value;
					ButtonWidget nextPageButton2 = this._nextPageButton;
					if (nextPageButton2 != null)
					{
						nextPageButton2.ClickEventHandlers.Add(this._nextPageClickedHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "NextPageButton");
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000447E File Offset: 0x0000267E
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004488 File Offset: 0x00002688
		[Editor(false)]
		public ButtonWidget PreviousPageButton
		{
			get
			{
				return this._previousPageButton;
			}
			set
			{
				if (this._previousPageButton != value)
				{
					ButtonWidget previousPageButton = this._previousPageButton;
					if (previousPageButton != null)
					{
						previousPageButton.ClickEventHandlers.Remove(this._previousPageClickedHandler);
					}
					this._previousPageButton = value;
					ButtonWidget previousPageButton2 = this._previousPageButton;
					if (previousPageButton2 != null)
					{
						previousPageButton2.ClickEventHandlers.Add(this._previousPageClickedHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "PreviousPageButton");
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000044EA File Offset: 0x000026EA
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000044F2 File Offset: 0x000026F2
		[Editor(false)]
		public TextWidget PageText
		{
			get
			{
				return this._pageText;
			}
			set
			{
				if (this._pageText != value)
				{
					this._pageText = value;
					base.OnPropertyChanged<TextWidget>(value, "PageText");
				}
			}
		}

		// Token: 0x0400005F RID: 95
		private Action<Widget> _nextPageClickedHandler;

		// Token: 0x04000060 RID: 96
		private Action<Widget> _previousPageClickedHandler;

		// Token: 0x04000061 RID: 97
		private Action<Widget, Widget> _onContainerChildAddedHandler;

		// Token: 0x04000062 RID: 98
		private Action<Widget> _onContainerChildRemovedHandler;

		// Token: 0x04000064 RID: 100
		protected int _currentPageIndex;

		// Token: 0x04000065 RID: 101
		private bool _isInitialized;

		// Token: 0x04000068 RID: 104
		private int _itemPerPage;

		// Token: 0x04000069 RID: 105
		private bool _loopNavigation;

		// Token: 0x0400006A RID: 106
		private Container _container;

		// Token: 0x0400006B RID: 107
		private ButtonWidget _nextPageButton;

		// Token: 0x0400006C RID: 108
		private ButtonWidget _previousPageButton;

		// Token: 0x0400006D RID: 109
		private TextWidget _pageText;
	}
}
