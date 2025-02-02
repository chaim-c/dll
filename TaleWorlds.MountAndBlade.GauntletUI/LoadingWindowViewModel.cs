using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000015 RID: 21
	public class LoadingWindowViewModel : ViewModel
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000650A File Offset: 0x0000470A
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00006512 File Offset: 0x00004712
		public bool CurrentlyShowingMultiplayer { get; private set; }

		// Token: 0x0600009E RID: 158 RVA: 0x0000651B File Offset: 0x0000471B
		public LoadingWindowViewModel(Action<bool, int> handleSPPartialLoading)
		{
			this._handleSPPartialLoading = handleSPPartialLoading;
			Action<bool, int> handleSPPartialLoading2 = this._handleSPPartialLoading;
			if (handleSPPartialLoading2 == null)
			{
				return;
			}
			handleSPPartialLoading2(true, this._currentImage + 1);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006544 File Offset: 0x00004744
		internal void Update()
		{
			if (this.Enabled)
			{
				bool flag = this.IsEligableForMultiplayerLoading();
				if (flag && !this.CurrentlyShowingMultiplayer)
				{
					this.SetForMultiplayer();
					return;
				}
				if (!flag && this.CurrentlyShowingMultiplayer)
				{
					this.SetForEmpty();
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006583 File Offset: 0x00004783
		private void HandleEnable()
		{
			if (this.IsEligableForMultiplayerLoading())
			{
				this.SetForMultiplayer();
				return;
			}
			this.SetForEmpty();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000659A File Offset: 0x0000479A
		private bool IsEligableForMultiplayerLoading()
		{
			return this._isMultiplayer && Mission.Current != null && Game.Current.GameStateManager.ActiveState is MissionState;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000065C4 File Offset: 0x000047C4
		private void SetForMultiplayer()
		{
			MissionState missionState = (MissionState)Game.Current.GameStateManager.ActiveState;
			string missionName = missionState.MissionName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(missionName);
			string text;
			if (num <= 1531392713U)
			{
				if (num != 731629611U)
				{
					if (num != 1038375761U)
					{
						if (num == 1531392713U)
						{
							if (missionName == "MultiplayerBattle")
							{
								text = "Battle";
								goto IL_12D;
							}
						}
					}
					else if (missionName == "MultiplayerDuel")
					{
						text = "Duel";
						goto IL_12D;
					}
				}
				else if (missionName == "MultiplayerSkirmish")
				{
					text = "Skirmish";
					goto IL_12D;
				}
			}
			else if (num <= 2065855055U)
			{
				if (num != 1705544657U)
				{
					if (num == 2065855055U)
					{
						if (!(missionName == "MultiplayerFreeForAll"))
						{
						}
					}
				}
				else if (missionName == "MultiplayerTeamDeathmatch")
				{
					text = "TeamDeathmatch";
					goto IL_12D;
				}
			}
			else if (num != 2440237701U)
			{
				if (num == 3434966542U)
				{
					if (missionName == "MultiplayerSiege")
					{
						text = "Siege";
						goto IL_12D;
					}
				}
			}
			else if (missionName == "MultiplayerCaptain")
			{
				text = "Captain";
				goto IL_12D;
			}
			text = missionState.MissionName;
			IL_12D:
			if (!string.IsNullOrEmpty(text))
			{
				this.DescriptionText = GameTexts.FindText("str_multiplayer_official_game_type_explainer", text).ToString();
			}
			else
			{
				this.DescriptionText = "";
			}
			this.GameModeText = GameTexts.FindText("str_multiplayer_official_game_type_name", text).ToString();
			TextObject textObject;
			if (GameTexts.TryGetText("str_multiplayer_scene_name", out textObject, missionState.CurrentMission.SceneName))
			{
				this.TitleText = textObject.ToString();
			}
			else
			{
				this.TitleText = missionState.CurrentMission.SceneName;
			}
			this.LoadingImageName = missionState.CurrentMission.SceneName;
			this.CurrentlyShowingMultiplayer = true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000678F File Offset: 0x0000498F
		private void SetForEmpty()
		{
			this.DescriptionText = "";
			this.TitleText = "";
			this.GameModeText = "";
			this.SetNextGenericImage();
			this.CurrentlyShowingMultiplayer = false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000067C0 File Offset: 0x000049C0
		private void SetNextGenericImage()
		{
			int arg = (this._currentImage >= 1) ? this._currentImage : this._totalGenericImageCount;
			this._currentImage = ((this._currentImage < this._totalGenericImageCount) ? (this._currentImage + 1) : 1);
			int arg2 = (this._currentImage < this._totalGenericImageCount) ? (this._currentImage + 1) : 1;
			Action<bool, int> handleSPPartialLoading = this._handleSPPartialLoading;
			if (handleSPPartialLoading != null)
			{
				handleSPPartialLoading(false, arg);
			}
			Action<bool, int> handleSPPartialLoading2 = this._handleSPPartialLoading;
			if (handleSPPartialLoading2 != null)
			{
				handleSPPartialLoading2(true, arg2);
			}
			this.IsDevelopmentMode = NativeConfig.IsDevelopmentMode;
			this.LoadingImageName = "loading_" + this._currentImage.ToString("00");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000686F File Offset: 0x00004A6F
		public void SetTotalGenericImageCount(int totalGenericImageCount)
		{
			this._totalGenericImageCount = totalGenericImageCount;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006878 File Offset: 0x00004A78
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00006880 File Offset: 0x00004A80
		[DataSourceProperty]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					base.OnPropertyChangedWithValue(value, "Enabled");
					if (value)
					{
						this.HandleEnable();
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000068A7 File Offset: 0x00004AA7
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000068AF File Offset: 0x00004AAF
		[DataSourceProperty]
		public bool IsDevelopmentMode
		{
			get
			{
				return this._isDevelopmentMode;
			}
			set
			{
				if (this._isDevelopmentMode != value)
				{
					this._isDevelopmentMode = value;
					base.OnPropertyChangedWithValue(value, "IsDevelopmentMode");
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000068CD File Offset: 0x00004ACD
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000068D5 File Offset: 0x00004AD5
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (this._titleText != value)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000068F8 File Offset: 0x00004AF8
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00006900 File Offset: 0x00004B00
		[DataSourceProperty]
		public string GameModeText
		{
			get
			{
				return this._gameModeText;
			}
			set
			{
				if (this._gameModeText != value)
				{
					this._gameModeText = value;
					base.OnPropertyChangedWithValue<string>(value, "GameModeText");
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00006923 File Offset: 0x00004B23
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000692B File Offset: 0x00004B2B
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (this._descriptionText != value)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000694E File Offset: 0x00004B4E
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00006956 File Offset: 0x00004B56
		[DataSourceProperty]
		public bool IsMultiplayer
		{
			get
			{
				return this._isMultiplayer;
			}
			set
			{
				if (this._isMultiplayer != value)
				{
					this._isMultiplayer = value;
					base.OnPropertyChangedWithValue(value, "IsMultiplayer");
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006974 File Offset: 0x00004B74
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000697C File Offset: 0x00004B7C
		[DataSourceProperty]
		public string LoadingImageName
		{
			get
			{
				return this._loadingImageName;
			}
			set
			{
				if (this._loadingImageName != value)
				{
					this._loadingImageName = value;
					base.OnPropertyChangedWithValue<string>(value, "LoadingImageName");
				}
			}
		}

		// Token: 0x04000079 RID: 121
		private int _currentImage;

		// Token: 0x0400007A RID: 122
		private int _totalGenericImageCount;

		// Token: 0x0400007B RID: 123
		private Action<bool, int> _handleSPPartialLoading;

		// Token: 0x0400007D RID: 125
		private bool _enabled;

		// Token: 0x0400007E RID: 126
		private bool _isDevelopmentMode;

		// Token: 0x0400007F RID: 127
		private bool _isMultiplayer;

		// Token: 0x04000080 RID: 128
		private string _loadingImageName;

		// Token: 0x04000081 RID: 129
		private string _titleText;

		// Token: 0x04000082 RID: 130
		private string _descriptionText;

		// Token: 0x04000083 RID: 131
		private string _gameModeText;
	}
}
