﻿using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x02000354 RID: 852
	public class TraitLevelingHelper
	{
		// Token: 0x060030A9 RID: 12457 RVA: 0x000CE018 File Offset: 0x000CC218
		private static void AddPlayerTraitXPAndLogEntry(TraitObject trait, int xpValue, ActionNotes context, Hero referenceHero)
		{
			int traitLevel = Hero.MainHero.GetTraitLevel(trait);
			Campaign.Current.PlayerTraitDeveloper.AddTraitXp(trait, xpValue);
			if (traitLevel != Hero.MainHero.GetTraitLevel(trait))
			{
				CampaignEventDispatcher.Instance.OnPlayerTraitChanged(trait, traitLevel);
			}
			if (MathF.Abs(xpValue) >= 10)
			{
				LogEntry.AddLogEntry(new PlayerReputationChangesLogEntry(trait, referenceHero, context));
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000CE074 File Offset: 0x000CC274
		public static void OnBattleWon(MapEvent mapEvent, float contribution)
		{
			float num = 0f;
			float strengthRatio = mapEvent.GetMapEventSide(PlayerEncounter.Current.PlayerSide).StrengthRatio;
			if (strengthRatio > 0.9f)
			{
				num = MathF.Min(20f, MathF.Sqrt(mapEvent.StrengthOfSide[(int)mapEvent.GetOtherSide(PlayerEncounter.Current.PlayerSide)]) * strengthRatio);
			}
			int xpValue = (int)(num * contribution);
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Valor, xpValue, ActionNotes.BattleValor, null);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000CE0E1 File Offset: 0x000CC2E1
		public static void OnTroopsSacrificed()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Valor, -30, ActionNotes.SacrificedTroops, null);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000CE0F2 File Offset: 0x000CC2F2
		public static void OnLordExecuted()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, -1000, ActionNotes.SacrificedTroops, null);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000CE106 File Offset: 0x000CC306
		public static void OnVillageRaided()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Mercy, -30, ActionNotes.VillageRaid, null);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000CE117 File Offset: 0x000CC317
		public static void OnHostileAction(int amount)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Honor, amount, ActionNotes.HostileAction, null);
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Mercy, amount, ActionNotes.HostileAction, null);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000CE135 File Offset: 0x000CC335
		public static void OnPartyTreatedWell()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Generosity, 20, ActionNotes.PartyTakenCareOf, null);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000CE146 File Offset: 0x000CC346
		public static void OnPartyStarved()
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Generosity, -20, ActionNotes.PartyHungry, null);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000CE158 File Offset: 0x000CC358
		public static void OnIssueFailed(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestFailed, targetHero);
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000CE190 File Offset: 0x000CC390
		public static void OnIssueSolvedThroughQuest(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestSuccess, targetHero);
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000CE1C8 File Offset: 0x000CC3C8
		public static void OnIssueSolvedThroughAlternativeSolution(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestSuccess, targetHero);
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000CE200 File Offset: 0x000CC400
		public static void OnIssueSolvedThroughBetrayal(Hero targetHero, Tuple<TraitObject, int>[] effectedTraits)
		{
			foreach (Tuple<TraitObject, int> tuple in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(tuple.Item1, tuple.Item2, ActionNotes.QuestBetrayal, targetHero);
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000CE235 File Offset: 0x000CC435
		public static void OnLordFreed(Hero targetHero)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Calculating, 20, ActionNotes.NPCFreed, targetHero);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000CE246 File Offset: 0x000CC446
		public static void OnPersuasionDefection(Hero targetHero)
		{
			TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(DefaultTraits.Calculating, 20, ActionNotes.PersuadedToDefect, targetHero);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000CE258 File Offset: 0x000CC458
		public static void OnSiegeAftermathApplied(Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, TraitObject[] effectedTraits)
		{
			foreach (TraitObject trait in effectedTraits)
			{
				TraitLevelingHelper.AddPlayerTraitXPAndLogEntry(trait, Campaign.Current.Models.SiegeAftermathModel.GetSiegeAftermathTraitXpChangeForPlayer(trait, settlement, aftermathType), ActionNotes.SiegeAftermath, null);
			}
		}

		// Token: 0x04000FDC RID: 4060
		private const int LordExecutedHonorPenalty = -1000;

		// Token: 0x04000FDD RID: 4061
		private const int TroopsSacrificedValorPenalty = -30;

		// Token: 0x04000FDE RID: 4062
		private const int VillageRaidedMercyPenalty = -30;

		// Token: 0x04000FDF RID: 4063
		private const int PartyStarvingGenerosityPenalty = -20;

		// Token: 0x04000FE0 RID: 4064
		private const int PartyTreatedWellGenerosityBonus = 20;

		// Token: 0x04000FE1 RID: 4065
		private const int LordFreedCalculatingBonus = 20;

		// Token: 0x04000FE2 RID: 4066
		private const int PersuasionDefectionCalculatingBonus = 20;
	}
}
