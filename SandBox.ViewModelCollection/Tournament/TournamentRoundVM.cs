using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Tournament
{
	// Token: 0x0200000B RID: 11
	public class TournamentRoundVM : ViewModel
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000059E5 File Offset: 0x00003BE5
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000059ED File Offset: 0x00003BED
		public TournamentRound Round { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000059F6 File Offset: 0x00003BF6
		public List<TournamentMatchVM> Matches { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x00005A00 File Offset: 0x00003C00
		public TournamentRoundVM()
		{
			this.Match1 = new TournamentMatchVM();
			this.Match2 = new TournamentMatchVM();
			this.Match3 = new TournamentMatchVM();
			this.Match4 = new TournamentMatchVM();
			this.Match5 = new TournamentMatchVM();
			this.Match6 = new TournamentMatchVM();
			this.Match7 = new TournamentMatchVM();
			this.Match8 = new TournamentMatchVM();
			this.Matches = new List<TournamentMatchVM>
			{
				this.Match1,
				this.Match2,
				this.Match3,
				this.Match4,
				this.Match5,
				this.Match6,
				this.Match7,
				this.Match8
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005ADD File Offset: 0x00003CDD
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Matches.ForEach(delegate(TournamentMatchVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005B0F File Offset: 0x00003D0F
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00005B17 File Offset: 0x00003D17
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00005B35 File Offset: 0x00003D35
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00005B3D File Offset: 0x00003D3D
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005B60 File Offset: 0x00003D60
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00005B68 File Offset: 0x00003D68
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00005B86 File Offset: 0x00003D86
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00005B8E File Offset: 0x00003D8E
		[DataSourceProperty]
		public TournamentMatchVM Match1
		{
			get
			{
				return this._match1;
			}
			set
			{
				if (value != this._match1)
				{
					this._match1 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match1");
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00005BAC File Offset: 0x00003DAC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00005BB4 File Offset: 0x00003DB4
		[DataSourceProperty]
		public TournamentMatchVM Match2
		{
			get
			{
				return this._match2;
			}
			set
			{
				if (value != this._match2)
				{
					this._match2 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match2");
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005BD2 File Offset: 0x00003DD2
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00005BDA File Offset: 0x00003DDA
		[DataSourceProperty]
		public TournamentMatchVM Match3
		{
			get
			{
				return this._match3;
			}
			set
			{
				if (value != this._match3)
				{
					this._match3 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match3");
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005BF8 File Offset: 0x00003DF8
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00005C00 File Offset: 0x00003E00
		[DataSourceProperty]
		public TournamentMatchVM Match4
		{
			get
			{
				return this._match4;
			}
			set
			{
				if (value != this._match4)
				{
					this._match4 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match4");
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005C1E File Offset: 0x00003E1E
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00005C26 File Offset: 0x00003E26
		[DataSourceProperty]
		public TournamentMatchVM Match5
		{
			get
			{
				return this._match5;
			}
			set
			{
				if (value != this._match5)
				{
					this._match5 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match5");
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005C44 File Offset: 0x00003E44
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005C4C File Offset: 0x00003E4C
		[DataSourceProperty]
		public TournamentMatchVM Match6
		{
			get
			{
				return this._match6;
			}
			set
			{
				if (value != this._match6)
				{
					this._match6 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match6");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005C6A File Offset: 0x00003E6A
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005C72 File Offset: 0x00003E72
		[DataSourceProperty]
		public TournamentMatchVM Match7
		{
			get
			{
				return this._match7;
			}
			set
			{
				if (value != this._match7)
				{
					this._match7 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match7");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005C90 File Offset: 0x00003E90
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00005C98 File Offset: 0x00003E98
		[DataSourceProperty]
		public TournamentMatchVM Match8
		{
			get
			{
				return this._match8;
			}
			set
			{
				if (value != this._match8)
				{
					this._match8 = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "Match8");
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public void Initialize()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.Matches[i].Initialize();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public void Initialize(TournamentRound round, TextObject name)
		{
			this.IsValid = true;
			this.Round = round;
			this.Count = round.Matches.Length;
			for (int i = 0; i < round.Matches.Length; i++)
			{
				this.Matches[i].Initialize(round.Matches[i]);
			}
			this.Name = name.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005D49 File Offset: 0x00003F49
		public IEnumerable<TournamentParticipantVM> GetParticipants()
		{
			foreach (TournamentMatchVM tournamentMatchVM in this.Matches)
			{
				if (tournamentMatchVM.IsValid)
				{
					foreach (TournamentParticipantVM tournamentParticipantVM in tournamentMatchVM.GetParticipants())
					{
						yield return tournamentParticipantVM;
					}
					IEnumerator<TournamentParticipantVM> enumerator2 = null;
				}
			}
			List<TournamentMatchVM>.Enumerator enumerator = default(List<TournamentMatchVM>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000034 RID: 52
		private TournamentMatchVM _match1;

		// Token: 0x04000035 RID: 53
		private TournamentMatchVM _match2;

		// Token: 0x04000036 RID: 54
		private TournamentMatchVM _match3;

		// Token: 0x04000037 RID: 55
		private TournamentMatchVM _match4;

		// Token: 0x04000038 RID: 56
		private TournamentMatchVM _match5;

		// Token: 0x04000039 RID: 57
		private TournamentMatchVM _match6;

		// Token: 0x0400003A RID: 58
		private TournamentMatchVM _match7;

		// Token: 0x0400003B RID: 59
		private TournamentMatchVM _match8;

		// Token: 0x0400003C RID: 60
		private int _count = -1;

		// Token: 0x0400003D RID: 61
		private string _name;

		// Token: 0x0400003E RID: 62
		private bool _isValid;
	}
}
