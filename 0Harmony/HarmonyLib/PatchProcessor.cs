using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000042 RID: 66
	public class PatchProcessor
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000C80D File Offset: 0x0000AA0D
		public PatchProcessor(Harmony instance, MethodBase original)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C823 File Offset: 0x0000AA23
		public PatchProcessor AddPrefix(HarmonyMethod prefix)
		{
			this.prefix = prefix;
			return this;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C82D File Offset: 0x0000AA2D
		public PatchProcessor AddPrefix(MethodInfo fixMethod)
		{
			this.prefix = new HarmonyMethod(fixMethod);
			return this;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C83C File Offset: 0x0000AA3C
		public PatchProcessor AddPostfix(HarmonyMethod postfix)
		{
			this.postfix = postfix;
			return this;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C846 File Offset: 0x0000AA46
		public PatchProcessor AddPostfix(MethodInfo fixMethod)
		{
			this.postfix = new HarmonyMethod(fixMethod);
			return this;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C855 File Offset: 0x0000AA55
		public PatchProcessor AddTranspiler(HarmonyMethod transpiler)
		{
			this.transpiler = transpiler;
			return this;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C85F File Offset: 0x0000AA5F
		public PatchProcessor AddTranspiler(MethodInfo fixMethod)
		{
			this.transpiler = new HarmonyMethod(fixMethod);
			return this;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C86E File Offset: 0x0000AA6E
		public PatchProcessor AddFinalizer(HarmonyMethod finalizer)
		{
			this.finalizer = finalizer;
			return this;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C878 File Offset: 0x0000AA78
		public PatchProcessor AddFinalizer(MethodInfo fixMethod)
		{
			this.finalizer = new HarmonyMethod(fixMethod);
			return this;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C888 File Offset: 0x0000AA88
		public static IEnumerable<MethodBase> GetAllPatchedMethods()
		{
			object obj = PatchProcessor.locker;
			IEnumerable<MethodBase> patchedMethods;
			lock (obj)
			{
				patchedMethods = HarmonySharedState.GetPatchedMethods();
			}
			return patchedMethods;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public MethodInfo Patch()
		{
			if (this.original == null)
			{
				throw new NullReferenceException("Null method for " + this.instance.Id);
			}
			if (!this.original.IsDeclaredMember<MethodBase>())
			{
				MethodBase declaredMember = this.original.GetDeclaredMember<MethodBase>();
				throw new ArgumentException("You can only patch implemented methods/constructors. Patch the declared method " + declaredMember.FullDescription() + " instead.");
			}
			object obj = PatchProcessor.locker;
			MethodInfo result;
			lock (obj)
			{
				PatchInfo patchInfo = HarmonySharedState.GetPatchInfo(this.original) ?? new PatchInfo();
				patchInfo.AddPrefixes(this.instance.Id, new HarmonyMethod[]
				{
					this.prefix
				});
				patchInfo.AddPostfixes(this.instance.Id, new HarmonyMethod[]
				{
					this.postfix
				});
				patchInfo.AddTranspilers(this.instance.Id, new HarmonyMethod[]
				{
					this.transpiler
				});
				patchInfo.AddFinalizers(this.instance.Id, new HarmonyMethod[]
				{
					this.finalizer
				});
				MethodInfo methodInfo = PatchFunctions.UpdateWrapper(this.original, patchInfo);
				HarmonySharedState.UpdatePatchInfo(this.original, methodInfo, patchInfo);
				result = methodInfo;
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public PatchProcessor Unpatch(HarmonyPatchType type, string harmonyID)
		{
			object obj = PatchProcessor.locker;
			lock (obj)
			{
				PatchInfo patchInfo = HarmonySharedState.GetPatchInfo(this.original);
				if (patchInfo == null)
				{
					patchInfo = new PatchInfo();
				}
				if (type == HarmonyPatchType.All || type == HarmonyPatchType.Prefix)
				{
					patchInfo.RemovePrefix(harmonyID);
				}
				if (type == HarmonyPatchType.All || type == HarmonyPatchType.Postfix)
				{
					patchInfo.RemovePostfix(harmonyID);
				}
				if (type == HarmonyPatchType.All || type == HarmonyPatchType.Transpiler)
				{
					patchInfo.RemoveTranspiler(harmonyID);
				}
				if (type == HarmonyPatchType.All || type == HarmonyPatchType.Finalizer)
				{
					patchInfo.RemoveFinalizer(harmonyID);
				}
				MethodInfo replacement = PatchFunctions.UpdateWrapper(this.original, patchInfo);
				HarmonySharedState.UpdatePatchInfo(this.original, replacement, patchInfo);
			}
			return this;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public PatchProcessor Unpatch(MethodInfo patch)
		{
			object obj = PatchProcessor.locker;
			lock (obj)
			{
				PatchInfo patchInfo = HarmonySharedState.GetPatchInfo(this.original);
				if (patchInfo == null)
				{
					patchInfo = new PatchInfo();
				}
				patchInfo.RemovePatch(patch);
				MethodInfo replacement = PatchFunctions.UpdateWrapper(this.original, patchInfo);
				HarmonySharedState.UpdatePatchInfo(this.original, replacement, patchInfo);
			}
			return this;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public static Patches GetPatchInfo(MethodBase method)
		{
			object obj = PatchProcessor.locker;
			PatchInfo patchInfo;
			lock (obj)
			{
				patchInfo = HarmonySharedState.GetPatchInfo(method);
			}
			if (patchInfo == null)
			{
				return null;
			}
			return new Patches(patchInfo.prefixes, patchInfo.postfixes, patchInfo.transpilers, patchInfo.finalizers);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000CB90 File Offset: 0x0000AD90
		public static List<MethodInfo> GetSortedPatchMethods(MethodBase original, Patch[] patches)
		{
			return PatchFunctions.GetSortedPatchMethods(original, patches, false);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public static Dictionary<string, Version> VersionInfo(out Version currentVersion)
		{
			currentVersion = typeof(Harmony).Assembly.GetName().Version;
			Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
			Action<Patch> <>9__2;
			Action<Patch> <>9__3;
			Action<Patch> <>9__4;
			Action<Patch> <>9__5;
			PatchProcessor.GetAllPatchedMethods().Do(delegate(MethodBase method)
			{
				object obj = PatchProcessor.locker;
				PatchInfo patchInfo;
				lock (obj)
				{
					patchInfo = HarmonySharedState.GetPatchInfo(method);
				}
				IEnumerable<Patch> prefixes = patchInfo.prefixes;
				Action<Patch> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(Patch fix)
					{
						assemblies[fix.owner] = fix.PatchMethod.DeclaringType.Assembly;
					});
				}
				prefixes.Do(action);
				IEnumerable<Patch> postfixes = patchInfo.postfixes;
				Action<Patch> action2;
				if ((action2 = <>9__3) == null)
				{
					action2 = (<>9__3 = delegate(Patch fix)
					{
						assemblies[fix.owner] = fix.PatchMethod.DeclaringType.Assembly;
					});
				}
				postfixes.Do(action2);
				IEnumerable<Patch> transpilers = patchInfo.transpilers;
				Action<Patch> action3;
				if ((action3 = <>9__4) == null)
				{
					action3 = (<>9__4 = delegate(Patch fix)
					{
						assemblies[fix.owner] = fix.PatchMethod.DeclaringType.Assembly;
					});
				}
				transpilers.Do(action3);
				IEnumerable<Patch> finalizers = patchInfo.finalizers;
				Action<Patch> action4;
				if ((action4 = <>9__5) == null)
				{
					action4 = (<>9__5 = delegate(Patch fix)
					{
						assemblies[fix.owner] = fix.PatchMethod.DeclaringType.Assembly;
					});
				}
				finalizers.Do(action4);
			});
			Dictionary<string, Version> result = new Dictionary<string, Version>();
			assemblies.Do(delegate(KeyValuePair<string, Assembly> info)
			{
				AssemblyName assemblyName = info.Value.GetReferencedAssemblies().FirstOrDefault((AssemblyName a) => a.FullName.StartsWith("0Harmony, Version", StringComparison.Ordinal));
				if (assemblyName != null)
				{
					result[info.Key] = assemblyName.Version;
				}
			});
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public static ILGenerator CreateILGenerator()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
			defaultInterpolatedStringHandler.AppendLiteral("ILGenerator_");
			defaultInterpolatedStringHandler.AppendFormatted<Guid>(Guid.NewGuid());
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(defaultInterpolatedStringHandler.ToStringAndClear(), typeof(void), Array.Empty<Type>());
			return dynamicMethodDefinition.GetILGenerator();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public static ILGenerator CreateILGenerator(MethodBase original)
		{
			MethodInfo methodInfo = original as MethodInfo;
			Type returnType = (methodInfo != null) ? methodInfo.ReturnType : typeof(void);
			List<Type> list = (from pi in original.GetParameters()
			select pi.ParameterType).ToList<Type>();
			if (!original.IsStatic)
			{
				list.Insert(0, original.DeclaringType);
			}
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("ILGenerator_" + original.Name, returnType, list.ToArray());
			return dynamicMethodDefinition.GetILGenerator();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		public static List<CodeInstruction> GetOriginalInstructions(MethodBase original, ILGenerator generator = null)
		{
			return MethodCopier.GetInstructions(generator ?? PatchProcessor.CreateILGenerator(original), original, 0);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		public static List<CodeInstruction> GetOriginalInstructions(MethodBase original, out ILGenerator generator)
		{
			generator = PatchProcessor.CreateILGenerator(original);
			return MethodCopier.GetInstructions(generator, original, 0);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000CD21 File Offset: 0x0000AF21
		public static List<CodeInstruction> GetCurrentInstructions(MethodBase original, int maxTranspilers = 2147483647, ILGenerator generator = null)
		{
			return MethodCopier.GetInstructions(generator ?? PatchProcessor.CreateILGenerator(original), original, maxTranspilers);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000CD35 File Offset: 0x0000AF35
		public static List<CodeInstruction> GetCurrentInstructions(MethodBase original, out ILGenerator generator, int maxTranspilers = 2147483647)
		{
			generator = PatchProcessor.CreateILGenerator(original);
			return MethodCopier.GetInstructions(generator, original, maxTranspilers);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public static IEnumerable<KeyValuePair<OpCode, object>> ReadMethodBody(MethodBase method)
		{
			return from instr in MethodBodyReader.GetInstructions(PatchProcessor.CreateILGenerator(method), method)
			select new KeyValuePair<OpCode, object>(instr.opcode, instr.operand);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		public static IEnumerable<KeyValuePair<OpCode, object>> ReadMethodBody(MethodBase method, ILGenerator generator)
		{
			return from instr in MethodBodyReader.GetInstructions(generator, method)
			select new KeyValuePair<OpCode, object>(instr.opcode, instr.operand);
		}

		// Token: 0x040000B5 RID: 181
		private readonly Harmony instance = instance;

		// Token: 0x040000B6 RID: 182
		private readonly MethodBase original = original;

		// Token: 0x040000B7 RID: 183
		private HarmonyMethod prefix;

		// Token: 0x040000B8 RID: 184
		private HarmonyMethod postfix;

		// Token: 0x040000B9 RID: 185
		private HarmonyMethod transpiler;

		// Token: 0x040000BA RID: 186
		private HarmonyMethod finalizer;

		// Token: 0x040000BB RID: 187
		internal static readonly object locker = new object();
	}
}
