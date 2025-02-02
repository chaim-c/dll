using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000012 RID: 18
	public class BooleanCampaignOptionData : CampaignOptionData
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000040A4 File Offset: 0x000022A4
		public BooleanCampaignOptionData(string identifier, int priorityIndex, CampaignOptionEnableState enableState, Func<float> getValue, Action<float> setValue, Func<CampaignOptionDisableStatus> getIsDisabledWithReason = null, bool isRelatedToDifficultyPreset = false, Func<float, CampaignOptionsDifficultyPresets> onGetDifficultyPresetFromValue = null, Func<CampaignOptionsDifficultyPresets, float> onGetValueFromDifficultyPreset = null) : base(identifier, priorityIndex, enableState, getValue, setValue, getIsDisabledWithReason, isRelatedToDifficultyPreset, onGetDifficultyPresetFromValue, onGetValueFromDifficultyPreset)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040C6 File Offset: 0x000022C6
		public override CampaignOptionDataType GetDataType()
		{
			return CampaignOptionDataType.Boolean;
		}
	}
}
