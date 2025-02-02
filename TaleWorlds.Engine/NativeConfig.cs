using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006E RID: 110
	public static class NativeConfig
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000088EA File Offset: 0x00006AEA
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x000088F1 File Offset: 0x00006AF1
		public static bool CheatMode { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000088F9 File Offset: 0x00006AF9
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00008900 File Offset: 0x00006B00
		public static bool IsDevelopmentMode { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00008908 File Offset: 0x00006B08
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0000890F File Offset: 0x00006B0F
		public static bool LocalizationDebugMode { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00008917 File Offset: 0x00006B17
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0000891E File Offset: 0x00006B1E
		public static bool GetUIDebugMode { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00008926 File Offset: 0x00006B26
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000892D File Offset: 0x00006B2D
		public static bool DisableSound { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00008935 File Offset: 0x00006B35
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0000893C File Offset: 0x00006B3C
		public static bool EnableEditMode { get; private set; }

		// Token: 0x0600089D RID: 2205 RVA: 0x00008944 File Offset: 0x00006B44
		public static void OnConfigChanged()
		{
			NativeConfig.CheatMode = EngineApplicationInterface.IConfig.GetCheatMode();
			NativeConfig.IsDevelopmentMode = EngineApplicationInterface.IConfig.GetDevelopmentMode();
			NativeConfig.GetUIDebugMode = EngineApplicationInterface.IConfig.GetUIDebugMode();
			NativeConfig.LocalizationDebugMode = EngineApplicationInterface.IConfig.GetLocalizationDebugMode();
			NativeConfig.EnableEditMode = EngineApplicationInterface.IConfig.GetEnableEditMode();
			NativeConfig.DisableSound = EngineApplicationInterface.IConfig.GetDisableSound();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x000089AB File Offset: 0x00006BAB
		public static bool TableauCacheEnabled
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetTableauCacheMode();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000089B7 File Offset: 0x00006BB7
		public static bool DoLocalizationCheckAtStartup
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetDoLocalizationCheckAtStartup();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000089C3 File Offset: 0x00006BC3
		public static bool EnableClothSimulation
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetEnableClothSimulation();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000089CF File Offset: 0x00006BCF
		public static int CharacterDetail
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetCharacterDetail();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000089DB File Offset: 0x00006BDB
		public static bool InvertMouse
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetInvertMouse();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000089E7 File Offset: 0x00006BE7
		public static string LastOpenedScene
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetLastOpenedScene();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000089F3 File Offset: 0x00006BF3
		public static int AutoSaveInMinutes
		{
			get
			{
				return EngineApplicationInterface.IConfig.AutoSaveInMinutes();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000089FF File Offset: 0x00006BFF
		public static bool GetUIDoNotUseGeneratedPrefabs
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetUIDoNotUseGeneratedPrefabs();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00008A0B File Offset: 0x00006C0B
		public static string DebugLoginUsername
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetDebugLoginUserName();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00008A17 File Offset: 0x00006C17
		public static string DebugLogicPassword
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetDebugLoginPassword();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00008A23 File Offset: 0x00006C23
		public static bool DisableGuiMessages
		{
			get
			{
				return EngineApplicationInterface.IConfig.GetDisableGuiMessages();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00008A2F File Offset: 0x00006C2F
		public static NativeOptions.ConfigQuality AutoGFXQuality
		{
			get
			{
				return (NativeOptions.ConfigQuality)EngineApplicationInterface.IConfig.GetAutoGFXQuality();
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00008A3B File Offset: 0x00006C3B
		public static void SetAutoConfigWrtHardware()
		{
			EngineApplicationInterface.IConfig.SetAutoConfigWrtHardware();
		}
	}
}
