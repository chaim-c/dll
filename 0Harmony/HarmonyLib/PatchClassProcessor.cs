using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000040 RID: 64
	public class PatchClassProcessor
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000B894 File Offset: 0x00009A94
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000B89C File Offset: 0x00009A9C
		public string Category { get; set; }

		// Token: 0x06000183 RID: 387 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		public PatchClassProcessor(Harmony instance, Type type)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.instance = instance;
			this.containerType = type;
			List<HarmonyMethod> fromType = HarmonyMethodExtensions.GetFromType(type);
			if (fromType == null || fromType.Count == 0)
			{
				return;
			}
			this.containerAttributes = HarmonyMethod.Merge(fromType);
			HarmonyMethod harmonyMethod = this.containerAttributes;
			MethodType value = harmonyMethod.methodType.GetValueOrDefault();
			if (harmonyMethod.methodType == null)
			{
				value = MethodType.Normal;
				harmonyMethod.methodType = new MethodType?(value);
			}
			this.Category = this.containerAttributes.category;
			this.auxilaryMethods = new Dictionary<Type, MethodInfo>();
			foreach (Type type2 in PatchClassProcessor.auxilaryTypes)
			{
				MethodInfo patchMethod = PatchTools.GetPatchMethod(this.containerType, type2.FullName);
				if (patchMethod != null)
				{
					this.auxilaryMethods[type2] = patchMethod;
				}
			}
			this.patchMethods = PatchTools.GetPatchMethods(this.containerType);
			foreach (AttributePatch attributePatch in this.patchMethods)
			{
				MethodInfo method = attributePatch.info.method;
				attributePatch.info = this.containerAttributes.Merge(attributePatch.info);
				attributePatch.info.method = method;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BA38 File Offset: 0x00009C38
		public List<MethodInfo> Patch()
		{
			if (this.containerAttributes == null)
			{
				return null;
			}
			Exception ex = null;
			if (!this.RunMethod<HarmonyPrepare, bool>(true, false, null, Array.Empty<object>()))
			{
				this.RunMethod<HarmonyCleanup>(ref ex, Array.Empty<object>());
				this.ReportException(ex, null);
				return new List<MethodInfo>();
			}
			List<MethodInfo> result = new List<MethodInfo>();
			MethodBase original = null;
			try
			{
				List<MethodBase> bulkMethods = this.GetBulkMethods();
				if (bulkMethods.Count == 1)
				{
					original = bulkMethods[0];
				}
				this.ReversePatch(ref original);
				result = ((bulkMethods.Count > 0) ? this.BulkPatch(bulkMethods, ref original, false) : this.PatchWithAttributes(ref original, false));
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			this.RunMethod<HarmonyCleanup>(ref ex, new object[]
			{
				ex
			});
			this.ReportException(ex, original);
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BB00 File Offset: 0x00009D00
		public void Unpatch()
		{
			if (this.containerAttributes == null)
			{
				return;
			}
			List<MethodBase> bulkMethods = this.GetBulkMethods();
			MethodBase methodBase = null;
			if (bulkMethods.Count > 0)
			{
				this.BulkPatch(bulkMethods, ref methodBase, true);
				return;
			}
			this.PatchWithAttributes(ref methodBase, true);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BB40 File Offset: 0x00009D40
		private void ReversePatch(ref MethodBase lastOriginal)
		{
			for (int i = 0; i < this.patchMethods.Count; i++)
			{
				AttributePatch attributePatch = this.patchMethods[i];
				HarmonyPatchType? type = attributePatch.type;
				HarmonyPatchType harmonyPatchType = HarmonyPatchType.ReversePatch;
				if (type.GetValueOrDefault() == harmonyPatchType & type != null)
				{
					MethodBase originalMethod = attributePatch.info.GetOriginalMethod();
					if (originalMethod != null)
					{
						lastOriginal = originalMethod;
					}
					ReversePatcher reversePatcher = this.instance.CreateReversePatcher(lastOriginal, attributePatch.info);
					object locker = PatchProcessor.locker;
					lock (locker)
					{
						reversePatcher.Patch(HarmonyReversePatchType.Original);
					}
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		private List<MethodInfo> BulkPatch(List<MethodBase> originals, ref MethodBase lastOriginal, bool unpatch)
		{
			PatchJobs<MethodInfo> patchJobs = new PatchJobs<MethodInfo>();
			for (int i = 0; i < originals.Count; i++)
			{
				lastOriginal = originals[i];
				PatchJobs<MethodInfo>.Job job = patchJobs.GetJob(lastOriginal);
				foreach (AttributePatch attributePatch in this.patchMethods)
				{
					string text = "You cannot combine TargetMethod, TargetMethods or [HarmonyPatchAll] with individual annotations";
					HarmonyMethod info = attributePatch.info;
					if (info.methodName != null)
					{
						throw new ArgumentException(text + " [" + info.methodName + "]");
					}
					if (info.methodType != null && info.methodType.Value != MethodType.Normal)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
						defaultInterpolatedStringHandler.AppendFormatted(text);
						defaultInterpolatedStringHandler.AppendLiteral(" [");
						defaultInterpolatedStringHandler.AppendFormatted<MethodType?>(info.methodType);
						defaultInterpolatedStringHandler.AppendLiteral("]");
						throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					if (info.argumentTypes != null)
					{
						throw new ArgumentException(text + " [" + info.argumentTypes.Description() + "]");
					}
					job.AddPatch(attributePatch);
				}
			}
			foreach (PatchJobs<MethodInfo>.Job job2 in patchJobs.GetJobs())
			{
				lastOriginal = job2.original;
				if (unpatch)
				{
					this.ProcessUnpatchJob(job2);
				}
				else
				{
					this.ProcessPatchJob(job2);
				}
			}
			return patchJobs.GetReplacements();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		private List<MethodInfo> PatchWithAttributes(ref MethodBase lastOriginal, bool unpatch)
		{
			PatchJobs<MethodInfo> patchJobs = new PatchJobs<MethodInfo>();
			foreach (AttributePatch attributePatch in this.patchMethods)
			{
				lastOriginal = attributePatch.info.GetOriginalMethod();
				if (lastOriginal == null)
				{
					throw new ArgumentException("Undefined target method for patch method " + attributePatch.info.method.FullDescription());
				}
				PatchJobs<MethodInfo>.Job job = patchJobs.GetJob(lastOriginal);
				job.AddPatch(attributePatch);
			}
			foreach (PatchJobs<MethodInfo>.Job job2 in patchJobs.GetJobs())
			{
				lastOriginal = job2.original;
				if (unpatch)
				{
					this.ProcessUnpatchJob(job2);
				}
				else
				{
					this.ProcessPatchJob(job2);
				}
			}
			return patchJobs.GetReplacements();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BE9C File Offset: 0x0000A09C
		private void ProcessPatchJob(PatchJobs<MethodInfo>.Job job)
		{
			MethodInfo replacement = null;
			bool flag = this.RunMethod<HarmonyPrepare, bool>(true, false, null, new object[]
			{
				job.original
			});
			Exception ex = null;
			if (flag)
			{
				object locker = PatchProcessor.locker;
				lock (locker)
				{
					try
					{
						PatchInfo patchInfo = HarmonySharedState.GetPatchInfo(job.original) ?? new PatchInfo();
						patchInfo.AddPrefixes(this.instance.Id, job.prefixes.ToArray());
						patchInfo.AddPostfixes(this.instance.Id, job.postfixes.ToArray());
						patchInfo.AddTranspilers(this.instance.Id, job.transpilers.ToArray());
						patchInfo.AddFinalizers(this.instance.Id, job.finalizers.ToArray());
						replacement = PatchFunctions.UpdateWrapper(job.original, patchInfo);
						HarmonySharedState.UpdatePatchInfo(job.original, replacement, patchInfo);
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
				}
			}
			this.RunMethod<HarmonyCleanup>(ref ex, new object[]
			{
				job.original,
				ex
			});
			this.ReportException(ex, job.original);
			job.replacement = replacement;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		private void ProcessUnpatchJob(PatchJobs<MethodInfo>.Job job)
		{
			PatchInfo patchInfo = HarmonySharedState.GetPatchInfo(job.original) ?? new PatchInfo();
			bool flag = job.original.HasMethodBody();
			if (flag)
			{
				job.postfixes.Do(delegate(HarmonyMethod patch)
				{
					patchInfo.RemovePatch(patch.method);
				});
				job.prefixes.Do(delegate(HarmonyMethod patch)
				{
					patchInfo.RemovePatch(patch.method);
				});
			}
			job.transpilers.Do(delegate(HarmonyMethod patch)
			{
				patchInfo.RemovePatch(patch.method);
			});
			if (flag)
			{
				job.finalizers.Do(delegate(HarmonyMethod patch)
				{
					patchInfo.RemovePatch(patch.method);
				});
			}
			MethodInfo replacement = PatchFunctions.UpdateWrapper(job.original, patchInfo);
			HarmonySharedState.UpdatePatchInfo(job.original, replacement, patchInfo);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		private List<MethodBase> GetBulkMethods()
		{
			bool flag = this.containerType.GetCustomAttributes(true).Any((object a) => a.GetType().FullName == PatchTools.harmonyPatchAllFullName);
			if (flag)
			{
				Type declaringType = this.containerAttributes.declaringType;
				if (declaringType == null)
				{
					throw new ArgumentException("Using " + PatchTools.harmonyPatchAllFullName + " requires an additional attribute for specifying the Class/Type");
				}
				List<MethodBase> list = new List<MethodBase>();
				list.AddRange(AccessTools.GetDeclaredConstructors(declaringType, null).Cast<MethodBase>());
				list.AddRange(AccessTools.GetDeclaredMethods(declaringType).Cast<MethodBase>());
				List<PropertyInfo> declaredProperties = AccessTools.GetDeclaredProperties(declaringType);
				list.AddRange((from prop in declaredProperties
				select prop.GetGetMethod(true) into method
				where method != null
				select method).Cast<MethodBase>());
				list.AddRange((from prop in declaredProperties
				select prop.GetSetMethod(true) into method
				where method != null
				select method).Cast<MethodBase>());
				return list;
			}
			else
			{
				List<MethodBase> list2 = new List<MethodBase>();
				IEnumerable<MethodBase> enumerable = this.RunMethod<HarmonyTargetMethods, IEnumerable<MethodBase>>(null, null, null, Array.Empty<object>());
				if (enumerable == null)
				{
					MethodBase methodBase = this.RunMethod<HarmonyTargetMethod, MethodBase>(null, null, delegate(MethodBase method)
					{
						if (method != null)
						{
							return null;
						}
						return "null";
					}, Array.Empty<object>());
					if (methodBase != null)
					{
						list2.Add(methodBase);
					}
					return list2;
				}
				string text = null;
				list2 = enumerable.ToList<MethodBase>();
				if (list2 == null)
				{
					text = "null";
				}
				else if (list2.Any((MethodBase m) => m == null))
				{
					text = "some element was null";
				}
				if (text == null)
				{
					return list2;
				}
				MethodInfo member;
				if (this.auxilaryMethods.TryGetValue(typeof(HarmonyTargetMethods), out member))
				{
					throw new Exception("Method " + member.FullDescription() + " returned an unexpected result: " + text);
				}
				throw new Exception("Some method returned an unexpected result: " + text);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private void ReportException(Exception exception, MethodBase original)
		{
			if (exception == null)
			{
				return;
			}
			if (this.containerAttributes.debug.GetValueOrDefault() || Harmony.DEBUG)
			{
				Version value;
				Harmony.VersionInfo(out value);
				FileLog.indentLevel = 0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
				defaultInterpolatedStringHandler.AppendLiteral("### Exception from user \"");
				defaultInterpolatedStringHandler.AppendFormatted(this.instance.Id);
				defaultInterpolatedStringHandler.AppendLiteral("\", Harmony v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(value);
				FileLog.Log(defaultInterpolatedStringHandler.ToStringAndClear());
				FileLog.Log("### Original: " + (((original != null) ? original.FullDescription() : null) ?? "NULL"));
				FileLog.Log("### Patch class: " + this.containerType.FullDescription());
				Exception ex = exception;
				HarmonyException ex2 = ex as HarmonyException;
				if (ex2 != null)
				{
					ex = ex2.InnerException;
				}
				string text = ex.ToString();
				while (text.Contains("\n\n"))
				{
					text = text.Replace("\n\n", "\n");
				}
				text = text.Split(new char[]
				{
					'\n'
				}).Join((string line) => "### " + line, "\n");
				FileLog.Log(text.Trim());
			}
			if (exception is HarmonyException)
			{
				throw exception;
			}
			throw new HarmonyException("Patching exception in method " + original.FullDescription(), exception);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C44C File Offset: 0x0000A64C
		private T RunMethod<S, T>(T defaultIfNotExisting, T defaultIfFailing, Func<T, string> failOnResult = null, params object[] parameters)
		{
			MethodInfo methodInfo;
			if (!this.auxilaryMethods.TryGetValue(typeof(S), out methodInfo))
			{
				return defaultIfNotExisting;
			}
			object[] inputs = (parameters ?? Array.Empty<object>()).Union(new object[]
			{
				this.instance
			}).ToArray<object>();
			object[] parameters2 = AccessTools.ActualParameters(methodInfo, inputs);
			if (methodInfo.ReturnType != typeof(void) && !typeof(T).IsAssignableFrom(methodInfo.ReturnType))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Method ");
				defaultInterpolatedStringHandler.AppendFormatted(methodInfo.FullDescription());
				defaultInterpolatedStringHandler.AppendLiteral(" has wrong return type (should be assignable to ");
				defaultInterpolatedStringHandler.AppendFormatted(typeof(T).FullName);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			T t = defaultIfFailing;
			try
			{
				if (methodInfo.ReturnType == typeof(void))
				{
					methodInfo.Invoke(null, parameters2);
					t = defaultIfNotExisting;
				}
				else
				{
					t = (T)((object)methodInfo.Invoke(null, parameters2));
				}
				if (failOnResult != null)
				{
					string text = failOnResult(t);
					if (text != null)
					{
						throw new Exception("Method " + methodInfo.FullDescription() + " returned an unexpected result: " + text);
					}
				}
			}
			catch (Exception exception)
			{
				this.ReportException(exception, methodInfo);
			}
			return t;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		private void RunMethod<S>(ref Exception exception, params object[] parameters)
		{
			MethodInfo methodInfo;
			if (this.auxilaryMethods.TryGetValue(typeof(S), out methodInfo))
			{
				object[] inputs = (parameters ?? Array.Empty<object>()).Union(new object[]
				{
					this.instance
				}).ToArray<object>();
				object[] parameters2 = AccessTools.ActualParameters(methodInfo, inputs);
				try
				{
					object obj = methodInfo.Invoke(null, parameters2);
					if (methodInfo.ReturnType == typeof(Exception))
					{
						exception = (obj as Exception);
					}
				}
				catch (Exception exception2)
				{
					this.ReportException(exception2, methodInfo);
				}
			}
		}

		// Token: 0x040000AA RID: 170
		private readonly Harmony instance;

		// Token: 0x040000AB RID: 171
		private readonly Type containerType;

		// Token: 0x040000AC RID: 172
		private readonly HarmonyMethod containerAttributes;

		// Token: 0x040000AD RID: 173
		private readonly Dictionary<Type, MethodInfo> auxilaryMethods;

		// Token: 0x040000AE RID: 174
		private readonly List<AttributePatch> patchMethods;

		// Token: 0x040000AF RID: 175
		private static readonly List<Type> auxilaryTypes = new List<Type>(4)
		{
			typeof(HarmonyPrepare),
			typeof(HarmonyCleanup),
			typeof(HarmonyTargetMethod),
			typeof(HarmonyTargetMethods)
		};
	}
}
