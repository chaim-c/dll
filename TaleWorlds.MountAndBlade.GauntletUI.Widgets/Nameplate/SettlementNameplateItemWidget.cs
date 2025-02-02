using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate
{
	// Token: 0x02000078 RID: 120
	public class SettlementNameplateItemWidget : Widget
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x00013D4F File Offset: 0x00011F4F
		public SettlementNameplateItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00013D58 File Offset: 0x00011F58
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00013D60 File Offset: 0x00011F60
		public bool IsOverWidget { get; private set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00013D69 File Offset: 0x00011F69
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00013D71 File Offset: 0x00011F71
		public int QuestType { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00013D7A File Offset: 0x00011F7A
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00013D82 File Offset: 0x00011F82
		public int IssueType { get; set; }

		// Token: 0x060006AD RID: 1709 RVA: 0x00013D8C File Offset: 0x00011F8C
		public void ParallelUpdate(float dt)
		{
			Widget widgetToShow = this._widgetToShow;
			Widget parentWidget = base.ParentWidget;
			if (widgetToShow == null)
			{
				Debug.FailedAssert("widgetToShow is null during ParallelUpdate!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Nameplate\\SettlementNameplateItemWidget.cs", "ParallelUpdate", 24);
				return;
			}
			if (parentWidget != null && parentWidget.IsEnabled)
			{
				this.IsOverWidget = this.IsMouseOverWidget();
				if (this.IsOverWidget && !this._hoverBegan)
				{
					this._hoverBegan = true;
					widgetToShow.IsVisible = true;
				}
				else if (!this.IsOverWidget && this._hoverBegan)
				{
					this._hoverBegan = false;
					widgetToShow.IsVisible = false;
				}
				if (!this.IsOverWidget && widgetToShow.IsVisible)
				{
					widgetToShow.IsVisible = false;
					return;
				}
			}
			else
			{
				widgetToShow.IsVisible = false;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00013E38 File Offset: 0x00012038
		private bool IsMouseOverWidget()
		{
			Vector2 globalPosition = base.GlobalPosition;
			return this.IsBetween(base.EventManager.MousePosition.X, globalPosition.X, globalPosition.X + base.Size.X) && this.IsBetween(base.EventManager.MousePosition.Y, globalPosition.Y, globalPosition.Y + base.Size.Y);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00013EAC File Offset: 0x000120AC
		private bool IsBetween(float number, float min, float max)
		{
			return number >= min && number <= max;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00013EBB File Offset: 0x000120BB
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00013EC3 File Offset: 0x000120C3
		public Widget SettlementNameplateCapsuleWidget
		{
			get
			{
				return this._settlementNameplateCapsuleWidget;
			}
			set
			{
				if (this._settlementNameplateCapsuleWidget != value)
				{
					this._settlementNameplateCapsuleWidget = value;
					base.OnPropertyChanged<Widget>(value, "SettlementNameplateCapsuleWidget");
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00013EE1 File Offset: 0x000120E1
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00013EE9 File Offset: 0x000120E9
		public GridWidget SettlementPartiesGridWidget
		{
			get
			{
				return this._settlementPartiesGridWidget;
			}
			set
			{
				if (this._settlementPartiesGridWidget != value)
				{
					this._settlementPartiesGridWidget = value;
					base.OnPropertyChanged<GridWidget>(value, "SettlementPartiesGridWidget");
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00013F07 File Offset: 0x00012107
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00013F0F File Offset: 0x0001210F
		public MapEventVisualBrushWidget MapEventVisualWidget
		{
			get
			{
				return this._mapEventVisualWidget;
			}
			set
			{
				if (this._mapEventVisualWidget != value)
				{
					this._mapEventVisualWidget = value;
					base.OnPropertyChanged<MapEventVisualBrushWidget>(value, "MapEventVisualWidget");
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00013F2D File Offset: 0x0001212D
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00013F35 File Offset: 0x00012135
		[Editor(false)]
		public Widget WidgetToShow
		{
			get
			{
				return this._widgetToShow;
			}
			set
			{
				if (this._widgetToShow != value)
				{
					this._widgetToShow = value;
					base.OnPropertyChanged<Widget>(value, "WidgetToShow");
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00013F53 File Offset: 0x00012153
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00013F5B File Offset: 0x0001215B
		public Widget SettlementNameplateInspectedWidget
		{
			get
			{
				return this._settlementNameplateInspectedWidget;
			}
			set
			{
				if (this._settlementNameplateInspectedWidget != value)
				{
					this._settlementNameplateInspectedWidget = value;
					base.OnPropertyChanged<Widget>(value, "SettlementNameplateInspectedWidget");
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00013F79 File Offset: 0x00012179
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00013F81 File Offset: 0x00012181
		public MaskedTextureWidget SettlementBannerWidget
		{
			get
			{
				return this._settlementBannerWidget;
			}
			set
			{
				if (this._settlementBannerWidget != value)
				{
					this._settlementBannerWidget = value;
					base.OnPropertyChanged<MaskedTextureWidget>(value, "SettlementBannerWidget");
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00013F9F File Offset: 0x0001219F
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00013FA7 File Offset: 0x000121A7
		public TextWidget SettlementNameTextWidget
		{
			get
			{
				return this._settlementNameTextWidget;
			}
			set
			{
				if (this._settlementNameTextWidget != value)
				{
					this._settlementNameTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "SettlementNameTextWidget");
				}
			}
		}

		// Token: 0x040002E9 RID: 745
		private bool _hoverBegan;

		// Token: 0x040002ED RID: 749
		private Widget _settlementNameplateCapsuleWidget;

		// Token: 0x040002EE RID: 750
		private Widget _settlementNameplateInspectedWidget;

		// Token: 0x040002EF RID: 751
		private MapEventVisualBrushWidget _mapEventVisualWidget;

		// Token: 0x040002F0 RID: 752
		private MaskedTextureWidget _settlementBannerWidget;

		// Token: 0x040002F1 RID: 753
		private TextWidget _settlementNameTextWidget;

		// Token: 0x040002F2 RID: 754
		private GridWidget _settlementPartiesGridWidget;

		// Token: 0x040002F3 RID: 755
		private Widget _widgetToShow;
	}
}
