using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002C RID: 44
	public class NavigatableListPanel : ListPanel
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008394 File Offset: 0x00006594
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000839C File Offset: 0x0000659C
		public ScrollablePanel ParentPanel { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x000083A5 File Offset: 0x000065A5
		public NavigatableListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000083C0 File Offset: 0x000065C0
		protected override void OnLateUpdate(float dt)
		{
			if (this._areIndicesDirty)
			{
				this.RefreshChildNavigationIndices();
				this._areIndicesDirty = false;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000083D7 File Offset: 0x000065D7
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			if (this.ParentPanel == null)
			{
				this.ParentPanel = base.FindParentPanel();
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000083F4 File Offset: 0x000065F4
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.OnGamepadNavigationFocusGained = new Action<Widget>(this.OnWidgetGainedGamepadFocus);
			child.EventFire += this.OnChildSiblingIndexChanged;
			child.boolPropertyChanged += this.OnChildVisibilityChanged;
			this._areIndicesDirty = true;
			this.UpdateEmptyNavigationWidget();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000844C File Offset: 0x0000664C
		protected override void OnAfterChildRemoved(Widget child)
		{
			base.OnAfterChildRemoved(child);
			child.OnGamepadNavigationFocusGained = null;
			child.EventFire -= this.OnChildSiblingIndexChanged;
			child.boolPropertyChanged -= this.OnChildVisibilityChanged;
			child.GamepadNavigationIndex = -1;
			this.UpdateEmptyNavigationWidget();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008498 File Offset: 0x00006698
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			for (int i = 0; i < base.Children.Count; i++)
			{
				base.Children[i].OnGamepadNavigationFocusGained = null;
				base.Children[i].EventFire -= this.OnChildSiblingIndexChanged;
				base.Children[i].boolPropertyChanged -= this.OnChildVisibilityChanged;
				base.Children[i].GamepadNavigationIndex = -1;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008520 File Offset: 0x00006720
		private void OnChildVisibilityChanged(PropertyOwnerObject child, string propertyName, bool value)
		{
			if (propertyName == "IsVisible")
			{
				Widget widget = (Widget)child;
				if (!value)
				{
					widget.GamepadNavigationIndex = -1;
					return;
				}
				this.SetNavigationIndexForChild(widget);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008553 File Offset: 0x00006753
		private void OnWidgetGainedGamepadFocus(Widget widget)
		{
			if (this.ParentPanel != null)
			{
				this.ParentPanel.ScrollToChild(widget, -1f, -1f, this.AutoScrollXOffset, this.AutoScrollYOffset, 0f, 0f);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008589 File Offset: 0x00006789
		private void OnChildSiblingIndexChanged(Widget widget, string eventName, object[] parameters)
		{
			if (eventName == "SiblingIndexChanged")
			{
				this._areIndicesDirty = true;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000085A0 File Offset: 0x000067A0
		private void SetNavigationIndexForChild(Widget widget)
		{
			int num;
			if (base.StackLayout.LayoutMethod == LayoutMethod.VerticalTopToBottom || base.StackLayout.LayoutMethod == LayoutMethod.HorizontalRightToLeft)
			{
				num = this.MaxIndex - widget.GetSiblingIndex() * this.StepSize;
			}
			else
			{
				num = this.MinIndex + widget.GetSiblingIndex() * this.StepSize;
			}
			if (num <= this.MaxIndex)
			{
				widget.GamepadNavigationIndex = num;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008605 File Offset: 0x00006805
		protected override void OnGamepadNavigationIndexUpdated(int newIndex)
		{
			if (newIndex != -1 && this.UseSelfIndexForMinimum)
			{
				this.SetNavigationIndicesFromSelf();
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008619 File Offset: 0x00006819
		private void SetNavigationIndicesFromSelf()
		{
			this.MinIndex = base.GamepadNavigationIndex;
			base.GamepadNavigationIndex = -1;
			this._areIndicesDirty = true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008638 File Offset: 0x00006838
		protected void RefreshChildNavigationIndices()
		{
			for (int i = 0; i < base.Children.Count; i++)
			{
				this.SetNavigationIndexForChild(base.Children[i]);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000866D File Offset: 0x0000686D
		private void UpdateEmptyNavigationWidget()
		{
			if (this._emptyNavigationWidget != null)
			{
				if (base.Children.Count == 0)
				{
					this.EmptyNavigationWidget.GamepadNavigationIndex = this.MinIndex;
					return;
				}
				this.EmptyNavigationWidget.GamepadNavigationIndex = -1;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000086A2 File Offset: 0x000068A2
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000086AA File Offset: 0x000068AA
		public int AutoScrollXOffset { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000086B3 File Offset: 0x000068B3
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000086BB File Offset: 0x000068BB
		public int AutoScrollYOffset { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000086C4 File Offset: 0x000068C4
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000086CC File Offset: 0x000068CC
		public int MinIndex
		{
			get
			{
				return this._minIndex;
			}
			set
			{
				if (value != this._minIndex)
				{
					this._minIndex = value;
					this.RefreshChildNavigationIndices();
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000086E4 File Offset: 0x000068E4
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000086EC File Offset: 0x000068EC
		public int MaxIndex
		{
			get
			{
				return this._maxIndex;
			}
			set
			{
				if (value != this._maxIndex)
				{
					this._maxIndex = value;
					this.RefreshChildNavigationIndices();
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008704 File Offset: 0x00006904
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000870C File Offset: 0x0000690C
		public int StepSize
		{
			get
			{
				return this._stepSize;
			}
			set
			{
				if (value != this._stepSize)
				{
					this._stepSize = value;
					this.RefreshChildNavigationIndices();
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008724 File Offset: 0x00006924
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000872C File Offset: 0x0000692C
		public bool UseSelfIndexForMinimum
		{
			get
			{
				return this._useSelfIndexForMinimum;
			}
			set
			{
				if (value != this._useSelfIndexForMinimum)
				{
					this._useSelfIndexForMinimum = value;
					if (this._useSelfIndexForMinimum && base.GamepadNavigationIndex != -1)
					{
						this.SetNavigationIndicesFromSelf();
					}
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008755 File Offset: 0x00006955
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000875D File Offset: 0x0000695D
		public Widget EmptyNavigationWidget
		{
			get
			{
				return this._emptyNavigationWidget;
			}
			set
			{
				if (value != this._emptyNavigationWidget)
				{
					if (this._emptyNavigationWidget != null)
					{
						this._emptyNavigationWidget.GamepadNavigationIndex = -1;
					}
					this._emptyNavigationWidget = value;
					this.UpdateEmptyNavigationWidget();
				}
			}
		}

		// Token: 0x0400010C RID: 268
		private bool _areIndicesDirty;

		// Token: 0x0400010E RID: 270
		private int _minIndex;

		// Token: 0x0400010F RID: 271
		private int _maxIndex = int.MaxValue;

		// Token: 0x04000110 RID: 272
		private int _stepSize = 1;

		// Token: 0x04000111 RID: 273
		private bool _useSelfIndexForMinimum;

		// Token: 0x04000112 RID: 274
		private Widget _emptyNavigationWidget;
	}
}
