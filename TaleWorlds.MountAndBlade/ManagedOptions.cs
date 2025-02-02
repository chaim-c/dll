using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000371 RID: 881
	public static class ManagedOptions
	{
		// Token: 0x06003089 RID: 12425 RVA: 0x000C8E74 File Offset: 0x000C7074
		public static float GetConfig(ManagedOptions.ManagedOptionsType type)
		{
			switch (type)
			{
			case ManagedOptions.ManagedOptionsType.Language:
				return (float)LocalizedTextManager.GetLanguageIds(NativeConfig.IsDevelopmentMode).IndexOf(BannerlordConfig.Language);
			case ManagedOptions.ManagedOptionsType.GyroOverrideForAttackDefend:
				return (float)(BannerlordConfig.GyroOverrideForAttackDefend ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ControlBlockDirection:
				return (float)BannerlordConfig.DefendDirectionControl;
			case ManagedOptions.ManagedOptionsType.ControlAttackDirection:
				return (float)BannerlordConfig.AttackDirectionControl;
			case ManagedOptions.ManagedOptionsType.NumberOfCorpses:
				return (float)BannerlordConfig.NumberOfCorpses;
			case ManagedOptions.ManagedOptionsType.BattleSize:
				return (float)BannerlordConfig.BattleSize;
			case ManagedOptions.ManagedOptionsType.ReinforcementWaveCount:
				return (float)BannerlordConfig.ReinforcementWaveCount;
			case ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson:
				return (float)BannerlordConfig.TurnCameraWithHorseInFirstPerson;
			case ManagedOptions.ManagedOptionsType.ShowBlood:
				return (float)(BannerlordConfig.ShowBlood ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ShowAttackDirection:
				return (float)(BannerlordConfig.DisplayAttackDirection ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ShowTargetingReticle:
				return (float)(BannerlordConfig.DisplayTargetingReticule ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.AutoSaveInterval:
				return (float)BannerlordConfig.AutoSaveInterval;
			case ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity:
				return BannerlordConfig.FriendlyTroopsBannerOpacity;
			case ManagedOptions.ManagedOptionsType.ReportDamage:
				return (float)(BannerlordConfig.ReportDamage ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ReportBark:
				return (float)(BannerlordConfig.ReportBark ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.LockTarget:
				return (float)(BannerlordConfig.LockTarget ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableTutorialHints:
				return (float)(BannerlordConfig.EnableTutorialHints ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ReportCasualtiesType:
				return (float)BannerlordConfig.ReportCasualtiesType;
			case ManagedOptions.ManagedOptionsType.ReportExperience:
				return (float)(BannerlordConfig.ReportExperience ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ReportPersonalDamage:
				return (float)(BannerlordConfig.ReportPersonalDamage ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.FirstPersonFov:
				return BannerlordConfig.FirstPersonFov;
			case ManagedOptions.ManagedOptionsType.CombatCameraDistance:
				return BannerlordConfig.CombatCameraDistance;
			case ManagedOptions.ManagedOptionsType.EnableDamageTakenVisuals:
				return (float)(BannerlordConfig.EnableDamageTakenVisuals ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableVoiceChat:
				return (float)(BannerlordConfig.EnableVoiceChat ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableDeathIcon:
				return (float)(BannerlordConfig.EnableDeathIcon ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableNetworkAlertIcons:
				return (float)(BannerlordConfig.EnableNetworkAlertIcons ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ForceVSyncInMenus:
				return (float)(BannerlordConfig.ForceVSyncInMenus ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableVerticalAimCorrection:
				return (float)(BannerlordConfig.EnableVerticalAimCorrection ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.ZoomSensitivityModifier:
				return BannerlordConfig.ZoomSensitivityModifier;
			case ManagedOptions.ManagedOptionsType.UIScale:
				return BannerlordConfig.UIScale;
			case ManagedOptions.ManagedOptionsType.CrosshairType:
				return (float)BannerlordConfig.CrosshairType;
			case ManagedOptions.ManagedOptionsType.EnableGenericAvatars:
				return (float)(BannerlordConfig.EnableGenericAvatars ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableGenericNames:
				return (float)(BannerlordConfig.EnableGenericNames ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.OrderType:
				return (float)BannerlordConfig.OrderType;
			case ManagedOptions.ManagedOptionsType.OrderLayoutType:
				return (float)BannerlordConfig.OrderLayoutType;
			case ManagedOptions.ManagedOptionsType.AutoTrackAttackedSettlements:
				return (float)BannerlordConfig.AutoTrackAttackedSettlements;
			case ManagedOptions.ManagedOptionsType.StopGameOnFocusLost:
				return (float)(BannerlordConfig.StopGameOnFocusLost ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.SlowDownOnOrder:
				return (float)(BannerlordConfig.SlowDownOnOrder ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HideFullServers:
				return (float)(BannerlordConfig.HideFullServers ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HideEmptyServers:
				return (float)(BannerlordConfig.HideEmptyServers ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HidePasswordProtectedServers:
				return (float)(BannerlordConfig.HidePasswordProtectedServers ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HideUnofficialServers:
				return (float)(BannerlordConfig.HideUnofficialServers ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HideModuleIncompatibleServers:
				return (float)(BannerlordConfig.HideModuleIncompatibleServers ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.HideBattleUI:
				return (float)(BannerlordConfig.HideBattleUI ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.UnitSpawnPrioritization:
				return (float)BannerlordConfig.UnitSpawnPrioritization;
			case ManagedOptions.ManagedOptionsType.EnableSingleplayerChatBox:
				return (float)(BannerlordConfig.EnableSingleplayerChatBox ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.EnableMultiplayerChatBox:
				return (float)(BannerlordConfig.EnableMultiplayerChatBox ? 1 : 0);
			case ManagedOptions.ManagedOptionsType.VoiceLanguage:
				return (float)LocalizedVoiceManager.GetVoiceLanguageIds().IndexOf(BannerlordConfig.VoiceLanguage);
			default:
				Debug.FailedAssert("ManagedOptionsType not found", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Options\\ManagedOptions\\ManagedOptions.cs", "GetConfig", 168);
				return 0f;
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000C9178 File Offset: 0x000C7378
		public static float GetDefaultConfig(ManagedOptions.ManagedOptionsType type)
		{
			switch (type)
			{
			case ManagedOptions.ManagedOptionsType.Language:
				return (float)LocalizedTextManager.GetLanguageIds(NativeConfig.IsDevelopmentMode).IndexOf(BannerlordConfig.DefaultLanguage);
			case ManagedOptions.ManagedOptionsType.GyroOverrideForAttackDefend:
				return 0f;
			case ManagedOptions.ManagedOptionsType.ControlBlockDirection:
				return 0f;
			case ManagedOptions.ManagedOptionsType.ControlAttackDirection:
				return 1f;
			case ManagedOptions.ManagedOptionsType.NumberOfCorpses:
				return 3f;
			case ManagedOptions.ManagedOptionsType.BattleSize:
				return 2f;
			case ManagedOptions.ManagedOptionsType.ReinforcementWaveCount:
				return 3f;
			case ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson:
				return 2f;
			case ManagedOptions.ManagedOptionsType.ShowBlood:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ShowAttackDirection:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ShowTargetingReticle:
				return 1f;
			case ManagedOptions.ManagedOptionsType.AutoSaveInterval:
				return 30f;
			case ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ReportDamage:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ReportBark:
				return 1f;
			case ManagedOptions.ManagedOptionsType.LockTarget:
				return 0f;
			case ManagedOptions.ManagedOptionsType.EnableTutorialHints:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ReportCasualtiesType:
				return 0f;
			case ManagedOptions.ManagedOptionsType.ReportExperience:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ReportPersonalDamage:
				return 1f;
			case ManagedOptions.ManagedOptionsType.FirstPersonFov:
				return 65f;
			case ManagedOptions.ManagedOptionsType.CombatCameraDistance:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableDamageTakenVisuals:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableVoiceChat:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableDeathIcon:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableNetworkAlertIcons:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ForceVSyncInMenus:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableVerticalAimCorrection:
				return 1f;
			case ManagedOptions.ManagedOptionsType.ZoomSensitivityModifier:
				return 0.66666f;
			case ManagedOptions.ManagedOptionsType.UIScale:
				return 1f;
			case ManagedOptions.ManagedOptionsType.CrosshairType:
				return 0f;
			case ManagedOptions.ManagedOptionsType.EnableGenericAvatars:
				return 0f;
			case ManagedOptions.ManagedOptionsType.EnableGenericNames:
				return 0f;
			case ManagedOptions.ManagedOptionsType.OrderType:
				return 0f;
			case ManagedOptions.ManagedOptionsType.OrderLayoutType:
				return 0f;
			case ManagedOptions.ManagedOptionsType.AutoTrackAttackedSettlements:
				return 0f;
			case ManagedOptions.ManagedOptionsType.StopGameOnFocusLost:
				return 1f;
			case ManagedOptions.ManagedOptionsType.SlowDownOnOrder:
				return 1f;
			case ManagedOptions.ManagedOptionsType.HideFullServers:
				return 0f;
			case ManagedOptions.ManagedOptionsType.HideEmptyServers:
				return 0f;
			case ManagedOptions.ManagedOptionsType.HidePasswordProtectedServers:
				return 0f;
			case ManagedOptions.ManagedOptionsType.HideUnofficialServers:
				return 0f;
			case ManagedOptions.ManagedOptionsType.HideModuleIncompatibleServers:
				return 0f;
			case ManagedOptions.ManagedOptionsType.HideBattleUI:
				return 0f;
			case ManagedOptions.ManagedOptionsType.UnitSpawnPrioritization:
				return 0f;
			case ManagedOptions.ManagedOptionsType.EnableSingleplayerChatBox:
				return 1f;
			case ManagedOptions.ManagedOptionsType.EnableMultiplayerChatBox:
				return 1f;
			case ManagedOptions.ManagedOptionsType.VoiceLanguage:
				return (float)LocalizedVoiceManager.GetVoiceLanguageIds().IndexOf(BannerlordConfig.VoiceLanguage);
			default:
				Debug.FailedAssert("ManagedOptionsType not found", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Options\\ManagedOptions\\ManagedOptions.cs", "GetDefaultConfig", 273);
				return 0f;
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000C93A9 File Offset: 0x000C75A9
		[MBCallback]
		internal static int GetConfigCount()
		{
			return 48;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000C93AD File Offset: 0x000C75AD
		[MBCallback]
		internal static float GetConfigValue(int type)
		{
			return ManagedOptions.GetConfig((ManagedOptions.ManagedOptionsType)type);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000C93B8 File Offset: 0x000C75B8
		public static void SetConfig(ManagedOptions.ManagedOptionsType type, float value)
		{
			switch (type)
			{
			case ManagedOptions.ManagedOptionsType.Language:
			{
				List<string> list = LocalizedTextManager.GetLanguageIds(NativeConfig.IsDevelopmentMode);
				if (value >= 0f && value < (float)list.Count)
				{
					BannerlordConfig.Language = list[(int)value];
				}
				else
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Options\\ManagedOptions\\ManagedOptions.cs", "SetConfig", 433);
					BannerlordConfig.Language = list[0];
				}
				break;
			}
			case ManagedOptions.ManagedOptionsType.GyroOverrideForAttackDefend:
				BannerlordConfig.GyroOverrideForAttackDefend = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ControlBlockDirection:
				BannerlordConfig.DefendDirectionControl = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.ControlAttackDirection:
				BannerlordConfig.AttackDirectionControl = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.NumberOfCorpses:
				BannerlordConfig.NumberOfCorpses = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.BattleSize:
				BannerlordConfig.BattleSize = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.ReinforcementWaveCount:
				BannerlordConfig.ReinforcementWaveCount = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson:
				BannerlordConfig.TurnCameraWithHorseInFirstPerson = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.ShowBlood:
				BannerlordConfig.ShowBlood = ((double)value != 0.0);
				break;
			case ManagedOptions.ManagedOptionsType.ShowAttackDirection:
				BannerlordConfig.DisplayAttackDirection = ((double)value != 0.0);
				break;
			case ManagedOptions.ManagedOptionsType.ShowTargetingReticle:
				BannerlordConfig.DisplayTargetingReticule = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.AutoSaveInterval:
				BannerlordConfig.AutoSaveInterval = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity:
				BannerlordConfig.FriendlyTroopsBannerOpacity = value;
				break;
			case ManagedOptions.ManagedOptionsType.ReportDamage:
				BannerlordConfig.ReportDamage = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ReportBark:
				BannerlordConfig.ReportBark = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.LockTarget:
				BannerlordConfig.LockTarget = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableTutorialHints:
				BannerlordConfig.EnableTutorialHints = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ReportCasualtiesType:
				BannerlordConfig.ReportCasualtiesType = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.ReportExperience:
				BannerlordConfig.ReportExperience = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ReportPersonalDamage:
				BannerlordConfig.ReportPersonalDamage = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.FirstPersonFov:
				BannerlordConfig.FirstPersonFov = value;
				break;
			case ManagedOptions.ManagedOptionsType.CombatCameraDistance:
				BannerlordConfig.CombatCameraDistance = value;
				break;
			case ManagedOptions.ManagedOptionsType.EnableDamageTakenVisuals:
				BannerlordConfig.EnableDamageTakenVisuals = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableVoiceChat:
				BannerlordConfig.EnableVoiceChat = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableDeathIcon:
				BannerlordConfig.EnableDeathIcon = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableNetworkAlertIcons:
				BannerlordConfig.EnableNetworkAlertIcons = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ForceVSyncInMenus:
				BannerlordConfig.ForceVSyncInMenus = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableVerticalAimCorrection:
				BannerlordConfig.EnableVerticalAimCorrection = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.ZoomSensitivityModifier:
				BannerlordConfig.ZoomSensitivityModifier = value;
				break;
			case ManagedOptions.ManagedOptionsType.UIScale:
				BannerlordConfig.UIScale = value;
				break;
			case ManagedOptions.ManagedOptionsType.CrosshairType:
				BannerlordConfig.CrosshairType = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.EnableGenericAvatars:
				BannerlordConfig.EnableGenericAvatars = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableGenericNames:
				BannerlordConfig.EnableGenericNames = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.OrderType:
				BannerlordConfig.OrderType = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.OrderLayoutType:
				BannerlordConfig.OrderLayoutType = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.AutoTrackAttackedSettlements:
				BannerlordConfig.AutoTrackAttackedSettlements = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.StopGameOnFocusLost:
				BannerlordConfig.StopGameOnFocusLost = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.SlowDownOnOrder:
				BannerlordConfig.SlowDownOnOrder = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HideFullServers:
				BannerlordConfig.HideFullServers = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HideEmptyServers:
				BannerlordConfig.HideEmptyServers = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HidePasswordProtectedServers:
				BannerlordConfig.HidePasswordProtectedServers = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HideUnofficialServers:
				BannerlordConfig.HideUnofficialServers = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HideModuleIncompatibleServers:
				BannerlordConfig.HideModuleIncompatibleServers = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.HideBattleUI:
				BannerlordConfig.HideBattleUI = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.UnitSpawnPrioritization:
				BannerlordConfig.UnitSpawnPrioritization = (int)value;
				break;
			case ManagedOptions.ManagedOptionsType.EnableSingleplayerChatBox:
				BannerlordConfig.EnableSingleplayerChatBox = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.EnableMultiplayerChatBox:
				BannerlordConfig.EnableMultiplayerChatBox = (value != 0f);
				break;
			case ManagedOptions.ManagedOptionsType.VoiceLanguage:
			{
				List<string> list = LocalizedVoiceManager.GetVoiceLanguageIds();
				if (value >= 0f && value < (float)list.Count)
				{
					BannerlordConfig.VoiceLanguage = list[(int)value];
				}
				else
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Options\\ManagedOptions\\ManagedOptions.cs", "SetConfig", 451);
					BannerlordConfig.VoiceLanguage = list[0];
				}
				break;
			}
			}
			ManagedOptions.OnManagedOptionChangedDelegate onManagedOptionChanged = ManagedOptions.OnManagedOptionChanged;
			if (onManagedOptionChanged == null)
			{
				return;
			}
			onManagedOptionChanged(type);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C9865 File Offset: 0x000C7A65
		public static SaveResult SaveConfig()
		{
			return BannerlordConfig.Save();
		}

		// Token: 0x040014B9 RID: 5305
		public static ManagedOptions.OnManagedOptionChangedDelegate OnManagedOptionChanged;

		// Token: 0x02000627 RID: 1575
		public enum ManagedOptionsType
		{
			// Token: 0x04001FD2 RID: 8146
			Language,
			// Token: 0x04001FD3 RID: 8147
			GyroOverrideForAttackDefend,
			// Token: 0x04001FD4 RID: 8148
			ControlBlockDirection,
			// Token: 0x04001FD5 RID: 8149
			ControlAttackDirection,
			// Token: 0x04001FD6 RID: 8150
			NumberOfCorpses,
			// Token: 0x04001FD7 RID: 8151
			BattleSize,
			// Token: 0x04001FD8 RID: 8152
			ReinforcementWaveCount,
			// Token: 0x04001FD9 RID: 8153
			TurnCameraWithHorseInFirstPerson,
			// Token: 0x04001FDA RID: 8154
			ShowBlood,
			// Token: 0x04001FDB RID: 8155
			ShowAttackDirection,
			// Token: 0x04001FDC RID: 8156
			ShowTargetingReticle,
			// Token: 0x04001FDD RID: 8157
			AutoSaveInterval,
			// Token: 0x04001FDE RID: 8158
			FriendlyTroopsBannerOpacity,
			// Token: 0x04001FDF RID: 8159
			ReportDamage,
			// Token: 0x04001FE0 RID: 8160
			ReportBark,
			// Token: 0x04001FE1 RID: 8161
			LockTarget,
			// Token: 0x04001FE2 RID: 8162
			EnableTutorialHints,
			// Token: 0x04001FE3 RID: 8163
			ReportCasualtiesType,
			// Token: 0x04001FE4 RID: 8164
			ReportExperience,
			// Token: 0x04001FE5 RID: 8165
			ReportPersonalDamage,
			// Token: 0x04001FE6 RID: 8166
			FirstPersonFov,
			// Token: 0x04001FE7 RID: 8167
			CombatCameraDistance,
			// Token: 0x04001FE8 RID: 8168
			EnableDamageTakenVisuals,
			// Token: 0x04001FE9 RID: 8169
			EnableVoiceChat,
			// Token: 0x04001FEA RID: 8170
			EnableDeathIcon,
			// Token: 0x04001FEB RID: 8171
			EnableNetworkAlertIcons,
			// Token: 0x04001FEC RID: 8172
			ForceVSyncInMenus,
			// Token: 0x04001FED RID: 8173
			EnableVerticalAimCorrection,
			// Token: 0x04001FEE RID: 8174
			ZoomSensitivityModifier,
			// Token: 0x04001FEF RID: 8175
			UIScale,
			// Token: 0x04001FF0 RID: 8176
			CrosshairType,
			// Token: 0x04001FF1 RID: 8177
			EnableGenericAvatars,
			// Token: 0x04001FF2 RID: 8178
			EnableGenericNames,
			// Token: 0x04001FF3 RID: 8179
			OrderType,
			// Token: 0x04001FF4 RID: 8180
			OrderLayoutType,
			// Token: 0x04001FF5 RID: 8181
			AutoTrackAttackedSettlements,
			// Token: 0x04001FF6 RID: 8182
			StopGameOnFocusLost,
			// Token: 0x04001FF7 RID: 8183
			SlowDownOnOrder,
			// Token: 0x04001FF8 RID: 8184
			HideFullServers,
			// Token: 0x04001FF9 RID: 8185
			HideEmptyServers,
			// Token: 0x04001FFA RID: 8186
			HidePasswordProtectedServers,
			// Token: 0x04001FFB RID: 8187
			HideUnofficialServers,
			// Token: 0x04001FFC RID: 8188
			HideModuleIncompatibleServers,
			// Token: 0x04001FFD RID: 8189
			HideBattleUI,
			// Token: 0x04001FFE RID: 8190
			UnitSpawnPrioritization,
			// Token: 0x04001FFF RID: 8191
			EnableSingleplayerChatBox,
			// Token: 0x04002000 RID: 8192
			EnableMultiplayerChatBox,
			// Token: 0x04002001 RID: 8193
			VoiceLanguage,
			// Token: 0x04002002 RID: 8194
			ManagedOptionTypeCount
		}

		// Token: 0x02000628 RID: 1576
		// (Invoke) Token: 0x06003C54 RID: 15444
		public delegate void OnManagedOptionChangedDelegate(ManagedOptions.ManagedOptionsType changedManagedOptionsType);
	}
}
