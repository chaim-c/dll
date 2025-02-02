using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000014 RID: 20
	[NullableContext(2)]
	[Nullable(0)]
	internal static class HarmonyExtensions
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000052BC File Offset: 0x000034BC
		public static bool TryPatch([Nullable(1)] this Harmony harmony, MethodBase original, MethodInfo prefix = null, MethodInfo postfix = null, MethodInfo transpiler = null, MethodInfo finalizer = null)
		{
			bool flag = original == null || (prefix == null && postfix == null && transpiler == null && finalizer == null);
			bool result;
			if (flag)
			{
				Trace.TraceError("HarmonyExtensions.TryPatch: 'original' or all methods are null");
				result = false;
			}
			else
			{
				HarmonyMethod prefixMethod = (prefix == null) ? null : new HarmonyMethod(prefix);
				HarmonyMethod postfixMethod = (postfix == null) ? null : new HarmonyMethod(postfix);
				HarmonyMethod transpilerMethod = (transpiler == null) ? null : new HarmonyMethod(transpiler);
				HarmonyMethod finalizerMethod = (finalizer == null) ? null : new HarmonyMethod(finalizer);
				try
				{
					harmony.Patch(original, prefixMethod, postfixMethod, transpilerMethod, finalizerMethod);
				}
				catch (Exception e)
				{
					Trace.TraceError(string.Format("HarmonyExtensions.TryPatch: Exception occurred: {0}, original '{1}'", e, original));
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005374 File Offset: 0x00003574
		public static ReversePatcher TryCreateReversePatcher([Nullable(1)] this Harmony harmony, MethodBase original, MethodInfo standin)
		{
			bool flag = original == null || standin == null;
			ReversePatcher result;
			if (flag)
			{
				Trace.TraceError("HarmonyExtensions.TryCreateReversePatcher: 'original' or 'standin' is null");
				result = null;
			}
			else
			{
				try
				{
					result = harmony.CreateReversePatcher(original, new HarmonyMethod(standin));
				}
				catch (Exception e)
				{
					Trace.TraceError(string.Format("HarmonyExtensions.TryCreateReversePatcher: Exception occurred: {0}, original '{1}'", e, original));
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000053DC File Offset: 0x000035DC
		public static bool TryCreateReversePatcher([Nullable(1)] this Harmony harmony, MethodBase original, MethodInfo standin, out ReversePatcher result)
		{
			bool flag = original == null || standin == null;
			bool result2;
			if (flag)
			{
				Trace.TraceError("HarmonyExtensions.TryCreateReversePatcher: 'original' or 'standin' is null");
				result = null;
				result2 = false;
			}
			else
			{
				try
				{
					result = harmony.CreateReversePatcher(original, new HarmonyMethod(standin));
					result2 = true;
				}
				catch (Exception e)
				{
					Trace.TraceError(string.Format("HarmonyExtensions.TryCreateReversePatcher: Exception occurred: {0}, original '{1}'", e, original));
					result = null;
					result2 = false;
				}
			}
			return result2;
		}
	}
}
