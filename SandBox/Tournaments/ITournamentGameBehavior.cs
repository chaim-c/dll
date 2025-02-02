using System;
using TaleWorlds.CampaignSystem.TournamentGames;

namespace SandBox.Tournaments
{
	// Token: 0x0200002B RID: 43
	public interface ITournamentGameBehavior
	{
		// Token: 0x0600011F RID: 287
		void StartMatch(TournamentMatch match, bool isLastRound);

		// Token: 0x06000120 RID: 288
		void SkipMatch(TournamentMatch match);

		// Token: 0x06000121 RID: 289
		bool IsMatchEnded();

		// Token: 0x06000122 RID: 290
		void OnMatchEnded();
	}
}
