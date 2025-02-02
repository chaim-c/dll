using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000083 RID: 131
	public class MultiplayerItemTabButtonWidget : ButtonWidget
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00015640 File Offset: 0x00013840
		public MultiplayerItemTabButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001564C File Offset: 0x0001384C
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.ItemType) || this._iconWidget == null)
			{
				return;
			}
			Sprite sprite = base.Context.SpriteData.GetSprite("StdAssets\\ItemIcons\\" + this.ItemType);
			this.IconWidget.Brush.DefaultLayer.Sprite = sprite;
			Sprite sprite2 = base.Context.SpriteData.GetSprite("StdAssets\\ItemIcons\\" + this.ItemType + "_selected");
			this.IconWidget.Brush.GetLayer("Selected").Sprite = sprite2;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000156E7 File Offset: 0x000138E7
		protected override void RefreshState()
		{
			base.RefreshState();
			if (base.IsSelected && base.ParentWidget is Container)
			{
				(base.ParentWidget as Container).OnChildSelected(this);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00015715 File Offset: 0x00013915
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001571D File Offset: 0x0001391D
		[Editor(false)]
		public string ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChanged<string>(value, "ItemType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00015746 File Offset: 0x00013946
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001574E File Offset: 0x0001394E
		[Editor(false)]
		public BrushWidget IconWidget
		{
			get
			{
				return this._iconWidget;
			}
			set
			{
				if (value != this._iconWidget)
				{
					this._iconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "IconWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x04000332 RID: 818
		private const string BaseSpritePath = "StdAssets\\ItemIcons\\";

		// Token: 0x04000333 RID: 819
		private string _itemType;

		// Token: 0x04000334 RID: 820
		private BrushWidget _iconWidget;
	}
}
