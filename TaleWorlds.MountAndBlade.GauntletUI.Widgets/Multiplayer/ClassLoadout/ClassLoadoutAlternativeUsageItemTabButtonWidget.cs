using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C2 RID: 194
	public class ClassLoadoutAlternativeUsageItemTabButtonWidget : ButtonWidget
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0001D16C File Offset: 0x0001B36C
		public ClassLoadoutAlternativeUsageItemTabButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001D178 File Offset: 0x0001B378
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.UsageType) || this._iconWidget == null)
			{
				return;
			}
			Sprite sprite = base.Context.SpriteData.GetSprite("MPClassLoadout\\UsageIcons\\" + this.UsageType);
			foreach (Style style in this.IconWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Sprite = sprite;
				}
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001D224 File Offset: 0x0001B424
		protected override void RefreshState()
		{
			base.RefreshState();
			if (base.IsSelected && base.ParentWidget is Container)
			{
				(base.ParentWidget as Container).OnChildSelected(this);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001D252 File Offset: 0x0001B452
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0001D25A File Offset: 0x0001B45A
		public string UsageType
		{
			get
			{
				return this._usageType;
			}
			set
			{
				if (value != this._usageType)
				{
					this._usageType = value;
					base.OnPropertyChanged<string>(value, "UsageType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001D283 File Offset: 0x0001B483
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0001D28B File Offset: 0x0001B48B
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

		// Token: 0x040004B8 RID: 1208
		private string _usageType;

		// Token: 0x040004B9 RID: 1209
		private BrushWidget _iconWidget;
	}
}
