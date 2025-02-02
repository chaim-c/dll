using System;
using System.Collections.Generic;

namespace HarmonyLib
{
	// Token: 0x0200004D RID: 77
	public static class CodeInstructionsExtensions
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00012EB7 File Offset: 0x000110B7
		public static bool Matches(this IEnumerable<CodeInstruction> instructions, CodeMatch[] matches)
		{
			return new CodeMatcher(instructions, null).MatchStartForward(matches).IsValid;
		}
	}
}
