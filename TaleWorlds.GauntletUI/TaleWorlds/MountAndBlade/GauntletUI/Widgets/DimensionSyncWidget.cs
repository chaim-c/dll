using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000007 RID: 7
	public class DimensionSyncWidget : Widget
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002202 File Offset: 0x00000402
		public DimensionSyncWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000220C File Offset: 0x0000040C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.DimensionToSync != DimensionSyncWidget.Dimensions.None && this.WidgetToCopyHeightFrom != null)
			{
				if (this.DimensionToSync == DimensionSyncWidget.Dimensions.Horizontal || this.DimensionToSync == DimensionSyncWidget.Dimensions.HorizontalAndVertical)
				{
					base.ScaledSuggestedWidth = this.WidgetToCopyHeightFrom.Size.X + (float)this.PaddingAmount * base._scaleToUse;
				}
				if (this.DimensionToSync == DimensionSyncWidget.Dimensions.Vertical || this.DimensionToSync == DimensionSyncWidget.Dimensions.HorizontalAndVertical)
				{
					base.ScaledSuggestedHeight = this.WidgetToCopyHeightFrom.Size.Y + (float)this.PaddingAmount * base._scaleToUse;
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000229E File Offset: 0x0000049E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000022A6 File Offset: 0x000004A6
		public Widget WidgetToCopyHeightFrom
		{
			get
			{
				return this._widgetToCopyHeightFrom;
			}
			set
			{
				if (this._widgetToCopyHeightFrom != value)
				{
					this._widgetToCopyHeightFrom = value;
					base.OnPropertyChanged<Widget>(value, "WidgetToCopyHeightFrom");
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000022CC File Offset: 0x000004CC
		public int PaddingAmount
		{
			get
			{
				return this._paddingAmount;
			}
			set
			{
				if (this._paddingAmount != value)
				{
					this._paddingAmount = value;
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000022DE File Offset: 0x000004DE
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000022E6 File Offset: 0x000004E6
		public DimensionSyncWidget.Dimensions DimensionToSync
		{
			get
			{
				return this._dimensionToSync;
			}
			set
			{
				if (this._dimensionToSync != value)
				{
					this._dimensionToSync = value;
				}
			}
		}

		// Token: 0x04000004 RID: 4
		private Widget _widgetToCopyHeightFrom;

		// Token: 0x04000005 RID: 5
		private DimensionSyncWidget.Dimensions _dimensionToSync;

		// Token: 0x04000006 RID: 6
		private int _paddingAmount;

		// Token: 0x02000075 RID: 117
		public enum Dimensions
		{
			// Token: 0x040003E4 RID: 996
			None,
			// Token: 0x040003E5 RID: 997
			Horizontal,
			// Token: 0x040003E6 RID: 998
			Vertical,
			// Token: 0x040003E7 RID: 999
			HorizontalAndVertical
		}
	}
}
