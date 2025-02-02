using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Tournament
{
	// Token: 0x02000009 RID: 9
	public class TournamentMatchVM : ViewModel
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00005169 File Offset: 0x00003369
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00005171 File Offset: 0x00003371
		public TournamentMatch Match { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000517A File Offset: 0x0000337A
		public List<TournamentTeamVM> Teams { get; }

		// Token: 0x0600004D RID: 77 RVA: 0x00005184 File Offset: 0x00003384
		public TournamentMatchVM()
		{
			this.Team1 = new TournamentTeamVM();
			this.Team2 = new TournamentTeamVM();
			this.Team3 = new TournamentTeamVM();
			this.Team4 = new TournamentTeamVM();
			this.Teams = new List<TournamentTeamVM>
			{
				this.Team1,
				this.Team2,
				this.Team3,
				this.Team4
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000520C File Offset: 0x0000340C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Teams.ForEach(delegate(TournamentTeamVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005240 File Offset: 0x00003440
		public void Initialize()
		{
			foreach (TournamentTeamVM tournamentTeamVM in this.Teams)
			{
				if (tournamentTeamVM.IsValid)
				{
					tournamentTeamVM.Initialize();
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000529C File Offset: 0x0000349C
		public void Initialize(TournamentMatch match)
		{
			int num = 0;
			this.Match = match;
			this.IsValid = (this.Match != null);
			this.Count = match.Teams.Count<TournamentTeam>();
			foreach (TournamentTeam team in match.Teams)
			{
				this.Teams[num].Initialize(team);
				num++;
			}
			this.State = 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005328 File Offset: 0x00003528
		public void Refresh(bool forceRefresh)
		{
			if (forceRefresh)
			{
				base.OnPropertyChanged("Count");
			}
			for (int i = 0; i < this.Count; i++)
			{
				TournamentTeamVM tournamentTeamVM = this.Teams[i];
				if (forceRefresh)
				{
					base.OnPropertyChanged("Team" + i + 1);
				}
				tournamentTeamVM.Refresh();
				for (int j = 0; j < tournamentTeamVM.Count; j++)
				{
					TournamentParticipantVM tournamentParticipantVM = tournamentTeamVM.Participants[j];
					tournamentParticipantVM.Score = tournamentParticipantVM.Participant.Score.ToString();
					tournamentParticipantVM.IsQualifiedForNextRound = this.Match.Winners.Contains(tournamentParticipantVM.Participant);
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000053E0 File Offset: 0x000035E0
		public void RefreshActiveMatch()
		{
			for (int i = 0; i < this.Count; i++)
			{
				TournamentTeamVM tournamentTeamVM = this.Teams[i];
				for (int j = 0; j < tournamentTeamVM.Count; j++)
				{
					TournamentParticipantVM tournamentParticipantVM = tournamentTeamVM.Participants[j];
					tournamentParticipantVM.Score = tournamentParticipantVM.Participant.Score.ToString();
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005440 File Offset: 0x00003640
		public void Refresh(TournamentMatchVM target)
		{
			base.OnPropertyChanged("Count");
			int num = 0;
			foreach (TournamentTeamVM tournamentTeamVM in from t in this.Teams
			where t.IsValid
			select t)
			{
				base.OnPropertyChanged("Team" + num + 1);
				tournamentTeamVM.Refresh();
				num++;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000054DC File Offset: 0x000036DC
		public IEnumerable<TournamentParticipantVM> GetParticipants()
		{
			List<TournamentParticipantVM> list = new List<TournamentParticipantVM>();
			if (this.Team1.IsValid)
			{
				list.AddRange(this.Team1.GetParticipants());
			}
			if (this.Team2.IsValid)
			{
				list.AddRange(this.Team2.GetParticipants());
			}
			if (this.Team3.IsValid)
			{
				list.AddRange(this.Team3.GetParticipants());
			}
			if (this.Team4.IsValid)
			{
				list.AddRange(this.Team4.GetParticipants());
			}
			return list;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00005568 File Offset: 0x00003768
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00005570 File Offset: 0x00003770
		[DataSourceProperty]
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				if (value != this._isValid)
				{
					this._isValid = value;
					base.OnPropertyChangedWithValue(value, "IsValid");
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000558E File Offset: 0x0000378E
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00005596 File Offset: 0x00003796
		[DataSourceProperty]
		public int State
		{
			get
			{
				return this._state;
			}
			set
			{
				if (value != this._state)
				{
					this._state = value;
					base.OnPropertyChangedWithValue(value, "State");
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000055B4 File Offset: 0x000037B4
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000055BC File Offset: 0x000037BC
		[DataSourceProperty]
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				if (value != this._count)
				{
					this._count = value;
					base.OnPropertyChangedWithValue(value, "Count");
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000055DA File Offset: 0x000037DA
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000055E2 File Offset: 0x000037E2
		[DataSourceProperty]
		public TournamentTeamVM Team1
		{
			get
			{
				return this._team1;
			}
			set
			{
				if (value != this._team1)
				{
					this._team1 = value;
					base.OnPropertyChangedWithValue<TournamentTeamVM>(value, "Team1");
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00005600 File Offset: 0x00003800
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00005608 File Offset: 0x00003808
		[DataSourceProperty]
		public TournamentTeamVM Team2
		{
			get
			{
				return this._team2;
			}
			set
			{
				if (value != this._team2)
				{
					this._team2 = value;
					base.OnPropertyChangedWithValue<TournamentTeamVM>(value, "Team2");
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00005626 File Offset: 0x00003826
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000562E File Offset: 0x0000382E
		[DataSourceProperty]
		public TournamentTeamVM Team3
		{
			get
			{
				return this._team3;
			}
			set
			{
				if (value != this._team3)
				{
					this._team3 = value;
					base.OnPropertyChangedWithValue<TournamentTeamVM>(value, "Team3");
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000564C File Offset: 0x0000384C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00005654 File Offset: 0x00003854
		[DataSourceProperty]
		public TournamentTeamVM Team4
		{
			get
			{
				return this._team4;
			}
			set
			{
				if (value != this._team4)
				{
					this._team4 = value;
					base.OnPropertyChangedWithValue<TournamentTeamVM>(value, "Team4");
				}
			}
		}

		// Token: 0x0400001E RID: 30
		private TournamentTeamVM _team1;

		// Token: 0x0400001F RID: 31
		private TournamentTeamVM _team2;

		// Token: 0x04000020 RID: 32
		private TournamentTeamVM _team3;

		// Token: 0x04000021 RID: 33
		private TournamentTeamVM _team4;

		// Token: 0x04000022 RID: 34
		private int _count = -1;

		// Token: 0x04000023 RID: 35
		private int _state = -1;

		// Token: 0x04000024 RID: 36
		private bool _isValid;

		// Token: 0x0200004F RID: 79
		public enum TournamentMatchState
		{
			// Token: 0x04000294 RID: 660
			Unfinished,
			// Token: 0x04000295 RID: 661
			Current,
			// Token: 0x04000296 RID: 662
			Over,
			// Token: 0x04000297 RID: 663
			Active
		}
	}
}
