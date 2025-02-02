using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200009A RID: 154
	public class SandBoxMission
	{
		// Token: 0x06001167 RID: 4455 RVA: 0x0004EF3E File Offset: 0x0004D13E
		public static IMission OpenTournamentArcheryMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return SandBoxManager.Instance.SandBoxMissionManager.OpenTournamentArcheryMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0004EF55 File Offset: 0x0004D155
		public static IMission OpenTournamentFightMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return SandBoxManager.Instance.SandBoxMissionManager.OpenTournamentFightMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0004EF6C File Offset: 0x0004D16C
		public static IMission OpenTournamentHorseRaceMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return SandBoxManager.Instance.SandBoxMissionManager.OpenTournamentHorseRaceMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0004EF83 File Offset: 0x0004D183
		public static IMission OpenTournamentJoustingMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return SandBoxManager.Instance.SandBoxMissionManager.OpenTournamentJoustingMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0004EF9A File Offset: 0x0004D19A
		public static IMission OpenBattleChallengeMission(string scene, IList<Hero> priorityCharsAttacker, IList<Hero> priorityCharsDefender)
		{
			return SandBoxManager.Instance.SandBoxMissionManager.OpenBattleChallengeMission(scene, priorityCharsAttacker, priorityCharsDefender);
		}
	}
}
