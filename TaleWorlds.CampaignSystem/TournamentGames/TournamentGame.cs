﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.TournamentGames
{
	// Token: 0x02000277 RID: 631
	public abstract class TournamentGame
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x0008EFFA File Offset: 0x0008D1FA
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this.Town);
			CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.CreationTime, collectedObjects);
			collectedObjects.Add(this.Prize);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0008F025 File Offset: 0x0008D225
		internal static object AutoGeneratedGetMemberValueTown(object o)
		{
			return ((TournamentGame)o).Town;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0008F032 File Offset: 0x0008D232
		internal static object AutoGeneratedGetMemberValueCreationTime(object o)
		{
			return ((TournamentGame)o).CreationTime;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0008F044 File Offset: 0x0008D244
		internal static object AutoGeneratedGetMemberValueMode(object o)
		{
			return ((TournamentGame)o).Mode;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0008F056 File Offset: 0x0008D256
		internal static object AutoGeneratedGetMemberValuePrize(object o)
		{
			return ((TournamentGame)o).Prize;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0008F063 File Offset: 0x0008D263
		internal static object AutoGeneratedGetMemberValue_lastRecordedLordCountForTournamentPrize(object o)
		{
			return ((TournamentGame)o)._lastRecordedLordCountForTournamentPrize;
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0008F075 File Offset: 0x0008D275
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x0008F07D File Offset: 0x0008D27D
		[SaveableProperty(10)]
		public Town Town { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0008F086 File Offset: 0x0008D286
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x0008F08E File Offset: 0x0008D28E
		[SaveableProperty(20)]
		public CampaignTime CreationTime { get; private set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x0008F097 File Offset: 0x0008D297
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x0008F09F File Offset: 0x0008D29F
		[SaveableProperty(30)]
		public TournamentGame.QualificationMode Mode { get; protected set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0008F0A8 File Offset: 0x0008D2A8
		public virtual int MaxTeamSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0008F0AB File Offset: 0x0008D2AB
		public virtual int MaxTeamNumberPerMatch
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0008F0AE File Offset: 0x0008D2AE
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0008F0B6 File Offset: 0x0008D2B6
		[SaveableProperty(40)]
		public ItemObject Prize { get; private set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0008F0BF File Offset: 0x0008D2BF
		public virtual float TournamentWinRenown
		{
			get
			{
				return (float)Campaign.Current.Models.TournamentModel.GetRenownReward(Hero.MainHero, this.Town);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0008F0E1 File Offset: 0x0008D2E1
		public virtual float TournamentWinInfluence
		{
			get
			{
				return (float)Campaign.Current.Models.TournamentModel.GetInfluenceReward(Hero.MainHero, this.Town);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060021A7 RID: 8615
		public abstract int RemoveTournamentAfterDays { get; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060021A8 RID: 8616
		public abstract int MaximumParticipantCount { get; }

		// Token: 0x060021A9 RID: 8617
		public abstract TextObject GetMenuText();

		// Token: 0x060021AA RID: 8618
		public abstract void OpenMission(Settlement settlement, bool isPlayerParticipating);

		// Token: 0x060021AB RID: 8619
		public abstract MBList<CharacterObject> GetParticipantCharacters(Settlement settlement, bool includePlayer = true);

		// Token: 0x060021AC RID: 8620
		protected abstract ItemObject GetTournamentPrize(bool includePlayer, int lastRecordedLordCountForTournamentPrize);

		// Token: 0x060021AD RID: 8621 RVA: 0x0008F103 File Offset: 0x0008D303
		protected TournamentGame(Town town, ItemObject prize = null)
		{
			this.Town = town;
			this.Prize = (prize ?? this.GetTournamentPrize(false, this._lastRecordedLordCountForTournamentPrize));
			this.CreationTime = CampaignTime.Now;
			this._lastRecordedLordCountForTournamentPrize = 0;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0008F13C File Offset: 0x0008D33C
		public virtual bool CanBeAParticipant(CharacterObject character, bool considerSkills)
		{
			return true;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0008F13F File Offset: 0x0008D33F
		public void PrepareForTournamentGame(bool isPlayerParticipating)
		{
			this.OpenMission(Settlement.CurrentSettlement, isPlayerParticipating);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0008F14D File Offset: 0x0008D34D
		public void UpdateTournamentPrize(bool includePlayer, bool removeCurrentPrize = false)
		{
			if (removeCurrentPrize)
			{
				this.Prize = null;
			}
			this.Prize = this.GetTournamentPrize(includePlayer, this._lastRecordedLordCountForTournamentPrize);
		}

		// Token: 0x04000A67 RID: 2663
		[SaveableField(60)]
		protected int _lastRecordedLordCountForTournamentPrize;

		// Token: 0x02000590 RID: 1424
		public enum QualificationMode
		{
			// Token: 0x04001739 RID: 5945
			IndividualScore,
			// Token: 0x0400173A RID: 5946
			TeamScore
		}
	}
}
