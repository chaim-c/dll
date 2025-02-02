using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200000E RID: 14
	public abstract class CampaignOptionData : ICampaignOptionData
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003D7C File Offset: 0x00001F7C
		public CampaignOptionData(string identifier, int priorityIndex, CampaignOptionEnableState enableState, Func<float> getValue, Action<float> setValue, Func<CampaignOptionDisableStatus> getIsDisabledWithReason = null, bool isRelatedToDifficultyPreset = false, Func<float, CampaignOptionsDifficultyPresets> onGetDifficultyPresetFromValue = null, Func<CampaignOptionsDifficultyPresets, float> onGetValueFromDifficultyPreset = null)
		{
			this._priorityIndex = priorityIndex;
			this._identifier = identifier;
			this._isRelatedToDifficultyPreset = isRelatedToDifficultyPreset;
			this._getIsDisabledWithReason = getIsDisabledWithReason;
			this._onGetDifficultyPresetFromValue = onGetDifficultyPresetFromValue;
			this._onGetValueFromDifficultyPreset = onGetValueFromDifficultyPreset;
			this._enableState = enableState;
			this._name = CampaignOptionData.GetNameOfOption(identifier);
			this._description = CampaignOptionData.GetDescriptionOfOption(identifier);
			this._getValue = getValue;
			this._setValue = setValue;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003DEC File Offset: 0x00001FEC
		public static TextObject GetNameOfOption(string optionIdentifier)
		{
			TextObject result;
			if (Input.IsGamepadActive && CampaignOptionData.CheckIsPlayStation() && GameTexts.TryGetText("str_campaign_options_type", out result, optionIdentifier + "_ps"))
			{
				return result;
			}
			return GameTexts.FindText("str_campaign_options_type", optionIdentifier);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E30 File Offset: 0x00002030
		public static TextObject GetDescriptionOfOption(string optionIdentifier)
		{
			TextObject result;
			if (Input.IsGamepadActive && CampaignOptionData.CheckIsPlayStation() && GameTexts.TryGetText("str_campaign_options_description", out result, optionIdentifier + "_ps"))
			{
				return result;
			}
			return GameTexts.FindText("str_campaign_options_description", optionIdentifier);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E71 File Offset: 0x00002071
		private static bool CheckIsPlayStation()
		{
			return Input.ControllerType.IsPlaystation();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E7D File Offset: 0x0000207D
		public int GetPriorityIndex()
		{
			return this._priorityIndex;
		}

		// Token: 0x060000A1 RID: 161
		public abstract CampaignOptionDataType GetDataType();

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E85 File Offset: 0x00002085
		public bool IsRelatedToDifficultyPreset()
		{
			return this._isRelatedToDifficultyPreset;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003E90 File Offset: 0x00002090
		public float GetValueFromDifficultyPreset(CampaignOptionsDifficultyPresets preset)
		{
			if (this._onGetValueFromDifficultyPreset != null)
			{
				return this._onGetValueFromDifficultyPreset(preset);
			}
			switch (preset)
			{
			case CampaignOptionsDifficultyPresets.Freebooter:
				return 0f;
			case CampaignOptionsDifficultyPresets.Warrior:
				return 1f;
			case CampaignOptionsDifficultyPresets.Bannerlord:
				return 2f;
			default:
				return 0f;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003EE0 File Offset: 0x000020E0
		public CampaignOptionDisableStatus GetIsDisabledWithReason()
		{
			Func<CampaignOptionDisableStatus> getIsDisabledWithReason = this._getIsDisabledWithReason;
			CampaignOptionDisableStatus? campaignOptionDisableStatus = (getIsDisabledWithReason != null) ? new CampaignOptionDisableStatus?(getIsDisabledWithReason()) : null;
			bool isDisabled = false;
			string text = string.Empty;
			float valueIfDisabled = -1f;
			if (this._enableState == CampaignOptionEnableState.Disabled)
			{
				isDisabled = true;
				text = GameTexts.FindText("str_campaign_options_disabled_reason", this._identifier).ToString();
			}
			else if (this._enableState == CampaignOptionEnableState.DisabledLater)
			{
				text = GameTexts.FindText("str_campaign_options_persistency_warning", null).ToString();
			}
			if (campaignOptionDisableStatus != null && campaignOptionDisableStatus.Value.IsDisabled)
			{
				isDisabled = true;
				if (!string.IsNullOrEmpty(campaignOptionDisableStatus.Value.DisabledReason))
				{
					if (!string.IsNullOrEmpty(text))
					{
						TextObject textObject = GameTexts.FindText("str_string_newline_string", null).CopyTextObject();
						textObject.SetTextVariable("STR1", text);
						textObject.SetTextVariable("STR2", campaignOptionDisableStatus.Value.DisabledReason);
						text = textObject.ToString();
					}
					else
					{
						text = campaignOptionDisableStatus.Value.DisabledReason;
					}
				}
				valueIfDisabled = campaignOptionDisableStatus.Value.ValueIfDisabled;
			}
			return new CampaignOptionDisableStatus(isDisabled, text, valueIfDisabled);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004006 File Offset: 0x00002206
		public string GetIdentifier()
		{
			return this._identifier;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000400E File Offset: 0x0000220E
		public CampaignOptionEnableState GetEnableState()
		{
			return this._enableState;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004016 File Offset: 0x00002216
		public string GetName()
		{
			return this._name.ToString();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004023 File Offset: 0x00002223
		public string GetDescription()
		{
			return this._description.ToString();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004030 File Offset: 0x00002230
		public float GetValue()
		{
			Func<float> getValue = this._getValue;
			if (getValue == null)
			{
				return 0f;
			}
			return getValue();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004047 File Offset: 0x00002247
		public void SetValue(float value)
		{
			Action<float> setValue = this._setValue;
			if (setValue == null)
			{
				return;
			}
			setValue(value);
		}

		// Token: 0x04000057 RID: 87
		private int _priorityIndex;

		// Token: 0x04000058 RID: 88
		private string _identifier;

		// Token: 0x04000059 RID: 89
		private bool _isRelatedToDifficultyPreset;

		// Token: 0x0400005A RID: 90
		private CampaignOptionEnableState _enableState;

		// Token: 0x0400005B RID: 91
		private TextObject _name;

		// Token: 0x0400005C RID: 92
		private TextObject _description;

		// Token: 0x0400005D RID: 93
		private Func<CampaignOptionDisableStatus> _getIsDisabledWithReason;

		// Token: 0x0400005E RID: 94
		protected Func<float> _getValue;

		// Token: 0x0400005F RID: 95
		protected Action<float> _setValue;

		// Token: 0x04000060 RID: 96
		protected Func<float, CampaignOptionsDifficultyPresets> _onGetDifficultyPresetFromValue;

		// Token: 0x04000061 RID: 97
		protected Func<CampaignOptionsDifficultyPresets, float> _onGetValueFromDifficultyPreset;
	}
}
