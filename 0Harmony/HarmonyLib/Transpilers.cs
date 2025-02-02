using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000045 RID: 69
	public static class Transpilers
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000CE83 File Offset: 0x0000B083
		public static IEnumerable<CodeInstruction> MethodReplacer(this IEnumerable<CodeInstruction> instructions, MethodBase from, MethodBase to)
		{
			if (from == null)
			{
				throw new ArgumentException("Unexpected null argument", "from");
			}
			if (to == null)
			{
				throw new ArgumentException("Unexpected null argument", "to");
			}
			foreach (CodeInstruction codeInstruction in instructions)
			{
				MethodBase left = codeInstruction.operand as MethodBase;
				if (left == from)
				{
					codeInstruction.opcode = (to.IsConstructor ? OpCodes.Newobj : OpCodes.Call);
					codeInstruction.operand = to;
				}
				yield return codeInstruction;
			}
			IEnumerator<CodeInstruction> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public static IEnumerable<CodeInstruction> Manipulator(this IEnumerable<CodeInstruction> instructions, Func<CodeInstruction, bool> predicate, Action<CodeInstruction> action)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return instructions.Select(delegate(CodeInstruction instruction)
			{
				if (predicate(instruction))
				{
					action(instruction);
				}
				return instruction;
			}).AsEnumerable<CodeInstruction>();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CF02 File Offset: 0x0000B102
		public static IEnumerable<CodeInstruction> DebugLogger(this IEnumerable<CodeInstruction> instructions, string text)
		{
			yield return new CodeInstruction(OpCodes.Ldstr, text);
			yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FileLog), "Debug", null, null));
			foreach (CodeInstruction codeInstruction in instructions)
			{
				yield return codeInstruction;
			}
			IEnumerator<CodeInstruction> enumerator = null;
			yield break;
			yield break;
		}
	}
}
