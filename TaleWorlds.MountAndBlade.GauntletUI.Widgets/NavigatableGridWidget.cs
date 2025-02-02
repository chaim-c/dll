using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002B RID: 43
	public class NavigatableGridWidget : GridWidget
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007FB3 File Offset: 0x000061B3
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007FBB File Offset: 0x000061BB
		public ScrollablePanel ParentPanel { get; set; }

		// Token: 0x06000221 RID: 545 RVA: 0x00007FC4 File Offset: 0x000061C4
		public NavigatableGridWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007FE0 File Offset: 0x000061E0
		protected override void OnLateUpdate(float dt)
		{
			if (this._areIndicesDirty)
			{
				for (int i = 0; i < base.ChildCount; i++)
				{
					base.Children[i].GamepadNavigationIndex = -1;
				}
				this.RefreshChildNavigationIndices();
				this._areIndicesDirty = false;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008025 File Offset: 0x00006225
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			if (this.ParentPanel == null)
			{
				this.ParentPanel = base.FindParentPanel();
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008044 File Offset: 0x00006244
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			this.SetNavigationIndexForChild(child);
			child.OnGamepadNavigationFocusGained = new Action<Widget>(this.OnWidgetGainedGamepadFocus);
			child.EventFire += this.OnChildSiblingIndexChanged;
			child.boolPropertyChanged += this.OnChildVisibilityChanged;
			this.UpdateEmptyNavigationWidget();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000809C File Offset: 0x0000629C
		protected override void OnAfterChildRemoved(Widget child)
		{
			base.OnAfterChildRemoved(child);
			child.OnGamepadNavigationFocusGained = null;
			child.EventFire -= this.OnChildSiblingIndexChanged;
			child.boolPropertyChanged -= this.OnChildVisibilityChanged;
			child.GamepadNavigationIndex = -1;
			this.UpdateEmptyNavigationWidget();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000080E8 File Offset: 0x000062E8
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			for (int i = 0; i < base.Children.Count; i++)
			{
				base.Children[i].OnGamepadNavigationFocusGained = null;
				base.Children[i].EventFire -= this.OnChildSiblingIndexChanged;
				base.Children[i].boolPropertyChanged -= this.OnChildVisibilityChanged;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008160 File Offset: 0x00006360
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

		// Token: 0x06000228 RID: 552 RVA: 0x00008193 File Offset: 0x00006393
		private void OnWidgetGainedGamepadFocus(Widget widget)
		{
			if (this.ParentPanel != null)
			{
				this.ParentPanel.ScrollToChild(widget, -1f, -1f, this.AutoScrollXOffset, this.AutoScrollYOffset, 0f, 0f);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000081C9 File Offset: 0x000063C9
		private void OnChildSiblingIndexChanged(Widget widget, string eventName, object[] parameters)
		{
			if (eventName == "SiblingIndexChanged")
			{
				this._areIndicesDirty = true;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000081E0 File Offset: 0x000063E0
		private void SetNavigationIndexForChild(Widget widget)
		{
			int num = this.MinIndex + widget.GetSiblingIndex() * this.StepSize;
			if (num <= this.MaxIndex)
			{
				widget.GamepadNavigationIndex = num;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008212 File Offset: 0x00006412
		protected override void OnGamepadNavigationIndexUpdated(int newIndex)
		{
			if (newIndex != -1 && this.UseSelfIndexForMinimum)
			{
				this.SetNavigationIndicesFromSelf();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008226 File Offset: 0x00006426
		private void SetNavigationIndicesFromSelf()
		{
			this.MinIndex = base.GamepadNavigationIndex;
			base.GamepadNavigationIndex = -1;
			this._areIndicesDirty = true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008242 File Offset: 0x00006442
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

		// Token: 0x0600022E RID: 558 RVA: 0x00008278 File Offset: 0x00006478
		protected void RefreshChildNavigationIndices()
		{
			for (int i = 0; i < base.Children.Count; i++)
			{
				this.SetNavigationIndexForChild(base.Children[i]);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000082AD File Offset: 0x000064AD
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000082B5 File Offset: 0x000064B5
		public int AutoScrollXOffset { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000082BE File Offset: 0x000064BE
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000082C6 File Offset: 0x000064C6
		public int AutoScrollYOffset { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000082CF File Offset: 0x000064CF
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000082D7 File Offset: 0x000064D7
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000082EF File Offset: 0x000064EF
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000082F7 File Offset: 0x000064F7
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000830F File Offset: 0x0000650F
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008317 File Offset: 0x00006517
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000832F File Offset: 0x0000652F
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00008337 File Offset: 0x00006537
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008360 File Offset: 0x00006560
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00008368 File Offset: 0x00006568
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

		// Token: 0x04000104 RID: 260
		private bool _areIndicesDirty;

		// Token: 0x04000107 RID: 263
		private int _minIndex;

		// Token: 0x04000108 RID: 264
		private int _maxIndex = int.MaxValue;

		// Token: 0x04000109 RID: 265
		private int _stepSize = 1;

		// Token: 0x0400010A RID: 266
		private bool _useSelfIndexForMinimum;

		// Token: 0x0400010B RID: 267
		private Widget _emptyNavigationWidget;
	}
}
