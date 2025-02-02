using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Tournament
{
	// Token: 0x0200000A RID: 10
	public class TournamentParticipantVM : ViewModel
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00005672 File Offset: 0x00003872
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000567A File Offset: 0x0000387A
		public TournamentParticipant Participant { get; private set; }

		// Token: 0x06000065 RID: 101 RVA: 0x00005683 File Offset: 0x00003883
		public TournamentParticipantVM()
		{
			this._visual = new ImageIdentifierVM(ImageIdentifierType.Null);
			this._character = new CharacterViewModel(CharacterViewModel.StanceTypes.CelebrateVictory);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000056C0 File Offset: 0x000038C0
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.IsInitialized)
			{
				this.Refresh(this.Participant, this.TeamColor);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000056E4 File Offset: 0x000038E4
		public void Refresh(TournamentParticipant participant, Color teamColor)
		{
			this.Participant = participant;
			this.TeamColor = teamColor;
			this.State = ((participant == null) ? 0 : ((participant.Character == CharacterObject.PlayerCharacter) ? 2 : 1));
			this.IsInitialized = true;
			this._latestParticipant = participant;
			if (participant != null)
			{
				this.Name = participant.Character.Name.ToString();
				CharacterCode characterCode = SandBoxUIHelper.GetCharacterCode(participant.Character, false);
				this.Character = new CharacterViewModel(CharacterViewModel.StanceTypes.CelebrateVictory);
				this.Character.FillFrom(participant.Character, -1);
				this.Visual = new ImageIdentifierVM(characterCode);
				this.IsValid = true;
				this.IsMainHero = participant.Character.IsPlayerCharacter;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005792 File Offset: 0x00003992
		public void ExecuteOpenEncyclopedia()
		{
			TournamentParticipant participant = this.Participant;
			if (((participant != null) ? participant.Character : null) != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Participant.Character.EncyclopediaLink);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000057C8 File Offset: 0x000039C8
		public void Refresh()
		{
			base.OnPropertyChanged("Name");
			base.OnPropertyChanged("Visual");
			base.OnPropertyChanged("Score");
			base.OnPropertyChanged("State");
			base.OnPropertyChanged("TeamColor");
			base.OnPropertyChanged("IsDead");
			TournamentParticipant latestParticipant = this._latestParticipant;
			this.IsMainHero = (latestParticipant != null && latestParticipant.Character.IsPlayerCharacter);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000583C File Offset: 0x00003A3C
		[DataSourceProperty]
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				if (value != this._isInitialized)
				{
					this._isInitialized = value;
					base.OnPropertyChangedWithValue(value, "IsInitialized");
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000585A File Offset: 0x00003A5A
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00005862 File Offset: 0x00003A62
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00005880 File Offset: 0x00003A80
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00005888 File Offset: 0x00003A88
		[DataSourceProperty]
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (value != this._isDead)
				{
					this._isDead = value;
					base.OnPropertyChangedWithValue(value, "IsDead");
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000058A6 File Offset: 0x00003AA6
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000058AE File Offset: 0x00003AAE
		[DataSourceProperty]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000058CC File Offset: 0x00003ACC
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000058D4 File Offset: 0x00003AD4
		[DataSourceProperty]
		public Color TeamColor
		{
			get
			{
				return this._teamColor;
			}
			set
			{
				if (value != this._teamColor)
				{
					this._teamColor = value;
					base.OnPropertyChangedWithValue(value, "TeamColor");
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000058F7 File Offset: 0x00003AF7
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000058FF File Offset: 0x00003AFF
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000591D File Offset: 0x00003B1D
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00005925 File Offset: 0x00003B25
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00005943 File Offset: 0x00003B43
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000594B File Offset: 0x00003B4B
		[DataSourceProperty]
		public bool IsQualifiedForNextRound
		{
			get
			{
				return this._isQualifiedForNextRound;
			}
			set
			{
				if (value != this._isQualifiedForNextRound)
				{
					this._isQualifiedForNextRound = value;
					base.OnPropertyChangedWithValue(value, "IsQualifiedForNextRound");
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00005969 File Offset: 0x00003B69
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00005971 File Offset: 0x00003B71
		[DataSourceProperty]
		public string Score
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
					base.OnPropertyChangedWithValue<string>(value, "Score");
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005994 File Offset: 0x00003B94
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000599C File Offset: 0x00003B9C
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000059BF File Offset: 0x00003BBF
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000059C7 File Offset: 0x00003BC7
		[DataSourceProperty]
		public CharacterViewModel Character
		{
			get
			{
				return this._character;
			}
			set
			{
				if (value != this._character)
				{
					this._character = value;
					base.OnPropertyChangedWithValue<CharacterViewModel>(value, "Character");
				}
			}
		}

		// Token: 0x04000025 RID: 37
		private TournamentParticipant _latestParticipant;

		// Token: 0x04000027 RID: 39
		private bool _isInitialized;

		// Token: 0x04000028 RID: 40
		private bool _isValid;

		// Token: 0x04000029 RID: 41
		private string _name = "";

		// Token: 0x0400002A RID: 42
		private string _score = "-";

		// Token: 0x0400002B RID: 43
		private bool _isQualifiedForNextRound;

		// Token: 0x0400002C RID: 44
		private int _state = -1;

		// Token: 0x0400002D RID: 45
		private ImageIdentifierVM _visual;

		// Token: 0x0400002E RID: 46
		private Color _teamColor;

		// Token: 0x0400002F RID: 47
		private bool _isDead;

		// Token: 0x04000030 RID: 48
		private bool _isMainHero;

		// Token: 0x04000031 RID: 49
		private CharacterViewModel _character;

		// Token: 0x02000051 RID: 81
		public enum TournamentPlayerState
		{
			// Token: 0x0400029C RID: 668
			EmptyPlayer,
			// Token: 0x0400029D RID: 669
			GenericPlayer,
			// Token: 0x0400029E RID: 670
			MainPlayer
		}
	}
}
