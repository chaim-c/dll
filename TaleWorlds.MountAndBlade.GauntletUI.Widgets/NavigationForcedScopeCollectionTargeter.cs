using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002E RID: 46
	public class NavigationForcedScopeCollectionTargeter : Widget
	{
		// Token: 0x0600026B RID: 619 RVA: 0x000089BC File Offset: 0x00006BBC
		public NavigationForcedScopeCollectionTargeter(UIContext context) : base(context)
		{
			this._collection = new GamepadNavigationForcedScopeCollection();
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 0f;
			base.SuggestedWidth = 0f;
			base.IsVisible = false;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000089FB File Offset: 0x00006BFB
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00008A08 File Offset: 0x00006C08
		public bool IsCollectionEnabled
		{
			get
			{
				return this._collection.IsEnabled;
			}
			set
			{
				if (value != this._collection.IsEnabled)
				{
					this._collection.IsEnabled = value;
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00008A24 File Offset: 0x00006C24
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00008A31 File Offset: 0x00006C31
		public bool IsCollectionDisabled
		{
			get
			{
				return this._collection.IsDisabled;
			}
			set
			{
				if (value != this._collection.IsDisabled)
				{
					this._collection.IsDisabled = value;
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00008A5A File Offset: 0x00006C5A
		public string CollectionID
		{
			get
			{
				return this._collection.CollectionID;
			}
			set
			{
				if (value != this._collection.CollectionID)
				{
					this._collection.CollectionID = value;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00008A7B File Offset: 0x00006C7B
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00008A88 File Offset: 0x00006C88
		public int CollectionOrder
		{
			get
			{
				return this._collection.CollectionOrder;
			}
			set
			{
				if (value != this._collection.CollectionOrder)
				{
					this._collection.CollectionOrder = value;
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00008AA4 File Offset: 0x00006CA4
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public Widget CollectionParent
		{
			get
			{
				return this._collection.ParentWidget;
			}
			set
			{
				if (this._collection.ParentWidget != value)
				{
					if (this._collection.ParentWidget != null)
					{
						base.GamepadNavigationContext.RemoveForcedScopeCollection(this._collection);
					}
					this._collection.ParentWidget = value;
					if (value != null)
					{
						base.GamepadNavigationContext.AddForcedScopeCollection(this._collection);
					}
				}
			}
		}

		// Token: 0x0400011B RID: 283
		private GamepadNavigationForcedScopeCollection _collection;
	}
}
