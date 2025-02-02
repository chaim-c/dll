using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000017 RID: 23
	internal static class PatchArgumentExtensions
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00008058 File Offset: 0x00006258
		private static HarmonyArgument[] AllHarmonyArguments(object[] attributes)
		{
			return (from harg in attributes.Select(delegate(object attr)
			{
				if (attr.GetType().Name != "HarmonyArgument")
				{
					return null;
				}
				return AccessTools.MakeDeepCopy<HarmonyArgument>(attr);
			})
			where harg != null
			select harg).ToArray<HarmonyArgument>();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000080B4 File Offset: 0x000062B4
		private static HarmonyArgument GetArgumentAttribute(this ParameterInfo parameter)
		{
			object[] customAttributes = parameter.GetCustomAttributes(false);
			return PatchArgumentExtensions.AllHarmonyArguments(customAttributes).FirstOrDefault<HarmonyArgument>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000080D4 File Offset: 0x000062D4
		private static HarmonyArgument[] GetArgumentAttributes(this MethodInfo method)
		{
			if (method == null || method is DynamicMethod)
			{
				return null;
			}
			object[] customAttributes = method.GetCustomAttributes(false);
			return PatchArgumentExtensions.AllHarmonyArguments(customAttributes);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000080FC File Offset: 0x000062FC
		private static HarmonyArgument[] GetArgumentAttributes(this Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(false);
			return PatchArgumentExtensions.AllHarmonyArguments(customAttributes);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008118 File Offset: 0x00006318
		private static string GetOriginalArgumentName(this ParameterInfo parameter, string[] originalParameterNames)
		{
			HarmonyArgument argumentAttribute = parameter.GetArgumentAttribute();
			if (argumentAttribute == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(argumentAttribute.OriginalName))
			{
				return argumentAttribute.OriginalName;
			}
			if (argumentAttribute.Index >= 0 && argumentAttribute.Index < originalParameterNames.Length)
			{
				return originalParameterNames[argumentAttribute.Index];
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008164 File Offset: 0x00006364
		private static string GetOriginalArgumentName(HarmonyArgument[] attributes, string name, string[] originalParameterNames)
		{
			if (((attributes != null) ? attributes.Length : 0) <= 0)
			{
				return null;
			}
			HarmonyArgument harmonyArgument = attributes.SingleOrDefault((HarmonyArgument p) => p.NewName == name);
			if (harmonyArgument == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(harmonyArgument.OriginalName))
			{
				return harmonyArgument.OriginalName;
			}
			if (originalParameterNames != null && harmonyArgument.Index >= 0 && harmonyArgument.Index < originalParameterNames.Length)
			{
				return originalParameterNames[harmonyArgument.Index];
			}
			return null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000081DC File Offset: 0x000063DC
		private static string GetOriginalArgumentName(this MethodInfo method, string[] originalParameterNames, string name)
		{
			string originalArgumentName = PatchArgumentExtensions.GetOriginalArgumentName((method != null) ? method.GetArgumentAttributes() : null, name, originalParameterNames);
			if (originalArgumentName != null)
			{
				return originalArgumentName;
			}
			HarmonyArgument[] attributes;
			if (method == null)
			{
				attributes = null;
			}
			else
			{
				Type declaringType = method.DeclaringType;
				attributes = ((declaringType != null) ? declaringType.GetArgumentAttributes() : null);
			}
			originalArgumentName = PatchArgumentExtensions.GetOriginalArgumentName(attributes, name, originalParameterNames);
			if (originalArgumentName != null)
			{
				return originalArgumentName;
			}
			return name;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008228 File Offset: 0x00006428
		internal static int GetArgumentIndex(this MethodInfo patch, string[] originalParameterNames, ParameterInfo patchParam)
		{
			if (patch is DynamicMethod)
			{
				return Array.IndexOf<string>(originalParameterNames, patchParam.Name);
			}
			string originalArgumentName = patchParam.GetOriginalArgumentName(originalParameterNames);
			if (originalArgumentName != null)
			{
				return Array.IndexOf<string>(originalParameterNames, originalArgumentName);
			}
			originalArgumentName = patch.GetOriginalArgumentName(originalParameterNames, patchParam.Name);
			if (originalArgumentName != null)
			{
				return Array.IndexOf<string>(originalParameterNames, originalArgumentName);
			}
			return -1;
		}
	}
}
