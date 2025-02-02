using System;

namespace TaleWorlds.CampaignSystem.TournamentGames
{
	// Token: 0x0200027E RID: 638
	public class TournamentRound
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x0009136F File Offset: 0x0008F56F
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x00091377 File Offset: 0x0008F577
		public TournamentMatch[] Matches { get; private set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00091380 File Offset: 0x0008F580
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x00091388 File Offset: 0x0008F588
		public int CurrentMatchIndex { get; private set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00091391 File Offset: 0x0008F591
		public TournamentMatch CurrentMatch
		{
			get
			{
				if (this.CurrentMatchIndex >= this.Matches.Length)
				{
					return null;
				}
				return this.Matches[this.CurrentMatchIndex];
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000913B4 File Offset: 0x0008F5B4
		public TournamentRound(int participantCount, int numberOfMatches, int numberOfTeamsPerMatch, int numberOfWinnerParticipants, TournamentGame.QualificationMode qualificationMode)
		{
			this.Matches = new TournamentMatch[numberOfMatches];
			this.CurrentMatchIndex = 0;
			int participantCount2 = participantCount / numberOfMatches;
			for (int i = 0; i < numberOfMatches; i++)
			{
				this.Matches[i] = new TournamentMatch(participantCount2, numberOfTeamsPerMatch, numberOfWinnerParticipants / numberOfMatches, qualificationMode);
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00091400 File Offset: 0x0008F600
		public void OnMatchEnded()
		{
			int currentMatchIndex = this.CurrentMatchIndex;
			this.CurrentMatchIndex = currentMatchIndex + 1;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00091420 File Offset: 0x0008F620
		public void EndMatch()
		{
			this.CurrentMatch.End();
			int currentMatchIndex = this.CurrentMatchIndex;
			this.CurrentMatchIndex = currentMatchIndex + 1;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00091448 File Offset: 0x0008F648
		public void AddParticipant(TournamentParticipant participant, bool firstTime = false)
		{
			foreach (TournamentMatch tournamentMatch in this.Matches)
			{
				if (tournamentMatch.IsParticipantRequired())
				{
					tournamentMatch.AddParticipant(participant, firstTime);
					return;
				}
			}
		}
	}
}
