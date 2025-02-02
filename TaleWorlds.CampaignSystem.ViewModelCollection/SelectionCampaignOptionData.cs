using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000014 RID: 20
	public class SelectionCampaignOptionData : CampaignOptionData
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004139 File Offset: 0x00002339
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004141 File Offset: 0x00002341
		public List<TextObject> Selections { get; private set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x0000414C File Offset: 0x0000234C
		public SelectionCampaignOptionData(string identifier, int priorityIndex, CampaignOptionEnableState enableState, Func<float> getValue, Action<float> setValue, List<TextObject> customSelectionTexts = null, Func<CampaignOptionDisableStatus> getIsDisabledWithReason = null, bool isRelatedToDifficultyPreset = false, Func<float, CampaignOptionsDifficultyPresets> onGetDifficultyPresetFromValue = null, Func<CampaignOptionsDifficultyPresets, float> onGetValueFromDifficultyPreset = null) : base(identifier, priorityIndex, enableState, getValue, setValue, getIsDisabledWithReason, isRelatedToDifficultyPreset, onGetDifficultyPresetFromValue, onGetValueFromDifficultyPreset)
		{
			if (customSelectionTexts != null)
			{
				this.Selections = customSelectionTexts;
				return;
			}
			this.Selections = this.GetPresetTexts(identifier);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004188 File Offset: 0x00002388
		private List<TextObject> GetPresetTexts(string identifier)
		{
			List<TextObject> list = new List<TextObject>();
			foreach (object obj in Enum.GetValues(typeof(CampaignOptions.Difficulty)))
			{
				list.Add(GameTexts.FindText("str_campaign_options_type_" + identifier, obj.ToString()));
			}
			return list;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004204 File Offset: 0x00002404
		public override CampaignOptionDataType GetDataType()
		{
			return CampaignOptionDataType.Selection;
		}
	}
}
