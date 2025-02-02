using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Tournament
{
	// Token: 0x0200000C RID: 12
	public class TournamentTeamVM : ViewModel
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005D59 File Offset: 0x00003F59
		public List<TournamentParticipantVM> Participants { get; }

		// Token: 0x0600009F RID: 159 RVA: 0x00005D64 File Offset: 0x00003F64
		public TournamentTeamVM()
		{
			this.Participant1 = new TournamentParticipantVM();
			this.Participant2 = new TournamentParticipantVM();
			this.Participant3 = new TournamentParticipantVM();
			this.Participant4 = new TournamentParticipantVM();
			this.Participant5 = new TournamentParticipantVM();
			this.Participant6 = new TournamentParticipantVM();
			this.Participant7 = new TournamentParticipantVM();
			this.Participant8 = new TournamentParticipantVM();
			this.Participants = new List<TournamentParticipantVM>
			{
				this.Participant1,
				this.Participant2,
				this.Participant3,
				this.Participant4,
				this.Participant5,
				this.Participant6,
				this.Participant7,
				this.Participant8
			};
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005E41 File Offset: 0x00004041
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Participants.ForEach(delegate(TournamentParticipantVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005E73 File Offset: 0x00004073
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00005E7B File Offset: 0x0000407B
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005E99 File Offset: 0x00004099
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005EA1 File Offset: 0x000040A1
		[DataSourceProperty]
		public int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				if (value != this._score)
				{
					this._score = value;
					base.OnPropertyChangedWithValue(value, "Score");
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005EBF File Offset: 0x000040BF
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005EC7 File Offset: 0x000040C7
		[DataSourceProperty]
		public TournamentParticipantVM Participant1
		{
			get
			{
				return this._participant1;
			}
			set
			{
				if (value != this._participant1)
				{
					this._participant1 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant1");
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005EE5 File Offset: 0x000040E5
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005EED File Offset: 0x000040ED
		[DataSourceProperty]
		public TournamentParticipantVM Participant2
		{
			get
			{
				return this._participant2;
			}
			set
			{
				if (value != this._participant2)
				{
					this._participant2 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant2");
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005F0B File Offset: 0x0000410B
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00005F13 File Offset: 0x00004113
		[DataSourceProperty]
		public TournamentParticipantVM Participant3
		{
			get
			{
				return this._participant3;
			}
			set
			{
				if (value != this._participant3)
				{
					this._participant3 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant3");
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005F31 File Offset: 0x00004131
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005F39 File Offset: 0x00004139
		[DataSourceProperty]
		public TournamentParticipantVM Participant4
		{
			get
			{
				return this._participant4;
			}
			set
			{
				if (value != this._participant4)
				{
					this._participant4 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant4");
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005F57 File Offset: 0x00004157
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005F5F File Offset: 0x0000415F
		[DataSourceProperty]
		public TournamentParticipantVM Participant5
		{
			get
			{
				return this._participant5;
			}
			set
			{
				if (value != this._participant5)
				{
					this._participant5 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant5");
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005F7D File Offset: 0x0000417D
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00005F85 File Offset: 0x00004185
		[DataSourceProperty]
		public TournamentParticipantVM Participant6
		{
			get
			{
				return this._participant6;
			}
			set
			{
				if (value != this._participant6)
				{
					this._participant6 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant6");
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005FA3 File Offset: 0x000041A3
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005FAB File Offset: 0x000041AB
		[DataSourceProperty]
		public TournamentParticipantVM Participant7
		{
			get
			{
				return this._participant7;
			}
			set
			{
				if (value != this._participant7)
				{
					this._participant7 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant7");
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005FC9 File Offset: 0x000041C9
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005FD1 File Offset: 0x000041D1
		[DataSourceProperty]
		public TournamentParticipantVM Participant8
		{
			get
			{
				return this._participant8;
			}
			set
			{
				if (value != this._participant8)
				{
					this._participant8 = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "Participant8");
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005FEF File Offset: 0x000041EF
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00005FF7 File Offset: 0x000041F7
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

		// Token: 0x060000B7 RID: 183 RVA: 0x00006018 File Offset: 0x00004218
		public void Initialize()
		{
			this.IsValid = (this._team != null);
			for (int i = 0; i < this.Count; i++)
			{
				TournamentParticipant participant = this._team.Participants.ElementAtOrDefault(i);
				this.Participants[i].Refresh(participant, Color.FromUint(this._team.TeamColor));
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006079 File Offset: 0x00004279
		public void Initialize(TournamentTeam team)
		{
			this._team = team;
			this.Count = team.TeamSize;
			this.IsValid = (this._team != null);
			this.Initialize();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000060A4 File Offset: 0x000042A4
		public void Refresh()
		{
			this.IsValid = (this._team != null);
			base.OnPropertyChanged("Count");
			int num = 0;
			foreach (TournamentParticipantVM tournamentParticipantVM in from p in this.Participants
			where p.IsValid
			select p)
			{
				base.OnPropertyChanged("Participant" + num);
				tournamentParticipantVM.Refresh();
				num++;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006148 File Offset: 0x00004348
		public IEnumerable<TournamentParticipantVM> GetParticipants()
		{
			if (this.Participant1.IsValid)
			{
				yield return this.Participant1;
			}
			if (this.Participant2.IsValid)
			{
				yield return this.Participant2;
			}
			if (this.Participant3.IsValid)
			{
				yield return this.Participant3;
			}
			if (this.Participant4.IsValid)
			{
				yield return this.Participant4;
			}
			if (this.Participant5.IsValid)
			{
				yield return this.Participant5;
			}
			if (this.Participant6.IsValid)
			{
				yield return this.Participant6;
			}
			if (this.Participant7.IsValid)
			{
				yield return this.Participant7;
			}
			if (this.Participant8.IsValid)
			{
				yield return this.Participant8;
			}
			yield break;
		}

		// Token: 0x0400003F RID: 63
		private TournamentTeam _team;

		// Token: 0x04000041 RID: 65
		private int _count = -1;

		// Token: 0x04000042 RID: 66
		private TournamentParticipantVM _participant1;

		// Token: 0x04000043 RID: 67
		private TournamentParticipantVM _participant2;

		// Token: 0x04000044 RID: 68
		private TournamentParticipantVM _participant3;

		// Token: 0x04000045 RID: 69
		private TournamentParticipantVM _participant4;

		// Token: 0x04000046 RID: 70
		private TournamentParticipantVM _participant5;

		// Token: 0x04000047 RID: 71
		private TournamentParticipantVM _participant6;

		// Token: 0x04000048 RID: 72
		private TournamentParticipantVM _participant7;

		// Token: 0x04000049 RID: 73
		private TournamentParticipantVM _participant8;

		// Token: 0x0400004A RID: 74
		private int _score;

		// Token: 0x0400004B RID: 75
		private bool _isValid;
	}
}
