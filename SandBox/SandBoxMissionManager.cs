using System;
using System.Collections.Generic;
using SandBox.Tournaments;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;

namespace SandBox
{
	// Token: 0x02000027 RID: 39
	public class SandBoxMissionManager : ISandBoxMissionManager
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000073AC File Offset: 0x000055AC
		public IMission OpenTournamentFightMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return TournamentMissionStarter.OpenTournamentFightMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000073BA File Offset: 0x000055BA
		public IMission OpenTournamentHorseRaceMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return TournamentMissionStarter.OpenTournamentHorseRaceMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000073C8 File Offset: 0x000055C8
		public IMission OpenTournamentJoustingMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return TournamentMissionStarter.OpenTournamentJoustingMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000073D6 File Offset: 0x000055D6
		public IMission OpenTournamentArcheryMission(string scene, TournamentGame tournamentGame, Settlement settlement, CultureObject culture, bool isPlayerParticipating)
		{
			return TournamentMissionStarter.OpenTournamentArcheryMission(scene, tournamentGame, settlement, culture, isPlayerParticipating);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000073E4 File Offset: 0x000055E4
		IMission ISandBoxMissionManager.OpenBattleChallengeMission(string scene, IList<Hero> priorityCharsAttacker, IList<Hero> priorityCharsDefender)
		{
			return TournamentMissionStarter.OpenBattleChallengeMission(scene, priorityCharsAttacker, priorityCharsDefender);
		}
	}
}
