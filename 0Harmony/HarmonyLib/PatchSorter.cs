using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x0200001B RID: 27
	internal class PatchSorter
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00008748 File Offset: 0x00006948
		internal PatchSorter(Patch[] patches, bool debug)
		{
			this.patches = (from x in patches
			select new PatchSorter.PatchSortingWrapper(x)).ToList<PatchSorter.PatchSortingWrapper>();
			this.debug = debug;
			using (List<PatchSorter.PatchSortingWrapper>.Enumerator enumerator = this.patches.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PatchSorter.PatchSortingWrapper node = enumerator.Current;
					node.AddBeforeDependency(from x in this.patches
					where node.innerPatch.before.Contains(x.innerPatch.owner)
					select x);
					node.AddAfterDependency(from x in this.patches
					where node.innerPatch.after.Contains(x.innerPatch.owner)
					select x);
				}
			}
			this.patches.Sort();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000882C File Offset: 0x00006A2C
		internal List<MethodInfo> Sort(MethodBase original)
		{
			if (this.sortedPatchArray != null)
			{
				return (from x in this.sortedPatchArray
				select x.GetMethod(original)).ToList<MethodInfo>();
			}
			this.handledPatches = new HashSet<PatchSorter.PatchSortingWrapper>();
			this.waitingList = new List<PatchSorter.PatchSortingWrapper>();
			this.result = new List<PatchSorter.PatchSortingWrapper>(this.patches.Count);
			Queue<PatchSorter.PatchSortingWrapper> queue = new Queue<PatchSorter.PatchSortingWrapper>(this.patches);
			Func<PatchSorter.PatchSortingWrapper, bool> <>9__3;
			while (queue.Count != 0)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in queue)
				{
					IEnumerable<PatchSorter.PatchSortingWrapper> after = patchSortingWrapper.after;
					Func<PatchSorter.PatchSortingWrapper, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((PatchSorter.PatchSortingWrapper x) => this.handledPatches.Contains(x)));
					}
					if (after.All(predicate))
					{
						this.AddNodeToResult(patchSortingWrapper);
						if (patchSortingWrapper.before.Count != 0)
						{
							this.ProcessWaitingList();
						}
					}
					else
					{
						this.waitingList.Add(patchSortingWrapper);
					}
				}
				this.CullDependency();
				queue = new Queue<PatchSorter.PatchSortingWrapper>(this.waitingList);
				this.waitingList.Clear();
			}
			this.sortedPatchArray = (from x in this.result
			select x.innerPatch).ToArray<Patch>();
			this.handledPatches = null;
			this.waitingList = null;
			this.patches = null;
			return (from x in this.sortedPatchArray
			select x.GetMethod(original)).ToList<MethodInfo>();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal bool ComparePatchLists(Patch[] patches)
		{
			if (this.sortedPatchArray == null)
			{
				this.Sort(null);
			}
			return patches != null && this.sortedPatchArray.Length == patches.Length && this.sortedPatchArray.All((Patch x) => patches.Contains(x, new PatchSorter.PatchDetailedComparer()));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008A30 File Offset: 0x00006C30
		private void CullDependency()
		{
			for (int i = this.waitingList.Count - 1; i >= 0; i--)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in this.waitingList[i].after)
				{
					if (!this.handledPatches.Contains(patchSortingWrapper))
					{
						this.waitingList[i].RemoveAfterDependency(patchSortingWrapper);
						if (this.debug)
						{
							string str = patchSortingWrapper.innerPatch.PatchMethod.FullDescription();
							string str2 = this.waitingList[i].innerPatch.PatchMethod.FullDescription();
							FileLog.LogBuffered("Breaking dependance between " + str + " and " + str2);
						}
						return;
					}
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008B14 File Offset: 0x00006D14
		private void ProcessWaitingList()
		{
			int num = this.waitingList.Count;
			int i = 0;
			while (i < num)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = this.waitingList[i];
				if (patchSortingWrapper.after.All(new Func<PatchSorter.PatchSortingWrapper, bool>(this.handledPatches.Contains)))
				{
					this.waitingList.Remove(patchSortingWrapper);
					this.AddNodeToResult(patchSortingWrapper);
					num--;
					i = 0;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008B81 File Offset: 0x00006D81
		private void AddNodeToResult(PatchSorter.PatchSortingWrapper node)
		{
			this.result.Add(node);
			this.handledPatches.Add(node);
		}

		// Token: 0x0400004F RID: 79
		private List<PatchSorter.PatchSortingWrapper> patches;

		// Token: 0x04000050 RID: 80
		private HashSet<PatchSorter.PatchSortingWrapper> handledPatches;

		// Token: 0x04000051 RID: 81
		private List<PatchSorter.PatchSortingWrapper> result;

		// Token: 0x04000052 RID: 82
		private List<PatchSorter.PatchSortingWrapper> waitingList;

		// Token: 0x04000053 RID: 83
		internal Patch[] sortedPatchArray;

		// Token: 0x04000054 RID: 84
		private readonly bool debug;

		// Token: 0x02000074 RID: 116
		private class PatchSortingWrapper : IComparable
		{
			// Token: 0x06000477 RID: 1143 RVA: 0x00016483 File Offset: 0x00014683
			internal PatchSortingWrapper(Patch patch)
			{
				this.innerPatch = patch;
				this.before = new HashSet<PatchSorter.PatchSortingWrapper>();
				this.after = new HashSet<PatchSorter.PatchSortingWrapper>();
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x000164A8 File Offset: 0x000146A8
			public int CompareTo(object obj)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = obj as PatchSorter.PatchSortingWrapper;
				return PatchInfoSerialization.PriorityComparer((patchSortingWrapper != null) ? patchSortingWrapper.innerPatch : null, this.innerPatch.index, this.innerPatch.priority);
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x000164E4 File Offset: 0x000146E4
			public override bool Equals(object obj)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = obj as PatchSorter.PatchSortingWrapper;
				return patchSortingWrapper != null && this.innerPatch.PatchMethod == patchSortingWrapper.innerPatch.PatchMethod;
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x00016518 File Offset: 0x00014718
			public override int GetHashCode()
			{
				return this.innerPatch.PatchMethod.GetHashCode();
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x0001652C File Offset: 0x0001472C
			internal void AddBeforeDependency(IEnumerable<PatchSorter.PatchSortingWrapper> dependencies)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in dependencies)
				{
					this.before.Add(patchSortingWrapper);
					patchSortingWrapper.after.Add(this);
				}
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x00016588 File Offset: 0x00014788
			internal void AddAfterDependency(IEnumerable<PatchSorter.PatchSortingWrapper> dependencies)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in dependencies)
				{
					this.after.Add(patchSortingWrapper);
					patchSortingWrapper.before.Add(this);
				}
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x000165E4 File Offset: 0x000147E4
			internal void RemoveAfterDependency(PatchSorter.PatchSortingWrapper afterNode)
			{
				this.after.Remove(afterNode);
				afterNode.before.Remove(this);
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x00016600 File Offset: 0x00014800
			internal void RemoveBeforeDependency(PatchSorter.PatchSortingWrapper beforeNode)
			{
				this.before.Remove(beforeNode);
				beforeNode.after.Remove(this);
			}

			// Token: 0x04000170 RID: 368
			internal readonly HashSet<PatchSorter.PatchSortingWrapper> after;

			// Token: 0x04000171 RID: 369
			internal readonly HashSet<PatchSorter.PatchSortingWrapper> before;

			// Token: 0x04000172 RID: 370
			internal readonly Patch innerPatch;
		}

		// Token: 0x02000075 RID: 117
		internal class PatchDetailedComparer : IEqualityComparer<Patch>
		{
			// Token: 0x0600047F RID: 1151 RVA: 0x0001661C File Offset: 0x0001481C
			public bool Equals(Patch x, Patch y)
			{
				return y != null && x != null && x.owner == y.owner && x.PatchMethod == y.PatchMethod && x.index == y.index && x.priority == y.priority && x.before.Length == y.before.Length && x.after.Length == y.after.Length && x.before.All(new Func<string, bool>(y.before.Contains<string>)) && x.after.All(new Func<string, bool>(y.after.Contains<string>));
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x000166DA File Offset: 0x000148DA
			public int GetHashCode(Patch obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
