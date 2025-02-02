using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000118 RID: 280
	[ExcludeFromCodeCoverage]
	internal sealed class ImmutableList<T>
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00015010 File Offset: 0x00013210
		public ImmutableList(ImmutableList<T> previousList, T value)
		{
			this.Items = new T[previousList.Items.Length + 1];
			Array.Copy(previousList.Items, this.Items, previousList.Items.Length);
			this.Items[this.Items.Length - 1] = value;
			this.Count = this.Items.Length;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015078 File Offset: 0x00013278
		private ImmutableList()
		{
			this.Items = new T[0];
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001508D File Offset: 0x0001328D
		public ImmutableList<T> Add(T value)
		{
			return new ImmutableList<T>(this, value);
		}

		// Token: 0x040001F1 RID: 497
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x040001F2 RID: 498
		public readonly T[] Items;

		// Token: 0x040001F3 RID: 499
		public readonly int Count;
	}
}
