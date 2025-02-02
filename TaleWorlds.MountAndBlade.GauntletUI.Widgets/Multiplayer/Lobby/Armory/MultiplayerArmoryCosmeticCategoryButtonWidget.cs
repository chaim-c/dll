using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000AB RID: 171
	public class MultiplayerArmoryCosmeticCategoryButtonWidget : ButtonWidget
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00019BF9 File Offset: 0x00017DF9
		public MultiplayerArmoryCosmeticCategoryButtonWidget(UIContext context) : base(context)
		{
			this.CosmeticTypeName = string.Empty;
			this.CosmeticCategoryName = string.Empty;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00019C18 File Offset: 0x00017E18
		private void UpdateCategorySprite()
		{
			if (string.IsNullOrEmpty(this.CosmeticCategoryName) || string.IsNullOrEmpty(this.CosmeticTypeName))
			{
				return;
			}
			Sprite sprite = null;
			if (this.CosmeticTypeName == "Clothing")
			{
				sprite = this.GetClothingCategorySprite(this.CosmeticCategoryName);
			}
			else if (this.CosmeticTypeName == "Taunt")
			{
				sprite = this.GetTauntCategorySprite(this.CosmeticCategoryName);
			}
			if (sprite != null)
			{
				base.Brush.DefaultLayer.Sprite = sprite;
				base.Brush.Sprite = sprite;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00019CA2 File Offset: 0x00017EA2
		private Sprite GetClothingCategorySprite(string clothingCategory)
		{
			Brush clothingCategorySpriteBrush = this.ClothingCategorySpriteBrush;
			if (clothingCategorySpriteBrush == null)
			{
				return null;
			}
			BrushLayer layer = clothingCategorySpriteBrush.GetLayer(clothingCategory);
			if (layer == null)
			{
				return null;
			}
			return layer.Sprite;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00019CC1 File Offset: 0x00017EC1
		private Sprite GetTauntCategorySprite(string tauntCategory)
		{
			Brush tauntCategorySpriteBrush = this.TauntCategorySpriteBrush;
			if (tauntCategorySpriteBrush == null)
			{
				return null;
			}
			BrushLayer layer = tauntCategorySpriteBrush.GetLayer(tauntCategory);
			if (layer == null)
			{
				return null;
			}
			return layer.Sprite;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00019CE0 File Offset: 0x00017EE0
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00019CE8 File Offset: 0x00017EE8
		[DataSourceProperty]
		public Brush ClothingCategorySpriteBrush
		{
			get
			{
				return this._clothingCategorySpriteBrush;
			}
			set
			{
				if (value != this._clothingCategorySpriteBrush)
				{
					this._clothingCategorySpriteBrush = value;
					base.OnPropertyChanged<Brush>(value, "ClothingCategorySpriteBrush");
					this.UpdateCategorySprite();
				}
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00019D0C File Offset: 0x00017F0C
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00019D14 File Offset: 0x00017F14
		[DataSourceProperty]
		public Brush TauntCategorySpriteBrush
		{
			get
			{
				return this._tauntCategorySpriteBrush;
			}
			set
			{
				if (value != this._tauntCategorySpriteBrush)
				{
					this._tauntCategorySpriteBrush = value;
					base.OnPropertyChanged<Brush>(value, "TauntCategorySpriteBrush");
					this.UpdateCategorySprite();
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00019D38 File Offset: 0x00017F38
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00019D40 File Offset: 0x00017F40
		[DataSourceProperty]
		public string CosmeticTypeName
		{
			get
			{
				return this._cosmeticTypeName;
			}
			set
			{
				if (value != this._cosmeticTypeName)
				{
					this._cosmeticTypeName = value;
					base.OnPropertyChanged<string>(value, "CosmeticTypeName");
					this.UpdateCategorySprite();
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00019D69 File Offset: 0x00017F69
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00019D71 File Offset: 0x00017F71
		[DataSourceProperty]
		public string CosmeticCategoryName
		{
			get
			{
				return this._cosmeticCategoryName;
			}
			set
			{
				if (value != this._cosmeticCategoryName)
				{
					this._cosmeticCategoryName = value;
					base.OnPropertyChanged<string>(value, "CosmeticCategoryName");
					this.UpdateCategorySprite();
				}
			}
		}

		// Token: 0x04000419 RID: 1049
		private const string _clothingTypeName = "Clothing";

		// Token: 0x0400041A RID: 1050
		private const string _tauntTypeName = "Taunt";

		// Token: 0x0400041B RID: 1051
		private Brush _clothingCategorySpriteBrush;

		// Token: 0x0400041C RID: 1052
		private Brush _tauntCategorySpriteBrush;

		// Token: 0x0400041D RID: 1053
		private string _cosmeticTypeName;

		// Token: 0x0400041E RID: 1054
		private string _cosmeticCategoryName;
	}
}
