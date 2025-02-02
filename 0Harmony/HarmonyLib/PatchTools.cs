using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using MonoMod.Core;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x0200001C RID: 28
	internal static class PatchTools
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00008B9C File Offset: 0x00006D9C
		internal static void DetourMethod(MethodBase method, MethodBase replacement)
		{
			Dictionary<MethodBase, ICoreDetour> obj = PatchTools.detours;
			lock (obj)
			{
				ICoreDetour coreDetour;
				if (PatchTools.detours.TryGetValue(method, out coreDetour))
				{
					coreDetour.Dispose();
				}
				PatchTools.detours[method] = DetourFactory.Current.CreateDetour(method, replacement, true);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008C04 File Offset: 0x00006E04
		private static Assembly GetExecutingAssemblyReplacement()
		{
			StackFrame[] frames = new StackTrace().GetFrames();
			StackFrame stackFrame = (frames != null) ? frames.Skip(1).FirstOrDefault<StackFrame>() : null;
			if (stackFrame != null)
			{
				MethodBase methodFromStackframe = Harmony.GetMethodFromStackframe(stackFrame);
				if (methodFromStackframe != null)
				{
					return methodFromStackframe.Module.Assembly;
				}
			}
			return Assembly.GetExecutingAssembly();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008C4D File Offset: 0x00006E4D
		internal static IEnumerable<CodeInstruction> GetExecutingAssemblyTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			return instructions.MethodReplacer(PatchTools.m_GetExecutingAssembly, PatchTools.m_GetExecutingAssemblyReplacement);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008C60 File Offset: 0x00006E60
		public static MethodInfo CreateMethod(string name, Type returnType, List<KeyValuePair<string, Type>> parameters, Action<ILGenerator> generator)
		{
			Type[] parameterTypes = (from p in parameters
			select p.Value).ToArray<Type>();
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(name, returnType, parameterTypes);
			for (int i = 0; i < parameters.Count; i++)
			{
				dynamicMethodDefinition.Definition.Parameters[i].Name = parameters[i].Key;
			}
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			generator(ilgenerator);
			return dynamicMethodDefinition.Generate();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008CEC File Offset: 0x00006EEC
		internal static MethodInfo GetPatchMethod(Type patchType, string attributeName)
		{
			Func<object, bool> <>9__1;
			MethodInfo methodInfo = patchType.GetMethods(AccessTools.all).FirstOrDefault(delegate(MethodInfo m)
			{
				IEnumerable<object> customAttributes = m.GetCustomAttributes(true);
				Func<object, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((object a) => a.GetType().FullName == attributeName));
				}
				return customAttributes.Any(predicate);
			});
			if (methodInfo == null)
			{
				string name = attributeName.Replace("HarmonyLib.Harmony", "");
				methodInfo = patchType.GetMethod(name, AccessTools.all);
			}
			return methodInfo;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00008D4C File Offset: 0x00006F4C
		internal static AssemblyBuilder DefineDynamicAssembly(string name)
		{
			AssemblyName name2 = new AssemblyName(name);
			return AppDomain.CurrentDomain.DefineDynamicAssembly(name2, AssemblyBuilderAccess.Run);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008D6C File Offset: 0x00006F6C
		internal static List<AttributePatch> GetPatchMethods(Type type)
		{
			IEnumerable<MethodInfo> declaredMethods = AccessTools.GetDeclaredMethods(type);
			Func<MethodInfo, AttributePatch> selector;
			if ((selector = PatchTools.<>O.<0>__Create) == null)
			{
				selector = (PatchTools.<>O.<0>__Create = new Func<MethodInfo, AttributePatch>(AttributePatch.Create));
			}
			return (from attributePatch in declaredMethods.Select(selector)
			where attributePatch != null
			select attributePatch).ToList<AttributePatch>();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008DC8 File Offset: 0x00006FC8
		internal static MethodBase GetOriginalMethod(this HarmonyMethod attr)
		{
			try
			{
				MethodType? methodType = attr.methodType;
				if (methodType != null)
				{
					switch (methodType.GetValueOrDefault())
					{
					case MethodType.Normal:
						if (attr.methodName == null)
						{
							return null;
						}
						return AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes, null);
					case MethodType.Getter:
						if (attr.methodName == null)
						{
							return AccessTools.DeclaredIndexer(attr.declaringType, attr.argumentTypes).GetGetMethod(true);
						}
						return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetGetMethod(true);
					case MethodType.Setter:
						if (attr.methodName == null)
						{
							return AccessTools.DeclaredIndexer(attr.declaringType, attr.argumentTypes).GetSetMethod(true);
						}
						return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetSetMethod(true);
					case MethodType.Constructor:
						return AccessTools.DeclaredConstructor(attr.declaringType, attr.argumentTypes, false);
					case MethodType.StaticConstructor:
						return (from c in AccessTools.GetDeclaredConstructors(attr.declaringType, null)
						where c.IsStatic
						select c).FirstOrDefault<ConstructorInfo>();
					case MethodType.Enumerator:
					{
						if (attr.methodName == null)
						{
							return null;
						}
						MethodInfo method = AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes, null);
						return AccessTools.EnumeratorMoveNext(method);
					}
					case MethodType.Async:
					{
						if (attr.methodName == null)
						{
							return null;
						}
						MethodInfo method2 = AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes, null);
						return AccessTools.AsyncMoveNext(method2);
					}
					}
				}
			}
			catch (AmbiguousMatchException ex)
			{
				throw new HarmonyException("Ambiguous match for HarmonyMethod[" + attr.Description() + "]", ex.InnerException ?? ex);
			}
			return null;
		}

		// Token: 0x04000055 RID: 85
		private static readonly Dictionary<MethodBase, ICoreDetour> detours = new Dictionary<MethodBase, ICoreDetour>();

		// Token: 0x04000056 RID: 86
		internal static readonly string harmonyMethodFullName = typeof(HarmonyMethod).FullName;

		// Token: 0x04000057 RID: 87
		internal static readonly string harmonyAttributeFullName = typeof(HarmonyAttribute).FullName;

		// Token: 0x04000058 RID: 88
		internal static readonly string harmonyPatchAllFullName = typeof(HarmonyPatchAll).FullName;

		// Token: 0x04000059 RID: 89
		internal static readonly MethodInfo m_GetExecutingAssemblyReplacementTranspiler = SymbolExtensions.GetMethodInfo(Expression.Lambda<Action>(Expression.Call(null, methodof(PatchTools.GetExecutingAssemblyTranspiler(IEnumerable<CodeInstruction>)), new Expression[]
		{
			Expression.Constant(null, typeof(IEnumerable<CodeInstruction>))
		}), Array.Empty<ParameterExpression>()));

		// Token: 0x0400005A RID: 90
		internal static readonly MethodInfo m_GetExecutingAssembly = SymbolExtensions.GetMethodInfo(Expression.Lambda<Action>(Expression.Call(null, methodof(Assembly.GetExecutingAssembly()), Array.Empty<Expression>()), Array.Empty<ParameterExpression>()));

		// Token: 0x0400005B RID: 91
		internal static readonly MethodInfo m_GetExecutingAssemblyReplacement = SymbolExtensions.GetMethodInfo(Expression.Lambda<Action>(Expression.Call(null, methodof(PatchTools.GetExecutingAssemblyReplacement()), Array.Empty<Expression>()), Array.Empty<ParameterExpression>()));

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400017B RID: 379
			public static Func<MethodInfo, AttributePatch> <0>__Create;
		}
	}
}
