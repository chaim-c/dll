﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000389 RID: 905
	internal class DisorganizedStateCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060035B8 RID: 13752 RVA: 0x000E97E8 File Offset: 0x000E79E8
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnd));
			CampaignEvents.PartyRemovedFromArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyRemovedFromArmy));
			CampaignEvents.GameMenuOptionSelectedEvent.AddNonSerializedListener(this, new Action<GameMenuOption>(this.OnGameMenuOptionSelected));
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000E9854 File Offset: 0x000E7A54
		private void OnGameMenuOptionSelected(GameMenuOption gameMenuOption)
		{
			if (this._checkForEvent && (gameMenuOption.IdString == "str_order_attack" || gameMenuOption.IdString == "attack"))
			{
				foreach (MapEventParty mapEventParty in MobileParty.MainParty.MapEvent.DefenderSide.Parties)
				{
					if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mapEventParty.Party))
					{
						mapEventParty.Party.MobileParty.SetDisorganized(true);
					}
				}
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000E990C File Offset: 0x000E7B0C
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsSallyOut)
			{
				if (!mapEvent.AttackerSide.IsMainPartyAmongParties())
				{
					using (List<MapEventParty>.Enumerator enumerator = mapEvent.DefenderSide.Parties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MapEventParty mapEventParty = enumerator.Current;
							if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mapEventParty.Party))
							{
								mapEventParty.Party.MobileParty.SetDisorganized(true);
							}
						}
						return;
					}
				}
				this._checkForEvent = true;
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000E99A8 File Offset: 0x000E7BA8
		private void OnPartyRemovedFromArmy(MobileParty mobileParty)
		{
			if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mobileParty.Party))
			{
				mobileParty.SetDisorganized(true);
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000E99D0 File Offset: 0x000E7BD0
		private void OnMapEventEnd(MapEvent mapEvent)
		{
			bool flag;
			if (mapEvent.AttackerSide.Parties.Sum((MapEventParty x) => x.HealthyManCountAtStart) == mapEvent.AttackerSide.Parties.Sum((MapEventParty x) => x.Party.NumberOfHealthyMembers))
			{
				flag = (mapEvent.DefenderSide.Parties.Sum((MapEventParty x) => x.HealthyManCountAtStart) != mapEvent.DefenderSide.Parties.Sum((MapEventParty x) => x.Party.NumberOfHealthyMembers));
			}
			else
			{
				flag = true;
			}
			if (flag && !mapEvent.IsHideoutBattle)
			{
				foreach (PartyBase partyBase in mapEvent.InvolvedParties)
				{
					if (partyBase.IsActive)
					{
						MobileParty mobileParty = partyBase.MobileParty;
						if ((mobileParty == null || !mobileParty.IsMainParty || !mapEvent.DiplomaticallyFinished || !mapEvent.AttackerSide.MapFaction.IsAtWarWith(mapEvent.DefenderSide.MapFaction)) && (!mapEvent.IsSallyOut || partyBase.MapEventSide.MissionSide == BattleSideEnum.Defender) && Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(partyBase))
						{
							partyBase.MobileParty.SetDisorganized(true);
						}
					}
				}
			}
			this._checkForEvent = false;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000E9B74 File Offset: 0x000E7D74
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<bool>("_checkForEvent", ref this._checkForEvent);
		}

		// Token: 0x04001140 RID: 4416
		private bool _checkForEvent;
	}
}
