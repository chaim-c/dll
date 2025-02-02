using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;
using TaleWorlds.ScreenSystem;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x02000013 RID: 19
	public class SaveLoadVM : ViewModel
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000886D File Offset: 0x00006A6D
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00008875 File Offset: 0x00006A75
		public bool IsBusyWithAnAction { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000887E File Offset: 0x00006A7E
		private IEnumerable<SaveGameFileInfo> _allSavedGames
		{
			get
			{
				return this.SaveGroups.SelectMany((SavedGameGroupVM s) => from v in s.SavedGamesList
				select v.Save);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000088AA File Offset: 0x00006AAA
		private SavedGameVM _defaultFirstSavedGame
		{
			get
			{
				SavedGameGroupVM savedGameGroupVM = this.SaveGroups.FirstOrDefault((SavedGameGroupVM x) => x.SavedGamesList.Count > 0);
				if (savedGameGroupVM == null)
				{
					return null;
				}
				return savedGameGroupVM.SavedGamesList.FirstOrDefault<SavedGameVM>();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000088E8 File Offset: 0x00006AE8
		public SaveLoadVM(bool isSaving, bool isCampaignMapOnStack)
		{
			this._isSaving = isSaving;
			this.SaveGroups = new MBBindingList<SavedGameGroupVM>();
			this.IsVisualDisabled = false;
			List<ModuleInfo> moduleInfos = ModuleHelper.GetModuleInfos(Utilities.GetModulesNames());
			int num = 0;
			SaveGameFileInfo[] saveFiles = MBSaveLoad.GetSaveFiles(null);
			IEnumerable<SaveGameFileInfo> enumerable = from s in saveFiles
			where s.IsCorrupted
			select s;
			foreach (IGrouping<string, SaveGameFileInfo> grouping in from s in saveFiles
			where !s.IsCorrupted
			select s into m
			group m by m.MetaData.GetUniqueGameId() into s
			orderby this.GetMostRecentSaveInGroup(s) descending
			select s)
			{
				SavedGameGroupVM savedGameGroupVM = new SavedGameGroupVM();
				if (string.IsNullOrWhiteSpace(grouping.Key))
				{
					savedGameGroupVM.IdentifierID = this._uncategorizedSaveGroupName.ToString();
				}
				else
				{
					num++;
					this._categorizedSaveGroupName.SetTextVariable("ID", num);
					savedGameGroupVM.IdentifierID = this._categorizedSaveGroupName.ToString();
				}
				foreach (SaveGameFileInfo saveGameFileInfo in grouping.OrderByDescending((SaveGameFileInfo s) => s.MetaData.GetCreationTime()))
				{
					bool isDiscrepancyDetectedForSave = SaveLoadVM.IsAnyModuleMissingFromSaveOrCurrentModules(moduleInfos, saveGameFileInfo.MetaData.GetModules());
					bool ironmanMode = saveGameFileInfo.MetaData.GetIronmanMode();
					savedGameGroupVM.SavedGamesList.Add(new SavedGameVM(saveGameFileInfo, this.IsSaving, new Action<SavedGameVM>(this.OnDeleteSavedGame), new Action<SavedGameVM>(this.OnSaveSelection), new Action(this.OnCancelLoadSave), new Action(this.ExecuteDone), false, isDiscrepancyDetectedForSave, ironmanMode));
				}
				this.SaveGroups.Add(savedGameGroupVM);
			}
			if (enumerable.Any<SaveGameFileInfo>())
			{
				SavedGameGroupVM savedGameGroupVM2 = new SavedGameGroupVM
				{
					IdentifierID = new TextObject("{=o9PIe7am}Corrupted", null).ToString()
				};
				foreach (SaveGameFileInfo save in enumerable)
				{
					savedGameGroupVM2.SavedGamesList.Add(new SavedGameVM(save, this.IsSaving, new Action<SavedGameVM>(this.OnDeleteSavedGame), new Action<SavedGameVM>(this.OnSaveSelection), new Action(this.OnCancelLoadSave), new Action(this.ExecuteDone), true, false, false));
				}
				this.SaveGroups.Add(savedGameGroupVM2);
			}
			this.RefreshCanCreateNewSave();
			this.OnSaveSelection(this._defaultFirstSavedGame);
			this.RefreshValues();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008C94 File Offset: 0x00006E94
		private void RefreshCanCreateNewSave()
		{
			this.CanCreateNewSave = !MBSaveLoad.IsMaxNumberOfSavesReached();
			this.CreateNewSaveHint = new HintViewModel(this.CanCreateNewSave ? TextObject.Empty : new TextObject("{=DeXfSjgY}Cannot create a new save. Save limit reached.", null), null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008CCC File Offset: 0x00006ECC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = new TextObject("{=hiCxFj4E}Saved Campaigns", null).ToString();
			this.DoneText = new TextObject("{=WiNRdfsm}Done", null).ToString();
			this.CreateNewSaveSlotText = new TextObject("{=eL8nhkhQ}Create New Save Slot", null).ToString();
			this.CancelText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();
			this.SaveLoadText = (this._isSaving ? new TextObject("{=bV75iwKa}Save", null).ToString() : new TextObject("{=9NuttOBC}Load", null).ToString());
			this.SearchPlaceholderText = new TextObject("{=tQOPRBFg}Search...", null).ToString();
			if (this.IsVisualDisabled)
			{
				this.VisualDisabledText = this._visualIsDisabledText.ToString();
			}
			this.SaveGroups.ApplyActionOnAllItems(delegate(SavedGameGroupVM x)
			{
				x.RefreshValues();
			});
			SavedGameVM currentSelectedSave = this.CurrentSelectedSave;
			if (currentSelectedSave == null)
			{
				return;
			}
			currentSelectedSave.RefreshValues();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008DD0 File Offset: 0x00006FD0
		private DateTime GetMostRecentSaveInGroup(IGrouping<string, SaveGameFileInfo> group)
		{
			SaveGameFileInfo saveGameFileInfo = (from g in @group
			orderby g.MetaData.GetCreationTime() descending
			select g).FirstOrDefault<SaveGameFileInfo>();
			if (saveGameFileInfo == null)
			{
				return default(DateTime);
			}
			return saveGameFileInfo.MetaData.GetCreationTime();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008E20 File Offset: 0x00007020
		private void OnSaveSelection(SavedGameVM save)
		{
			if (save != this.CurrentSelectedSave)
			{
				if (this.CurrentSelectedSave != null)
				{
					this.CurrentSelectedSave.IsSelected = false;
				}
				this.CurrentSelectedSave = save;
				if (this.CurrentSelectedSave != null)
				{
					this.CurrentSelectedSave.IsSelected = true;
				}
				this.IsAnyItemSelected = (this.CurrentSelectedSave != null);
				this.IsActionEnabled = (this.IsAnyItemSelected && !this.CurrentSelectedSave.IsCorrupted);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008E94 File Offset: 0x00007094
		public void ExecuteCreateNewSaveGame()
		{
			InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=7WdWK2Dt}Save Game", null).ToString(), new TextObject("{=WDlVhNuq}Name your save file", null).ToString(), true, true, new TextObject("{=WiNRdfsm}Done", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), new Action<string>(this.OnSaveAsDone), null, false, new Func<string, Tuple<bool, string>>(this.IsSaveGameNameApplicable), "", ""), false, false);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008F14 File Offset: 0x00007114
		private Tuple<bool, string> IsSaveGameNameApplicable(string inputText)
		{
			string item = string.Empty;
			bool item2 = true;
			if (string.IsNullOrEmpty(inputText))
			{
				item = this._textIsEmptyReasonText.ToString();
				item2 = false;
			}
			else if (inputText.All((char c) => char.IsWhiteSpace(c)))
			{
				item = this._allSpaceReasonText.ToString();
				item2 = false;
			}
			else if (inputText.Any((char c) => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
			{
				item = this._textHasSpecialCharReasonText.ToString();
				item2 = false;
			}
			else if (inputText.Length >= 30)
			{
				this._textTooLongReasonText.SetTextVariable("MAX_LENGTH", 30);
				item = this._textTooLongReasonText.ToString();
				item2 = false;
			}
			else if (MBSaveLoad.IsSaveFileNameReserved(inputText))
			{
				item = this._saveNameReservedReasonText.ToString();
				item2 = false;
			}
			else if (this._allSavedGames.Any((SaveGameFileInfo s) => string.Equals(s.Name, inputText, StringComparison.InvariantCultureIgnoreCase)))
			{
				item = this._saveAlreadyExistsReasonText.ToString();
				item2 = false;
			}
			return new Tuple<bool, string>(item2, item);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000904E File Offset: 0x0000724E
		private void OnSaveAsDone(string saveName)
		{
			Campaign.Current.SaveHandler.SaveAs(saveName);
			this.ExecuteDone();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009066 File Offset: 0x00007266
		public void ExecuteDone()
		{
			ScreenManager.PopScreen();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000906D File Offset: 0x0000726D
		public void ExecuteLoadSave()
		{
			this.LoadSelectedSave();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009075 File Offset: 0x00007275
		private void LoadSelectedSave()
		{
			if (!this.IsBusyWithAnAction && this.CurrentSelectedSave != null && !this.CurrentSelectedSave.IsCorrupted)
			{
				this.CurrentSelectedSave.ExecuteSaveLoad();
				this.IsBusyWithAnAction = true;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000090A6 File Offset: 0x000072A6
		private void OnCancelLoadSave()
		{
			this.IsBusyWithAnAction = false;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000090AF File Offset: 0x000072AF
		private void ExecuteResetCurrentSave()
		{
			this.CurrentSelectedSave = null;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000090B8 File Offset: 0x000072B8
		private void OnDeleteSavedGame(SavedGameVM savedGame)
		{
			if (!this.IsBusyWithAnAction)
			{
				this.IsBusyWithAnAction = true;
				string text = new TextObject("{=M1AEHJ76}Please notice that this save is created for a session which has Ironman mode enabled. There is no other save file for the related session. Are you sure you want to delete this save game?", null).ToString();
				string text2 = new TextObject("{=HH2mZq8J}Are you sure you want to delete this save game?", null).ToString();
				string titleText = new TextObject("{=QHV8aeEg}Delete Save", null).ToString();
				string text3 = savedGame.Save.MetaData.GetIronmanMode() ? text : text2;
				InformationManager.ShowInquiry(new InquiryData(titleText, text3, true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), delegate()
				{
					this.IsBusyWithAnAction = true;
					bool flag = MBSaveLoad.DeleteSaveGame(savedGame.Save.Name);
					this.IsBusyWithAnAction = false;
					if (flag)
					{
						this.DeleteSave(savedGame);
						this.OnSaveSelection(this._defaultFirstSavedGame);
						this.RefreshCanCreateNewSave();
						return;
					}
					this.OnDeleteSaveUnsuccessful();
				}, delegate()
				{
					this.IsBusyWithAnAction = false;
				}, "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009190 File Offset: 0x00007390
		private void OnDeleteSaveUnsuccessful()
		{
			string titleText = new TextObject("{=oZrVNUOk}Error", null).ToString();
			string text = new TextObject("{=PY00wRz4}Failed to delete the save file.", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(titleText, text, true, false, new TextObject("{=WiNRdfsm}Done", null).ToString(), string.Empty, null, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000091F4 File Offset: 0x000073F4
		private void DeleteSave(SavedGameVM save)
		{
			foreach (SavedGameGroupVM savedGameGroupVM in this.SaveGroups)
			{
				if (savedGameGroupVM.SavedGamesList.Contains(save))
				{
					savedGameGroupVM.SavedGamesList.Remove(save);
					break;
				}
			}
			if (string.IsNullOrEmpty(BannerlordConfig.LatestSaveGameName) || save.Save.Name == BannerlordConfig.LatestSaveGameName)
			{
				SavedGameVM defaultFirstSavedGame = this._defaultFirstSavedGame;
				BannerlordConfig.LatestSaveGameName = (((defaultFirstSavedGame != null) ? defaultFirstSavedGame.Save.Name : null) ?? string.Empty);
				BannerlordConfig.Save();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000092A8 File Offset: 0x000074A8
		public void DeleteSelectedSave()
		{
			if (this.CurrentSelectedSave != null)
			{
				this.OnDeleteSavedGame(this.CurrentSelectedSave);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000092BE File Offset: 0x000074BE
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM deleteInputKey = this.DeleteInputKey;
			if (deleteInputKey == null)
			{
				return;
			}
			deleteInputKey.OnFinalize();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000092F8 File Offset: 0x000074F8
		private static bool IsAnyModuleMissingFromSaveOrCurrentModules(List<ModuleInfo> loadedModules, string[] modulesInSave)
		{
			string[] modulesInSave2 = modulesInSave;
			for (int i = 0; i < modulesInSave2.Length; i++)
			{
				string moduleName = modulesInSave2[i];
				if (loadedModules.All((ModuleInfo loadedModule) => loadedModule.Name != moduleName))
				{
					return true;
				}
			}
			Func<ModuleInfo, bool> <>9__1;
			Func<ModuleInfo, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((ModuleInfo loadedModule) => modulesInSave.All((string moduleName) => loadedModule.Name != moduleName)));
			}
			using (IEnumerator<ModuleInfo> enumerator = loadedModules.Where(predicate).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ModuleInfo moduleInfo = enumerator.Current;
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000093B4 File Offset: 0x000075B4
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000093BC File Offset: 0x000075BC
		[DataSourceProperty]
		public bool IsSearchAvailable
		{
			get
			{
				return this._isSearchAvailable;
			}
			set
			{
				if (value != this._isSearchAvailable)
				{
					this._isSearchAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsSearchAvailable");
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000093DA File Offset: 0x000075DA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000093E2 File Offset: 0x000075E2
		[DataSourceProperty]
		public string SearchText
		{
			get
			{
				return this._searchText;
			}
			set
			{
				if (value != this._searchText)
				{
					value.IndexOf(this._searchText ?? "");
					this._searchText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchText");
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000941B File Offset: 0x0000761B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009423 File Offset: 0x00007623
		[DataSourceProperty]
		public string SearchPlaceholderText
		{
			get
			{
				return this._searchPlaceholderText;
			}
			set
			{
				if (value != this._searchPlaceholderText)
				{
					this._searchPlaceholderText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchPlaceholderText");
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009446 File Offset: 0x00007646
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000944E File Offset: 0x0000764E
		[DataSourceProperty]
		public string VisualDisabledText
		{
			get
			{
				return this._visualDisabledText;
			}
			set
			{
				if (value != this._visualDisabledText)
				{
					this._visualDisabledText = value;
					base.OnPropertyChangedWithValue<string>(value, "VisualDisabledText");
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009471 File Offset: 0x00007671
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009479 File Offset: 0x00007679
		[DataSourceProperty]
		public MBBindingList<SavedGameGroupVM> SaveGroups
		{
			get
			{
				return this._saveGroups;
			}
			set
			{
				if (value != this._saveGroups)
				{
					this._saveGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<SavedGameGroupVM>>(value, "SaveGroups");
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009497 File Offset: 0x00007697
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000949F File Offset: 0x0000769F
		[DataSourceProperty]
		public SavedGameVM CurrentSelectedSave
		{
			get
			{
				return this._currentSelectedSave;
			}
			set
			{
				if (value != this._currentSelectedSave)
				{
					this._currentSelectedSave = value;
					base.OnPropertyChangedWithValue<SavedGameVM>(value, "CurrentSelectedSave");
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000094BD File Offset: 0x000076BD
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000094C5 File Offset: 0x000076C5
		[DataSourceProperty]
		public string CreateNewSaveSlotText
		{
			get
			{
				return this._createNewSaveSlotText;
			}
			set
			{
				if (value != this._createNewSaveSlotText)
				{
					this._createNewSaveSlotText = value;
					base.OnPropertyChangedWithValue<string>(value, "CreateNewSaveSlotText");
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000094E8 File Offset: 0x000076E8
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000094F0 File Offset: 0x000076F0
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009513 File Offset: 0x00007713
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000951B File Offset: 0x0000771B
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000953E File Offset: 0x0000773E
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009546 File Offset: 0x00007746
		[DataSourceProperty]
		public bool IsSaving
		{
			get
			{
				return this._isSaving;
			}
			set
			{
				if (value != this._isSaving)
				{
					this._isSaving = value;
					base.OnPropertyChangedWithValue(value, "IsSaving");
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009564 File Offset: 0x00007764
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000956C File Offset: 0x0000776C
		[DataSourceProperty]
		public bool CanCreateNewSave
		{
			get
			{
				return this._canCreateNewSave;
			}
			set
			{
				if (value != this._canCreateNewSave)
				{
					this._canCreateNewSave = value;
					base.OnPropertyChangedWithValue(value, "CanCreateNewSave");
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000958A File Offset: 0x0000778A
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009592 File Offset: 0x00007792
		[DataSourceProperty]
		public bool IsVisualDisabled
		{
			get
			{
				return this._isVisualDisabled;
			}
			set
			{
				if (value != this._isVisualDisabled)
				{
					this._isVisualDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsVisualDisabled");
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000095B0 File Offset: 0x000077B0
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000095B8 File Offset: 0x000077B8
		[DataSourceProperty]
		public HintViewModel CreateNewSaveHint
		{
			get
			{
				return this._createNewSaveHint;
			}
			set
			{
				if (value != this._createNewSaveHint)
				{
					this._createNewSaveHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CreateNewSaveHint");
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000095D6 File Offset: 0x000077D6
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000095DE File Offset: 0x000077DE
		[DataSourceProperty]
		public bool IsActionEnabled
		{
			get
			{
				return this._isActionEnabled;
			}
			set
			{
				if (value != this._isActionEnabled)
				{
					this._isActionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsActionEnabled");
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000095FC File Offset: 0x000077FC
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00009604 File Offset: 0x00007804
		[DataSourceProperty]
		public bool IsAnyItemSelected
		{
			get
			{
				return this._isAnyItemSelected;
			}
			set
			{
				if (value != this._isAnyItemSelected)
				{
					this._isAnyItemSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyItemSelected");
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009622 File Offset: 0x00007822
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000962A File Offset: 0x0000782A
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000964D File Offset: 0x0000784D
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009655 File Offset: 0x00007855
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

		// Token: 0x060001CA RID: 458 RVA: 0x00009678 File Offset: 0x00007878
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009687 File Offset: 0x00007887
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009696 File Offset: 0x00007896
		public void SetDeleteInputKey(HotKey hotkey)
		{
			this.DeleteInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000096A5 File Offset: 0x000078A5
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000096AD File Offset: 0x000078AD
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000096CB File Offset: 0x000078CB
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000096D3 File Offset: 0x000078D3
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000096F1 File Offset: 0x000078F1
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000096F9 File Offset: 0x000078F9
		public InputKeyItemVM DeleteInputKey
		{
			get
			{
				return this._deleteInputKey;
			}
			set
			{
				if (value != this._deleteInputKey)
				{
					this._deleteInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DeleteInputKey");
				}
			}
		}

		// Token: 0x040000AD RID: 173
		private const int _maxSaveFileNameLength = 30;

		// Token: 0x040000AF RID: 175
		private readonly TextObject _categorizedSaveGroupName = new TextObject("{=nVGqjtaa}Campaign {ID}", null);

		// Token: 0x040000B0 RID: 176
		private readonly TextObject _uncategorizedSaveGroupName = new TextObject("{=uncategorized_save}Uncategorized", null);

		// Token: 0x040000B1 RID: 177
		private readonly TextObject _textIsEmptyReasonText = new TextObject("{=7AI8jA0b}Input text cannot be empty.", null);

		// Token: 0x040000B2 RID: 178
		private readonly TextObject _textHasSpecialCharReasonText = new TextObject("{=kXRdeawC}Input text cannot include special characters.", null);

		// Token: 0x040000B3 RID: 179
		private readonly TextObject _textTooLongReasonText = new TextObject("{=B3W6fcQX}Input text cannot be longer than {MAX_LENGTH} characters.", null);

		// Token: 0x040000B4 RID: 180
		private readonly TextObject _saveAlreadyExistsReasonText = new TextObject("{=aG6XMhA1}A saved game file already exists with this name.", null);

		// Token: 0x040000B5 RID: 181
		private readonly TextObject _saveNameReservedReasonText = new TextObject("{=M4WMKyE1}Input text includes reserved text.", null);

		// Token: 0x040000B6 RID: 182
		private readonly TextObject _allSpaceReasonText = new TextObject("{=Rtakaivj}Input text needs to include at least one non-space character.", null);

		// Token: 0x040000B7 RID: 183
		private readonly TextObject _visualIsDisabledText = new TextObject("{=xlEZ02Qw}Character visual is disabled during 'Save As' on the campaign map.", null);

		// Token: 0x040000B8 RID: 184
		private bool _isSearchAvailable;

		// Token: 0x040000B9 RID: 185
		private string _searchText;

		// Token: 0x040000BA RID: 186
		private string _searchPlaceholderText;

		// Token: 0x040000BB RID: 187
		private string _doneText;

		// Token: 0x040000BC RID: 188
		private string _createNewSaveSlotText;

		// Token: 0x040000BD RID: 189
		private string _titleText;

		// Token: 0x040000BE RID: 190
		private string _visualDisabledText;

		// Token: 0x040000BF RID: 191
		private bool _isSaving;

		// Token: 0x040000C0 RID: 192
		private bool _isActionEnabled;

		// Token: 0x040000C1 RID: 193
		private bool _isAnyItemSelected;

		// Token: 0x040000C2 RID: 194
		private bool _canCreateNewSave;

		// Token: 0x040000C3 RID: 195
		private bool _isVisualDisabled;

		// Token: 0x040000C4 RID: 196
		private string _saveLoadText;

		// Token: 0x040000C5 RID: 197
		private string _cancelText;

		// Token: 0x040000C6 RID: 198
		private HintViewModel _createNewSaveHint;

		// Token: 0x040000C7 RID: 199
		private MBBindingList<SavedGameGroupVM> _saveGroups;

		// Token: 0x040000C8 RID: 200
		private SavedGameVM _currentSelectedSave;

		// Token: 0x040000C9 RID: 201
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040000CA RID: 202
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040000CB RID: 203
		private InputKeyItemVM _deleteInputKey;
	}
}
