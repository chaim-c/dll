﻿using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200037C RID: 892
	public class CampaignWarManagerBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600347F RID: 13439 RVA: 0x000DCEFD File Offset: 0x000DB0FD
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.MapEventEnded));
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000DCF18 File Offset: 0x000DB118
		private void MapEventEnded(MapEvent mapEvent)
		{
			if (mapEvent.AttackerSide.LeaderParty.MapFaction != null && !mapEvent.AttackerSide.LeaderParty.MapFaction.IsBanditFaction && mapEvent.DefenderSide.LeaderParty.MapFaction != null && !mapEvent.DefenderSide.LeaderParty.MapFaction.IsBanditFaction)
			{
				IFaction mapFaction = mapEvent.AttackerSide.MapFaction;
				IFaction mapFaction2 = mapEvent.DefenderSide.MapFaction;
				if (mapFaction.MapFaction != mapFaction2.MapFaction)
				{
					StanceLink stanceWith = mapFaction.GetStanceWith(mapFaction2);
					stanceWith.Casualties1 += ((stanceWith.Faction1 == mapFaction) ? mapEvent.AttackerSide.Casualties : mapEvent.DefenderSide.Casualties);
					stanceWith.Casualties2 += ((stanceWith.Faction2 == mapFaction) ? mapEvent.AttackerSide.Casualties : mapEvent.DefenderSide.Casualties);
					if (mapEvent.MapEventSettlement != null && mapEvent.BattleState == BattleState.AttackerVictory)
					{
						if (mapEvent.MapEventSettlement.IsVillage && mapEvent.MapEventSettlement.Village.VillageState == Village.VillageStates.Looted)
						{
							int num;
							if (mapFaction == stanceWith.Faction1)
							{
								StanceLink stanceLink = stanceWith;
								num = stanceLink.SuccessfulRaids1;
								stanceLink.SuccessfulRaids1 = num + 1;
								return;
							}
							StanceLink stanceLink2 = stanceWith;
							num = stanceLink2.SuccessfulRaids2;
							stanceLink2.SuccessfulRaids2 = num + 1;
							return;
						}
						else if (mapEvent.MapEventSettlement.IsFortification && mapEvent.EventType == MapEvent.BattleTypes.Siege)
						{
							int num;
							if (mapFaction == stanceWith.Faction1)
							{
								StanceLink stanceLink3 = stanceWith;
								num = stanceLink3.SuccessfulSieges1;
								stanceLink3.SuccessfulSieges1 = num + 1;
								return;
							}
							StanceLink stanceLink4 = stanceWith;
							num = stanceLink4.SuccessfulSieges2;
							stanceLink4.SuccessfulSieges2 = num + 1;
						}
					}
				}
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000DD0B2 File Offset: 0x000DB2B2
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
