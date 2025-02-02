using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.Engine.GauntletUI
{
	// Token: 0x02000007 RID: 7
	public static class UIConfig
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003090 File Offset: 0x00001290
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003097 File Offset: 0x00001297
		public static bool DoNotUseGeneratedPrefabs { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000030A6 File Offset: 0x000012A6
		public static bool DebugModeEnabled { get; set; }

		// Token: 0x06000047 RID: 71 RVA: 0x000030AE File Offset: 0x000012AE
		public static bool GetIsUsingGeneratedPrefabs()
		{
			return !NativeConfig.GetUIDoNotUseGeneratedPrefabs && !UIConfig.DoNotUseGeneratedPrefabs;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030C1 File Offset: 0x000012C1
		public static bool GetIsHotReloadEnabled()
		{
			return NativeConfig.GetUIDebugMode || UIConfig.DebugModeEnabled;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030D4 File Offset: 0x000012D4
		[CommandLineFunctionality.CommandLineArgumentFunction("set_debug_mode", "ui")]
		public static string SetDebugMode(List<string> args)
		{
			string result = "Format is \"ui.set_debug_mode [1/0]\".";
			if (args.Count != 1)
			{
				return result;
			}
			int num;
			if (int.TryParse(args[0], out num) && (num == 1 || num == 0))
			{
				UIConfig.DebugModeEnabled = (num == 1);
				return "Success.";
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000311C File Offset: 0x0000131C
		[CommandLineFunctionality.CommandLineArgumentFunction("use_generated_prefabs", "ui")]
		public static string SetUsingGeneratedPrefabs(List<string> args)
		{
			string result = "Format is \"ui.use_generated_prefabs [1/0].\"";
			if (args.Count != 1)
			{
				return result;
			}
			int num;
			if (int.TryParse(args[0], out num) && (num == 1 || num == 0))
			{
				UIConfig.DoNotUseGeneratedPrefabs = (num == 0);
				return "Success.";
			}
			return result;
		}
	}
}
