using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Options.ManagedOptions;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x02000381 RID: 897
	public static class OptionsProvider
	{
		// Token: 0x0600313D RID: 12605 RVA: 0x000CBCC1 File Offset: 0x000C9EC1
		public static OptionCategory GetVideoOptionCategory(bool isMainMenu, Action onBrightnessClick, Action onExposureClick, Action onBenchmarkClick)
		{
			return new OptionCategory(OptionsProvider.GetVideoGeneralOptions(isMainMenu, onBrightnessClick, onExposureClick, onBenchmarkClick), OptionsProvider.GetVideoOptionGroups());
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000CBCD6 File Offset: 0x000C9ED6
		private static IEnumerable<IOptionData> GetVideoGeneralOptions(bool isMainMenu, Action onBrightnessClick, Action onExposureClick, Action onBenchmarkClick)
		{
			if (isMainMenu)
			{
				yield return new ActionOptionData("Benchmark", onBenchmarkClick);
			}
			yield return new ActionOptionData(NativeOptions.NativeOptionsType.Brightness, onBrightnessClick);
			yield return new ActionOptionData(NativeOptions.NativeOptionsType.ExposureCompensation, onExposureClick);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.SelectedMonitor);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.SelectedAdapter);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.DisplayMode);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ScreenResolution);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.RefreshRate);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.VSync);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ForceVSyncInMenus);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.FrameLimiter);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.SharpenAmount);
			yield break;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000CBCFB File Offset: 0x000C9EFB
		private static IEnumerable<IOptionData> GetPerformanceGeneralOptions(bool isMultiplayer)
		{
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.OverAll);
			yield break;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000CBD04 File Offset: 0x000C9F04
		private static IEnumerable<OptionGroup> GetVideoOptionGroups()
		{
			return null;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000CBD07 File Offset: 0x000C9F07
		public static OptionCategory GetPerformanceOptionCategory(bool isMultiplayer)
		{
			return new OptionCategory(OptionsProvider.GetPerformanceGeneralOptions(isMultiplayer), OptionsProvider.GetPerformanceOptionGroups(isMultiplayer));
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000CBD1A File Offset: 0x000C9F1A
		private static IEnumerable<OptionGroup> GetPerformanceOptionGroups(bool isMultiplayer)
		{
			yield return new OptionGroup(new TextObject("{=sRTd3RI5}Graphics", null), OptionsProvider.GetPerformanceGraphicsOptions(isMultiplayer));
			yield return new OptionGroup(new TextObject("{=vDMe8SCV}Resolution Scaling", null), OptionsProvider.GetPerformanceResolutionScalingOptions(isMultiplayer));
			yield return new OptionGroup(new TextObject("{=2zcrC0h1}Gameplay", null), OptionsProvider.GetPerformanceGameplayOptions(isMultiplayer));
			yield return new OptionGroup(new TextObject("{=xebFLnH2}Audio", null), OptionsProvider.GetPerformanceAudioOptions());
			yield break;
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000CBD2A File Offset: 0x000C9F2A
		public static IEnumerable<IOptionData> GetPerformanceGraphicsOptions(bool isMultiplayer)
		{
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.Antialiasing);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ShaderQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.TextureBudget);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.TextureQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.TextureFiltering);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.CharacterDetail);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ShadowmapResolution);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ShadowmapType);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ShadowmapFiltering);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ParticleDetail);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.ParticleQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.FoliageQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.TerrainQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.EnvironmentDetail);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.Occlusion);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.DecalQuality);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.WaterQuality);
			if (!isMultiplayer)
			{
				yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.NumberOfCorpses);
			}
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.NumberOfRagDolls);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.LightingQuality);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.ClothSimulation);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.SunShafts);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.Tesselation);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.InteractiveGrass);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.SSR);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.SSSSS);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.MotionBlur);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.DepthOfField);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.Bloom);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.FilmGrain);
			if (NativeOptions.CheckGFXSupportStatus(65))
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.PostFXVignette);
			}
			if (NativeOptions.CheckGFXSupportStatus(64))
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.PostFXChromaticAberration);
			}
			if (NativeOptions.CheckGFXSupportStatus(62))
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.PostFXLensFlare);
			}
			if (NativeOptions.CheckGFXSupportStatus(66))
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.PostFXHexagonVignette);
			}
			if (NativeOptions.CheckGFXSupportStatus(63))
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.PostFXStreaks);
			}
			yield break;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000CBD3A File Offset: 0x000C9F3A
		public static IEnumerable<IOptionData> GetPerformanceResolutionScalingOptions(bool isMultiplayer)
		{
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.DLSS);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.ResolutionScale);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.DynamicResolution);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.DynamicResolutionTarget);
			yield break;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000CBD43 File Offset: 0x000C9F43
		public static IEnumerable<IOptionData> GetPerformanceGameplayOptions(bool isMultiplayer)
		{
			if (!isMultiplayer)
			{
				yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.BattleSize);
			}
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.AnimationSamplingQuality);
			yield break;
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000CBD53 File Offset: 0x000C9F53
		public static IEnumerable<IOptionData> GetPerformanceAudioOptions()
		{
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.MaxSimultaneousSoundEventCount);
			yield break;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000CBD5C File Offset: 0x000C9F5C
		private static IEnumerable<IOptionData> GetAudioGeneralOptions(bool isMultiplayer)
		{
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.MasterVolume);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.SoundVolume);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.MusicVolume);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.VoiceOverVolume);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.SoundDevice);
			yield return new NativeSelectionOptionData(NativeOptions.NativeOptionsType.SoundOutput);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.KeepSoundInBackground);
			if (isMultiplayer)
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableVoiceChat);
			}
			else
			{
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.SoundOcclusion);
			}
			yield break;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000CBD6C File Offset: 0x000C9F6C
		public static OptionCategory GetAudioOptionCategory(bool isMultiplayer)
		{
			return new OptionCategory(OptionsProvider.GetAudioGeneralOptions(isMultiplayer), OptionsProvider.GetAudioOptionGroups(isMultiplayer));
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000CBD7F File Offset: 0x000C9F7F
		private static IEnumerable<OptionGroup> GetAudioOptionGroups(bool isMultiplayer)
		{
			return null;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000CBD82 File Offset: 0x000C9F82
		public static OptionCategory GetGameplayOptionCategory(bool isMainMenu, bool isMultiplayer)
		{
			return new OptionCategory(OptionsProvider.GetGameplayGeneralOptions(isMultiplayer), OptionsProvider.GetGameplayOptionGroups(isMainMenu, isMultiplayer));
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000CBD96 File Offset: 0x000C9F96
		private static IEnumerable<IOptionData> GetGameplayGeneralOptions(bool isMultiplayer)
		{
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.Language);
			if (!isMultiplayer)
			{
				yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.VoiceLanguage);
				yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.UnitSpawnPrioritization);
				yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.ReinforcementWaveCount);
			}
			yield break;
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000CBDA6 File Offset: 0x000C9FA6
		private static IEnumerable<OptionGroup> GetGameplayOptionGroups(bool isMainMenu, bool isMultiplayer)
		{
			yield return new OptionGroup(new TextObject("{=m9KoYCv5}Controls", null), OptionsProvider.GetGameplayControlsOptions(isMainMenu, isMultiplayer));
			yield return new OptionGroup(new TextObject("{=uZ6q4Qs2}Visuals", null), OptionsProvider.GetGameplayVisualOptions(isMultiplayer));
			yield return new OptionGroup(new TextObject("{=gAfbULHM}Camera", null), OptionsProvider.GetGameplayCameraOptions(isMultiplayer));
			yield return new OptionGroup(new TextObject("{=WRMyiiYJ}User Interface", null), OptionsProvider.GetGameplayUIOptions(isMultiplayer));
			if (!isMultiplayer)
			{
				yield return new OptionGroup(new TextObject("{=ys9baYiQ}Campaign", null), OptionsProvider.GetGameplayCampaignOptions());
			}
			yield break;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000CBDBD File Offset: 0x000C9FBD
		private static IEnumerable<IOptionData> GetGameplayControlsOptions(bool isMainMenu, bool isMultiplayer)
		{
			bool isDualSense = Input.ControllerType.IsPlaystation();
			if (isDualSense)
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.GyroOverrideForAttackDefend);
			}
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.ControlBlockDirection);
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.ControlAttackDirection);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.MouseYMovementScale);
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.MouseSensitivity);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.InvertMouseYAxis);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.EnableVibration);
			yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.EnableAlternateAiming);
			if (isDualSense)
			{
				if (isMainMenu)
				{
					yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.EnableTouchpadMouse);
				}
				yield return new NativeBooleanOptionData(NativeOptions.NativeOptionsType.EnableGyroAssistedAim);
				yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.GyroAimSensitivity);
			}
			if (!isMultiplayer)
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.LockTarget);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.SlowDownOnOrder);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.StopGameOnFocusLost);
			}
			yield break;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000CBDD4 File Offset: 0x000C9FD4
		private static IEnumerable<IOptionData> GetGameplayVisualOptions(bool isMultiplayer)
		{
			yield return new NativeNumericOptionData(NativeOptions.NativeOptionsType.TrailAmount);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ShowBlood);
			yield break;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000CBDDD File Offset: 0x000C9FDD
		private static IEnumerable<IOptionData> GetGameplayCameraOptions(bool isMultiplayer)
		{
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.FirstPersonFov);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.CombatCameraDistance);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableVerticalAimCorrection);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.ZoomSensitivityModifier);
			yield break;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000CBDE6 File Offset: 0x000C9FE6
		private static IEnumerable<IOptionData> GetGameplayUIOptions(bool isMultiplayer)
		{
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.CrosshairType);
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.OrderType);
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.OrderLayoutType);
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.ReportCasualtiesType);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.UIScale);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ShowAttackDirection);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ShowTargetingReticle);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ReportDamage);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ReportBark);
			if (!isMultiplayer)
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ReportExperience);
			}
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.ReportPersonalDamage);
			yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableDamageTakenVisuals);
			if (isMultiplayer)
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableMultiplayerChatBox);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableDeathIcon);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableNetworkAlertIcons);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableGenericAvatars);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableGenericNames);
			}
			else
			{
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableSingleplayerChatBox);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.HideBattleUI);
				yield return new ManagedBooleanOptionData(ManagedOptions.ManagedOptionsType.EnableTutorialHints);
			}
			yield break;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000CBDF6 File Offset: 0x000C9FF6
		private static IEnumerable<IOptionData> GetGameplayCampaignOptions()
		{
			yield return new ManagedSelectionOptionData(ManagedOptions.ManagedOptionsType.AutoTrackAttackedSettlements);
			yield return new ManagedNumericOptionData(ManagedOptions.ManagedOptionsType.AutoSaveInterval);
			yield break;
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000CBDFF File Offset: 0x000C9FFF
		public static IEnumerable<string> GetGameKeyCategoriesList(bool isMultiplayer)
		{
			yield return GameKeyMainCategories.ActionCategory;
			yield return GameKeyMainCategories.OrderMenuCategory;
			if (!isMultiplayer)
			{
				yield return GameKeyMainCategories.CampaignMapCategory;
				yield return GameKeyMainCategories.MenuShortcutCategory;
				yield return GameKeyMainCategories.PhotoModeCategory;
			}
			else
			{
				yield return GameKeyMainCategories.PollCategory;
			}
			yield return GameKeyMainCategories.ChatCategory;
			yield break;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000CBE0F File Offset: 0x000CA00F
		public static OptionCategory GetControllerOptionCategory()
		{
			return new OptionCategory(OptionsProvider.GetControllerBaseOptions(), OptionsProvider.GetControllerOptionGroups());
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000CBE20 File Offset: 0x000CA020
		private static IEnumerable<IOptionData> GetControllerBaseOptions()
		{
			return null;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000CBE23 File Offset: 0x000CA023
		private static IEnumerable<OptionGroup> GetControllerOptionGroups()
		{
			return null;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000CBE28 File Offset: 0x000CA028
		public static Dictionary<NativeOptions.NativeOptionsType, float[]> GetDefaultNativeOptions()
		{
			if (OptionsProvider._defaultNativeOptions == null)
			{
				OptionsProvider._defaultNativeOptions = new Dictionary<NativeOptions.NativeOptionsType, float[]>();
				foreach (NativeOptionData nativeOptionData in NativeOptions.VideoOptions.Union(NativeOptions.GraphicsOptions))
				{
					float[] array = new float[OptionsProvider._overallConfigCount];
					bool flag = false;
					for (int i = 0; i < OptionsProvider._overallConfigCount; i++)
					{
						array[i] = NativeOptions.GetDefaultConfigForOverallSettings(nativeOptionData.Type, i);
						if (array[i] < 0f)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						OptionsProvider._defaultNativeOptions[nativeOptionData.Type] = array;
					}
				}
			}
			return OptionsProvider._defaultNativeOptions;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000CBEE8 File Offset: 0x000CA0E8
		public static Dictionary<ManagedOptions.ManagedOptionsType, float[]> GetDefaultManagedOptions()
		{
			if (OptionsProvider._defaultManagedOptions == null)
			{
				OptionsProvider._defaultManagedOptions = new Dictionary<ManagedOptions.ManagedOptionsType, float[]>();
				float[] array = new float[OptionsProvider._overallConfigCount];
				for (int i = 0; i < OptionsProvider._overallConfigCount; i++)
				{
					array[i] = (float)i;
				}
				OptionsProvider._defaultManagedOptions.Add(ManagedOptions.ManagedOptionsType.BattleSize, array);
				array = new float[OptionsProvider._overallConfigCount];
				for (int j = 0; j < OptionsProvider._overallConfigCount; j++)
				{
					array[j] = (float)j;
				}
				OptionsProvider._defaultManagedOptions.Add(ManagedOptions.ManagedOptionsType.NumberOfCorpses, array);
			}
			return OptionsProvider._defaultManagedOptions;
		}

		// Token: 0x04001521 RID: 5409
		private static readonly int _overallConfigCount = NativeSelectionOptionData.GetOptionsLimit(NativeOptions.NativeOptionsType.OverAll) - 1;

		// Token: 0x04001522 RID: 5410
		private static Dictionary<NativeOptions.NativeOptionsType, float[]> _defaultNativeOptions;

		// Token: 0x04001523 RID: 5411
		private static Dictionary<ManagedOptions.ManagedOptionsType, float[]> _defaultManagedOptions;
	}
}
