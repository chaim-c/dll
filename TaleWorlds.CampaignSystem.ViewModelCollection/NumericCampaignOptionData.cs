using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000013 RID: 19
	public class NumericCampaignOptionData : CampaignOptionData
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000040C9 File Offset: 0x000022C9
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000040D1 File Offset: 0x000022D1
		public float MinValue { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000040DA File Offset: 0x000022DA
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000040E2 File Offset: 0x000022E2
		public float MaxValue { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000040EB File Offset: 0x000022EB
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000040F3 File Offset: 0x000022F3
		public bool IsDiscrete { get; private set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x000040FC File Offset: 0x000022FC
		public NumericCampaignOptionData(string identifier, int priorityIndex, CampaignOptionEnableState enableState, Func<float> getValue, Action<float> setValue, float minValue, float maxValue, bool isDiscrete, Func<CampaignOptionDisableStatus> getIsDisabledWithReason = null, bool isRelatedToDifficultyPreset = false, Func<float, CampaignOptionsDifficultyPresets> onGetDifficultyPresetFromValue = null, Func<CampaignOptionsDifficultyPresets, float> onGetValueFromDifficultyPreset = null) : base(identifier, priorityIndex, enableState, getValue, setValue, getIsDisabledWithReason, isRelatedToDifficultyPreset, onGetDifficultyPresetFromValue, onGetValueFromDifficultyPreset)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.IsDiscrete = isDiscrete;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004136 File Offset: 0x00002336
		public override CampaignOptionDataType GetDataType()
		{
			return CampaignOptionDataType.Numeric;
		}
	}
}
