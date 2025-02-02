using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000061 RID: 97
	public class KingdomDiplomacyFactionItemVM : ViewModel
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x000237D2 File Offset: 0x000219D2
		public KingdomDiplomacyFactionItemVM(IFaction faction)
		{
			this.Hint = new HintViewModel(faction.Name, null);
			this.Visual = new ImageIdentifierVM(BannerCode.CreateFrom(faction.Banner), true);
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00023803 File Offset: 0x00021A03
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0002380B File Offset: 0x00021A0B
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00023829 File Offset: 0x00021A29
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00023831 File Offset: 0x00021A31
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x040003B9 RID: 953
		private HintViewModel _hint;

		// Token: 0x040003BA RID: 954
		private ImageIdentifierVM _visual;
	}
}
