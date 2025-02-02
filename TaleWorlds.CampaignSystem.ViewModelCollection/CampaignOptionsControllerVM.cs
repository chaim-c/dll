using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000008 RID: 8
	public class CampaignOptionsControllerVM : ViewModel
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000034F0 File Offset: 0x000016F0
		public CampaignOptionsControllerVM(MBBindingList<CampaignOptionItemVM> options)
		{
			this._optionItems = new Dictionary<string, CampaignOptionItemVM>();
			this.Options = options;
			CampaignOptionItemVM campaignOptionItemVM = this.Options.FirstOrDefault((CampaignOptionItemVM x) => x.OptionData.GetIdentifier() == "DifficultyPresets");
			this._difficultyPreset = (((campaignOptionItemVM != null) ? campaignOptionItemVM.OptionData : null) as SelectionCampaignOptionData);
			this.Options.Sort(new CampaignOptionsControllerVM.CampaignOptionComparer());
			for (int i = 0; i < this.Options.Count; i++)
			{
				this._optionItems.Add(this.Options[i].OptionData.GetIdentifier(), this.Options[i]);
			}
			this.Options.ApplyActionOnAllItems(delegate(CampaignOptionItemVM x)
			{
				x.RefreshDisabledStatus();
			});
			this.Options.ApplyActionOnAllItems(delegate(CampaignOptionItemVM x)
			{
				x.SetOnValueChangedCallback(new Action<CampaignOptionItemVM>(this.OnOptionChanged));
			});
			this._difficultyPresetRelatedOptions = (from x in this.Options
			where x.OptionData.IsRelatedToDifficultyPreset()
			select x).ToList<CampaignOptionItemVM>();
			this.UpdatePresetData(this._difficultyPresetRelatedOptions.FirstOrDefault<CampaignOptionItemVM>());
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003630 File Offset: 0x00001830
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignOptionsManager.ClearCachedOptions();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000363D File Offset: 0x0000183D
		private void OnOptionChanged(CampaignOptionItemVM optionVM)
		{
			this.UpdatePresetData(optionVM);
			this.Options.ApplyActionOnAllItems(delegate(CampaignOptionItemVM x)
			{
				x.RefreshDisabledStatus();
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003670 File Offset: 0x00001870
		private void UpdatePresetData(CampaignOptionItemVM changedOption)
		{
			if (this._isUpdatingPresetData || changedOption == null)
			{
				return;
			}
			CampaignOptionItemVM campaignOptionItemVM;
			if (!this._optionItems.TryGetValue(this._difficultyPreset.GetIdentifier(), out campaignOptionItemVM))
			{
				return;
			}
			this._isUpdatingPresetData = true;
			if (changedOption.OptionData == this._difficultyPreset)
			{
				using (List<CampaignOptionItemVM>.Enumerator enumerator = this._difficultyPresetRelatedOptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CampaignOptionItemVM campaignOptionItemVM2 = enumerator.Current;
						string identifier = campaignOptionItemVM2.OptionData.GetIdentifier();
						CampaignOptionsDifficultyPresets preset = (CampaignOptionsDifficultyPresets)this._difficultyPreset.GetValue();
						float valueFromDifficultyPreset = campaignOptionItemVM2.OptionData.GetValueFromDifficultyPreset(preset);
						CampaignOptionItemVM campaignOptionItemVM3;
						if (this._optionItems.TryGetValue(identifier, out campaignOptionItemVM3) && !campaignOptionItemVM3.IsDisabled)
						{
							campaignOptionItemVM3.SetValue(valueFromDifficultyPreset);
						}
					}
					goto IL_156;
				}
			}
			if (this._difficultyPresetRelatedOptions.Any((CampaignOptionItemVM x) => x.OptionData.GetIdentifier() == changedOption.OptionData.GetIdentifier()))
			{
				CampaignOptionItemVM campaignOptionItemVM4 = this._difficultyPresetRelatedOptions[0];
				CampaignOptionsDifficultyPresets campaignOptionsDifficultyPresets = this.FindOptionPresetForValue(campaignOptionItemVM4.OptionData);
				bool flag = true;
				for (int i = 0; i < this._difficultyPresetRelatedOptions.Count; i++)
				{
					if (this.FindOptionPresetForValue(this._difficultyPresetRelatedOptions[i].OptionData) != campaignOptionsDifficultyPresets)
					{
						flag = false;
						break;
					}
				}
				campaignOptionItemVM.SetValue(flag ? ((float)campaignOptionsDifficultyPresets) : 3f);
			}
			IL_156:
			this._isUpdatingPresetData = false;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000037EC File Offset: 0x000019EC
		private CampaignOptionsDifficultyPresets FindOptionPresetForValue(ICampaignOptionData option)
		{
			float value = option.GetValue();
			if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Freebooter) == value)
			{
				return CampaignOptionsDifficultyPresets.Freebooter;
			}
			if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Warrior) == value)
			{
				return CampaignOptionsDifficultyPresets.Warrior;
			}
			if (option.GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets.Bannerlord) == value)
			{
				return CampaignOptionsDifficultyPresets.Bannerlord;
			}
			return CampaignOptionsDifficultyPresets.Custom;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003825 File Offset: 0x00001A25
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000382D File Offset: 0x00001A2D
		[DataSourceProperty]
		public MBBindingList<CampaignOptionItemVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChangedWithValue<MBBindingList<CampaignOptionItemVM>>(value, "Options");
				}
			}
		}

		// Token: 0x04000048 RID: 72
		private const string _difficultyPresetsId = "DifficultyPresets";

		// Token: 0x04000049 RID: 73
		internal const int AutosaveDisableValue = -1;

		// Token: 0x0400004A RID: 74
		private SelectionCampaignOptionData _difficultyPreset;

		// Token: 0x0400004B RID: 75
		private Dictionary<string, CampaignOptionItemVM> _optionItems;

		// Token: 0x0400004C RID: 76
		private bool _isUpdatingPresetData;

		// Token: 0x0400004D RID: 77
		private List<CampaignOptionItemVM> _difficultyPresetRelatedOptions;

		// Token: 0x0400004E RID: 78
		private MBBindingList<CampaignOptionItemVM> _options;

		// Token: 0x02000143 RID: 323
		private class CampaignOptionComparer : IComparer<CampaignOptionItemVM>
		{
			// Token: 0x06001F8E RID: 8078 RVA: 0x00070340 File Offset: 0x0006E540
			public int Compare(CampaignOptionItemVM x, CampaignOptionItemVM y)
			{
				int priorityIndex = x.OptionData.GetPriorityIndex();
				int priorityIndex2 = y.OptionData.GetPriorityIndex();
				return priorityIndex.CompareTo(priorityIndex2);
			}
		}
	}
}
