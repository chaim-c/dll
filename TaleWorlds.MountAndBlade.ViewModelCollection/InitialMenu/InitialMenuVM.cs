using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu
{
	// Token: 0x0200003E RID: 62
	public class InitialMenuVM : ViewModel
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x00016F6C File Offset: 0x0001516C
		public InitialMenuVM(InitialState initialState)
		{
			this.SelectProfileText = new TextObject("{=wubDWOlh}Select Profile", null).ToString();
			this.DownloadingText = new TextObject("{=i4Oo6aoM}Downloading Content...", null).ToString();
			if (HotKeyManager.ShouldNotifyDocumentVersionDifferent())
			{
				MBInformationManager.AddQuickInformation(new TextObject("{=0Itt3bZM}Current keybind document version is outdated. Keybinds have been reverted to defaults.", null), 0, null, "");
			}
			this.GameVersionText = Utilities.GetApplicationVersionWithBuildNumber().ToString();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016FE2 File Offset: 0x000151E2
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.MenuOptions.ApplyActionOnAllItems(delegate(InitialMenuOptionVM o)
			{
				o.RefreshValues();
			});
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00017014 File Offset: 0x00015214
		public void RefreshMenuOptions()
		{
			this.MenuOptions = new MBBindingList<InitialMenuOptionVM>();
			GameState activeState = GameStateManager.Current.ActiveState;
			foreach (InitialStateOption initialStateOption in Module.CurrentModule.GetInitialStateOptions())
			{
				this.MenuOptions.Add(new InitialMenuOptionVM(initialStateOption));
			}
			this.IsDownloadingContent = Utilities.IsOnlyCoreContentEnabled();
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017090 File Offset: 0x00015290
		public override void OnFinalize()
		{
			base.OnFinalize();
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00017098 File Offset: 0x00015298
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x000170A0 File Offset: 0x000152A0
		[DataSourceProperty]
		public MBBindingList<InitialMenuOptionVM> MenuOptions
		{
			get
			{
				return this._menuOptions;
			}
			set
			{
				if (value != this._menuOptions)
				{
					this._menuOptions = value;
					base.OnPropertyChangedWithValue<MBBindingList<InitialMenuOptionVM>>(value, "MenuOptions");
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000170BE File Offset: 0x000152BE
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x000170C6 File Offset: 0x000152C6
		[DataSourceProperty]
		public string DownloadingText
		{
			get
			{
				return this._downloadingText;
			}
			set
			{
				if (value != this._downloadingText)
				{
					this._downloadingText = value;
					base.OnPropertyChangedWithValue<string>(value, "DownloadingText");
				}
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000170E9 File Offset: 0x000152E9
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x000170F1 File Offset: 0x000152F1
		[DataSourceProperty]
		public string SelectProfileText
		{
			get
			{
				return this._selectProfileText;
			}
			set
			{
				if (value != this._selectProfileText)
				{
					this._selectProfileText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectProfileText");
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00017114 File Offset: 0x00015314
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0001711C File Offset: 0x0001531C
		[DataSourceProperty]
		public string ProfileName
		{
			get
			{
				return this._profileName;
			}
			set
			{
				if (value != this._profileName)
				{
					this._profileName = value;
					base.OnPropertyChangedWithValue<string>(value, "ProfileName");
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001713F File Offset: 0x0001533F
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00017147 File Offset: 0x00015347
		[DataSourceProperty]
		public string GameVersionText
		{
			get
			{
				return this._gameVersionText;
			}
			set
			{
				if (value != this._gameVersionText)
				{
					this._gameVersionText = value;
					base.OnPropertyChangedWithValue<string>(value, "GameVersionText");
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001716A File Offset: 0x0001536A
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00017172 File Offset: 0x00015372
		[DataSourceProperty]
		public bool IsProfileSelectionEnabled
		{
			get
			{
				return this._isProfileSelectionEnabled;
			}
			set
			{
				if (value != this._isProfileSelectionEnabled)
				{
					this._isProfileSelectionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsProfileSelectionEnabled");
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00017190 File Offset: 0x00015390
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00017198 File Offset: 0x00015398
		[DataSourceProperty]
		public bool IsDownloadingContent
		{
			get
			{
				return this._isDownloadingContent;
			}
			set
			{
				if (value != this._isDownloadingContent)
				{
					this._isDownloadingContent = value;
					base.OnPropertyChangedWithValue(value, "IsDownloadingContent");
				}
			}
		}

		// Token: 0x04000293 RID: 659
		private MBBindingList<InitialMenuOptionVM> _menuOptions;

		// Token: 0x04000294 RID: 660
		private bool _isProfileSelectionEnabled;

		// Token: 0x04000295 RID: 661
		private bool _isDownloadingContent;

		// Token: 0x04000296 RID: 662
		private string _selectProfileText;

		// Token: 0x04000297 RID: 663
		private string _profileName;

		// Token: 0x04000298 RID: 664
		private string _downloadingText;

		// Token: 0x04000299 RID: 665
		private string _gameVersionText;
	}
}
