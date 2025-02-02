using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200000D RID: 13
	internal class ScriptingInterfaceOfIConfig : IConfig
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000D438 File Offset: 0x0000B638
		public void Apply(int texture_budget, int sharpen_amount, int hdr, int dof_mode, int motion_blur, int ssr, int size, int texture_filtering, int trail_amount, int dynamic_resolution_target)
		{
			ScriptingInterfaceOfIConfig.call_ApplyDelegate(texture_budget, sharpen_amount, hdr, dof_mode, motion_blur, ssr, size, texture_filtering, trail_amount, dynamic_resolution_target);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000D460 File Offset: 0x0000B660
		public void ApplyConfigChanges(bool resizeWindow)
		{
			ScriptingInterfaceOfIConfig.call_ApplyConfigChangesDelegate(resizeWindow);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000D46D File Offset: 0x0000B66D
		public int AutoSaveInMinutes()
		{
			return ScriptingInterfaceOfIConfig.call_AutoSaveInMinutesDelegate();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000D479 File Offset: 0x0000B679
		public bool CheckGFXSupportStatus(int enum_id)
		{
			return ScriptingInterfaceOfIConfig.call_CheckGFXSupportStatusDelegate(enum_id);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000D486 File Offset: 0x0000B686
		public int GetAutoGFXQuality()
		{
			return ScriptingInterfaceOfIConfig.call_GetAutoGFXQualityDelegate();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000D492 File Offset: 0x0000B692
		public int GetCharacterDetail()
		{
			return ScriptingInterfaceOfIConfig.call_GetCharacterDetailDelegate();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000D49E File Offset: 0x0000B69E
		public bool GetCheatMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetCheatModeDelegate();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000D4AA File Offset: 0x0000B6AA
		public int GetCurrentSoundDeviceIndex()
		{
			return ScriptingInterfaceOfIConfig.call_GetCurrentSoundDeviceIndexDelegate();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		public string GetDebugLoginPassword()
		{
			if (ScriptingInterfaceOfIConfig.call_GetDebugLoginPasswordDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		public string GetDebugLoginUserName()
		{
			if (ScriptingInterfaceOfIConfig.call_GetDebugLoginUserNameDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000D4E2 File Offset: 0x0000B6E2
		public float GetDefaultRGLConfig(int type)
		{
			return ScriptingInterfaceOfIConfig.call_GetDefaultRGLConfigDelegate(type);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000D4EF File Offset: 0x0000B6EF
		public void GetDesktopResolution(ref int width, ref int height)
		{
			ScriptingInterfaceOfIConfig.call_GetDesktopResolutionDelegate(ref width, ref height);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000D4FD File Offset: 0x0000B6FD
		public bool GetDevelopmentMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetDevelopmentModeDelegate();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000D509 File Offset: 0x0000B709
		public bool GetDisableGuiMessages()
		{
			return ScriptingInterfaceOfIConfig.call_GetDisableGuiMessagesDelegate();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000D515 File Offset: 0x0000B715
		public bool GetDisableSound()
		{
			return ScriptingInterfaceOfIConfig.call_GetDisableSoundDelegate();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000D521 File Offset: 0x0000B721
		public int GetDlssOptionCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetDlssOptionCountDelegate();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000D52D File Offset: 0x0000B72D
		public int GetDlssTechnique()
		{
			return ScriptingInterfaceOfIConfig.call_GetDlssTechniqueDelegate();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000D539 File Offset: 0x0000B739
		public bool GetDoLocalizationCheckAtStartup()
		{
			return ScriptingInterfaceOfIConfig.call_GetDoLocalizationCheckAtStartupDelegate();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000D545 File Offset: 0x0000B745
		public bool GetEnableClothSimulation()
		{
			return ScriptingInterfaceOfIConfig.call_GetEnableClothSimulationDelegate();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000D551 File Offset: 0x0000B751
		public bool GetEnableEditMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetEnableEditModeDelegate();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000D55D File Offset: 0x0000B75D
		public bool GetInvertMouse()
		{
			return ScriptingInterfaceOfIConfig.call_GetInvertMouseDelegate();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000D569 File Offset: 0x0000B769
		public string GetLastOpenedScene()
		{
			if (ScriptingInterfaceOfIConfig.call_GetLastOpenedSceneDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000D57F File Offset: 0x0000B77F
		public bool GetLocalizationDebugMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetLocalizationDebugModeDelegate();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000D58B File Offset: 0x0000B78B
		public int GetMonitorDeviceCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetMonitorDeviceCountDelegate();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000D597 File Offset: 0x0000B797
		public string GetMonitorDeviceName(int i)
		{
			if (ScriptingInterfaceOfIConfig.call_GetMonitorDeviceNameDelegate(i) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		public int GetRefreshRateAtIndex(int index)
		{
			return ScriptingInterfaceOfIConfig.call_GetRefreshRateAtIndexDelegate(index);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000D5BB File Offset: 0x0000B7BB
		public int GetRefreshRateCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetRefreshRateCountDelegate();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000D5C7 File Offset: 0x0000B7C7
		public void GetResolution(ref int width, ref int height)
		{
			ScriptingInterfaceOfIConfig.call_GetResolutionDelegate(ref width, ref height);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		public Vec2 GetResolutionAtIndex(int index)
		{
			return ScriptingInterfaceOfIConfig.call_GetResolutionAtIndexDelegate(index);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000D5E2 File Offset: 0x0000B7E2
		public int GetResolutionCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetResolutionCountDelegate();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000D5EE File Offset: 0x0000B7EE
		public float GetRGLConfig(int type)
		{
			return ScriptingInterfaceOfIConfig.call_GetRGLConfigDelegate(type);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000D5FB File Offset: 0x0000B7FB
		public float GetRGLConfigForDefaultSettings(int type, int defaultSettings)
		{
			return ScriptingInterfaceOfIConfig.call_GetRGLConfigForDefaultSettingsDelegate(type, defaultSettings);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000D609 File Offset: 0x0000B809
		public int GetSoundDeviceCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetSoundDeviceCountDelegate();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000D615 File Offset: 0x0000B815
		public string GetSoundDeviceName(int i)
		{
			if (ScriptingInterfaceOfIConfig.call_GetSoundDeviceNameDelegate(i) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000D62C File Offset: 0x0000B82C
		public bool GetTableauCacheMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetTableauCacheModeDelegate();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000D638 File Offset: 0x0000B838
		public bool GetUIDebugMode()
		{
			return ScriptingInterfaceOfIConfig.call_GetUIDebugModeDelegate();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000D644 File Offset: 0x0000B844
		public bool GetUIDoNotUseGeneratedPrefabs()
		{
			return ScriptingInterfaceOfIConfig.call_GetUIDoNotUseGeneratedPrefabsDelegate();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000D650 File Offset: 0x0000B850
		public int GetVideoDeviceCount()
		{
			return ScriptingInterfaceOfIConfig.call_GetVideoDeviceCountDelegate();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000D65C File Offset: 0x0000B85C
		public string GetVideoDeviceName(int i)
		{
			if (ScriptingInterfaceOfIConfig.call_GetVideoDeviceNameDelegate(i) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000D673 File Offset: 0x0000B873
		public bool Is120HzAvailable()
		{
			return ScriptingInterfaceOfIConfig.call_Is120HzAvailableDelegate();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000D67F File Offset: 0x0000B87F
		public bool IsDlssAvailable()
		{
			return ScriptingInterfaceOfIConfig.call_IsDlssAvailableDelegate();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000D68B File Offset: 0x0000B88B
		public void ReadRGLConfigFiles()
		{
			ScriptingInterfaceOfIConfig.call_ReadRGLConfigFilesDelegate();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000D697 File Offset: 0x0000B897
		public void RefreshOptionsData()
		{
			ScriptingInterfaceOfIConfig.call_RefreshOptionsDataDelegate();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000D6A3 File Offset: 0x0000B8A3
		public int SaveRGLConfig()
		{
			return ScriptingInterfaceOfIConfig.call_SaveRGLConfigDelegate();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		public void SetAutoConfigWrtHardware()
		{
			ScriptingInterfaceOfIConfig.call_SetAutoConfigWrtHardwareDelegate();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000D6BB File Offset: 0x0000B8BB
		public void SetBrightness(float brightness)
		{
			ScriptingInterfaceOfIConfig.call_SetBrightnessDelegate(brightness);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public void SetCustomResolution(int width, int height)
		{
			ScriptingInterfaceOfIConfig.call_SetCustomResolutionDelegate(width, height);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000D6D6 File Offset: 0x0000B8D6
		public void SetDefaultGameConfig()
		{
			ScriptingInterfaceOfIConfig.call_SetDefaultGameConfigDelegate();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
		public void SetRGLConfig(int type, float value)
		{
			ScriptingInterfaceOfIConfig.call_SetRGLConfigDelegate(type, value);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public void SetSharpenAmount(float sharpen_amount)
		{
			ScriptingInterfaceOfIConfig.call_SetSharpenAmountDelegate(sharpen_amount);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		public void SetSoundDevice(int i)
		{
			ScriptingInterfaceOfIConfig.call_SetSoundDeviceDelegate(i);
		}

		// Token: 0x0400003E RID: 62
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400003F RID: 63
		public static ScriptingInterfaceOfIConfig.ApplyDelegate call_ApplyDelegate;

		// Token: 0x04000040 RID: 64
		public static ScriptingInterfaceOfIConfig.ApplyConfigChangesDelegate call_ApplyConfigChangesDelegate;

		// Token: 0x04000041 RID: 65
		public static ScriptingInterfaceOfIConfig.AutoSaveInMinutesDelegate call_AutoSaveInMinutesDelegate;

		// Token: 0x04000042 RID: 66
		public static ScriptingInterfaceOfIConfig.CheckGFXSupportStatusDelegate call_CheckGFXSupportStatusDelegate;

		// Token: 0x04000043 RID: 67
		public static ScriptingInterfaceOfIConfig.GetAutoGFXQualityDelegate call_GetAutoGFXQualityDelegate;

		// Token: 0x04000044 RID: 68
		public static ScriptingInterfaceOfIConfig.GetCharacterDetailDelegate call_GetCharacterDetailDelegate;

		// Token: 0x04000045 RID: 69
		public static ScriptingInterfaceOfIConfig.GetCheatModeDelegate call_GetCheatModeDelegate;

		// Token: 0x04000046 RID: 70
		public static ScriptingInterfaceOfIConfig.GetCurrentSoundDeviceIndexDelegate call_GetCurrentSoundDeviceIndexDelegate;

		// Token: 0x04000047 RID: 71
		public static ScriptingInterfaceOfIConfig.GetDebugLoginPasswordDelegate call_GetDebugLoginPasswordDelegate;

		// Token: 0x04000048 RID: 72
		public static ScriptingInterfaceOfIConfig.GetDebugLoginUserNameDelegate call_GetDebugLoginUserNameDelegate;

		// Token: 0x04000049 RID: 73
		public static ScriptingInterfaceOfIConfig.GetDefaultRGLConfigDelegate call_GetDefaultRGLConfigDelegate;

		// Token: 0x0400004A RID: 74
		public static ScriptingInterfaceOfIConfig.GetDesktopResolutionDelegate call_GetDesktopResolutionDelegate;

		// Token: 0x0400004B RID: 75
		public static ScriptingInterfaceOfIConfig.GetDevelopmentModeDelegate call_GetDevelopmentModeDelegate;

		// Token: 0x0400004C RID: 76
		public static ScriptingInterfaceOfIConfig.GetDisableGuiMessagesDelegate call_GetDisableGuiMessagesDelegate;

		// Token: 0x0400004D RID: 77
		public static ScriptingInterfaceOfIConfig.GetDisableSoundDelegate call_GetDisableSoundDelegate;

		// Token: 0x0400004E RID: 78
		public static ScriptingInterfaceOfIConfig.GetDlssOptionCountDelegate call_GetDlssOptionCountDelegate;

		// Token: 0x0400004F RID: 79
		public static ScriptingInterfaceOfIConfig.GetDlssTechniqueDelegate call_GetDlssTechniqueDelegate;

		// Token: 0x04000050 RID: 80
		public static ScriptingInterfaceOfIConfig.GetDoLocalizationCheckAtStartupDelegate call_GetDoLocalizationCheckAtStartupDelegate;

		// Token: 0x04000051 RID: 81
		public static ScriptingInterfaceOfIConfig.GetEnableClothSimulationDelegate call_GetEnableClothSimulationDelegate;

		// Token: 0x04000052 RID: 82
		public static ScriptingInterfaceOfIConfig.GetEnableEditModeDelegate call_GetEnableEditModeDelegate;

		// Token: 0x04000053 RID: 83
		public static ScriptingInterfaceOfIConfig.GetInvertMouseDelegate call_GetInvertMouseDelegate;

		// Token: 0x04000054 RID: 84
		public static ScriptingInterfaceOfIConfig.GetLastOpenedSceneDelegate call_GetLastOpenedSceneDelegate;

		// Token: 0x04000055 RID: 85
		public static ScriptingInterfaceOfIConfig.GetLocalizationDebugModeDelegate call_GetLocalizationDebugModeDelegate;

		// Token: 0x04000056 RID: 86
		public static ScriptingInterfaceOfIConfig.GetMonitorDeviceCountDelegate call_GetMonitorDeviceCountDelegate;

		// Token: 0x04000057 RID: 87
		public static ScriptingInterfaceOfIConfig.GetMonitorDeviceNameDelegate call_GetMonitorDeviceNameDelegate;

		// Token: 0x04000058 RID: 88
		public static ScriptingInterfaceOfIConfig.GetRefreshRateAtIndexDelegate call_GetRefreshRateAtIndexDelegate;

		// Token: 0x04000059 RID: 89
		public static ScriptingInterfaceOfIConfig.GetRefreshRateCountDelegate call_GetRefreshRateCountDelegate;

		// Token: 0x0400005A RID: 90
		public static ScriptingInterfaceOfIConfig.GetResolutionDelegate call_GetResolutionDelegate;

		// Token: 0x0400005B RID: 91
		public static ScriptingInterfaceOfIConfig.GetResolutionAtIndexDelegate call_GetResolutionAtIndexDelegate;

		// Token: 0x0400005C RID: 92
		public static ScriptingInterfaceOfIConfig.GetResolutionCountDelegate call_GetResolutionCountDelegate;

		// Token: 0x0400005D RID: 93
		public static ScriptingInterfaceOfIConfig.GetRGLConfigDelegate call_GetRGLConfigDelegate;

		// Token: 0x0400005E RID: 94
		public static ScriptingInterfaceOfIConfig.GetRGLConfigForDefaultSettingsDelegate call_GetRGLConfigForDefaultSettingsDelegate;

		// Token: 0x0400005F RID: 95
		public static ScriptingInterfaceOfIConfig.GetSoundDeviceCountDelegate call_GetSoundDeviceCountDelegate;

		// Token: 0x04000060 RID: 96
		public static ScriptingInterfaceOfIConfig.GetSoundDeviceNameDelegate call_GetSoundDeviceNameDelegate;

		// Token: 0x04000061 RID: 97
		public static ScriptingInterfaceOfIConfig.GetTableauCacheModeDelegate call_GetTableauCacheModeDelegate;

		// Token: 0x04000062 RID: 98
		public static ScriptingInterfaceOfIConfig.GetUIDebugModeDelegate call_GetUIDebugModeDelegate;

		// Token: 0x04000063 RID: 99
		public static ScriptingInterfaceOfIConfig.GetUIDoNotUseGeneratedPrefabsDelegate call_GetUIDoNotUseGeneratedPrefabsDelegate;

		// Token: 0x04000064 RID: 100
		public static ScriptingInterfaceOfIConfig.GetVideoDeviceCountDelegate call_GetVideoDeviceCountDelegate;

		// Token: 0x04000065 RID: 101
		public static ScriptingInterfaceOfIConfig.GetVideoDeviceNameDelegate call_GetVideoDeviceNameDelegate;

		// Token: 0x04000066 RID: 102
		public static ScriptingInterfaceOfIConfig.Is120HzAvailableDelegate call_Is120HzAvailableDelegate;

		// Token: 0x04000067 RID: 103
		public static ScriptingInterfaceOfIConfig.IsDlssAvailableDelegate call_IsDlssAvailableDelegate;

		// Token: 0x04000068 RID: 104
		public static ScriptingInterfaceOfIConfig.ReadRGLConfigFilesDelegate call_ReadRGLConfigFilesDelegate;

		// Token: 0x04000069 RID: 105
		public static ScriptingInterfaceOfIConfig.RefreshOptionsDataDelegate call_RefreshOptionsDataDelegate;

		// Token: 0x0400006A RID: 106
		public static ScriptingInterfaceOfIConfig.SaveRGLConfigDelegate call_SaveRGLConfigDelegate;

		// Token: 0x0400006B RID: 107
		public static ScriptingInterfaceOfIConfig.SetAutoConfigWrtHardwareDelegate call_SetAutoConfigWrtHardwareDelegate;

		// Token: 0x0400006C RID: 108
		public static ScriptingInterfaceOfIConfig.SetBrightnessDelegate call_SetBrightnessDelegate;

		// Token: 0x0400006D RID: 109
		public static ScriptingInterfaceOfIConfig.SetCustomResolutionDelegate call_SetCustomResolutionDelegate;

		// Token: 0x0400006E RID: 110
		public static ScriptingInterfaceOfIConfig.SetDefaultGameConfigDelegate call_SetDefaultGameConfigDelegate;

		// Token: 0x0400006F RID: 111
		public static ScriptingInterfaceOfIConfig.SetRGLConfigDelegate call_SetRGLConfigDelegate;

		// Token: 0x04000070 RID: 112
		public static ScriptingInterfaceOfIConfig.SetSharpenAmountDelegate call_SetSharpenAmountDelegate;

		// Token: 0x04000071 RID: 113
		public static ScriptingInterfaceOfIConfig.SetSoundDeviceDelegate call_SetSoundDeviceDelegate;

		// Token: 0x020000AB RID: 171
		// (Invoke) Token: 0x060007AF RID: 1967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyDelegate(int texture_budget, int sharpen_amount, int hdr, int dof_mode, int motion_blur, int ssr, int size, int texture_filtering, int trail_amount, int dynamic_resolution_target);

		// Token: 0x020000AC RID: 172
		// (Invoke) Token: 0x060007B3 RID: 1971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyConfigChangesDelegate([MarshalAs(UnmanagedType.U1)] bool resizeWindow);

		// Token: 0x020000AD RID: 173
		// (Invoke) Token: 0x060007B7 RID: 1975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AutoSaveInMinutesDelegate();

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x060007BB RID: 1979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckGFXSupportStatusDelegate(int enum_id);

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x060007BF RID: 1983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAutoGFXQualityDelegate();

		// Token: 0x020000B0 RID: 176
		// (Invoke) Token: 0x060007C3 RID: 1987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetCharacterDetailDelegate();

		// Token: 0x020000B1 RID: 177
		// (Invoke) Token: 0x060007C7 RID: 1991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetCheatModeDelegate();

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060007CB RID: 1995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetCurrentSoundDeviceIndexDelegate();

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x060007CF RID: 1999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDebugLoginPasswordDelegate();

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x060007D3 RID: 2003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDebugLoginUserNameDelegate();

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x060007D7 RID: 2007
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetDefaultRGLConfigDelegate(int type);

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x060007DB RID: 2011
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetDesktopResolutionDelegate(ref int width, ref int height);

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x060007DF RID: 2015
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetDevelopmentModeDelegate();

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x060007E3 RID: 2019
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetDisableGuiMessagesDelegate();

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x060007E7 RID: 2023
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetDisableSoundDelegate();

		// Token: 0x020000BA RID: 186
		// (Invoke) Token: 0x060007EB RID: 2027
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDlssOptionCountDelegate();

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x060007EF RID: 2031
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDlssTechniqueDelegate();

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060007F3 RID: 2035
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetDoLocalizationCheckAtStartupDelegate();

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060007F7 RID: 2039
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetEnableClothSimulationDelegate();

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x060007FB RID: 2043
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetEnableEditModeDelegate();

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x060007FF RID: 2047
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetInvertMouseDelegate();

		// Token: 0x020000C0 RID: 192
		// (Invoke) Token: 0x06000803 RID: 2051
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetLastOpenedSceneDelegate();

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x06000807 RID: 2055
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetLocalizationDebugModeDelegate();

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x0600080B RID: 2059
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMonitorDeviceCountDelegate();

		// Token: 0x020000C3 RID: 195
		// (Invoke) Token: 0x0600080F RID: 2063
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMonitorDeviceNameDelegate(int i);

		// Token: 0x020000C4 RID: 196
		// (Invoke) Token: 0x06000813 RID: 2067
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRefreshRateAtIndexDelegate(int index);

		// Token: 0x020000C5 RID: 197
		// (Invoke) Token: 0x06000817 RID: 2071
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRefreshRateCountDelegate();

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x0600081B RID: 2075
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetResolutionDelegate(ref int width, ref int height);

		// Token: 0x020000C7 RID: 199
		// (Invoke) Token: 0x0600081F RID: 2079
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetResolutionAtIndexDelegate(int index);

		// Token: 0x020000C8 RID: 200
		// (Invoke) Token: 0x06000823 RID: 2083
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetResolutionCountDelegate();

		// Token: 0x020000C9 RID: 201
		// (Invoke) Token: 0x06000827 RID: 2087
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRGLConfigDelegate(int type);

		// Token: 0x020000CA RID: 202
		// (Invoke) Token: 0x0600082B RID: 2091
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRGLConfigForDefaultSettingsDelegate(int type, int defaultSettings);

		// Token: 0x020000CB RID: 203
		// (Invoke) Token: 0x0600082F RID: 2095
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSoundDeviceCountDelegate();

		// Token: 0x020000CC RID: 204
		// (Invoke) Token: 0x06000833 RID: 2099
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSoundDeviceNameDelegate(int i);

		// Token: 0x020000CD RID: 205
		// (Invoke) Token: 0x06000837 RID: 2103
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetTableauCacheModeDelegate();

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x0600083B RID: 2107
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetUIDebugModeDelegate();

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x0600083F RID: 2111
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetUIDoNotUseGeneratedPrefabsDelegate();

		// Token: 0x020000D0 RID: 208
		// (Invoke) Token: 0x06000843 RID: 2115
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVideoDeviceCountDelegate();

		// Token: 0x020000D1 RID: 209
		// (Invoke) Token: 0x06000847 RID: 2119
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVideoDeviceNameDelegate(int i);

		// Token: 0x020000D2 RID: 210
		// (Invoke) Token: 0x0600084B RID: 2123
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool Is120HzAvailableDelegate();

		// Token: 0x020000D3 RID: 211
		// (Invoke) Token: 0x0600084F RID: 2127
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsDlssAvailableDelegate();

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x06000853 RID: 2131
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReadRGLConfigFilesDelegate();

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x06000857 RID: 2135
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RefreshOptionsDataDelegate();

		// Token: 0x020000D6 RID: 214
		// (Invoke) Token: 0x0600085B RID: 2139
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SaveRGLConfigDelegate();

		// Token: 0x020000D7 RID: 215
		// (Invoke) Token: 0x0600085F RID: 2143
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAutoConfigWrtHardwareDelegate();

		// Token: 0x020000D8 RID: 216
		// (Invoke) Token: 0x06000863 RID: 2147
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBrightnessDelegate(float brightness);

		// Token: 0x020000D9 RID: 217
		// (Invoke) Token: 0x06000867 RID: 2151
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCustomResolutionDelegate(int width, int height);

		// Token: 0x020000DA RID: 218
		// (Invoke) Token: 0x0600086B RID: 2155
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDefaultGameConfigDelegate();

		// Token: 0x020000DB RID: 219
		// (Invoke) Token: 0x0600086F RID: 2159
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRGLConfigDelegate(int type, float value);

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x06000873 RID: 2163
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSharpenAmountDelegate(float sharpen_amount);

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x06000877 RID: 2167
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSoundDeviceDelegate(int i);
	}
}
