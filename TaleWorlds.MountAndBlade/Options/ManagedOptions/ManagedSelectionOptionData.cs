using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Options.ManagedOptions
{
	// Token: 0x02000385 RID: 901
	public class ManagedSelectionOptionData : ManagedOptionData, ISelectionOptionData, IOptionData
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x000CC15C File Offset: 0x000CA35C
		public ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType type) : base(type)
		{
			this._selectableOptionsLimit = ManagedSelectionOptionData.GetOptionsLimit(type);
			this._selectableOptionNames = ManagedSelectionOptionData.GetOptionNames(type);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000CC17D File Offset: 0x000CA37D
		public int GetSelectableOptionsLimit()
		{
			return this._selectableOptionsLimit;
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000CC185 File Offset: 0x000CA385
		public IEnumerable<SelectionData> GetSelectableOptionNames()
		{
			return this._selectableOptionNames;
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000CC190 File Offset: 0x000CA390
		public static int GetOptionsLimit(ManagedOptions.ManagedOptionsType optionType)
		{
			if (optionType <= ManagedOptions.ManagedOptionsType.ReportCasualtiesType)
			{
				switch (optionType)
				{
				case ManagedOptions.ManagedOptionsType.Language:
					return LocalizedTextManager.GetLanguageIds(NativeConfig.IsDevelopmentMode).Count;
				case ManagedOptions.ManagedOptionsType.GyroOverrideForAttackDefend:
					break;
				case ManagedOptions.ManagedOptionsType.ControlBlockDirection:
					return 3;
				case ManagedOptions.ManagedOptionsType.ControlAttackDirection:
					return 3;
				case ManagedOptions.ManagedOptionsType.NumberOfCorpses:
					return 6;
				case ManagedOptions.ManagedOptionsType.BattleSize:
					return 7;
				case ManagedOptions.ManagedOptionsType.ReinforcementWaveCount:
					return 4;
				case ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson:
					return 4;
				default:
					if (optionType == ManagedOptions.ManagedOptionsType.ReportCasualtiesType)
					{
						return 3;
					}
					break;
				}
			}
			else
			{
				switch (optionType)
				{
				case ManagedOptions.ManagedOptionsType.CrosshairType:
					return 2;
				case ManagedOptions.ManagedOptionsType.EnableGenericAvatars:
				case ManagedOptions.ManagedOptionsType.EnableGenericNames:
					break;
				case ManagedOptions.ManagedOptionsType.OrderType:
					return 2;
				case ManagedOptions.ManagedOptionsType.OrderLayoutType:
					return 2;
				case ManagedOptions.ManagedOptionsType.AutoTrackAttackedSettlements:
					return 3;
				default:
					if (optionType == ManagedOptions.ManagedOptionsType.UnitSpawnPrioritization)
					{
						return 4;
					}
					if (optionType == ManagedOptions.ManagedOptionsType.VoiceLanguage)
					{
						return LocalizedVoiceManager.GetVoiceLanguageIds().Count;
					}
					break;
				}
			}
			return 0;
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000CC230 File Offset: 0x000CA430
		private static IEnumerable<SelectionData> GetOptionNames(ManagedOptions.ManagedOptionsType type)
		{
			if (type == ManagedOptions.ManagedOptionsType.Language)
			{
				List<string> languageIds = LocalizedTextManager.GetLanguageIds(NativeConfig.IsDevelopmentMode);
				int num;
				for (int i = 0; i < languageIds.Count; i = num + 1)
				{
					yield return new SelectionData(false, LocalizedTextManager.GetLanguageTitle(languageIds[i]));
					num = i;
				}
				languageIds = null;
			}
			else if (type == ManagedOptions.ManagedOptionsType.VoiceLanguage)
			{
				List<string> languageIds = LocalizedVoiceManager.GetVoiceLanguageIds();
				int num;
				for (int i = 0; i < languageIds.Count; i = num + 1)
				{
					yield return new SelectionData(false, LocalizedTextManager.GetLanguageTitle(languageIds[i]));
					num = i;
				}
				languageIds = null;
			}
			else
			{
				int i = ManagedSelectionOptionData.GetOptionsLimit(type);
				string typeName = type.ToString();
				int num;
				for (int j = 0; j < i; j = num + 1)
				{
					yield return new SelectionData(true, "str_options_type_" + typeName + "_" + j.ToString());
					num = j;
				}
				typeName = null;
			}
			yield break;
		}

		// Token: 0x04001528 RID: 5416
		private readonly int _selectableOptionsLimit;

		// Token: 0x04001529 RID: 5417
		private readonly IEnumerable<SelectionData> _selectableOptionNames;
	}
}
