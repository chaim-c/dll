using System;
using System.Collections.Generic;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x02000031 RID: 49
	public class GameplayCheatsVM : ViewModel
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00010F70 File Offset: 0x0000F170
		public GameplayCheatsVM(Action onClose, IEnumerable<GameplayCheatBase> cheats)
		{
			this._onClose = onClose;
			this._initialCheatList = cheats;
			this.Cheats = new MBBindingList<CheatItemBaseVM>();
			this._activeCheatGroups = new List<CheatGroupItemVM>();
			this._mainTitleText = new TextObject("{=OYtysXzk}Cheats", null);
			this.FillWithCheats(cheats);
			this.RefreshValues();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public override void RefreshValues()
		{
			base.RefreshValues();
			for (int i = 0; i < this.Cheats.Count; i++)
			{
				this.Cheats[i].RefreshValues();
			}
			if (this._activeCheatGroups.Count > 0)
			{
				TextObject textObject = new TextObject("{=1tiF5JhE}{TITLE} > {SUBTITLE}", null);
				for (int j = 0; j < this._activeCheatGroups.Count; j++)
				{
					if (j == 0)
					{
						textObject.SetTextVariable("TITLE", this._mainTitleText.ToString());
					}
					else
					{
						textObject.SetTextVariable("TITLE", textObject.ToString());
					}
					textObject.SetTextVariable("SUBTITLE", this._activeCheatGroups[j].Name.ToString());
				}
				this.Title = textObject.ToString();
				this.ButtonCloseLabel = GameTexts.FindText("str_back", null).ToString();
				return;
			}
			this.Title = this._mainTitleText.ToString();
			this.ButtonCloseLabel = GameTexts.FindText("str_close", null).ToString();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000110CF File Offset: 0x0000F2CF
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM closeInputKey = this.CloseInputKey;
			if (closeInputKey == null)
			{
				return;
			}
			closeInputKey.OnFinalize();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000110E8 File Offset: 0x0000F2E8
		private void FillWithCheats(IEnumerable<GameplayCheatBase> cheats)
		{
			this.Cheats.Clear();
			foreach (GameplayCheatBase gameplayCheatBase in cheats)
			{
				GameplayCheatItem cheat;
				GameplayCheatGroup cheatGroup;
				if ((cheat = (gameplayCheatBase as GameplayCheatItem)) != null)
				{
					this.Cheats.Add(new CheatActionItemVM(cheat, new Action<CheatActionItemVM>(this.OnCheatActionExecuted)));
				}
				else if ((cheatGroup = (gameplayCheatBase as GameplayCheatGroup)) != null)
				{
					this.Cheats.Add(new CheatGroupItemVM(cheatGroup, new Action<CheatGroupItemVM>(this.OnCheatGroupSelected)));
				}
			}
			this.RefreshValues();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001118C File Offset: 0x0000F38C
		private void OnCheatActionExecuted(CheatActionItemVM cheatItem)
		{
			this._activeCheatGroups.Clear();
			this.FillWithCheats(this._initialCheatList);
			TextObject textObject = new TextObject("{=1QAEyN2V}Cheat Used: {CHEAT}", null);
			textObject.SetTextVariable("CHEAT", cheatItem.Name.ToString());
			InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000111E1 File Offset: 0x0000F3E1
		private void OnCheatGroupSelected(CheatGroupItemVM cheatGroup)
		{
			this._activeCheatGroups.Add(cheatGroup);
			IEnumerable<GameplayCheatBase> enumerable;
			if (cheatGroup == null)
			{
				enumerable = null;
			}
			else
			{
				GameplayCheatGroup cheatGroup2 = cheatGroup.CheatGroup;
				enumerable = ((cheatGroup2 != null) ? cheatGroup2.GetCheats() : null);
			}
			this.FillWithCheats(enumerable ?? this._initialCheatList);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011218 File Offset: 0x0000F418
		public void ExecuteClose()
		{
			if (this._activeCheatGroups.Count > 0)
			{
				this._activeCheatGroups.RemoveAt(this._activeCheatGroups.Count - 1);
				if (this._activeCheatGroups.Count > 0)
				{
					this.FillWithCheats(this._activeCheatGroups[this._activeCheatGroups.Count - 1].CheatGroup.GetCheats());
					return;
				}
				this.FillWithCheats(this._initialCheatList);
				return;
			}
			else
			{
				Action onClose = this._onClose;
				if (onClose == null)
				{
					return;
				}
				onClose();
				return;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001129F File Offset: 0x0000F49F
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000112A7 File Offset: 0x0000F4A7
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000112CA File Offset: 0x0000F4CA
		// (set) Token: 0x0600039D RID: 925 RVA: 0x000112D2 File Offset: 0x0000F4D2
		[DataSourceProperty]
		public string ButtonCloseLabel
		{
			get
			{
				return this._buttonCloseLabel;
			}
			set
			{
				if (value != this._buttonCloseLabel)
				{
					this._buttonCloseLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ButtonCloseLabel");
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000112F5 File Offset: 0x0000F4F5
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000112FD File Offset: 0x0000F4FD
		[DataSourceProperty]
		public MBBindingList<CheatItemBaseVM> Cheats
		{
			get
			{
				return this._cheats;
			}
			set
			{
				if (value != this._cheats)
				{
					this._cheats = value;
					base.OnPropertyChangedWithValue<MBBindingList<CheatItemBaseVM>>(value, "Cheats");
				}
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001131B File Offset: 0x0000F51B
		public void SetCloseInputKey(HotKey hotKey)
		{
			this.CloseInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001132A File Offset: 0x0000F52A
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00011332 File Offset: 0x0000F532
		[DataSourceProperty]
		public InputKeyItemVM CloseInputKey
		{
			get
			{
				return this._closeInputKey;
			}
			set
			{
				if (value != this._closeInputKey)
				{
					this._closeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CloseInputKey");
				}
			}
		}

		// Token: 0x040001E1 RID: 481
		private readonly Action _onClose;

		// Token: 0x040001E2 RID: 482
		private readonly IEnumerable<GameplayCheatBase> _initialCheatList;

		// Token: 0x040001E3 RID: 483
		private readonly TextObject _mainTitleText;

		// Token: 0x040001E4 RID: 484
		private List<CheatGroupItemVM> _activeCheatGroups;

		// Token: 0x040001E5 RID: 485
		private string _title;

		// Token: 0x040001E6 RID: 486
		private string _buttonCloseLabel;

		// Token: 0x040001E7 RID: 487
		private MBBindingList<CheatItemBaseVM> _cheats;

		// Token: 0x040001E8 RID: 488
		private InputKeyItemVM _closeInputKey;
	}
}
