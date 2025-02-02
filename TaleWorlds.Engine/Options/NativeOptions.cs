using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x020000A4 RID: 164
	public class NativeOptions
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public static string GetGFXPresetName(NativeOptions.ConfigQuality presetIndex)
		{
			switch (presetIndex)
			{
			case NativeOptions.ConfigQuality.GFXVeryLow:
				return "1";
			case NativeOptions.ConfigQuality.GFXLow:
				return "2";
			case NativeOptions.ConfigQuality.GFXMedium:
				return "3";
			case NativeOptions.ConfigQuality.GFXHigh:
				return "4";
			case NativeOptions.ConfigQuality.GFXVeryHigh:
				return "5";
			case NativeOptions.ConfigQuality.GFXCustom:
				return "Custom";
			default:
				return "Unknown";
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0000D722 File Offset: 0x0000B922
		public static bool IsGFXOptionChangeable(NativeOptions.ConfigQuality config)
		{
			return config < NativeOptions.ConfigQuality.GFXCustom;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0000D728 File Offset: 0x0000B928
		private static void CorrectSelection(List<NativeOptionData> audioOptions)
		{
			foreach (NativeOptionData nativeOptionData in audioOptions)
			{
				if (nativeOptionData.Type == NativeOptions.NativeOptionsType.SoundDevice)
				{
					int num = 0;
					for (int i = 0; i < NativeOptions.GetSoundDeviceCount(); i++)
					{
						if (NativeOptions.GetSoundDeviceName(i) != "")
						{
							num = i;
						}
					}
					if (nativeOptionData.GetValue(false) > (float)num)
					{
						NativeOptions.SetConfig(NativeOptions.NativeOptionsType.SoundDevice, 0f);
						nativeOptionData.SetValue(0f);
					}
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000BFF RID: 3071 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		// (remove) Token: 0x06000C00 RID: 3072 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public static event Action OnNativeOptionsApplied;

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0000D828 File Offset: 0x0000BA28
		public static List<NativeOptionData> VideoOptions
		{
			get
			{
				if (NativeOptions._videoOptions == null)
				{
					NativeOptions._videoOptions = new List<NativeOptionData>();
					for (NativeOptions.NativeOptionsType nativeOptionsType = NativeOptions.NativeOptionsType.None; nativeOptionsType < NativeOptions.NativeOptionsType.TotalOptions; nativeOptionsType++)
					{
						if (nativeOptionsType - NativeOptions.NativeOptionsType.DisplayMode <= 7 || nativeOptionsType == NativeOptions.NativeOptionsType.SharpenAmount)
						{
							NativeOptions._videoOptions.Add(new NativeNumericOptionData(nativeOptionsType));
						}
					}
				}
				return NativeOptions._videoOptions;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0000D874 File Offset: 0x0000BA74
		public static List<NativeOptionData> GraphicsOptions
		{
			get
			{
				if (NativeOptions._graphicsOptions == null)
				{
					NativeOptions._graphicsOptions = new List<NativeOptionData>();
					for (NativeOptions.NativeOptionsType nativeOptionsType = NativeOptions.NativeOptionsType.None; nativeOptionsType < NativeOptions.NativeOptionsType.TotalOptions; nativeOptionsType++)
					{
						switch (nativeOptionsType)
						{
						case NativeOptions.NativeOptionsType.MaxSimultaneousSoundEventCount:
						case NativeOptions.NativeOptionsType.OverAll:
						case NativeOptions.NativeOptionsType.ShaderQuality:
						case NativeOptions.NativeOptionsType.TextureBudget:
						case NativeOptions.NativeOptionsType.TextureQuality:
						case NativeOptions.NativeOptionsType.ShadowmapResolution:
						case NativeOptions.NativeOptionsType.ShadowmapType:
						case NativeOptions.NativeOptionsType.ShadowmapFiltering:
						case NativeOptions.NativeOptionsType.ParticleDetail:
						case NativeOptions.NativeOptionsType.ParticleQuality:
						case NativeOptions.NativeOptionsType.FoliageQuality:
						case NativeOptions.NativeOptionsType.CharacterDetail:
						case NativeOptions.NativeOptionsType.EnvironmentDetail:
						case NativeOptions.NativeOptionsType.TerrainQuality:
						case NativeOptions.NativeOptionsType.NumberOfRagDolls:
						case NativeOptions.NativeOptionsType.AnimationSamplingQuality:
						case NativeOptions.NativeOptionsType.Occlusion:
						case NativeOptions.NativeOptionsType.TextureFiltering:
						case NativeOptions.NativeOptionsType.WaterQuality:
						case NativeOptions.NativeOptionsType.Antialiasing:
						case NativeOptions.NativeOptionsType.LightingQuality:
						case NativeOptions.NativeOptionsType.DecalQuality:
							NativeOptions._graphicsOptions.Add(new NativeSelectionOptionData(nativeOptionsType));
							break;
						case NativeOptions.NativeOptionsType.DLSS:
							if (NativeOptions.GetIsDLSSAvailable())
							{
								NativeOptions._graphicsOptions.Add(new NativeSelectionOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.DepthOfField:
						case NativeOptions.NativeOptionsType.SSR:
						case NativeOptions.NativeOptionsType.ClothSimulation:
						case NativeOptions.NativeOptionsType.InteractiveGrass:
						case NativeOptions.NativeOptionsType.SunShafts:
						case NativeOptions.NativeOptionsType.SSSSS:
						case NativeOptions.NativeOptionsType.Tesselation:
						case NativeOptions.NativeOptionsType.Bloom:
						case NativeOptions.NativeOptionsType.FilmGrain:
						case NativeOptions.NativeOptionsType.MotionBlur:
						case NativeOptions.NativeOptionsType.DynamicResolution:
							NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							break;
						case NativeOptions.NativeOptionsType.PostFXLensFlare:
							if (EngineApplicationInterface.IConfig.CheckGFXSupportStatus(62))
							{
								NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.PostFXStreaks:
							if (EngineApplicationInterface.IConfig.CheckGFXSupportStatus(63))
							{
								NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.PostFXChromaticAberration:
							if (EngineApplicationInterface.IConfig.CheckGFXSupportStatus(64))
							{
								NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.PostFXVignette:
							if (EngineApplicationInterface.IConfig.CheckGFXSupportStatus(65))
							{
								NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.PostFXHexagonVignette:
							if (EngineApplicationInterface.IConfig.CheckGFXSupportStatus(66))
							{
								NativeOptions._graphicsOptions.Add(new NativeBooleanOptionData(nativeOptionsType));
							}
							break;
						case NativeOptions.NativeOptionsType.DynamicResolutionTarget:
							NativeOptions._graphicsOptions.Add(new NativeNumericOptionData(nativeOptionsType));
							break;
						}
					}
				}
				return NativeOptions._graphicsOptions;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		public static void ReadRGLConfigFiles()
		{
			EngineApplicationInterface.IConfig.ReadRGLConfigFiles();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public static float GetConfig(NativeOptions.NativeOptionsType type)
		{
			return EngineApplicationInterface.IConfig.GetRGLConfig((int)type);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0000DAE1 File Offset: 0x0000BCE1
		public static float GetDefaultConfig(NativeOptions.NativeOptionsType type)
		{
			return EngineApplicationInterface.IConfig.GetDefaultRGLConfig((int)type);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		public static float GetDefaultConfigForOverallSettings(NativeOptions.NativeOptionsType type, int config)
		{
			return EngineApplicationInterface.IConfig.GetRGLConfigForDefaultSettings((int)type, config);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public static int GetGameKeys(int keyType, int i)
		{
			Debug.FailedAssert("This is not implemented. Changed from Exception to not cause crash.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\Options\\NativeOptions\\NativeOptions.cs", "GetGameKeys", 326);
			return 0;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public static string GetSoundDeviceName(int i)
		{
			return EngineApplicationInterface.IConfig.GetSoundDeviceName(i);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0000DB25 File Offset: 0x0000BD25
		public static string GetMonitorDeviceName(int i)
		{
			return EngineApplicationInterface.IConfig.GetMonitorDeviceName(i);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0000DB32 File Offset: 0x0000BD32
		public static string GetVideoDeviceName(int i)
		{
			return EngineApplicationInterface.IConfig.GetVideoDeviceName(i);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		public static int GetSoundDeviceCount()
		{
			return EngineApplicationInterface.IConfig.GetSoundDeviceCount();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		public static int GetMonitorDeviceCount()
		{
			return EngineApplicationInterface.IConfig.GetMonitorDeviceCount();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0000DB57 File Offset: 0x0000BD57
		public static int GetVideoDeviceCount()
		{
			return EngineApplicationInterface.IConfig.GetVideoDeviceCount();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0000DB63 File Offset: 0x0000BD63
		public static int GetResolutionCount()
		{
			return EngineApplicationInterface.IConfig.GetResolutionCount();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0000DB6F File Offset: 0x0000BD6F
		public static void RefreshOptionsData()
		{
			EngineApplicationInterface.IConfig.RefreshOptionsData();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0000DB7B File Offset: 0x0000BD7B
		public static int GetRefreshRateCount()
		{
			return EngineApplicationInterface.IConfig.GetRefreshRateCount();
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0000DB87 File Offset: 0x0000BD87
		public static int GetRefreshRateAtIndex(int index)
		{
			return EngineApplicationInterface.IConfig.GetRefreshRateAtIndex(index);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public static void SetCustomResolution(int width, int height)
		{
			EngineApplicationInterface.IConfig.SetCustomResolution(width, height);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0000DBA2 File Offset: 0x0000BDA2
		public static void GetResolution(ref int width, ref int height)
		{
			EngineApplicationInterface.IConfig.GetDesktopResolution(ref width, ref height);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public static void GetDesktopResolution(ref int width, ref int height)
		{
			EngineApplicationInterface.IConfig.GetDesktopResolution(ref width, ref height);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		public static Vec2 GetResolutionAtIndex(int index)
		{
			return EngineApplicationInterface.IConfig.GetResolutionAtIndex(index);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0000DBCB File Offset: 0x0000BDCB
		public static int GetDLSSTechnique()
		{
			return EngineApplicationInterface.IConfig.GetDlssTechnique();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0000DBD7 File Offset: 0x0000BDD7
		public static bool Is120HzAvailable()
		{
			return EngineApplicationInterface.IConfig.Is120HzAvailable();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0000DBE3 File Offset: 0x0000BDE3
		public static int GetDLSSOptionCount()
		{
			return EngineApplicationInterface.IConfig.GetDlssOptionCount();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0000DBEF File Offset: 0x0000BDEF
		public static bool GetIsDLSSAvailable()
		{
			return EngineApplicationInterface.IConfig.IsDlssAvailable();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0000DBFB File Offset: 0x0000BDFB
		public static bool CheckGFXSupportStatus(int enumType)
		{
			return EngineApplicationInterface.IConfig.CheckGFXSupportStatus(enumType);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public static void SetConfig(NativeOptions.NativeOptionsType type, float value)
		{
			EngineApplicationInterface.IConfig.SetRGLConfig((int)type, value);
			NativeOptions.OnNativeOptionChangedDelegate onNativeOptionChanged = NativeOptions.OnNativeOptionChanged;
			if (onNativeOptionChanged == null)
			{
				return;
			}
			onNativeOptionChanged(type);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0000DC26 File Offset: 0x0000BE26
		public static void ApplyConfigChanges(bool resizeWindow)
		{
			EngineApplicationInterface.IConfig.ApplyConfigChanges(resizeWindow);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0000DC33 File Offset: 0x0000BE33
		public static void SetGameKeys(int keyType, int index, int key)
		{
			Debug.FailedAssert("This is not implemented. Changed from Exception to not cause crash.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\Options\\NativeOptions\\NativeOptions.cs", "SetGameKeys", 439);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0000DC50 File Offset: 0x0000BE50
		public static void Apply(int texture_budget, int sharpen_amount, int hdr, int dof_mode, int motion_blur, int ssr, int size, int texture_filtering, int trail_amount, int dynamic_resolution_target)
		{
			EngineApplicationInterface.IConfig.Apply(texture_budget, sharpen_amount, hdr, dof_mode, motion_blur, ssr, size, texture_filtering, trail_amount, dynamic_resolution_target);
			Action onNativeOptionsApplied = NativeOptions.OnNativeOptionsApplied;
			if (onNativeOptionsApplied == null)
			{
				return;
			}
			onNativeOptionsApplied();
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0000DC86 File Offset: 0x0000BE86
		public static SaveResult SaveConfig()
		{
			return (SaveResult)EngineApplicationInterface.IConfig.SaveRGLConfig();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0000DC92 File Offset: 0x0000BE92
		public static void SetBrightness(float gamma)
		{
			EngineApplicationInterface.IConfig.SetBrightness(gamma);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public static void SetDefaultGameKeys()
		{
			Debug.FailedAssert("This is not implemented. Changed from Exception to not cause crash.", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\Options\\NativeOptions\\NativeOptions.cs", "SetDefaultGameKeys", 464);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		public static void SetDefaultGameConfig()
		{
			EngineApplicationInterface.IConfig.SetDefaultGameConfig();
		}

		// Token: 0x04000204 RID: 516
		public static NativeOptions.OnNativeOptionChangedDelegate OnNativeOptionChanged;

		// Token: 0x04000206 RID: 518
		private static List<NativeOptionData> _videoOptions;

		// Token: 0x04000207 RID: 519
		private static List<NativeOptionData> _graphicsOptions;

		// Token: 0x020000D0 RID: 208
		public enum ConfigQuality
		{
			// Token: 0x0400046E RID: 1134
			GFXVeryLow,
			// Token: 0x0400046F RID: 1135
			GFXLow,
			// Token: 0x04000470 RID: 1136
			GFXMedium,
			// Token: 0x04000471 RID: 1137
			GFXHigh,
			// Token: 0x04000472 RID: 1138
			GFXVeryHigh,
			// Token: 0x04000473 RID: 1139
			GFXCustom
		}

		// Token: 0x020000D1 RID: 209
		public enum NativeOptionsType
		{
			// Token: 0x04000475 RID: 1141
			None = -1,
			// Token: 0x04000476 RID: 1142
			MasterVolume,
			// Token: 0x04000477 RID: 1143
			SoundVolume,
			// Token: 0x04000478 RID: 1144
			MusicVolume,
			// Token: 0x04000479 RID: 1145
			VoiceChatVolume,
			// Token: 0x0400047A RID: 1146
			VoiceOverVolume,
			// Token: 0x0400047B RID: 1147
			SoundDevice,
			// Token: 0x0400047C RID: 1148
			MaxSimultaneousSoundEventCount,
			// Token: 0x0400047D RID: 1149
			SoundOutput,
			// Token: 0x0400047E RID: 1150
			SoundPreset,
			// Token: 0x0400047F RID: 1151
			KeepSoundInBackground,
			// Token: 0x04000480 RID: 1152
			SoundOcclusion,
			// Token: 0x04000481 RID: 1153
			MouseSensitivity,
			// Token: 0x04000482 RID: 1154
			InvertMouseYAxis,
			// Token: 0x04000483 RID: 1155
			MouseYMovementScale,
			// Token: 0x04000484 RID: 1156
			TrailAmount,
			// Token: 0x04000485 RID: 1157
			EnableVibration,
			// Token: 0x04000486 RID: 1158
			EnableGyroAssistedAim,
			// Token: 0x04000487 RID: 1159
			GyroAimSensitivity,
			// Token: 0x04000488 RID: 1160
			EnableTouchpadMouse,
			// Token: 0x04000489 RID: 1161
			EnableAlternateAiming,
			// Token: 0x0400048A RID: 1162
			DisplayMode,
			// Token: 0x0400048B RID: 1163
			SelectedMonitor,
			// Token: 0x0400048C RID: 1164
			SelectedAdapter,
			// Token: 0x0400048D RID: 1165
			ScreenResolution,
			// Token: 0x0400048E RID: 1166
			RefreshRate,
			// Token: 0x0400048F RID: 1167
			ResolutionScale,
			// Token: 0x04000490 RID: 1168
			FrameLimiter,
			// Token: 0x04000491 RID: 1169
			VSync,
			// Token: 0x04000492 RID: 1170
			Brightness,
			// Token: 0x04000493 RID: 1171
			OverAll,
			// Token: 0x04000494 RID: 1172
			ShaderQuality,
			// Token: 0x04000495 RID: 1173
			TextureBudget,
			// Token: 0x04000496 RID: 1174
			TextureQuality,
			// Token: 0x04000497 RID: 1175
			ShadowmapResolution,
			// Token: 0x04000498 RID: 1176
			ShadowmapType,
			// Token: 0x04000499 RID: 1177
			ShadowmapFiltering,
			// Token: 0x0400049A RID: 1178
			ParticleDetail,
			// Token: 0x0400049B RID: 1179
			ParticleQuality,
			// Token: 0x0400049C RID: 1180
			FoliageQuality,
			// Token: 0x0400049D RID: 1181
			CharacterDetail,
			// Token: 0x0400049E RID: 1182
			EnvironmentDetail,
			// Token: 0x0400049F RID: 1183
			TerrainQuality,
			// Token: 0x040004A0 RID: 1184
			NumberOfRagDolls,
			// Token: 0x040004A1 RID: 1185
			AnimationSamplingQuality,
			// Token: 0x040004A2 RID: 1186
			Occlusion,
			// Token: 0x040004A3 RID: 1187
			TextureFiltering,
			// Token: 0x040004A4 RID: 1188
			WaterQuality,
			// Token: 0x040004A5 RID: 1189
			Antialiasing,
			// Token: 0x040004A6 RID: 1190
			DLSS,
			// Token: 0x040004A7 RID: 1191
			LightingQuality,
			// Token: 0x040004A8 RID: 1192
			DecalQuality,
			// Token: 0x040004A9 RID: 1193
			DepthOfField,
			// Token: 0x040004AA RID: 1194
			SSR,
			// Token: 0x040004AB RID: 1195
			ClothSimulation,
			// Token: 0x040004AC RID: 1196
			InteractiveGrass,
			// Token: 0x040004AD RID: 1197
			SunShafts,
			// Token: 0x040004AE RID: 1198
			SSSSS,
			// Token: 0x040004AF RID: 1199
			Tesselation,
			// Token: 0x040004B0 RID: 1200
			Bloom,
			// Token: 0x040004B1 RID: 1201
			FilmGrain,
			// Token: 0x040004B2 RID: 1202
			MotionBlur,
			// Token: 0x040004B3 RID: 1203
			SharpenAmount,
			// Token: 0x040004B4 RID: 1204
			PostFXLensFlare,
			// Token: 0x040004B5 RID: 1205
			PostFXStreaks,
			// Token: 0x040004B6 RID: 1206
			PostFXChromaticAberration,
			// Token: 0x040004B7 RID: 1207
			PostFXVignette,
			// Token: 0x040004B8 RID: 1208
			PostFXHexagonVignette,
			// Token: 0x040004B9 RID: 1209
			BrightnessMin,
			// Token: 0x040004BA RID: 1210
			BrightnessMax,
			// Token: 0x040004BB RID: 1211
			BrightnessCalibrated,
			// Token: 0x040004BC RID: 1212
			ExposureCompensation,
			// Token: 0x040004BD RID: 1213
			DynamicResolution,
			// Token: 0x040004BE RID: 1214
			DynamicResolutionTarget,
			// Token: 0x040004BF RID: 1215
			NumOfOptionTypes,
			// Token: 0x040004C0 RID: 1216
			TotalOptions
		}

		// Token: 0x020000D2 RID: 210
		// (Invoke) Token: 0x06000CB3 RID: 3251
		public delegate void OnNativeOptionChangedDelegate(NativeOptions.NativeOptionsType changedNativeOptionsType);
	}
}
