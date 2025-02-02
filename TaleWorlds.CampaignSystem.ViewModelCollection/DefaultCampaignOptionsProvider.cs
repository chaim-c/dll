using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200000C RID: 12
	public class DefaultCampaignOptionsProvider : ICampaignOptionProvider
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003A66 File Offset: 0x00001C66
		public IEnumerable<ICampaignOptionData> GetGameplayCampaignOptions()
		{
			yield return new SelectionCampaignOptionData("DifficultyPresets", 100, CampaignOptionEnableState.Enabled, () => (float)this._difficultyPreset, delegate(float value)
			{
				this._difficultyPreset = (CampaignOptionsDifficultyPresets)value;
			}, this.GetPresetTexts("DifficultyPresets"), null, false, null, null);
			IEnumerable<ICampaignOptionData> difficultyRelatedOptions = this.GetDifficultyRelatedOptions();
			foreach (ICampaignOptionData campaignOptionData in difficultyRelatedOptions)
			{
				yield return campaignOptionData;
			}
			IEnumerator<ICampaignOptionData> enumerator = null;
			yield return new BooleanCampaignOptionData("AutoAllocateClanMemberPerks", 1000, CampaignOptionEnableState.Enabled, delegate()
			{
				if (!CampaignOptions.AutoAllocateClanMemberPerks)
				{
					return 0f;
				}
				return 1f;
			}, delegate(float value)
			{
				CampaignOptions.AutoAllocateClanMemberPerks = (value == 1f);
			}, null, false, null, null);
			yield return new BooleanCampaignOptionData("IronmanMode", 1100, CampaignOptionEnableState.Disabled, delegate()
			{
				if (!CampaignOptions.IsIronmanMode)
				{
					return 0f;
				}
				return 1f;
			}, delegate(float value)
			{
				CampaignOptions.IsIronmanMode = (value == 1f);
			}, null, false, null, null);
			yield return new ActionCampaignOptionData("ResetTutorial", 10000, CampaignOptionEnableState.Enabled, new Action(this.ExecuteResetTutorial), null);
			if (Input.IsGamepadActive)
			{
				yield return new ActionCampaignOptionData("EnableCheats", 11000, CampaignOptionEnableState.Enabled, new Action(this.ExecuteEnableCheats), null);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003A76 File Offset: 0x00001C76
		public IEnumerable<ICampaignOptionData> GetCharacterCreationCampaignOptions()
		{
			IEnumerable<ICampaignOptionData> difficultyOptions = this.GetDifficultyRelatedOptions();
			yield return new SelectionCampaignOptionData("DifficultyPresets", 100, CampaignOptionEnableState.Enabled, () => (float)this._difficultyPreset, delegate(float value)
			{
				this._difficultyPreset = (CampaignOptionsDifficultyPresets)value;
			}, this.GetPresetTexts("DifficultyPresets"), null, false, null, null);
			foreach (ICampaignOptionData campaignOptionData in difficultyOptions)
			{
				yield return campaignOptionData;
			}
			IEnumerator<ICampaignOptionData> enumerator = null;
			yield return new BooleanCampaignOptionData("AutoAllocateClanMemberPerks", 1000, CampaignOptionEnableState.Enabled, delegate()
			{
				if (!CampaignOptions.AutoAllocateClanMemberPerks)
				{
					return 0f;
				}
				return 1f;
			}, delegate(float value)
			{
				CampaignOptions.AutoAllocateClanMemberPerks = (value == 1f);
			}, null, false, null, null);
			yield return new BooleanCampaignOptionData("IronmanMode", 1100, CampaignOptionEnableState.DisabledLater, delegate()
			{
				if (!CampaignOptions.IsIronmanMode)
				{
					return 0f;
				}
				return 1f;
			}, delegate(float value)
			{
				CampaignOptions.IsIronmanMode = (value == 1f);
			}, null, false, null, null);
			yield break;
			yield break;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003A86 File Offset: 0x00001C86
		private IEnumerable<ICampaignOptionData> GetDifficultyRelatedOptions()
		{
			yield return new SelectionCampaignOptionData("PlayerReceivedDamage", 200, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.PlayerReceivedDamage, delegate(float value)
			{
				CampaignOptions.PlayerReceivedDamage = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("PlayerTroopsReceivedDamage", 300, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.PlayerTroopsReceivedDamage, delegate(float value)
			{
				CampaignOptions.PlayerTroopsReceivedDamage = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("MaximumIndexPlayerCanRecruit", 400, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.RecruitmentDifficulty, delegate(float value)
			{
				CampaignOptions.RecruitmentDifficulty = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("PlayerMapMovementSpeed", 500, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.PlayerMapMovementSpeed, delegate(float value)
			{
				CampaignOptions.PlayerMapMovementSpeed = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("PersuasionSuccess", 600, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.PersuasionSuccessChance, delegate(float value)
			{
				CampaignOptions.PersuasionSuccessChance = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("CombatAIDifficulty", 700, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.CombatAIDifficulty, delegate(float value)
			{
				CampaignOptions.CombatAIDifficulty = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("ClanMemberBattleDeathPossibility", 800, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.ClanMemberDeathChance, delegate(float value)
			{
				CampaignOptions.ClanMemberDeathChance = (CampaignOptions.Difficulty)value;
			}, null, null, true, null, null);
			yield return new SelectionCampaignOptionData("BattleDeath", 900, CampaignOptionEnableState.Enabled, () => (float)CampaignOptions.BattleDeath, delegate(float value)
			{
				CampaignOptions.BattleDeath = (CampaignOptions.Difficulty)value;
			}, null, new Func<CampaignOptionDisableStatus>(this.GetBattleDeathDisabledStatusWithReason), true, null, null);
			yield break;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003A98 File Offset: 0x00001C98
		private CampaignOptionsDifficultyPresets GetDefaultPresetForValue(float value)
		{
			switch ((int)value)
			{
			case 0:
				return CampaignOptionsDifficultyPresets.Freebooter;
			case 1:
				return CampaignOptionsDifficultyPresets.Warrior;
			case 2:
				return CampaignOptionsDifficultyPresets.Bannerlord;
			default:
				return CampaignOptionsDifficultyPresets.Custom;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003AC3 File Offset: 0x00001CC3
		private float GetDefaultValueForDifficultyPreset(CampaignOptionsDifficultyPresets preset)
		{
			switch (preset)
			{
			case CampaignOptionsDifficultyPresets.Freebooter:
				return 0f;
			case CampaignOptionsDifficultyPresets.Warrior:
				return 1f;
			case CampaignOptionsDifficultyPresets.Bannerlord:
				return 2f;
			default:
				return -1f;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003AF0 File Offset: 0x00001CF0
		private CampaignOptionDisableStatus GetBattleDeathDisabledStatusWithReason()
		{
			if (CampaignOptions.IsLifeDeathCycleDisabled)
			{
				TextObject variable = GameTexts.FindText("str_campaign_options_type", "IsLifeDeathCycleEnabled");
				TextObject textObject = GameTexts.FindText("str_campaign_options_dependency_warning", null);
				textObject.SetTextVariable("OPTION", variable);
				if (!CampaignOptionsManager.GetOptionWithIdExists("IsLifeDeathCycleEnabled"))
				{
					string variable2 = textObject.ToString();
					TextObject textObject2 = new TextObject("{=K87pubLc}The option \"{DEPENDENT_OPTION}\" can be enabled by activating \"{MODULE_NAME}\" module.", null);
					textObject2.SetTextVariable("DEPENDENT_OPTION", variable);
					textObject2.SetTextVariable("MODULE_NAME", "Birth and Death Options");
					textObject = GameTexts.FindText("str_string_newline_string", null).CopyTextObject();
					textObject.SetTextVariable("STR1", variable2);
					textObject.SetTextVariable("STR2", textObject2.ToString());
				}
				return new CampaignOptionDisableStatus(true, textObject.ToString(), 0f);
			}
			return new CampaignOptionDisableStatus(false, string.Empty, 0f);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private List<TextObject> GetPresetTexts(string identifier)
		{
			List<TextObject> list = new List<TextObject>();
			foreach (object obj in Enum.GetValues(typeof(CampaignOptionsDifficultyPresets)))
			{
				list.Add(GameTexts.FindText("str_campaign_options_type_" + identifier, obj.ToString()));
			}
			return list;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C3C File Offset: 0x00001E3C
		private void ExecuteResetTutorial()
		{
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=a4GDfSel}Reset Tutorials", null).ToString(), new TextObject("{=I2sZ7K28}Are you sure want to reset tutorials?", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.ResetTutorials), null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003CB4 File Offset: 0x00001EB4
		private void ExecuteEnableCheats()
		{
			MapState mapState;
			if ((mapState = (GameStateManager.Current.ActiveState as MapState)) != null)
			{
				mapState.Handler.OnGameplayCheatsEnabled();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003CDF File Offset: 0x00001EDF
		private void ResetTutorials()
		{
			Game.Current.EventManager.TriggerEvent<ResetAllTutorialsEvent>(new ResetAllTutorialsEvent());
			InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=Iefr8Fra}Tutorials have been reset.", null).ToString()));
		}

		// Token: 0x04000055 RID: 85
		private CampaignOptionsDifficultyPresets _difficultyPreset;
	}
}
