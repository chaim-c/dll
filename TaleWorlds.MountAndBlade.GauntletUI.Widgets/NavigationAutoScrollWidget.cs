using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002D RID: 45
	public class NavigationAutoScrollWidget : Widget
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008789 File Offset: 0x00006989
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00008791 File Offset: 0x00006991
		public ScrollablePanel ParentPanel { get; set; }

		// Token: 0x0600025D RID: 605 RVA: 0x0000879A File Offset: 0x0000699A
		public NavigationAutoScrollWidget(UIContext context) : base(context)
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 0f;
			base.SuggestedWidth = 0f;
			base.IsVisible = false;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000087D0 File Offset: 0x000069D0
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			if (this.ParentPanel == null && base.ParentWidget != null)
			{
				for (Widget parentWidget = base.ParentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
				{
					ScrollablePanel parentPanel;
					if ((parentPanel = (parentWidget as ScrollablePanel)) != null)
					{
						this.ParentPanel = parentPanel;
						return;
					}
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008818 File Offset: 0x00006A18
		private void OnWidgetGainedGamepadFocus(Widget widget)
		{
			if (this.ParentPanel != null)
			{
				this.ParentPanel.ScrollToChild(this.ScrollTarget ?? widget, -1f, -1f, this.ScrollXOffset, this.ScrollYOffset, 0f, 0f);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008858 File Offset: 0x00006A58
		private void UpdateTargetAutoScrollAndChildren()
		{
			if (this._trackedWidget != null)
			{
				Widget trackedWidget = this._trackedWidget;
				trackedWidget.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Combine(trackedWidget.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedGamepadFocus));
				foreach (Widget widget in this._trackedWidget.Children)
				{
					if (this.IncludeChildren)
					{
						Widget widget2 = widget;
						widget2.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Combine(widget2.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedGamepadFocus));
					}
					else
					{
						Widget widget3 = widget;
						widget3.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Remove(widget3.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedGamepadFocus));
					}
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000892C File Offset: 0x00006B2C
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00008934 File Offset: 0x00006B34
		public int ScrollYOffset { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000893D File Offset: 0x00006B3D
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00008945 File Offset: 0x00006B45
		public int ScrollXOffset { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000894E File Offset: 0x00006B4E
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00008956 File Offset: 0x00006B56
		public bool IncludeChildren
		{
			get
			{
				return this._includeChildren;
			}
			set
			{
				if (value != this._includeChildren)
				{
					this._includeChildren = value;
					this.UpdateTargetAutoScrollAndChildren();
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000896E File Offset: 0x00006B6E
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00008976 File Offset: 0x00006B76
		public Widget TrackedWidget
		{
			get
			{
				return this._trackedWidget;
			}
			set
			{
				if (value != this._trackedWidget)
				{
					if (this._trackedWidget != null)
					{
						this._trackedWidget.OnGamepadNavigationFocusGained = null;
					}
					this._trackedWidget = value;
					this.UpdateTargetAutoScrollAndChildren();
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000089A2 File Offset: 0x00006BA2
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000089AA File Offset: 0x00006BAA
		public Widget ScrollTarget
		{
			get
			{
				return this._scrollTarget;
			}
			set
			{
				if (value != this._scrollTarget)
				{
					this._scrollTarget = value;
				}
			}
		}

		// Token: 0x04000118 RID: 280
		private bool _includeChildren;

		// Token: 0x04000119 RID: 281
		private Widget _trackedWidget;

		// Token: 0x0400011A RID: 282
		private Widget _scrollTarget;
	}
}
