using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class Patch : IComparable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B588 File Offset: 0x00009788
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000B620 File Offset: 0x00009820
		public MethodInfo PatchMethod
		{
			get
			{
				if (this.patchMethod == null)
				{
					Module module = (from a in AppDomain.CurrentDomain.GetAssemblies()
					where !a.FullName.StartsWith("Microsoft.VisualStudio")
					select a).SelectMany((Assembly a) => a.GetLoadedModules()).First((Module m) => m.ModuleVersionId.ToString() == this.moduleGUID);
					this.patchMethod = (MethodInfo)module.ResolveMethod(this.methodToken);
				}
				return this.patchMethod;
			}
			set
			{
				this.patchMethod = value;
				this.methodToken = this.patchMethod.MetadataToken;
				this.moduleGUID = this.patchMethod.Module.ModuleVersionId.ToString();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B66C File Offset: 0x0000986C
		public Patch(MethodInfo patch, int index, string owner, int priority, string[] before, string[] after, bool debug)
		{
			if (patch is DynamicMethod)
			{
				throw new Exception("Cannot directly reference dynamic method \"" + patch.FullDescription() + "\" in Harmony. Use a factory method instead that will return the dynamic method.");
			}
			this.index = index;
			this.owner = owner;
			this.priority = ((priority == -1) ? 400 : priority);
			this.before = (before ?? Array.Empty<string>());
			this.after = (after ?? Array.Empty<string>());
			this.debug = debug;
			this.PatchMethod = patch;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B6F5 File Offset: 0x000098F5
		public Patch(HarmonyMethod method, int index, string owner) : this(method.method, index, owner, method.priority, method.before, method.after, method.debug.GetValueOrDefault())
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B724 File Offset: 0x00009924
		internal Patch(int index, string owner, int priority, string[] before, string[] after, bool debug, int methodToken, string moduleGUID)
		{
			this.index = index;
			this.owner = owner;
			this.priority = ((priority == -1) ? 400 : priority);
			this.before = (before ?? Array.Empty<string>());
			this.after = (after ?? Array.Empty<string>());
			this.debug = debug;
			this.methodToken = methodToken;
			this.moduleGUID = moduleGUID;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B794 File Offset: 0x00009994
		public MethodInfo GetMethod(MethodBase original)
		{
			MethodInfo methodInfo = this.PatchMethod;
			if (methodInfo.ReturnType != typeof(DynamicMethod) && methodInfo.ReturnType != typeof(MethodInfo))
			{
				return methodInfo;
			}
			if (!methodInfo.IsStatic)
			{
				return methodInfo;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length != 1)
			{
				return methodInfo;
			}
			if (parameters[0].ParameterType != typeof(MethodBase))
			{
				return methodInfo;
			}
			return methodInfo.Invoke(null, new object[]
			{
				original
			}) as MethodInfo;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B822 File Offset: 0x00009A22
		public override bool Equals(object obj)
		{
			return obj != null && obj is Patch && this.PatchMethod == ((Patch)obj).PatchMethod;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B847 File Offset: 0x00009A47
		public int CompareTo(object obj)
		{
			return PatchInfoSerialization.PriorityComparer(obj, this.index, this.priority);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B85B File Offset: 0x00009A5B
		public override int GetHashCode()
		{
			return this.PatchMethod.GetHashCode();
		}

		// Token: 0x040000A1 RID: 161
		public readonly int index;

		// Token: 0x040000A2 RID: 162
		public readonly string owner;

		// Token: 0x040000A3 RID: 163
		public readonly int priority;

		// Token: 0x040000A4 RID: 164
		public readonly string[] before;

		// Token: 0x040000A5 RID: 165
		public readonly string[] after;

		// Token: 0x040000A6 RID: 166
		public readonly bool debug;

		// Token: 0x040000A7 RID: 167
		[NonSerialized]
		private MethodInfo patchMethod;

		// Token: 0x040000A8 RID: 168
		private int methodToken;

		// Token: 0x040000A9 RID: 169
		private string moduleGUID;
	}
}
