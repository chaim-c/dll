using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x0200003A RID: 58
	public class GameOverStatsProvider
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00012FD2 File Offset: 0x000111D2
		public GameOverStatsProvider()
		{
			this._statSource = Campaign.Current.GetCampaignBehavior<IStatisticsCampaignBehavior>();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012FEA File Offset: 0x000111EA
		public IEnumerable<StatCategory> GetGameOverStats()
		{
			yield return new StatCategory("General", this.GetGeneralStats(this._statSource));
			yield return new StatCategory("Battle", this.GetBattleStats(this._statSource));
			yield return new StatCategory("Finance", this.GetFinanceStats(this._statSource));
			yield return new StatCategory("Crafting", this.GetCraftingStats(this._statSource));
			yield return new StatCategory("Companion", this.GetCompanionStats(this._statSource));
			yield break;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012FFA File Offset: 0x000111FA
		private IEnumerable<StatItem> GetGeneralStats(IStatisticsCampaignBehavior source)
		{
			int num = (int)source.GetTotalTimePlayed().ToYears;
			int num2 = (int)source.GetTotalTimePlayed().ToSeasons % 4;
			int num3 = (int)source.GetTotalTimePlayed().ToDays % 21;
			GameTexts.SetVariable("YEAR_IS_PLURAL", (num != 1) ? 1 : 0);
			GameTexts.SetVariable("YEAR", num);
			GameTexts.SetVariable("SEASON_IS_PLURAL", (num2 != 1) ? 1 : 0);
			GameTexts.SetVariable("SEASON", num2);
			GameTexts.SetVariable("DAY_IS_PLURAL", (num3 != 1) ? 1 : 0);
			GameTexts.SetVariable("DAY", num3);
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_YEAR_years", null));
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_SEASON_seasons", null));
			string text = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			GameTexts.SetVariable("STR1", text);
			GameTexts.SetVariable("STR2", GameTexts.FindText("str_DAY_days", null));
			text = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			yield return new StatItem("CampaignPlayTime", text, StatItem.StatType.None);
			string content = string.Format("{0:0.##}", TimeSpan.FromSeconds(source.GetTotalTimePlayedInSeconds()).TotalHours);
			GameTexts.SetVariable("PLURAL_HOURS", 1);
			GameTexts.SetVariable("HOUR", content);
			yield return new StatItem("CampaignRealPlayTime", GameTexts.FindText("str_hours", null).ToString(), StatItem.StatType.None);
			yield return new StatItem("ChildrenBorn", source.GetNumberOfChildrenBorn().ToString(), StatItem.StatType.None);
			yield return new StatItem("InfluenceEarned", source.GetTotalInfluenceEarned().ToString(), StatItem.StatType.Influence);
			yield return new StatItem("IssuesSolved", source.GetNumberOfIssuesSolved().ToString(), StatItem.StatType.Issue);
			yield return new StatItem("TournamentsWon", source.GetNumberOfTournamentWins().ToString(), StatItem.StatType.Tournament);
			yield return new StatItem("HighestLeaderboardRank", source.GetHighestTournamentRank().ToString(), StatItem.StatType.None);
			yield return new StatItem("PrisonersRecruited", source.GetNumberOfPrisonersRecruited().ToString(), StatItem.StatType.None);
			yield return new StatItem("TroopsRecruited", source.GetNumberOfTroopsRecruited().ToString(), StatItem.StatType.None);
			yield return new StatItem("ClansDefected", source.GetNumberOfClansDefected().ToString(), StatItem.StatType.None);
			yield return new StatItem("TotalCrimeGained", source.GetTotalCrimeRatingGained().ToString(), StatItem.StatType.Crime);
			yield break;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001300A File Offset: 0x0001120A
		private IEnumerable<StatItem> GetBattleStats(IStatisticsCampaignBehavior source)
		{
			int numberOfBattlesWon = source.GetNumberOfBattlesWon();
			int numberOfBattlesLost = source.GetNumberOfBattlesLost();
			int content = numberOfBattlesWon + numberOfBattlesLost;
			GameTexts.SetVariable("BATTLES_WON", numberOfBattlesWon);
			GameTexts.SetVariable("BATTLES_LOST", numberOfBattlesLost);
			GameTexts.SetVariable("ALL_BATTLES", content);
			yield return new StatItem("BattlesWonLostAll", GameTexts.FindText("str_battles_won_lost", null).ToString(), StatItem.StatType.None);
			yield return new StatItem("BiggestBattleWonAsLeader", source.GetLargestBattleWonAsLeader().ToString(), StatItem.StatType.None);
			yield return new StatItem("BiggestArmyByPlayer", source.GetLargestArmyFormedByPlayer().ToString(), StatItem.StatType.None);
			yield return new StatItem("EnemyClansDestroyed", source.GetNumberOfEnemyClansDestroyed().ToString(), StatItem.StatType.None);
			yield return new StatItem("HeroesKilledInBattle", source.GetNumberOfHeroesKilledInBattle().ToString(), StatItem.StatType.Kill);
			yield return new StatItem("TroopsEliminatedByPlayer", source.GetNumberOfTroopsKnockedOrKilledByPlayer().ToString(), StatItem.StatType.Kill);
			yield return new StatItem("TroopsEliminatedByParty", source.GetNumberOfTroopsKnockedOrKilledAsParty().ToString(), StatItem.StatType.Kill);
			yield return new StatItem("HeroPrisonersTaken", source.GetNumberOfHeroPrisonersTaken().ToString(), StatItem.StatType.None);
			yield return new StatItem("TroopPrisonersTaken", source.GetNumberOfTroopPrisonersTaken().ToString(), StatItem.StatType.None);
			yield return new StatItem("CapturedTowns", source.GetNumberOfTownsCaptured().ToString(), StatItem.StatType.None);
			yield return new StatItem("CapturedCastles", source.GetNumberOfCastlesCaptured().ToString(), StatItem.StatType.None);
			yield return new StatItem("ClearedHideouts", source.GetNumberOfHideoutsCleared().ToString(), StatItem.StatType.None);
			yield return new StatItem("RaidedVillages", source.GetNumberOfVillagesRaided().ToString(), StatItem.StatType.None);
			double toDays = source.GetTimeSpentAsPrisoner().ToDays;
			string content2 = string.Format("{0:0.##}", toDays);
			GameTexts.SetVariable("DAY_IS_PLURAL", 1);
			GameTexts.SetVariable("DAY", content2);
			yield return new StatItem("DaysSpentAsPrisoner", GameTexts.FindText("str_DAY_days", null).ToString(), StatItem.StatType.None);
			yield break;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001301A File Offset: 0x0001121A
		private IEnumerable<StatItem> GetFinanceStats(IStatisticsCampaignBehavior source)
		{
			yield return new StatItem("TotalDenarsEarned", source.GetTotalDenarsEarned().ToString("0.##"), StatItem.StatType.Gold);
			yield return new StatItem("DenarsFromCaravans", source.GetDenarsEarnedFromCaravans().ToString("0.##"), StatItem.StatType.Gold);
			yield return new StatItem("DenarsFromWorkshops", source.GetDenarsEarnedFromWorkshops().ToString("0.##"), StatItem.StatType.Gold);
			yield return new StatItem("DenarsFromRansoms", source.GetDenarsEarnedFromRansoms().ToString("0.##"), StatItem.StatType.Gold);
			yield return new StatItem("DenarsFromTaxes", source.GetDenarsEarnedFromTaxes().ToString("0.##"), StatItem.StatType.Gold);
			GameTexts.SetVariable("LEFT", source.GetDenarsEarnedFromTributes().ToString("0.##"));
			GameTexts.SetVariable("RIGHT", source.GetDenarsPaidAsTributes().ToString("0.##"));
			yield return new StatItem("TributeCollectedTributePaid", GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString(), StatItem.StatType.None);
			yield break;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001302A File Offset: 0x0001122A
		private IEnumerable<StatItem> GetCraftingStats(IStatisticsCampaignBehavior source)
		{
			yield return new StatItem("WeaponsCrafted", source.GetNumberOfWeaponsCrafted().ToString(), StatItem.StatType.None);
			string content = source.GetMostExpensiveItemCrafted().Item1 ?? GameTexts.FindText("str_no_items_crafted", null).ToString();
			GameTexts.SetVariable("LEFT", content);
			GameTexts.SetVariable("RIGHT", source.GetMostExpensiveItemCrafted().Item2.ToString());
			yield return new StatItem("MostExpensiveCraft", GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString(), StatItem.StatType.Gold);
			yield return new StatItem("NumberOfPiecesUnlocked", source.GetNumberOfCraftingPartsUnlocked().ToString(), StatItem.StatType.None);
			yield return new StatItem("CraftingOrdersCompleted", source.GetNumberOfCraftingOrdersCompleted().ToString(), StatItem.StatType.None);
			yield break;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001303A File Offset: 0x0001123A
		private IEnumerable<StatItem> GetCompanionStats(IStatisticsCampaignBehavior source)
		{
			yield return new StatItem("NumberOfHiredCompanions", source.GetNumberOfCompanionsHired().ToString(), StatItem.StatType.None);
			string content = source.GetCompanionWithMostIssuesSolved().Item1 ?? GameTexts.FindText("str_no_companions_with_issues_solved", null).ToString();
			GameTexts.SetVariable("LEFT", content);
			GameTexts.SetVariable("RIGHT", source.GetCompanionWithMostIssuesSolved().Item2);
			yield return new StatItem("MostIssueCompanion", GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString(), StatItem.StatType.Issue);
			string content2 = source.GetCompanionWithMostKills().Item1 ?? GameTexts.FindText("str_no_companions_with_kills", null).ToString();
			GameTexts.SetVariable("LEFT", content2);
			GameTexts.SetVariable("RIGHT", source.GetCompanionWithMostKills().Item2);
			yield return new StatItem("MostKillCompanion", GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString(), StatItem.StatType.Kill);
			yield break;
		}

		// Token: 0x04000233 RID: 563
		private readonly IStatisticsCampaignBehavior _statSource;
	}
}
