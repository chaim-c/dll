using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000007 RID: 7
	public class CampaignOptionsVM : ViewModel
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003378 File Offset: 0x00001578
		public CampaignOptionsVM(Action onClose)
		{
			this._onClose = onClose;
			MBBindingList<CampaignOptionItemVM> mbbindingList = new MBBindingList<CampaignOptionItemVM>();
			List<ICampaignOptionData> gameplayCampaignOptions = CampaignOptionsManager.GetGameplayCampaignOptions();
			for (int i = 0; i < gameplayCampaignOptions.Count; i++)
			{
				mbbindingList.Add(new CampaignOptionItemVM(gameplayCampaignOptions[i]));
			}
			this.OptionsController = new CampaignOptionsControllerVM(mbbindingList);
			this.RefreshValues();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000033D4 File Offset: 0x000015D4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = new TextObject("{=PXT6aA4J}Campaign Options", null).ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.ResetTutorialText = new TextObject("{=oUz16Nav}Reset Tutorial", null).ToString();
			this.OptionsController.RefreshValues();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003434 File Offset: 0x00001634
		public void ExecuteDone()
		{
			Action onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003446 File Offset: 0x00001646
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000344E File Offset: 0x0000164E
		[DataSourceProperty]
		public CampaignOptionsControllerVM OptionsController
		{
			get
			{
				return this._optionsController;
			}
			set
			{
				if (value != this._optionsController)
				{
					this._optionsController = value;
					base.OnPropertyChangedWithValue<CampaignOptionsControllerVM>(value, "OptionsController");
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000346C File Offset: 0x0000166C
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003474 File Offset: 0x00001674
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003497 File Offset: 0x00001697
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000349F File Offset: 0x0000169F
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000034C2 File Offset: 0x000016C2
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000034CA File Offset: 0x000016CA
		[DataSourceProperty]
		public string ResetTutorialText
		{
			get
			{
				return this._resetTutorialText;
			}
			set
			{
				if (value != this._resetTutorialText)
				{
					this._resetTutorialText = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetTutorialText");
				}
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly Action _onClose;

		// Token: 0x04000044 RID: 68
		private string _titleText;

		// Token: 0x04000045 RID: 69
		private string _doneText;

		// Token: 0x04000046 RID: 70
		private string _resetTutorialText;

		// Token: 0x04000047 RID: 71
		private CampaignOptionsControllerVM _optionsController;
	}
}
