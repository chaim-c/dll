using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200038F RID: 911
	public class FindingItemOnMapBehavior : CampaignBehaviorBase
	{
		// Token: 0x060036CC RID: 14028 RVA: 0x000F5513 File Offset: 0x000F3713
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.DailyTickParty));
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000F552C File Offset: 0x000F372C
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000F5530 File Offset: 0x000F3730
		public void DailyTickParty(MobileParty party)
		{
			TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
			if ((faceTerrainType == TerrainType.Steppe || faceTerrainType == TerrainType.Plain) && party.HasPerk(DefaultPerks.Scouting.BeastWhisperer, false) && MBRandom.RandomFloat < DefaultPerks.Scouting.BeastWhisperer.PrimaryBonus)
			{
				ItemObject randomElementWithPredicate = Items.All.GetRandomElementWithPredicate((ItemObject x) => x.IsMountable && !x.NotMerchandise);
				if (randomElementWithPredicate != null)
				{
					party.ItemRoster.AddToCounts(randomElementWithPredicate, 1);
					if (party.IsMainParty)
					{
						TextObject textObject = new TextObject("{=vl9bawa7}{COUNT} {?(COUNT > 1)}{PLURAL(ANIMAL_NAME)} are{?}{ANIMAL_NAME} is{\\?} added to your party.", null);
						textObject.SetTextVariable("COUNT", 1);
						textObject.SetTextVariable("ANIMAL_NAME", randomElementWithPredicate.Name);
						InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
					}
				}
			}
		}
	}
}
