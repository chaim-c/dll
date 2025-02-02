using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.TournamentGames
{
	// Token: 0x0200027D RID: 637
	public class TournamentParticipant
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00091290 File Offset: 0x0008F490
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x00091298 File Offset: 0x0008F498
		public int Score { get; private set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000912A1 File Offset: 0x0008F4A1
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x000912A9 File Offset: 0x0008F4A9
		public CharacterObject Character { get; private set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000912B2 File Offset: 0x0008F4B2
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x000912BA File Offset: 0x0008F4BA
		public UniqueTroopDescriptor Descriptor { get; private set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x000912C3 File Offset: 0x0008F4C3
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x000912CB File Offset: 0x0008F4CB
		public TournamentTeam Team { get; private set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x000912D4 File Offset: 0x0008F4D4
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x000912DC File Offset: 0x0008F4DC
		public Equipment MatchEquipment { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000912E5 File Offset: 0x0008F4E5
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x000912ED File Offset: 0x0008F4ED
		public bool IsAssigned { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000912F6 File Offset: 0x0008F4F6
		public bool IsPlayer
		{
			get
			{
				CharacterObject character = this.Character;
				return character != null && character.IsPlayerCharacter;
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00091309 File Offset: 0x0008F509
		public TournamentParticipant(CharacterObject character, UniqueTroopDescriptor descriptor = default(UniqueTroopDescriptor))
		{
			this.Character = character;
			this.Descriptor = (descriptor.IsValid ? descriptor : new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed));
			this.Team = null;
			this.IsAssigned = false;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00091347 File Offset: 0x0008F547
		public void SetTeam(TournamentTeam team)
		{
			this.Team = team;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00091350 File Offset: 0x0008F550
		public int AddScore(int score)
		{
			this.Score += score;
			return this.Score;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00091366 File Offset: 0x0008F566
		public void ResetScore()
		{
			this.Score = 0;
		}
	}
}
