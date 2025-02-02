using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;
using TaleWorlds.ScreenSystem;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x02000012 RID: 18
	public class SavedGameVM : ViewModel
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007B23 File Offset: 0x00005D23
		public SaveGameFileInfo Save { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007B2B File Offset: 0x00005D2B
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00007B33 File Offset: 0x00005D33
		public bool RequiresInquiryOnLoad { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007B3C File Offset: 0x00005D3C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00007B44 File Offset: 0x00005D44
		public bool IsModuleDiscrepancyDetected { get; private set; }

		// Token: 0x06000152 RID: 338 RVA: 0x00007B50 File Offset: 0x00005D50
		public SavedGameVM(SaveGameFileInfo save, bool isSaving, Action<SavedGameVM> onDelete, Action<SavedGameVM> onSelection, Action onCancelLoadSave, Action onDone, bool isCorruptedSave = false, bool isDiscrepancyDetectedForSave = false, bool isIronman = false)
		{
			this.Save = save;
			this._isSaving = isSaving;
			this._onDelete = onDelete;
			this._onSelection = onSelection;
			this._onCancelLoadSave = onCancelLoadSave;
			this._onDone = onDone;
			this.IsCorrupted = isCorruptedSave;
			this.SavedGameProperties = new MBBindingList<SavedGamePropertyVM>();
			this.LoadedModulesInSave = new MBBindingList<SavedGameModuleInfoVM>();
			if (isIronman)
			{
				GameTexts.SetVariable("RANK", this.Save.MetaData.GetCharacterName());
				GameTexts.SetVariable("NUMBER", new TextObject("{=Fm0rjkH7}Ironman", null));
				this.NameText = new TextObject("{=AVoWvlue}{RANK} ({NUMBER})", null).ToString();
			}
			else
			{
				this.NameText = this.Save.Name;
			}
			this._newlineTextObject.SetTextVariable("newline", "\n");
			this._gameVersion = MBSaveLoad.CurrentVersion;
			this._saveVersion = this.Save.MetaData.GetApplicationVersion();
			this.IsModuleDiscrepancyDetected = (isCorruptedSave || isDiscrepancyDetectedForSave);
			this.MainHeroVisualCode = (this.IsModuleDiscrepancyDetected ? string.Empty : this.Save.MetaData.GetCharacterVisualCode());
			this.BannerTextCode = (this.IsModuleDiscrepancyDetected ? string.Empty : this.Save.MetaData.GetClanBannerCode());
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.LoadedModulesInSave.Clear();
			this.SavedGameProperties.Clear();
			this.SaveVersionAsString = this._saveVersion.ToString();
			if (this._gameVersion != this._saveVersion)
			{
				this.RequiresInquiryOnLoad = true;
			}
			foreach (string text in this.Save.MetaData.GetModules())
			{
				string value = this.Save.MetaData.GetModuleVersion(text).ToString();
				this.LoadedModulesInSave.Add(new SavedGameModuleInfoVM(text, "", value));
			}
			this.CharacterNameText = this.Save.MetaData.GetCharacterName();
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.Save.MetaData.GetClanBannerCode()), true);
			this.DeleteText = new TextObject("{=deleteaction}Delete", null).ToString();
			this.ModulesText = new TextObject("{=JXyxj1J5}Modules", null).ToString();
			DateTime creationTime = this.Save.MetaData.GetCreationTime();
			this.RealTimeText1 = LocalizedTextManager.GetDateFormattedByLanguage(BannerlordConfig.Language, creationTime);
			this.RealTimeText2 = LocalizedTextManager.GetTimeFormattedByLanguage(BannerlordConfig.Language, creationTime);
			int playerHealthPercentage = this.Save.MetaData.GetPlayerHealthPercentage();
			TextObject textObject = new TextObject("{=gYATKZJp}{NUMBER}%", null);
			textObject.SetTextVariable("NUMBER", playerHealthPercentage.ToString());
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.Health, textObject, new TextObject("{=hZrwUIaq}Health", null)));
			int mainHeroGold = this.Save.MetaData.GetMainHeroGold();
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.Gold, SavedGameVM.GetAbbreviatedValueTextFromValue(mainHeroGold), new TextObject("{=Hxf6bzmR}Current Denars", null)));
			int valueAmount = (int)this.Save.MetaData.GetClanInfluence();
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.Influence, SavedGameVM.GetAbbreviatedValueTextFromValue(valueAmount), new TextObject("{=RVPidk5a}Influence", null)));
			int num = this.Save.MetaData.GetMainPartyHealthyMemberCount() + this.Save.MetaData.GetMainPartyWoundedMemberCount();
			int mainPartyPrisonerMemberCount = this.Save.MetaData.GetMainPartyPrisonerMemberCount();
			TextObject textObject2 = TextObject.Empty;
			if (mainPartyPrisonerMemberCount > 0)
			{
				textObject2 = new TextObject("{=6qYaQkDD}{COUNT} + {PRISONER_COUNT}p", null);
				textObject2.SetTextVariable("COUNT", num);
				textObject2.SetTextVariable("PRISONER_COUNT", mainPartyPrisonerMemberCount);
			}
			else
			{
				textObject2 = new TextObject(num, null);
			}
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.PartySize, textObject2, new TextObject("{=IXwOaa98}Party Size", null)));
			int value2 = (int)this.Save.MetaData.GetMainPartyFood();
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.Food, new TextObject(value2, null), new TextObject("{=qSi4DlT4}Food", null)));
			int clanFiefs = this.Save.MetaData.GetClanFiefs();
			this.SavedGameProperties.Add(new SavedGamePropertyVM(SavedGamePropertyVM.SavedGameProperty.Fiefs, new TextObject(clanFiefs, null), new TextObject("{=SRjrhb0A}Owned Fief Count", null)));
			TextObject textObject3 = new TextObject("{=GZWPHmAw}Day : {DAY}", null);
			string variable = ((int)this.Save.MetaData.GetDayLong()).ToString();
			textObject3.SetTextVariable("DAY", variable);
			this.GameTimeText = textObject3.ToString();
			TextObject textObject4 = new TextObject("{=IwhpeT8C}Level : {PLAYER_LEVEL}", null);
			textObject4.SetTextVariable("PLAYER_LEVEL", this.Save.MetaData.GetMainHeroLevel().ToString());
			this.LevelText = textObject4.ToString();
			this.DateTimeHint = new HintViewModel(new TextObject("{=!}" + this.RealTimeText1, null), null);
			this.UpdateButtonHint = new HintViewModel(new TextObject("{=ZDPIq4hi}Load the selected save game, overwrite it with the current version of the game and get back to this screen.", null), null);
			this.SaveLoadText = (this._isSaving ? new TextObject("{=bV75iwKa}Save", null).ToString() : new TextObject("{=9NuttOBC}Load", null).ToString());
			this.OverrideSaveText = new TextObject("{=hYL3CFHX}Do you want to overwrite this saved game?", null).ToString();
			this.UpdateSaveText = new TextObject("{=FFiPLPbs}Update Save", null).ToString();
			this.CorruptedSaveText = new TextObject("{=RoYPofhK}Corrupted Save", null).ToString();
			ValueTuple<bool, TextObject> isDisabledWithReason = this.GetIsDisabledWithReason();
			this.IsDisabled = isDisabledWithReason.Item1;
			this.DisabledReasonHint = new HintViewModel(isDisabledWithReason.Item2, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008118 File Offset: 0x00006318
		[return: TupleElementNames(new string[]
		{
			"IsDisabled",
			"Reason"
		})]
		private ValueTuple<bool, TextObject> GetIsDisabledWithReason()
		{
			ApplicationVersion applicationVersion = this.Save.MetaData.GetApplicationVersion();
			ApplicationVersion applicationVersionWithBuildNumber = Utilities.GetApplicationVersionWithBuildNumber();
			if (applicationVersionWithBuildNumber < applicationVersion)
			{
				TextObject textObject = new TextObject("{=9svpUWeo}Save version ({SAVE_VERSION}) is higher than the current version ({CURRENT_VERSION}).", null);
				textObject.SetTextVariable("SAVE_VERSION", applicationVersion.ToString());
				textObject.SetTextVariable("CURRENT_VERSION", applicationVersionWithBuildNumber.ToString());
				return new ValueTuple<bool, TextObject>(true, textObject);
			}
			return new ValueTuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008198 File Offset: 0x00006398
		public void ExecuteSaveLoad()
		{
			if (!this.IsCorrupted)
			{
				if (this._isSaving)
				{
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=Q1HIlJxe}Overwrite", null).ToString(), this.OverrideSaveText, true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), new Action(this.OnOverrideSaveAccept), delegate()
					{
						Action onCancelLoadSave = this._onCancelLoadSave;
						if (onCancelLoadSave == null)
						{
							return;
						}
						onCancelLoadSave();
					}, "", 0f, null, null, null), false, false);
					return;
				}
				SandBoxSaveHelper.TryLoadSave(this.Save, new Action<LoadResult>(this.StartGame), this._onCancelLoadSave);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000823F File Offset: 0x0000643F
		private void StartGame(LoadResult loadResult)
		{
			if (Game.Current != null)
			{
				ScreenManager.PopScreen();
				GameStateManager.Current.CleanStates(0);
				GameStateManager.Current = Module.CurrentModule.GlobalGameStateManager;
			}
			MBSaveLoad.OnStartGame(loadResult);
			MBGameManager.StartNewGame(new SandBoxGameManager(loadResult));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008278 File Offset: 0x00006478
		private void OnOverrideSaveAccept()
		{
			Campaign.Current.SaveHandler.SaveAs(this.Save.Name);
			this._onDone();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000082A0 File Offset: 0x000064A0
		private static TextObject GetAbbreviatedValueTextFromValue(int valueAmount)
		{
			string variable = "";
			decimal num = valueAmount;
			if (valueAmount < 10000)
			{
				return new TextObject(valueAmount, null);
			}
			if (valueAmount >= 10000 && valueAmount < 1000000)
			{
				variable = new TextObject("{=thousandabbr}k", null).ToString();
				num /= 1000m;
			}
			else if (valueAmount >= 1000000 && valueAmount < 1000000000)
			{
				variable = new TextObject("{=millionabbr}m", null).ToString();
				num /= 1000000m;
			}
			else if (valueAmount >= 1000000000 && valueAmount <= 2147483647)
			{
				variable = new TextObject("{=billionabbr}b", null).ToString();
				num /= 1000000000m;
			}
			int num2 = (int)num;
			string text = num2.ToString();
			if (text.Length < 3)
			{
				text += ".";
				string text2 = num.ToString("F3").Split(new char[]
				{
					'.'
				}).ElementAtOrDefault(1);
				if (text2 != null)
				{
					for (int i = 0; i < 3 - num2.ToString().Length; i++)
					{
						if (text2.ElementAtOrDefault(i) != '\0')
						{
							text += text2.ElementAtOrDefault(i).ToString();
						}
					}
				}
			}
			TextObject textObject = new TextObject("{=mapbardenarvalue}{DENAR_AMOUNT}{VALUE_ABBREVIATION}", null);
			textObject.SetTextVariable("DENAR_AMOUNT", text);
			textObject.SetTextVariable("VALUE_ABBREVIATION", variable);
			return textObject;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008416 File Offset: 0x00006616
		public void ExecuteUpdate()
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008418 File Offset: 0x00006618
		public void ExecuteDelete()
		{
			this._onDelete(this);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008426 File Offset: 0x00006626
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008434 File Offset: 0x00006634
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000843C File Offset: 0x0000663C
		[DataSourceProperty]
		public MBBindingList<SavedGamePropertyVM> SavedGameProperties
		{
			get
			{
				return this._savedGameProperties;
			}
			set
			{
				if (value != this._savedGameProperties)
				{
					this._savedGameProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SavedGamePropertyVM>>(value, "SavedGameProperties");
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000845A File Offset: 0x0000665A
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00008462 File Offset: 0x00006662
		[DataSourceProperty]
		public MBBindingList<SavedGameModuleInfoVM> LoadedModulesInSave
		{
			get
			{
				return this._loadedModulesInSave;
			}
			set
			{
				if (value != this._loadedModulesInSave)
				{
					this._loadedModulesInSave = value;
					base.OnPropertyChangedWithValue<MBBindingList<SavedGameModuleInfoVM>>(value, "LoadedModulesInSave");
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00008480 File Offset: 0x00006680
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00008488 File Offset: 0x00006688
		[DataSourceProperty]
		public string SaveVersionAsString
		{
			get
			{
				return this._saveVersionAsString;
			}
			set
			{
				if (value != this._saveVersionAsString)
				{
					this._saveVersionAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "SaveVersionAsString");
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000084AB File Offset: 0x000066AB
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000084B3 File Offset: 0x000066B3
		[DataSourceProperty]
		public string DeleteText
		{
			get
			{
				return this._deleteText;
			}
			set
			{
				if (value != this._deleteText)
				{
					this._deleteText = value;
					base.OnPropertyChangedWithValue<string>(value, "DeleteText");
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000084D6 File Offset: 0x000066D6
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000084DE File Offset: 0x000066DE
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000084FC File Offset: 0x000066FC
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00008504 File Offset: 0x00006704
		[DataSourceProperty]
		public bool IsCorrupted
		{
			get
			{
				return this._isCorrupted;
			}
			set
			{
				if (value != this._isCorrupted)
				{
					this._isCorrupted = value;
					base.OnPropertyChangedWithValue(value, "IsCorrupted");
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008522 File Offset: 0x00006722
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000852A File Offset: 0x0000672A
		[DataSourceProperty]
		public string BannerTextCode
		{
			get
			{
				return this._bannerTextCode;
			}
			set
			{
				if (value != this._bannerTextCode)
				{
					this._bannerTextCode = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerTextCode");
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000854D File Offset: 0x0000674D
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00008555 File Offset: 0x00006755
		[DataSourceProperty]
		public string SaveLoadText
		{
			get
			{
				return this._saveLoadText;
			}
			set
			{
				if (value != this._saveLoadText)
				{
					this._saveLoadText = value;
					base.OnPropertyChangedWithValue<string>(value, "SaveLoadText");
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008578 File Offset: 0x00006778
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00008580 File Offset: 0x00006780
		[DataSourceProperty]
		public string OverrideSaveText
		{
			get
			{
				return this._overwriteSaveText;
			}
			set
			{
				if (value != this._overwriteSaveText)
				{
					this._overwriteSaveText = value;
					base.OnPropertyChangedWithValue<string>(value, "OverrideSaveText");
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000085A3 File Offset: 0x000067A3
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000085AB File Offset: 0x000067AB
		[DataSourceProperty]
		public string UpdateSaveText
		{
			get
			{
				return this._updateSaveText;
			}
			set
			{
				if (value != this._updateSaveText)
				{
					this._updateSaveText = value;
					base.OnPropertyChangedWithValue<string>(value, "UpdateSaveText");
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000085CE File Offset: 0x000067CE
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000085D6 File Offset: 0x000067D6
		[DataSourceProperty]
		public string ModulesText
		{
			get
			{
				return this._modulesText;
			}
			set
			{
				if (value != this._modulesText)
				{
					this._modulesText = value;
					base.OnPropertyChangedWithValue<string>(value, "ModulesText");
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000085F9 File Offset: 0x000067F9
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00008601 File Offset: 0x00006801
		[DataSourceProperty]
		public string CorruptedSaveText
		{
			get
			{
				return this._corruptedSaveText;
			}
			set
			{
				if (value != this._corruptedSaveText)
				{
					this._corruptedSaveText = value;
					base.OnPropertyChangedWithValue<string>(value, "CorruptedSaveText");
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008624 File Offset: 0x00006824
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000862C File Offset: 0x0000682C
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000864F File Offset: 0x0000684F
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00008657 File Offset: 0x00006857
		[DataSourceProperty]
		public string GameTimeText
		{
			get
			{
				return this._gameTimeText;
			}
			set
			{
				if (value != this._gameTimeText)
				{
					this._gameTimeText = value;
					base.OnPropertyChangedWithValue<string>(value, "GameTimeText");
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000867A File Offset: 0x0000687A
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00008682 File Offset: 0x00006882
		[DataSourceProperty]
		public string CharacterNameText
		{
			get
			{
				return this._characterNameText;
			}
			set
			{
				if (value != this._characterNameText)
				{
					this._characterNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "CharacterNameText");
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000086A5 File Offset: 0x000068A5
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000086AD File Offset: 0x000068AD
		[DataSourceProperty]
		public string MainHeroVisualCode
		{
			get
			{
				return this._mainHeroVisualCode;
			}
			set
			{
				if (value != this._mainHeroVisualCode)
				{
					this._mainHeroVisualCode = value;
					base.OnPropertyChangedWithValue<string>(value, "MainHeroVisualCode");
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000086D0 File Offset: 0x000068D0
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000086D8 File Offset: 0x000068D8
		[DataSourceProperty]
		public CharacterViewModel CharacterVisual
		{
			get
			{
				return this._characterVisual;
			}
			set
			{
				if (value != this._characterVisual)
				{
					this._characterVisual = value;
					base.OnPropertyChangedWithValue<CharacterViewModel>(value, "CharacterVisual");
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000086F6 File Offset: 0x000068F6
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000086FE File Offset: 0x000068FE
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000871C File Offset: 0x0000691C
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00008724 File Offset: 0x00006924
		[DataSourceProperty]
		public string RealTimeText1
		{
			get
			{
				return this._realTimeText1;
			}
			set
			{
				if (value != this._realTimeText1)
				{
					this._realTimeText1 = value;
					base.OnPropertyChangedWithValue<string>(value, "RealTimeText1");
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008747 File Offset: 0x00006947
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000874F File Offset: 0x0000694F
		[DataSourceProperty]
		public string RealTimeText2
		{
			get
			{
				return this._realTimeText2;
			}
			set
			{
				if (value != this._realTimeText2)
				{
					this._realTimeText2 = value;
					base.OnPropertyChangedWithValue<string>(value, "RealTimeText2");
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008772 File Offset: 0x00006972
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000877A File Offset: 0x0000697A
		[DataSourceProperty]
		public string LevelText
		{
			get
			{
				return this._levelText;
			}
			set
			{
				if (value != this._levelText)
				{
					this._levelText = value;
					base.OnPropertyChangedWithValue<string>(value, "LevelText");
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000879D File Offset: 0x0000699D
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000087A5 File Offset: 0x000069A5
		[DataSourceProperty]
		public HintViewModel DateTimeHint
		{
			get
			{
				return this._dateTimeHint;
			}
			set
			{
				if (value != this._dateTimeHint)
				{
					this._dateTimeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DateTimeHint");
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000087C3 File Offset: 0x000069C3
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000087CB File Offset: 0x000069CB
		[DataSourceProperty]
		public HintViewModel UpdateButtonHint
		{
			get
			{
				return this._updateButtonHint;
			}
			set
			{
				if (value != this._updateButtonHint)
				{
					this._updateButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UpdateButtonHint");
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000087E9 File Offset: 0x000069E9
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000087F1 File Offset: 0x000069F1
		[DataSourceProperty]
		public HintViewModel DisabledReasonHint
		{
			get
			{
				return this._disabledReasonHint;
			}
			set
			{
				if (value != this._disabledReasonHint)
				{
					this._disabledReasonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledReasonHint");
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000880F File Offset: 0x00006A0F
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00008817 File Offset: 0x00006A17
		[DataSourceProperty]
		public bool IsFilteredOut
		{
			get
			{
				return this._isFilteredOut;
			}
			set
			{
				if (value != this._isFilteredOut)
				{
					this._isFilteredOut = value;
					base.OnPropertyChangedWithValue(value, "IsFilteredOut");
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008835 File Offset: 0x00006A35
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000883D File Offset: 0x00006A3D
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x0400008B RID: 139
		private readonly bool _isSaving;

		// Token: 0x0400008C RID: 140
		private readonly Action _onDone;

		// Token: 0x0400008D RID: 141
		private readonly Action<SavedGameVM> _onDelete;

		// Token: 0x0400008E RID: 142
		private readonly Action<SavedGameVM> _onSelection;

		// Token: 0x0400008F RID: 143
		private readonly Action _onCancelLoadSave;

		// Token: 0x04000090 RID: 144
		private readonly TextObject _newlineTextObject = new TextObject("{=ol0rBSrb}{STR1}{newline}{STR2}", null);

		// Token: 0x04000091 RID: 145
		private readonly ApplicationVersion _gameVersion;

		// Token: 0x04000092 RID: 146
		private readonly ApplicationVersion _saveVersion;

		// Token: 0x04000093 RID: 147
		private MBBindingList<SavedGamePropertyVM> _savedGameProperties;

		// Token: 0x04000094 RID: 148
		private MBBindingList<SavedGameModuleInfoVM> _loadedModulesInSave;

		// Token: 0x04000095 RID: 149
		private HintViewModel _dateTimeHint;

		// Token: 0x04000096 RID: 150
		private HintViewModel _updateButtonHint;

		// Token: 0x04000097 RID: 151
		private HintViewModel _disabledReasonHint;

		// Token: 0x04000098 RID: 152
		private ImageIdentifierVM _clanBanner;

		// Token: 0x04000099 RID: 153
		private CharacterViewModel _characterVisual;

		// Token: 0x0400009A RID: 154
		private string _deleteText;

		// Token: 0x0400009B RID: 155
		private string _nameText;

		// Token: 0x0400009C RID: 156
		private string _gameTimeText;

		// Token: 0x0400009D RID: 157
		private string _realTimeText1;

		// Token: 0x0400009E RID: 158
		private string _realTimeText2;

		// Token: 0x0400009F RID: 159
		private string _levelText;

		// Token: 0x040000A0 RID: 160
		private string _characterNameText;

		// Token: 0x040000A1 RID: 161
		private string _saveLoadText;

		// Token: 0x040000A2 RID: 162
		private string _overwriteSaveText;

		// Token: 0x040000A3 RID: 163
		private string _updateSaveText;

		// Token: 0x040000A4 RID: 164
		private string _modulesText;

		// Token: 0x040000A5 RID: 165
		private string _corruptedSaveText;

		// Token: 0x040000A6 RID: 166
		private string _saveVersionAsString;

		// Token: 0x040000A7 RID: 167
		private string _mainHeroVisualCode;

		// Token: 0x040000A8 RID: 168
		private string _bannerTextCode;

		// Token: 0x040000A9 RID: 169
		private bool _isSelected;

		// Token: 0x040000AA RID: 170
		private bool _isCorrupted;

		// Token: 0x040000AB RID: 171
		private bool _isFilteredOut;

		// Token: 0x040000AC RID: 172
		private bool _isDisabled;
	}
}
