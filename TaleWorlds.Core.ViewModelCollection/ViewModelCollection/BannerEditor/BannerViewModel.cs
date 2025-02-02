using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.BannerEditor
{
	// Token: 0x02000028 RID: 40
	public class BannerViewModel : ViewModel
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005B70 File Offset: 0x00003D70
		public Banner Banner { get; }

		// Token: 0x060001D5 RID: 469 RVA: 0x00005B78 File Offset: 0x00003D78
		public BannerViewModel(Banner banner)
		{
			this.Banner = banner;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005B92 File Offset: 0x00003D92
		public void SetCode(string code)
		{
			this.BannerCode = code;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005B9B File Offset: 0x00003D9B
		public void SetIconMeshID(int meshID)
		{
			this.Banner.BannerDataList[1].MeshId = meshID;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public void SetPrimaryColorId(int colorID)
		{
			this.Banner.BannerDataList[0].ColorId = colorID;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005BCD File Offset: 0x00003DCD
		public void SetSecondaryColorId(int colorID)
		{
			this.Banner.BannerDataList[0].ColorId2 = colorID;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005BE6 File Offset: 0x00003DE6
		public void SetSigilColorId(int colorID)
		{
			this.Banner.BannerDataList[1].ColorId = colorID;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005BFF File Offset: 0x00003DFF
		public void SetIconSize(int newSize)
		{
			this.Banner.BannerDataList[1].Size = new Vec2((float)newSize, (float)newSize);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005C20 File Offset: 0x00003E20
		public int GetPrimaryColorId()
		{
			return this.Banner.BannerDataList[0].ColorId;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005C38 File Offset: 0x00003E38
		public uint GetPrimaryColor()
		{
			return BannerManager.Instance.ReadOnlyColorPalette.First((KeyValuePair<int, BannerColor> w) => w.Key == this.GetPrimaryColorId()).Value.Color;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005C70 File Offset: 0x00003E70
		public int GetSecondaryColorId()
		{
			return this.Banner.BannerDataList[0].ColorId2;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005C88 File Offset: 0x00003E88
		public int GetSigilColorId()
		{
			return this.Banner.BannerDataList[1].ColorId;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public uint GetSigilColor()
		{
			return BannerManager.Instance.ReadOnlyColorPalette.First((KeyValuePair<int, BannerColor> w) => w.Key == this.GetSigilColorId()).Value.Color;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005CD8 File Offset: 0x00003ED8
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00005CE0 File Offset: 0x00003EE0
		[DataSourceProperty]
		public string BannerCode
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerCode");
					this.Banner.Deserialize(value);
				}
			}
		}

		// Token: 0x040000C0 RID: 192
		private string _bannerCode = "";

		// Token: 0x040000C1 RID: 193
		private const int _backgroundIndex = 0;

		// Token: 0x040000C2 RID: 194
		private const int _bannerIconIndex = 1;
	}
}
