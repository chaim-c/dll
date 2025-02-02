﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace SandBox.GameComponents
{
	// Token: 0x02000097 RID: 151
	public class SandboxBattleInitializationModel : BattleInitializationModel
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public override List<FormationClass> GetAllAvailableTroopTypes()
		{
			List<FormationClass> list = new List<FormationClass>();
			MapEventSide mapEventSide = PlayerEncounter.Battle.GetMapEventSide(PlayerEncounter.Battle.PlayerSide);
			bool flag = PlayerEncounter.Battle.GetLeaderParty(PlayerEncounter.Battle.PlayerSide) == PartyBase.MainParty;
			bool flag2 = PartyBase.MainParty.MobileParty.Army != null && PartyBase.MainParty.MobileParty.Army.LeaderParty == PartyBase.MainParty.MobileParty;
			bool flag3 = flag && flag2;
			for (int i = 0; i < mapEventSide.Parties.Count; i++)
			{
				MapEventParty mapEventParty = mapEventSide.Parties[i];
				if (flag3 || mapEventParty.Party == PartyBase.MainParty)
				{
					for (int j = 0; j < mapEventParty.Party.MemberRoster.Count; j++)
					{
						CharacterObject characterAtIndex = mapEventParty.Party.MemberRoster.GetCharacterAtIndex(j);
						TroopRosterElement elementCopyAtIndex = mapEventParty.Party.MemberRoster.GetElementCopyAtIndex(j);
						if (!characterAtIndex.IsHero && elementCopyAtIndex.WoundedNumber < elementCopyAtIndex.Number)
						{
							if (characterAtIndex.IsInfantry && !characterAtIndex.IsMounted && !list.Contains(FormationClass.Infantry))
							{
								list.Add(FormationClass.Infantry);
							}
							if (characterAtIndex.IsRanged && !characterAtIndex.IsMounted && !list.Contains(FormationClass.Ranged))
							{
								list.Add(FormationClass.Ranged);
							}
							if (characterAtIndex.IsMounted && !characterAtIndex.IsRanged && !list.Contains(FormationClass.Cavalry))
							{
								list.Add(FormationClass.Cavalry);
							}
							if (characterAtIndex.IsMounted && characterAtIndex.IsRanged && !list.Contains(FormationClass.HorseArcher))
							{
								list.Add(FormationClass.HorseArcher);
							}
							if (list.Count == 4)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00029C7C File Offset: 0x00027E7C
		protected override bool CanPlayerSideDeployWithOrderOfBattleAux()
		{
			if (Mission.Current.IsSallyOutBattle)
			{
				return false;
			}
			MapEvent playerMapEvent = MapEvent.PlayerMapEvent;
			if (MapEvent.PlayerMapEvent == null)
			{
				return false;
			}
			PartyBase leaderParty = playerMapEvent.GetLeaderParty(playerMapEvent.PlayerSide);
			return (leaderParty == PartyBase.MainParty || (leaderParty.IsSettlement && leaderParty.Settlement.OwnerClan.Leader == Hero.MainHero) || playerMapEvent.IsPlayerSergeant()) && Mission.Current.GetMissionBehavior<MissionAgentSpawnLogic>().GetNumberOfPlayerControllableTroops() >= 20;
		}
	}
}
