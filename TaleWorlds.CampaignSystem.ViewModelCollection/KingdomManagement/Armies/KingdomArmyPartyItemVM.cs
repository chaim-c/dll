using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies
{
	// Token: 0x02000079 RID: 121
	public class KingdomArmyPartyItemVM : ViewModel
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002AF9B File Offset: 0x0002919B
		public KingdomArmyPartyItemVM(MobileParty party)
		{
			this._party = party;
			Hero leaderHero = party.LeaderHero;
			this.Visual = new ImageIdentifierVM(CampaignUIHelper.GetCharacterCode((leaderHero != null) ? leaderHero.CharacterObject : null, false));
			this.RefreshValues();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002AFD3 File Offset: 0x000291D3
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._party.Name.ToString();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002AFF1 File Offset: 0x000291F1
		private void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(MobileParty), new object[]
			{
				this._party,
				true,
				false
			});
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002B023 File Offset: 0x00029223
		private void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002B02A File Offset: 0x0002922A
		public void ExecuteLink()
		{
			if (this._party != null && this._party.LeaderHero != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._party.LeaderHero.EncyclopediaLink);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B060 File Offset: 0x00029260
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B068 File Offset: 0x00029268
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002B086 File Offset: 0x00029286
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B08E File Offset: 0x0002928E
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x040004D9 RID: 1241
		private MobileParty _party;

		// Token: 0x040004DA RID: 1242
		private ImageIdentifierVM _visual;

		// Token: 0x040004DB RID: 1243
		private string _name;
	}
}
