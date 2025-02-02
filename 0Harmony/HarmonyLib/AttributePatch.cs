using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200001A RID: 26
	internal class AttributePatch
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00008560 File Offset: 0x00006760
		internal static AttributePatch Create(MethodInfo patch)
		{
			if (patch == null)
			{
				throw new NullReferenceException("Patch method cannot be null");
			}
			object[] customAttributes = patch.GetCustomAttributes(true);
			string name = patch.Name;
			HarmonyPatchType? patchType = AttributePatch.GetPatchType(name, customAttributes);
			if (patchType == null)
			{
				return null;
			}
			HarmonyPatchType? harmonyPatchType = patchType;
			HarmonyPatchType harmonyPatchType2 = HarmonyPatchType.ReversePatch;
			if (!(harmonyPatchType.GetValueOrDefault() == harmonyPatchType2 & harmonyPatchType != null) && !patch.IsStatic)
			{
				throw new ArgumentException("Patch method " + patch.FullDescription() + " must be static");
			}
			IEnumerable<object> source = (from attr in customAttributes
			where attr.GetType().BaseType.FullName == PatchTools.harmonyAttributeFullName
			select attr).Select(delegate(object attr)
			{
				FieldInfo fieldInfo = AccessTools.Field(attr.GetType(), "info");
				return fieldInfo.GetValue(attr);
			});
			Func<object, HarmonyMethod> selector;
			if ((selector = AttributePatch.<>O.<0>__MakeDeepCopy) == null)
			{
				selector = (AttributePatch.<>O.<0>__MakeDeepCopy = new Func<object, HarmonyMethod>(AccessTools.MakeDeepCopy<HarmonyMethod>));
			}
			List<HarmonyMethod> attributes = source.Select(selector).ToList<HarmonyMethod>();
			HarmonyMethod harmonyMethod = HarmonyMethod.Merge(attributes);
			harmonyMethod.method = patch;
			return new AttributePatch
			{
				info = harmonyMethod,
				type = patchType
			};
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008670 File Offset: 0x00006870
		private static HarmonyPatchType? GetPatchType(string methodName, object[] allAttributes)
		{
			HashSet<string> hashSet = new HashSet<string>(from attr in allAttributes
			select attr.GetType().FullName into name
			where name.StartsWith("Harmony")
			select name);
			HarmonyPatchType? result = null;
			foreach (HarmonyPatchType value in AttributePatch.allPatchTypes)
			{
				string text = value.ToString();
				if (text == methodName || hashSet.Contains("HarmonyLib.Harmony" + text))
				{
					result = new HarmonyPatchType?(value);
					break;
				}
			}
			return result;
		}

		// Token: 0x0400004C RID: 76
		private static readonly HarmonyPatchType[] allPatchTypes = new HarmonyPatchType[]
		{
			HarmonyPatchType.Prefix,
			HarmonyPatchType.Postfix,
			HarmonyPatchType.Transpiler,
			HarmonyPatchType.Finalizer,
			HarmonyPatchType.ReversePatch
		};

		// Token: 0x0400004D RID: 77
		internal HarmonyMethod info;

		// Token: 0x0400004E RID: 78
		internal HarmonyPatchType? type;

		// Token: 0x02000072 RID: 114
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400016A RID: 362
			public static Func<object, HarmonyMethod> <0>__MakeDeepCopy;
		}
	}
}
