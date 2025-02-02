using System;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000155 RID: 341
	[NullableContext(2)]
	[Nullable(0)]
	internal class Traverse2<T>
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00020FF9 File Offset: 0x0001F1F9
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x00021006 File Offset: 0x0001F206
		public T Value
		{
			get
			{
				return this._traverse.GetValue<T>();
			}
			set
			{
				this._traverse.SetValue(value);
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002101A File Offset: 0x0001F21A
		private Traverse2()
		{
			this._traverse = new Traverse2(null);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00021030 File Offset: 0x0001F230
		[NullableContext(1)]
		public Traverse2(Traverse2 traverse)
		{
			this._traverse = traverse;
		}

		// Token: 0x0400029E RID: 670
		[Nullable(1)]
		private readonly Traverse2 _traverse;
	}
}
