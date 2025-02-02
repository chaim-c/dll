using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002F RID: 47
	public class NavigationScopeTargeter : Widget
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008B0D File Offset: 0x00006D0D
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00008B15 File Offset: 0x00006D15
		public GamepadNavigationScope NavigationScope { get; private set; }

		// Token: 0x06000278 RID: 632 RVA: 0x00008B1E File Offset: 0x00006D1E
		public NavigationScopeTargeter(UIContext context) : base(context)
		{
			this.NavigationScope = new GamepadNavigationScope();
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 0f;
			base.SuggestedWidth = 0f;
			base.IsVisible = false;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008B5D File Offset: 0x00006D5D
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00008B6A File Offset: 0x00006D6A
		public string ScopeID
		{
			get
			{
				return this.NavigationScope.ScopeID;
			}
			set
			{
				if (value != this.NavigationScope.ScopeID)
				{
					this.NavigationScope.ScopeID = value;
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008B8B File Offset: 0x00006D8B
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008B98 File Offset: 0x00006D98
		public GamepadNavigationTypes ScopeMovements
		{
			get
			{
				return this.NavigationScope.ScopeMovements;
			}
			set
			{
				if (value != this.NavigationScope.ScopeMovements)
				{
					this.NavigationScope.ScopeMovements = value;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008BB4 File Offset: 0x00006DB4
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00008BC1 File Offset: 0x00006DC1
		public GamepadNavigationTypes AlternateScopeMovements
		{
			get
			{
				return this.NavigationScope.AlternateScopeMovements;
			}
			set
			{
				if (value != this.NavigationScope.AlternateScopeMovements)
				{
					this.NavigationScope.AlternateScopeMovements = value;
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008BDD File Offset: 0x00006DDD
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00008BEA File Offset: 0x00006DEA
		public int AlternateMovementStepSize
		{
			get
			{
				return this.NavigationScope.AlternateMovementStepSize;
			}
			set
			{
				if (value != this.NavigationScope.AlternateMovementStepSize)
				{
					this.NavigationScope.AlternateMovementStepSize = value;
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00008C06 File Offset: 0x00006E06
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00008C13 File Offset: 0x00006E13
		public bool HasCircularMovement
		{
			get
			{
				return this.NavigationScope.HasCircularMovement;
			}
			set
			{
				if (value != this.NavigationScope.HasCircularMovement)
				{
					this.NavigationScope.HasCircularMovement = value;
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00008C2F File Offset: 0x00006E2F
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00008C3C File Offset: 0x00006E3C
		public bool DoNotAutomaticallyFindChildren
		{
			get
			{
				return this.NavigationScope.DoNotAutomaticallyFindChildren;
			}
			set
			{
				if (value != this.NavigationScope.DoNotAutomaticallyFindChildren)
				{
					this.NavigationScope.DoNotAutomaticallyFindChildren = value;
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00008C58 File Offset: 0x00006E58
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00008C65 File Offset: 0x00006E65
		public bool DoNotAutoGainNavigationOnInit
		{
			get
			{
				return this.NavigationScope.DoNotAutoGainNavigationOnInit;
			}
			set
			{
				if (value != this.NavigationScope.DoNotAutoGainNavigationOnInit)
				{
					this.NavigationScope.DoNotAutoGainNavigationOnInit = value;
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008C81 File Offset: 0x00006E81
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00008C8E File Offset: 0x00006E8E
		public bool ForceGainNavigationBasedOnDirection
		{
			get
			{
				return this.NavigationScope.ForceGainNavigationBasedOnDirection;
			}
			set
			{
				if (value != this.NavigationScope.ForceGainNavigationBasedOnDirection)
				{
					this.NavigationScope.ForceGainNavigationBasedOnDirection = value;
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008CAA File Offset: 0x00006EAA
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00008CB7 File Offset: 0x00006EB7
		public bool ForceGainNavigationOnClosestChild
		{
			get
			{
				return this.NavigationScope.ForceGainNavigationOnClosestChild;
			}
			set
			{
				if (value != this.NavigationScope.ForceGainNavigationOnClosestChild)
				{
					this.NavigationScope.ForceGainNavigationOnClosestChild = value;
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00008CD3 File Offset: 0x00006ED3
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public bool NavigateFromScopeEdges
		{
			get
			{
				return this.NavigationScope.NavigateFromScopeEdges;
			}
			set
			{
				if (value != this.NavigationScope.NavigateFromScopeEdges)
				{
					this.NavigationScope.NavigateFromScopeEdges = value;
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00008CFC File Offset: 0x00006EFC
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00008D09 File Offset: 0x00006F09
		public bool UseDiscoveryAreaAsScopeEdges
		{
			get
			{
				return this.NavigationScope.UseDiscoveryAreaAsScopeEdges;
			}
			set
			{
				if (value != this.NavigationScope.UseDiscoveryAreaAsScopeEdges)
				{
					this.NavigationScope.UseDiscoveryAreaAsScopeEdges = value;
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00008D25 File Offset: 0x00006F25
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00008D32 File Offset: 0x00006F32
		public bool DoNotAutoNavigateAfterSort
		{
			get
			{
				return this.NavigationScope.DoNotAutoNavigateAfterSort;
			}
			set
			{
				if (value != this.NavigationScope.DoNotAutoNavigateAfterSort)
				{
					this.NavigationScope.DoNotAutoNavigateAfterSort = value;
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00008D4E File Offset: 0x00006F4E
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00008D5B File Offset: 0x00006F5B
		public bool FollowMobileTargets
		{
			get
			{
				return this.NavigationScope.FollowMobileTargets;
			}
			set
			{
				if (value != this.NavigationScope.FollowMobileTargets)
				{
					this.NavigationScope.FollowMobileTargets = value;
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008D77 File Offset: 0x00006F77
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00008D84 File Offset: 0x00006F84
		public bool DoNotAutoCollectChildScopes
		{
			get
			{
				return this.NavigationScope.DoNotAutoCollectChildScopes;
			}
			set
			{
				if (value != this.NavigationScope.DoNotAutoCollectChildScopes)
				{
					this.NavigationScope.DoNotAutoCollectChildScopes = value;
				}
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008DA0 File Offset: 0x00006FA0
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00008DAD File Offset: 0x00006FAD
		public bool IsDefaultNavigationScope
		{
			get
			{
				return this.NavigationScope.IsDefaultNavigationScope;
			}
			set
			{
				if (value != this.NavigationScope.IsDefaultNavigationScope)
				{
					this.NavigationScope.IsDefaultNavigationScope = value;
				}
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008DC9 File Offset: 0x00006FC9
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00008DD6 File Offset: 0x00006FD6
		public float ExtendDiscoveryAreaTop
		{
			get
			{
				return this.NavigationScope.ExtendDiscoveryAreaTop;
			}
			set
			{
				if (value != this.NavigationScope.ExtendDiscoveryAreaTop)
				{
					this.NavigationScope.ExtendDiscoveryAreaTop = value;
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008DF2 File Offset: 0x00006FF2
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00008DFF File Offset: 0x00006FFF
		public float ExtendDiscoveryAreaRight
		{
			get
			{
				return this.NavigationScope.ExtendDiscoveryAreaRight;
			}
			set
			{
				if (value != this.NavigationScope.ExtendDiscoveryAreaRight)
				{
					this.NavigationScope.ExtendDiscoveryAreaRight = value;
				}
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008E1B File Offset: 0x0000701B
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00008E28 File Offset: 0x00007028
		public float ExtendDiscoveryAreaBottom
		{
			get
			{
				return this.NavigationScope.ExtendDiscoveryAreaBottom;
			}
			set
			{
				if (value != this.NavigationScope.ExtendDiscoveryAreaBottom)
				{
					this.NavigationScope.ExtendDiscoveryAreaBottom = value;
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00008E44 File Offset: 0x00007044
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00008E51 File Offset: 0x00007051
		public float ExtendDiscoveryAreaLeft
		{
			get
			{
				return this.NavigationScope.ExtendDiscoveryAreaLeft;
			}
			set
			{
				if (value != this.NavigationScope.ExtendDiscoveryAreaLeft)
				{
					this.NavigationScope.ExtendDiscoveryAreaLeft = value;
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008E6D File Offset: 0x0000706D
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00008E7A File Offset: 0x0000707A
		public float ExtendChildrenCursorAreaLeft
		{
			get
			{
				return this.NavigationScope.ExtendChildrenCursorAreaLeft;
			}
			set
			{
				if (value != this.NavigationScope.ExtendChildrenCursorAreaLeft)
				{
					this.NavigationScope.ExtendChildrenCursorAreaLeft = value;
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008E96 File Offset: 0x00007096
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00008EA3 File Offset: 0x000070A3
		public float ExtendChildrenCursorAreaRight
		{
			get
			{
				return this.NavigationScope.ExtendChildrenCursorAreaRight;
			}
			set
			{
				if (value != this.NavigationScope.ExtendChildrenCursorAreaRight)
				{
					this.NavigationScope.ExtendChildrenCursorAreaRight = value;
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008EBF File Offset: 0x000070BF
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008ECC File Offset: 0x000070CC
		public float ExtendChildrenCursorAreaTop
		{
			get
			{
				return this.NavigationScope.ExtendChildrenCursorAreaTop;
			}
			set
			{
				if (value != this.NavigationScope.ExtendChildrenCursorAreaTop)
				{
					this.NavigationScope.ExtendChildrenCursorAreaTop = value;
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008EE8 File Offset: 0x000070E8
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00008EF5 File Offset: 0x000070F5
		public float ExtendChildrenCursorAreaBottom
		{
			get
			{
				return this.NavigationScope.ExtendChildrenCursorAreaBottom;
			}
			set
			{
				if (value != this.NavigationScope.ExtendChildrenCursorAreaBottom)
				{
					this.NavigationScope.ExtendChildrenCursorAreaBottom = value;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008F11 File Offset: 0x00007111
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00008F1E File Offset: 0x0000711E
		public float DiscoveryAreaOffsetX
		{
			get
			{
				return this.NavigationScope.DiscoveryAreaOffsetX;
			}
			set
			{
				if (value != this.NavigationScope.DiscoveryAreaOffsetX)
				{
					this.NavigationScope.DiscoveryAreaOffsetX = value;
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00008F3A File Offset: 0x0000713A
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00008F47 File Offset: 0x00007147
		public float DiscoveryAreaOffsetY
		{
			get
			{
				return this.NavigationScope.DiscoveryAreaOffsetY;
			}
			set
			{
				if (value != this.NavigationScope.DiscoveryAreaOffsetY)
				{
					this.NavigationScope.DiscoveryAreaOffsetY = value;
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00008F63 File Offset: 0x00007163
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00008F70 File Offset: 0x00007170
		public bool IsScopeEnabled
		{
			get
			{
				return this.NavigationScope.IsEnabled;
			}
			set
			{
				if (value != this.NavigationScope.IsEnabled)
				{
					this.NavigationScope.IsEnabled = value;
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00008F8C File Offset: 0x0000718C
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00008F99 File Offset: 0x00007199
		public bool IsScopeDisabled
		{
			get
			{
				return this.NavigationScope.IsDisabled;
			}
			set
			{
				if (value != this.NavigationScope.IsDisabled)
				{
					this.NavigationScope.IsDisabled = value;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00008FB5 File Offset: 0x000071B5
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00008FC2 File Offset: 0x000071C2
		public string UpNavigationScope
		{
			get
			{
				return this.NavigationScope.UpNavigationScopeID;
			}
			set
			{
				if (value != this.NavigationScope.UpNavigationScopeID)
				{
					this.NavigationScope.UpNavigationScopeID = value;
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00008FE3 File Offset: 0x000071E3
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00008FF0 File Offset: 0x000071F0
		public string RightNavigationScope
		{
			get
			{
				return this.NavigationScope.RightNavigationScopeID;
			}
			set
			{
				if (value != this.NavigationScope.RightNavigationScopeID)
				{
					this.NavigationScope.RightNavigationScopeID = value;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00009011 File Offset: 0x00007211
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000901E File Offset: 0x0000721E
		public string DownNavigationScope
		{
			get
			{
				return this.NavigationScope.DownNavigationScopeID;
			}
			set
			{
				if (value != this.NavigationScope.DownNavigationScopeID)
				{
					this.NavigationScope.DownNavigationScopeID = value;
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000903F File Offset: 0x0000723F
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000904C File Offset: 0x0000724C
		public string LeftNavigationScope
		{
			get
			{
				return this.NavigationScope.LeftNavigationScopeID;
			}
			set
			{
				if (value != this.NavigationScope.LeftNavigationScopeID)
				{
					this.NavigationScope.LeftNavigationScopeID = value;
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000906D File Offset: 0x0000726D
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00009075 File Offset: 0x00007275
		public NavigationScopeTargeter UpNavigationScopeTargeter
		{
			get
			{
				return this._upNavigationScopeTargeter;
			}
			set
			{
				if (value != this._upNavigationScopeTargeter)
				{
					this._upNavigationScopeTargeter = value;
					this.NavigationScope.UpNavigationScope = value.NavigationScope;
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00009098 File Offset: 0x00007298
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000090A0 File Offset: 0x000072A0
		public NavigationScopeTargeter RightNavigationScopeTargeter
		{
			get
			{
				return this._rightNavigationScopeTargeter;
			}
			set
			{
				if (value != this._rightNavigationScopeTargeter)
				{
					this._rightNavigationScopeTargeter = value;
					this.NavigationScope.RightNavigationScope = value.NavigationScope;
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000090C3 File Offset: 0x000072C3
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000090CB File Offset: 0x000072CB
		public NavigationScopeTargeter DownNavigationScopeTargeter
		{
			get
			{
				return this._downNavigationScopeTargeter;
			}
			set
			{
				if (value != this._downNavigationScopeTargeter)
				{
					this._downNavigationScopeTargeter = value;
					this.NavigationScope.DownNavigationScope = value.NavigationScope;
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000090EE File Offset: 0x000072EE
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000090F6 File Offset: 0x000072F6
		public NavigationScopeTargeter LeftNavigationScopeTargeter
		{
			get
			{
				return this._leftNavigationScopeTargeter;
			}
			set
			{
				if (value != this._leftNavigationScopeTargeter)
				{
					this._leftNavigationScopeTargeter = value;
					this.NavigationScope.LeftNavigationScope = value.NavigationScope;
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00009119 File Offset: 0x00007319
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00009128 File Offset: 0x00007328
		public Widget ScopeParent
		{
			get
			{
				return this.NavigationScope.ParentWidget;
			}
			set
			{
				if (this.NavigationScope.ParentWidget != value)
				{
					if (this.NavigationScope.ParentWidget != null)
					{
						base.GamepadNavigationContext.RemoveNavigationScope(this.NavigationScope);
					}
					this.NavigationScope.ParentWidget = value;
					this.NavigationScope.ParentWidget.EventFire += this.OnParentConnectedToTheRoot;
					base.GamepadNavigationContext.AddNavigationScope(this.NavigationScope, false);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000919B File Offset: 0x0000739B
		private void OnParentConnectedToTheRoot(Widget widget, string eventName, object[] arguments)
		{
			if (eventName == "ConnectedToRoot" && !base.GamepadNavigationContext.HasNavigationScope(this.NavigationScope))
			{
				base.GamepadNavigationContext.AddNavigationScope(this.NavigationScope, false);
			}
		}

		// Token: 0x0400011D RID: 285
		private NavigationScopeTargeter _upNavigationScopeTargeter;

		// Token: 0x0400011E RID: 286
		private NavigationScopeTargeter _rightNavigationScopeTargeter;

		// Token: 0x0400011F RID: 287
		private NavigationScopeTargeter _downNavigationScopeTargeter;

		// Token: 0x04000120 RID: 288
		private NavigationScopeTargeter _leftNavigationScopeTargeter;
	}
}
